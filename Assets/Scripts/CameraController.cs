using UnityEngine;
using System.Collections;
#region Delegates
#endregion
public class CameraController : MonoBehaviour {

	#region Private References      
    [SerializeField, Range(0.0f, 1.0f)]
    private float _lerpRate;
    private float _xRotation;
    private float _yYRotation;

	#endregion

	#region Private Methods
    private void Rotate(float xMovement, float yMovement)
    {
        _xRotation += xMovement;
        _yYRotation += yMovement;
    }
	#endregion

	#region Unity CallBacks
	
	void Start ()
	{
        InputManager.MouseMoved += Rotate;
	}
	
	void Update ()
	{
        _xRotation = Mathf.Lerp(_xRotation, 0, _lerpRate);
        _yYRotation = Mathf.Lerp(_yYRotation, 0, _lerpRate);
        transform.eulerAngles += new Vector3(0, _xRotation, -_yYRotation);

	}

   void OnDestroy()
   {
       InputManager.MouseMoved += Rotate;    
   }
	#endregion
}
