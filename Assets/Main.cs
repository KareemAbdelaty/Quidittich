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
    // Start is called before the first frame update
    void Start()
    {
        //create floor
        Instantiate(land);
        //create snitch
        Instantiate(snitch);
        //create wizards
        Instantiate(wizard);
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

}
