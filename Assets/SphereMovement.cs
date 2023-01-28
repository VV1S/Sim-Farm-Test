using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour, IMoveable
{
    private float timeCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
    }

    public void Move()
    {
        
    }

    public void Stop()
    {
        
    }
}

public interface IMoveable
{
    void Move();
    void Stop();
}
