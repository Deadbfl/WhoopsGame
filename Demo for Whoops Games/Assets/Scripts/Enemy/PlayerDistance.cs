using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistance : MonoBehaviour
{

    public float playerDistance;
    GameObject targetPanel;
    EnemyAI enemyAI;
    Shooting shooting;
    PlayerMovement playerMovement;
    EnemyHealth enemyHealth;
    public bool isDead;
    Transform player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        targetPanel = gameObject.transform.GetChild(12).gameObject;
        targetPanel.SetActive(false);
        playerMovement = player.GetComponent<PlayerMovement>();
        shooting = player.GetComponent<Shooting>();
        enemyAI = GetComponent<EnemyAI>();

    }
    private void Update()
    {
        CanShootMe();
        IsTarget();
    }
    public float CalculatePlayerDistance()
    {
        Vector3 playerVec = new Vector3(player.position.x, 0, player.position.z);
        Vector3 enemyVec = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
        return playerDistance = Vector3.Distance(playerVec, enemyVec);

    }
    void IsTarget()
    {
        if (shooting.target == gameObject.transform.GetChild(7) && CalculatePlayerDistance() <= shooting.shootingRadius && !shooting.isGiveMoney)
        {
            targetPanel.SetActive(true);
            enemyHealth = GetComponent<EnemyHealth>();
        }
        else
        {
            targetPanel.SetActive(false);
        }
    }
    void CanShootMe()
    {
        if (CalculatePlayerDistance() <= shooting.shootingRadius && playerMovement.vertical == 0 && !isDead && !shooting.isGiveMoney)
        {
            enemyAI.curretEnemyStatus = EnemyAI.EnemyStatus.Escape;
            shooting.LookAtTheTarget();
            shooting.PlayIsShootAnim();
        }

    }
}

