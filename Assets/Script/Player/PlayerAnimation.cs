using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerController playerController;
    private PlayerCharacter playerCharacter;

    private AudioSource audioSource;
    public AudioClip walkAudio, hitAudio, dieAudio;
    bool isPlayingWalkAudio = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        playerCharacter = GetComponent<PlayerCharacter>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        SetAnimator();
    }

    private void SetAnimator()
    {
        anim.SetBool("IsRunning", playerController.isRunning);
        anim.SetBool("Invulnerable", playerCharacter.Invulnerable);

        if (!isPlayingWalkAudio && playerController.isRunning)
        {
            audioSource.clip = walkAudio;
            audioSource.Play();
            isPlayingWalkAudio = true;
        }
        else if (isPlayingWalkAudio && !playerController.isRunning)
        {
            audioSource.Pause();
            isPlayingWalkAudio = false;
        }
    }
    public void Anim_Hit()
    {
        anim.SetTrigger("Hit");
        audioSource.PlayOneShot(hitAudio);
    }
    public void Anim_Die()
    {
        anim.SetBool("Die", true);
        audioSource.PlayOneShot(dieAudio);
        audioSource.Stop();
    }
    public void Set_Invulnerable(bool val)
    {
        anim.SetBool("Invulnerable", val);
    }
    public void Set_Stunned(bool val)
    {
        anim.SetBool("Stunned", val);
    }

}
