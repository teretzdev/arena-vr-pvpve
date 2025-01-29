using System;

namespace ArenaDeathMatch.Effects
{
    public class EffectsManager
    {
        private ParticleSystemManager particleSystemManager;
        private AnimationManager animationManager;
        private VFXManager vfxManager;
        private AudioManager audioManager;

        public EffectsManager()
        {
            particleSystemManager = new ParticleSystemManager();
            animationManager = new AnimationManager();
            vfxManager = new VFXManager();
            audioManager = new AudioManager();
        }

        public void UpdateEffects()
        {
            particleSystemManager.Update();
            animationManager.Update();
            vfxManager.Update();
            audioManager.Update();
        }
    }

    public class ParticleSystemManager
    {
        public void Update()
        {
            // Update particle systems
            Console.WriteLine("Updating particle systems.");
        }
    }

    public class AnimationManager
    {
        public void Update()
        {
            // Update animations
            Console.WriteLine("Updating animations.");
        }
    }

    public class VFXManager
    {
        public void Update()
        {
            // Update visual effects
            Console.WriteLine("Updating visual effects.");
        }
    }

    public class AudioManager
    {
        public void Update()
        {
            // Update audio effects
            Console.WriteLine("Updating audio effects.");
        }
    }
}
