using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    private const int BLOCK_MAX_TYPE = 7;
    public GameObject[] _Blocks;

    public void SpawnBlock()
    {
        int randomIndex = Random.Range(0, BLOCK_MAX_TYPE - 1);

        Instantiate(_Blocks[randomIndex], transform.position, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnBlock();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
