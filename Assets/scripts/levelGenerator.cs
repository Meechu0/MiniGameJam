using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelGenerator : MonoBehaviour
{
    private const float spawnDistance = 200f;
    [SerializeField] private Transform levelPart_start;
    [SerializeField] private List<Transform> levelpartList;
    [SerializeField] private GameObject Player;
    public float distance;
    public Score scoreTracker;

    public GameObject gameOverPanel;

    private Vector3 lastEndPosition;
    public float yDistance;

    private void Awake()
    {
        lastEndPosition = levelPart_start.Find("EndPosition").position;
        int startingSpawnLevelParts = 5;
        for(int i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnRoom();
        }
    }
    private void Update()
    {
        if(Vector3.Distance(Player.transform.position,lastEndPosition) < spawnDistance)
        {
            //spawn another level part;
            SpawnRoom();
        }
        distance =(Vector3.Distance(Player.transform.position, lastEndPosition));
        yDistance = Player.transform.position.y - lastEndPosition.y;
        if(yDistance < -250)
        {
            scoreTracker.GameOver();
        }
    }

    private void SpawnRoom()
    {
        Transform chosenRoomPrefab = levelpartList[Random.Range(0, levelpartList.Count)];
        Transform lastRoomLocation = SpawnRoom(chosenRoomPrefab,lastEndPosition);
        lastEndPosition = lastRoomLocation.Find("EndPosition").position;
    }

    private Transform SpawnRoom(Transform RoomPrefab, Vector3 spawnPosition)
    {
        Transform roomPrefabTransform = Instantiate(RoomPrefab, spawnPosition, Quaternion.identity);
        return roomPrefabTransform;
    }


        
}
