using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 //Κίνηση παίχτη

public class Mover : MonoBehaviour
{
    //SerializeField μπορούμε να βάλουμε τιμή μέσα από το unity
    [SerializeField]float moveSpeed = 10f;
    //[SerializeField]float yValue = 1f;
    //[SerializeField]float zValue = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        PrintInstructions();
    }

    // Update is called once per frame
    void Update()
    {
       MovePlayer();
    }

    // Custom Methods
    void PrintInstructions(){
        Debug.Log("Welcome to the game");
        Debug.Log("This is my first project");
        Debug.Log("Hope you enjoy");
    }

    void MovePlayer(){
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        
        transform.Translate(xValue,0,zValue);
    }
}
