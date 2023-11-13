using System.Collections;
using System.IO;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// 负责选择问题后，1播放动画 2播放音效 3计入统计 4选择问题和背景 的一系列操作
/// </summary>
public class ClickToQuestions : MonoBehaviour
{
    /// <summary>
    /// 计算选题动画时间间隔
    /// </summary>
    float helpTime = 0;

    /// <summary>
    /// 使用过的问题索引池
    /// </summary>
    HashSet<int> usedIndex = new HashSet<int>();

    /// <summary>
    /// 容量为5，防止短期重复使用过的队列
    /// </summary>
    Queue<int> usedBackQueue = new Queue<int>(10);

    /// <summary>
    /// 容量为1，储存上次问题的栈
    /// </summary>
    Stack<string> usedQues = new Stack<string>(1);

    /// <summary>
    /// 问题池子
    /// </summary>
    string[] questions = { };

    /// <summary>
    /// 屏幕中心问题栏
    /// </summary>
    public Text quesionText;

    /// <summary>
    /// 手动添加音效播放器和切片。
    /// </summary>
    public AudioSource clickAudio;
    public AudioSource pageAudio1;
    public AudioSource pageAudio2;
    public AudioSource pageAudio3;
    public AudioSource pageAudio4;
    public AudioSource endAudio;

    /// <summary>
    /// 问题开始按钮
    /// </summary>
    public Button clickButton;

    /// <summary>
    /// 设置按钮
    /// </summary>
    public Button settingButton;

    /// <summary>
    /// 设置背景的下拉菜单的物体
    /// </summary>
    public GameObject dropBackMenuObj;

    /// <summary>
    /// 动画间隔时间
    /// </summary>
    public float intervalTime = 1f;

    /// <summary>
    /// 动画总持续时间
    /// </summary>
    public float waitTime = 3f;

    /// <summary>
    /// 问题池子URL
    /// </summary>
    public string questionsURL = @"C:\Questions\tmp.txt";

    /// <summary>
    /// 统计数据
    /// </summary>
    public GameObject statistic;

    /// <summary>
    /// 是否是循环游戏模式（暂不使用）
    /// </summary>
    public bool isLoopGameMode = false;

    /// <summary>
    /// 背景动画物体
    /// </summary>
    public GameObject backScene;

    /// <summary>
    /// 背景动画源
    /// </summary>
    public GameObject backSource;

    /// <summary>
    /// 随机选定问题，用于动画
    /// </summary>
    public void ClickToPickQuesionRandomlyAnimation()
    {
        //播放按钮音效
        clickAudio.Play();
        Debug.Log("播放抽选音效");

        //按钮组暂时不可交互
        clickButton.interactable = false;
        settingButton.interactable = false;
        Debug.Log("设置按钮可交互false");

        //设置按钮暂不显示
        settingButton.GetComponentInChildren<Text>().text="";
        Debug.Log("按钮文本暂不显示");

        //等待时间后，做剩下的执行
        StartCoroutine(WaitButtonTime(waitTime));
    }


    void Start()
    {
        AddQuesToPool();
        Debug.Log("Start中执行添加问题方法");
    }

