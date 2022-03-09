using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    [SerializeField] GameObject CameraHolder;

    [SerializeField] KeyCode MenuBuild = KeyCode.B;

    [SerializeField] bool MenuToggle;

    [SerializeField] float ToggleTimer = 1f;


    CameraLook look;
    CameraMove move;


    private void Awake()
    {
        look = CameraHolder.GetComponent<CameraLook>();
        move = CameraHolder.GetComponent<CameraMove>();
    }

    // Start is called before the first frame update
    void Start()
    {
        MenuToggle = false;
    }

    // Update is called once per frame
    void Update()
    {
        ToggleCamera();
    }

    void ToggleCamera()
    {
        if (Input.GetKeyDown(MenuBuild) && MenuToggle)
        {
            MenuToggle = !MenuToggle;

            Debug.Log("Build menu close!");

            //Get current State
            MenuToggle = look.enabled;

            //Flip it
            MenuToggle = !MenuToggle;

            //Set the current State to the flipped value
            look.enabled = !look.enabled;

        }

        if (Input.GetKeyDown(MenuBuild) && !MenuToggle)
        {        
           
            Debug.Log("Build menu open!");
        }
            MenuToggle = !MenuToggle;
    }

    IEnumerator ToggleDelay()
    {

        //MenuToggle = MenuToggle ? true : false;
        /*if (MenuToggle) MenuToggle = false;
        if (!MenuToggle) MenuToggle = true;*/
        yield return new WaitForSeconds(ToggleTimer);
    }
}
