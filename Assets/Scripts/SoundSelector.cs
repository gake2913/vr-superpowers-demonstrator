using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSelector : MonoBehaviour
{

    public GameObject SoundObjectPrefab;
    public float MaxRange = 10;
    public bool ScaleSoundObjects = true;

    private Transform playerCam;
    private Transform soundObjectParent;
    private List<Transform> soundObjects;
    private Dictionary<Transform, AudioSource> soundSources;

    private AnimationCurve scale;

    // Start is called before the first frame update
    void Start()
    {
        playerCam = Camera.main.transform;
        soundObjectParent = new GameObject("SoundSelectorParent").transform;
        soundObjects = new List<Transform>();
        soundSources = new Dictionary<Transform, AudioSource>();

        scale = AnimationCurve.Linear(0, 1, 10, 0);
        scale.postWrapMode = WrapMode.ClampForever;
        scale.preWrapMode = WrapMode.ClampForever;
    }

    // Update is called once per frame
    void Update()
    {
        soundObjectParent.transform.position = transform.position;

        if (Input.GetKeyDown(KeyCode.C))
        {
            AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
            foreach (AudioSource source in audioSources)
            {
                float dist = Vector3.Distance(playerCam.position, source.transform.position);
                if (dist < MaxRange)
                {
                    GameObject go = Instantiate(SoundObjectPrefab);
                    go.transform.parent = soundObjectParent;
                    Vector3 toSource = (source.transform.position - playerCam.position);
                    go.transform.position = playerCam.position + (dist < 1 ? toSource : toSource / dist);
                    go.transform.LookAt(source.transform.position);
                    go.transform.localScale = ScaleSoundObjects ? scale.Evaluate(dist) * Vector3.one : Vector3.one;
                    soundObjects.Add(go.transform);
                    soundSources[go.transform] = source;
                }
            }
        }

        if (Input.GetKey(KeyCode.C))
        {
            foreach(Transform t in soundObjects)
            {
                AudioSource source = soundSources[t];

                float dist = Vector3.Distance(playerCam.position, source.transform.position);
                Vector3 toSource = (source.transform.position - playerCam.position);
                t.position = playerCam.position + (dist < 1 ? toSource : toSource / dist);
                t.transform.LookAt(source.transform.position);
                t.transform.localScale = ScaleSoundObjects ? scale.Evaluate(dist) * Vector3.one : Vector3.one;

                bool active = Vector3.Angle(playerCam.forward, toSource) < 5;
                source.mute = !active;
                t.GetComponent<SoundDisplay>().SetActive(active);
            }

        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            foreach(Transform t in soundObjects)
            {
                soundSources[t].mute = false;
                Destroy(t.gameObject);
            }

            soundObjects.Clear();
            soundSources.Clear();
        }
    }
}
