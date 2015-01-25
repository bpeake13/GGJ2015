using System;
using System.Collections.Generic;
using System.IO;
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
        {
            string recordingFilePath = "recordings/rec_000.bin";
            if (!Directory.Exists("recordings"))
                Directory.CreateDirectory("recordings");

            Stream src = GameController.Instance.RecordingWriter.BaseStream;
            int length = (int)src.Length;
            src.Seek(0, SeekOrigin.Begin);

            Stream dst = File.Open(recordingFilePath, FileMode.OpenOrCreate);

            byte[] data = new byte[length];
            src.Read(data, 0, length);
            dst.Write(data, 0, length);

            dst.Close();
            src.Close();
        }
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