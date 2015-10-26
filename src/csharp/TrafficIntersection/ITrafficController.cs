using System;

namespace TrafficIntersection
{
    public interface ITrafficController
    {
        IObservable<Light> NorthLight { get; }
        IObservable<Light> SouthLight { get; }
        IObservable<Light> WestLight { get; }
        IObservable<Light> EastLight { get; }
    }
}