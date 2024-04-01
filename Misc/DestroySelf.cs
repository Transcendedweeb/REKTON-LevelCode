using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
