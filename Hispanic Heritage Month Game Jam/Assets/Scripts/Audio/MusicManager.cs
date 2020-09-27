using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    /* TL;DR:
     * I'm using PlayScheduled to stitch together two audio files as seamlessly as possible.
     * The gist of it is that there's the main section of the loop that remains the same but 
     * the endings of the loop are selected at random so we can get a lot of variety out of little musical material
     * */

    #region Variable Declaration
    [Header("AUDIO SOURCES")]
    [SerializeField] private AudioSource[] mainSectionSource;
    [SerializeField] private AudioSource[] endingSource;
    private int toggleMainSectionSource;
    private int toggleEndingSource;
    [Space]
    [Header("AUDIO CLIPS")]
    [SerializeField] private AudioClip mainSectionClip;
    [SerializeField] private AudioClip[] endingClips;
    private int randomIndex;
    private int lastIndex;
    //Timers
    private double endingDelayInSeconds = 24;
    private double loopDuration = 31;
    private double nextStartTime;
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
           ConcatenateMainSectionWithRandomEnding();
        }
    }
    #region Music Logic
    private void ConcatenateMainSectionWithRandomEnding()
    {
        //Switches toggle variables to alternate between audio sources for PlayScheduled()
        toggleMainSectionSource = 1 - toggleMainSectionSource;
        toggleEndingSource = 1 - toggleEndingSource;

        //Get random index for the ending clip and check if it's the same as the last loop
        while(randomIndex == lastIndex)
        {
            randomIndex = Random.Range(0, endingClips.Length);
        }
        lastIndex = randomIndex;

        //Assign clips to audio sources
        mainSectionSource[toggleMainSectionSource].clip = mainSectionClip;
        endingSource[toggleEndingSource].clip = endingClips[randomIndex];

        //Schedule audio sources
        mainSectionSource[toggleMainSectionSource].PlayScheduled(nextStartTime);
        endingSource[toggleEndingSource].PlayScheduled(nextStartTime + endingDelayInSeconds);

        //Increment nextStartTime with total duration for the loop
        nextStartTime += loopDuration;
    }
    #endregion
}
