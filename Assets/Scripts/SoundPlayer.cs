using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] _audioSources = new AudioSource[2];
    [SerializeField]
    private AudioClip _introClip;
    [SerializeField]
    private AudioClip _mainLoopClip;

    private int _activeSourceIndex;
    private double _nextStartTime;

    private void Start()
    {
        PlayIntroAndScheduleLoop();
    }

    private void PlayIntroAndScheduleLoop()
    {
        _activeSourceIndex = 0;
        _audioSources[_activeSourceIndex].clip = _introClip;
        _audioSources[_activeSourceIndex].Play();
        double introDuration = (double)_introClip.samples / _introClip.frequency;
        _nextStartTime = AudioSettings.dspTime + introDuration;

        int nextSourceIndex = 1 - _activeSourceIndex;
        _audioSources[nextSourceIndex].clip = _mainLoopClip;
        _audioSources[nextSourceIndex].loop = true;
        _audioSources[nextSourceIndex].PlayScheduled(_nextStartTime);
    }
}