using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PandaExpress2DGame
{
    public class DamangeMonster : MonoBehaviour
    {
        public float damageHP = 10f;
        public string targetTag = "Enemy";

        private void OnTriggerEnter2D(Collider2D collision)
        {
            DoDamageToEnemy(collision);
        }

        public void DoDamageToEnemy(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(targetTag))
            {
                collision.GetComponentInChildren<HealthManager>().TakeDamage(damageHP);
            }
        }

        public void AddDamageUp(float value)
        {
            damageHP += value;
        }
    }
}