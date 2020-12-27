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
    private CanvasGroup canvasGroup;

    void Awake()
    {
        inputFieldArray = GetComponentsInChildren<InputField>();
        //오류가 있다면 캔버스 하위의 inputfield의 순서를 조정해주세요.
        //위쪽에 있을수록 낮은 인덱스에 할당됩니다.
        applyButton = GetComponent<Button>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    // Start is called before the first frame update
    void Start()
    {
        player.GetCommand(ref inputFieldArray);
        canvasGroup.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            canvasGroup.interactable = !canvasGroup.interactable;
        }
    }

    public void ResetCommand()
    {
        bool isComplete = player.GetComponent<CommandSystem>().ReSetCommand(player.skillNameArray, ref inputFieldArray);
        if (isComplete)
        {
            player.ResetCommandArray(ref inputFieldArray);
        }
        player.GetCommand(ref inputFieldArray);
    }
}
