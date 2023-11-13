using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 废弃类
/// </summary>
public class SwitchScene : MonoBehaviour
{
    bool isMainActive = true;
    public GameObject mainScene;
    public GameObject settingScene;

    public void ChangeSituation()
    {
        if (isMainActive)
        {
            this.GetComponentInChildren<Text>().text = "主菜单";
            mainScene.SetActive(false);
            settingScene.SetActive(true);
        }
        else
        {
            this.GetComponentInChildren<Text>().text = "设置";
            mainScene.SetActive(true);
            settingScene.SetActive(false);
        }
        isMainActive = !isMainActive;
    }

}
