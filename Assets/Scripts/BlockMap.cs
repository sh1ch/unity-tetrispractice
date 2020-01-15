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
    /// <param name="transform">ブロックの組み合わせを表す Transform。</param>
    public void Set(Transform transform)
    {
        if (transform == null) throw new ArgumentNullException("指定した Transform の値は null です。");

        Vector2 blockPos = transform.position;

        int x = Convert.ToInt32(blockPos.x);
        int y = Convert.ToInt32(blockPos.y);

        _Map[x, y] = transform;
    }

    /// <summary>
    /// 指定した列番号のブロックをクリアし、マップを下詰めします。
    /// </summary>
    /// <param name="y">列番号。</param>
    public void ClearLine(int y)
    {
        for (int x = 0; x < MAX_X; x++)
        {
            UnSet(x, y);
        }

        // クリアした行よりも上に存在するマップデータを下詰めします
        DownLines(y);
    }

    /// <summary>
    /// 指定したコレクションの列のブロックをクリアし、マップを下詰めします。
    /// </summary>
    /// <param name="lines">列番号を表すコレクション。</param>
    public void ClearLines(IEnumerable<int> lines)
    {
        foreach (var y in lines)
        {
            ClearLine(y);
        }
    }

    /// <summary>
    /// 指定したマップの座標に存在するブロックをクリアします。
    /// </summary>
    /// <param name="transform">ブロックの組み合わせを表す Transform。</param>
    public void UnSet(Transform transform)
    {
        Vector2 blockPos = transform.position;

        int x = Convert.ToInt32(blockPos.x);
        int y = Convert.ToInt32(blockPos.y);

        if (Exists(x, y))
        {
            Destroy(_Map[x, y].gameObject);
            _Map[x, y] = null;
        }
    }

    /// <summary>
    /// 指定したマップの座標に存在するブロックをクリアします。
    /// </summary>
    /// <param name="x">x 座標の位置。</param>
    /// <param name="y">y 座標の位置。</param>
    public void UnSet(int x, int y)
    {
        if (Exists(x, y))
        {
            Destroy(_Map[x, y].gameObject);
            _Map[x, y] = null;
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
    /// <param name="transform">ブロックの組み合わせを表す Transform。</param>
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

    /// <summary>
    /// クリア可能な列番号のコレクションを取得します。
    /// </summary>
    /// <returns>クリア可能な列番号のコレクションを返却します。存在しないとき、null を返却。</returns>
    public IEnumerable<int> GetClearableLines()
    {
        var clearableLines = new List<int>();

        for (int y = 0; y < MAX_Y; y++)
        {
            int x;
            for (x = 0; x < MAX_X; x++)
            {
                if (!Exists(x, y))
                {
                    break;
                }
            }

            // 行はすべて埋まっている
            if (x == MAX_X)
            {
                clearableLines.Add(y);
            }
        }

        return clearableLines.Count > 0 ? clearableLines : null;
    }

    /// <summary>
    /// 指定した行番号より上に存在するブロックの列の位置を一段下に落とします。
    /// </summary>
    /// <param name="y"></param>
    private void DownLines(int y)
    {
        for (int sy = y + 1; sy < MAX_Y; sy++)
        {
            for (var x = 0; x < MAX_X; x++)
            {
                if (Exists(x, sy))
                {
                    var block = _Map[x, sy];

                    _Map[x, sy - 1] = block;
                    _Map[x, sy] = null;

                    block.position += new Vector3(0, -1, 0);
                }
            }
        }
    }

}
