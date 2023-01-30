using System.Collections.Generic;

namespace Controls
{
    public class PlayCommandPublisher
    {
        private static Dictionary<int, List<IPlayable>> newSubscribers = new Dictionary<int , List<IPlayable>>();

        public void Subscribe(IPlayable playable)
        {
            if (newSubscribers.ContainsKey(playable.GameObject.GetInstanceID()))
                newSubscribers[playable.GameObject.GetInstanceID()].Add(playable);
            else
                newSubscribers.Add(playable.GameObject.GetInstanceID(), new List<IPlayable>(){playable});
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