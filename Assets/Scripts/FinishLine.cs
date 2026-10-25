using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] ParticleSystem finishParticles;
    void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Player");

        if(collision.gameObject.layer == layerIndex)
        {
            finishParticles.Play();
            Invoke("ReloadScene",0.5f);
        }
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(0);

    }
}
