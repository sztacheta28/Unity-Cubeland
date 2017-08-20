using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static List<Vector3> grassBoxesPos = new List<Vector3>();
    public static List<int> emptyGrassBoxes = new List<int>();
    public static List<int> magazinesGenerator = new List<int>();
    public static List<Vector3> sandBoxesPos = new List<Vector3>();
    public static List<Vector3> waterBoxesPos = new List<Vector3>();
    public static List<Vector3> treesPos = new List<Vector3>();
    public static List<int> treesTypes = new List<int>();
    public static List<Vector3> enemiesPos = new List<Vector3>();
    public static List<Transform> enemiesTransforms = new List<Transform>();
    public static List<int> enemiesTypes = new List<int>();
    public static Vector3 playerPosition = new Vector3();
    public static int playerLife = 100;
    public static int scores = 0;
    public static int bullets = 50;

    public string fileName = "save.txt";

    public GameObject player;

    void Awake()
    {
        //clear static members in every scene load
        SaveManager.grassBoxesPos = new List<Vector3>();
        SaveManager.emptyGrassBoxes = new List<int>();
        SaveManager.magazinesGenerator = new List<int>();
        SaveManager.sandBoxesPos = new List<Vector3>();
        SaveManager.waterBoxesPos = new List<Vector3>();
        SaveManager.treesPos = new List<Vector3>();
        SaveManager.treesTypes = new List<int>();
        SaveManager.enemiesPos = new List<Vector3>();
        SaveManager.enemiesTransforms = new List<Transform>();
        SaveManager.enemiesTypes = new List<int>();
    }


    //save all to file
    public void Save()
    {
        File.WriteAllText(fileName, "");

        StreamWriter sw = new StreamWriter(fileName);

        //line 1
        for (int i = 0; i < grassBoxesPos.Count; ++i)
        {
            sw.Write("{0},{1},{2};", grassBoxesPos[i].x, grassBoxesPos[i].y, grassBoxesPos[i].z);
        }
        sw.WriteLine("");
        //line 2
        for (int i = 0; i < waterBoxesPos.Count; ++i)
        {
            sw.Write("{0},{1},{2};", waterBoxesPos[i].x, waterBoxesPos[i].y, waterBoxesPos[i].z);
        }
        sw.WriteLine("");
        //line 3
        for (int i = 0; i < sandBoxesPos.Count; ++i)
        {
            sw.Write("{0},{1},{2};", sandBoxesPos[i].x, sandBoxesPos[i].y, sandBoxesPos[i].z);
        }
        sw.WriteLine("");
        //line 4
        for (int i = 0; i < treesPos.Count; ++i)
        {
            sw.Write("{0},{1},{2};", treesPos[i].x, treesPos[i].y, treesPos[i].z);
        }
        sw.WriteLine("");
        //line 5
        for (int i = 0; i < treesTypes.Count; ++i)
        {
            sw.Write("{0};", treesTypes[i]);
        }
        sw.WriteLine("");
        //line 6
        for (int i = 0; i < enemiesTransforms.Count; ++i)
        {
            if (enemiesTransforms[i] != null)
            {
                sw.Write("{0},{1},{2};", enemiesTransforms[i].position.x, enemiesTransforms[i].position.y, enemiesTransforms[i].position.z);
            }
        }
        sw.WriteLine("");
        //line 7

        print("TYPES : " + enemiesTypes.Count + " ENEMIES_POS : " + enemiesTransforms.Count);

        for (int i = 0; i < enemiesTypes.Count; ++i)
        {
            if (enemiesTransforms[i] != null)
            {
                sw.Write("{0};", enemiesTypes[i]);
            }
        }
        sw.WriteLine("");
        //line 8
        for (int i = 0; i < emptyGrassBoxes.Count; ++i)
        {
            sw.Write("{0};", emptyGrassBoxes[i]);
        }
        sw.WriteLine("");
        //line 9
        for (int i = 0; i < magazinesGenerator.Count; ++i)
        {
            sw.Write("{0};", magazinesGenerator[i]);
        }
        sw.WriteLine("");
        //line 10
        sw.WriteLine(Player.Life);
        //line 11
        sw.WriteLine(ScoresManager.currentPoints);
        //line 12
        sw.WriteLine("{0},{1},{2}", player.transform.position.x, player.transform.position.y, player.transform.position.z);
        //line 13
        sw.Write(Shooting.bullets);
        sw.Close();

        PlayerPrefs.SetInt("savedSomething", 1);
    }

    //load all from file
    public void Load()
    {
        StreamReader sr = new StreamReader(fileName);

        int counter = 0; //counter <=> line number in file

        while (sr.Peek() >= 0 && counter < 13)
        {
            ++counter;

            string line = sr.ReadLine();

            if (counter == 1)
            {
                string[] vectorsStr = line.Split(';');

                print("GRASS "+(vectorsStr.Length - 1));

                for (int i = 0; i < vectorsStr.Length-1; ++i)
                {
                    string[] vectorStr = vectorsStr[i].Split(',');
                    float x = float.Parse(vectorStr[0]);
                    float y = float.Parse(vectorStr[1]);
                    float z = float.Parse(vectorStr[2]);

                    SaveManager.grassBoxesPos.Add(new Vector3(x, y, z));
                }
            }
            else if (counter == 2)
            {
                string[] vectorsStr = line.Split(';');

                print("WATER " + (vectorsStr.Length - 1));

                for (int i = 0; i < vectorsStr.Length-1; ++i)
                {
                    string[] vectorStr = vectorsStr[i].Split(',');                    
                    float x = float.Parse(vectorStr[0]);
                    float y = float.Parse(vectorStr[1]);
                    float z = float.Parse(vectorStr[2]);

                    SaveManager.waterBoxesPos.Add(new Vector3(x, y, z));
                }
            }
            else if (counter == 3)
            {
                string[] vectorsStr = line.Split(';');

                print("SAND " + (vectorsStr.Length - 1));

                for (int i = 0; i < vectorsStr.Length-1; ++i)
                {
                    string[] vectorStr = vectorsStr[i].Split(',');
                    float x = float.Parse(vectorStr[0]);
                    float y = float.Parse(vectorStr[1]);
                    float z = float.Parse(vectorStr[2]);

                    SaveManager.sandBoxesPos.Add(new Vector3(x, y, z));
                }
            }
            else if (counter == 4)
            {
                string[] vectorsStr = line.Split(';');

                print("TRESS_POS " + (vectorsStr.Length - 1));

                for (int i = 0; i < vectorsStr.Length-1; ++i)
                {
                    string[] vectorStr = vectorsStr[i].Split(',');
                    float x = float.Parse(vectorStr[0]);
                    float y = float.Parse(vectorStr[1]);
                    float z = float.Parse(vectorStr[2]);

                    SaveManager.treesPos.Add(new Vector3(x, y, z));
                }
            }
            else if (counter == 5)
            {
                string[] vectorsStr = line.Split(';');

                print("TRESS_TYPES " + (vectorsStr.Length - 1));

                for (int i = 0; i < vectorsStr.Length-1; ++i)
                {
                    int type = int.Parse(vectorsStr[i]);
                    SaveManager.treesTypes.Add(type);
                }
            }
            else if (counter == 6)
            {
                string[] vectorsStr = line.Split(';');

                print("ENEMIES_POS " + (vectorsStr.Length - 1));

                for (int i = 0; i < vectorsStr.Length-1; ++i)
                {
                    string[] vectorStr = vectorsStr[i].Split(',');
                    float x = float.Parse(vectorStr[0]);
                    float y = float.Parse(vectorStr[1]);
                    float z = float.Parse(vectorStr[2]);

                    SaveManager.enemiesPos.Add(new Vector3(x, y, z));
                }
            }
            else if (counter == 7)
            {
                string[] vectorsStr = line.Split(';');

                print("ENEMIES_TYPES " + (vectorsStr.Length - 1));

                for (int i = 0; i < vectorsStr.Length-1; ++i)
                {
                    int type = int.Parse(vectorsStr[i]);
                    SaveManager.enemiesTypes.Add(type);
                }
            }
            else if (counter == 8)
            {
                string[] vectorsStr = line.Split(';');

                print("EMPTY_GRASS_BOXES " + (vectorsStr.Length - 1));

                for (int i = 0; i < vectorsStr.Length-1; ++i)
                {
                    int type = int.Parse(vectorsStr[i]);
                    SaveManager.emptyGrassBoxes.Add(type);
                }
            }
            else if (counter == 9)
            {
                string[] vectorsStr = line.Split(';');

                print("MAGAZINES_GENERATORS " + (vectorsStr.Length - 1));

                for (int i = 0; i < vectorsStr.Length-1; ++i)
                {
                    int type = int.Parse(vectorsStr[i]);
                    SaveManager.magazinesGenerator.Add(type);
                }
            }
            else if (counter == 10)
            {
                SaveManager.playerLife = int.Parse(line);
            }
            else if (counter == 11)
            {
                SaveManager.scores = int.Parse(line);
            }
            else if (counter == 12)
            {
                string[] pos = line.Split(',');
                float x = float.Parse(pos[0]);
                float y = float.Parse(pos[1]);
                float z = float.Parse(pos[2]);

                SaveManager.playerPosition = new Vector3(x, y, z);
            }
            else if (counter == 13)
            {
                SaveManager.bullets = int.Parse(line);
            }
        }
    }
}
