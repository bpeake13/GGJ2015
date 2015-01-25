using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class GameOverState : GameState
{
    public GameOverState(int winnerIndex)
    {
        this.winnerIndex = winnerIndex;
    }

    public override void Enter()
    {
        if (GameController.Instance.IsPlayback)
            GameController.Instance.PlaybackReader.Close();
        else
            GameController.Instance.RecordingWriter.Close();
        WinnerDataStructure.Create(winnerIndex);
    }

    public override void Update()
    {
        timer -= Time.unscaledDeltaTime;
        if (timer <= 0)
            Application.LoadLevel(3);
    }

    public override void Exit()
    {
        
    }

    private int winnerIndex;

    private float timer = 2f;
}