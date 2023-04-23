using Extensions;
using UnityEngine;
using UnityEngine.Events;
using Enums;
namespace Signals
{
    public class AnimationSignals : MonoSingleton<AnimationSignals>
    {
        public UnityAction<AnimationStates> OnPlayingAnimation=delegate{  };
    }
}
