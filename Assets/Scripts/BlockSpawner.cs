using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <see cref="BlockSpawner"/> クラスは、落下するブロックを生成するクラスです。
/// </summary>
public class BlockSpawner : MonoBehaviour
{
    /// <summary>
    /// 現在、落下＆操作中のブロックを取得します。
    /// </summary>
    public GameObject SelectedBlock { get; private set; }

    public void SpawnBlock()
    {
        var prophet = GameObject.Find("BlockProphet").GetComponent<BlockProphet>();
        var Spawner = GameObject.Find("BlockSpawner");

        // 預言クラスから次のブロックを取得する
        var nextBlock = prophet.NextBlock;

        if (nextBlock != null)
        {
            SelectedBlock = nextBlock;
            SelectedBlock.AddComponent<BlockMovement>();
        }

        SelectedBlock.transform.position = Spawner.transform.position;

        // 次のブロックを予言する
        prophet.Predict();
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
