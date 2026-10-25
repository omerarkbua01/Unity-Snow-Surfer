using Unity.Mathematics;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 8f;
    [SerializeField] float baseSpeed = 30f;
    [SerializeField] float boostSpeed = 42f;
    [SerializeField] ParticleSystem powerupParticles;
    [SerializeField] ScoreManager scoreManager;
    int activepowerupParticles;
    InputAction moveAction;
    Rigidbody2D _rb;
    SurfaceEffector2D surfaceEffector2D;
    Vector2 moveVector;
    float previusRotation;
    float totalRotatiom;
    bool canController = true;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        _rb=GetComponent<Rigidbody2D>();
        surfaceEffector2D=FindFirstObjectByType<SurfaceEffector2D>();
    }

    void Update()
    {
        if(canController)
        {
            RotatePlayer();
            BoostPlayer();   
            CalculateFlips();
        }
    }
  

    void RotatePlayer()
    {
        moveVector=moveAction.ReadValue<Vector2>();
        if (moveVector.x < 0)
        {
            _rb.AddTorque(torqueAmount);
        }
        else if (moveVector.x > 0)
        {
            _rb.AddTorque(-torqueAmount);
        }
    }

    void BoostPlayer()
    {
        if(moveVector.y > 0)
        {
            surfaceEffector2D.speed =boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed=baseSpeed;
        }
    }

    void CalculateFlips()
    {
        float currentRotation = transform.rotation.eulerAngles.z;
        totalRotatiom +=Mathf.DeltaAngle(previusRotation,currentRotation);
        if(totalRotatiom > 350 || totalRotatiom < -350)
        {
            totalRotatiom =0;
            scoreManager.AddScore(20);
        }
        previusRotation=currentRotation;
    }

    public void DisableControl()
    {
        canController = false;
    }
    public void ActivatePowerup(PowerupSO powerup)
    {
        powerupParticles.Play();
        activepowerupParticles +=1;
        if (powerup.GetPowerupType() == "speed")
        {
            baseSpeed +=powerup.GetvalueChange();
            boostSpeed +=powerup.GetvalueChange();
        }
         else if (powerup.GetPowerupType() == "torque")
        {
            torqueAmount +=powerup.GetvalueChange();
        }
    }

       public void DeactivatePowerup(PowerupSO powerup)
    {
        activepowerupParticles -=1;
        if(activepowerupParticles == 0)
        {
            powerupParticles.Stop();
        }
        if (powerup.GetPowerupType() == "speed")
        {
            baseSpeed -=powerup.GetvalueChange();
            boostSpeed -=powerup.GetvalueChange();
        }
        else if (powerup.GetPowerupType() == "torque")
        {
            torqueAmount -=powerup.GetvalueChange();
        }
    }
}
