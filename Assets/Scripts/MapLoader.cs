using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class MapLoader : MonoBehaviour
{
    //boxes
    public GameObject grassPrefab;
    public GameObject waterPrefab;
    public GameObject sandPrefab;

    //clouds
    public GameObject cloud1Prefab;
    public GameObject cloud2Prefab;
    public GameObject cloud3Prefab;
    public GameObject cloud4Prefab;
    public GameObject cloud5Prefab;
    public GameObject cloud6Prefab;

    //trees
    public GameObject tree1Prefab;
    public GameObject tree2Prefab;
    public GameObject tree3Prefab;

    public EnemiesManager enemiesManager;
    public NavMeshSurface navMeshSurface; //to generate posible paths

    public static List<Transform> emptyGrassBoxes = new List<Transform>(); //posible places to instantiate enemies
    public static List<Transform> trees = new List<Transform>();

    public GameObject player; //to set player position

    public SaveManager saveManager;

    void Start()
    {   //reset static members 
        MapGenerator.emptyGrassBoxes = new List<Transform>();
        MapGenerator.trees = new List<Transform>();

        Time.timeScale = 0.0f; //dont start before end of generating terrain
        saveManager.Load(); //load saved terrain
}

    int i = 0;
    void Update()
    {
        if(i == 0)
        {
            CreateGrassBoxes();
        }
        else if(i == 1)
        {
            CreateSandBoxes();
        }
        else if (i == 2)
        {
            CreateWaterBoxes();
        }
        else if (i == 3)
        {
            CreateTrees();
        }
        else if (i == 4)
        {
            CreateClouds();
        }
        else if (i == 5)
        {
            LoadEnemies();
        }
        else if (i == 6)
        {
            LoadPlayer();
        }
        else if (i == 7)
        {
            LoadGameplaySettings();
        }
        else if (i == 8)
        {
            StartGame();
        }

        ++i;
    }

    void CreateGrassBoxes()
    {
        print("CreateGrassBoxes");
        for (int i = 0; i<SaveManager.grassBoxesPos.Count; ++i)
        {
            GameObject cubeGrass = Instantiate(grassPrefab);
            cubeGrass.transform.position = SaveManager.grassBoxesPos[i];
            cubeGrass.transform.parent = transform;

            if(SaveManager.emptyGrassBoxes[i] == 1)
            {
                MapGenerator.emptyGrassBoxes.Add(cubeGrass.transform);
            }

            if (SaveManager.magazinesGenerator[i] == 1)
            {
                cubeGrass.AddComponent<MagazinesGenerator>();
            }
        }
    }

    void CreateSandBoxes()
    {
        print("CreateSandBoxes");
        for (int i = 0; i < SaveManager.sandBoxesPos.Count; ++i)
        {
            GameObject cubeSand = Instantiate(sandPrefab);
            cubeSand.transform.position = SaveManager.sandBoxesPos[i];
            cubeSand.transform.parent = transform;
        }
    }

    void CreateWaterBoxes()
    {
        print("CreateWaterBoxes");
        for (int i = 0; i < SaveManager.waterBoxesPos.Count; ++i)
        {
            GameObject cubeWater = Instantiate(waterPrefab);
            cubeWater.transform.position = SaveManager.waterBoxesPos[i];
            cubeWater.transform.parent = transform;
        }
    }

    void CreateTrees()
    {
        print("CreateTrees");
        for (int i = 0; i < SaveManager.treesPos.Count; ++i)
        {
            GameObject tree = null;
            int prefabType = SaveManager.treesTypes[i];


            if (prefabType == 0)
            {
                tree = Instantiate(tree1Prefab);
            }
            else if (prefabType == 1)
            {
                tree = Instantiate(tree2Prefab);
            }
            else if (prefabType == 2)
            {
                tree = Instantiate(tree3Prefab);
            }
            else if (prefabType == 3)
            {
                tree = Instantiate(tree1Prefab);
            }
            else
            {
                tree = Instantiate(tree1Prefab);
            }

            tree.transform.position = SaveManager.treesPos[i];
            tree.transform.parent = transform;

            int rndRot = Random.Range(0, 4);
            tree.transform.rotation = Quaternion.Euler(0, rndRot * 90, 0);
            MapGenerator.trees.Add(tree.transform);
        }
    }

    void LoadEnemies()
    {
        print("LoadEnemies");
        for (int i = 0; i < SaveManager.enemiesPos.Count; ++i)
        {
            enemiesManager.InstantiateEnemyAtPosition(SaveManager.enemiesPos[i], false, SaveManager.enemiesTypes[i]);
        }
    }

    void LoadPlayer()
    {
        print("LoadPlayer");
        player.SetActive(true);
        player.transform.SetPositionAndRotation(SaveManager.playerPosition, Quaternion.identity);
        
    }

    void LoadGameplaySettings()
    {
        print("LoadGameplaySettings");
        Player.Life = SaveManager.playerLife;
        ScoresManager.lastSaveOffset = SaveManager.scores;
        Shooting.bullets = SaveManager.bullets;
    }

    void StartGame()
    {
        print("StartGame");        
        navMeshSurface.BuildNavMesh();
        ScoresManager.waitingOffset = (int)Time.timeSinceLevelLoad;
        Gameplay.startedAttack = true;

        Time.timeScale = 1.0f;
    }

    void CreateClouds()
    {
        print("CreateClouds");
        for (int i = -10; i < 10; ++i)
        {
            for (int j = -10; j < 10; ++j)
            {
                //CLOUD
                GameObject cloud = null;
                int rndPrefab = Random.Range(0, 6);
                if (rndPrefab == 0)
                {
                    cloud = Instantiate(cloud1Prefab);
                }
                else if (rndPrefab == 1)
                {
                    cloud = Instantiate(cloud2Prefab);
                }
                else if (rndPrefab == 2)
                {
                    cloud = Instantiate(cloud3Prefab);
                }
                else if (rndPrefab == 3)
                {
                    cloud = Instantiate(cloud4Prefab);
                }
                else if (rndPrefab == 4)
                {
                    cloud = Instantiate(cloud5Prefab);
                }
                else if (rndPrefab == 5)
                {
                    cloud = Instantiate(cloud6Prefab);
                }

                int cloudX = Random.Range(i * 15, i * 15 + 15);
                int cloudZ = Random.Range(j * 15, j * 15 + 15);

                cloud.transform.position = new Vector3(cloudX, 40, cloudZ);
                cloud.transform.parent = transform;

                int rndRot = Random.Range(0, 4);
                cloud.transform.rotation = Quaternion.Euler(0, rndRot * 90, 0);
            }
        }
    }
}
