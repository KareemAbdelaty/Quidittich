using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBehavior : MonoBehaviour
{
    public string team;
    public int aggresiveness;
    public int maxExhaustion;
    public int maxVelocity;
    public int weight;
    public int currentExhaustion;
    public int mindControl;
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
        upward = false;
    }


    void FixedUpdate()
    {
        generateForce();

    }
    void OnCollisionEnter()
    {

    }
    void onKnockOut()
    {

    }
    void onWakeup()
    {

    }
    void tired()
    {

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
}
