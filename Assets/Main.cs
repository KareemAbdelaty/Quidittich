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
    public WizardBehavior[] griffindor;
    public WizardBehavior[] slythrin;
    System.Random rand;
    public float snitchThrust;
    public float wizardThrust;
    public int xmax;
    public int xmin;
    public int ymax;
    public int ymin;
    public int zmax;
    public int zmin;
    public int repulsion;
    public int knockouts;
    public int powershares;
    public int mindcontrolls;
    public bool toggle;
    public enum views
    {
        ingame,
        griffindor,
        slythrin,
    }
    public views view;
    public int win; //indicates which gui to draw
    // Start is called before the first frame update
    void Start()
    {
        toggle = false;
        xmin = -700;
        xmax = 700;
        ymin = 0;
        ymax = 500;
        zmin = -700;
        zmax = 700;
        knockouts =0;
        powershares =0;
        mindcontrolls = 0;
        view = views.ingame;
        rand = new System.Random();
        griffindorStartpostion = 490;
        slythrinStartpostion = -490;
        griffindor = new WizardBehavior[TeamSize];
        slythrin = new WizardBehavior[TeamSize];
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
        if (toggle)
        {
            turnOff();
        }
        else
        {
            turnON();
        }
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


    void restart()
    {
        griffindorScore = 0;
        slythrinScore = 0;
        knockouts = 0;
        powershares = 0;
        mindcontrolls = 0;
        resetPositios();
        view = views.ingame;
        turnON();
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
        //snitch
        Rigidbody snitchScript = GameObject.FindGameObjectWithTag("snitch").GetComponent<Rigidbody>();
        SnitchBehaviourScript snitchScript2 = GameObject.FindGameObjectWithTag("snitch").GetComponent<SnitchBehaviourScript>();
        snitchScript.constraints = RigidbodyConstraints.None;
        snitchScript2.initForce();
        //wizards
        GameObject[] wizards = GameObject.FindGameObjectsWithTag("wizard");
        for(int i = 0; i < wizards.Length; i++)
        {
            Rigidbody wr = wizards[i].GetComponent<Rigidbody>();
            wr.constraints = RigidbodyConstraints.None;
            WizardBehavior b = wizards[i].GetComponent<WizardBehavior>();
            b.initForce();
        }

    }
    public void turnOff()
    {
        //snitch
        Rigidbody snitchScript = GameObject.FindGameObjectWithTag("snitch").GetComponent<Rigidbody>();
        snitchScript.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        //wizards
        GameObject[] wizards = GameObject.FindGameObjectsWithTag("wizard");
        for (int i = 0; i < wizards.Length; i++)
        {
            Rigidbody wr = wizards[i].GetComponent<Rigidbody>();
            wr.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
    }
    public void resetPositios()
    {
        GameObject s = GameObject.FindGameObjectWithTag("snitch");
        SnitchBehaviourScript snitchScript = GameObject.FindGameObjectWithTag("snitch").GetComponent<SnitchBehaviourScript>();
        s.transform.position = new Vector3(snitchScript.startx, snitchScript.starty, snitchScript.startz);
        GameObject[] wizards = GameObject.FindGameObjectsWithTag("wizard");

        for (int i = 0; i < wizards.Length; i++)
        {
            WizardBehavior b = wizards[i].GetComponent<WizardBehavior>();
            if(b.team == "griffindor")
            {
                wizards[i].transform.position = new Vector3(Random.Range(xmin, xmax), 10, griffindorStartpostion);
            }
            else
            {
                wizards[i].transform.position = new Vector3(Random.Range(xmin, xmax), 10, slythrinStartpostion);
            }
        }

    }
    public void setupLand()
    {
        land = Instantiate(land);
        Rigidbody r = GameObject.FindGameObjectWithTag("Land").GetComponent<Rigidbody>();
        r.useGravity = false;
        r.freezeRotation = true;
        r.constraints = RigidbodyConstraints.FreezePosition;
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
            b.id = i;
            b.aggresiveness = (int)generateStats(22, 3);
            b.maxExhaustion = (int)generateStats(65, 13);
            b.maxVelocity =(int) generateStats(18, 2);
            b.weight = (int)generateStats(75, 12);
            b.currentExhaustion = 0;
            b.invulnerability = rand.Next(0, 5);
            b.teamRage = rand.Next(0, 10);
            b.rechargeRate = 3;
            b.mindcontrol = rand.Next(0, 8);
            b.unconscious = false;
            b.xmax = xmax;
            b.xmin = xmin;
            b.ymax = ymax;
            b.ymin = ymin;
            b.thrust = wizardThrust;
            b.zmax = zmax;
            b.zmin = zmin;
            b.repulsion = repulsion;
            b.rb = rb;
            rb.useGravity = false;
            rb.mass =  b.weight*0.01f;
            w.transform.position = new Vector3(Random.Range(xmin, xmax), 10, griffindorStartpostion);
            MeshRenderer m = w.GetComponent<MeshRenderer>();
            m.material = Resources.Load("WizardRed", typeof(Material)) as Material;
            griffindor[i] = b;
        }
        for (int i = 0; i < TeamSize; i++)
        {
            GameObject w = (GameObject)Instantiate(wizard);
            WizardBehavior b = w.GetComponent<WizardBehavior>();
            Rigidbody rb = w.GetComponent<Rigidbody>();
            b.team = "slythrin";
            b.id = i;
            b.aggresiveness = (int)generateStats(30, 7);
            b.maxExhaustion =(int) generateStats(50, 15);
            b.maxVelocity =(int) generateStats(16, 2);
            b.weight =(int) generateStats(85, 17);
            b.currentExhaustion = 0;
            b.invulnerability = rand.Next(0, 10);
            b.teamRage = rand.Next(0, 10);
            b.rechargeRate = 1;
            b.mindcontrol = rand.Next(0, 15);
            b.unconscious = false;
            b.xmax = xmax;
            b.xmin = xmin;
            b.ymax = ymax;
            b.ymin = ymin;
            b.zmax = zmax;
            b.zmin = zmin;
            b.repulsion = repulsion;
            b.thrust = wizardThrust;
            b.rb = rb;
            rb.useGravity = false;
            rb.mass = b.weight*0.01f;
            w.transform.position = new Vector3(Random.Range(xmin, xmax), 10, slythrinStartpostion);
            MeshRenderer m = w.GetComponent<MeshRenderer>();
            m.material = Resources.Load("WizardGreen", typeof(Material)) as Material; 
            slythrin[i] = b;
        }
    }
    public void slythrinWin()
    {
        view = views.slythrin;
    }
    public void griffindorWin()
    {
        view = views.griffindor;
    }
    void OnGUI()
    {
        switch(view)
        {
            case views.ingame:
                string a = "Griffindor Score " + griffindorScore;
                string b = "Slythrin Score " + slythrinScore;
                string c = "current Knockouts: " + knockouts;
                string d = "current powershares: " + powershares;
                string e = "current mind controlls " + mindcontrolls;
                GUI.Label(new Rect(10, 10, 120, 20), a);
                GUI.Label(new Rect(Screen.width - 200, 10, 120, 20), b);
                GUI.Label(new Rect(10, 50, 120, 40), c);
                GUI.Label(new Rect(Screen.width - 200, 50, 120, 40), d);
                GUI.Label(new Rect(10, 90, 120, 40), e);
                toggle = GUI.Toggle(new Rect(Screen.width / 2, 10, 100, 30), toggle, "Pause");
                break;
            case views.griffindor:
                GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 500, 500), "Griffindor Win");
                if (GUI.Button(new Rect(Screen.width/2, Screen.height- 40, 50, 30), "Restart"))
                {
                    restart();
                }                   
                break;
            case views.slythrin:
                GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 500, 500), "Slythrin win");
                if (GUI.Button(new Rect(Screen.width / 2, Screen.height - 40, 50, 30), "Restart"))
                {
                    restart();
                }
                break;

        }
    }
}
