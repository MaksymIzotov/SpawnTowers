using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;

    private float damage;

    private void Start()
    {
        Destroy(gameObject, 5);
    }
    void Update()
    {
        transform.position += transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime;
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<IDamagable>().TakeDamage(damage);

        Destroy(gameObject);
    }
}
