using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update Method Called.");
    }

    void FixedUpdate()
    {
        Debug.Log("FixedUpdate Method Called.");
    }
}
