using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float speed = 0.007f;
    Vector3 lastMousePos;
    private float minSize = 1f;
    private float maxSize = 20f;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.orthographicSize < maxSize)
        {
            Camera.main.orthographicSize += 0.5f;
            speed *= 1.05f;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize > minSize)
        {
            Camera.main.orthographicSize -= 0.5f;
            speed /= 1.05f;
        }
        if (Input.GetMouseButton(2) || Input.GetMouseButton(0))
        {
            if(lastMousePos != Vector3.zero)
            {
                Vector3 offset = (lastMousePos - Input.mousePosition) * speed;
                //offset = Quaternion.AngleAxis(angleOffset, Vector3.forward) * offset;
                transform.position += new Vector3(offset.x, offset.y, 0);
            }
        }
        if (Input.GetMouseButtonUp(2) || Input.GetMouseButton(0))
        {
            lastMousePos = Vector3.zero;
        }
        lastMousePos = Input.mousePosition;
    }
}