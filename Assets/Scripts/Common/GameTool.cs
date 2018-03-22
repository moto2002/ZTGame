﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using XLua;

[LuaCallCSharp]
public class GameTool
{
    public static void Log(params object[] args)
    {
        string str = "";

        for (int i = 0; i < args.Length; i++)
        {
            if (i == 0)
            {
                if (null != args[i])
                    str = args[i].ToString();
                else
                    str = "null";
            }
            else
            {
                if (null != args[i])
                    str = str + "  " + args[i].ToString();
                else
                    str = str + "  " + "null";
            }
        }
        Debug.Log(str);
    }

    public static void LogError(params object[] args)
    {
        string str = "";

        for (int i = 0; i < args.Length; i++)
        {
            if (i == 0)
            {
                if (null != args[i])
                    str = args[i].ToString();
                else
                    str = "null";
            }
            else
            {
                if (null != args[i])
                    str = str + "  " + args[i].ToString();
                else
                    str = str + "  " + "null";
            }
        }
        Debug.LogError(str);
    }

    //是否点击在ui上
    public static bool IsPointerOverUIObject(Vector2 screenPosition)
    {
        //判断是否点击的是UI，有效应对安卓没有反应的情况，true为UI  
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    //是否已经释放了
    public static bool IsDestroyed(GameObject gameObject)
    {
        return gameObject == null && !ReferenceEquals(gameObject, null);
    }

    public static float SignedAngleBetween(Vector3 a, Vector3 b)
    {
        Vector3 n = Vector3.down;
        float angle = Vector3.Angle(a, b);
        float sign = Mathf.Sign(Vector3.Dot(n, Vector3.Cross(a, b)));
        float signed_angle = angle * sign;
        return (signed_angle < 0) ? 360 + signed_angle : signed_angle;
    }
}
