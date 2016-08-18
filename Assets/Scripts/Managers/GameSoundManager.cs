using UnityEngine;
using System.Collections;

public class GameSoundManager : MonoBehaviour
{
    public GameManager gameManager;

    // Фоновая музыка
    public AudioSource musicSource;
    public AudioClip music;

    // Звуки
    public AudioSource soundSource;
    // Звук монетки
    public AudioClip soundCoin;

    // Воспроизвести музыку
    public void PlayMusic()
    {
        if(musicSource.clip == null)
        {
            musicSource.clip = music;
            musicSource.loop = true;
        }
        musicSource.Play();
    }

    // Пауза
    public void PauseMusic()
    {
        musicSource.Pause();
    }

    // Воспроизвести звук
    private void _PlaySound(AudioClip clip)
    {
        if(soundSource.isPlaying)
            soundSource.Pause();
        soundSource.PlayOneShot(clip);
    }

    // Звук монеты
    public void PlaySoundCoin()
    {
        if(soundCoin != null)
            _PlaySound(soundCoin);
    }

}
