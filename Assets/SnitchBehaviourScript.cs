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
    public int startx;
    public int starty;
    public int startz;
    public Rigidbody rb;
    public float thrust;
    private bool upward; 
    private Vector3 vec;
    private Vector3 opposite;
    // Start is called before the first frame update
    void Start()
    {
        upward = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate() { 
        Vector3 currentpos = transform.position;
        if ((currentpos.x - 5 <= randomx)&&(randomx <= currentpos.x + 5)&& (currentpos.y - 5 <= randomy) && (randomy <= currentpos.y+ 5)&&( currentpos.z - 5 <= randomz)&&(randomx <= currentpos.x + 5))
        {
            generateNewCoordinates();
            removeForce();
            generateForce();
        }
        if (upward)
        {
            Vector3 vec3 = new Vector3(0, -50, 0);
            rb.AddForce(vec3, ForceMode.Force);
            upward = false ;
        }
    }
    public void generateNewCoordinates()
    {
        randomx = Random.Range(xmin, xmax);
        randomy = Random.Range(ymin, ymax);
        randomz = Random.Range(zmin, zmax);
    }
    public void generateForce()
    {
        Vector3 vec2 = new Vector3(randomx, randomy, randomz);
        vec = (vec2 - transform.position).normalized;
        opposite = new Vector3(-vec.x, -vec.y, -vec.z);
        rb.AddForce(vec * thrust, ForceMode.Force);
    }
    public void removeForce()
    {
        rb.AddForce(opposite * thrust, ForceMode.Force);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Land")
        {
            Vector3 vec3 = new Vector3(0, 50, 0);
            rb.AddForce(vec3, ForceMode.Force);
            upward = true;
        }
        if(collision.gameObject.tag == "wizard")
        {
            WizardBehavior w = collision.gameObject.GetComponent<WizardBehavior>();
            if (w.team== "slythrin")
            {
                Vector3 vec3 = new Vector3(startx, starty, startz);
                transform.position = vec3;
                Main m = GameObject.FindGameObjectWithTag("main").GetComponent<Main>();
                m.slythrinScore++;
            }
            else
            {
                Vector3 vec3 = new Vector3(startx, starty, startz);
                transform.position = vec3;
                Main m = GameObject.FindGameObjectWithTag("main").GetComponent<Main>();
                m.griffindorScore++;
            }
        }
    }
}
