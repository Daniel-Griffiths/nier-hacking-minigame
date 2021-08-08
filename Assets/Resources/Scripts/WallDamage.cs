using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDamage : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == Tag.player) {
          other.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }
}
