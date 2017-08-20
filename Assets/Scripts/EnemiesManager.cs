using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    private static int count = 0;

    public GameObject duckPrefab; //enemy 0
    public GameObject sheepPrefab; //enemy 1
    public GameObject pigPrefab; //enemy 2

    public float rateTime = 3f;
    private float nextTime = 20f;

    public static int Count
    {
        get
        {
            return EnemiesManager.count;
        }
    }

    void Update()
    {
        //add new enemy
        if (Time.time > nextTime)
        {
            nextTime = Time.time + rateTime;
            rateTime *= 0.99f; //instantiate more often
            InstantiateEnemyInRandomPosition();
        }
    }

    //enemy counter up
    public static void Add()
    {
        ++EnemiesManager.count;
    }

    //enemy counter down
    public static void Delete()
    {
        --EnemiesManager.count;
    }

    //enemy counter reset
    public static void RestartCount()
    {
        EnemiesManager.count = 0;
    }


    public void InstantiateEnemyInRandomPosition()
    {
        //Random empty position
		int rndGrassCubeIdx = Random.Range (0, MapGenerator.emptyGrassBoxes.Count);
		Vector3 rndGrassCubePosition = MapGenerator.emptyGrassBoxes[rndGrassCubeIdx].position;

        int rndPrefab = Random.Range(0, 3); //random enemy

        if (rndPrefab == 0)
        {
            SaveManager.enemiesTransforms.Add(Instantiate<GameObject>(sheepPrefab, new Vector3(rndGrassCubePosition.x, rndGrassCubePosition.y + 1, rndGrassCubePosition.z), Quaternion.identity).transform);            
        }
        else if (rndPrefab == 1)
        {
            SaveManager.enemiesTransforms.Add(Instantiate<GameObject>(pigPrefab, new Vector3(rndGrassCubePosition.x, rndGrassCubePosition.y + 1, rndGrassCubePosition.z), Quaternion.identity).transform);
        }
        else if (rndPrefab == 2)
        {
            SaveManager.enemiesTransforms.Add(Instantiate<GameObject>(duckPrefab, new Vector3(rndGrassCubePosition.x, rndGrassCubePosition.y + 1, rndGrassCubePosition.z), Quaternion.identity).transform);
        }

        SaveManager.enemiesTypes.Add(rndPrefab);
    }

    /// <summary>
    /// Instantiate enemy at given position
    /// </summary>
    /// <param name="pos">position of enemy</param>
    /// <param name="isRandom">is random type of enemy</param>
    /// <param name="type">if dont random instantiate this type of enemy</param>
    public void InstantiateEnemyAtPosition(Vector3 pos, bool isRandom = true, int type = 0)
    {
        int rndPrefab = Random.Range(0, 3);

        if (!isRandom)
        {
            rndPrefab = type;
        }

        if (rndPrefab == 0) //duck
        {
            SaveManager.enemiesTransforms.Add(Instantiate<GameObject>(sheepPrefab, pos, Quaternion.identity).transform);
        }
        else if (rndPrefab == 1) //sheep
        {
            SaveManager.enemiesTransforms.Add(Instantiate<GameObject>(pigPrefab, pos, Quaternion.identity).transform);
        }
        else if (rndPrefab == 2) //pig
        {
            SaveManager.enemiesTransforms.Add(Instantiate<GameObject>(duckPrefab, pos, Quaternion.identity).transform);
        }

        if (isRandom)
        {
            SaveManager.enemiesTypes.Add(rndPrefab);
        }
    }
}