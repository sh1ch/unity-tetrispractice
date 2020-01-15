using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private GameObject _ScorePoint = null;

    /// <summary>
    /// 現在の得点を取得または設定します。
    /// </summary>
    public int Value { get; set; } = 0;

    // Start is called before the first frame update
    void Start()
    {
        _ScorePoint = GameObject.Find("ScorePointText");
    }

    private void Update()
    {
        var textComponent = _ScorePoint.GetComponent<Text>();

        textComponent.text = Value.ToString();
    }

    public void Reset()
    {
        Value = 0;
    }

    public void Add(int point)
    {
        Value += point;
    }
}
