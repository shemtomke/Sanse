using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform targetTransform;
    Vector3 tempVec3 = new Vector3();

    public void LateUpdate()
    {
        tempVec3.x = targetTransform.position.x;
        tempVec3.y = this.transform.position.y;
        tempVec3.z = this.transform.position.z;
        this.transform.position = tempVec3;
    }
}
