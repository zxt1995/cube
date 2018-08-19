using UnityEngine;
using System.Collections;
/// <summary>
/// Camera move.
/// 挂载对象：摄像机
/// 作用：摄像机跟随人物
/// </summary>
public class CameraMove : MonoBehaviour
{
    //摄像机的移动速度
    public float moveSpeed = 3f;
    //摄像机的旋转速度
    public float turnSpeed = 10f;
    //定义私有玩家
    private Transform m_Player;
    //摄像机与玩家之间的初始偏移量
    private Vector3 offset;
    //射线的碰撞信息
    private RaycastHit hit;
    //摄像机与玩家之间的距离
    private float distance;
    //摄像机的观察点
    private Vector3[] currentPoints;
    //通过Awake拿到自身的组件
    void Awake()
    {
        //定义一个v3类型的数组，里面有5个元素
        currentPoints = new Vector3[5];
    }
    //初始化游戏对象
    void Start()
    {
        while (true)
        {
            m_Player = GameObject.FindWithTag("Player").transform;
            if (m_Player != null)
                break;
        }
        //游戏开始时摄像机与玩家之间的距离
        distance = Vector3.Distance(transform.position, m_Player.position);
        //摄像机指向玩家
        //玩家与摄像机之间的偏移量
        offset = m_Player.position - transform.position;
    }


    //物理引擎相关的放到FixedUpdate中
    //LateUpdate可以避免卡顿
    void LateUpdate()
    {
        //摄像机观察的第一个点
        Vector3 startPosition = m_Player.position - offset;
        //摄像机的最后一个点
        Vector3 endPosition = m_Player.position + Vector3.up * distance;
        //把摄像机的五个观察点放到数组中，并且1，2,3三个观察点使用线性插值让摄像机平滑移动Slerp
        currentPoints[1] = Vector3.Slerp(startPosition, endPosition, 0.25f);
        currentPoints[2] = Vector3.Slerp(startPosition, endPosition, 0.5f);
        currentPoints[3] = Vector3.Slerp(startPosition, endPosition, 0.75f);
        currentPoints[0] = startPosition;
        currentPoints[4] = endPosition;
        //定义一个变量用来存储固定帧可以看到玩家的观察点
        //viewposition = currentPoints [0]
        Vector3 viewposition = currentPoints[0];
        //for循环遍历这些点，如果找到最合适的点就把那个当前点赋值给可以看到玩家的观察点CheckView检测某个点能否看到玩家
        for (int i = 0; i < currentPoints.Length; i++)
        {
            //如果检测到某个点可以看到玩家
            if (CheckView(currentPoints[i]))
            {
                //把这个当前点赋值给viewposition
                viewposition = currentPoints[i];
                //之后返回不在继续遍历
                break;
            }
        }
        //把摄像机移动到观察点
        transform.position = Vector3.Lerp(transform.position, viewposition, Time.deltaTime * moveSpeed);
        //调用摄像机旋转方法
        SmoothRotate();
    }
    /// <summary>
    /// Checks the view.
    /// 检测某个点是否可以看到玩家
    /// </summary>
    /// <returns><c>true</c>, if view was checked, <c>false</c> otherwise.</returns>
    /// <param name="pos">Position.</param>
    //检测某个点能否看到玩家的方法bool类型
    bool CheckView(Vector3 pos)
    {
        //定义玩家与观察点之间的方向向量
        Vector3 dir = m_Player.position - pos;
        //发射射线
        if (Physics.Raycast(pos, dir, out hit))
        {
            //如果射线打到玩家
            if (hit.collider.tag == "Player")
            {
                //返回true
                return true;
            }
        }
        //不然返回false
        return false;
    }
    /// <summary>
    /// Smooths the rotate.
    /// 摄像机旋转的方法
    /// </summary>
    /// 摄像机旋转的方法
    void SmoothRotate()
    {
        //指向起始位置
        //摄像机到玩家的向量
        Vector3 m_Dir = m_Player.position - transform.position;
        //要旋转的角度
        Quaternion qua = Quaternion.LookRotation(m_Dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, qua, Time.deltaTime * turnSpeed);
        //把摄像机x,y轴锁死
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, 0);

    }
}
