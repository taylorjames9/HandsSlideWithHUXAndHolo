using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingPiece : MonoBehaviour {


	public Vector3 StartPos{get; private set;}
	public Vector3 EndPos{get; private set;}

	public float PercentageAlongTrajectory{get{return percentageAlongTrajectory;} private set{percentageAlongTrajectory = value;}}

	public Vector3 DirectionToCenter{get{return directionToCenter;} private set{directionToCenter = value;}}

	public Vector3 OppDirectionToCenter{get{return oppDirectionToCenter;}private set{oppDirectionToCenter = value;}}

	private ExplodingObject myExplodingParent; 

	// Use this for initialization
	void Start () {
		myExplodingParent = transform.parent.GetComponent<ExplodingObject>();
		CalculateStartPos();
		CalculateDistanceToCenter();
		FindDirectionToCenter();
		Find_OPP_DirectionToCenter();
		CalculateEndPos();
	}
	
	// Update is called once per frame
	void Update () {
		//if hand manager hands is engaged
		if(myExplodingParent.Engaged){
			//if(HandManager.Instance.PercentageOfTotalHandExpansion <1.0){
			//mylocation is going to be ... 
			transform.position = startPos + endPos*HandManager.Instance.PercentageOfTotalHandExpansion;
			//}
		} 
	}

	void CalculateStartPos(){
		startPos = transform.position;
	}
	void CalculateEndPos(){
		//multiply the startPos by a constant amount, in the opposite direction as the center
		endPos = StartPos + (oppDirectionToCenter * myExplodingParent.ExplosionMultiplier);
		GameObject go = Instantiate(myExplodingParent.EndPointMarker, endPos, Quaternion.identity);
	}
	void CalculateDistanceToCenter(){
		//Vector3.Distance(transform.position, myExplodingParent.CenterPiece.position);
        Vector3.Distance(transform.position, myExplodingParent.transform.position);

    }
    void FindDirectionToCenter(){
		//// Gets a vector that points from the player's position to the target's.
		//var heading = target.position - player.position;
		directionToCenter = myExplodingParent.transform.position - transform.position;
	}
	void Find_OPP_DirectionToCenter(){
		///Gets a vector that points from the player's position to the target's.
		//var heading = target.position - player.position;
		oppDirectionToCenter = -1*directionToCenter;
	}

	float MyDistToStartPos(){
		return Vector3.Distance(transform.position, startPos);
	}

	float MyDistToEndPos(){
		float myDist = Vector3.Distance(transform.position, endPos);
		//Debug.Log("My Distance to end point"+myDist);
		return myDist;
	}

	private Vector3 startPos;
	private Vector3 endPos;
	private float percentageAlongTrajectory;
	private Vector3 directionToCenter;
	private Vector3 oppDirectionToCenter;
}
