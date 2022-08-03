using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSelectorSource : MonoBehaviour
{

    public ParticleSystem Idle;
    public ParticleSystem Beam;
    public Material ParticleMaterial;

    public Transform Player;

    public Color ColorInactive = new Color(0, 0.78f, 1);
    public Color ColorActive = new Color(0.13f, 1, 0);

    public float BeamThreshold = 0.8f;

    public AudioSource AudioSource;

    private float startVolume;

    // Start is called before the first frame update
    void Start()
    {
        startVolume = AudioSource.volume;
    }

    // Update is called once per frame
    void Update()
    {
        Beam.transform.LookAt(Player);
    }

    public void SetVolume(float volume)
    {
        ParticleMaterial.color = Color.Lerp(ColorInactive, ColorActive, volume);
        
        if (!Beam.isPlaying && volume > BeamThreshold) Beam.Play();
        if (Beam.isPlaying && volume < BeamThreshold) Beam.Stop();

        AudioSource.volume = startVolume * volume;
    }

    public void Reset()
    {
        AudioSource.volume = startVolume;
    }
}
