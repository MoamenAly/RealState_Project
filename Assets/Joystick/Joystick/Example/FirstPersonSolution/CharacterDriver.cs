using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class CharacterDriver : MonoBehaviour
{
    #region Character Move /角色移动
    CharacterController controller;
    public  float speed = 3;
    #endregion

    #region View Rotate/视角旋转
    [Header("Character Left Right Rotate Setting:")]
    public float rotateSensitivity = 30;    //方向灵敏度  
  //  public Slider rotateSensitivitySlider;

    [Header("Camera Up Down Rotate Setting:")]
    public Camera m_Camera;
   public float viewSensitivity = 3;    //上下最大视角条件灵敏度  
    public float upLimite = -20;  // 上仰角度限制
    public float dnLimite = 30;   //  俯视角度限制
    //public Slider viewSensitivitySlider;
    public  bool movechar = true;
    private static CharacterDriver instance;

    public static CharacterDriver Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<CharacterDriver>();
            return instance;
        }
    }
    #endregion
    void Start()
    {
        m_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void OnEnable()
    {
        controller = GetComponent<CharacterController>();
    }

  public static bool state = false; //idle isfalse
    /// <summary>
    /// Drive the character to move 
    /// 驱动角色移动
    /// </summary>
    /// <param name="v"></param>
    public void Move(Vector2 v)
    {
        if (v.x != 0 || v.y != 0)
        {
            Debug.Log(state);
            Vector3 direction = transform.TransformDirection(new Vector3(v.x, 0, v.y));
            if(movechar==true)
            controller.SimpleMove(direction * speed);
            if (state == false)
            {
                state = true;
            }
        }
        else
        {
            if (state == true)
            {
                state = false;
            }
        }
    }

    public void Rotate(Vector2 v)
    {
        if (v.x != 0)
        {
            transform.Rotate(-Vector3.up * v.x * rotateSensitivity * Time.deltaTime, Space.Self);
        }
        if (v.y != 0)
        {
            var current = m_Camera.transform.localEulerAngles.x;
            if (current >= 180) current -= 360;
            if ((current > -45 || v.y < 0) && (current < 60 || v.y > 0))
            {
                m_Camera.transform.Rotate(-Vector3.right * -v.y * viewSensitivity * Time.deltaTime, Space.Self);
            }
        }
    }
    public void onmouseremoved()
    {

    }

    private void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        if (v != 0 || h != 0)
        {
            Move(new Vector2(h, v));
        }
    }
}
