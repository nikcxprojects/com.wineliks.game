using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get => FindObjectOfType<SFXManager>(); }

    [SerializeField] AudioSource source;

    [Space(10)]
    [SerializeField] AudioClip win;
    [SerializeField] AudioClip lose;

    public void PlayEffect(int id)
    {
        source.Stop();
        source.PlayOneShot(id switch
        {
            0 => lose, 1 => win
        });
    }
}
