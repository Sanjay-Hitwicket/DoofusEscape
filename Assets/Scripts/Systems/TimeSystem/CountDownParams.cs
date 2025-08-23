using System;
using System.Threading;

namespace Systems.TimeSystem {
    public class CountDownParams {
        public float duration;
        public Action onCompleteCallback;
        public CancellationToken cancellationToken = default;
    }
}