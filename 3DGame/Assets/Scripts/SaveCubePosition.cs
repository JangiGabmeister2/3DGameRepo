using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class SaveCubePosition : MonoBehaviour
{
    private float _xPosition;
    private float _zPosition;

    public float[] positions;
    public string[] splitter;
    public float[] loadedPositions;

    public string path = "Assets/Game Systems/Resources/Save/TextSaveFile.txt";

    public GameObject cube;
    public GameObject centerCube;

    public Text text;
    
    void Start()
    {
        text.text = "";
    }

    void Update()
    {
        _xPosition = cube.transform.position.x;
        _zPosition = cube.transform.position.z;
        cube.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        positions[0] = _xPosition;
        positions[1] = _zPosition;
    }

    public void Write()
    {
        StreamWriter writer = new StreamWriter(path, false);

        for (int i = 0; i < positions.Length; i++)
        {
            if (i < positions.Length - 1)
            {
                writer.Write(positions[i] + "|");
            }
            else
            {
                writer.Write(positions[i]);
            }
        }
        writer.Close();

        AssetDatabase.ImportAsset(path);

        text.text = "Saved\ncube position!";
    }

    public void Read()
    {
        StreamReader reader = new StreamReader(path);

        string tempRead = reader.ReadLine();

        splitter = tempRead.Split("|");

        loadedPositions = new float[splitter.Length];

        for (int i = 0; i < loadedPositions.Length; i++)
        {
            loadedPositions[i] = float.Parse(splitter[i]);
        }

        cube.transform.position = new Vector3(loadedPositions[0], centerCube.transform.position.y, loadedPositions[1]);

        reader.Close();

        text.text = "Loaded\ncube position!";   
    }
}
