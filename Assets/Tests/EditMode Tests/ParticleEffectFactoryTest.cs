using NUnit.Framework;
using UnityEngine;

namespace Sphere.Tests
{
    public class ParticleEffectFactoryTests
    {
        private ParticleSystem particleEffect;
        private ParticleEffectFactory particleEffectFactory;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            particleEffect = Resources.Load<ParticleSystem>("Particle Effects/Fireworks");
            particleEffectFactory = new ParticleEffectFactory();
        }

        [Test]
        public void Create_Given_ParticleEffect_And_Position_Should_Instantiate_ParticleEffect()
        {
            // Arrange
            var position = Vector3.one;

            // Act
            var newParticleEffect = particleEffectFactory.Create(particleEffect, position);

            // Assert
            Assert.AreNotSame(particleEffect, newParticleEffect);
            Assert.AreEqual(position, newParticleEffect.transform.position);
        }
    }
}