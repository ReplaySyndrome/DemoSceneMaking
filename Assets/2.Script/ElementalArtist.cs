using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class ElementalArtist : MonoBehaviour
{

    private static CharacterController characterController;
    private static Animator animator;

    public float speed = 5f;
    public float gravity = 9.8f;
    public float jumpspeed = 10f;
    public float rotSpeed = 5f;
    public Vector3 dir;
    public float laydistance = 40;
    bool isground = false;


    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, laydistance))
        {
            isground = true;
        }
        else
        {
            isground = false;
        }
        float mouseX = Input.GetAxis("Mouse X");
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("EnterCombo");
            Debug.Log("시작");
        }

        
        transform.Rotate(Vector3.up * rotSpeed * mouseX);
        if (isground)
        {
            
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            dir = new Vector3(x, 0, z);

            dir = transform.TransformDirection(dir);

            dir = dir * speed;
            
            if(Input.GetButton("Jump"))
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
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, Vector3.forward * laydistance);

    }


}
