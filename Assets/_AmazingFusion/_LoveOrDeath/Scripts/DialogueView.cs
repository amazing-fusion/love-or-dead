using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SmartLocalization;

namespace com.AmazingFusion.LoveOrDeath {
    public class DialogueView : Singleton<DialogueView> {

        [SerializeField]
        EasingAnimation _showAnimation;

        [SerializeField]
        EasingAnimation _closeAnimation;

        [SerializeField]
        TMP_Text _text;

        [SerializeField]
        CharacterAnimator _animatorController;

        CharacterAction _characterAction;
        CharacterAction.ActionResult _result;

		Vector3 _startPosition;

        List<string> _ultimateSpeech;
        List<string> _noneSpeech;
        List<string> _winSpeech;
        List<string> _loseSpeech;
        List<string> _bothSpeech;

		public Vector3 StartPosition{

			get{ 
				return _startPosition;
			
			}
		}

        public event System.Action OnClose;

        void Start() {

            _ultimateSpeech = LanguageManager.Instance.GetKeysWithinCategory("Speech.Ultimate.");
            _noneSpeech = LanguageManager.Instance.GetKeysWithinCategory("Speech.None.");
            _winSpeech = LanguageManager.Instance.GetKeysWithinCategory("Speech.Win.");
            _loseSpeech = LanguageManager.Instance.GetKeysWithinCategory("Speech.Lose.");
            _bothSpeech = LanguageManager.Instance.GetKeysWithinCategory("Speech.Both.");

			_startPosition = Transform.localPosition;

            _showAnimation.OnEnd += (IEffectable effect) => { AnimationsAndSounds(); };   

            CombatController.Instance.OnCombatActionsResolved += (CharacterAction action, CharacterAction.ActionResult result) => {
                _result = result;
                _characterAction = action;
                SetText(action, result);
                EffectsManager.Instance.AddEffect(_showAnimation);
            };

			CombatController.Instance.OnCombatStart += () => {InizialicePosition(); };
        }

		public void InizialicePosition(){
		
			Transform.localPosition = _startPosition;
		}

        public void AnimationsAndSounds()
        {
            switch (_result)
            {
                case CharacterAction.ActionResult.None:
                    _animatorController.PlayNoneDamageAnimation();
                    break;
                case CharacterAction.ActionResult.Win:
                    if (_characterAction.Type == CharacterAction.ActionType.Ultimate)
                    {
                        AudioController.Instance.PlayRivalLovingSound();
                    }
                    else
                    {
                        AudioController.Instance.PlayRivalLovingSound();
                    }
                    _animatorController.PlayRivalLovingAnimation();
                    break;
                case CharacterAction.ActionResult.Lose:

                    AudioController.Instance.PlayRivalHitSound();
                    _animatorController.PlayRivalHitAnimation();
                    break;
                case CharacterAction.ActionResult.Both:
                    
                    _animatorController.PlayBothAttackAnimation();
                    break;
            }
        }

        public void SetText(CharacterAction action, CharacterAction.ActionResult result) {

            AudioController.Instance.PlayPublicEuphoricSound();

            switch (result) {
                case CharacterAction.ActionResult.None:
                    //SmartLocalization.LanguageManager.Instance.
                    _text.text = LanguageManager.Instance.GetTextValue(_noneSpeech[Random.Range(0, _noneSpeech.Count)]);
                  //  _animatorController.PlayNoneDamageAnimation();
                    break;
                case CharacterAction.ActionResult.Win:
                    if (action.Type == CharacterAction.ActionType.Ultimate) {
                        _text.text = LanguageManager.Instance.GetTextValue(_ultimateSpeech[Random.Range(0, _winSpeech.Count)]);
                    } else {
                        _text.text = LanguageManager.Instance.GetTextValue(_winSpeech[Random.Range(0, _winSpeech.Count)]);
                    }
                    //_animatorController.PlayRivalLovingAnimation();
                    break;
                case CharacterAction.ActionResult.Lose:
                    _text.text = LanguageManager.Instance.GetTextValue(_loseSpeech[Random.Range(0, _loseSpeech.Count)]);
                    //_animatorController.PlayRivalHitAnimation();
                    break;
                case CharacterAction.ActionResult.Both:
                    _text.text = LanguageManager.Instance.GetTextValue(_bothSpeech[Random.Range(0, _bothSpeech.Count)]);
                   // _animatorController.PlayBothAttackAnimation();
                    break;
            }
        }

        public void Close() {
            _closeAnimation.OnEnd += Closed;
            _closeAnimation.Play();

            _animatorController.PlayIdleAnimation();
		
        }

        void Closed(IEffectable effect) {
            _closeAnimation.OnEnd -= Closed;
            if (OnClose != null) OnClose();
        }
    }
}