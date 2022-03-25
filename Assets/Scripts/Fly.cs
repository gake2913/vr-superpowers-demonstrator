using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Selectable
{

    public bool Idle = true;
    public bool PlayerMode = false;
    public float IdleRange = 0.1f;
    public float TargetIntervall = 0.1f;
    public GameObject FlyPlayerPrefab;

    private bool lastIdle = true;
    private Vector3 targetPosition;
    private float targetRotation;
    private Vector3 centerPos;
    private GameObject flyPlayer;
    private PlayerController player;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        lastIdle = Idle;

        if (Idle)
        {
            StartIdle();
        }

        StartCoroutine(ChooseTargets());

        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (PlayerMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ExitPlayerMode();
            }
        }

        if (lastIdle != Idle)
        {
            if (Idle)
            {
                StartIdle();
            }

            lastIdle = Idle;
        }

        if (Idle)
        {
            Vector3 vel = Vector3.zero;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, 0.1f);
            transform.position = newPos;
        }
    }

    private void StartIdle()
    {
        centerPos = transform.position;
    }

    private IEnumerator ChooseTargets()
    {
        while (true)
        {
            targetPosition = Random.insideUnitSphere * IdleRange + centerPos;
            targetRotation = Random.Range(-180f, 180f);
            yield return new WaitForSeconds(TargetIntervall);
        }
    }

    public override void Select()
    {
        base.Select();

        if (!PlayerMode)
        {
            // Go into player mode
            EnterPlayerMode();
        }
        else
        {
            // leave player mode
            ExitPlayerMode();
        }
    }

    private void EnterPlayerMode()
    {
        Debug.Log("Entering Fly Player Mode");

        Idle = false;

        foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
        {
            mr.enabled = false;
        }
        GetComponent<Collider>().enabled = false;

        player.enabled = false;
        player.GetComponentInChildren<Selector>().enabled = false;
        player.GetComponentInChildren<Camera>().enabled = false;
        player.GetComponentInChildren<AudioListener>().enabled = false;

        flyPlayer = Instantiate(FlyPlayerPrefab);
        flyPlayer.transform.position = centerPos;
        flyPlayer.transform.rotation = transform.rotation;

        Camera flyCam = flyPlayer.GetComponentInChildren<Camera>();
        Portal[] portals = FindObjectsOfType<Portal>();
        foreach (Portal portal in portals)
        {
            portal.playerCam = flyCam;
        }

        PlayerMode = true;
    }

    private void ExitPlayerMode()
    {
        Debug.Log("Leaving Fly Player Mode");

        Idle = true;
        lastIdle = true;

        foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
        {
            mr.enabled = true;
        }
        GetComponent<Collider>().enabled = true;

        player.enabled = true;
        player.GetComponentInChildren<Selector>().enabled = true;
        player.GetComponentInChildren<Camera>().enabled = true;
        player.GetComponentInChildren<AudioListener>().enabled = true;

        Portal[] portals = FindObjectsOfType<Portal>();
        foreach (Portal portal in portals)
        {
            portal.playerCam = player.GetComponentInChildren<Camera>();
        }

        transform.position = flyPlayer.transform.position;
        centerPos = flyPlayer.transform.position;
        transform.rotation = flyPlayer.transform.rotation;
        Destroy(flyPlayer);
        flyPlayer = null;

        PlayerMode = false;
    }
}
