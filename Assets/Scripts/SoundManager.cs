using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;

    public AudioClip backgroundMusic;

    public AudioClip jumpSound;
    public AudioClip pickSound;
    public AudioClip dieSound;
    public AudioClip walkSound;
    public AudioClip achieveSound;
    public AudioClip respawnSound;
    public AudioClip movingRockSound;

    private bool isJumpSoundPlaying;
    private bool isPickSoundPlaying;
    private bool isDieSoundPlaying;
    private bool isWalkSoundPlaying;
    private bool isAchieveSoundPlaying;
    private bool isRespawnSoundPlaying;
    private bool isMovingRockSoundPlaying;

    public AudioSource musicSource;
    public AudioSource soundEffectSource;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        musicSource = gameObject.AddComponent<AudioSource>();
        soundEffectSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start() {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic() {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayJumpSound() {
        if (!isJumpSoundPlaying) {
            isJumpSoundPlaying =true;
            soundEffectSource.clip = jumpSound;
            soundEffectSource.Play();
        }
    }

    public void PlayPickSound() {
        if (!isPickSoundPlaying) {
            isPickSoundPlaying = true;
            soundEffectSource.clip = pickSound;
            
            soundEffectSource.Play();
        }
    }

    public void PlayDieSound() {
        if (!isDieSoundPlaying) {
            isDieSoundPlaying = true;
            soundEffectSource.PlayOneShot(dieSound);
        }
    }

    public void PlayAchieveSound() {
        if (!isAchieveSoundPlaying) {
            isAchieveSoundPlaying = true;
            soundEffectSource.PlayOneShot(achieveSound);
        }
    }

    public void PlayWalkingSound() {
        if (!isWalkSoundPlaying) {
            isWalkSoundPlaying = true;
            soundEffectSource.loop = true;
            soundEffectSource.PlayOneShot(walkSound);
        }
    }

    public void PlayRespawnSound() {
        if (!isRespawnSoundPlaying) {
            isRespawnSoundPlaying = true;
            soundEffectSource.PlayOneShot(respawnSound);
        }
    }

    public void PlayMovingRockSound() {
        if (!isMovingRockSoundPlaying) {
            isMovingRockSoundPlaying = true;
            soundEffectSource.loop = true;
            soundEffectSource.PlayOneShot(movingRockSound);
        }
    }

    public void StopFXSound() {
        isAchieveSoundPlaying = isDieSoundPlaying =
        isJumpSoundPlaying = isMovingRockSoundPlaying =
        isPickSoundPlaying = isRespawnSoundPlaying = isWalkSoundPlaying = false;
        soundEffectSource.Stop();
    }
}