using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class ElementalArtist : MonoBehaviour
{
    //
    private static CharacterController characterController;
    private static Animator animator;
    private CommandSystem commandSystem;

    private AnimatorStateInfo currStateInfo;


    public float speed = 5f;
    public float gravity = 9.8f;
    public float jumpspeed = 10f;
    public float rotSpeed = 5f;
    public Vector3 dir;


    private bool canAirCombo = true;


    [Header("Must Set Same Size")]//고쳐야함니다.
    [Tooltip("Please Fill In Animator Parameter Name")]
    public string[] skillNameArray;
    [Tooltip("Please Fill In Command Under 10")]
    public string[] commandArray;

    public string[] GetSkillNameArray
    {
        get
        {
            return skillNameArray;
        }
    }


    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        commandSystem = GetComponent<CommandSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(skillNameArray.Length == commandArray.Length)
        {
            commandSystem.SetCommand(ref skillNameArray, ref commandArray); 
            
        }
        else
        {
            throw new System.Exception("스킬배열의 길이와 커맨드배열의 길이가 다릅니다. 둘의 길이가 같도록 수정해 주세요.");
        }
        commandSystem.PrintDict();
    }


    // Update is called once per frame
    void Update()
    {
        GetCurAnimatorStateCache();        
        CheckMouseInput();
        PlayerMove();
        SetAnimatorParameter();
    }

    void PlayerRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * rotSpeed * mouseX);
    }

    void GetCurAnimatorStateCache()
    {
        currStateInfo = animator.GetCurrentAnimatorStateInfo(0);
    }

    void CheckMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        //키가 여러가지가 있죠.
        //여기서는 왼쪽키만 사용했지만 마우스 중간버튼, 오른쪽버튼도 하려면 배열로 처리하면 될 것 같습니다.
        {
            if (currStateInfo.IsName("IdleWalk")) //must fix
            {
                commandSystem.FindCommand();
            }

            else if(currStateInfo.IsName("Jump") || currStateInfo.IsName("Fall"))
            {
                if (canAirCombo)
                {
                    canAirCombo = false;
                    animator.SetTrigger("AirCombo1");
                }
            }
            else
            {
                animator.SetTrigger("NextCombo");
            }
        }
    }

    void PlayerMove()
    {
        
        if (characterController.isGrounded && currStateInfo.IsName("IdleWalk"))
        {

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            dir = new Vector3(x, 0, z);

            dir = transform.TransformDirection(dir);
            PlayerRotate();
            dir = dir * speed;
            canAirCombo = true;
            if (Input.GetButton("Jump"))
            {
                dir.y = jumpspeed;
                animator.SetTrigger("Jump");
            }
        }

        if (characterController.isGrounded && !currStateInfo.IsName("IdleWalk"))
        {
            canAirCombo = true;
            dir = new Vector3(0, 0, 0);
        }

        if(currStateInfo.IsTag("InAir"))
        {
            dir = new Vector3(0, 0, 0);
        }

        dir.y -= gravity * Time.deltaTime;
        characterController.Move(dir * Time.deltaTime);
    }

    void SetAnimatorParameter()
    {
        animator.SetFloat("MoveX", dir.x);
        animator.SetFloat("MoveZ", dir.z);
        animator.SetFloat("SpeedY", dir.y);
        animator.SetBool("IsGround", characterController.isGrounded);
    }

    public void GetCommand(ref UnityEngine.UI.InputField[] inputfields)
    {
        for(int i=0;i<commandArray.Length;++i)
        {
            inputfields[i].text = commandArray[i];
        }
    }

    public void ResetCommandArray(ref UnityEngine.UI.InputField[] inputfields)
    {
        for(int i=0;i<inputfields.Length;++i)
        {
            commandArray[i] = inputfields[i].text;
        }
    }

}
