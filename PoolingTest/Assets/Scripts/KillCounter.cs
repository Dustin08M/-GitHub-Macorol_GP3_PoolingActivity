using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    public static KillCounter instance;
    public int enemiesKilled;
    public TMP_Text Counter;
    bool ifReached30Kills = false;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Counter.text = "Kills: " + enemiesKilled;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NotifyEnemyDeath()
    {
        enemiesKilled++;
        Counter.text = "Kills: " + enemiesKilled;
        Debug.Log("Enemy Killed: "+enemiesKilled);
        if (enemiesKilled >= 30 && !ifReached30Kills)
        {
            Invoke("HpNotif", 0.2f);
            ChangeEnemyHP();
        }
    }
    private void ChangeEnemyHP()
    {
        GameObject[] Enemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemies in Enemy)
        {
            EnemyScript unit = enemies.GetComponent<EnemyScript>();
            unit.EnemyHp = 3;
        }
    }
    private void HpNotif()
    {
        Debug.Log("Enemy hp is now 3");
    }
}
