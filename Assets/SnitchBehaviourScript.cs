using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnitchBehaviourScript : MonoBehaviour
{
    public string id = "Snitch";
    public float randomx;
    public float randomy;
    public float randomz;
    public int xmax;
    public int xmin;
    public int ymax;
    public int ymin;
    public int zmax;
    public int zmin;
    public Rigidbody rb;
    public float thrust;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate() { 
        Vector3 currentpos = GameObject.FindGameObjectWithTag("snitch").transform.position;
        if (randomx == currentpos.x && randomy == currentpos.y && randomz == currentpos.z)
        {
            generateNewCoordinates();
        }
        generateForce();
    }
    public void generateNewCoordinates()
    {
        randomx = Random.Range(xmin, xmax);
        randomy = Random.Range(ymin, ymax);
        randomz = Random.Range(zmin, zmax);
    }
    public void generateForce()
    {
        Vector3 vec = new Vector3(randomx, randomy, randomz);
        rb.AddRelativeForce(vec * thrust);
    }

    public void OnCollisionEnter()
    {

    }
}
