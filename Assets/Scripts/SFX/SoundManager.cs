using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public SfxSO[] SfxList = { };
    public AudioSource SfxSource;

    public static SoundManager instance;

    private void Awake()
    {
        instance = this;
    }
    public void PlaySfx(string SfxName)
    {
        SfxSO sfx = Array.Find(SfxList, x => x.SfxName == SfxName);

        SfxSource.PlayOneShot(sfx.SfxFile);
    }
}
