using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] GameObject CameraHolder;
    [SerializeField] GameObject Camera;
    [SerializeField] Object OriginalLocation;
    [SerializeField] Transform OriginalPos;
    [SerializeField] GameObject TweenTarget;
    [SerializeField] KeyCode MenuBuild = KeyCode.B;
    [SerializeField] float TweenTime = 1f;
    GameObject ObjectToDestroy;
    [SerializeField] bool BuildingMenu;
    [SerializeField] bool SpawnedLocation;
    [Header(" ")]


    CameraLook look;
    CameraMove move;


    private void Awake()
    {
        look = CameraHolder.GetComponent<CameraLook>();
        move = CameraHolder.GetComponent<CameraMove>();

        BuildingMenu = false;
        SpawnedLocation = false;
    }


    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        if (Input.GetKeyDown(MenuBuild) && !BuildingMenu)
        {
            Toggle();
            Tween();
        }
        else if (Input.GetKeyDown(MenuBuild) && BuildingMenu)
        {
            Toggle();
        }

        if (BuildingMenu && !SpawnedLocation) SpawnLocationSaver();

        if (!BuildingMenu && SpawnedLocation) DeSpawnLocationSaver();
    }

    void Toggle()
    {
        //Flip the boolean
        BuildingMenu = !BuildingMenu;

        //Set the enable state to the oposite of the state of the boolean.
        look.enabled = !BuildingMenu;
        move.enabled = !BuildingMenu;
    }

    void Tween()
    {
        LeanTween.moveX(CameraHolder, TweenTarget.transform.position.x, TweenTime);
        LeanTween.moveY(CameraHolder, TweenTarget.transform.position.y, TweenTime);
        LeanTween.moveZ(CameraHolder, TweenTarget.transform.position.z, TweenTime);

        LeanTween.rotateX(Camera, TweenTarget.transform.eulerAngles.x, TweenTime);
        LeanTween.rotateY(Camera, TweenTarget.transform.eulerAngles.y, TweenTime);
        LeanTween.rotateZ(Camera, TweenTarget.transform.eulerAngles.z, TweenTime);
    }

    void SpawnLocationSaver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SpawnedLocation = true;
        Instantiate(OriginalLocation, OriginalPos.position, OriginalPos.rotation);
    }

    void DeSpawnLocationSaver()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        StartCoroutine(DelaySpawn());
        ObjectToDestroy = GameObject.FindGameObjectWithTag("Position");

        LeanTween.moveX(CameraHolder, ObjectToDestroy.transform.position.x, TweenTime);
        LeanTween.moveY(CameraHolder, ObjectToDestroy.transform.position.y, TweenTime);
        LeanTween.moveZ(CameraHolder, ObjectToDestroy.transform.position.z, TweenTime);

        LeanTween.rotateX(Camera, ObjectToDestroy.transform.eulerAngles.x, TweenTime);
        LeanTween.rotateY(Camera, ObjectToDestroy.transform.eulerAngles.y, TweenTime);
        LeanTween.rotateZ(Camera, ObjectToDestroy.transform.eulerAngles.z, TweenTime);
    }

    IEnumerator DelaySpawn()
    {
        ObjectToDestroy = GameObject.FindGameObjectWithTag("Position");
        SpawnedLocation = false;
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Destroying residual objects!");
        Destroy(ObjectToDestroy);
    }
}
