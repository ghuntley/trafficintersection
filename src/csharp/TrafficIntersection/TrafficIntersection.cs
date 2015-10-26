using System;

namespace TrafficIntersection
{
    public class TrafficIntersection
    {
        private readonly ITrafficController _trafficController;

        public TrafficIntersection(ITrafficController trafficController)
        {
            _trafficController = trafficController;

            NorthLight = _trafficController.NorthLight;
            SouthLight = _trafficController.SouthLight;
            WestLight = _trafficController.WestLight;
            EastLight = _trafficController.EastLight;
        }

        public IObservable<Light> NorthLight { get; }
        public IObservable<Light> SouthLight { get; }
        public IObservable<Light> WestLight { get; }
        public IObservable<Light> EastLight { get; }
    }
}