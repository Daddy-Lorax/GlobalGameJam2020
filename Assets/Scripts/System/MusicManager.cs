using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager ins; //Singleton Instance

    public AudioMixer audioMixer;
    public AudioSource audioSource;
    public AudioMixerSnapshot[] snapshots;
    public float[][] snapshotWeights = new float[][] { new float[] { 1f, 0f }, new float[] { 0f, 1f } };

    private Coroutine currentCrossfade;

    public float transitionTime = 0.5f;

    public int currentTrack = 0;

    public AudioClip[] musicTracks;

    private void Awake()
    {
        if (ins == null)
        {
            //DontDestroyOnLoad(gameObject);
            ins = this;

            //Init this
            audioSource = GetComponent<AudioSource>();
            //audioSource.Play();
        }
        else if (ins != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }


    public void ChangeMusic(int track)
    {
        //if (musicTracks[track] == audioSource.clip) { return; }

        //audioSource.clip = musicTracks[track];
        //audioMixer.TransitionToSnapshots(snapshots, snapshotWeights[track], transitionTime);
        if (currentCrossfade != null)
        {
            StopCoroutine(currentCrossfade);
        }
        currentCrossfade= StartCoroutine(CrossFade(track));
    }

    //Custom Crossfade routine since mixer snapshots create a dip in volume
    public IEnumerator CrossFade(int inIndex)
    {
        string[] mixerVarNames = new string[] { "volume1", "volume2" };
        //Obtain the current volumes
        float[] startVolumes = new float[] { -80f, -80f };
        for (int i = 0; i < startVolumes.Length; i++)
        {
            audioMixer.GetFloat(mixerVarNames[i], out startVolumes[i]);
            //Apply log space transform
            startVolumes[i] = Mathf.Pow(10f, startVolumes[i] / 20f);
        }
        Debug.Log("start vols "+string.Join(", ", startVolumes));
        float[] currentVolumes = startVolumes;
        float[] targetVolumes = new float[] { 0.0001f, 0.0001f };
        targetVolumes[inIndex] = 1f;

        //Setup Lerp
        float currentTime = 0;

        while (currentTime < transitionTime)
        {
            //Take step of lerp
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(startVolumes[inIndex], targetVolumes[inIndex], currentTime / transitionTime);
            float otherVol = 1f - newVol;
            //Find sum of other volumes
            float otherSum = 0f;
            Array.ForEach(currentVolumes, delegate (float i) { otherSum += i; });//fancy way of summing the array
            otherSum -= currentVolumes[inIndex]; //remove the current incoming track though!

            //Enforce Convex Combination of volumes
            for (int i = 0; i < currentVolumes.Length; i++)
            {
                if (i == inIndex)
                {
                    currentVolumes[i] = newVol;
                }
                else
                {
                    if (otherVol > 0.001f && otherSum > 0.001f)
                    {
                        currentVolumes[i] = currentVolumes[i] * otherVol / otherSum;
                    }
                    else { currentVolumes[i] = 0.0001f; }
                }
                audioMixer.SetFloat(mixerVarNames[i], Mathf.Log10(currentVolumes[i]) * 20f);
            }
            
            Debug.Log("current vols "+string.Join(", ",currentVolumes));
            //audioMixer.SetFloat("volume1", Mathf.Log10(newVol) * 20f);
            yield return null;
        }
        //Explicitly set to targets for final itteration to avoid unfinished lerp
        for (int i = 0; i < targetVolumes.Length; i++)
        {
            audioMixer.SetFloat(mixerVarNames[i], Mathf.Log10(targetVolumes[i]) * 20f);
        }
        currentCrossfade = null;
        yield break;
    }

    public static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10f, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeMusic(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeMusic(1);
        }
    }
}
