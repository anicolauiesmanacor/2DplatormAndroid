using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;

    private GameManager gManager;
    public PlayerController pController;

    public AudioClip backgroundMusicIntro;
    public AudioClip[] musicTracks;
    public AudioClip backgroundMusicEnd;

    public AudioClip jumpSound;
    public AudioClip pickSound;
    public AudioClip dieSound;
    public AudioClip walkSound;
    public AudioClip achieveSound;
    public AudioClip respawnSound;
    public AudioClip movingRockSound;

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
        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update() {
        if (!gManager.gameOver) {
            if (!gManager.startGame) {
                if (!musicSource.isPlaying) {
                    musicSource.loop = false;
                    musicSource.clip = backgroundMusicIntro;
                    musicSource.Play();
                }
            } else {
                musicSource.loop = false;
                if (!musicSource.isPlaying) {
                    int r = Random.Range(0, musicTracks.Length);
                    musicSource.clip = musicTracks[r];
                    musicSource.Play();
                }
            }
        } else {
            musicSource.loop = false;
            musicSource.clip = backgroundMusicEnd;
            musicSource.Play();
        }
    }

    public void PlayJumpSound() {
        soundEffectSource.loop = false;
        soundEffectSource.clip = jumpSound;
        soundEffectSource.Play();
        StartCoroutine(EffectCooldown(jumpSound.length));
    }

    public void PlayPickSound() {
        soundEffectSource.clip = pickSound;
        soundEffectSource.loop = false;
        soundEffectSource.Play();
        StartCoroutine(EffectCooldown(pickSound.length));
    }

    public void PlayDieSound() {
        soundEffectSource.loop = false;
        soundEffectSource.clip = dieSound;
        soundEffectSource.Play();
        StartCoroutine(EffectCooldown(dieSound.length));
    }

    private System.Collections.IEnumerator EffectCooldown(float duration) {
        yield return new WaitForSeconds(duration);
    }

    public void PlayAchieveSound() {
        soundEffectSource.loop = false;
        soundEffectSource.PlayOneShot(achieveSound);
    }

    public void PlayWalkingSound() {
        soundEffectSource.loop = true;
        soundEffectSource.clip = walkSound;
        soundEffectSource.Play();

    }

    public void PlayRespawnSound() {
        soundEffectSource.loop = false;
        soundEffectSource.PlayOneShot(respawnSound);
    }

    public void PlayMovingRockSound() {
        soundEffectSource.loop = true;
        soundEffectSource.PlayOneShot(movingRockSound);
    }

    public void StopFXSound() {
        //soundEffectSource.Stop();
        soundEffectSource.loop = false;
    }
}