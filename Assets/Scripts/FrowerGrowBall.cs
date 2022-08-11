using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrowerGrowBall : MonoBehaviour
{

    public GameObject FlowerPrefab;
    public Transform RedRoomRoot;

    public bool CheckingCollisions = false;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Duplicate()
    {
        GameObject go = Instantiate(gameObject, transform.position, transform.rotation, transform.parent);
        go.GetComponent<Rigidbody>().velocity = Vector3.zero;
        go.GetComponent<Outline>().enabled = false;
        
        
    }

    public void CheckCollisions()
    {
        CheckingCollisions = true;
        //rb.detectCollisions = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!CheckingCollisions) return;

        if (Mathf.Abs(transform.position.x - RedRoomRoot.position.x) > 10) return;
        if (Mathf.Abs(transform.position.y - RedRoomRoot.position.y) > 20) return;
        if (Mathf.Abs(transform.position.z - RedRoomRoot.position.z) > 10) return;

        if (!PowersManager.instance.RedActive) return;

        ContactPoint contact = collision.contacts[0];

        Vector3 position = contact.point;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal * Random.Range(0, 360f));

        GameObject flower = Instantiate(FlowerPrefab, position, rotation);

        Destroy(gameObject);
    }
}
