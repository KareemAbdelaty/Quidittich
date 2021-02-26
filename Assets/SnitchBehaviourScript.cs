using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnitchBehaviourScript : MonoBehaviour
{
    public string id = "Snitch";
    public int x = 10;
    public int y = 10;
    public int z = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(x* Time.deltaTime, x* Time.deltaTime, z * Time.deltaTime);
    }
    void FixedUpdate() { 
    
    }
    public void generateNewCoordinates()
    {

    }
    void LateUpdate()
    {

    }
}
