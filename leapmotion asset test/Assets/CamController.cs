using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class CamController : MonoBehaviour
{
    public GameObject leapController;
    public GameObject hand;
    public GameObject player;
    private float startX;
    private float cameraX; 
    // Start is called before the first frame update
    void Start()
    {
        leapController = GameObject.FindGameObjectWithTag("Controller");
        
        //leapController.GetComponent<LeapServiceProvider>().up;
        startX = player.transform.position.x;
        cameraX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + (cameraX - startX), transform.position.y, transform.position.z);
        if (GameObject.FindGameObjectsWithTag("RightHand").Length > 0)
        {
            hand = GameObject.FindGameObjectWithTag("RightHand");
            hand.GetComponent<CapsuleHand>().UpdateHand();
        }
            
    }
}
