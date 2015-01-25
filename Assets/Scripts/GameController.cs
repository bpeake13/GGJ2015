using UnityEngine;
using System.Collections;
using System.IO;

public class GameController : MonoBehaviour {

    //Singlton setup
    public static GameController Instance
    {
        get { return instance; }
    }
    private static GameController instance;



    //Public variables to change in the editor.
    public int BOARD_WIDTH;
    public int BOARD_HEIGHT;

    //Structure to hold the pieces that make up the board.
    PieceStructure pieces;

    //Create a structure to spawn items
    ItemSpawner itemSpawner;

    [SerializeField]
    bool isPlayback;

    BinaryWriter recordingWriter;

    BinaryReader playbackReader;

    public void GameFinish()
    {
        if (isPlayback)
            playbackReader.Close();
        else
            recordingWriter.Close();
    }

	// Use this for initialization
	void Start () {

        //Set the singleton
        instance = this;

        if(isPlayback)
        {
            string recordingFilePath = "recordings/rec_000.bin";
            if(File.Exists(recordingFilePath))
            {
                Stream playbackStream = File.OpenRead(recordingFilePath);
                playbackReader = new BinaryReader(playbackStream);

                Random.seed = playbackReader.ReadInt32();
            }
            else
            {
                isPlayback = false;
            }
        }

        if(!isPlayback)
        {
            string recordingFilePath = "recordings/rec_000.bin";
            if (!Directory.Exists("recordings"))
                Directory.CreateDirectory("recordings");

            Stream recordingStream = File.Open(recordingFilePath, FileMode.OpenOrCreate);
            recordingWriter = new BinaryWriter(recordingStream);

            recordingWriter.Write(Random.seed);
        }

        //Set up the piece structure/board
        pieces = new PieceStructure(gameObject, BOARD_WIDTH, BOARD_HEIGHT);

        //Set up the Board Generator to build the playing field.
        pieces.GenerateTiles();
        BoardGenerator.SpawnWallsOnBoard(pieces);

        //Initialize the item spawner
        itemSpawner = new ItemSpawner(pieces);
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.Delete))
        {
            Application.LoadLevel(0);
        }
	}

    /// <summary>
    /// Getter for the piece structure, holding the positions of all of the pieces on the board.
    /// </summary>
    /// <returns></returns>
    public PieceStructure GetPieceStructure()
    {
        return pieces;
    }

    /// <summary>
    /// Getter for the item spawner
    /// </summary>
    /// <returns></returns>
    public ItemSpawner GetItemSpawner()
    {
        return itemSpawner;
    }

    public bool IsPlayback
    {
        get { return isPlayback; }
    }

    public BinaryReader PlaybackReader
    {
        get { return playbackReader; }
    }

    public BinaryWriter RecordingWriter
    {
        get { return recordingWriter; }
    }
}
