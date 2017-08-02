using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HUX;

public class HandScript_temp : MonoBehaviour {

	enum HandType {None, LeftHand, RightHand};

	[SerializeField]
	private HandType hand; 

	[SerializeField]
	private float moveSpeed = 0.5f;

    [SerializeField]
    private int handNumber;
	
	// Update is called once per frame
	void Update () {

        if (hand.Equals(HandType.LeftHand))
        {
            if (Input.GetKey(KeyCode.Z))
                transform.Translate((-1) * moveSpeed * Time.deltaTime, 0, 0);
            else if (Input.GetKey(KeyCode.C))
                transform.Translate( moveSpeed * Time.deltaTime, 0, 0);
        }
        else if (hand.Equals(HandType.RightHand))
        {
            if (Input.GetKey(KeyCode.B))
                transform.Translate((-1) * moveSpeed * Time.deltaTime, 0, 0);
            else if (Input.GetKey(KeyCode.M))
                transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
	}

    private void LateUpdate()
    {
        //transform.position = HUX.Focus.FocusManager.Instance.Focusers[0].
        transform.position = InputSources.Instance.hands.GetWorldPosition(handNumber);
    }
}
