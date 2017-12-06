using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAwake : MonoBehaviour
{

    public static PauseAwake Instance;
    // Use this for initialization
    void Start()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        Instance = this;
    }

    // Update is called once per frame
    void Awake()
    {
        // Stay persistent between levels
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(gameObject);
    }
}
