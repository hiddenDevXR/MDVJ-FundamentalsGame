using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    Rigidbody m_rigidbody;
    [SerializeField]
    CapsuleCollider m_capsuleCollider;
    [SerializeField]
    Animator m_animator;
    [SerializeField]
    GameObject root;
    
    
    bool dead = false;

    public GameObject item;
    public GameObject explotionVFX;

    public List<Collider> ragdollParts = new List<Collider>();

    private float hp = 100;

    private void Awake()
    {
        SetRagdoll();
    }

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_capsuleCollider = GetComponent<CapsuleCollider>();
        m_animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(dead) return;

        if(GameManager.playerController.transform.position.x < transform.position.x)
        {
            transform.localEulerAngles = new Vector3(0f, -90f, 0f);
        }

        else if(GameManager.playerController.transform.position.x > transform.position.x)
        {
            transform.localEulerAngles = new Vector3(0f, 90f, 0f);
        }
    }

    public void TakeDamage()
    {

        m_animator.SetInteger("State", 1);

        hp -= 40;

        if(hp < 0)
        {
            StartCoroutine(ManageRagDoll());
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 1f, 0f);
            GameObject  itemInstance = Instantiate(item, spawnPosition, Quaternion.identity);
            dead = true;
        }
    }

    private void SetRagdoll()
    {
        Collider[] colliders = root.GetComponentsInChildren<Collider>();
        foreach (Collider coll in colliders)
        {
            coll.isTrigger = true;
            coll.gameObject.layer = 6;
            ragdollParts.Add(coll);
        }
    }
    public float upForce;
    IEnumerator ManageRagDoll()
    {
        m_rigidbody.AddForce(Vector3.up * upForce);
        yield return new WaitForSeconds(0.5f);
        TurnOnRagDoll();
    }

    private void TurnOnRagDoll()
    {
        m_rigidbody.useGravity = false;
        m_rigidbody.velocity = Vector3.zero;
        explotionVFX.SetActive(true);
        GetComponent<AudioSource>().Play();
        m_capsuleCollider.enabled = false;
        m_animator.avatar = null;
        m_animator.enabled = false;
        
        
        foreach(Collider coll in ragdollParts)
        {
            coll.attachedRigidbody.velocity = Vector3.zero;
            coll.isTrigger = false;
        }
    }

    public void GoBackToIdle()
    {
        m_animator.SetInteger("State", 0);
    }
}
