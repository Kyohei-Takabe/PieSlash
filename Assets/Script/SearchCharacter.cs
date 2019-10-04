using UnityEngine;
using System.Collections;

public class SearchCharacter : MonoBehaviour
{
    private MoveEnemy moveEnemy;
	public GameObject target;
    public Pond[] ponds;
	CharacterStatus status;

	//private GameObject HitRange;

	void Start()
	{
		status = GetComponent<CharacterStatus>();
		moveEnemy = GetComponent<MoveEnemy>();

	}

    void Update()
	{


		if (status.pieCream < 25)
        {
            //this.gameObject.GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
            MoveEnemy.EnemyState state = moveEnemy.GetState();
            if (state == MoveEnemy.EnemyState.Wait || state == MoveEnemy.EnemyState.Walk)
            {
				foreach(Pond pond in ponds){
					if(pond.hasCream){
						moveEnemy.SetState(MoveEnemy.EnemyState.Chase, pond.transform.transform);
					}
				}            
            }
        }
    }

	//playerが範囲内にいるときOnAreaはtrue
    void OnTriggerStay(Collider col)
    {

        //　プレイヤーキャラクターを発見
		//	freezeTiem間隔で攻撃をする
		if(moveEnemy.GetState() == MoveEnemy.EnemyState.Restore){
			return;
		}

		if (col.tag == "Player" && status.pieCream >= 25)
        {
            //　敵キャラクターの状態を取得
            MoveEnemy.EnemyState state = moveEnemy.GetState();
            //　敵キャラクターが追いかける状態でなければ追いかける設定に変更
            if (state == MoveEnemy.EnemyState.Wait || state == MoveEnemy.EnemyState.Walk)
            {
                moveEnemy.SetState(MoveEnemy.EnemyState.Chase, col.transform);
            }
            transform.LookAt(new Vector3(col.transform.position.x, col.transform.position.y, col.transform.position.z));
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("見失う");
            moveEnemy.SetState(MoveEnemy.EnemyState.Wait);
        }
    }


	//要　変更検討
    void OnTiggerEnter(Collider col)
    {        
		if (col.tag == "Pond" && status.pieCream < 25)
        {
			moveEnemy.SetState(MoveEnemy.EnemyState.Restore);
        }
    }
}



