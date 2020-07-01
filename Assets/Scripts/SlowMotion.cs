using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public float slowMoSpeed;
    private PlayerMovement playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //If bool inSpeedMode is true from Player Script.
        if(playerScript.inSpeedMode)
        {
            if(Input.GetMouseButtonDown(0))
            {
               
                Time.fixedDeltaTime = Time.timeScale;
                Time.timeScale = slowMoSpeed;
            }
            else if(Input.GetMouseButtonUp(0))
            {
                Time.timeScale = 1;
            }
        }
    }
}
