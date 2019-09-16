using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
public class SpawnZone : MonoBehaviour
{
    [System.Serializable]
    public class RoundInfo
    {
        [System.Serializable]
        public class SpwanInfo
        {
            //怪物类型
            public E_EnemyType EmemyType;
            /// <summary>
            /// 可能出现的点
            /// </summary>
            public SpwanPointEnemy[] SpwanPoint;
            //怪物出生延时
            public float SpwanDelay = 0.0f;
            //出生后是否转向玩家
            public bool RotateToPlayer = true;
            //主要用在boss上，当他死亡后就不在刷新怪
            public bool WhinKilledStopSpwan = false;
        }
        //一个波次出怪配置
        public SpwanInfo[] Spwans;
        //每个波次出怪间隔
        public float SpwanDelay = 0.0f;
        //下一波次出现的最小怪物数量
        public int MinEnemiesFomLastRound = 0;

    }
    //出怪状态
    public enum E_State
    {
        WaitStart,
        SpawnEnemys,
        WaitAllDead,
        Finished
    }
    //出怪配置信息
    public RoundInfo[] SpawnRounds;
    //所有怪物出生的点
    private SpwanPointEnemy[] SpwanPointEnemys;
    // Start is called before the first frame update
    public E_State State=E_State.WaitStart;
    void Awake()
    {
        SpwanPointEnemys = GetComponentsInChildren<SpwanPointEnemy>();
    }
    void Start()
    {
        
    }
    void LateUpdate()
    {
        if(State != E_State.WaitAllDead)
        {
            return;
        }
        //所有怪物都死亡，关卡结束开启下关
        if(true)
        {
            State = E_State.Finished;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
            return; 
        if(SpawnRounds!=null && SpawnRounds.Length>0)
        {
            //开始出怪
            StartCoroutine(SpawnEnemysInRounds());
        }
        else
        {
            //没有配置则在出怪点开始出怪
            StartCoroutine(SpawnEnemys());
        }
        BoxCollider box = GetComponent<BoxCollider>();
        box.enabled = false;

    }
    IEnumerator SpawnEnemysInRounds()
    {
        State = E_State.SpawnEnemys;
        for (int i=0;i< SpawnRounds.Length;i++)
        {
            RoundInfo round = SpawnRounds[i];
            float delay = round.SpwanDelay;
            
            while(delay>0)
            {
                yield return new WaitForSeconds(0.2f);
                delay-=0.2f;
            }
            for(int k=0;k<round.Spwans.Length;k++)
            {
                RoundInfo.SpwanInfo spwanInfo = round.Spwans[k];
                //出怪延时
                yield return new WaitForSeconds(spwanInfo.SpwanDelay);
                SpwanPointEnemy[] temp;
                if(spwanInfo.SpwanPoint!=null&& spwanInfo.SpwanPoint.Length>0)
                {
                    temp = spwanInfo.SpwanPoint;
                }
                else
                {
                    temp = SpwanPointEnemys;
                }
                //选择最佳出怪点
                SpwanPointEnemy point = temp[0];
                //改变怪物的朝像
                if(spwanInfo.RotateToPlayer==true)
                {
                    Vector3 playerPos=Vector3.zero;
                    Vector3 dir = playerPos - point.transform.position;
                    dir.Normalize();
                    point.transform.forward = dir;
                }
                //开场动画
                yield return new WaitForSeconds(0.5f);
                Agent enemy = CreateEnemy(point,spwanInfo.EmemyType);
                //添加缓存
                yield return new WaitForSeconds(1.0f);

            }
        }
        State = E_State.WaitAllDead;

    }
    IEnumerator SpawnEnemys()
    {
        State = E_State.SpawnEnemys;
        yield return new WaitForEndOfFrame();
        for(int i=0;i< SpwanPointEnemys.Length; i++)
        {
            yield return new WaitForSeconds(1.0f);
            //产生怪
            StartCoroutine(SpawnEnemy(SpwanPointEnemys[i]));
            yield return new WaitForSeconds(Random.Range(0.5f,1.5f));
        }
        yield return new WaitForSeconds(5.0f);
        State = E_State.WaitAllDead;
    }

    IEnumerator SpawnEnemy(SpwanPointEnemy point)
    {
        //todo: 开场动画
        yield return new WaitForSeconds(0.5f);
        //todo：创建怪
        CreateEnemy(point);

    }

    Agent CreateEnemy(SpwanPointEnemy point,E_EnemyType enemyType = E_EnemyType.None)
    {
        if(enemyType == E_EnemyType.None)
        {
            enemyType = point.EnemyType;
        }
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Monster");
        Agent enemy = GameObject.Instantiate(prefab).GetComponent<Agent>();
        if(enemy!=null)
        {
            enemy.transform.position = point.transform.position;
        }
        return enemy;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDrawGizmos()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        if(collider!=null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube( collider.center+ transform.position,collider.size);
        }
        if (SpwanPointEnemys == null)
            return;
        foreach(SpwanPointEnemy point in SpwanPointEnemys)
        {
            if(collider!=null)
            {
                Gizmos.DrawLine(transform.position+collider.center, point.transform.position);
            }else
            {
                Gizmos.DrawLine(transform.position, point.transform.position);
            }
        }
            
       
    }
}
