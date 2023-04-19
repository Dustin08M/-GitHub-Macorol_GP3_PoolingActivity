using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyScript : MonoBehaviour
{

    [SerializeField] private Transform Player;
    Renderer materialChange;
    public int EnemyHp = 2;
    private int CurrentHP;
    public GameObject HitFx;
   
    public Color NeutralColor = Color.red;
    public Color HitColor = Color.white;

    private Queue<GameObject> HitPool;
    public int EnemyHitPoolSize = 5;
    private NavMeshAgent navAgent;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        materialChange = GetComponent<Renderer>();
        CurrentHP = EnemyHp;
        materialChange.material.color = NeutralColor;

        HitPool = new Queue<GameObject>();
        for (int i = 0; i < HitPool.Count; i++)
        {
            GameObject hitSprite = Instantiate(HitFx);
            hitSprite.SetActive(false);
            HitPool.Enqueue(hitSprite);
        }
    }
    void Update()
    {
        if (Player != null)
        {
            navAgent.SetDestination(GameObject.FindGameObjectWithTag("Player").gameObject.transform.position);
        }
    }
    void SpawnHitPool (Vector3 position, Quaternion rotation)
    {
        if (HitPool.Count > 0)
        {
            GameObject hitSprite = HitPool.Dequeue();
            hitSprite.transform.position = position;
            hitSprite.transform.rotation = rotation;
            hitSprite.SetActive(true);
            StartCoroutine(ReturnHitFX(hitSprite));
        }
        else
        {
            GameObject hitSprite = Instantiate(HitFx, position, rotation);
            StartCoroutine(ReturnHitFX(hitSprite));
        }
    }

    public void TakeDamage(int Damage)
    {
        EnemyHp -= Damage;
        CurrentHP = EnemyHp;
        if (CurrentHP <= 0)
        {
            Destroy(gameObject);
            KillCounter.instance.NotifyEnemyDeath();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            SpawnHitPool(transform.position, transform.rotation);
            materialChange.material.color = HitColor;
            Invoke("BackToNeutralColor", .2f);
            TakeDamage(1);
            Debug.Log("Damage Taken");
            ReturnBulletPrefab(other.gameObject);
            StartCoroutine(ReturnBulletPrefab(other.gameObject));
        }
    }

    private void BackToNeutralColor()
    {
        materialChange.material.color = NeutralColor;
    }

    IEnumerator ReturnHitFX(GameObject hitSpriteFx)
    {
        yield return new WaitForSeconds(.5f);
        hitSpriteFx.SetActive(false);
        HitPool.Enqueue(hitSpriteFx);
    }

    IEnumerator ReturnBulletPrefab(GameObject bulletPrefab)
    {
        yield return new WaitForSeconds(1f);
        bulletPrefab.SetActive(false);
    }
}

