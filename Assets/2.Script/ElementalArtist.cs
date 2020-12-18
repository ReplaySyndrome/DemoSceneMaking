﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class ElementalArtist : MonoBehaviour
{

    private static CharacterController characterController;
    private static Animator animator;

    private AnimatorStateInfo currStateInfo;

    private readonly int hashIdleWalk = Animator.StringToHash("IdleWalk");
    private readonly int hashCombo1 = Animator.StringToHash("Combo1");
    private readonly int hashCombo2 = Animator.StringToHash("Combo2");
    private readonly int hashCombo3 = Animator.StringToHash("Combo3");

    private CommandSystem commandSystem;

    


    public float speed = 5f;
    public float gravity = 9.8f;
    public float jumpspeed = 10f;
    public float rotSpeed = 5f;
    public Vector3 dir;
    public float laydistance = 40;

    [Header("Must Set Same Size")]
    [Tooltip("Please Fill In Animator Parameter Name")]
    public string[] skillNameArray;
    [Tooltip("Please Fill In Command Under 10")]
    public string[] commandArray;



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
            Debug.Log("커맨드 생성");
            commandSystem.SetCommand(ref skillNameArray, ref commandArray); 
            
        }
        commandSystem.PrintDict();
    }


    // Update is called once per frame
    void Update()
    {
        currStateInfo = animator.GetCurrentAnimatorStateInfo(0);


        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * rotSpeed * mouseX);


        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("시작");
            if (currStateInfo.IsName("IdleWalk")) //must fix
            {
                
                commandSystem.FindCommand();
                
            }
            else
            {
                animator.SetTrigger("NextCombo");
            }
        }


        
        if (characterController.isGrounded && currStateInfo.IsName("IdleWalk"))
        {

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            dir = new Vector3(x, 0, z);

            dir = transform.TransformDirection(dir);

            dir = dir * speed;

            if (Input.GetButton("Jump"))
            {
                dir.y = jumpspeed;
            }

        }

        dir.y -= gravity * Time.deltaTime;
        characterController.Move(dir * Time.deltaTime);

        animator.SetFloat("MoveX", dir.x);
        animator.SetFloat("MoveZ", dir.z);
        animator.SetFloat("SpeedY", dir.y);
        animator.SetBool("IsGround", characterController.isGrounded);
    }


}
