using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTreeWorld : MonoBehaviour
{
    private static QuadTreeWorld _instance;

    public static QuadTreeWorld Instance { get => _instance; set => _instance = value; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public Transform TreePlane
    {
        get
        {
            return transform;
        }
    }

    public int TreeLeavesCount
    {
        get
        {
            return transform.childCount;
        }
    }
}
