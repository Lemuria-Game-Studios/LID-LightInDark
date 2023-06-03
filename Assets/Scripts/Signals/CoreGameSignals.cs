using System;
using Enums;
using UnityEngine.Events;
using Extensions;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction<GameStates> OnChangeGameState= delegate {  };
        public UnityAction OnSavingGame = delegate {  };    
        public UnityAction OnLoadingGame = delegate {  };
        public UnityAction<GameStates> OnUIManagement = delegate {  };
        public UnityAction OnPausingGame = delegate {  };
        public UnityAction OnResumingGame = delegate {  };
        public UnityAction<float> DashMeter = delegate {  };
        public Func<GameStates> OnGettingGameState = () => GameStates.Game;
        public Func<byte> OnGettingAttackLevel = () => 0;
        public Func<byte> OnGettingHealthLevel = () => 0;
        public Func<byte> OnGettingSpeedLevel = () => 0;
        public Func<byte> OnGettingAttackSpeedLevel = () => 0;
        public UnityAction OnSavingSkillTree = delegate {  };
        public UnityAction OnLoadingSkillTree = delegate {  };
        public UnityAction<byte,byte,byte,byte> OnSettingLevelValues =delegate {  };
        public UnityAction OnResettingSkillTree = delegate {  };
        public UnityAction<MoneyOperations,ushort> OnSettingMoney = delegate {  };
        public Func<ushort> OnGettingMoney = () => 0;
        public UnityAction OnMoneyUI = delegate {  };
        public Func<ushort> OnGettingHealthLevelCost = () => 0;
        public Func<ushort> OnGettingAttackLevelCost = () => 0;
        public Func<ushort> OnGettingAttackSpeedLevelCost = () => 0;
        public Func<ushort> OnGettingSpeedLevelCost = () => 0;
        public UnityAction<ushort,ushort,ushort,ushort> OnSettingLevelCostValues =delegate {  };
    }
}
