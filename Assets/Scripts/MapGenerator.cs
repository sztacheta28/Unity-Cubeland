using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class MapGenerator : MonoBehaviour
{
    public float rateTime = 1f;
    private float nextTime = 0f;

    public float scaleModifier = 1f;
    public float scale = 1f;

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

    public AudioSource gongSound;

	public static List<Transform> emptyGrassBoxes = new List<Transform>(); //posible places to instantiate enemies
    public static List<Transform> trees = new List<Transform>();

    void Start()
    {
        //reset static members
		MapGenerator.emptyGrassBoxes = new List<Transform>();
        MapGenerator.trees = new List<Transform>();

        //random parameters of map
        scaleModifier = Random.Range(10f, 20f);
        scale = Random.Range(1.5f, 4f) * scaleModifier;

        CreateClouds();
        CreateTerrain(0); //create first slice of terrain
    }

    int i = 1;
    void Update()
    {
        //CREATE TERRAIN STEP BY STEP
        if (i <= 50 && nextTime < Time.time)
        {
            CreateTerrain(i);
            CreateTerrain(-i);
            nextTime = Time.time + rateTime;
			if(i == 50){
				navMeshSurface.BuildNavMesh ();
                ScoresManager.waitingOffset = (int)Time.timeSinceLevelLoad;
                Gameplay.startedAttack = true;
                gongSound.Play();
            }
            i++;
        }
    }

    //create i-th step of terrain
    void CreateTerrain(int i)
    {
        for (int j = 0; j < 100; ++j)
        {
            float height = Mathf.Floor(Mathf.PerlinNoise((i + 50) / scale, j / scale) * scaleModifier);

            if (Random.Range(0, 600) == 0)
            {
                //ENEMIES
				enemiesManager.InstantiateEnemyAtPosition(new Vector3(i, height+0.75f, j));
            }


            if (height <= 4)
            {
                //SAND
                GameObject cubeSand = Instantiate(sandPrefab);
                cubeSand.transform.position = new Vector3(i, height, j);                
                cubeSand.transform.parent = transform;
                SaveManager.sandBoxesPos.Add(new Vector3(i, height, j));

                if (height < 4)
                {
                    for (float l = height + 1; l < 5; ++l)
                    {
                        //WATER
                        GameObject cubeWater = Instantiate(waterPrefab);
                        cubeWater.transform.position = new Vector3(i, l, j);
                        cubeWater.transform.parent = transform;
                        SaveManager.waterBoxesPos.Add(new Vector3(i, l, j));
                    }
                }
            }
            else
            {
                //GRASS
                GameObject cubeGrass = Instantiate(grassPrefab);
                cubeGrass.transform.position = new Vector3(i, height, j);
                cubeGrass.transform.parent = transform;
                SaveManager.grassBoxesPos.Add(new Vector3(i, height, j));

                //TREE
                if (Random.Range (0, 200) == 0) {
                    SaveManager.emptyGrassBoxes.Add(1);
                    SaveManager.magazinesGenerator.Add(0);
                    //NO SPAWN PLAYER AREA
                    if (!(i>-5 && i <5 && j>45 && j<55)) {
                        //DON'T SPAWN 2 TREES TOO CLOSE
                        bool closeToOtherTree = false;
                        foreach (Transform otherTree in trees)
                        {
                            if (Vector3.Distance(otherTree.position, new Vector3(i, height + 1, j)) < 7)
                            {
                                closeToOtherTree = true;
                                break;
                            }
                        }

                        if (!closeToOtherTree)
                        {
                            GameObject tree = null;
                            int treeCorrectionHeight = 0;
                            int rndPrefab = Random.Range(0, 4);

                            if (rndPrefab == 0)
                            {
                                SaveManager.treesTypes.Add(0);
                                tree = Instantiate(tree1Prefab);
                                treeCorrectionHeight = 5;
                            }
                            else if (rndPrefab == 1)
                            {
                                SaveManager.treesTypes.Add(1);
                                tree = Instantiate(tree2Prefab);
                                treeCorrectionHeight = 1;
                            }
                            else if (rndPrefab == 2)
                            {
                                SaveManager.treesTypes.Add(2);
                                tree = Instantiate(tree3Prefab);
                                treeCorrectionHeight = 1;
                            }
                            else if (rndPrefab == 3)
                            {
                                SaveManager.treesTypes.Add(3);
                                tree = Instantiate(tree1Prefab);
                                treeCorrectionHeight = 6;
                            }

                            tree.transform.position = new Vector3(i, height + treeCorrectionHeight, j);
                            tree.transform.parent = transform;

                            int rndRot = Random.Range(0, 4);
                            tree.transform.rotation = Quaternion.Euler(0, rndRot * 90, 0);
                            SaveManager.treesPos.Add(tree.transform.position);
                            trees.Add(tree.transform);
                        }
                    }
				}
                //MAGAZINE SPAWN
                else if (Random.Range (0, 10) == 0) {
					cubeGrass.AddComponent<MagazinesGenerator> ();
                    SaveManager.emptyGrassBoxes.Add(0);
                    SaveManager.magazinesGenerator.Add(1);
                } else {
					emptyGrassBoxes.Add (cubeGrass.transform);
                    SaveManager.emptyGrassBoxes.Add(1);
                    SaveManager.magazinesGenerator.Add(0);
                }
            }
        }
    }

    void CreateClouds()
    {
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
