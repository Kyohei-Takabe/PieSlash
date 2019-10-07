using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pond : MonoBehaviour
{
	public bool hasCream { get; set; }
	public Material material;
	Collider collider;
	float r, g, b;
	public float cream { get; set; }

	private void Awake()
	{
		r = material.color.r;
		g = material.color.g;
		b = material.color.b;
	}

	// Start is called before the first frame update
	void Start()
    {
		//this.material = GetComponentInChildren<Material>();
		collider = GetComponent<Collider>();

		hasCream = false;
		cream = 0.0f;
	}

    // Update is called once per frame
    void Update()
    {
		if(cream <= 0){
			hasCream = false;
		}
		if(hasCream){
			collider.isTrigger = true;
		}
		else{
			collider.isTrigger = false;
		}
    }
	private void OnTriggerStay(Collider other)
	{
		if (hasCream)
		{
			string tag = other.tag;
			if (tag == "Player" || tag == "Enemy")
			{
				CharacterStatus status = other.transform.GetComponent<CharacterStatus>();
				if (status.pieCream < 100.0f)
				{
					float pieCream = status.pieCream;
					if(cream <= 1.0f){
						status.pieCream += cream;
						cream = 0;
					}

					else{
						if (pieCream + 1.0f > 100.0f)
						{
							status.pieCream = 100.0f;
							float mod = 100.0f - pieCream;
							cream -= mod;
						}

						else
						{
							status.pieCream += 1.0f;
							cream -= 1.0f;
						}
					}
				}
			}
		}
	}

	public void VanishMaterial(){
		if (hasCream)
		{
			material.color = new Color(r, g, b, 1);
		}
		else
		{
			material.color = new Color(r, g, b, 0);
		}
	}
}
