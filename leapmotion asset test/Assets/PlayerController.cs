using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using System;

public class PlayerController : MonoBehaviour
{
    public Player player;
    public GameObject hand;
    public GameObject leapController;
    private float holdDuration = 0.0f;
    private float timeStamp = 0.0f;
    private Leap.Hand h;
    private bool closedFist = false;

    void Start()
    {
        leapController = GameObject.FindGameObjectWithTag("Controller");
        Leap.Hand h = leapController.GetComponent<LeapProvider>().CurrentFrame.Hand(0);

    }
    //To get input from player
    void Update()
    {
        // Space bar jump method
        if (player.GetComponent<Player>().CheckIfGrounded())
        {
            //spaceBarJump();
            keyJump();
            detectFist();
            fistJump();
        }
    }

    private void fistJump()
    {
        if (closedFist)
        {
            holdDuration += Time.deltaTime;
            holdDuration = Mathf.Clamp(holdDuration, 0.0f, 5.0f);
            //player.ChangeColor(holdDuration / 5f, 1f - (holdDuration / 5f), 0f);
            hand.GetComponent<CapsuleHand>().updateHandColor(holdDuration / 5f, 1f - (holdDuration/5f), 0f);
        }
        else
        {
            if (holdDuration > 0.1f)
            {
                player.Move(holdDuration);
                holdDuration = 0.0f;
                //player.ChangeColor(0f, 0f, 0f);
                hand.GetComponent<CapsuleHand>().updateHandColor(0f, 0f, 0f);
            } 
        }
    }

    private void detectFist()
    {
        // If there is a hand in the current frame
        if (leapController.GetComponent<LeapProvider>().CurrentFrame.Hands.Count > 0)
        {
            //Acquire this hand
            h = leapController.GetComponent<LeapProvider>().CurrentFrame.Hands[0];
            ////If all fingers are not extended, there is a fist
            //if (!h.Fingers[0].IsExtended
            //    && !h.Fingers[1].IsExtended
            //    && !h.Fingers[2].IsExtended
            //    && !h.Fingers[3].IsExtended
            //    && !h.Fingers[4].IsExtended) { closedFist = true; }
            //else { closedFist = false; }
            if (h.GrabAngle > 3.0f) closedFist = true;
            else closedFist = false;
        }
    }

    public void keyJump()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.Move(1f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.Move(2f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.Move(3f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            player.Move(4f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            player.Move(5f);
        }
    }

    public void spaceBarJump(){
        if (Input.GetButton("Jump"))
        {
            holdDuration += Time.deltaTime;
            holdDuration = Mathf.Clamp(holdDuration, 0.0f, 5.0f);
            player.ChangeColor(holdDuration / 5f, 0f, 0f);
            if (GameObject.FindGameObjectsWithTag("RightHand").Length > 0)
            {
                hand = GameObject.FindGameObjectWithTag("RightHand");
                hand.GetComponent<CapsuleHand>().updateHandColor(holdDuration / 5f, 0f, 0f);
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            Debug.Log("Jumped for " + holdDuration + " seconds of force");
            player.Move(holdDuration);
            holdDuration = 0.0f;
            player.ChangeColor(0f, 0f, 0f);
        }
    }
}
