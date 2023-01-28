using System.Collections.Generic;

namespace Controls
{
    public class PlayCommandPublisher
    {
        private static List<IPlayable> subscribers = new List<IPlayable>();

        public void Subscribe(IPlayable playable)
        {
            subscribers.Add(playable);
        }

        public void PlaySubscribers()
        {
            foreach (var playable in subscribers)
            {
                playable.Play();
            }
        }

        public void StopSubscribers()
        {
            foreach (var playable in subscribers)
            {
                playable.Stop();
            }
        }
    }
}