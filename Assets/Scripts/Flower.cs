using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Flower : MonoBehaviour
{

    [Range(0, 3)] public int Stage = 0;

    public GameObject Stage1;
    public GameObject Stage2;
    public GameObject Stage3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Stage1.SetActive(false);
        Stage2.SetActive(false);
        Stage3.SetActive(false);

        switch(Stage)
        {
            case 1: Stage1.SetActive(true); break;
            case 2: Stage2.SetActive(true); break;
            case 3: Stage3.SetActive(true); break;
        }
    }

    public void AddStage()
    {
        if (!PowersManager.instance.RedActive) return;

        Stage++;
        if (Stage > 3) Stage = 0;
    }
}
