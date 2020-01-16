using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockMovement : MonoBehaviour
{
    private Score _Score = null;

    // Start is called before the first frame update
    void Start()
    {
        _Score = GameObject.Find("ScorePointText").GetComponent<Score>();

        // 生成が発生したタイミングでブロックの衝突が発生したときゲームオーバー
        if (IsHit(transform))
        {
            Invoke(nameof(GotoGameoverScene), 1.0F);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown("d"))
        {
            MoveRight();
        }

        if (Input.GetKeyDown("s"))
        {
            if (!MoveBottom())
            {
                // 下への動作を確定できれば 10 点
                _Score.Add(10);
            }
        }

        if (Input.GetKeyDown("w"))
        {
            Rotate();
        }
    }

    void FixedUpdate()
    {
        if (MoveBottom())
        {
            enabled = false;


            var map = FindObjectOfType<BlockMap>();

            if (map != null)
            {
                // マップに書き込み
                map.Write(transform);

                // マップのクリア
                var clearableLines = map.GetClearableLines();

                if (clearableLines != null)
                {
                    var clearCount = map.ClearLines(clearableLines);

                    _Score.Add(clearCount * 1000);

                    AudioManager.Instance.PlayOneShot(SoundType.Clear);
                }
                else
                {
                    // ブロックの固定のみ
                    AudioManager.Instance.PlayOneShot(SoundType.Stop);
                }
            }

            // 次のブロックを生成する
            FindObjectOfType<BlockSpawner>()?.SpawnBlock();
        }

        // ブロックが強制移動するたびに 10 点
        _Score.Add(10);
    }

    private bool IsHit(Transform transform)
    {
        // 壁にあたるとき
        if (IsHitWall(transform)) return true;

        var map = FindObjectOfType<BlockMap>();

        foreach (Transform child in transform)
        {
            Vector2 block1Pos = child.position;

            if (map.Exists((int)Math.Round(block1Pos.x), (int)Math.Round(block1Pos.y)))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsHitWall(Transform transform)
    {
        var childCount = 0;

        foreach (Transform child in transform)
        {
            Vector2 block1Pos = child.position;

            childCount += 1;

            if (IsHitBorder(block1Pos))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsHitBorder(Vector2 pos)
    {
        return ((int)Math.Round(pos.x) < 0 || (int)Math.Round(pos.x) > 11 || (int)Math.Round(pos.y) < 0);
    }

    private void MoveLeft()
    {
        transform.position += new Vector3(-1, 0, 0);

        if (IsHit(transform))
        {
            transform.position += new Vector3(+1, 0, 0);
        }
    }

    private void MoveRight()
    {
        transform.position += new Vector3(+1, 0, 0);

        if (IsHit(transform))
        {
            transform.position += new Vector3(-1, 0, 0);
        }
    }

    private bool MoveBottom()
    {
        var isHit = false;
        transform.position += new Vector3(0, -1, 0);

        isHit = IsHit(transform);

        if (isHit)
        {
            transform.position += new Vector3(0, +1, 0);
        }

        return isHit;
    }

    private void Rotate()
    {
        transform.Rotate(0, 0, 90);

        if (IsHit(transform))
        {
            transform.Rotate(0, 0, -90);
        }
        else
        {
            // 回転できるときだけ、効果音を鳴らす
            AudioManager.Instance.PlayOneShot(SoundType.Rotate);
        }
    }

    private void GotoGameoverScene()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("GameoverScene");
    }
}
