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
    public int mindcontrol;
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
        if (p.y < 0)
        {
            p.y = 200;
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
        rb.velocity = Vector3.zero;
        Main m = GameObject.FindGameObjectWithTag("main").GetComponent<Main>();
        System.Random rng = new System.Random();
        int n = rng.Next(0, 100);
        if(n <= invulnerability)
        {
            Debug.Log(team + " wizard " + id + " is INVULNERABLE");
            currentExhaustion += 10;
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
                    m.slythrin[i].maxVelocity += maxVelocity;
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
                    m.griffindor[i].maxVelocity += maxVelocity;
                    m.griffindor[i].aggresiveness += aggresiveness;
                }
            }
        }
        if (knockOutby != null)
        {
            WizardBehavior w = knockOutby.GetComponent<WizardBehavior>();
            if (w.mindcontrol < mindcontrol)
            {
                m.mindcontrolls++;
                mindControlling = true;
                Debug.Log(w.team + " wizard " + id + "'s mind has been hexed");
                if (w.team == "slythrin")
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
        }
        unconscious = true;
        rb.useGravity = true;
        m.knockouts++;


    }
    void onWakeup()
    {

        unconscious = false;
        Main m = GameObject.FindGameObjectWithTag("main").GetComponent<Main>();
        initForce();
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
                    if(m.slythrin[i].maxVelocity - maxVelocity > 0)
                    {
                        m.slythrin[i].maxVelocity -= maxVelocity;
                    }
                    if (m.slythrin[i].aggresiveness - aggresiveness > 0)
                    {
                        m.slythrin[i].aggresiveness -= aggresiveness; 
                    }

                }
            }
            else
            {
                for (int i = 0; i < m.griffindor.Length; i++)
                {
                    if (m.griffindor[i].id == id)
                    {
                        Debug.Log("Passed");
                        continue;
                    }
                    if (m.griffindor[i].maxVelocity - maxVelocity > 0)
                    {
                        m.griffindor[i].maxVelocity -= maxVelocity; 
                    }
                    if (m.griffindor[i].aggresiveness - aggresiveness > 0)
                    {
                        m.griffindor[i].aggresiveness -= aggresiveness;
                    }
                    
                }
                Debug.Log(team + " is now calmer");
            }

        }
        if (mindControlling == true)
        {
            m.mindcontrolls--;
            WizardBehavior w = knockOutby.GetComponent<WizardBehavior>();
            mindControlling = false ;
            if (w.team == "slythrin")
            {
                w.team = "griffindor";
                Debug.Log(w.team + " wizard " + id + "'s mind is now free ");
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
        knockOutby = null;
    }
    IEnumerator wakeup()
    {
        rb.useGravity = false;
        yield return new WaitForSeconds(20);
        onWakeup();
    }
    void recharging()
    {

       currentExhaustion--;

        
    }
    public void initForce()
    {
        timer++;
        if (timer == 1000)
        {
            currentExhaustion++;
            timer = 0;
        }
        Vector3 snitch = GameObject.FindGameObjectWithTag("snitch").transform.position;
        Vector3 vec2 = new Vector3(snitch.x, snitch.y, snitch.z);
        vec = (vec2 - transform.position).normalized;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 200);
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
        rep = rep * repulsion;
        vec = vec + rep;
        vec = Vector3.ClampMagnitude(vec, maxVelocity);
        rb.AddForce(vec, ForceMode.Force);
        opposite = -vec;
        float temp = rb.velocity.magnitude;

    }
    public void generateForce()
    {
        timer++;
        if(timer == 1000)
        {
            currentExhaustion++;
            timer = 0;
        }
        Vector3 snitch = GameObject.FindGameObjectWithTag("snitch").transform.position;
        Vector3 vec2 = new Vector3(snitch.x, snitch.y, snitch.z);
        vec = (vec2 - transform.position).normalized;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 200);
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
        rep = rep * repulsion;
        vec = vec + rep;
        vec = Vector3.ClampMagnitude(vec, maxVelocity);
        rb.AddForce(opposite * thrust, ForceMode.Force);
        rb.AddForce(vec * thrust, ForceMode.Force);
        opposite = -vec;
        

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Land")
        {
                Debug.Log(team + " wizard " + id + " has been knocked out by hitting the ground");
                unconscious = true;
                if (team == "griffindor")
                {
                    transform.position = new Vector3(Random.Range(xmin, xmax), 1000, 490);
                    StartCoroutine(wakeup());
                }
                else
                {
                    transform.position = new Vector3(Random.Range(xmin, xmax), 1000, -490);
                    StartCoroutine(wakeup());
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
                    knockOutby = collision.gameObject;
                    Debug.Log(team + " wizard " + id + " has been knocked out by " + w.team + " " + w.id);
                    onKnockOut();
                }
                else
                {
                    Debug.Log(w.team + " wizard " + w.id + " has been knocked out by " + team + " " + id);
                    w.onKnockOut();
                    w.knockOutby = gameObject;
                }
            }
            else
            {
                System.Random rng = new System.Random();
                double player1 = aggresiveness * (rng.NextDouble() * (1.2 - 0.8) + 0.8) * (1 - (currentExhaustion / maxExhaustion));
                double player2 = w.aggresiveness * (rng.NextDouble() * (1.2 - 0.8) + 0.8) * (1 - (w.currentExhaustion / w.maxExhaustion));
                 if (player1 <= player2 && rng.Next(0, 100) > 95)
                 {
                    Debug.Log("Friendly fire at " +team + ", " + w.id + " has knocked out " + id );
                    onKnockOut();
                  }
                 else if (rng.Next(0, 100) > 95)
                 {
                    Debug.Log("Friendly fire at " + team + ", " + id + " has knocked out " + w.id);
                    w.onKnockOut();
                  }
                }
            }
        }
    }
