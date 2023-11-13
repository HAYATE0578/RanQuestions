using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���ڼ���ͳ�����ݣ�������������ʷ����������һ�������������
/// </summary>
public class Statistic : MonoBehaviour
{
    /// <summary>
    /// ��������
    /// </summary>
    public static int sumQuestions;

    /// <summary>
    /// ��ʷ������
    /// </summary>
    public static int askedQuestions;

    /// <summary>
    /// ��һ�������������
    /// </summary>
    public static string lastQuestion = "����";

    /// <summary>
    /// ������������
    /// </summary>
    /// <param name="num">��������</param>
    public void AskedIncrease(int num)
    {
        askedQuestions = num;
        InputString();
    }

    /// <summary>
    /// ������ʷ������
    /// </summary>
    /// <param name="num">��ʷ������</param>
    public void ConfirmSum(int num)
    {
        sumQuestions = num;
        InputString();
    }

    /// <summary>
    /// ������һ�������������
    /// </summary>
    /// <param name="num">�ϸ������������</param>
    public void LastQues(string mes)
    {
        lastQuestion = mes;
        InputString();
    }

    /// <summary>
    /// ����ܵ������н��
    /// </summary>
    public void InputString()
    {
        string input = "";
        input += "��������: " + sumQuestions +",";
        input += "��ʷ������: " + askedQuestions; 
        input += "\r\n";
        input += "�ϴ�����: " + lastQuestion;
        this.GetComponentInChildren<Text>().text = input;
    }
}
