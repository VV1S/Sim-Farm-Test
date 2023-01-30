using UnityEngine;

public interface IPlayable
{
    public GameObject GameObject { get; set; }
    void Play();
    void Stop();
}