LUX Imperium
--

Control light via DMX from any application using Enttec OpenDmxUsb definitions in Json files.

The project is build around:

* "OpenDMX" demo from Enttec.
* The "Enttec OpenDmxUsb" hardware from Enttec.

The project is build for .NET Framework 4.8 - as it was needed in Windows forms application.


Discoveries during made during the project:
---

The only fixtures/appliances I have access to and used for testing:

* The REDSHOW PAR36-12 (Old light fixture) can be controlled by sending single packages and the fixture keeps the state.
* The ADJ Fog Furry JET (Smoke machine) must receive continues packages to execute.


LuxImperium
--- 

The LuxImperium is a library to control DMX devices. It is a wrapper around the FTD2XX_NET.dll. The FTD2XX_NET.dll is a wrapper around the FTD2XX.dll. The FTD2XX.dll is a driver for the FTDI USB to Serial chip. The FTDI USB to Serial chip is used in the Enttec Open DMX USB device.

LuxImperiumGovernor.cs is the class that controls the DMX device. It is the class that sends the DMX data to the fixtures defined in a scene file.

LuxImperium.Test
---

Unit tests for LuxImperium library.

LuxImperium-Console
---

The console application to show the **LuxImperium** library in action.


