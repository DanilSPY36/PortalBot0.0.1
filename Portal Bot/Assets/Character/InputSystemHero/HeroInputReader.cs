using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInputReader : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    [SerializeField] private RayCastPortal _portalCast;
    private HeroInput _inputAction;

    private void Awake()
    {
        _inputAction = new HeroInput();
        _inputAction.Hero.HorizontalMovement.performed += OnHorizontalMovement;
        _inputAction.Hero.HorizontalMovement.canceled += OnHorizontalMovement;

        _inputAction.Hero.Jump.performed += JumpUP;

        _inputAction.Hero.LeftPortalCast.performed += PortalCastL;
        _inputAction.Hero.RightPortalCast.performed += PortalCastR;
    }

    private void OnEnable()
    {
        _inputAction.Enable();
    }
    private void JumpUP(InputAction.CallbackContext context)
    {
        _hero.Jump();
    }
    private void OnHorizontalMovement(InputAction.CallbackContext context)
    {
        
        float direction =  context.ReadValue<float>();
        _hero.SetDirection(new Vector3(direction, 0f, 0f));
    }
    private void OnSay(InputAction.CallbackContext context)
    {
        _hero.Say();
    }
    private void PortalCastL(InputAction.CallbackContext context)
    {
        _hero.PortalCastL();
    }
    private void PortalCastR(InputAction.CallbackContext context)
    {
        _hero.PortalCastR();

    }
}
