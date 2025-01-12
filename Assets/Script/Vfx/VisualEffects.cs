using UnityEngine;

public class VisualEffects : MonoBehaviour
{

    [Header("Audio Effects")]
    private AudioSource audioSource;
    public AudioClip audioClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(audioClip);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimationFinished()
    {
        Destroy(this.gameObject);
    }
}
