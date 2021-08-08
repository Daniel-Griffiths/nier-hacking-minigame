using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public Vector3 offset;
	public Transform target;
	public float smoothSpeed = 0.125f;

	void FixedUpdate()
	{
		if(target){
			Vector3 desiredPosition = target.position + offset;
			Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
			transform.position = smoothedPosition;
		}
	}
}