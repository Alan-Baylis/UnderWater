using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject Target; 

	// Update is called once per frame
	void Update ()
	{
	    var targetPostion = Target.transform.position;
	    targetPostion.z = -2;
	    transform.position = targetPostion;
	}
}
