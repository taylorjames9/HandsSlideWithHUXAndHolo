using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HUX;

public class HandScript_temp : MonoBehaviour {

	enum HandType {None, LeftHand, RightHand};

	[SerializeField]
	private HandType hand; 

	[SerializeField]
	private float moveSpeed;

    [SerializeField]
    private int handNumber;

    private int count;
	
	// Update is called once per frame
	void Update () {

#if UNITY_EDITOR
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

#endif
    }

    private void LateUpdate()
    {
#if !UNITY_EDITOR
        if(GetComponent<HUX.Utility.LocalHandInput>().IsTracked){
            transform.position = GetComponent<HUX.Utility.LocalHandInput>().LocalPosition;
        } else {
            HandManager.Instance.Engaged = false;
        }

#endif
    }
}
