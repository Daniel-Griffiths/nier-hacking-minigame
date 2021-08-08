using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private const float _bulletLifetime = 10f;

    void Start()
    {
        // Destroy bullet after x seconds
        Invoke("DestroyBullet", _bulletLifetime);

        GameObject player = Resources.Load("Prefabs/Player") as GameObject;

        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
    }

    void  OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != Tag.player) {
           DestroyBullet();
        }
    }

    void  DestroyBullet()
    {  
        Destroy(gameObject);
    }
}