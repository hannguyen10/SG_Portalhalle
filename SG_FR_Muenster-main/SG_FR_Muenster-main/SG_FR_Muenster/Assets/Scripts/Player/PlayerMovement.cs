using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
   


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        isGrounded = controller.isGrounded;
    }
    //receive inputs and apply to character controller
    public void ProcessMove(Vector2 input)
    {
        if (Dialogue.IsDialogueActive || QuizManager.IsQuizActive)
            return;
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

       
        controller.Move(playerVelocity * Time.deltaTime);

    }

}
