using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    [SerializeField] private int Scale = 10;
    [SerializeField] private GameObject ChestPrefab;
    [SerializeField] private GameObject ElevatorPrefab;
    [SerializeField] private GameObject EnemyPrefab;

    private Board _board;
    private bool _isElevatorSpawned = false;

    private void Awake()
    {
        _board = GetComponent<Board>();

    }
    // Start is called before the first frame update
    void Start()
    {
        _board.Setup_LevelGen(SetupBox);
            _board.RechargeBoxes();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupBox(Box box)
    {
        Debug.Log("Setup box");
        if (box.IsDangerous)
        {
            Instantiate(ChestPrefab, new Vector3(box.RowIndex * Scale, 0, box.ColumnIndex * Scale), Quaternion.identity);
        }
        else if (box.DangerNearby > 0)
        {
            Instantiate(EnemyPrefab, new Vector3(box.RowIndex * Scale, 0, box.ColumnIndex * Scale), Quaternion.identity);

        }
        else if (box.DangerNearby == 0 && !_isElevatorSpawned)
        {
            Instantiate(ElevatorPrefab, new Vector3(box.RowIndex * Scale, 0, box.ColumnIndex * Scale), Quaternion.identity);
            _isElevatorSpawned = true;
        }
    }

}
