using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <see cref="BlockProphet"/> クラスは、次のブロックを予言するクラスです。
/// </summary>
public class BlockProphet : MonoBehaviour
{
    private const int BLOCK_MAX_TYPE = 7;
    public GameObject[] _Blocks;

    [HideInInspector]
    public GameObject _NextBlock = null;

    /// <summary>
    /// 予言したブロックを取得します。
    /// </summary>
    public GameObject NextBlock
    {
        get
        {
            return _NextBlock != null ? _NextBlock : Predict();
        }
    }

    private void Start()
    {
        Predict();
    }

    /// <summary>
    /// 次に使うブロックを予言します。
    /// </summary>
    /// <returns></returns>
    public GameObject Predict()
    {
        int randomIndex = Random.Range(0, BLOCK_MAX_TYPE - 1);

        _NextBlock = Instantiate(_Blocks[randomIndex], transform.position, Quaternion.identity);

        return _NextBlock;
    }
}
