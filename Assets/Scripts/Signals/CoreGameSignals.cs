using Enums;
using UnityEngine.Events;
using Extensions;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction<GameStates> onChangeGameState= delegate {  };
    }
}
