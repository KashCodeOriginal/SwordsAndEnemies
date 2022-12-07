using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera _mainCam;

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        Quaternion rotation = _mainCam.transform.rotation;
        
        transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
    }
}
