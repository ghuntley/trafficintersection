# Traffic Intersection [![Build status](https://ci.appveyor.com/api/projects/status/vmwsabmeub8r14c5?svg=true)](https://ci.appveyor.com/project/ghuntley/trafficintersection)

[![Play Video](https://i.imgur.com/izcC3UH.png)](https://ghuntley.wistia.com/medias/4z2sn5u49c)

# Specification
You are required to provide the code for an application that simulates a set of traffic lights at
an intersection. The traffic lights are designated (N, S) and (E, W) like a compass.

## Requirements
* When switching from green to red, the yellow light must be displayed for 30 seconds prior to it switching to red and the opposite direction switching to green from red.
* The lights will change automatically every 5 minutes.
* You're not required to optimize the solution, just focus on a functional approach to requirements.
* Provide the output for the light changes which occur during the period 9am and 9:30am.
* You must provide unit tests for all logic.
* Create a repo on bitbucket/github and provide the link.

## Interpretation
* Implement a traffic intersection that has four lights (N/S/W/E) with no turning bays.
* Lights transition as follows - Red = 270sec, Yellow = 30 sec, Green 300 sec which results in traffic start/stop every 5 minutes.
* Traffic flows North <--> South and West <--> East, cars allowed to turn left or right only when safe as there are no turning bays.
* North <--> South and West <--> East traffic light state are chained/subscribed and state propagates to the opposite neighbour.
* All logic must be unit tested.
* Source code uploaded onto public repo.
* Provide 30 minutes of traffic light state change data.

## Assumptions
* No turning bays, otherwise additional traffic lights or rows within a traffic light will need to be installed.
* No pedestrians/crosswalks.

# Unit Testing
* This repository has been integrated with AppVeyor which performs builds of the solution after [every commit](https://ci.appveyor.com/project/ghuntley/trafficintersection) to ensure the builds are always green. In the root of this repository there is a file called [appveyor.yml](https://github.com/ghuntley/trafficintersection/blob/master/appveyor.yml) which controls the configuration.

* A virtual clock that can be programmatically rewound or fast-forwarded is passed into the constructor of the TrafficLightController. It is specifically used to verify the that the state sequence is functions correctly whilst ensuring that the unit tests complete fast without having to wait a full 10 minutes on the system clock.

		xUnit.net console test runner (32-bit .NET 4.0.30319.42000)
		Copyright (C) 2015 Outercurve Foundation.

		Discovering: TrafficIntersection.Tests
		Discovered:  TrafficIntersection.Tests
		Starting: TrafficIntersection.Tests.DLL
		Finished: TrafficIntersection.Tests.DLL

		=== TEST EXECUTION SUMMARY ===
		   TrafficIntersection.Tests.DLL  Total: 7, Errors: 0, Failed: 0, Skipped: 0, Time: 2.389s
		Packaging artifacts...OK
		Build success

# Remarks
* Whilst the specification does not mention it, there really should be a couple second pause at a red stage for safety reasons. ie. safety window to protect against a car speeding through a yellow light.

# Results

TBA

