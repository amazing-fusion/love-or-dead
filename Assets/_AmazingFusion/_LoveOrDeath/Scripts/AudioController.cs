using UnityEngine;
using System.Collections;

namespace com.AmazingFusion.LoveOrDeath
{
    public class AudioController : GlobalSingleton<AudioController>
    {
        #region Unity Editor Members

        [Header("Music")]
        [SerializeField]
        AudioClip _combatMusicClip;

        [SerializeField]
        AudioClip _victoryMusicClip;

        [SerializeField]
        AudioClip _menuMusicClip;

        [SerializeField]
        AudioClip _defeatMusicClip;

        [Header("Sonido")]
        [SerializeField]
        AudioClip _playerTalkingSoundClip;

        [SerializeField]
        AudioClip _rivalLovingSoundClip;

        [SerializeField]
        AudioClip _rivalHitSoundClip;

        [SerializeField]
        AudioClip _rivalKissLoseSoundClip;

        [SerializeField]
        AudioClip _rivalKissWinSoundClip;

        [SerializeField]
        AudioClip _publicEuphoricSoundClip;

        [SerializeField]
        AudioClip _publicIdleSoundClip;

        [SerializeField]
        AudioClip _playerDiyingSoundClip;

        [Header("Sonido UI")] 

        [SerializeField]
        AudioClip _beginDragSoundClip;

        [SerializeField]
        AudioClip _dropSoundClip;

        [SerializeField]
        AudioClip _ultimateReadySoundClip;

        [SerializeField]
        AudioClip _kissSoundClip;

        [SerializeField]
        AudioClip _cardWinSoundClip;



        #endregion;

        #region Private Members

        AudioSource _musicSource;
        AudioSource _uiFxSource;
        AudioSource _gameFxSource;
        AudioSource _musicPublic;

        #endregion

        protected override void Awake()
        {
            base.Awake();

            _musicSource = gameObject.AddComponent<AudioSource>();
            _musicSource.loop = true;

            _musicPublic = gameObject.AddComponent<AudioSource>();
            _musicPublic.loop = true;

            _uiFxSource = gameObject.AddComponent<AudioSource>();
            _gameFxSource = gameObject.AddComponent<AudioSource>();

            //MusicOn(SettingsManager.Instance.MusicOn);
            //SoundOn(SettingsManager.Instance.SoundOn);
        }

        public void MusicOn(bool on)
        {
            _musicSource.volume = on ? 1 : 0;
        }

        public void SoundOn(bool on)
        {
            _uiFxSource.volume = on ? 1 : 0;
            _gameFxSource.volume = on ? 1 : 0;
        }

        public void PlayMusic(AudioClip music)
        {
            _musicSource.Stop();
            _musicSource.clip = music;
            _musicSource.Play();
        }

        public void PlayPublicMusic(AudioClip music)
        {
            _musicPublic.Stop();
            _musicPublic.clip = music;
            _musicPublic.Play();
        }

        public void PlayUISound(AudioClip sound)
        {
            if (sound == null)
            {
                Debug.LogWarning("Sound does not exists");
                return;
            }
            Debug.Log("Play sound: " + sound.name);
            _uiFxSource.Stop();
            _uiFxSource.clip = sound;
            _uiFxSource.Play();
            
        }

        public void PlayGameSound(AudioClip sound)
        {
            _gameFxSource.Stop();
            _gameFxSource.clip = sound;
            _gameFxSource.Play();
        }

        #region Music

        public void PlayCombatMusic()
        {
			_musicSource.loop = true;
            PlayMusic(_combatMusicClip);
        }

        public void PlayVictoryMusic()
        {
			_musicSource.loop = false;
            PlayMusic(_victoryMusicClip);
        }

        public void PlayDefeatMusic()
        {
			_musicSource.loop = false;
            PlayMusic(_defeatMusicClip);
        }

        public void PlayMenuMusic()
        {
            PlayPublicMusic(_publicIdleSoundClip);
        }

        #endregion

        #region UI Sounds
        
        public void PlayBeginDragUISound()
        {
            PlayUISound(_beginDragSoundClip);
        }

        public void PlayDropUISound()
        {
            PlayUISound(_dropSoundClip);
        }

        public void PlayUltimateReadyUISound()
        {
            PlayUISound(_ultimateReadySoundClip);
        }

        public void PlayKissUISound()
        {
            PlayUISound(_kissSoundClip);
        }

        public void PlayCardWinUISound()
        {
            PlayUISound(_cardWinSoundClip);
        }

        #endregion

        #region Game Sounds

        public void PlayPlayerTalkSound()
        {
            PlayGameSound(_playerTalkingSoundClip);
        }

        public void PlayRivalLovingSound()
        {
            PlayGameSound(_rivalLovingSoundClip);
        }

        public void PlayRivalHitSound()
        {
            PlayGameSound(_rivalHitSoundClip);
        }

        public void PlayRivalKissLoseSound()
        {
            PlayGameSound(_rivalKissLoseSoundClip);
        }

        public void PlayRivalKissWinSound()
        {
            PlayGameSound(_rivalKissWinSoundClip);
        }

        public void PlayPublicEuphoricSound()
        {
            PlayGameSound(_publicEuphoricSoundClip);
        }

        public void PlayPublicIdleSound()
        {
            PlayGameSound(_publicIdleSoundClip);
        }

        public void PlayPlayerDiyingSound()
        {
            PlayGameSound(_playerDiyingSoundClip);
        }

        #endregion
    }
}