using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCounter : MonoBehaviour
{
    public static ResetCounter instance;

    public int resetCounted;

    private void Awake()
    {
        if (instance == null) 
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance !=this)
        {
            Destroy(gameObject);
        }
    }
}
