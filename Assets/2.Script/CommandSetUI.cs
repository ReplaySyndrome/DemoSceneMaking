using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandSetUI : MonoBehaviour
{
    [SerializeField]
    private InputField[] inputFieldArray;
    private Button applyButton;
    public ElementalArtist player;

    void Awake()
    {
        inputFieldArray = GetComponentsInChildren<InputField>();
        //오류가 있다면 캔버스 하위의 inputfield의 순서를 조정해주세요.
        //위쪽에 있을수록 낮은 인덱스에 할당됩니다.
        applyButton = GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        player.GetCommand(ref inputFieldArray);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetCommand()
    {
        player.GetComponent<CommandSystem>().ReSetCommand(player.skillNameArray, ref inputFieldArray);
        player.ResetCommandArray(ref inputFieldArray);
        player.GetCommand(ref inputFieldArray);
    }
}
