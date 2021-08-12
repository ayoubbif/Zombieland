using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    AudioSource zombieAudioSource;
    [SerializeField] AudioClip[] zombieAttackClips;

void Start()
{
    zombieAudioSource = GetComponent<AudioSource>();
}
    public void PlayZombieAttack()
    {
        if (zombieAttackClips.Length == 0)
        {
            return;
        }
        float currentTime = Time.time;

        if (!zombieAudioSource.isPlaying)
        {
            System.Random random = new System.Random();
            int randomClipIndex = random.Next(0, zombieAttackClips.Length);
            zombieAudioSource.clip = zombieAttackClips[randomClipIndex];
            zombieAudioSource.PlayOneShot(zombieAttackClips[randomClipIndex]);
        }
    }
}

