using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDoubleShoot : MonoBehaviour, IShootable
{
    [SerializeField] private Transform gun1, gun2;

    public void Shoot(Transform target, TowerData data)
    {
        if (target == null) return;
        if (DragAndDrop.Instance.GetIsDragged(transform) == true) return;

        GameObject bullet1 = Instantiate(data.bulletPrefab, gun1.position, transform.rotation);
        GameObject bullet2 = Instantiate(data.bulletPrefab, gun2.position, transform.rotation);

        bullet1.GetComponent<BulletController>().SetDamage(data.damage);
        bullet2.GetComponent<BulletController>().SetDamage(data.damage);
    }
}
