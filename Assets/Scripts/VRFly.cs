using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFly : MonoBehaviour
{

    public VRMovementFlying Player;
    public Transform YellowRoomRoot;

    public Animator FadeToBlack;

    private bool flyMode = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {
        if (Mathf.Abs(transform.position.x - YellowRoomRoot.position.x) > 5) return;
        if (Mathf.Abs(transform.position.y - YellowRoomRoot.position.y) > 10) return;
        if (Mathf.Abs(transform.position.z - YellowRoomRoot.position.z) > 5) return;

        if (!flyMode) EnterFlyMode();
    }

    private void EnterFlyMode()
    {
        FadeToBlack.SetTrigger("Fade");
        StartCoroutine(EnterFlyModeCorouting());
    }

    private IEnumerator EnterFlyModeCorouting()
    {
        yield return new WaitForSeconds(0.5f);
        flyMode = true;
        Player.Activate(transform);
    }

    public void ExitFlyMode()
    {
        flyMode = false;
    }
}
