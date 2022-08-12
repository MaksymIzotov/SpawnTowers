using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour, IShootable
{
    public void Shoot(Transform target, TowerData data)
    {
        if (target == null) return;
        if (DragAndDrop.Instance.GetIsDragged(transform) == true) return;

        GameObject bullet = Instantiate(data.bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<BulletController>().SetDamage(data.damage);
    }
}
