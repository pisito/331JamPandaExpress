using Pisit.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_EnableSprint : MonoBehaviour
{
    public CharacterController2D playerController2D;

    private void Start()
    {
        if (playerController2D == null)
        {
            GameObject foundObject = GameObject.FindGameObjectWithTag("Player");
            playerController2D = foundObject.GetComponent<CharacterController2D>();
        }
    }

    public void EnablePlayerToSprint()
    {
        Debug.Log("Enable player to sprint");
        playerController2D.canSprint = true; // enable sprint
        //playerController2D.multiJumpMax += value;
        DestroyMySelf();
    }

    public void DestroyMySelf()
    {
        Destroy(gameObject, 0.2f); //2 milli seconds
    }
}
