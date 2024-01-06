using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Destructible : MonoBehaviour
{
    public GameObject gate;

    public void TakeDamage()
    {
        gate.SetActive(false);
    }
}
