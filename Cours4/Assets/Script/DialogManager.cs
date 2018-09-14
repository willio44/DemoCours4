using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour {

    [SerializeField] public GameObject dialogPrefab;
    [SerializeField] public GameObject mainCanvas;

    private bool actionAxisInUse = true;
    private GameObject player;
    private bool dialogIsInitiated = false;
    private DialogText currentDialog;
    private DialogDisplayer currentDialogDisplayer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        ProcessInput();
    }


    public void StartDialog(DialogText dialog)
    {
        dialogIsInitiated = true;
        player.GetComponent<PlayerMovement>().DisableControl();
        currentDialog = dialog;
        GameObject currentDialogObject = Instantiate(dialogPrefab, mainCanvas.transform);
        currentDialogDisplayer = currentDialogObject.GetComponent<DialogDisplayer>();
        currentDialogDisplayer.SetDialogText(currentDialog.GetDialogText());
    }


    public void ProcessInput()
    {
        if (ShouldProcessInput())
        {
            actionAxisInUse = true;
            if (currentDialog.IsNextDialog())
            {
                currentDialog = currentDialog.GetNextDialog();
                currentDialogDisplayer.SetDialogText(currentDialog.GetDialogText());
            }
            else
            {
                EndDialog();
            }
        }
        ValidateAxisInUse();
    }


    public void EndDialog()
    {
        dialogIsInitiated = false;
        currentDialogDisplayer.CloseDialog();
        player.GetComponent<PlayerMovement>().EnableControl();
        currentDialog = null;
    }


    private bool ShouldProcessInput()
    {
        if (dialogIsInitiated)
        {
            if (!actionAxisInUse && Input.GetAxis("Jump") != 0)
            {
                print(!actionAxisInUse);
                return true;
            }
        }
        return false;
    }


    private void ValidateAxisInUse()
    {
        if (Input.GetAxis("Jump") != 0)
        {
            actionAxisInUse = true;
        }
        else
        {
            actionAxisInUse = false;
        }
    }
}
 

