using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleTeleporter : MonoBehaviour
{

    public InvisibleTeleporter ConnectedTeleporter;
    public bool IsTeleporting = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsTeleporting)
        {
            IsTeleporting = false;
            return;
        }

        if (other.tag != "PlayerRig") return;

        Debug.Log(other.name);

        IsTeleporting = true;
        ConnectedTeleporter.IsTeleporting = true;

        Matrix4x4 m = ConnectedTeleporter.transform.localToWorldMatrix * transform.worldToLocalMatrix * other.transform.parent.localToWorldMatrix;
        other.transform.parent.SetPositionAndRotation(m.GetColumn(3), m.rotation);
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsTeleporting) return;

        if (other.tag != "PlayerRig") return;

        Debug.Log("Exit");

        IsTeleporting = false;
        ConnectedTeleporter.IsTeleporting = false;
    }
}
