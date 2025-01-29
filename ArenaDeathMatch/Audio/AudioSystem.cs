using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace ArenaDeathMatch.Audio
{
    public class AudioSystem : MonoBehaviour
    {
        public static AudioSystem Instance { get; private set; }

        [Header("Audio Configuration")]
        public AudioSettings audioSettings;
        public AudioMixerController mixerController;
        public SpatialAudioManager spatialAudio;
        public AudioEventManager eventManager;

        [Header("Sound Banks")]
        public WeaponSoundBank weaponSounds;
        public EnvironmentSoundBank environmentSounds;
        public UIAudioBank uiSounds;
        public MusicController musicController;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                InitializeAudioSystem();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #region Initialization

        private void InitializeAudioSystem()
        {
            mixerController.Initialize(audioSettings);
            spatialAudio.Initialize();
            eventManager.Initialize();
            LoadSoundBanks();
        }

        private void LoadSoundBanks()
        {
            weaponSounds.Initialize();
            environmentSounds.Initialize();
            uiSounds.Initialize();
            musicController.Initialize();
        }

        #endregion

        #region Audio Mixer Control

        public class AudioMixerController
        {
            public AudioMixer mainMixer;
            private Dictionary<string, float> mixerValues;

            public void Initialize(AudioSettings settings)
            {
                mixerValues = new Dictionary<string, float>();
                ApplySettings(settings);
            }

            public void SetVolume(string parameterName, float volume)
            {
                float decibelValue = ConvertToDecibels(volume);
                mainMixer.SetFloat(parameterName, decibelValue);
                mixerValues[parameterName] = volume;
            }

            private float ConvertToDecibels(float volume)
            {
                return volume > 0.0f ? 20f * Mathf.Log10(volume) : -80f;
            }

            public void ApplySettings(AudioSettings settings)
            {
                SetVolume("MasterVolume", settings.masterVolume);
                SetVolume("MusicVolume", settings.musicVolume);
                SetVolume("SFXVolume", settings.sfxVolume);
                SetVolume("UIVolume", settings.uiVolume);
            }
        }

        #endregion

        #region Spatial Audio

        public class SpatialAudioManager
        {
            private ObjectPool<AudioSource> sourcePool;
            private List<SpatialAudioEmitter> activeEmitters;
            private AudioListener mainListener;

            public void Initialize()
            {
                sourcePool = new ObjectPool<AudioSource>(CreateAudioSource, 20);
                activeEmitters = new List<SpatialAudioEmitter>();
                mainListener = Camera.main.GetComponent<AudioListener>();
            }

            public SpatialAudioEmitter CreateSpatialSound(AudioClip clip, Vector3 position, SpatialAudioSettings settings)
            {
                AudioSource source = sourcePool.Get();
                SpatialAudioEmitter emitter = new SpatialAudioEmitter(source, clip, settings);
                emitter.SetPosition(position);
                activeEmitters.Add(emitter);
                return emitter;
            }

            private AudioSource CreateAudioSource()
            {
                GameObject obj = new GameObject("SpatialAudioSource");
                AudioSource source = obj.AddComponent<AudioSource>();
                ConfigureAudioSource(source);
                return source;
            }

            private void ConfigureAudioSource(AudioSource source)
            {
                source.spatialBlend = 1f;
                source.rolloffMode = AudioRolloffMode.Custom;
                source.dopplerLevel = 1f;
                source.spread = 60f;
            }

            public class SpatialAudioEmitter
            {
                private AudioSource source;
                private SpatialAudioSettings settings;
                private Vector3 velocity;
                private Vector3 lastPosition;

                public SpatialAudioEmitter(AudioSource source, AudioClip clip, SpatialAudioSettings settings)
                {
                    this.source = source;
                    this.settings = settings;
                    ConfigureEmitter(clip);
                }

                private void ConfigureEmitter(AudioClip clip)
                {
                    source.clip = clip;
                    source.minDistance = settings.minDistance;
                    source.maxDistance = settings.maxDistance;
                    source.rolloffMode = settings.rolloffMode;
                    source.dopplerLevel = settings.dopplerLevel;
                }

                public void Update()
                {
                    UpdateVelocity();
                    ApplyDopplerEffect();
                }

                private void UpdateVelocity()
                {
                    velocity = (source.transform.position - lastPosition) / Time.deltaTime;
                    lastPosition = source.transform.position;
                }

                private void ApplyDopplerEffect()
                {
                    // Implementation for Doppler effect
                }
            }
        }

        #endregion

        #region Sound Banks

        public abstract class SoundBank : ScriptableObject
        {
            protected Dictionary<string, AudioClip> audioClips;
            protected AudioSettings settings;

            public virtual void Initialize()
            {
                audioClips = new Dictionary<string, AudioClip>();
                LoadAudioClips();
            }

            protected abstract void LoadAudioClips();

            public AudioClip GetClip(string soundID)
            {
                return audioClips.ContainsKey(soundID) ? audioClips[soundID] : null;
            }
        }

        [CreateAssetMenu(fileName = "WeaponSoundBank", menuName = "Audio/Weapon Sound Bank")]
        public class WeaponSoundBank : SoundBank
        {
            [System.Serializable]
            public class WeaponSoundSet
            {
                public string weaponID;
                public AudioClip fire;
                public AudioClip reload;
                public AudioClip empty;
                public AudioClip impact;
            }

            public List<WeaponSoundSet> weaponSounds;

            protected override void LoadAudioClips()
            {
                foreach (var soundSet in weaponSounds)
                {
                    audioClips[$"{soundSet.weaponID}_fire"] = soundSet.fire;
                    audioClips[$"{soundSet.weaponID}_reload"] = soundSet.reload;
                    audioClips[$"{soundSet.weaponID}_empty"] = soundSet.empty;
                    audioClips[$"{soundSet.weaponID}_impact"] = soundSet.impact;
                }
            }
        }

        #endregion

        #region Music System

        public class MusicController
        {
            private AudioSource musicSource;
            private MusicTrack currentTrack;
            private float crossfadeDuration = 2f;
            private Coroutine fadeCoroutine;

            [System.Serializable]
            public class MusicTrack
            {
                public string trackID;
                public AudioClip clip;
                public float volume = 1f;
                public bool loop = true;
                public float fadeInDuration = 2f;
                public float fadeOutDuration = 2f;
            }

            public void PlayTrack(string trackID, bool crossfade = true)
            {
                MusicTrack newTrack = GetTrack(trackID);
                if (newTrack == null) return;

                if (crossfade && currentTrack != null)
                {
                    StartCrossfade(newTrack);
                }
                else
                {
                    PlayTrackImmediate(newTrack);
                }
            }

            private void StartCrossfade(MusicTrack newTrack)
            {
                if (fadeCoroutine != null)
                    StopCoroutine(fadeCoroutine);

                fadeCoroutine = StartCoroutine(CrossfadeRoutine(newTrack));
            }

            private IEnumerator CrossfadeRoutine(MusicTrack newTrack)
            {
                AudioSource newSource = CreateAudioSource(newTrack);
                float elapsed = 0f;

                while (elapsed < crossfadeDuration)
                {
                    elapsed += Time.deltaTime;
                    float t = elapsed / crossfadeDuration;

                    musicSource.volume = Mathf.Lerp(currentTrack.volume, 0f, t);
                    newSource.volume = Mathf.Lerp(0f, newTrack.volume, t);

                    yield return null;
                }

                Destroy(musicSource.gameObject);
                musicSource = newSource;
                currentTrack = newTrack;
            }

            private AudioSource CreateAudioSource(MusicTrack track)
            {
                GameObject obj = new GameObject("MusicSource");
                AudioSource source = obj.AddComponent<AudioSource>();
                source.clip = track.clip;
                source.volume = track.volume;
                source.loop = track.loop;
                source.Play();
                return source;
            }

            private MusicTrack GetTrack(string trackID)
            {
                // Implementation to retrieve the music track by ID
                return null;
            }

            private void PlayTrackImmediate(MusicTrack track)
            {
                if (musicSource != null)
                {
                    Destroy(musicSource.gameObject);
                }
                musicSource = CreateAudioSource(track);
                currentTrack = track;
            }
        }

        #endregion

        #region Audio Event System

        public class AudioEventManager
        {
            private Dictionary<string, AudioEvent> audioEvents;

            public void Initialize()
            {
                audioEvents = new Dictionary<string, AudioEvent>();
                LoadAudioEvents();
            }

            public void TriggerEvent(string eventID, Vector3 position)
            {
                if (audioEvents.TryGetValue(eventID, out AudioEvent audioEvent))
                {
                    audioEvent.Play(position);
                }
            }

            [System.Serializable]
            public class AudioEvent
            {
                public string eventID;
                public AudioClip[] variations;
                public AudioEventSettings settings;

                public void Play(Vector3 position)
                {
                    AudioClip clip = GetRandomVariation();
                    if (clip == null) return;

                    AudioSource source = AudioSystem.Instance.spatialAudio.CreateSpatialSound(
                        clip, position, settings.spatialSettings).source;
                    
                    ApplySettings(source);
                }

                private AudioClip GetRandomVariation()
                {
                    if (variations == null || variations.Length == 0)
                        return null;

                    return variations[Random.Range(0, variations.Length)];
                }

                private void ApplySettings(AudioSource source)
                {
                    source.volume = settings.volume;
                    source.pitch = Random.Range(settings.pitchRange.x, settings.pitchRange.y);
                    source.loop = settings.loop;
                    source.Play();
                }
            }
        }

        #endregion

        #region Data Structures

        [System.Serializable]
        public class AudioSettings
        {
            public float masterVolume = 1f;
            public float musicVolume = 0.8f;
            public float sfxVolume = 1f;
            public float uiVolume = 1f;
            public bool enableSpatialAudio = true;
            public bool enableDoppler = true;
            public int maxConcurrentSounds = 32;
        }

        [System.Serializable]
        public class SpatialAudioSettings
        {
            public float minDistance = 1f;
            public float maxDistance = 50f;
            public AudioRolloffMode rolloffMode = AudioRolloffMode.Custom;
            public float dopplerLevel = 1f;
            public AnimationCurve customRolloff;
        }

        [System.Serializable]
        public class AudioEventSettings
        {
            public float volume = 1f;
            public Vector2 pitchRange = new Vector2(0.9f, 1.1f);
            public SpatialAudioSettings spatialSettings;
            public bool loop = false;
            public float fadeInDuration = 0f;
            public float fadeOutDuration = 0f;
        }

        #endregion
    }
}
