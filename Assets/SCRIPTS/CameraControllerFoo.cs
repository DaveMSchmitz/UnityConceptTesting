using UnityEngine;

public class CameraControllerFoo : MonoBehaviour
{

    public GameObject focus;
    public float cameraBuffer;
    public float smoothness;
    public float clampYpos;

    private Vector3 focusPosition;
    

	// Use this for initialization
	void Start () {
//		playerControllerFoo = focus.GetComponent<PlayerControllerFoo>();
        
    }
	
	// Update is called once per frame
	void Update ()
	{

	    float x = focus.transform.position.x + Mathf.Sign(focus.transform.localScale.x) * cameraBuffer;
	    float y = Mathf.Clamp(focus.transform.position.y, clampYpos, float.MaxValue);
	    float z = transform.position.z;

//        transform.position = new Vector3(transform.position.x, y, transform.position.z);

        focusPosition = new Vector3(x,y,z);
	    transform.position = Vector3.Lerp(transform.position, focusPosition, smoothness * Time.deltaTime);

    }
}
