using Sirenix.OdinInspector;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;


[CreateAssetMenu(menuName = "SoundManager")]
public class SoundManager : SingletonScriptableObject<SoundManager>
{
    #region Serialized Fields

    [Title("Volume Settings")]
    [SerializeField, Range(0f,1f)]
    public float sfxVolume;
    [SerializeField,Range(0f,1f)]
    [OnValueChanged("UpdateMusicVolume")]
    public float musicVolume;
    
    [Title("Music Track")]
    public EventReference music;
    
    [Title("Sounds By Category")] 
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine)]
    public  Dictionary<string, Dictionary<string, EventReference>> sounds;
    
    [TitleGroup("Add New Sound")]
    [HorizontalGroup("Add New Sound/labels", 0.5f, LabelWidth = 20)]
    [TitleGroup("Add New Sound/labels/Category")]
    
    [HideLabel]
    public string category;

    [TitleGroup("Add New Sound/labels/Name")]
    [HideLabel]
    public string soundName;
    
    [TitleGroup("Add New Sound")]
    [SerializeField]
    public EventReference fmodEvent;

    #endregion
    
    #region Inspector Actions
    
    [TitleGroup("Add New Sound")]
    [Button("Submit")]
    private void NamedButton()
    {
        if (sounds == null) sounds = new Dictionary<string, Dictionary<string, EventReference>>();
        if (!sounds.ContainsKey(category)) sounds.Add(category, new Dictionary<string, EventReference>());
        if (sounds[category].ContainsKey(soundName))
        {
            sounds[category][soundName] = fmodEvent;
        }
        else
        {
            sounds[category].Add(soundName, fmodEvent);
        }
    }
    
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
    
    public FMOD.Studio.EventInstance PLayMusic(float v = 1)
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
