using UnityEngine;
using System.Collections;
public class MoveEnemy : MonoBehaviour
{
    public enum EnemyState
    {
        Walk,
        Wait,
        Chase,
        Attack,
        Freeze
    };

	const float GravityPower = 9.8f;
	private CharacterController enemyController;
    private Animator animator;
    //　目的地
    private Vector3 destination;
    //　歩くスピード
    [SerializeField]
    private float walkSpeed = 1.0f;
    //　速度
    private Vector3 velocity;
    //　移動方向
    private Vector3 direction;
    //　到着フラグ
    private bool arrived;
    //　SetPositionスクリプト
    private SetPosition setPosition;
    //　待ち時間
    [SerializeField]
    private float waitTime = 5f;
    //　経過時間
    private float elapsedTime;
    // 敵の状態
    private EnemyState state;
    //　プレイヤーTransform
    private Transform playerTransform;
    //　攻撃した後のフリーズ時間
    [SerializeField]
    private float freezeTime = 0.5f;

	CharacterStatus status;

	public GameObject PIE;

	//public Pond[] Ponds;
	// Use this for initialization
	void Start()
    {
        enemyController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        setPosition = GetComponent<SetPosition>();
        setPosition.CreateRandomPosition();
        velocity = Vector3.zero;
        arrived = false;
        elapsedTime = 0f;
        SetState(EnemyState.Walk);
		status = GetComponent<CharacterStatus>();
    }
    // Update is called once per frame
    void Update()
    {
		Vector3 localScale = transform.localScale;
		localScale.x = (status.mass + 40)/100;
		transform.localScale = localScale;
		//　見回りまたはキャラクターを追いかける状態
		if (state == EnemyState.Walk || state == EnemyState.Chase)
        {
            //　キャラクターを追いかける状態であればキャラクターの目的地を再設定
            if (state == EnemyState.Chase)
            {
                setPosition.SetDestination(playerTransform.position);
            }

            if (enemyController.isGrounded)
            {
                velocity = Vector3.zero;
                animator.SetFloat("Speed", 2.0f);
                direction = (setPosition.GetDestination() - transform.position).normalized;
                transform.LookAt(new Vector3(setPosition.GetDestination().x, transform.position.y, setPosition.GetDestination().z));
				velocity = direction * walkSpeed * status.acceralationRate;
				//velocity = transform.forward * walkSpeed * status.acceralationRate;
            }

            if (state == EnemyState.Walk)
            {
                //　目的地に到着したかどうかの判定
                if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 0.7f)
                {
                    SetState(EnemyState.Wait);
                    animator.SetFloat("Speed", 0.0f);
                }
            }
            else if (state == EnemyState.Chase)
            {
                //　攻撃する距離だったら攻撃
				if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 5f && status.pieCream >= 25.0f)
                {
                    SetState(EnemyState.Attack);
                }
            }
        }

        else if (state == EnemyState.Wait)
        {
            elapsedTime += Time.deltaTime;

            //　待ち時間を越えたら次の目的地を設定
            if (elapsedTime > waitTime)
            {
                SetState(EnemyState.Walk);
            }
            //　攻撃後のフリーズ状態
        }

        else if (state == EnemyState.Freeze)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > freezeTime)
            {
                SetState(EnemyState.Walk);
            }
        }

		else if (state == EnemyState.Attack){
			if (status.pieCream > 5.0f) {
				Transform trans = transform;
				trans.position = new Vector3(this.transform.position.x + 0.5f, this.transform.position.y + 1.5f, this.transform.position.z);
				trans.Rotate(0, 90, 0);

				//GameObject PIEs = Instantiate(PIE) as GameObject;
				GameObject PIEs = Instantiate(PIE, trans);
				PIEs.tag = "EnemyPie";
				PIEs.GetComponent<Rigidbody>().velocity = transform.forward * status.throwSpeed;
				status.pieCream -= 5.0f;

				SetState(EnemyState.Freeze);
			}
			else{
				SetState(EnemyState.Walk);
			}
		}

		//速度に重力分を加算する
		velocity += Vector3.down * GravityPower * Time.deltaTime;
		//接地している場合，地面に押し付けるための変数
		//これはCharaterContorollerの特性のため
		Vector3 snapGround = Vector3.zero;

		if (enemyController.isGrounded)
		{
			snapGround = Vector3.down;
		}

		enemyController.Move(velocity * Time.deltaTime + snapGround);
    }
    //　敵キャラクターの状態変更メソッド
    public void SetState(EnemyState tempState, Transform targetObj = null)
    {
        state = tempState;
        if (tempState == EnemyState.Walk)
        {
            arrived = false;
            elapsedTime = 0f;
            setPosition.CreateRandomPosition();
        }
        else if (tempState == EnemyState.Chase)
        {
            //　待機状態から追いかける場合もあるのでOff
            arrived = false;
            //　追いかける対象をセット
            playerTransform = targetObj;
        }
        else if (tempState == EnemyState.Wait)
        {
            elapsedTime = 0f;
            arrived = true;
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
        }
        else if (tempState == EnemyState.Attack)
        {
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            animator.SetBool("Attack", true);
        }
        else if (tempState == EnemyState.Freeze)
        {
            elapsedTime = 0f;
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            animator.SetBool("Attack", false);
        }
    }
    //　敵キャラクターの状態取得メソッド
    public EnemyState GetState()
    {
        return state;
    }
}