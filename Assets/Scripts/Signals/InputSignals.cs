using Extensions;
using UnityEngine.Events;
using System;
using Enums;

namespace Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        public UnityAction OnSwordAttack= delegate {  };
        public UnityAction OnArchering = delegate {  };
        public UnityAction CanDash =delegate {  };
        public Func<bool> OnGetCanDash = () => false;
        public UnityAction<float,float> OnMovementAndRotation=delegate {  };
        public Func<AnimationStates> OnGettingAnimationState=delegate { return AnimationStates.Idle; };
    }
}
