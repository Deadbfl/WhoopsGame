using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 15;
    Transform player;
    Shooting shooting;
    int damage;

    Vector3 moddifiedTarget;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        shooting = player.GetComponent<Shooting>();
        moddifiedTarget = new Vector3(shooting.target.position.x, transform.position.y, shooting.target.position.z);
        damage = Random.Range(2, 4);
        LookAtWithBullet();

    }
    void Update()
    {
        BulletMove();

    }
    void LookAtWithBullet()
    {
        transform.LookAt(moddifiedTarget);
    }
    private void BulletMove()
    {
        moddifiedTarget = new Vector3(shooting.target.position.x, transform.position.y, shooting.target.position.z);
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().health -= damage;
            other.gameObject.GetComponent<EnemyHealth>().enemyHealthBar.fillAmount = other.gameObject.GetComponent<EnemyHealth>().health / other.gameObject.GetComponent<EnemyHealth>().maxHealth;
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
