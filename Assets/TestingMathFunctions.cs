using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingMathFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(MathFunctions.CrossProductMatrix(new Vector3(0.2f, -0.5f, -1f), new Vector3(1, 0.5f, 0.3f)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
