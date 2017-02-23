using UnityEngine;
using System.Collections;

namespace com.AmazingFusion.LoveOrDeath
{
    public class AudioController : GlobalSingleton<AudioController>
    {
        #region Unity Editor Members

        [Header("Music")]
        [SerializeField]
        AudioClip _townMusicClip;

        /*[Header("UI General Sounds")]
        [SerializeField]
        AudioClip _optionSoundClip;

        [SerializeField]
        AudioClip _openSettingsSoundClip;

        [SerializeField]
        AudioClip _closeSettingsSoundClip;*/

        [Header("General")]
        [SerializeField]
        AudioClip _levelUpSoundClip;
        /*[SerializeField]
        AudioClip _experienceUpSoundClip;
        [SerializeField]
        AudioClip _softCurrencyEarnedSoundClip;
        [SerializeField]
        AudioClip _hardCurrencyEarnedSoundClip;*/

        [Header("Items production Sounds")]
        //[SerializeField]
        //AudioClip _addWorkerItemsProductionSoundClip;

        //[SerializeField]
        //AudioClip _removeWorkerItemsProductionSoundClip;

        [SerializeField]
        AudioClip _warningItemsProductionSoundClip;

        [SerializeField]
        AudioClip _collectItemsProductionSoundClip;

        [Header("Orders Board Sounds")]
        [SerializeField]
        AudioClip _openOrdersBoardSoundClip;

        [SerializeField]
        AudioClip _closeOrdersBoardSoundClip;

        [SerializeField]
        AudioClip _selectOrdersBoardSoundClip;

        [SerializeField]
        AudioClip _unselectOrdersBoardSoundClip;

        [SerializeField]
        AudioClip _completeOrdersBoardSoundClip;

        [SerializeField]
        AudioClip _recycleOrdersBoardSoundClip;

        [Header("Building Sounds")]
        [SerializeField]
        AudioClip _openBuildingPanelSoundClip;

        [SerializeField]
        AudioClip _closeBuildingPanelSoundClip;

        [SerializeField]
        AudioClip _selectTabBuildingPanelSoundClip;

        [SerializeField]
        AudioClip _warningBuildingPanelSoundClip;

        [SerializeField]
        AudioClip _startBuildingSoundClip;

        [Header("Editing Mode Sounds")]
        [SerializeField]
        AudioClip _startEditingModeSoundClip;

        [SerializeField]
        AudioClip _endEditingModeSoundClip;

        #endregion;

        #region Private Members

        AudioSource _musicSource;
        AudioSource _uiFxSource;
        AudioSource _gameFxSource;

        #endregion

        protected override void Awake()
        {
            base.Awake();

            _musicSource = gameObject.AddComponent<AudioSource>();
            _musicSource.loop = true;

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

        public void PlayVillageGameMusic()
        {
            PlayMusic(_townMusicClip);
        }

        #endregion

        #region UI Sounds

        #region UI General Sounds
        /*public void PlayOptionSound()
        {
            PlayUISound(_optionSoundClip);
        }

        public void PlayOpenSettingsSound()
        {
            PlayUISound(_openSettingsSoundClip);
        }

        public void PlayCloseSettingsSound()
        {
            PlayUISound(_closeSettingsSoundClip);
        }*/

        public void PlayLevelUpSound()
        {
            PlayUISound(_levelUpSoundClip);
        }

        /*public void PlayExperienceUpSound()
        {
            PlayUISound(_experienceUpSoundClip);
        }

        public void PlaySoftCurrencyEarnedSound()
        {
            PlayUISound(_softCurrencyEarnedSoundClip);
        }

        public void PlayHardCurrencyEarnedSound()
        {
            PlayUISound(_hardCurrencyEarnedSoundClip);
        }*/

        #endregion

        #region Items Production Sounds

        //public void PlayAddWorkerItemsProductionSound()
        //{
        //    PlayUISound(_addWorkerItemsProductionSoundClip);
        //}

        //public void PlayRemoveWorkerItemsProductionSound()
        //{
        //    PlayUISound(_removeWorkerItemsProductionSoundClip);
        //}

        public void PlayWarningItemsProductionSound()
        {
            PlayUISound(_warningItemsProductionSoundClip);
        }

        public void PlayCollectItemsProductionSound()
        {
            PlayUISound(_collectItemsProductionSoundClip);
        }

        #endregion

        #region Orders Board Sounds

        public void PlayOpenOrdersBoardSound()
        {
            PlayUISound(_openOrdersBoardSoundClip);
        }

        public void PlayCloseOrdersBoardSound()
        {
            PlayUISound(_closeOrdersBoardSoundClip);
        }

        public void PlaySelectOrdersBoardSound()
        {
            PlayUISound(_selectOrdersBoardSoundClip);
        }

        public void PlayUnselectOrdersBoardSound()
        {
            PlayUISound(_unselectOrdersBoardSoundClip);
        }

        public void PlayCompleteOrdersBoardSound()
        {
            PlayUISound(_completeOrdersBoardSoundClip);
        }

        public void PlayRecycleOrdersBoardSound()
        {
            PlayUISound(_recycleOrdersBoardSoundClip);
        }

        #endregion

        #region Editing Mode

        public void PlayStartEditingModeSound()
        {
            PlayUISound(_startEditingModeSoundClip);
        }

        public void PlayEndEditingModeSound()
        {
            PlayUISound(_endEditingModeSoundClip);
        }

        #endregion

        #region Building Panel Sounds

        public void PlayOpenBuildingPanelSound()
        {
            PlayUISound(_openBuildingPanelSoundClip);
        }

        public void PlayCloseBuildingPanelSound()
        {
            PlayUISound(_closeBuildingPanelSoundClip);
        }

        public void PlaySelectTabBuildingPanelSound()
        {
            PlayUISound(_selectTabBuildingPanelSoundClip);
        }

        public void PlayWarningBuildingPanelSound()
        {
            PlayUISound(_warningBuildingPanelSoundClip);
        }

        public void PlayStartBuildingSound()
        {
            PlayUISound(_startBuildingSoundClip);
        }

        #endregion

        #endregion

        #region Game Sounds


        #endregion
    }
}