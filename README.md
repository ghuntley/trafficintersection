# Traffic Intersection [![Build status](https://ci.appveyor.com/api/projects/status/vmwsabmeub8r14c5?svg=true)](https://ci.appveyor.com/project/ghuntley/trafficintersection)

<!--[![Play Video](https://i.imgur.com/izcC3UH.png)](https://ghuntley.wistia.com/medias/4z2sn5u49c)
-->

![Traffic Intersection](https://raw.githubusercontent.com/ghuntley/trafficintersection/master/assets/trafficintersection.png)

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

# Implementation

Because there is no delay at the `RedRed` stage (although there should be due to safety) the lights were implemented as follows:


State         | North-South | East-West | Delay
------------- | ------------|-----------|-------
0 - RedRed    | Red         | Red       | 0
1 - RedGreen  | Red         | Green     | 270
2 - RedYellow | Red         | Yellow    | 30
3 - GreenRed  | Green       | Red       | 270
4 - YellowRed | Yellow      | Red       | 30

In the above implementation `RedRed` exists purely as the default state when the controller and traffic lights are initialized. It could be argued that it is not actually even needed. If there was a delay introduced for `RedRed` then a additional state change would be needed between `RedYellow` and `GreenRed`.

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

    2015-10-27 09:01:00.298 +11:00 [Information] North is now Red
    2015-10-27 09:01:00.338 +11:00 [Information] South is now Red
    2015-10-27 09:01:00.340 +11:00 [Information] West is now Red
    2015-10-27 09:01:00.340 +11:00 [Information] East is now Red
    2015-10-27 09:01:01.285 +11:00 [Information] North is now Red
    2015-10-27 09:01:01.286 +11:00 [Information] South is now Red
    2015-10-27 09:01:01.287 +11:00 [Information] East is now Red
    2015-10-27 09:01:01.288 +11:00 [Information] West is now Red
    2015-10-27 09:01:02.285 +11:00 [Information] North is now Red
    2015-10-27 09:01:02.286 +11:00 [Information] South is now Red
    2015-10-27 09:01:02.287 +11:00 [Information] East is now Green
    2015-10-27 09:01:02.288 +11:00 [Information] West is now Green
    2015-10-27 09:05:32.316 +11:00 [Information] North is now Red
    2015-10-27 09:05:32.316 +11:00 [Information] South is now Red
    2015-10-27 09:05:32.316 +11:00 [Information] East is now Yellow
    2015-10-27 09:05:32.316 +11:00 [Information] West is now Yellow
    2015-10-27 09:06:02.786 +11:00 [Information] North is now Green
    2015-10-27 09:06:02.786 +11:00 [Information] South is now Green
    2015-10-27 09:06:02.786 +11:00 [Information] East is now Red
    2015-10-27 09:06:02.786 +11:00 [Information] West is now Red
    2015-10-27 09:10:32.945 +11:00 [Information] North is now Yellow
    2015-10-27 09:10:32.945 +11:00 [Information] South is now Yellow
    2015-10-27 09:10:32.945 +11:00 [Information] East is now Red
    2015-10-27 09:10:32.945 +11:00 [Information] West is now Red
    2015-10-27 09:11:03.414 +11:00 [Information] North is now Red
    2015-10-27 09:11:03.414 +11:00 [Information] South is now Red
    2015-10-27 09:11:03.414 +11:00 [Information] East is now Red
    2015-10-27 09:11:03.414 +11:00 [Information] West is now Red
    2015-10-27 09:11:04.430 +11:00 [Information] North is now Red
    2015-10-27 09:11:04.430 +11:00 [Information] South is now Red
    2015-10-27 09:11:04.430 +11:00 [Information] East is now Green
    2015-10-27 09:11:04.430 +11:00 [Information] West is now Green
    2015-10-27 09:15:35.210 +11:00 [Information] North is now Red
    2015-10-27 09:15:35.210 +11:00 [Information] South is now Red
    2015-10-27 09:15:35.226 +11:00 [Information] East is now Yellow
    2015-10-27 09:15:35.226 +11:00 [Information] West is now Yellow
    2015-10-27 09:16:05.625 +11:00 [Information] North is now Green
    2015-10-27 09:16:05.625 +11:00 [Information] South is now Green
    2015-10-27 09:16:05.625 +11:00 [Information] East is now Red
    2015-10-27 09:16:05.625 +11:00 [Information] West is now Red
    2015-10-27 09:20:36.141 +11:00 [Information] North is now Yellow
    2015-10-27 09:20:36.141 +11:00 [Information] South is now Yellow
    2015-10-27 09:20:36.157 +11:00 [Information] East is now Red
    2015-10-27 09:20:36.157 +11:00 [Information] West is now Red
    2015-10-27 09:21:06.547 +11:00 [Information] North is now Red
    2015-10-27 09:21:06.547 +11:00 [Information] South is now Red
    2015-10-27 09:21:06.547 +11:00 [Information] East is now Red
    2015-10-27 09:21:06.547 +11:00 [Information] West is now Red
    2015-10-27 09:21:07.554 +11:00 [Information] North is now Red
    2015-10-27 09:21:07.569 +11:00 [Information] South is now Red
    2015-10-27 09:21:07.569 +11:00 [Information] East is now Green
    2015-10-27 09:21:07.569 +11:00 [Information] West is now Green
    2015-10-27 09:25:38.031 +11:00 [Information] North is now Red
    2015-10-27 09:25:38.031 +11:00 [Information] South is now Red
    2015-10-27 09:25:38.031 +11:00 [Information] East is now Yellow
    2015-10-27 09:25:38.031 +11:00 [Information] West is now Yellow
    2015-10-27 09:26:08.444 +11:00 [Information] North is now Green
    2015-10-27 09:26:08.444 +11:00 [Information] South is now Green
    2015-10-27 09:26:08.444 +11:00 [Information] East is now Red
    2015-10-27 09:26:08.444 +11:00 [Information] West is now Red
    2015-10-27 09:30:39.081 +11:00 [Information] North is now Yellow
    2015-10-27 09:30:39.081 +11:00 [Information] South is now Yellow
    2015-10-27 09:30:39.081 +11:00 [Information] East is now Red
    2015-10-27 09:30:39.081 +11:00 [Information] West is now Red
    2015-10-27 09:31:09.469 +11:00 [Information] North is now Red
    2015-10-27 09:31:09.469 +11:00 [Information] South is now Red
    2015-10-27 09:31:09.469 +11:00 [Information] East is now Red
    2015-10-27 09:31:09.469 +11:00 [Information] West is now Red
    2015-10-27 09:31:10.485 +11:00 [Information] North is now Red
    2015-10-27 09:31:10.485 +11:00 [Information] South is now Red
    2015-10-27 09:31:10.485 +11:00 [Information] East is now Green
    2015-10-27 09:31:10.485 +11:00 [Information] West is now Green
    2015-10-27 09:35:41.049 +11:00 [Information] North is now Red
    2015-10-27 09:35:41.049 +11:00 [Information] South is now Red
    2015-10-27 09:35:41.049 +11:00 [Information] East is now Yellow
    2015-10-27 09:35:41.049 +11:00 [Information] West is now Yellow
