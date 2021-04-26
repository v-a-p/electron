using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    public GameObject dialog;
    public float dialogTime = 4f; //Time the dialog appears on the screen
    public float dialogTimer;

    // Start is called before the first frame update
    void Start()
    {
        dialog.SetActive(false); //Hide dialog when game starts
        dialogTimer = -1;
    }

    // Update is called once per frame
    void Update()
    {
        dialogTimer -= Time.deltaTime;
        if (dialogTimer < 0) dialog.SetActive(false);
    }

    public void ShowDialog()
    {
        dialogTimer = dialogTime;
        dialog.SetActive(true);
    }
}
