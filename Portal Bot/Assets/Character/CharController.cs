using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [Header("Physics")]
    [SerializeField] private Rigidbody _ridgidBody;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce = 10.00f;
    [SerializeField] private float _gravityForce = -9.81f;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _groudLayer;

    [Header("Dashing")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashingTime = 0.25f;
    [SerializeField] private float dashingCooldown = 1f;
    [SerializeField] private TrailRenderer tr;
    private bool _activeDash = true;
    private bool _isDashing;

    private int JumpCounter = 0;
    private bool _charIsGrounded;


    private void Start()
    {
        _ridgidBody.centerOfMass = new Vector3(0f, 1f, 0f);
        _ridgidBody.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    AntiJump();
        //}
        if (Input.GetKeyDown(KeyCode.E) && _activeDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    public Rigidbody GetRB()
    {
        return _ridgidBody;
    }
    private void Jump()
    {
        _charIsGrounded = Physics.Raycast(_groundChecker.position, Vector3.down, 0.2f, _groudLayer);
        if (_charIsGrounded)
        {
            JumpCounter = 0;
        }
        if (JumpCounter < 2)
        {
            if (_charIsGrounded || JumpCounter < 2)
            {
                Debug.Log(JumpCounter);
                _ridgidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }
        JumpCounter++;
    }
    private void AntiJump()
    {
        if (!_charIsGrounded && JumpCounter == 2)
        {
            _ridgidBody.AddForce(Vector3.down * _gravityForce, ForceMode.VelocityChange);
        }
    }
    private void MoveCharacter()
    {
        float h = Input.GetAxis("Horizontal");

        Vector3 velocity = new Vector3(h, 0, 0) * _speed;
        velocity.y = _ridgidBody.velocity.y;
        Vector3 worldVelocity = transform.TransformVector(velocity);

        _ridgidBody.velocity = worldVelocity;
    }

    private IEnumerator Dash()
    {
        _activeDash = false;
        _isDashing = true;
        float originalGravity = _ridgidBody.angularDrag;
        _ridgidBody.velocity = new Vector3(transform.localScale.x * dashForce, 0f, 0f);
        yield return new WaitForSeconds(dashingTime);
        _ridgidBody.velocity = new Vector3(originalGravity, 0f, 0f);
        _isDashing= false;
        yield return new WaitForSeconds(dashingCooldown);
        _activeDash = true;
    }
}