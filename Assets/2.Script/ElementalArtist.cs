using System.Collections;
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



    public float speed = 5f;
    public float gravity = 9.8f;
    public float jumpspeed = 10f;
    public float rotSpeed = 5f;
    public Vector3 dir;
    public float laydistance = 40;
    //bool isground = false;




    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        currStateInfo = animator.GetCurrentAnimatorStateInfo(0);





        float mouseX = Input.GetAxis("Mouse X");
        if (Input.GetMouseButtonDown(0))
        {
            if (currStateInfo.IsName("IdleWalk")) //must fix
            {
                animator.SetTrigger("EnterCombo");
                Debug.Log("시작");
            }
            else
            {
                animator.SetTrigger("NextCombo");
            }
        }


        transform.Rotate(Vector3.up * rotSpeed * mouseX);
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
