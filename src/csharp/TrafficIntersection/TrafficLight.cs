using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace TrafficIntersection
{
    public class TrafficLight : ITrafficLight
    {
        private readonly ReplaySubject<Light> _state;

        public TrafficLight(Light state = Light.Red)
        {
            const int bufferSize = 1;
            _state = new ReplaySubject<Light>(bufferSize);
            ProgressToState(state);
        }

        public DateTimeOffset LastStateChange { get; private set; }

        public IObservable<Light> State => _state.AsObservable();

        public void ProgressToState(Light state)
        {
            _state.OnNext(state);
            LastStateChange = DateTimeOffset.Now;
        }
    }
}