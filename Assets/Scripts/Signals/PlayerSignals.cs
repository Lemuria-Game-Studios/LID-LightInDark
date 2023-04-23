using Extensions;
using UnityEngine.Events;

namespace Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction OnAttacking = delegate { };
        public UnityAction OnSpelling = delegate {  };
        public UnityAction<float> OnSettingSpeed = delegate {  };
        public UnityAction OnDashing =delegate {  };
        public UnityAction CanDash =delegate {  };
    }
}
