using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public int slythrinScore = 0;
    public int griffindorScore = 0;
    public int TeamSize= 20;
    public int GoalLimit = 100;
    public int griffindorStartpostion;
    public int   slythrinStartpostion;
    public GameObject land;
    public GameObject wizard;
    public GameObject snitch;
    public GameObject[] griffindor;
    public GameObject[] slythrin;
    System.Random rand;
    public float snitchThrust;
    public float wizardThrust;
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
        ymax = 200;
        zmin = -500;
        zmax = 500;
        rand = new System.Random();
        griffindorStartpostion = 490;
        slythrinStartpostion = -490;
        griffindor = new GameObject[TeamSize];
        slythrin = new GameObject[TeamSize];
        //create floor
        setupLand();
        //create snitch
        setupSnitch();
        //create wizards'
        setupWizards();
    }

    // Update is called once per frame
    void Update()
    {
        if(slythrinScore >= GoalLimit)
        {
            turnOff();
            slythrinWin();
        }else if(griffindorScore >= GoalLimit)
        {
            turnOff();
            griffindorWin();
        }
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
        double u1 = 1.0 - rand.NextDouble(); 
        double u2 = 1.0 - rand.NextDouble();
        double randStdNormal = System.Math.Sqrt(-2.0 * System.Math.Log(u1)) *
                     System.Math.Sin(2.0 * System.Math.PI * u2); 
        double randNormal =
                     m + u * randStdNormal;
        return randNormal;
    }
    public void turnON()
    {

    }
    public void turnOff()
    {

    }
    public void resetPositios()
    {

    }
    public void setupLand()
    {
        land = Instantiate(land);
        Rigidbody r = GameObject.FindGameObjectWithTag("Land").GetComponent<Rigidbody>();
        r.useGravity = false;
    }
    public void setupSnitch()
    {
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
    }
    public void setupWizards()
    {
        for (int i = 0; i < TeamSize; i++)
        {
            GameObject w = (GameObject)Instantiate(wizard);
            WizardBehavior b = w.GetComponent<WizardBehavior>();
            Rigidbody rb = w.GetComponent<Rigidbody>();
            b.team = "griffindor";
            b.aggresiveness = (int)generateStats(22, 3);
            b.maxExhaustion = (int)generateStats(65, 13);
            b.maxVelocity =(int) generateStats(18, 2);
            b.weight = (int)generateStats(75, 12);
            b.currentExhaustion = 0;
            b.mindControl = rand.Next(0,5);
            b.invulnerability = rand.Next(0, 5);
            b.teamRage = rand.Next(0, 10);
            b.rechargeRate = 3;
            b.unconscious = false;
            b.xmax = xmax;
            b.xmin = xmin;
            b.ymax = ymax;
            b.ymin = ymin;
            b.thrust = wizardThrust;
            b.zmax = zmax;
            b.zmin = zmin;
            b.rb = rb;
            rb.useGravity = false;
            rb.mass = b.weight;
            w.transform.position = new Vector3(Random.Range(xmin, xmax), 10, griffindorStartpostion);
            MeshRenderer m = w.GetComponent<MeshRenderer>();
            m.material = Resources.Load("WizardRed", typeof(Material)) as Material;
            griffindor[i] = w;
        }
        for (int i = 0; i < TeamSize; i++)
        {
            GameObject w = (GameObject)Instantiate(wizard);
            WizardBehavior b = w.GetComponent<WizardBehavior>();
            Rigidbody rb = w.GetComponent<Rigidbody>();
            b.team = "slythrin";
            b.aggresiveness = (int)generateStats(30, 7);
            b.maxExhaustion =(int) generateStats(50, 15);
            b.maxVelocity =(int) generateStats(16, 2);
            b.weight =(int) generateStats(85, 17);
            b.currentExhaustion = 0;
            b.mindControl = rand.Next(0, 10);
            b.invulnerability = rand.Next(0, 10);
            b.teamRage = rand.Next(0, 10);
            b.rechargeRate = 1;
            b.unconscious = false;
            b.xmax = xmax;
            b.xmin = xmin;
            b.ymax = ymax;
            b.ymin = ymin;
            b.zmax = zmax;
            b.zmin = zmin;
            b.thrust = wizardThrust;
            b.rb = rb;
            rb.useGravity = false;
            rb.mass = b.weight;
            w.transform.position = new Vector3(Random.Range(xmin, xmax), 10, slythrinStartpostion);
            MeshRenderer m = w.GetComponent<MeshRenderer>();
            m.material = Resources.Load("WizardGreen", typeof(Material)) as Material; 
            slythrin[i] = w;
        }
    }
    public void slythrinWin()
    {

    }
    public void griffindorWin()
    {

    }
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Griffindor Score" + griffindorScore);
        GUI.Label(new Rect(170, 10, 100, 20), "Slythrin Score " + slythrinScore);
    }
}
