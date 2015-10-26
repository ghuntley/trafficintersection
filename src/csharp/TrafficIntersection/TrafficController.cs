using System;
using System.ComponentModel;
using System.Reactive.Concurrency;

namespace TrafficIntersection
{
    public class TrafficController : ITrafficController
    {
        private readonly ITrafficLight _eastTrafficLight = new TrafficLight();
        private readonly ITrafficLight _northTrafficLight = new TrafficLight();
        private readonly IScheduler _scheduler;
        private readonly ITrafficLight _southTrafficLight = new TrafficLight();
        private readonly ITrafficLight _westTrafficLight = new TrafficLight();

        public TrafficController(IScheduler scheduler)
        {
            _scheduler = scheduler;

            // export traffic lights from the controller
            NorthLight = _northTrafficLight.State;
            SouthLight = _southTrafficLight.State;
            WestLight = _westTrafficLight.State;
            EastLight = _eastTrafficLight.State;

            // expire/force a traffic light state change to happen on the first SchedulePeriod tick.
            NextTrafficStateChange = _scheduler.Now;

            _scheduler.SchedulePeriodic(TimeSpan.FromSeconds(1), () =>
            {
                if (_scheduler.Now < NextTrafficStateChange) return;

                switch (NextTrafficState)
                {
                    case TrafficState.RedRed:
                        ChangeNorthSouthLightsTo(Light.Red);
                        ChangeEastWestLightsTo(Light.Red);

                        SleepForDuration(Light.Red);

                        SetNextStateTo(TrafficState.RedGreen);
                        break;
                    case TrafficState.RedGreen:
                        ChangeNorthSouthLightsTo(Light.Red);
                        ChangeEastWestLightsTo(Light.Green);

                        SleepForDuration(Light.Green);

                        SetNextStateTo(TrafficState.RedYellow);
                        break;
                    case TrafficState.RedYellow:
                        ChangeNorthSouthLightsTo(Light.Red);
                        ChangeEastWestLightsTo(Light.Yellow);

                        SleepForDuration(Light.Yellow);

                        SetNextStateTo(TrafficState.GreenRed);
                        break;
                    case TrafficState.GreenRed:
                        ChangeNorthSouthLightsTo(Light.Green);
                        ChangeEastWestLightsTo(Light.Red);

                        SleepForDuration(Light.Green);

                        SetNextStateTo(TrafficState.YellowRed);
                        break;
                    case TrafficState.YellowRed:
                        ChangeNorthSouthLightsTo(Light.Yellow);
                        ChangeEastWestLightsTo(Light.Red);

                        SleepForDuration(Light.Yellow);

                        SetNextStateTo(TrafficState.RedRed);
                        break;
                    default:
                        throw new InvalidEnumArgumentException();
                }
            });
        }

        /// <summary>
        ///     Possible states are RedRed, RedGreen, RedYellow, GreenRed, YellowRed in that order.
        /// </summary>
        public TrafficState NextTrafficState { get; private set; }

        /// <summary>
        ///     When the next traffic state change is due.
        /// </summary>
        /// <remarks>
        ///     The time represented here does is not a reflection of the time on the system clock, it is the time on the virtual
        ///     clock on the IScheduler that is passed into the constructor of this class.
        /// </remarks>
        public DateTimeOffset NextTrafficStateChange { get; private set; }

        public IObservable<Light> NorthLight { get; }
        public IObservable<Light> SouthLight { get; }
        public IObservable<Light> WestLight { get; }
        public IObservable<Light> EastLight { get; }

        private void SetNextStateTo(TrafficState state)
        {
            NextTrafficState = state;
        }

        private void SleepForDuration(Light light)
        {
            switch (light)
            {
                case Light.Red:
                    // Whilst the specification does not mention it, there really should be a couple second pause
                    // at a red stage for safety reasons. ie. safety window to protect against a car speeding
                    // through a yellow light.
                    NextTrafficStateChange = _scheduler.Now;
                    break;
                case Light.Green:
                    NextTrafficStateChange = _scheduler.Now.AddSeconds(270);
                    break;
                case Light.Yellow:
                    NextTrafficStateChange = _scheduler.Now.AddSeconds(30);
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        private void ChangeNorthSouthLightsTo(Light light)
        {
            _northTrafficLight.ProgressToState(light);
            _southTrafficLight.ProgressToState(light);
        }

        private void ChangeEastWestLightsTo(Light light)
        {
            _eastTrafficLight.ProgressToState(light);
            _westTrafficLight.ProgressToState(light);
        }
    }
}