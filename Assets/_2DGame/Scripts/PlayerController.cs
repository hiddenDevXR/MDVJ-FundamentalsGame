using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody m_rigidbody;
    public Animator m_animator;
    private AttackSystem m_attackSystem;
    [SerializeField]
    private AudioSource voiceAudioSource;
    [SerializeField]
    private AudioSource actionAudioSource;

    private Vector3 direction = Vector3.zero;
    [SerializeField]
    private bool grounded = true;
    private bool blocked = false;
    public float speed = 3f;
    public float jumpForcer = 10f;
    public AudioClip jumpClip;
    public AudioClip stepClip;

    PlayerBaseState state;
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerJumpState jumpState = new PlayerJumpState();

    enum PlayerState { Idle, Run, Jump, Jump_End, Dead, Attack };
    PlayerState currentState = PlayerState.Idle;

    

    void Start()
    {
        state = idleState;
        state.EnterState(this);

        if (m_rigidbody == null)
            m_rigidbody = GetComponent<Rigidbody>();
        if (m_animator == null)
            m_animator = GetComponent<Animator>();

        m_attackSystem = GetComponent<AttackSystem>();
    }

    private void Update()
    {
        if (GameManager.state == GameManager.State.Intro) return;

        state.UpdateState(this);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            m_rigidbody.AddForce(new Vector2(m_rigidbody.velocity.x, jumpForcer));
            SetAnimation(PlayerState.Jump);
            grounded = false;
            voiceAudioSource.clip = jumpClip;
            voiceAudioSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SetAnimation(PlayerState.Attack);
        }
    }

    //private void PlayActionClip(AudioClip clip)
    //{
    //    m_audioSource.clip = clip;
    //    m_audioSource.pitch = Random.Range(1f, 1.1f);
    //    m_audioSource.Play();
    //}

    private void FixedUpdate()
    {

        if (GameManager.state == GameManager.State.Intro) return;

        float step = Time.deltaTime * speed;
        direction = new Vector3(Input.GetAxis("Horizontal") * step, m_rigidbody.velocity.y, 0f);

        if (direction.x != 0f)
        {
            if (direction.x < 0f)
            {
                transform.localEulerAngles = new Vector3(0f, -90f, 0f);
                m_attackSystem.direction = -1;
            }
                
            else if (direction.x > 0f)
            {
                transform.localEulerAngles = new Vector3(0f, 90f, 0f);
                m_attackSystem.direction = 1;
            }
        }

        if (grounded)
            SetAnimation(PlayerState.Run);            

        if (direction.x == 0f && grounded)
            SetAnimation(PlayerState.Idle);

        m_rigidbody.velocity = direction;
    }

    private void SetAnimation(PlayerState state)
    {
        m_animator.SetInteger("State", (int)state);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Limit"))
        {
            if(!grounded)
            {
                SetAnimation(PlayerState.Jump_End);
                grounded = true;
            }
        } 
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Limit"))
            blocked = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Limit"))
            blocked = false;
    }

    public void TakeDamage()
    {
        SetAnimation(PlayerState.Dead);
    }

    public Vector3 GetVelocity()
    {
        return m_rigidbody.velocity;
    }

    public bool IsBlocked()
    {
        return blocked;
    }   
    
    public void SwitchState(PlayerBaseState state)
    {
        this.state = state;
        state.EnterState(this);
    }

    public void PlayStepSFX()
    {
        actionAudioSource.pitch = Random.Range(1f, 2f);
        actionAudioSource.Play();
    }
}
