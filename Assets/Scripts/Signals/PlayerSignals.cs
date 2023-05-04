using System;
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
    }
}
