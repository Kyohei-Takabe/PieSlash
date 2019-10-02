using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public GameObject target;
    public GameObject pond;
    private NavMeshAgent agent;
    private BoxCollider coli;
    int PieGauge;
    //float agentspeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        coli = GetComponent<BoxCollider>();
        PieGauge = 25;
    }

    // Update is called once per frame
    void Update()
    {

        if (PieGauge >= 50)
        {
            //agent.destination = target.transform.position;
            //agent.stoppingDistance = 5;
            //agent.speed = agentspeed;
            coli.size = new Vector3(25, 1, 20);
            coli.center = new Vector3(0, 0, 5);
        }
        else if (PieGauge < 50)
        {
            //agent.destination = pond.transform.position;
            //agent.stoppingDistance = 0;
            coli.size = new Vector3(1, 1, 1);
            coli.center = new Vector3(0, 0, 0);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (PieGauge < 50)
        {
            //池が範囲内にない時
            if (other.gameObject.tag != "Pond")
            {
                //行動内容
                //agent.speed = agentspeed;
            }
            //池が範囲内にある時
            if (other.gameObject.tag == "Pond")
            {
                //行動内容
                PieGauge = 100;
            }
            if (PieGauge >= 25 && other.gameObject.tag == "Player")
            {
                //パイ投げの挙動

            }
        }
        //Playerが範囲内から出る時
        if (other.gameObject.tag == "Player")
        {
            //行動内容
            //agent.speed = agentspeed;
        }
    }
    void OnTriggerStay(Collider other)
    {
        //Playerが範囲内から出る時
        if (other.gameObject.tag == "Player")
        {
            //行動内容
            //agent.speed = agentspeed;
        }
    }
    void OnTriggerExit(Collider other)
    {
        //Playerが範囲内から出る時
        if (other.gameObject.tag == "Player")
        {
            //行動内容
            //agent.speed = 0.0f;
        }
    }
}