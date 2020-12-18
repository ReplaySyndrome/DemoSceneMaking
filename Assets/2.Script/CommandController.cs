using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct KeyTime
{
    public float presstime;
    public char keycode;

    public KeyTime(float t, char c)
    {
        presstime = t;
        keycode = c;
    }
}


public class CommandController : MonoBehaviour
{
    private List<KeyTime> keyboardAxisList = new List<KeyTime>(); //눌린 키들을 담아둘 자료구조
    public float listKeepTime = 1.0f; // 리스트에 눌린 키들을 담고있는 시간
    public uint listMaxSize = 10;
    public float printtime = 0.5f;
    private const int mouseLeftButtonCode = 0;

    private Dictionary<string, string> CommandDict = new Dictionary<string, string>();


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("PrintPerVariation"); //리스트 출력을 해제하고싶으시면 주석처리하세요.

        CommandDict.Add("W", "올려치기");
        CommandDict.Add("A", "왼쪽치기");
        CommandDict.Add("S", "내려치기");
        CommandDict.Add("D", "오른쪽치기");
        CommandDict.Add("WW", "대쉬치기");
        CommandDict.Add("WWW", "또대쉬치기");
        CommandDict.Add("WWWWWW", "연타치기");
        CommandDict.Add("WDA", "돌려치기");
        CommandDict.Add("WDDAS", "소환하기");
        CommandDict.Add("SS", "뒤로구르기");
        CommandDict.Add("AWDS", "딴짓하기");
        CommandDict.Add("WAD", "이걸못찾네병신");


    }

    // Update is called once per frame
    void Update()
    {
        ListCheck();
        AxisInputCheck();
        FindCommand();
    }

    IEnumerator PrintPerVariation() // 단지 출력용입니다.
    {
        while (true)
        {
            string s = "";

            foreach (KeyTime i in keyboardAxisList)
            {
                s += i.presstime.ToString() + " " + i.keycode.ToString() + " ";
            }
            Debug.Log(s);

            yield return new WaitForSeconds(printtime);
        }
    }
    void ListCheck() //시간이 지나거나 리스트가 꽉차면 앞에서부터 제거합니다.
    {
        for (int i = 0; i < keyboardAxisList.Count; ++i) //foreach로 해도 되긴 하지만 유니티 콘솔창에서 오류를 출력하네요.
        {
            if (Time.time - keyboardAxisList[i].presstime > listKeepTime)
            {
                keyboardAxisList.Remove(keyboardAxisList[i]);
            }
        }
    }
    void AxisInputCheck() // 입력받는 코드 주제에 코드가 너무 길어요. 나중에 수정해야겠습니다.
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (keyboardAxisList.Count >= listMaxSize)
            {
                keyboardAxisList.RemoveAt(0);
            }
            keyboardAxisList.Add(new KeyTime(Time.time, 'W'));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (keyboardAxisList.Count >= listMaxSize)
            {
                keyboardAxisList.RemoveAt(0);
            }
            keyboardAxisList.Add(new KeyTime(Time.time, 'A'));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (keyboardAxisList.Count >= listMaxSize)
            {
                keyboardAxisList.RemoveAt(0);
            }
            keyboardAxisList.Add(new KeyTime(Time.time, 'S'));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (keyboardAxisList.Count >= listMaxSize)
            {
                keyboardAxisList.RemoveAt(0);
            }
            keyboardAxisList.Add(new KeyTime(Time.time, 'D'));
        }


    }
    void FindCommand()
    {
        if (Input.GetMouseButtonDown(mouseLeftButtonCode))
        {
            string keylist = "";
            foreach (KeyTime c in keyboardAxisList)
            {
                keylist += c.keycode;
            }
            string tofindcommandstring = "";
            string invokeCommand = null;

            for (int i = keylist.Length - 1; i >= 0; --i) //뒤에서 부터 검사합니다.
            {
                tofindcommandstring = keylist[i] + tofindcommandstring;

                foreach (var dict in CommandDict)
                {

                    if (dict.Key.Contains(tofindcommandstring))
                    {
                        if (dict.Key.Equals(tofindcommandstring))
                        {
                            invokeCommand = tofindcommandstring;
                        }
                    }

                }
            }


            if (invokeCommand != null)
            {
                Debug.Log(invokeCommand);
                Debug.Log(CommandDict[invokeCommand]); //이부분을 위해서 Dict로 했습니다. 여기서라도 빨리찾아야죠.
                keyboardAxisList.Clear();
            }
        }
    }

}
