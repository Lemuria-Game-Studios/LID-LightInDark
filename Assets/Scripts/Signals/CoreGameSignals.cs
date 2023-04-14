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
        public UnityAction OnAppearingInGameUI = delegate {  };
        public UnityAction OnAppearingMenuUI = delegate {  };
        public UnityAction OnPausingGame = delegate {  };
        public UnityAction OnResumingGame = delegate {  };
    }
}
