using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cafe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ArchitectureSystem.build_system.MoneyOutput = 50;
    }
}
