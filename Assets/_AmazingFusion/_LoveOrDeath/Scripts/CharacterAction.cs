using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.LoveOrDeath
{
    public class CharacterAction : ScriptableObject {

        public const string ACTIONS_PATH = "Actions/";

        public enum ActionType {
            Energetic,
            Offensive,
            Defensive,
            Ultimate
        }

        public enum ActionResult {
            None,
            Win,
            Lose,
            Both
        }

        private static CharacterAction[] _actionList;

        [SerializeField]
        string _key;

        [SerializeField]
        ActionType _type;

        [SerializeField]
        int _requiredLevel;

        [SerializeField]
        int _energyCost;

        [SerializeField]
        int _energyEarned;

        [SerializeField]
        int _damage;

        public string Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }

        public ActionType Type {
            get {
                return _type;
            }
        }

        public int RequiredLevel
        {
            get
            {
                return _requiredLevel;
            }
        }

        public int EnergyCost
        {
            get
            {
                return _energyCost;
            }
        }

        public int EnergyEarned
        {
            get
            {
                return _energyEarned;
            }
        }

        public int Damage
        {
            get
            {
                return _damage;
            }
        }

        public static CharacterAction[] ActionList
        {
            get
            {
                if(_actionList == null)
                {
                    _actionList = LoadAll();
                }
                return _actionList;
            }
        }

        public ActionResult ClashAction(CharacterAction action)
        {
            ActionResult actionResult = ActionResult.None;
            
            if (Type == ActionType.Offensive)
            {
                if(action.Type == ActionType.Defensive)
                {
                    actionResult = ActionResult.Lose;
                }
                else if (action.Type == ActionType.Offensive)
                {
                    actionResult = ActionResult.Both;
                }
                else if(action.Type == ActionType.Energetic)
                {
                    actionResult = ActionResult.Win;
                }
                else if(action.Type == ActionType.Ultimate)
                {
                    actionResult = ActionResult.Lose;
                }
            }
            else if (Type == ActionType.Defensive)
            {
                if (action.Type == ActionType.Defensive)
                {
                    actionResult = ActionResult.None;
                }
                else if (action.Type == ActionType.Offensive)
                {
                    actionResult = ActionResult.Win;
                }
                else if (action.Type == ActionType.Energetic)
                {
                    actionResult = ActionResult.Lose;
                }
                else if (action.Type == ActionType.Ultimate)
                {
                    actionResult = ActionResult.Lose;
                }
            }
            else if (Type == ActionType.Energetic)
            {
                if (action.Type == ActionType.Defensive)
                {
                    actionResult = ActionResult.Win;
                }
                else if (action.Type == ActionType.Offensive)
                {
                    actionResult = ActionResult.Both;
                }
                else if (action.Type == ActionType.Energetic)
                {
                    actionResult = ActionResult.Both;
                }
                else if (action.Type == ActionType.Ultimate)
                {
                    actionResult = ActionResult.Both;
                }
            }
            else if (Type == ActionType.Ultimate)
            {
                if (action.Type == ActionType.Defensive)
                {
                    actionResult = ActionResult.Win;
                }
                else if (action.Type == ActionType.Offensive)
                {
                    actionResult = ActionResult.Win;
                }
                else if (action.Type == ActionType.Energetic)
                {
                    actionResult = ActionResult.Win;
                }
                else if (action.Type == ActionType.Ultimate)
                {
                    actionResult = ActionResult.Win;
                }
            }

            return actionResult;
        }

        public void ActionResolved(bool win) {
            if (OnActionResolved != null) OnActionResolved(win);
        }

        public static CharacterAction[] LoadAll() {
            return Resources.LoadAll<CharacterAction>(ACTIONS_PATH);
        }

        public event System.Action<bool> OnActionResolved;
    }
}

