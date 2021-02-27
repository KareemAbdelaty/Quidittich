using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBehavior : MonoBehaviour
{
    public string team;
    public int id;
    public int aggresiveness;
    public int maxExhaustion;
    public bool exhausted;
    public int maxVelocity;
    public int weight;
    public int currentExhaustion;
    public bool sharingPower;
    public float repulsion;
    public int invulnerability;
    public int teamRage; //when dead distribute power
    public int rechargeRate;
    public bool unconscious;
    public bool mindControlling;
    public bool mindcontrol;
    public GameObject knockOutby;
    public int xmax;
    public int xmin;
    public int ymax;
    public int timer;
    public int ymin;
    public int zmax;
    public int zmin;
    public int startx;
    public int starty;
    public float thrust;
    private Vector3 vec;
    private Vector3 opposite;
    public Rigidbody rb;
    private bool upward;
    // Start is called before the first frame update
    void Start()
    {
        exhausted = false;
        sharingPower = false;
        timer = 0;
        knockOutby = null;
        mindControlling = false;
        initForce();
    }


    void FixedUpdate()
    {
        Vector3 p = transform.position;
        if(p.x < xmin || p.x > xmax)
        {
            p.x = 50;
        }
        if (p.y < ymin || p.y > ymax)
        {
            p.y = 200;
        }
        if (p.z < zmin || p.z > zmax)
        {
            p.z = 50;
        }
        transform.position = p;
        if (!unconscious)
        {
            if (currentExhaustion >= maxExhaustion)
            {
                exhausted = true;
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                Debug.Log(team + " wizard " + id + " is exhausted");
            }
            else if (currentExhaustion <= 0)
            {
                exhausted = false;
                rb.constraints = RigidbodyConstraints.None;
                initForce();
                Debug.Log(team + " wizard " + id + " has rested");
            }
            if (!exhausted)
            {
                generateForce();
            }
            else
            {
                recharging();
            }
        }



    }
    void onKnockOut()
    {
        Debug.Log(team + " wizard " + id + " is knocked out");
        rb.velocity = Vector3.zero;
        Main m = GameObject.FindGameObjectWithTag("main").GetComponent<Main>();
        m.knockouts++;
        System.Random rng = new System.Random();
        int n = rng.Next(0, 100);
        if(n <= invulnerability)
        {
            Debug.Log(team + " wizard " + id + " is INVULNERABLE");
            currentExhaustion += 10;
            m.knockouts--;
            return;
        }
        n = rng.Next(0, 100);
        if (n <= teamRage)
        {
            Debug.Log(team + " is enraged because wizard " + id + " was knocked out ");
            sharingPower = true;
            m.powershares++;
            if(team == "slythrin")
            {
                for(int i = 0; i< m.slythrin.Length; i++)
                {
                    if (m.slythrin[i].id == id)
                    {
                        continue;
                    }
                    m.slythrin[i].currentExhaustion = 0;
                    m.slythrin[i].maxVelocity += maxVelocity;
                    m.slythrin[i].maxExhaustion += maxExhaustion;
                    m.slythrin[i].aggresiveness += aggresiveness;
                }
            }
            else
            {
                for (int i = 0; i < m.griffindor.Length; i++)
                {
                    if (m.griffindor[i].id == id)
                    {
                        continue;
                    }
                    m.griffindor[i].currentExhaustion = 0;
                    m.griffindor[i].maxVelocity += maxVelocity;
                    m.griffindor[i].maxExhaustion += maxExhaustion;
                    m.griffindor[i].aggresiveness += aggresiveness;
                }
            }
        }
        n = rng.Next(0, 100);
        if (n < mindcontrol)
        {
            m.mindControlls++;
            WizardBehavior w = knockOutby.GetComponent<WizardBehavior>();
            mindControlling = true;
            if (knockOutby.team == "slythrin")
            {
                w.team = "griffindor";
                MeshRenderer me = knockOutby.GetComponent<MeshRenderer>();
                me.material = Resources.Load("WizardRed", typeof(Material)) as Material;
            }
            else
            {
                w.team = "slythrin";
                MeshRenderer me = knockOutby.GetComponent<MeshRenderer>();
                me.material = Resources.Load("WizardGreen", typeof(Material)) as Material;
            }
        }
        unconscious = true;
        rb.useGravity = true;


    }
    void onWakeup()
    {

        unconscious = false;
        currentExhaustion = 0;
        Main m = GameObject.FindGameObjectWithTag("main").GetComponent<Main>();
        initForce();
        m.knockouts--:
        Debug.Log(team + " wizard " + id + " is back in the game");
        if (sharingPower)
        {
            m.powershares--;
            sharingPower = false;
            if (team == "slythrin")
            {
                Debug.Log(team + " is now calmer");
                for (int i = 0; i < m.slythrin.Length; i++)
                {
                    if (m.slythrin[i].id == id)
                    {
                        continue;
                    }
                    m.slythrin[i].maxVelocity -= maxVelocity;
                    m.slythrin[i].maxExhaustion -= maxExhaustion;
                    m.slythrin[i].aggresiveness -= aggresiveness;
                }
            }
            else
            {
                for (int i = 0; i < m.griffindor.Length; i++)
                {
                    if (m.griffindor[i].id == id)
                    {
                        continue;
                    }
                    Debug.Log(team + " is now calmer");
                    m.griffindor[i].maxVelocity -= maxVelocity;
                    m.griffindor[i].maxExhaustion -= maxExhaustion;
                    m.griffindor[i].aggresiveness -= aggresiveness;
                }
            }

        }
        if (mindControlling == true)
        {
            m.mindControlls--;
            WizardBehavior w = knockOutby.GetComponent<WizardBehavior>();
            mindControlling = false ;
            if (knockOutby.team == "slythrin")
            {
                w.team = "griffindor";
                MeshRenderer me = knockOutby.GetComponent<MeshRenderer>();
                me.material = Resources.Load("WizardRed", typeof(Material)) as Material;
            }
            else
            {
                w.team = "slythrin";
                MeshRenderer me = knockOutby.GetComponent<MeshRenderer>();
                me.material = Resources.Load("WizardGreen", typeof(Material)) as Material;
            }
            knockOutby = null;
        }

    }
    IEnumerator wakeup()
    {
        rb.useGravity = false;
        yield return new WaitForSeconds(5);
        onWakeup();
    }
    void recharging()
    {
        currentExhaustion--;
    }
    public void initForce()
    {
        timer++;
        if (timer == 50)
        {
            currentExhaustion++;
            timer = 0;
        }
        Vector3 snitch = GameObject.FindGameObjectWithTag("snitch").transform.position;
        Vector3 vec2 = new Vector3(snitch.x, snitch.y, snitch.z);
        vec = (vec2 - transform.position).normalized;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);
        Vector3 rep = Vector3.zero;
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag == "snitch")
            {
                continue;
            }
            Vector3 obj = hitColliders[i].gameObject.transform.position;
            Vector3 vec3 = new Vector3(obj.x, obj.y, obj.z);
            rep = -(vec3 - transform.position).normalized;


        }
        //   rep = rep * repulsion;
        vec = vec + rep;
        rb.AddForce(vec * thrust, ForceMode.Force);
        opposite = -vec;
        float temp = rb.velocity.magnitude;

    }
    public void generateForce()
    {
        timer++;
        if(timer == 50)
        {
            currentExhaustion++;
            timer = 0;
        }
        Vector3 snitch = GameObject.FindGameObjectWithTag("snitch").transform.position;
        Vector3 vec2 = new Vector3(snitch.x, snitch.y, snitch.z);
        vec = (vec2 - transform.position).normalized;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);
        Vector3 rep = Vector3.zero;
        for(int i = 0; i< hitColliders.Length; i++)
        {
            if(hitColliders[i].gameObject.tag == "snitch")
            {
                continue;
            }
            Vector3 obj = hitColliders[i].gameObject.transform.position;
            Vector3 vec3 = new Vector3(obj.x, obj.y, obj.z);
            rep  = -(vec3 - transform.position).normalized;
            

        }
     //   rep = rep * repulsion;
        vec = vec + rep;
        rb.AddForce(opposite * thrust, ForceMode.Force);
        rb.AddForce(vec * thrust, ForceMode.Force);
        opposite = -vec;
        float temp = rb.velocity.magnitude;
        

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Land")
        {
            if (!unconscious)
            {
                unconscious = true;
                if (team == "griffindor")
                {
                    transform.position = new Vector3(Random.Range(xmin, xmax), 500, 490);
                    StartCoroutine(wakeup());
                }
                else
                {
                    transform.position = new Vector3(Random.Range(xmin, xmax), 500, -490);
                    StartCoroutine(wakeup());
                }
            }
        }
        if (collision.gameObject.tag == "wizard")
        {
            WizardBehavior w = collision.gameObject.GetComponent<WizardBehavior>();
            if (w.team != team)
            {
                System.Random rng = new System.Random();
                double player1 = aggresiveness * (rng.NextDouble() * (1.2 - 0.8) + 0.8) * (1 - (currentExhaustion / maxExhaustion));
                double player2 = w.aggresiveness * (rng.NextDouble() * (1.2 - 0.8) + 0.8) * (1 - (w.currentExhaustion / w.maxExhaustion));
                if (player1 <= player2)
                {
                    knockOutby = collision.gameObject; ;
                    onKnockOut();
                }
                else
                {
                    w.onKnockOut();
                    w.knockOutby = this;
                }
            }
            else
            {
                System.Random rng = new System.Random();
                double player1 = aggresiveness * (rng.NextDouble() * (1.2 - 0.8) + 0.8) * (1 - (currentExhaustion / maxExhaustion));
                double player2 = w.aggresiveness * (rng.NextDouble() * (1.2 - 0.8) + 0.8) * (1 - (w.currentExhaustion / w.maxExhaustion));
                 if (player1 <= player2 && rng.Next(0, 100) > 95)
                 {
                    Debug.Log("Friendly fire at " +team);
                    onKnockOut();
                  }
                 else if (rng.Next(0, 100) > 95)
                 {
                    Debug.Log("Friendly fire at " +team);
                    w.onKnockOut();
                  }
                }
            }
        }
    }
