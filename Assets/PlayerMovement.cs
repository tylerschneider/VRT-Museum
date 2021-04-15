using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    public void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();

        Vector3 inputx = transform.GetChild(0).Find("Main Camera").right * inputVec.x;
        inputx.y = 0;
        Vector3 inputy = transform.GetChild(0).Find("Main Camera").forward * inputVec.y;
        inputy.y = 0;

        GetComponent<CharacterController>().Move(inputx * Time.deltaTime * speed);
        GetComponent<CharacterController>().Move(inputy * Time.deltaTime * speed);
    }
}
