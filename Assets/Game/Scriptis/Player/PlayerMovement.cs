using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (characterController == null)
        {
            Debug.LogWarning("CharacterController is missing!", this);
        }

        Keyboard keyboard = Keyboard.current;
        if ( keyboard == null )
        {
            return;
        }
        
        Vector3 direction = Vector3.zero;
        if (keyboard.leftArrowKey.isPressed)
        {   
            direction = Vector3.left;
        }
        if (keyboard.rightArrowKey.isPressed)
        {
            direction = direction + Vector3.right;
        }
        if (keyboard.upArrowKey.isPressed)
        {
            direction.x = 0;
            direction = Vector3.forward;
        }
        if (keyboard.downArrowKey.isPressed)
        {
            direction.x = 0;
            direction = direction + Vector3.back;
        }
        

        Vector3 dimention = direction * speed * Time.deltaTime;
        characterController.Move(dimention);
    }
}
