using UnityEngine;
using UnityEngine.UI;

public class ClickerSound : MonoBehaviour
{
    public AudioSource sfxSource;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlayPop);
    }

    void PlayPop()
    {
        sfxSource.PlayOneShot(sfxSource.clip);
    }
}
