using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomButton : MonoBehaviour
{

    TMPro.TMP_Text m_buttonText;
    private float m_Scale = 22f;
    private AudioSource m_AudioSource;

    private void Start()
    {
        m_buttonText = GetComponent<TMPro.TMP_Text>();
        m_Scale = m_buttonText.fontSize;
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void Select()
    {
        m_buttonText.fontSize = 24;
        m_AudioSource.Play();
    }

    public void Deselect()
    {
        m_buttonText.fontSize = m_Scale;
        m_AudioSource.Play();
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
