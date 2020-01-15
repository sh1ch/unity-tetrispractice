using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMap : MonoBehaviour
{
    private const int MAX_X = 12;
    private const int MAX_Y = 18;

    private Transform[,] _Map = new Transform[MAX_X, MAX_Y];

    /// <summary>
    /// ブロックのマップを表すテキストを取得します。
    /// <para>
    /// 落下中のブロック インスタンスを指定することで、マップに追加することができます。
    /// </para>
    /// </summary>
    /// <param name="fallingBlock">落下中のブロック。</param>
    /// <returns>マップを表すテキスト（0=ブロック無, 1=ブロック有）</returns>
    public string GetText(GameObject fallingBlock)
    {
        var text = "";
        var column = "";

        for (int y = 0; y < MAX_Y; y++)
        {
            for (int x = 0; x < MAX_X; x++)
            {
                bool existsFallingBlock = false;

                // 現在、操作＆落下中のブロックが存在するか
                if (fallingBlock != null)
                {
                    var blocksTransfrom = fallingBlock.transform;
                    var i = 0;

                    foreach (Transform block in blocksTransfrom.transform)
                    {
                        Vector2 pos = block.position;
                        i += 1;

                        int fx = Convert.ToInt32(pos.x);
                        int fy = Convert.ToInt32(pos.y);

                        if (x == fx && y == fy)
                        {
                            existsFallingBlock = true;
                            break;
                        }
                    }
                }

                column += (_Map[x, y] != null) || existsFallingBlock ? "1" : "0";
            }

            text = column + "\r\n" + text;
            column = "";
        }

        return text;
    }

    public void Write(Transform transform)
    {
        foreach(Transform block in transform)
        {
            Set(block);
        }
    }

    /// <summary>
    /// 指定したブロックをマップ座標に設置します。
    /// </summary>
    /// <param name="transform"></param>
    public void Set(Transform transform)
    {
        if (transform == null) throw new ArgumentNullException("指定した Transform の値は null です。");

        Vector2 blockPos = transform.position;

        int x = Convert.ToInt32(transform.position.x);
        int y = Convert.ToInt32(transform.position.y);

        _Map[x, y] = transform;

        // TODO ブロック列のクリアを確認
    }

    /// <summary>
    /// 指定したマップの座標に存在するブロックをクリアします。
    /// </summary>
    /// <param name="x">x 座標の位置。</param>
    /// <param name="y">y 座標の位置。</param>
    public void UnSet(float x, float y)
    {
        int ix = Convert.ToInt32(transform.position.x);
        int iy = Convert.ToInt32(transform.position.y);

        if (Exists(ix, iy))
        {
            Destroy(_Map[ix, iy].gameObject);
            _Map[ix, iy] = null;
        }
    }

    /// <summary>
    /// 指定したマップの座標にブロックが存在するかどうかを示す値を取得します。
    /// </summary>
    /// <param name="x">x 座標の位置。</param>
    /// <param name="y">y 座標の位置。</param>
    /// <returns>有効のとき true。それ以外のとき false。</returns>
    public bool Exists(int x, int y)
    {
        return !(_Map[x, y] == null);
    }

    /// <summary>
    /// 指定したマップの座標にブロックが存在するかどうかを示す値を取得します。
    /// </summary>
    /// <param name="x">x 座標の位置。</param>
    /// <param name="y">y 座標の位置。</param>
    /// <returns>有効のとき true。それ以外のとき false。</returns>
    public bool Exists(float x, float y)
    {
        int ix = Convert.ToInt32(transform.position.x);
        int iy = Convert.ToInt32(transform.position.y);

        return Exists(ix, iy);
    }

    /// <summary>
    /// 指定したブロックの位置に別のブロックが存在するかどうかを示す値を取得します。
    /// </summary>
    /// <param name="x">x 座標の位置。</param>
    /// <param name="y">y 座標の位置。</param>
    /// <returns>有効のとき true。それ以外のとき false。</returns>
    public bool Exists(Transform transform)
    {
        if (transform == null) throw new ArgumentNullException("指定した Transform の値は null です。");

        Vector2 blockPos = transform.position;

        int x = Convert.ToInt32(transform.position.x);
        int y = Convert.ToInt32(transform.position.y);

        return Exists(x, y);
    }

    /// <summary>
    /// マップの状態を初期化し、すべての座標を無効にします。
    /// </summary>
    public void ClearAll()
    {
        for (int y = 0; y < MAX_Y; y++)
        {
            for (int x = 0; x < MAX_X; x++)
            {
                if (Exists(x, y))
                {
                    Destroy(_Map[x, y].gameObject);
                    _Map[x, y] = null;
                }
            }
        }
    }


}
