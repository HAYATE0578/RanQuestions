using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 废弃类
/// </summary>
public class BackGroundController : MonoBehaviour
{
    public GameObject[] backs;

    // Start is called before the first frame update
    void Start()
    {
        //("游戏背景总数为："+backs.Length);
    }

    public void ChooseBackByIndex(int index)
    {
        foreach(GameObject g in backs)
        {
            g.SetActive(false);
            GC.Collect();
        }
        backs[index].SetActive(true);
    }

    public int GetLen()
    {
        return backs.Length;
    }
}
