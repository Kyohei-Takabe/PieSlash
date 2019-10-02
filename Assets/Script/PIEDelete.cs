using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIEDelete : MonoBehaviour
{
    GameObject enemy;
    SearchCharacter searchCharacter;
    void Start()
    {
        enemy = GameObject.Find("Enemy");
        searchCharacter = enemy.GetComponent<SearchCharacter>();
        searchCharacter.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 5, enemy.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        searchCharacter.attackflag = true;
        searchCharacter.pieflag = true;
    }
}
