using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Pieを生成する機能を持つクラス
public class PieGenerator : MonoBehaviour
{
	public GameObject piePrefab;
<<<<<<< HEAD:Assets/Scripts/PieGenerator.cs
=======
	public CharacterStatus status;
>>>>>>> 895241d... 2019/10/02の作業分:Assets/Script/PieGenerator.cs
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public GameObject Generate(Transform _transform, Hand hand)
	{
		if(status.pieCream < 5.0f){
			return null;
		}
		if(SceneManager.GetActiveScene().name == "Play"){
			status.pieCream -= 5.0f;
		}
		Transform trans = _transform;
		//Vector3 size = _transform.localScale;

		if (hand == Hand.right)
		{
			trans.Translate(-0.05f, 0, 0);
		}
		if (hand == Hand.left)
		{
<<<<<<< HEAD:Assets/Scripts/PieGenerator.cs
			trans.Translate(0.05f, 0, 0);
=======
			trans.Translate(0.075f, 0, 0);
			trans.Rotate(0, 180, 0);
>>>>>>> 895241d... 2019/10/02の作業分:Assets/Script/PieGenerator.cs
		}

		trans.Rotate(new Vector3(0,0,90));

		GameObject newPie = Instantiate(piePrefab, trans);

		newPie.tag = "PlayerPie";

		return newPie;
	}
}
