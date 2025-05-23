﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static InputExtension;

public class TouchPad : MonoBehaviour
{
    /// <summary>
    /// 以此字符开头的UI不会阻碍 TouchPad 拖拽
    /// </summary>
    public string filterPrefix = "#";
    public TouchPadEvent OnTouchPadValueChanged = new TouchPadEvent();
    [System.Serializable]
    public class TouchPadEvent : UnityEvent<Vector2> { }

    //记录的那些不在UI上触发的点 ，matian
    private List<Touch> fingerIds = new List<Touch>();
    public static TouchPad instance;
    public CharacterDriver characterDriver;
    void Awake()
    {
        instance = this;
        characterDriver = GameObject.FindObjectOfType<CharacterDriver>();
        OnTouchPadValueChanged.AddListener(characterDriver.Rotate);
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (var item in Input.touches)
            {
                int index = fingerIds.FindIndex(touch => touch.fingerId == item.fingerId);
                if (index == -1)
                {
                    //2. 收集有效 Touch ，其特点为：刚按下 + 没打到UI上 + 在屏幕右侧
                    if (item.phase == TouchPhase.Began && item.position.x > Screen.width * 0.5f && !item.IsRaycastUI(filterPrefix))
                    {
                        fingerIds.Add(item);
                    }
                }
                else
                {
                    if (item.phase == TouchPhase.Ended)
                    {
                        fingerIds.RemoveAt(index); //3. 如果此Touch 已失效（unity 会回传 phase = end 的 touch），则剔除之
                    }
                    else
                    {
                        fingerIds[index] = item; //4. 由于Touch是  非引用类型的临时变量，所以要主动更新之
                    }
                }
            }

            //5. 有效Touch 处于 move 则可以驱动事件了：
            foreach (var item in fingerIds)
            {
                if (item.phase == TouchPhase.Moved)
                {
                    OnTouchPadValueChanged.Invoke(item.deltaPosition);
                }
            }
        }
        if (Input.GetMouseButtonDown(1) && !IsMouseRaycastUI("#"))
        {
            canmove = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            canmove = false;
        }
        if (Input.GetMouseButton(1) && canmove)
        {
            var h = Input.GetAxis("Mouse X");
            var v = Input.GetAxis("Mouse Y");
            OnTouchPadValueChanged.Invoke(new Vector2(h, v));
        }

#if UNITY_EDITOR
        // 仅供编辑器测试
        if (Input.GetMouseButtonDown(1)&&!IsMouseRaycastUI("#"))
        {
            canmove = true;
        }
        if (Input .GetMouseButtonUp(1))
        {
            canmove = false;
        }
        if (Input.GetMouseButton(1)&&canmove)
        {
            var h = Input.GetAxis("Mouse X");
            var v = Input.GetAxis("Mouse Y");
            OnTouchPadValueChanged.Invoke(new Vector2(h, v));
        }
#endif
    }
    private bool canmove;

}