    /// <summary>
    /// 通过URL添加问题进入问题池(START执行)
    /// </summary>
    void AddQuesToPool()
    {
        try
        {
            questions = questions.Concat(File.ReadAllLines(questionsURL)).ToArray();
            Debug.Log("已经获得问题池。");
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString() + "因为文件异常，连接失败。");
        }
    }

    void Update()
    {
        AnimationOfQuestions();
    }

    /// <summary>
    /// 使用时间间隔抽选实现动画效果(UPDATE执行)
    /// </summary>
    void AnimationOfQuestions()
    {
        if (helpTime < Time.time && clickButton.interactable == false)
        {
            Debug.Log("不断改变问题中");

            //随机的选择“翻页”的声音同时不断改变问题。
            PickAudioRandomly();
            ChangeQuesionsRandomly();

            helpTime = Time.time + intervalTime;
            Debug.Log("辅助动画计算时间已重写");
        }
    }

    /// <summary>
    /// 一轮抽选后的最终执行
    /// </summary>
    /// <param name="seconds">动画的总体持续时间</param>
    /// <returns></returns>
    IEnumerator WaitButtonTime(float seconds)
    {
        //等待中
        yield return new WaitForSeconds(seconds);
        Debug.Log("动画duration结束");

        //使按钮可交互
        clickButton.interactable = true;
        settingButton.interactable = true;
        Debug.Log("设置按钮可交互true");

        //设置按钮的字段重新显示
        settingButton.GetComponentInChildren<Text>().text = "设置";
        Debug.Log("按钮文本重新显示");

        //执行最后的改变问题方法
        ChangeQuesionsRandomlyAndMemo();

        //播放抽选完成的提示音
        endAudio.Play();
        Debug.Log("播放完成提示音");
    }

    /// <summary>
    /// 实现动画的抽选问题方法
    /// </summary>
    void ChangeQuesionsRandomly()
    {
        int index = Random.Range(0, questions.Length);
        Debug.Log("动画抽选：随机抽选数生成");

        //防止一个问题被重新选择

        index = Random.Range(0, questions.Length);
        Debug.Log("动画抽选：选到一次重复值");
        
        quesionText.text = questions[index];


        Debug.Log("动画抽选：本轮结束");
    }

    /// <summary>
    /// 实现动画结束最终问题的抽选方法
    /// </summary>
    void ChangeQuesionsRandomlyAndMemo()
    {
        int index = Random.Range(0, questions.Length);
        Debug.Log("最终抽选：随机抽选数生成");

        //防止一个问题被重新选择
        while (usedIndex.Contains(index))
        {
            index = Random.Range(0, questions.Length);
            Debug.Log("最终抽选：选到一次重复值");

            //所有的问题都被问过后的执行
            if (usedIndex.Count == questions.Length)
            {
                quesionText.text = "恭喜，您已经问过了所有的问题！单击屏幕重新开始！";

                //清除历史池和统计记录
                usedIndex.Clear();
                AskedCal(0);

                Debug.Log("最终抽选：问题池耗尽");
                return;
            }
        }

        //如果是非循环模式，添加使用过的问题进入历史池，数字进入统计
        if (isLoopGameMode)
            quesionText.text = questions[index];
        else if (!isLoopGameMode)
        {
            usedIndex.Add(index);
            Debug.Log("历史池添加一次");

            //屏幕中心显示抽选问题
            quesionText.text = questions[index];

            //屏幕左下角统计
            SumCal(questions.Length);
            AskedCal(usedIndex.Count());

            //通过栈，来实现统计中出现上一次选择的问题。
            if (usedQues.Count == 0)
            {
                usedQues.Push(questions[index]);
            }
            else
            {
                ShowLastQues(usedQues.Pop());
                usedQues.Push(questions[index]);
            }

            //执行一次动画的抽选
            ChangeBackGroundRandomly();
        }
    }

    /// <summary>
    /// 随机选取翻页声音实现动画。
    /// </summary>
    void PickAudioRandomly()
    {
        int index = Random.Range(0, 4);
        Debug.Log("随机选取一次翻页声音");

        switch (index)
        {
            case 0:
                pageAudio1.Play();
                break;
            case 1:
                pageAudio2.Play();
                break;
            case 2:
                pageAudio3.Play();
                break;
            case 3:
                pageAudio4.Play();
                break;
        }
    }

    /// <summary>
    /// 设置使用过的问题数字
    /// </summary>
    /// <param name="num">使用过的问题数</param>
    void AskedCal(int num)
    {
        statistic.GetComponent<Statistic>().AskedIncrease(num);
        Debug.Log("历史问题数已添加到统计");
    }

    /// <summary>
    /// 确定总问题数字
    /// </summary>
    /// <param name="num">总问题数</param>
    void SumCal(int num)
    {
        statistic.GetComponent<Statistic>().ConfirmSum(num);
        Debug.Log("总问题数已添加到统计");
    }

    /// <summary>
    /// 把上次选择的问题添加到最底下
    /// </summary>
    /// <param name="mes">上轮问题</param>
    void ShowLastQues(string mes)
    {
        statistic.GetComponent<Statistic>().LastQues(mes);
        Debug.Log("上次问题已添加");
    }


    /// <summary>
    /// 每一次问题选定后，随机改变背景
    /// </summary>
    void ChangeBackGroundRandomly()
    {
        //获得背景源
        VideoClip [] sourceArr = backSource.GetComponentInChildren<ChooseBackground>().vc;

        //随机设置背景的索引
        int randomIndex =Random.Range(0, sourceArr.Length);

        //通过进队出队防止短期内重复一个背景
        while (usedBackQueue.Contains(randomIndex))
        {
            randomIndex = Random.Range(0, sourceArr.Length);
            Debug.Log("历史背景重选");
        }

        //入队
        if (usedBackQueue.Count <= sourceArr.Length*3/4)//防止过多的背景来回重复。
            usedBackQueue.Enqueue(randomIndex);

        //过载则出队
        else if (usedBackQueue.Count > sourceArr.Length * 3 / 4)
        {
            usedBackQueue.Dequeue();
            usedBackQueue.Enqueue(randomIndex);
        }

        //设置新的随机背景，延时避免闪光弹
        StartCoroutine(WaitWait(0.17f));
        backScene.GetComponentInChildren<VideoPlayer>().clip = sourceArr[randomIndex];
        dropBackMenuObj.GetComponent<Dropdown>().value = randomIndex;

        Debug.Log("新的随机背景选择");
    }

    /// <summary>
    /// 设置延迟防止闪光弹
    /// </summary>
    /// <param name="time">设定延迟读取视频时间</param>
    /// <returns></returns>
    IEnumerator WaitWait(float time)
    {
        //全黑色会覆盖视频。最后把Renderer调回白色。
        backScene.GetComponentInChildren<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(time);
        backScene.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
}
