using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMoney : MonoBehaviour
{
    Transform tilePoint, player;
    [SerializeField] float lerpSpeed = 1;
    Shooting shooting;
    OpenTile openTile;
    float bulletDistance;
    private void Awake()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        tilePoint = player.GetComponent<Shooting>().tilePoint;
        openTile = GameObject.FindGameObjectWithTag("TileObject").GetComponent<OpenTile>();
    }

    private void Update()
    {

        transform.position = Vector3.Lerp(transform.position, tilePoint.position, lerpSpeed * Time.deltaTime);
        Vector3 playerVec = new Vector3(player.position.x, 0, player.position.z);
        Vector3 bulletVec = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
        bulletDistance = Vector3.Distance(playerVec, bulletVec);
        if (bulletDistance >= 5)
        {
            //openTile.neededMoneyCount--;
            Destroy(gameObject);
        }
    }
}
