using UnityEngine;
using System.Collections;

public class ControlScript : MonoBehaviour {

	int i = 1;
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0)){

			i++;
			i %= 2;
			GetComponent<Animator> ().SetFloat ("moveSpeed", i+1);

		}

	}
}
