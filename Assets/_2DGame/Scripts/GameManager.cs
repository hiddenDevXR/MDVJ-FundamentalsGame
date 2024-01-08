using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController m_playerController;

    [SerializeField]
    private TMPro.TMP_Text timeText;

    public static PlayerController playerController;
    public static float remainingTime = 60;

    public GameObject endScreen, winScreen, lossScreen;
    RecollectorSystem recollectorSystem;
    public enum  State { Intro, Game, Pause, Dead }
    public static State state = State.Intro;

    void Awake()
    {
        remainingTime = 60f;
        state = State.Intro;
        playerController = m_playerController;
        recollectorSystem = playerController.GetComponent<RecollectorSystem>();
    }

    private void Update()
    {
        if (state == State.Intro) return;

        if(state == State.Game)
        {
            remainingTime -= Time.deltaTime;
            timeText.text = "0:" + Mathf.Floor(remainingTime).ToString();

            if(remainingTime <= 0)
            {       
                if (recollectorSystem.GetFillProgress() < 1)
                    OnLose();
            }

            else if(remainingTime > 0)
            {
                if (recollectorSystem.GetFillProgress() >= 1)
                    OnWin();
            }
        }
    }

    public void BeginGame()
    {
        state = State.Game;
    }

    void OnWin()
    {
        endScreen.SetActive(true);
        winScreen.SetActive(true);
        GetComponent<PoolingTest>().Unsuscribe();
    }

    void OnLose()
    {
        endScreen.SetActive(true);
        lossScreen.SetActive(true);
        GetComponent<PoolingTest>().Unsuscribe();
    }
}
