using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PandaExpress2DGame
{
    public class DamagePlayer : MonoBehaviour
    {
        public float damageHP = 10f;
        public string targetTag = "Player";

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag(targetTag))
            {
                collision.GetComponentInChildren<HealthManager>().TakeDamage(damageHP);
            }
        }
    }
}

