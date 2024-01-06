using UnityEngine;
using System.Collections;

public class RandomSoundPlayer : MonoBehaviour
{
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    public AudioClip sound4;
    public AudioClip sound5;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayRandomSounds());
    }

    IEnumerator PlayRandomSounds()
    {
        while (true)
        {
           // float delay = Random.Range(30f, 45f); // Random delay between 30 seconds and 5 minutes
           // yield return new WaitForSeconds(delay);

            int clipNumber = Random.Range(1, 6); // Randomly pick a number between 1 and 5
            Debug.Log("AMBIENCT SOND PLAYING"+clipNumber);
            switch (clipNumber)
            {
                case 1:
                    audioSource.PlayOneShot(sound1);
                    yield return new WaitForSeconds(15f); // Wait for 1 minute for sound1
                    break;
                case 2:
                    audioSource.PlayOneShot(sound2);
                    yield return new WaitForSeconds(20f);
                    break;
                case 3:
                    audioSource.PlayOneShot(sound3);
                    yield return new WaitForSeconds(25f);
                    break;
                case 4:
                    audioSource.PlayOneShot(sound4);
                    yield return new WaitForSeconds(30f);
                    break;
                case 5:
                    audioSource.PlayOneShot(sound5);
                    yield return new WaitForSeconds(40f); // Wait for 5 minutes for sound5
                    break;
            }
        }
    }
}
