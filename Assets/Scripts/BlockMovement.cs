﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public float Speed { get; set; } = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
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
            MoveBottom();
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

            // マップに書き込み
            FindObjectOfType<BlockMap>()?.Write(transform);

            // 次のブロックを生成する
            FindObjectOfType<BlockSpawner>()?.SpawnBlock();
        }
    }

    private bool IsHitWall(Transform transform)
    {
        var childCount = 0;

        foreach (Transform child in transform)
        {
            Vector2 block1Pos = child.position;

            childCount += 1;

            // Debug.Log($"C:{childCount}, POS:{block1Pos}, D:{DateTime.Now}");

            if (IsHitBorder(block1Pos))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsHitBorder(Vector2 pos)
    {
        return ((int)pos.x < 0 || (int)pos.x > 11 || (int)pos.y <= 0);
    }

    private void MoveLeft()
    {
        transform.position += new Vector3(-1, 0, 0);

        if (IsHitWall(transform))
        {
            transform.position += new Vector3(+1, 0, 0);
        }
    }

    private void MoveRight()
    {
        transform.position += new Vector3(+1, 0, 0);

        if (IsHitWall(transform))
        {
            transform.position += new Vector3(-1, 0, 0);
        }
    }

    private bool MoveBottom()
    {
        var isHit = false;
        transform.position += new Vector3(0, -1, 0);

        isHit = IsHitWall(transform);

        if (isHit)
        {
            transform.position += new Vector3(0, +1, 0);
        }

        return isHit;
    }

    private void Rotate()
    {
        transform.Rotate(0, 0, 90);

        if (IsHitWall(transform))
        {
            transform.Rotate(0, 0, -90);
        }
    }
}