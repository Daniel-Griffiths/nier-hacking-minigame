using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    private const float _bulletLifetime = 10f;

    void Start()
    {
        // Destroy bullet after x seconds
        Invoke("DestroyBullet", _bulletLifetime);

        // GameObject enemy = Resources.Load("Prefabs/Enemy") as GameObject;

        // Physics.IgnoreCollision(enemy.GetComponent<Collider>(), GetComponent<Collider>());
    }

    void  OnTriggerEnter(Collider other)
    {
        if (other.transform.tag ==Tag.player || other.transform.tag == Tag.bullet) {
            other.gameObject.GetComponent<PlayerController>()?.TakeDamage();
            DestroyBullet();
        }
    }

    void  DestroyBullet()
    {  
        Destroy(gameObject);
    }
}