using System;
using System.Reactive.Linq;
using FluentAssertions;
using Xunit;

namespace TrafficIntersection.Tests
{
    public class TrafficLightTests
    {
        [Fact]
        public void ForSafetyATrafficLightShouldDefaultToRed()
        {
            var trafficLight = new TrafficLight();
            trafficLight.State.Select(x => x).Subscribe(state => { state.Should().Be(Light.Red); });
        }

        [Theory]
        [InlineData(Light.Red, Light.Red)]
        [InlineData(Light.Green, Light.Yellow)]
        [InlineData(Light.Yellow, Light.Green)]
        public void TrafficLightShouldProgressAsExpected(Light initialState, Light newState)
        {
            var trafficLight = new TrafficLight(initialState);

            trafficLight.ProgressToState(newState);
            trafficLight.State.Select(x => x).Subscribe(state => { state.Should().Be(newState); });
        }
    }
}