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

    private Dictionary<AudioSource, float> sourceVolumes;
    private AudioSource[] audioSources;

    private bool buttonPrev = false;

    // Start is called before the first frame update
    void Start()
    {
        buttonPrev = TriggerAxis.action.ReadValue<float>() > 0.5f;

        sourceVolumes = new Dictionary<AudioSource, float>();
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

            audioSources = FindObjectsOfType<AudioSource>();
            foreach (AudioSource source in audioSources)
            {
                sourceVolumes[source] = source.volume;

                Vector3 camForward = playerCam.forward;
                Vector3 toSource = source.transform.position - playerCam.position;

                float angle = Vector3.Angle(camForward, toSource);

                source.volume = sourceVolumes[source] * VolumeFalloff.Evaluate(angle / 180f);
            }
        }

        if (buttonActive)
        {
            foreach(AudioSource source in audioSources)
            {
                Vector3 camForward = playerCam.forward;
                Vector3 toSource = source.transform.position - playerCam.position;

                float angle = Vector3.Angle(camForward, toSource);

                source.volume = sourceVolumes[source] * VolumeFalloff.Evaluate(angle / 180f);
            }

        }

        if (!buttonActive && buttonPrev)
        {
            VignetteAnimator.SetTrigger("Off");

            foreach(AudioSource source in audioSources)
            {
                source.volume = sourceVolumes[source];
            }
        }

        buttonPrev = buttonActive;
    }
}
