using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private TowerData data;

    [SerializeField] private float rotationSpeed;

    private Transform target;

    private void Start()
    {
        StartCoroutine(PreShoot());
    }

    private void Update()
    {
        if (target == null) { GetClosestEnemy(GameObject.FindGameObjectsWithTag("Enemy")); return; }
                 

        RotateToEnemy();
    }


    IEnumerator PreShoot()
    {
        yield return new WaitForSeconds(data.shootingSpeed);

        GetComponent<IShootable>().Shoot(target, data);
        StartCoroutine(PreShoot());
    }

    private void RotateToEnemy()
    {
        var targetRotation = Quaternion.LookRotation(target.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void GetClosestEnemy(GameObject[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }

        target = bestTarget;
    }

    public int GetLevel()
    {
        return data.level;
    }
}
