﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public bool keyNeeded = false;              //Is key needed for the door
    public bool gotKey;                  //Has the player acquired key
    public GameObject keyGameObject;            //If player has Key,  assign it here
    public GameObject txtToDisplay;             //Display the information about how to close/open the door

    private bool playerInZone;                  //Check if the player is in the zone
    private bool doorOpened;                    //Check if door is currently opened or not

    private Animation doorAnim;
    private BoxCollider doorCollider;           //To enable the player to go through the door if door is opened else block him

    enum DoorState
    {
        Closed,
        Opened,
        Jammed
    }

    DoorState doorState = new DoorState();      //To check the current state of the door

    public QuizController quizController;

    public Canvas quizCanvas;

    private bool waitingForQuiz = true; // Declare the waitingForQuiz variable

    /// <summary>
    /// Initial State of every variables
    /// </summary>
    private void Start()
    {
        quizCanvas.enabled = false;

        gotKey = false;
        doorOpened = false;                     //Is the door currently opened
        playerInZone = false;                   //Player not in zone
        doorState = DoorState.Closed;           //Starting state is door closed

        txtToDisplay.SetActive(false);

        doorAnim = transform.parent.gameObject.GetComponent<Animation>();
        doorCollider = transform.parent.gameObject.GetComponent<BoxCollider>();

        //If Key is needed and the KeyGameObject is not assigned, stop playing and throw error
        if (keyNeeded && keyGameObject == null)
        {
            //UnityEditor.EditorApplication.isPlaying = false;
            Debug.LogError("Assign Key GameObject");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        txtToDisplay.SetActive(true);
        playerInZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerInZone = false;
        txtToDisplay.SetActive(false);
    }

   private void Update()
    {
        if (playerInZone)
        {
            if (doorState == DoorState.Closed || gotKey)
            {
                // Check if the quiz has been answered correctly
                if (quizController != null && quizController.IsQuizAnsweredCorrectly())
                {
                    txtToDisplay.GetComponent<Text>().text = "Press 'E' to Open";
                    doorCollider.enabled = false;
                }
                else
                {
                    txtToDisplay.GetComponent<Text>().text = "Quiz answer required to open";
                    doorCollider.enabled = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && playerInZone)
        if (Input.GetKeyDown(KeyCode.E) && playerInZone && doorState == DoorState.Closed)
        {
            quizController.StartQuiz();
            quizCanvas.enabled = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;


            doorOpened = !doorOpened;           //The toggle function of door to open/close

            StartCoroutine(WaitForQuizToBeAnsweredCorrectly());
        }
    }
           
    private IEnumerator WaitForQuizToBeAnsweredCorrectly()
    {
        while (waitingForQuiz)
        {
            if (quizController.IsQuizAnsweredCorrectly())
            {
                Debug.Log("Quiz answered correctly!");

                // Continue with door opening logic
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                quizCanvas.enabled = false;

                if (doorState == DoorState.Closed && !doorAnim.isPlaying)
                {
                    if (!keyNeeded)
                    {
                        doorAnim.Play("Door_Open");
                        doorState = DoorState.Opened;
                    }
                    else if (keyNeeded && !gotKey)
                    {
                        if (doorAnim.GetClip("Door_Jam") != null)
                            doorAnim.Play("Door_Jam");
                        doorState = DoorState.Jammed;
                    }
                }

                if (doorState == DoorState.Closed && gotKey && !doorAnim.isPlaying)
                {
                    doorAnim.Play("Door_Open");
                    doorState = DoorState.Opened;
                }

                waitingForQuiz = false;
            }
            else
            {
                // Continue waiting and check again in the next frame
                yield return null;
            }
        }
    }
}