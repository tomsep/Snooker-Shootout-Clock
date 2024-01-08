# Snooker Shootout Clock
Remote controllable Android application for tracking both frame time and shot time during a snooker shootout game.

https://github.com/tomsep/Snooker-Shootout-Clock/assets/52826851/9d0f9b1e-a9e6-455d-bc4a-f7f3e27072b5

## Features
* Frame and shot timers.
* Shot time is automatically reduced from 15s to 10s after half-time is passed.
* Remote controllable shot timer. Compatible with most camera shutter remotes.
* Timer countdown sound effects.
* Half-time, last minute, last 30s, and last 15s announcement sounds.

### Building 
- In a terminal window, navigate to the project's root directory and run the following command:
```
./gradlew assemble
```
- On successful completion of the build, the output files can be found in
  [`plugin/demo/addons`](plugin/demo/addons)

- Next build the Godot 4.2 poject in [`plugin/demo`](plugin/demo)
- Export the project from Godot to Android.
