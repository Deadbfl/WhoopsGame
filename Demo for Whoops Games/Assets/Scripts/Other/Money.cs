using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    Transform player;
    Shooting shooting;
    int moneyAmount;
    float playerDistance;
    [SerializeField] float collectingRadius=2,collectingTime=0.02f;
    Vector3 moneyVec, playerVec;
    private void Awake()
    {
       player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
       moneyAmount = Random.Range(27, 31);
    }
    private void Update()
    {
        CalculatePlayerDistance();
        if (playerDistance<=collectingRadius)
        {
            transform.position = Vector3.Lerp(transform.position,playerVec, collectingTime);
        }
    }
    public float CalculatePlayerDistance()
    {
        playerVec = new Vector3(player.position.x, 1f, player.position.z);
        moneyVec = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        return playerDistance = Vector3.Distance(playerVec, moneyVec);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shooting = collision.gameObject.GetComponent<Shooting>();
            shooting.totalMoney += moneyAmount;
            shooting.TotalMoneyWrite();
            Destroy(gameObject);
        }
    }
}
