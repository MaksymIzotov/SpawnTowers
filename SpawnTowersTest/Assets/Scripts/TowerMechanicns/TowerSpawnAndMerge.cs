using System;
using System.Collections;
using UnityEngine;

public class TowerSpawnAndMerge : MonoBehaviour
{
    #region Tile Class Init
    [Serializable]
    public struct Tile
    {
        public GameObject tile;
        public GameObject tower;
        public bool isBusy;
        public int level;
    }
    #endregion

    [SerializeField] private Tile[] tiles;
    [SerializeField] private GameObject[] towerPrefabs;

    private MoneyController moneyController;
    private TowerPriceController towerPriceController;

    private void Start()
    {
        if (PlayerPrefs.HasKey("IsSaved") == false)
            PlayerPrefs.SetInt("IsSaved", 0);

        moneyController = GetComponent<MoneyController>();
        towerPriceController = GetComponent<TowerPriceController>();

        if (PlayerPrefs.GetInt("IsSaved") == 1)
            SetTowers(SaveLoad.Instance.LoadData());
    }

    public void SetTowers(SaveData[] data)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].isBusy = data[i].isBusy;
            tiles[i].level = data[i].level;

            if (tiles[i].isBusy)
                SpawnTower(i, tiles[i].level);
        }

        SaveLoad.Instance.SaveData(tiles);
    }

    public void BuyTower()
    {
        if (moneyController.GetAmount() < towerPriceController.GetAmount())
            return;

        int index = GetRandomTile();
        if (index == -1) { return; }

        SpawnTower(index, 1);
        moneyController.AddAmount(-towerPriceController.GetAmount());
        towerPriceController.AddAmount(1);

        SaveLoad.Instance.SaveData(tiles);
    }

    private int GetRandomTile()
    {
        int amount = 0;

        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].isBusy == false)
                amount++;
        }

        if (amount == 0)
            return -1;

        int index = UnityEngine.Random.Range(0, amount);
        amount = 0;

        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].isBusy == true) { continue; }

            if (amount == index)
                return i;

                amount++;
        }

        return -1;
    }

    private void SpawnTower(int index, int lvl)
    {

        Vector3 pos = new Vector3(tiles[index].tile.transform.position.x, tiles[index].tile.transform.position.y + 0.7f, tiles[index].tile.transform.position.z);


        GameObject tower = Instantiate(towerPrefabs[lvl-1], pos, Quaternion.identity);
        tiles[index].isBusy = true;
        tiles[index].tower = tower;
        tiles[index].level = lvl;
    }

    public void CheckTile(string name, GameObject tower)
    {
        Tile tileTo = new Tile();

        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].tile.name == name)
                tileTo = tiles[i];
        } 

        if (tileTo.isBusy)
        {
            if (tileTo.tower == tower)
            {
                ReturnTower(tower);
                return;
            }
        }

        if (tileTo.isBusy == false)
            PlaceTower(tower, tileTo);
        else if (tileTo.tower.GetComponent<TowerController>().GetLevel() == tower.GetComponent<TowerController>().GetLevel())
            MergeTowers(tower, tileTo.tower, tileTo);
        else
            ReturnTower(tower);
    }

    private void PlaceTower(GameObject tower, Tile tile)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].tower == tower)
            {
                tiles[i].isBusy = false;
                tiles[i].tower = null;
                tiles[i].level = 0;

                break;
            }
        }

        int index = 0;
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].tile.name == tile.tile.name)
                index = i;
        }

        Vector3 pos = new Vector3(tile.tile.transform.position.x, tile.tile.transform.position.y + 0.7f, tile.tile.transform.position.z);

        tower.transform.position = pos;

        tiles[index].isBusy = true;
        tiles[index].tower = tower;
        tiles[index].level = tower.GetComponent<TowerController>().GetLevel();
        SaveLoad.Instance.SaveData(tiles);
    }

    private void ReturnTower(GameObject tower)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].tower == tower)
            {
                Vector3 pos = new Vector3(tiles[i].tile.transform.position.x, tiles[i].tile.transform.position.y + 0.7f, tiles[i].tile.transform.position.z);

                tower.transform.position = pos;

                tiles[i].isBusy = true;
                tiles[i].tower = tower;
                tiles[i].level = tower.GetComponent<TowerController>().GetLevel();

                break;
            }
        }
    }

    private void MergeTowers(GameObject tower1, GameObject tower2, Tile tile)
    {
        if(tower1.GetComponent<TowerController>().GetLevel() + 1 > towerPrefabs.Length)
        {
            ReturnTower(tower1);
            return;
        }    

        for (int i = 0; i < tiles.Length; i++)
        {
            if(tiles[i].tower == tile.tower)
            {
                tiles[i].level++;
                SpawnTower(i, tiles[i].level);
            }
        }

        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].tower == tower1)
            {
                tiles[i].isBusy = false;
                tiles[i].tower = null;
                tiles[i].level = 0;

                break;
            }
        }

        Destroy(tower1);
        Destroy(tower2);

        SaveLoad.Instance.SaveData(tiles);
    }

}
