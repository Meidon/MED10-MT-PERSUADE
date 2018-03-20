using UnityEngine;
using System.Collections;

public class RotationCube : MonoBehaviour
{


	private float ramdonTime;
	private Vector3 rotateDir;
    public float rotationSpeed;

	void Start ()
	{
		float x, y, z;
		x = Random.Range (5, 10);
		y = Random.Range (5, 10);
		z = Random.Range (5, 10);
		rotateDir = new Vector3(x,y,z);

		ramdonTime = Random.Range (2, 5);
	}
	
	void Update ()
	{
		ramdonTime -= Time.deltaTime * 1;

		this.transform.Rotate(rotateDir * Time.deltaTime*rotationSpeed);


		if (ramdonTime <= 0) {
			Start ();
		}
	}



}

