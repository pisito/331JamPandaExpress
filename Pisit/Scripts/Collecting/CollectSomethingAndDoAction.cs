using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectSomethingAndDoAction : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public string targetTag = "Player";
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            Debug.Log("I enter the colldier 2D OwO");
            onTriggerEnter?.Invoke();
        } 
    }
}
