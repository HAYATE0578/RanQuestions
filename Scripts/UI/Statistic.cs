using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 用于计算统计数据：总问题数，历史问题数，上一个问题的文字列
/// </summary>
public class Statistic : MonoBehaviour
{
    /// <summary>
    /// 总问题数
    /// </summary>
    public static int sumQuestions;

    /// <summary>
    /// 历史问题数
    /// </summary>
    public static int askedQuestions;

    /// <summary>
    /// 上一个问题的文字列
    /// </summary>
    public static string lastQuestion = "暂无";

    /// <summary>
    /// 更新总问题数
    /// </summary>
    /// <param name="num">总问题数</param>
    public void AskedIncrease(int num)
    {
        askedQuestions = num;
        InputString();
    }

    /// <summary>
    /// 更新历史问题数
    /// </summary>
    /// <param name="num">历史问题数</param>
    public void ConfirmSum(int num)
    {
        sumQuestions = num;
        InputString();
    }

    /// <summary>
    /// 更新上一个问题的文字列
    /// </summary>
    /// <param name="num">上个问题的文字列</param>
    public void LastQues(string mes)
    {
        lastQuestion = mes;
        InputString();
    }

    /// <summary>
    /// 输出总的文字列结果
    /// </summary>
    public void InputString()
    {
        string input = "";
        input += "总问题数: " + sumQuestions +",";
        input += "历史问题数: " + askedQuestions; 
        input += "\r\n";
        input += "上次问题: " + lastQuestion;
        this.GetComponentInChildren<Text>().text = input;
    }
}
