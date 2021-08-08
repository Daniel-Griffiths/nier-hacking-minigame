using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAngle : MonoBehaviour
{
    public Camera mainCamera;
    public bool isLowAngle = false;

    private Vector3 _originalAngle;
    private Vector3 _originalOffset;

    void Start()
    {
        _originalAngle = mainCamera.transform.eulerAngles;
        _originalOffset = mainCamera.gameObject.GetComponent<CameraFollow>().offset;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == Tag.player) {
            if(isLowAngle){
                Vector3 rotation = mainCamera.transform.eulerAngles;

                rotation.x = 15f;
                mainCamera.transform.eulerAngles = rotation;
                mainCamera.gameObject.GetComponent<CameraFollow>().offset = new Vector3(0f, 2f, -5f);
            } else {
                mainCamera.transform.eulerAngles = _originalAngle;
                mainCamera.gameObject.GetComponent<CameraFollow>().offset =_originalOffset;
            }
        }
    }
}
