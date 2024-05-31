using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundEffects/Sfx Object")]
public class SfxSO : ScriptableObject
{
    public string SfxName;
    public AudioClip SfxFile;
}
