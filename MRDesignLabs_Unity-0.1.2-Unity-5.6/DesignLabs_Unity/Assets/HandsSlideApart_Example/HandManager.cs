using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance { get; private set; }

	public ExplodingObject ExplodingObject_01;

	public const float MaxHandExtension = 10.0f;

    public HandScript_temp HandLeft;
    public HandScript_temp HandRight;


    public float DistBtwnHands { get { return distBtwnHands; } private set { distBtwnHands = value; } }
    public float BaseDistBtwnHands { get { return baseDistBtwnHands; } private set { baseDistBtwnHands = value; } }

    public float DistBetweenHandsNormalized{get{return distBetweenHandsNormalized;} private set{distBetweenHandsNormalized = value;}}

	public float PercentageOfTotalHandExpansion{get{return percentageOfTotalHandExpansion;} private set{percentageOfTotalHandExpansion = value;}}

	public float TotalDistanceForExpansion{get{return totalDistanceForExpansion;} private set{totalDistanceForExpansion = value;}}

    // Use this for initialization
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

		CheckBaseDistanceBetweenHands();

#if UNITY_EDITOR
        if (Input.GetKeyDown (KeyCode.Space)){
			Debug.Log("Engaged ExplodingObject");
			ExplodingObject_01.Engaged = true;
			baseDistBtwnHands = CheckDistanceBetweenHands();
			totalDistanceForExpansion = MaxHandExtension - baseDistBtwnHands;
		}
#endif
#if UNITY_WSA
        if(HandLeft.transform.GetComponent<HUX.Utility.LocalHandInput>().Pressed || HandRight.transform.GetComponent<HUX.Utility.LocalHandInput>().Pressed)
        {
            Debug.Log("Engaged ExplodingObject");
            ExplodingObject_01.Engaged = true;
            baseDistBtwnHands = CheckDistanceBetweenHands();
            totalDistanceForExpansion = MaxHandExtension - baseDistBtwnHands;
        }
#endif
        //percentage of total expansion = current hand distance / totalDistance available ForExpansion 
        //(length between fingers of two outstretched arms)
        PercentHandExpansion();
    }

	float CheckDistanceBetweenHands(){

		//Debug.Log("Dist left to right : "+Vector3.Distance(HandLeft.transform.position, HandRight.transform.position));
		distBtwnHands = Vector3.Distance(HandLeft.transform.position, HandRight.transform.position);
		return distBtwnHands;
	}

    float DistanceBetweenHandsNormalized(){

        return distBtwnHands - baseDistBtwnHands;
    }

    void CheckBaseDistanceBetweenHands()
    {
        if (ExplodingObject_01.Engaged)
        {
            if (distBtwnHands < baseDistBtwnHands)
                baseDistBtwnHands = distBtwnHands;
            else
            {
				//expand objects
            }
        }
    }

	float PercentHandExpansion(){
		distBtwnHands = CheckDistanceBetweenHands();
        distBetweenHandsNormalized = DistanceBetweenHandsNormalized();
		//percentage of total expansion = current hand distance / totalDistanceForExpansion
		percentageOfTotalHandExpansion = distBetweenHandsNormalized/totalDistanceForExpansion;
		//Debug.Log("Percentage of hand Expansion "+percentageOfTotalHandExpansion);
		return percentageOfTotalHandExpansion;
	}

//private variables
    private float distBtwnHands;
    private float baseDistBtwnHands;
	private float totalDistanceForExpansion;
	private float percentageOfTotalHandExpansion;
    private float distBetweenHandsNormalized;
}
