using UnityEngine;

namespace Sphere
{
    public class ParticleEffectFactory
    {
        public ParticleSystem Create(ParticleSystem particleEffect, Vector3 position)
        {
            var newParticleEffect = UnityEngine.Object.Instantiate(particleEffect);
            newParticleEffect.transform.position = position;
            return newParticleEffect;
        }
    }
}