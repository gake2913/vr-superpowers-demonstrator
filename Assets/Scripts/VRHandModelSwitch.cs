using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRHandModelSwitch : MonoBehaviour
{

    public GameObject[] ModelPrefabs;
    public GameObject OriginalModel;

    private GameObject shownModel = null;

    private GameObject[] spawnedModels;

    private bool hide = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnedModels = new GameObject[ModelPrefabs.Length];

        for (int i = 0; i < spawnedModels.Length; i++)
        {
            GameObject model = Instantiate(ModelPrefabs[i], transform);
            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = Quaternion.identity;
            spawnedModels[i] = model;
            model.SetActive(false);
        }

        shownModel = OriginalModel;
        shownModel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeModel(int index)
    {
        //Debug.Log("Change Model " + index);

        if (index < -1 || index >= ModelPrefabs.Length)
        {
            Debug.LogError(gameObject.name + " VRHandModelSwitch: Could not find Index " + index);
            return;
        }

        shownModel.SetActive(false);
        
        if (index == -1) shownModel = OriginalModel;
        else shownModel = spawnedModels[index];

        if (!hide)
            shownModel.SetActive(true);
    }

    public void ChangeModel(int index, bool hide)
    {
        SetHideStatus(hide);
        ChangeModel(index);
    }

    public void SetHideStatus(bool hide)
    {
        this.hide = hide;

        if (hide)
        {
            shownModel.SetActive(false);
        }
        else
        {
            shownModel.SetActive(true);
        }
    }
}
