using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// 主要用于设置选项中背景的选择
/// </summary>
public class ChooseBackground : MonoBehaviour
{

    /// <summary>
    /// 获取播放背景的游戏物体
    /// </summary>
    public GameObject background;
    public VideoClip[] vc;
    
    /// <summary>
    /// 通过VALUE数值改变视频
    /// </summary>
    /// <param name="value">视频数组的索引数字</param>
    public void ChangeBackground(int value)
    {
        background.GetComponentInChildren<VideoPlayer>().clip= vc[value];
        //("背景已经改变");
    }
}
