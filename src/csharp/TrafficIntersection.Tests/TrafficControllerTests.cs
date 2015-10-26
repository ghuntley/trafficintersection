using System;
using FluentAssertions;
using Microsoft.Reactive.Testing;
using ReactiveUI.Testing;
using Xunit;

namespace TrafficIntersection.Tests
{
    public class TrafficControllerTests
    {
        [Fact]
        public void TrafficLightsShouldChangeToExpected()
        {
            new TestScheduler().With(
                scheduler =>
                {
                    scheduler.Start();

                    var trafficController = new TrafficController(scheduler);

                    // progress by one second past RedRed state to the RedGreen state.
                    scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

                    trafficController.NorthLight.Subscribe(state => { state.Should().Be(Light.Red); });

                    trafficController.SouthLight.Subscribe(state => { state.Should().Be(Light.Red); });

                    trafficController.WestLight.Subscribe(state => { state.Should().Be(Light.Green); });

                    trafficController.EastLight.Subscribe(state => { state.Should().Be(Light.Green); });
                });
        }

        [Fact]
        public void StatesShouldChangeInCorrectOrderWithCorrectTimings()
        {
            new TestScheduler().With(
                scheduler =>
                {
                    scheduler.Start();

                    var trafficController = new TrafficController(scheduler);

                    trafficController.NextTrafficState.Should().Be(TrafficState.RedRed);

                    scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
                    trafficController.NextTrafficState.Should().Be(TrafficState.RedGreen);
                    scheduler.AdvanceBy(TimeSpan.FromSeconds(270).Ticks);

                    trafficController.NextTrafficState.Should().Be(TrafficState.RedYellow);
                    scheduler.AdvanceBy(TimeSpan.FromSeconds(30).Ticks);

                    trafficController.NextTrafficState.Should().Be(TrafficState.GreenRed);
                    scheduler.AdvanceBy(TimeSpan.FromSeconds(270).Ticks);

                    trafficController.NextTrafficState.Should().Be(TrafficState.YellowRed);
                    scheduler.AdvanceBy(TimeSpan.FromSeconds(30).Ticks);

                    trafficController.NextTrafficState.Should().Be(TrafficState.RedRed);
                });
        }
    }
}