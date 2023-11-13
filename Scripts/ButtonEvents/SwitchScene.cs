using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������
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
            this.GetComponentInChildren<Text>().text = "���˵�";
            mainScene.SetActive(false);
            settingScene.SetActive(true);
        }
        else
        {
            this.GetComponentInChildren<Text>().text = "����";
            mainScene.SetActive(true);
            settingScene.SetActive(false);
        }
        isMainActive = !isMainActive;
    }

}
