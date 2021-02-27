using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBehavior : MonoBehaviour
{
    public string team;
    public int aggresiveness;
    public int maxExhaustion;
    public bool exhausted;
    public int maxVelocity;
    public int weight;
    public int currentExhaustion;
    public int mindControl;
    public bool mindControlled;
    public bool sharingPower;
    public float repulsion;
    public int invulnerability;
    public int teamRage; //when dead distribute power
    public int rechargeRate;
    public bool unconscious;
    public int xmax;
    public int xmin;
    public int ymax;
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
        mindControlled = false;
        sharingPower = false;
        repulsion = 200;
    }


    void FixedUpdate()
    {
        if (!unconscious)
        {
            if (currentExhaustion == maxExhaustion)
            {
                exhausted = true;
            }
            else if (currentExhaustion == 0)
            {
                exhausted = false;
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
        unconscious = true;
        if(team == "griffindor")
        {
            transform.position = new Vector3(Random.Range(xmin, xmax), 10, 490);
        }
        else
        {
            transform.position = new Vector3(Random.Range(xmin, xmax), 10, -490);
        }
    }
    void onWakeup()
    {
        unconscious = false;
    }
    void recharging()
    {

    }
    public void generateForce()
    {
        Vector3 snitch = GameObject.FindGameObjectWithTag("snitch").transform.position;
        Vector3 vec2 = new Vector3(snitch.x, snitch.y, snitch.z);
        vec = (vec2 - transform.position).normalized;
        opposite = new Vector3(-vec.x, -vec.y, -vec.z);
        rb.AddForce(vec * thrust, ForceMode.Force);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Land")
        {
            onKnockOut();
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
                    onKnockOut();
                }
                else
                {
                    w.onKnockOut();
                }
            }
            else
            {
                System.Random rng = new System.Random();
                double player1 = aggresiveness * (rng.NextDouble() * (1.2 - 0.8) + 0.8) * (1 - (currentExhaustion / maxExhaustion));
                double player2 = w.aggresiveness * (rng.NextDouble() * (1.2 - 0.8) + 0.8) * (1 - (w.currentExhaustion / w.maxExhaustion));
                 if (player1 <= player2 && rng.Next(0, 100) > 95)
                 {
                    onKnockOut();
                  }
                 else if (rng.Next(0, 100) > 95)
                 {
                    w.onKnockOut();
                  }
                }
            }
        }
    }
