using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMap : MonoBehaviour
{
    private const int MAX_X = 12;
    private const int MAX_Y = 18;

    private bool[,] Map = new bool[12, 18];

    public void Write(Transform transform)
    {
        foreach(Transform block in transform)
        {
            Vector2 pos = block.position;

            // 配列は 0, 0 を始点とする
            Set((int)pos.x, (int)pos.y);
            Debug.Log($"x:({pos.x}), y:({pos.y})");
        }
    }

    /// <summary>
    /// 指定したマップの座標を有効にします。
    /// </summary>
    /// <param name="x">x 座標の位置。</param>
    /// <param name="y">y 座標の位置。</param>
    public void Set(int x, int y)
    {
        Map[x, y] = true;
    }

    /// <summary>
    /// 指定したマップの座標を無効にします。
    /// </summary>
    /// <param name="x">x 座標の位置。</param>
    /// <param name="y">y 座標の位置。</param>
    public void UnSet(int x, int y)
    {
        Map[x, y] = false;
    }

    /// <summary>
    /// 指定したマップの座標が有効であるかどうかを示す値を取得します。
    /// </summary>
    /// <param name="x">x 座標の位置。</param>
    /// <param name="y">y 座標の位置。</param>
    /// <returns>有効のとき true。それ以外のとき false。</returns>
    public bool Exists(int x, int y)
    {
        return (Map[x, y] == true);
    }

    /// <summary>
    /// 指定したマップの座標が有効であるかどうかを示す値を取得します。
    /// </summary>
    /// <param name="points">x 座標の位置と y 座標の位置を表す組の値。</param>
    /// <returns>有効のとき true。それ以外のとき false。</returns>
    public bool Exists(IEnumerable<Tuple<int, int>> points)
    {
        foreach (var point in points)
        {
            if (Exists(point.Item1, point.Item2))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// マップの状態を初期化し、すべての座標を無効にします。
    /// </summary>
    public void Clear()
    {
        for (int y = 0; y < MAX_Y; y++)
        {
            for (int x = 0; x < MAX_X; x++)
            {
                Map[x, y] = false;
            }
        }
    }
}
