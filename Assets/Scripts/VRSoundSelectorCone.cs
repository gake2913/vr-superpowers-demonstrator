using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRSoundSelectorCone : MonoBehaviour
{

    public InputActionReference TriggerAxis;

    public AnimationCurve VolumeFalloff;
    public Animator VignetteAnimator;
    public Transform TealRoomRoot;
    public Transform playerCam;
    public GameObject SoundSourcePrefab;

    private List<SoundSelectorSource> soundSources = new List<SoundSelectorSource>();

    private bool buttonPrev = false;

    // Start is called before the first frame update
    void Start()
    {
        buttonPrev = TriggerAxis.action.ReadValue<float>() > 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - TealRoomRoot.position.x) > 5) return;
        if (Mathf.Abs(transform.position.y - TealRoomRoot.position.y) > 10) return;
        if (Mathf.Abs(transform.position.z - TealRoomRoot.position.z) > 5) return;

        bool buttonActive = TriggerAxis.action.ReadValue<float>() > 0.5f;

        if (buttonActive && !buttonPrev)
        {
            VignetteAnimator.SetTrigger("On");

            AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
            foreach (AudioSource source in audioSources)
            {
                GameObject soundSourceGO = Instantiate(SoundSourcePrefab, source.transform);
                SoundSelectorSource soundSource = soundSourceGO.GetComponent<SoundSelectorSource>();
                soundSources.Add(soundSource);
                soundSource.AudioSource = source;
                soundSource.Player = playerCam;

                Vector3 camForward = playerCam.forward;
                Vector3 toSource = source.transform.position - playerCam.position;

                float angle = Vector3.Angle(camForward, toSource);

                soundSource.SetVolume(VolumeFalloff.Evaluate(angle / 180f));
            }
        }

        if (buttonActive)
        {
            foreach (SoundSelectorSource source in soundSources)
            {
                Vector3 camForward = playerCam.forward;
                Vector3 toSource = source.transform.position - playerCam.position;

                float angle = Vector3.Angle(camForward, toSource);

                source.SetVolume(VolumeFalloff.Evaluate(angle / 180f));
            }

        }

        if (!buttonActive && buttonPrev)
        {
            VignetteAnimator.SetTrigger("Off");

            foreach (SoundSelectorSource source in soundSources)
            {
                source.Reset();
                Destroy(source.gameObject);
            }
        }

        buttonPrev = buttonActive;
    }
}
