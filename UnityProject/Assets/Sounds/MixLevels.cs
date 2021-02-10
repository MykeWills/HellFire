using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour
{
    public AudioMixer MainSound;


    public void SetSfxLvl(float sfxLvl)
    {
        MainSound.SetFloat("MasterSound", sfxLvl);
    }
    public void SetMusicLvl(float musicLvl)
    {
        MainSound.SetFloat("MasterMusic", musicLvl);
    }
    public void SetMasterLvl(float masterLvl)
    {
        MainSound.SetFloat("MasterVolume", masterLvl);
    }

}