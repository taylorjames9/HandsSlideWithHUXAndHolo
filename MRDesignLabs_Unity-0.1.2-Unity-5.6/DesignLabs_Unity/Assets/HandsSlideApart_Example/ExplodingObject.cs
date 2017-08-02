using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingObject : MonoBehaviour {
	public bool Engaged{get{return engaged;}set{engaged = value;}}
	public List<ExplodingPiece> ExplodingPiecesList;

	public GameObject EndPointMarker;

	public float ExplosionMultiplier = 100.0f;

	//public Transform CenterPiece;

	//if all pieces are not direct children of the parent object, then populate ExplodingPiecesList manually
	public bool AllPiecesAreDirectChildren;


	// Use this for initialization
	void Start () {
		if(AllPiecesAreDirectChildren){
			foreach(Transform t in transform){
				if(t.GetComponent<ExplodingPiece>())
					ExplodingPiecesList.Add(t.GetComponent<ExplodingPiece>());
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}




	private bool engaged;
}
