using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;


[CreateAssetMenu(menuName = "SoundManager")]
public class SoundManager : SingletonScriptableObject<SoundManager>
{
    #region Serialized Fields

    [SerializeField, Range(0f,1f)]
    public float sfxVolume;
    [SerializeField,Range(0f,1f)]
    public float musicVolume;
    
    public EventReference music;
    
    public  Dictionary<string, Dictionary<string, EventReference>> sounds;
    
    #endregion
    
    #region Inspector Actions

    #endregion

    private FMOD.Studio.EventInstance _musicEvent;

    public FMOD.Studio.EventInstance PlaySound(string sCategory, string sName, float v = 1)
    {
        float volume = v * sfxVolume;
        FMOD.Studio.EventInstance sound = RuntimeManager.CreateInstance(sounds[sCategory][sName]);
        if (volume > 0.05f)
        {
            sound.setVolume(volume);
            sound.start();
            sound.release();
        }
        return sound;
    }
    
    public FMOD.Studio.EventInstance PlaySound(string path, float v = 1)
    {
        float volume = v * sfxVolume;
        FMOD.Studio.EventInstance sound = RuntimeManager.CreateInstance(path);
        if (volume > 0.05f)
        {
            sound.setVolume(volume);
            sound.start();
            sound.release();
        }
        return sound;
    }
    
    public FMOD.Studio.EventInstance PLayMusic(float v = 0.5f)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        _musicEvent.getPlaybackState(out state);
        if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING) _musicEvent.stop(STOP_MODE.ALLOWFADEOUT);
        
        _musicEvent = RuntimeManager.CreateInstance(music);
        
        float volume = v * musicVolume;
        _musicEvent.setVolume(volume);
        _musicEvent.start();
        _musicEvent.release();
        return _musicEvent;
    }
    
    // Needs to be called from another script
    //Made StartMusic.cs for that
    public void UpdateMusicVolume()
    {
        _musicEvent.setVolume(musicVolume);
    }

    public void SetMusicVolume(float v)
    {
        musicVolume = v;
        UpdateMusicVolume();
    }
}
