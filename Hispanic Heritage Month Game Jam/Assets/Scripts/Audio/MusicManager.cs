using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    /* TL;DR:
     * I'm using PlayScheduled to stitch together audio files as seamlessly as possible.
     * Base layer: Guitar loop with one main section that remains the same but 
     * the endings of the loop are selected at randomly.
     * Layer 2: There are three variants of the mandolin loop that are selected randomly every loop
     * Layer 3: Same thing as base layer but the main section is also randomly selected. Brazilian viola.
     * 
     * The layering logic is currently being done with audio mixer snapshots
     * */

    #region Variable Declaration
    [Header("AUDIO SOURCES")]
    [SerializeField] private AudioSource[] baseMainSectionSource;
    [SerializeField] private AudioSource[] baseEndingSource;
    [SerializeField] private AudioSource[] layer2Source;
    [SerializeField] private AudioSource[] layer3MainSectionSource;
    [SerializeField] private AudioSource[] layer3EndingSource;
    private int toggleAudioSource;
    [Space]
    [Header("AUDIO CLIPS")]
    [SerializeField] private AudioClip baseMainSectionClip;
    [SerializeField] private AudioClip[] baseEndingClips;
    [SerializeField] private AudioClip[] layer2Clips;
    [SerializeField] private AudioClip[] layer3MainSectionClips;
    [SerializeField] private AudioClip[] layer3EndingClips;
    private int baseRandomIndex;
    private int baseLastIndex;
    private int layer2RandomIndex;
    private int layer2LastIndex;
    private int layer3MainRandomIndex;
    private int layer3MainLastIndex;
    private int layer3EndingRandomIndex;
    private int layer3EndingLastIndex;
    [Space]
    [Header("SNAPSHOTS")]
    [SerializeField] private AudioMixerSnapshot baseLayerOnly;
    [SerializeField] private AudioMixerSnapshot layer2On;
    [SerializeField] private AudioMixerSnapshot layer3On;
    [SerializeField] private AudioMixerSnapshot layers2and3;
    [SerializeField] private AudioMixerSnapshot allLayersOn;
    [SerializeField] private AudioMixerSnapshot musicOff;
    [SerializeField] private float fadeInTime;
    [SerializeField] private float fadeOutTime;
    [SerializeField] private float musicOutTime;

    //Timers
    private double endingDelayInSeconds = 24;
    private double loopDuration = 31;
    private double nextStartTime;
    private float timer;
    [SerializeField] private SceneChanger sceneChanger;
    #endregion

    void Start()
    {
        nextStartTime = AudioSettings.dspTime + 1;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(AudioSettings.dspTime > nextStartTime - 1)
        {
            PlayAndLoopMusic();
        }

        if (sceneChanger.isStarting)
        {
            timer += Time.deltaTime;
            print(timer);

            if(timer > 60 && timer < 65)
            {
                MusicMandolinOn();
            }
            else if(timer > 120 && timer < 125)
            {
                MusicViolaOn();
            }
            else if(timer > 170 && timer < 175)
            {
                MusicFull();
            }
        }
    }

    #region Music Concatenation Logic
    private void PlayAndLoopMusic()
    {
        ToggleSourceAndRandomizeAudioClips();
        AssignRandomClipsToAudioSources();
        ScheduleAudioSourcesAndIncrementNextStartTime();
    }

    private void ToggleSourceAndRandomizeAudioClips()
    {
        //Switches toggle variable to alternate between audio sources for PlayScheduled()
        toggleAudioSource = 1 - toggleAudioSource;

        //Get random index for random clips and check if it's the same as the last loop

        //Base Layer
        while (baseRandomIndex == baseLastIndex)
        {
            baseRandomIndex = Random.Range(0, baseEndingClips.Length);
        }
        baseLastIndex = baseRandomIndex;
        //Layer2
        while (layer2RandomIndex == layer2LastIndex)
        {
            layer2RandomIndex = Random.Range(0, layer2Clips.Length);
        }
        layer2LastIndex = layer2RandomIndex;
        //Layer3
        while (layer3MainRandomIndex == layer3MainLastIndex)
        {
            layer3MainRandomIndex = Random.Range(0, layer3MainSectionClips.Length);
        }
        layer3MainLastIndex = layer3MainRandomIndex;

        layer3EndingRandomIndex = Random.Range(0, layer3EndingClips.Length);
    }

    private void AssignRandomClipsToAudioSources()
    {
        baseMainSectionSource[toggleAudioSource].clip = baseMainSectionClip;
        baseEndingSource[toggleAudioSource].clip = baseEndingClips[baseRandomIndex];

        layer2Source[toggleAudioSource].clip = layer2Clips[layer2RandomIndex];

        layer3MainSectionSource[toggleAudioSource].clip = layer3MainSectionClips[layer3MainRandomIndex];
        layer3EndingSource[toggleAudioSource].clip = layer3EndingClips[layer3EndingRandomIndex];
    }

    private void ScheduleAudioSourcesAndIncrementNextStartTime()
    {
        //Schedule audio sources
        baseMainSectionSource[toggleAudioSource].PlayScheduled(nextStartTime);
        baseEndingSource[toggleAudioSource].PlayScheduled(nextStartTime + endingDelayInSeconds);

        layer2Source[toggleAudioSource].PlayScheduled(nextStartTime);

        layer3MainSectionSource[toggleAudioSource].PlayScheduled(nextStartTime);
        layer3EndingSource[toggleAudioSource].PlayScheduled(nextStartTime + endingDelayInSeconds);

        //Increment nextStartTime with total duration for the loop
        nextStartTime += loopDuration;
    }
    #endregion

    #region Music Layering Logic

    public void MusicGuitarOnly()
    {
        baseLayerOnly.TransitionTo(fadeOutTime);
    }

    public void MusicMandolinOn()
    {
        layer2On.TransitionTo(fadeInTime);
    }

    public void MusicViolaOn()
    {
        layer3On.TransitionTo(fadeInTime);
    }

    public void MusicViolaAndMandolinOnly()
    {

        layers2and3.TransitionTo(fadeInTime);
    }

    public void MusicFull()
    {
        allLayersOn.TransitionTo(fadeInTime);
    }

    public void MusicOff()
    {
        musicOff.TransitionTo(musicOutTime);
    }
    #endregion
}
