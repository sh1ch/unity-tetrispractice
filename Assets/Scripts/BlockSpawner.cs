using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    private const int BLOCK_MAX_TYPE = 7;
    public GameObject[] _Blocks;

    /// <summary>
    /// 現在、落下＆操作中のブロックを取得します。
    /// </summary>
    public GameObject SelectedBlock { get; private set; }

    public void SpawnBlock()
    {
        int randomIndex = Random.Range(0, BLOCK_MAX_TYPE - 1);

        SelectedBlock = Instantiate(_Blocks[randomIndex], transform.position, Quaternion.identity);
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
