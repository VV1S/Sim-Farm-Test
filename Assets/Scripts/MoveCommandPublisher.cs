using System.Collections.Generic;

public class MoveCommandPublisher
{
    private static List<IMoveable> subscribers = new List<IMoveable>();

    public void Subscribe(IMoveable moveable)
    {
        subscribers.Add(moveable);
    }

    public void MoveSubscribers()
    {
        foreach (var moveable in subscribers)
        {
            moveable.Move();
        }
    }

    public void StopSubscribers()
    {
        foreach (var moveable in subscribers)
        {
            moveable.Stop();
        }
    }
}