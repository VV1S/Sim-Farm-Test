using System.Collections.Generic;

namespace Controls
{
    public class PlayCommandPublisher
    {
        private static Dictionary<int, List<IPlayable>> newSubscribers = new Dictionary<int , List<IPlayable>>();

        public void Subscribe(int id, IPlayable playable)
        {
            if (newSubscribers.ContainsKey(id))
                newSubscribers[id].Add(playable);
            else
                newSubscribers.Add(id, new List<IPlayable>(){playable});
        }

        public void Unsubscribe(int id)
        {
            newSubscribers.Remove(id);
        }

        public void PlaySubscribers()
        {
            foreach (var playables in newSubscribers.Values)
            {
                foreach (var playable in playables)
                {
                    playable.Play();
                }
            }
        }

        public void StopSubscribers()
        {
            foreach (var playables in newSubscribers.Values)
            {
                foreach (var playable in playables)
                {
                    playable.Stop();
                }
            }
        }
    }
}