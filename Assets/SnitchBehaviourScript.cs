using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnitchBehaviourScript : MonoBehaviour
{
    public bool on;
    public float randomx; //random x position
    public float randomy; //random y position
    public float randomz; // random z position
    public string last; // last team that caught the snitch
    public int xmax; // maximum x in the world
    public int xmin; // minimum x in the world
    public int ymax;
    public int ymin;
    public int zmax;
    public int zmin;
    public int startx; //starting positions
    public int starty;
    public int startz;
    public Rigidbody rb; //refrence to snitch RigidBody
    public float thrust; // How much thrust
    private bool upward; // on land collision go up
    private Vector3 vec; // current force
    private Vector3 opposite; // opposite current force


    // Start is called before the first frame update
    void Start()
    {
        upward = false;
        last = "";
        initForce();
        on = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate() {
        Vector3 p = transform.position;
        if (p.x < xmin || p.x > xmax)
        {
            p.x = 50;
        }
        if (p.y < ymin || p.y > ymax)
        {
            p.y = 1000;
            Debug.Log("here");
        }
        if (p.z < zmin || p.z > zmax)
        {
            p.z = 50;
        }
        Vector3 currentpos = transform.position;
        if ((currentpos.x - 10 <= randomx)&&(randomx <= currentpos.x + 10)&& (currentpos.y - 10 <= randomy) && (randomy <= currentpos.y+ 10)&&( currentpos.z - 10<= randomz)&&(randomx <= currentpos.x + 10))
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
        near();
    }
    public void generateNewCoordinates()
    {
        randomx = Random.Range(xmin, xmax);
        randomy = Random.Range(ymin, ymax);
        randomz = Random.Range(zmin, zmax);
    }
    public void initForce()
    {
        generateNewCoordinates();
        Vector3 vec2 = new Vector3(randomx, randomy, randomz);
        vec = (vec2 - transform.position).normalized;
        opposite = new Vector3(-vec.x, -vec.y, -vec.z);
        rb.AddForce(vec * thrust, ForceMode.Force);
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
    public void near()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag == "wizard")
            {
                WizardBehavior w = hitColliders[i].gameObject.GetComponent<WizardBehavior>();
                caught(w.team);
                break;
            }

        }
    }
    void caught(string team)
    {
        int points;
        if(team == last)
        {
            points = 2;
        }
        else
        {
            points = 1;
        }
        Vector3 vec3 = new Vector3(startx, starty, startz);
        transform.position = vec3;
        rb.velocity = Vector3.zero;
        initForce();
        Main m = GameObject.FindGameObjectWithTag("main").GetComponent<Main>();
        if(team == "slythrin")
        {
            m.slythrinScore +=points;
        }
        else
        {
            m.griffindorScore += points;
        }
        last = team;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Land")
        {
            transform.position = new Vector3(transform.position.x, 50, transform.position.z);
            rb.velocity = Vector3.zero;
            generateForce();
        }
        if(collision.gameObject.tag == "wizard")
        {
            WizardBehavior w = collision.gameObject.GetComponent<WizardBehavior>();
            if (w.team== "slythrin")
            {
                caught("slythrin");
            }
            else
            {
                caught("griffindor");
            }
        }
    }
}
