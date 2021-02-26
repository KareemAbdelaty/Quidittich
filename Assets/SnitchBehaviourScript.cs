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
    Vector3 vec;
    Vector3 opposite;
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
        if ((currentpos.x - 5 <= randomx)&&(randomx <= currentpos.x + 5)&& (currentpos.y - 5 <= randomy) && (randomy <= currentpos.y+ 5)&&( currentpos.z - 5 <= randomz)&&(randomx <= currentpos.x + 5))
        {
            generateNewCoordinates();
            removeForce();
            generateForce();
        }
    }
    public void generateNewCoordinates()
    {
        randomx = Random.Range(-100, 100);
        randomy = Random.Range(-100, 100);
        randomz = Random.Range(-100, 100);
    }
    public void generateForce()
    {
        Vector3 vec2 = new Vector3(randomx, randomy, randomz);
        vec = (vec2 - GameObject.FindGameObjectWithTag("snitch").transform.position).normalized;
        opposite = new Vector3(-vec.x, -vec.y, -vec.z);
        rb.AddForce(vec * thrust);
    }
    public void removeForce()
    {
        rb.AddForce(opposite * thrust);
    }

    public void OnCollisionEnter()
    {

    }
}
