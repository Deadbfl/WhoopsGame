using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenTile : MonoBehaviour
{

    [SerializeField] GameObject money;
    [SerializeField] GameObject moneyPanel;
    Transform moneyPoint;
    Transform player;
    Shooting shooting;
    [SerializeField] TMP_Text needenMoneyText;
    float radius = 3;
    float moneyDelay = 0;
    float areaDistance;
    int oneShotEnter = 0, oneShotExit = 0;
    int giveMoneyCount = 0;
    public int neededMoneyCount, neededMoneyForTile = 500;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        moneyPoint = GameObject.FindGameObjectWithTag("MoneyPoint").GetComponent<Transform>();
        shooting = player.GetComponent<Shooting>();
        needenMoneyText.text = neededMoneyForTile.ToString();

    }
    private void Update()
    {

        Vector3 playerVec = new Vector3(player.position.x, 0, player.position.z);
        Vector3 areaVec = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
        areaDistance = Vector3.Distance(playerVec, areaVec);
        if (moneyDelay <= 0)
        {
            moneyDelay = 0;
        }
        else { moneyDelay -= Time.deltaTime; }
        if (areaDistance <= radius && moneyDelay <= 0 && neededMoneyForTile > 0 && shooting.totalMoney > 0 && shooting.totalMoney >= (int)Mathf.Pow(2, giveMoneyCount))
        {
            while (oneShotEnter == 0)
            {
                oneShotExit = 0;
                shooting.isGiveMoney = true;
                oneShotEnter++;
            }
            
            player.GetComponent<Shooting>().tilePoint = transform.GetChild(1);
            Instantiate(money, moneyPoint.position, moneyPoint.rotation);
            neededMoneyForTile -= (int)Mathf.Pow(2, giveMoneyCount);
            needenMoneyText.text = neededMoneyForTile.ToString();
            shooting.totalMoney -= (int)Mathf.Pow(2, giveMoneyCount);
            shooting.TotalMoneyWrite();
            giveMoneyCount++;
            moneyDelay += 0.2f;
        }
        if (areaDistance > radius)
        {
            while (oneShotExit == 0)
            {
                oneShotEnter = 0;
                neededMoneyCount = 0;
                shooting.isGiveMoney = false;
                oneShotExit++;

            }

            giveMoneyCount = 0;
        }
        if (neededMoneyForTile > shooting.totalMoney)
        {
            shooting.isGiveMoney = false;
        }
        if (neededMoneyForTile <= 0)
        {
            GetComponent<BoxCollider>().enabled = false;
            transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
            moneyPanel.SetActive(false);
            needenMoneyText.text = 0.ToString();
            neededMoneyForTile = 0;
        }
    }
    void FindNeededMoneyCount()
    {
        float voidMoney;
        voidMoney = neededMoneyForTile;
        for (int i = 0; i < neededMoneyForTile; i++)
        {
            voidMoney -= Mathf.Pow(2, i);
            if (voidMoney >= 0)
            {
                neededMoneyCount++;
            }
        }
    }
}
