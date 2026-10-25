using UnityEngine;

public class SnowTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem SnowParticles;
    void OnCollisionEnter2D(Collision2D collision)
    {
         int layerIndex=LayerMask.NameToLayer("Floor");


        if(collision.gameObject.layer == layerIndex)
        {
            SnowParticles.Play();
        }  
    }
    void OnCollisionExit2D(Collision2D collision)
    {
         int layerIndex=LayerMask.NameToLayer("Floor");


        if(collision.gameObject.layer == layerIndex)
        {
           SnowParticles.Stop();
        }  
    }
}
