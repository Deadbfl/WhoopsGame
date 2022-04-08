using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    GameObject furniture;
    [SerializeField] Vector3 rotationVec;
    private void Awake()
    {
        furniture = transform.GetChild(0).gameObject;

    }
    private void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.GetComponentInParent<Rotate>().transform.Rotate(rotationVec); ;
            furniture.SetActive(true);
        }
    }
}
