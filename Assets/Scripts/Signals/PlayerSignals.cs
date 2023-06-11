using System;
using Enums;
using Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction OnDashing =delegate {  };
        public Func<float> OnGettingDashMeter= () => 0;
        public UnityAction<float> OnSettingDashMeter= delegate {  };
        public Func<Transform> OnGettingTransform= () => null;
        public Func<ushort> OnGettingAttackSpeed = () => 0;
        public Func<float> OnGettingSpeed = () => 0;
        public UnityAction<LevelUp> OnLevelUp = delegate {  }; 
        public Func<ushort> OnGettingAttackPower = () => 0;
        public Func<short> OnGettingHealth = () => 0;
        public  UnityAction<ushort,ushort,float,ushort> OnSettingAttributes = delegate{  };
        public UnityAction<float,float> OnUpdatingHealthBar = delegate {  };
        public Func<ushort> OnGettingArrowPower = () => 0;
    }
}
