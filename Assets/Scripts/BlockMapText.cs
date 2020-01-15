﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockMapText : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var textComponent = GameObject.Find("BlockMapText").GetComponent<Text>();

        if (textComponent == null)
        {
            Debug.Log("BlockMapText コンポーネントを取得できませんでした。ブロックのテキスト出力に失敗しました。");
            return; 
        }

        var map = GameObject.Find("BlockMap").GetComponent<BlockMap>();
        var spawner = GameObject.Find("BlockSpawner").GetComponent<BlockSpawner>();

        textComponent.text = map.GetText(spawner.SelectedBlock);
    }
}
