using System;

namespace TrafficIntersection
{
    public interface ITrafficLight
    {
        IObservable<Light> State { get; }
        void ProgressToState(Light state);
    }
}