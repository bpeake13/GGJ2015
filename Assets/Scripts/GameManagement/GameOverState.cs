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
        Application.LoadLevel(2);
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        
    }

    private int winnerIndex;
}