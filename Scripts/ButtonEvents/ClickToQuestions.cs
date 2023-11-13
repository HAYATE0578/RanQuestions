using System.Collections;
using System.IO;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// ����ѡ�������1���Ŷ��� 2������Ч 3����ͳ�� 4ѡ������ͱ��� ��һϵ�в���
/// </summary>
public class ClickToQuestions : MonoBehaviour
{
    /// <summary>
    /// ����ѡ�⶯��ʱ����
    /// </summary>
    float helpTime = 0;

    /// <summary>
    /// ʹ�ù�������������
    /// </summary>
    HashSet<int> usedIndex = new HashSet<int>();

    /// <summary>
    /// ����Ϊ5����ֹ�����ظ�ʹ�ù��Ķ���
    /// </summary>
    Queue<int> usedBackQueue = new Queue<int>(10);

    /// <summary>
    /// ����Ϊ1�������ϴ������ջ
    /// </summary>
    Stack<string> usedQues = new Stack<string>(1);

    /// <summary>
    /// �������
    /// </summary>
    string[] questions = { };

    /// <summary>
    /// ��Ļ����������
    /// </summary>
    public Text quesionText;

    /// <summary>
    /// �ֶ������Ч����������Ƭ��
    /// </summary>
    public AudioSource clickAudio;
    public AudioSource pageAudio1;
    public AudioSource pageAudio2;
    public AudioSource pageAudio3;
    public AudioSource pageAudio4;
    public AudioSource endAudio;

    /// <summary>
    /// ���⿪ʼ��ť
    /// </summary>
    public Button clickButton;

    /// <summary>
    /// ���ð�ť
    /// </summary>
    public Button settingButton;

    /// <summary>
    /// ���ñ����������˵�������
    /// </summary>
    public GameObject dropBackMenuObj;

    /// <summary>
    /// �������ʱ��
    /// </summary>
    public float intervalTime = 1f;

    /// <summary>
    /// �����ܳ���ʱ��
    /// </summary>
    public float waitTime = 3f;

    /// <summary>
    /// �������URL
    /// </summary>
    public string questionsURL = @"C:\Questions\tmp.txt";

    /// <summary>
    /// ͳ������
    /// </summary>
    public GameObject statistic;

    /// <summary>
    /// �Ƿ���ѭ����Ϸģʽ���ݲ�ʹ�ã�
    /// </summary>
    public bool isLoopGameMode = false;

    /// <summary>
    /// ������������
    /// </summary>
    public GameObject backScene;

    /// <summary>
    /// ��������Դ
    /// </summary>
    public GameObject backSource;

    /// <summary>
    /// ���ѡ�����⣬���ڶ���
    /// </summary>
    public void ClickToPickQuesionRandomlyAnimation()
    {
        //���Ű�ť��Ч
        clickAudio.Play();
        Debug.Log("���ų�ѡ��Ч");

        //��ť����ʱ���ɽ���
        clickButton.interactable = false;
        settingButton.interactable = false;
        Debug.Log("���ð�ť�ɽ���false");

        //���ð�ť�ݲ���ʾ
        settingButton.GetComponentInChildren<Text>().text="";
        Debug.Log("��ť�ı��ݲ���ʾ");

        //�ȴ�ʱ�����ʣ�µ�ִ��
        StartCoroutine(WaitButtonTime(waitTime));
    }


    void Start()
    {
        AddQuesToPool();
        Debug.Log("Start��ִ��������ⷽ��");
    }

