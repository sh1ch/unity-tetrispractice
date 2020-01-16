using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <see cref="AudioManager"/> クラスは、効果音再生をサポートするクラスです。
/// </summary>
public class AudioManager : MonoBehaviour
{
    private static AudioManager _Instance = null;

    public AudioClip _RotateSound;
    public AudioClip _ClearSound;
    public AudioClip _StopSound;

    private AudioSource _SoundEffect;

    /// <summary>
    /// 唯一の <see cref="AudioManager"/> クラスのインスタンスを取得します。
    /// </summary>
    public static AudioManager Instance 
    {
        get { return _Instance; } 
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            _Instance = this;
        }
        else if (_Instance != this)
        {
            Destroy(gameObject);
        }

        var source = GetComponent<AudioSource>();
        _SoundEffect = source;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 指定した効果音を再生します。
    /// </summary>
    /// <param name="type">効果音の種別。</param>
    public void PlayOneShot(SoundType type)
    {
        AudioClip clip = null;

        switch (type)
        {
            case SoundType.Clear:
                clip = _ClearSound;
                break;
            case SoundType.Rotate:
                clip = _RotateSound;
                break;
            case SoundType.Stop:
                clip = _StopSound;
                break;
            default:
                throw new MissingComponentException($"指定したコンポーネントは存在しませんでした。type = {type}");
        }

        /* C# 8.0
        var clip = type switch
        {
            SoundType.Clear => _ClearSound,
            SoundType.Rotate => _RotateSound,
            SoundType.Stop => _StopSound,
            _ => throw new MissingComponentException($"指定したコンポーネントは存在しませんでした。type = {type}");
        };
        */

        PlayOneShot(clip);
    }

    private void PlayOneShot(AudioClip clip)
    {
        _SoundEffect?.PlayOneShot(clip);
    }
}
