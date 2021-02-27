using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public int Score = 0;
    public int TeamSize= 20;
    public int GoalLimit = 100;
    public Vector3 griffindorStartpostion;
    public Vector3 slythrinStartpostion;
    public GameObject land;
    public GameObject wizard;
    public GameObject snitch;
    public GameObject[] wizards;
    public float snitchThrust;
    public int xmax;
    public int xmin;
    public int ymax;
    public int ymin;
    public int zmax;
    public int zmin;
    // Start is called before the first frame update
    void Start()
    {
        xmin = -500;
        xmax = 500;
        ymin = 0;
        ymax = 1000;
        zmin = -500;
        zmax = 500;

        //create floor
        land = Instantiate(land);
        Rigidbody r = GameObject.FindGameObjectWithTag("Land").GetComponent<Rigidbody>();
        r.useGravity = false;
        //create snitch
        Instantiate(snitch);
        SnitchBehaviourScript snitchScript = GameObject.FindGameObjectWithTag("snitch").GetComponent<SnitchBehaviourScript>();
        snitchScript.rb = GameObject.FindGameObjectWithTag("snitch").GetComponent<Rigidbody>();
        snitchScript.rb.useGravity = false;
        snitchScript.xmax = xmax;
        snitchScript.xmin = xmin;
        snitchScript.ymax = ymax;
        snitchScript.ymin = ymin;
        snitchScript.zmax = zmax;
        snitchScript.zmin = zmin;
        snitchScript.generateNewCoordinates();
        snitchScript.thrust = snitchThrust;
        snitchScript.generateForce();
        //create wizards'
        for(int i =0; i < TeamSize; i++)
        {
            Instantiate(wizard);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //check if endgame
    }

    void endgame() { 
        //endgame screen
    }

    void restart()
    {
        //restart
    }
    double generateStats(double m, double u)
    {
        return m;
    }
    
}
