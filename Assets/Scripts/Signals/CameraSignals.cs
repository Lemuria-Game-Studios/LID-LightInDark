using System;
using Extensions;
using UnityEngine;

namespace Signals
{
    public class CameraSignals : MonoSingleton<CameraSignals>
    {
        public Func<Transform> OnGettingCameraTransform = () => null;
    }
}
