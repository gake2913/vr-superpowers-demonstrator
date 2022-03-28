using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundDisplay : MonoBehaviour
{

    public Image Background;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActive(bool active)
    {
        Background.color = active ? Color.green : Color.red;
    }
}
