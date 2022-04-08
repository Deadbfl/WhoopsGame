using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    Shooting totalMoney;
    Transform startTransform, finishTransform, player;
    Vector3 finishVec;
    [SerializeField] int valueOfItem = 20;
    [SerializeField] float furnitureDistance,setDistance=2.5f;
    private void Awake()
    {
        startTransform = transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        totalMoney = player.GetComponent<Shooting>();

    }
    private void Update()
    {
        finishVec = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, 1, GameObject.FindGameObjectWithTag("Player").transform.position.z);
        Vector3 playerVec = new Vector3(player.position.x, 0, player.position.z);
        Vector3 furnitureVec = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
        furnitureDistance = Vector3.Distance(playerVec, furnitureVec);
        if (furnitureDistance <= setDistance)
        {
            transform.position = Vector3.Lerp(startTransform.position, finishVec, 7f * Time.deltaTime);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            totalMoney.totalMoney += valueOfItem;
            totalMoney.TotalMoneyWrite();
            Destroy(gameObject);
        }
    }
}
