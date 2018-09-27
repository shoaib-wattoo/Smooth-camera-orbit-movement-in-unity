### Objective
Main objective of this blog post is to give you an idea about Smooth Camera Orbit Movement in Unity.

## Step 1 : Orbit Movement
**What is Orbit Movement?**

Camera is the most important part of a game.
Camera provides visuals of objects of a game to the user.

For better presentation, give some functionality to the player of the game. To give visual effect, camera positioning movement is very essential.

One of the Camera movements is the orbit movement in which the camera orbits around the game object.

## Step 2 : Need of Camera Orbit Movement

**What is the need of camera orbit movement?**

There is some essential functionality, which needs camera orbit movement.

E.g. Let say in a car racing game, player is allowed to choose a car. When the player goes to a car house to choose a car at that time for showing the car from all the sides putting multiple cameras is not a very good idea of presentation. Here camera orbit movement gives wow effect. Camera orbits around the car and keeps an eye on the car for the user input.

Second can be the rotation of planets around the sun.

There are lots of effects and functionalities where camera orbit movement is must.

But, we will consider the above example for simple orbit movement.

## Step 3 : Implementation

### 3.1 Basic Steps

#### How to orbit camera around the Game Object?
- Given below is a different approach which is used to orbit the camera around the game object.
- Take an object (object in center) around which the camera needs to orbit.
- Create an empty game object.
- Place it at the same place where the object is placed in the center.
- Set Camera position as required.
- Make camera child of the newly created empty game object in step 2.
- Now on input from the user (e.g. Mouse drag) rotate the parent object of the camera.

Now let’s take a working example.

### 3.2 Working Example

Here in this script Mouse moved event is used to delegate the action to Camera controller. Movement of the mouse in horizontal and vertical directions is taken by using axis and is passed to the Cameracontroller. Make Camera Controller script. Assign it to the parent object of the camera.

- Place a car model.
If you don’t have it, then you can download it from the internet.
Otherwise download full project from here.
- Set camera position according to your need.

![](http://www.theappguruz.com/app/uploads/2015/06/camera-position-accroding-to-your-need.png)

- Now, make an empty game object and move it at the position of the car.
- Make Camer child of the empty game object.

![](http://www.theappguruz.com/app/uploads/2015/06/8177hierarchy.png)

### 3.3 Code Sample

Finally, set up is done so now make the scripts. Make InputManager script to take input and invoke action input. Make a new empty game object and name it InputManager and assign this script to the inputmanager gameobject.

```csharp
public delegate void MouseMoved(float xMovement, float yMovement);
public class InputManager : MonoBehaviour
{
    #region Private References
    private float _xMovement;
    private float _yMovement;
    #endregion
 
    #region Events
    public static event MouseMoved MouseMoved;
    #endregion
 
    #region Event Invoker Methods
    private static void OnMouseMoved(float xmovement, float ymovement)
    {
        var handler = MouseMoved;
        if (handler != null) handler(xmovement, ymovement);
    }
    #endregion
 
 
    #region Private Methods
    private void InvokeActionOnInput()
    {
        if (Input.GetMouseButton(0))
        {
            _xMovement = Input.GetAxis("Mouse X");
            _yMovement = Input.GetAxis("Mouse Y");
            OnMouseMoved(_xMovement, _yMovement);
        }
    }
    #endregion
 
    #region Unity CallBacks
 
    void Update()
    {
        InvokeActionOnInput();
    }
    #endregion
}
```

Here in this script Mouse moved event is used to delegate the action to Camera controller.

Movement of the mouse in horizontal and vertical directions is taken by using axis and is passed to the Cameracontroller.

Make camera child of the empty game object.

```csharp
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
       InputManager.MouseMoved -= Rotate;    
   }
#endregion
}
```
Here in start (), mouse moved event is registered with Rotate ().

In Rotate (), movement of the mouse in horizontal and vertical directions is added to the delta rotation containers.

In Update (), delta rotations are used to rotate the camera parent by adding toEuler angles. Delta rotations are damped using lerp for smoother effect after you leave the mouse button.

Now, you are done for the camera orbit movement around the car.

- To set Euler angles see gizmos of camera parent object. Decide around which axis you want to move the camera.Here in our case camera parent gizmos shows forward which is in the direction of car’s forward and up is in y – direction. So for horizontal movement of the mouse we need to rotate the camera around y-axis and for vertical movement we need to rotate the camera around z- axis. You can set it as per your need.One can restrict camera rotation by clamping Euler angles or delta rotation value wherever needed.E.g. Let say in this case camera should not go below ground level.

### 3.4 What actually we have done ?

Here, actually camera parent is rotated and accordingly the camera rotates.

That is advantageous because it avoids possibilities of gimbal lock.

Camera automatically keeps an eye on the car. No need to translate or rotate camera. Just one line code.

In short Camera orbits movement, without rotating the camera!!!!
