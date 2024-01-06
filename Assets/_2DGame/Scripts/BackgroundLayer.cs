using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BackgroundLayer : MonoBehaviour
{

    public float speedFactor = 1;
    private bool enable = true;


    private void Update()
    {
        if (GameManager.playerController.IsBlocked())
            enable = false;
        else enable = true;
    }

    private void FixedUpdate()
    {
        if(enable)
        {
            transform.position = new Vector3(transform.position.x - GameManager.playerController.GetVelocity().x * speedFactor,
                                 transform.position.y,
                                 transform.position.z);
        }
    }
}