    /// <summary>
    /// ͨ��URL���������������(STARTִ��)
    /// </summary>
    void AddQuesToPool()
    {
        try
        {
            questions = questions.Concat(File.ReadAllLines(questionsURL)).ToArray();
            Debug.Log("�Ѿ��������ء�");
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString() + "��Ϊ�ļ��쳣������ʧ�ܡ�");
        }
    }

    void Update()
    {
        AnimationOfQuestions();
    }

    /// <summary>
    /// ʹ��ʱ������ѡʵ�ֶ���Ч��(UPDATEִ��)
    /// </summary>
    void AnimationOfQuestions()
    {
        if (helpTime < Time.time && clickButton.interactable == false)
        {
            Debug.Log("���ϸı�������");

            //�����ѡ�񡰷�ҳ��������ͬʱ���ϸı����⡣
            PickAudioRandomly();
            ChangeQuesionsRandomly();

            helpTime = Time.time + intervalTime;
            Debug.Log("������������ʱ������д");
        }
    }

    /// <summary>
    /// һ�ֳ�ѡ�������ִ��
    /// </summary>
    /// <param name="seconds">�������������ʱ��</param>
    /// <returns></returns>
    IEnumerator WaitButtonTime(float seconds)
    {
        //�ȴ���
        yield return new WaitForSeconds(seconds);
        Debug.Log("����duration����");

        //ʹ��ť�ɽ���
        clickButton.interactable = true;
        settingButton.interactable = true;
        Debug.Log("���ð�ť�ɽ���true");

        //���ð�ť���ֶ�������ʾ
        settingButton.GetComponentInChildren<Text>().text = "����";
        Debug.Log("��ť�ı�������ʾ");

        //ִ�����ĸı����ⷽ��
        ChangeQuesionsRandomlyAndMemo();

        //���ų�ѡ��ɵ���ʾ��
        endAudio.Play();
        Debug.Log("���������ʾ��");
    }

    /// <summary>
    /// ʵ�ֶ����ĳ�ѡ���ⷽ��
    /// </summary>
    void ChangeQuesionsRandomly()
    {
        int index = Random.Range(0, questions.Length);
        Debug.Log("������ѡ�������ѡ������");

        //��ֹһ�����ⱻ����ѡ��

        index = Random.Range(0, questions.Length);
        Debug.Log("������ѡ��ѡ��һ���ظ�ֵ");
        
        quesionText.text = questions[index];


        Debug.Log("������ѡ�����ֽ���");
    }

    /// <summary>
    /// ʵ�ֶ���������������ĳ�ѡ����
    /// </summary>
    void ChangeQuesionsRandomlyAndMemo()
    {
        int index = Random.Range(0, questions.Length);
        Debug.Log("���ճ�ѡ�������ѡ������");

        //��ֹһ�����ⱻ����ѡ��
        while (usedIndex.Contains(index))
        {
            index = Random.Range(0, questions.Length);
            Debug.Log("���ճ�ѡ��ѡ��һ���ظ�ֵ");

            //���е����ⶼ���ʹ����ִ��
            if (usedIndex.Count == questions.Length)
            {
                quesionText.text = "��ϲ�����Ѿ��ʹ������е����⣡������Ļ���¿�ʼ��";

                //�����ʷ�غ�ͳ�Ƽ�¼
                usedIndex.Clear();
                AskedCal(0);

                Debug.Log("���ճ�ѡ������غľ�");
                return;
            }
        }

        //����Ƿ�ѭ��ģʽ�����ʹ�ù������������ʷ�أ����ֽ���ͳ��
        if (isLoopGameMode)
            quesionText.text = questions[index];
        else if (!isLoopGameMode)
        {
            usedIndex.Add(index);
            Debug.Log("��ʷ�����һ��");

            //��Ļ������ʾ��ѡ����
            quesionText.text = questions[index];

            //��Ļ���½�ͳ��
            SumCal(questions.Length);
            AskedCal(usedIndex.Count());

            //ͨ��ջ����ʵ��ͳ���г�����һ��ѡ������⡣
            if (usedQues.Count == 0)
            {
                usedQues.Push(questions[index]);
            }
            else
            {
                ShowLastQues(usedQues.Pop());
                usedQues.Push(questions[index]);
            }

            //ִ��һ�ζ����ĳ�ѡ
            ChangeBackGroundRandomly();
        }
    }

    /// <summary>
    /// ���ѡȡ��ҳ����ʵ�ֶ�����
    /// </summary>
    void PickAudioRandomly()
    {
        int index = Random.Range(0, 4);
        Debug.Log("���ѡȡһ�η�ҳ����");

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
    /// ����ʹ�ù�����������
    /// </summary>
    /// <param name="num">ʹ�ù���������</param>
    void AskedCal(int num)
    {
        statistic.GetComponent<Statistic>().AskedIncrease(num);
        Debug.Log("��ʷ����������ӵ�ͳ��");
    }

    /// <summary>
    /// ȷ������������
    /// </summary>
    /// <param name="num">��������</param>
    void SumCal(int num)
    {
        statistic.GetComponent<Statistic>().ConfirmSum(num);
        Debug.Log("������������ӵ�ͳ��");
    }

    /// <summary>
    /// ���ϴ�ѡ���������ӵ������
    /// </summary>
    /// <param name="mes">��������</param>
    void ShowLastQues(string mes)
    {
        statistic.GetComponent<Statistic>().LastQues(mes);
        Debug.Log("�ϴ����������");
    }


    /// <summary>
    /// ÿһ������ѡ��������ı䱳��
    /// </summary>
    void ChangeBackGroundRandomly()
    {
        //��ñ���Դ
        VideoClip [] sourceArr = backSource.GetComponentInChildren<ChooseBackground>().vc;

        //������ñ���������
        int randomIndex =Random.Range(0, sourceArr.Length);

        //ͨ�����ӳ��ӷ�ֹ�������ظ�һ������
        while (usedBackQueue.Contains(randomIndex))
        {
            randomIndex = Random.Range(0, sourceArr.Length);
            Debug.Log("��ʷ������ѡ");
        }

        //���
        if (usedBackQueue.Count <= sourceArr.Length*3/4)//��ֹ����ı��������ظ���
            usedBackQueue.Enqueue(randomIndex);

        //���������
        else if (usedBackQueue.Count > sourceArr.Length * 3 / 4)
        {
            usedBackQueue.Dequeue();
            usedBackQueue.Enqueue(randomIndex);
        }

        //�����µ������������ʱ�������ⵯ
        StartCoroutine(WaitWait(0.17f));
        backScene.GetComponentInChildren<VideoPlayer>().clip = sourceArr[randomIndex];
        dropBackMenuObj.GetComponent<Dropdown>().value = randomIndex;

        Debug.Log("�µ��������ѡ��");
    }

    /// <summary>
    /// �����ӳٷ�ֹ���ⵯ
    /// </summary>
    /// <param name="time">�趨�ӳٶ�ȡ��Ƶʱ��</param>
    /// <returns></returns>
    IEnumerator WaitWait(float time)
    {
        //ȫ��ɫ�Ḳ����Ƶ������Renderer���ذ�ɫ��
        backScene.GetComponentInChildren<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(time);
        backScene.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
}
