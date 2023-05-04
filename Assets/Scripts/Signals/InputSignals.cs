using Extensions;
using UnityEngine.Events;
using System;

namespace Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        public UnityAction OnSwordAttack= delegate {  };
        public UnityAction OnArchering = delegate {  };
        public UnityAction CanDash =delegate {  };
        public Func<bool> OnGetIsDashing = () => false;
    }
}
