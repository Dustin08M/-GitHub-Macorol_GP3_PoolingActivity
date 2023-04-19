using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform BulletSpawnpoint;
    public float BulletSpeed = 10f;
    public int BulletPoolSize = 10;

    private Queue<GameObject> bulletPool;

    void Start()
    {
        bulletPool = new Queue<GameObject>();
        for (int i = 0; i < BulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(BulletPrefab);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 position = BulletSpawnpoint.position;
            Quaternion rotation = BulletSpawnpoint.rotation;
            SpawnBullet(position, rotation);
        }
    }

    void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().velocity = BulletSpawnpoint.forward * BulletSpeed;
            StartCoroutine(ReturnBulletPrefab(bullet));
        }
        else
        {
            GameObject bullet = Instantiate(BulletPrefab, position, rotation);
            bullet.GetComponent<Rigidbody>().velocity = BulletSpawnpoint.forward * BulletSpeed;
            StartCoroutine(ReturnBulletPrefab(bullet));
        }
    }

    IEnumerator ReturnBulletPrefab(GameObject bulletPrefab)
    {
        yield return new WaitForSeconds(1f);
        bulletPrefab.SetActive(false);
        bulletPool.Enqueue(bulletPrefab);
    }
}
