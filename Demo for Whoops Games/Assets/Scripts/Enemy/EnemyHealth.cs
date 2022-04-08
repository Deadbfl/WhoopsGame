using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    public Image enemyHealthBar;
    [SerializeField] GameObject healthBarCanvas;
    GameObject player;
    EnemyAI enemyAI;
    [SerializeField] GameObject money;
    [SerializeField] Transform[] moneySpawnPoints;
    public Transform[] enemySpawnPoints;
    public float health, maxHealth = 10;
    int randomSpawnPointMoney = 0;
    int spawnPointIndex;
    int createOneTime = 0;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyAI = GetComponent<EnemyAI>();
        health = maxHealth;
        enemyHealthBar.fillAmount = 1;
        GetComponent<PlayerDistance>().enabled = true;
        GetComponent<Animator>().enabled = true;
        GetComponent<EnemyAI>().enabled = true;
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<EnemyHealth>().enabled = true;
        healthBarCanvas.SetActive(true);
        transform.GetChild(12).gameObject.GetComponent<Canvas>().enabled = true;
        transform.GetChild(13).gameObject.GetComponent<Canvas>().enabled = true;
        transform.GetChild(13).GetComponent<LookAtTheCamera>().enabled = true;

    }
    private void Update()
    {
        if (health <= 0)
        {
            healthBarCanvas.SetActive(false);
            MakeDeadEnemy();
        }
    }
    void CreateEnemy()
    {

        spawnPointIndex = Random.Range(0, enemySpawnPoints.Length);
        Instantiate(enemyPrefab, enemySpawnPoints[spawnPointIndex].position, enemySpawnPoints[spawnPointIndex].rotation);
    }
    void MakeDeadEnemy()
    {
        while (createOneTime == 0)
        {

            player.GetComponent<Shooting>().PlayIsShootAnimFalse();
            CreateEnemy();
            GetComponent<PlayerDistance>().isDead = true;
            enemyAI.curretEnemyStatus = EnemyAI.EnemyStatus.Dead;
            gameObject.tag = "DeadEnemy";
            createOneTime++;
        }
    }
    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            randomSpawnPointMoney = Random.Range(0, moneySpawnPoints.Length);
            Instantiate(money, moneySpawnPoints[randomSpawnPointMoney].position, moneySpawnPoints[randomSpawnPointMoney].rotation);
            Instantiate(money, moneySpawnPoints[randomSpawnPointMoney].position, moneySpawnPoints[randomSpawnPointMoney].rotation);
        }
    }
}
