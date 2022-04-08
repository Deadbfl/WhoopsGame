using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{
    [SerializeField] Transform rayStartPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] TMP_Text totalMoneyText;
    public Transform target;
    public Transform tilePoint;
    public float shootingRadius = 5;
    GameObject[] playerDistanceGameobjects;
    int findOwnerOfDistance = -1;
    public int totalMoney=100;
    public bool isGiveMoney;
    Vector3 moddifiedTarget;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        TotalMoneyWrite();
    }

    public void TotalMoneyWrite()
    {
        totalMoneyText.text = totalMoney.ToString();
    }

    private void Update()
    {
        if (totalMoney<=0)
        {
            totalMoney = 0;
        }
        CompareDistance();
    }
    public void Shoot()
    {
        
        CreateBullet();
        
    }
    public void LookAtTheTarget()
    {

        transform.LookAt(moddifiedTarget); // smoothluk ekleyebilirim
    }
    public void PlayIsShootAnim()
    {
        anim.SetBool("isShoot", true);
    }
    public void PlayIsShootAnimFalse()
    {
        anim.SetBool("isShoot", false);
    }
    void CreateBullet()
    {
        
        GameObject bulletClone = Instantiate(bullet,bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    
    }
    float CompareDistance()
    {
        playerDistanceGameobjects = GameObject.FindGameObjectsWithTag("Enemy");
        float[] playerDistances = new float[playerDistanceGameobjects.Length];// sadece olum ve spawnlamada kontrol edilmeli
        for (int i = 0; i < playerDistanceGameobjects.Length; i++)
        {
            playerDistances[i] = playerDistanceGameobjects[i].GetComponent<PlayerDistance>().CalculatePlayerDistance();
        }
        do
        {
            findOwnerOfDistance++;
        } 
        while (Mathf.Min(playerDistances) != playerDistanceGameobjects[findOwnerOfDistance].GetComponent<PlayerDistance>().CalculatePlayerDistance());
        target = playerDistanceGameobjects[findOwnerOfDistance].transform.GetChild(7);//target check burada yapiliyor
        moddifiedTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
        findOwnerOfDistance = -1;
        if (Mathf.Min(playerDistances)>=shootingRadius)
        {
            anim.SetBool("isShoot", false);
        }
        return Mathf.Min(playerDistances);
    }
}
