using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <see cref="SoundType"/> 列挙型は、効果音の種別を定義します。
/// </summary>
public enum SoundType : int
{
    /// <summary>
    /// 回転の効果音。
    /// </summary>
    Rotate = 1,
    /// <summary>
    /// ブロックが停止したときの効果音。
    /// </summary>
    Stop = 2,
    /// <summary>
    /// ブロックの行をクリアしたときの効果音。
    /// </summary>
    Clear = 3,
}
