using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_audioSource;
    [SerializeField]
    private MeshRenderer m_meshRenderer;
    [SerializeField]
    private SphereCollider m_sphereCollider;

    private void Start()
    {
        if(m_audioSource == null)
            m_audioSource = GetComponent<AudioSource>();
        if(m_meshRenderer == null)
            m_meshRenderer = GetComponentInChildren<MeshRenderer>();
        if( m_sphereCollider == null)
            m_sphereCollider = GetComponent<SphereCollider>();
    }

    public void Remove()
    {
        m_meshRenderer.enabled = false;
        m_sphereCollider.enabled = false;
        m_audioSource.pitch = Random.Range(0.5f, 1.5f);
        m_audioSource.Play();
        Destroy(gameObject, 1f);
    }

    IEnumerator DisableOnTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    public void PoolRemove()
    {
        m_meshRenderer.enabled = false;
        m_sphereCollider.enabled = false;
        m_audioSource.pitch = Random.Range(0.5f, 1.5f);
        m_audioSource.Play();
        StartCoroutine(DisableOnTime(1));
    }

    public void OnEnable()
    {
        m_meshRenderer.enabled = true;
        m_sphereCollider.enabled = true;
    }
}
