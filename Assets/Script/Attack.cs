using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    private SearchCharacter searchCharacter;
    bool attackflag;
    public GameObject PIE;
    int PIEgaege = 100;

    void Start()
    {
        searchCharacter.GetComponentInParent<SearchCharacter>();
        attackflag = true;
    }
    void Update()
    {
        if(attackflag == true && searchCharacter.OnArea == true)
        {
            PIE.GetComponent<Rigidbody>().AddForce(searchCharacter.target.transform.position);
            attackflag = false;
        }
        if(PIEgaege >= 25)
        {
            GameObject PIEs = Instantiate(PIE) as GameObject;
            PIE.transform.position = (searchCharacter.target.transform.position);
        }
    }

}