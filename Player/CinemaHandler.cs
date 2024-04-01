using UnityEngine;

public class CinemaHandler : MonoBehaviour
{
    void MoveBodyToCamera(){
        GameObject camera = transform.GetChild(0).gameObject;
        transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y-0.9020001f, camera.transform.position.z);
    }
}
