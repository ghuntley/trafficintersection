using System;
using FluentAssertions;
using Microsoft.Reactive.Testing;
using ReactiveUI.Testing;
using Xunit;

namespace TrafficIntersection.Tests
{
    public class TrafficIntersectionTests
    {
        [Fact]
        public void TrafficLightsShouldChangetoExpected()
        {
            new TestScheduler().With(
                scheduler =>
                {
                    scheduler.Start();

                    var trafficController = new TrafficController(scheduler);
                    var intersection = new TrafficIntersection(trafficController);

                    // progress by one second past RedRed state to the RedGreen state.
                    scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);

                    intersection.NorthLight.Subscribe(state => { state.Should().Be(Light.Red); });

                    intersection.SouthLight.Subscribe(state => { state.Should().Be(Light.Red); });

                    intersection.WestLight.Subscribe(state => { state.Should().Be(Light.Green); });

                    intersection.EastLight.Subscribe(state => { state.Should().Be(Light.Green); });
                });
        }
    }
}