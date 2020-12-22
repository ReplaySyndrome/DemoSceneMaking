using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CommandSystem : MonoBehaviour
{
    private List<KeyValuePair<float, char>> keyboardAxisList;
    [SerializeField]
    private float listKeepTime = 1.0f;
    [SerializeField]
    private uint listMaxSize = 10;
    private Dictionary<string, int> commandDict;
    private char[] axisArray;

    private Animator playerAnimator;
    


    private const int mouseLeftButtonCode = 0;
    private const int mouseMiddleButtonCode = 1;
    private const int mouseRightButtonCode = 2;

    void Awake()
    {
        commandDict = new Dictionary<string, int>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        if(!playerAnimator)
        {
            Debug.Log("못찾았누");
        }
        keyboardAxisList = new List<KeyValuePair<float, char>>();
        //commandDict = new Dictionary<string, int>();
        axisArray = new char[4] { 'w', 'a', 's', 'd' } ;
    }

    // Update is called once per frame
    void Update()
    {
        AxisInputCheck();
    }

    void FixedUpdate()
    {
        ListCheck();
    }


    void AxisInputCheck() // 입력받는 코드 주제에 코드가 너무 길어요. 나중에 수정해야겠습니다.
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (keyboardAxisList.Count >= listMaxSize)
            {
                keyboardAxisList.RemoveAt(0);
            }
            keyboardAxisList.Add(new KeyValuePair<float, char>(Time.time, axisArray[0]));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (keyboardAxisList.Count >= listMaxSize)
            {
                keyboardAxisList.RemoveAt(0);
            }
            keyboardAxisList.Add(new KeyValuePair<float, char>(Time.time, axisArray[1]));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (keyboardAxisList.Count >= listMaxSize)
            {
                keyboardAxisList.RemoveAt(0);
            }
            keyboardAxisList.Add(new KeyValuePair<float, char>(Time.time, axisArray[2]));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (keyboardAxisList.Count >= listMaxSize)
            {
                keyboardAxisList.RemoveAt(0);
            }
            keyboardAxisList.Add(new KeyValuePair<float, char>(Time.time, axisArray[3]));
        }
    }

    void ListCheck() //시간이 지나거나 리스트가 꽉차면 앞에서부터 제거합니다.
    {
        for (int i = 0; i < keyboardAxisList.Count; ++i) //foreach로 해도 되긴 하지만 유니티 콘솔창에서 오류를 출력하네요.
        {
            if (Time.time - keyboardAxisList[i].Key > listKeepTime)
            {
                keyboardAxisList.Remove(keyboardAxisList[i]);
            }
        }
    }

    public void PrintDict()
    {
        foreach(var i in commandDict)
        {
            Debug.Log(i.Key + " " +  i.Value.ToString());
        }
    }

    public void SetCommand(ref string[] skillNameArray,ref string[] commandArray ) 
    // 반드시고쳐야한다 inspector에 반드시 pair로 노출시킨다.
    {
        Debug.Log(skillNameArray[0] + "," + commandArray[0]);
        for (int i=0;i<skillNameArray.Length;++i) // 둘이 길이가 같은지 계산하고 넘어오니깐 상관없다
        {
            commandDict.Add(commandArray[i], Animator.StringToHash(skillNameArray[i]));
        }
    }

    public void FindCommand() //제가 찾은알고리즘입니다.
    {
        if (Input.GetMouseButtonDown(mouseLeftButtonCode))
        {
            string keylist = "";
            foreach (var c in keyboardAxisList)
            {
                keylist += c.Value;
            }

            keyboardAxisList.ToString();
            string tofindcommandstring = "";
            string invokeCommand = "";

            for (int i = keylist.Length - 1; i >= 0; --i) //뒤에서 부터 검사합니다. O(n) ContainsKey == O(1)
            {
                tofindcommandstring = keylist[i] + tofindcommandstring;               
                if (commandDict.ContainsKey(tofindcommandstring))
                    //찾아도 빠져나가지 않습니다. 더 긴 커맨드가 있을 수 있으니까요                    
                {
                    invokeCommand = tofindcommandstring;
                }
            }



            //이렇게 해두면 추적이 어려울것 같은데 커맨드 없이 공격하는 스킬을 위해서는 어쩔수 없을 것 같습니다.
            //나중에 좋은 생각이 나면 수정해드리겠습니다.
            if (commandDict.ContainsKey(invokeCommand))
            {
                playerAnimator.SetTrigger(commandDict[invokeCommand]);
            }
            keyboardAxisList.Clear();
            
        }
    }

    public void ReSetCommand(string[] skillNamearr, ref UnityEngine.UI.InputField[] inputFields)
    // 첫번째 변수는 프로퍼티로 가져오는데 ref 한정자로 가져올수가 없네요 어쩔 수 없이 복사복을 생성합니다. 
    // 이제생각해보니 복사본이 더 안전한 것 같기도하네요.
    // 이거 중복검사를 해야하는데 아 그냥합시다. 알아서잘하겠지
    {
        commandDict.Clear();

        foreach(var i in inputFields)
        {
            Debug.Log(i.text);
        }


        for (int i = 0; i < skillNamearr.Length; ++i) // 둘이 길이가 같은지 계산하고 넘어오니깐 상관없다
        {
            commandDict.Add(inputFields[i].text, Animator.StringToHash(skillNamearr[i]));
        }

        

    }
}
