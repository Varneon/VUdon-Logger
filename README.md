<div>

# [VUdon](https://github.com/Varneon/VUdon) - Logger [![GitHub Repo stars](https://img.shields.io/github/stars/Varneon/VUdon-Logger?style=flat&label=Stars)](https://github.com/Varneon/VUdon-Logger/stargazers) [![GitHub all releases](https://img.shields.io/github/downloads/Varneon/VUdon-Logger/total?color=blue&label=Downloads&style=flat)](https://github.com/Varneon/VUdon-Logger/releases) [![GitHub tag (latest SemVer)](https://img.shields.io/github/v/tag/Varneon/VUdon-Logger?color=blue&label=Release&sort=semver&style=flat)](https://github.com/Varneon/VUdon-Logger/releases/latest)

</div>

Runtime logger for UdonSharp. Send log messages to an UdonLogger in the scene and view them on a world-space UI.
### Why should you use this?
+ Works even when VRChat's own logs are turned off - don't miss critical info when you might have forgotten to turn logging on
+ Rapid troubleshooting for isolated contexts - add reference to an UdonLogger and feed any logs that might help
+ Isolated to your chosen scope (no need to parse through thousands of lines of VRChat logs or to use custom prefixes)
+ No skill barrier for a visitor to view the logs and report them to you

## UdonConsole Prefab
**UdonConsole** is an in-world console window for viewing the logged messages in game. Implements the **UdonLogger** class for use in a similar window design to the **ConsoleWindow** in the **Unity Editor**.

## UdonLogger U# Class
[**UdonLogger**](https://github.com/Varneon/VUdon-Logger/blob/main/Packages/com.varneon.vudon.logger/Runtime/Udon%20Programs/Abstract/UdonLogger.cs) is an abstract class similar to `UnityEngine.ILogger` interface, which you can extend freely to suit your purposes.

> [!IMPORTANT]
> UdonConsole after `0.4.0` uses [TextMeshProUGUI](https://docs.unity3d.com/Packages/com.unity.textmeshpro@2.1/api/TMPro.TextMeshProUGUI.html) instead of native [Unity UI Text](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/script-Text.html) components! Not all rich color tags are supported anymore, such as `<color=silver>` and `<color=magenta>`. Read the wiki page to learn mode: [UdonConsole: Supported Rich Text Color Tags](https://github.com/Varneon/VUdon-Logger/wiki/UdonConsole:-Supported-Rich-Text-Color-Tags)

![image](https://github.com/Varneon/VUdon-Logger/assets/26690821/bf83f488-e6a5-41e0-9210-71612cfc194d)

# How to Use VUdon Logger

[How To Use UdonConsole](https://github.com/Varneon/VUdon-Logger/wiki/How-To-Use-UdonConsole)

[Implementing UdonLogger](https://github.com/Varneon/VUdon-Logger/wiki/Implementing-UdonLogger) *(Advanced)*

## Installation

### Dependencies - `1`
* [VUdon Editors](https://github.com/Varneon/VUdon-Editors) *(Makes the prefab inspector more user-friendly)*

### A) Import with [VRChat Creator Companion](https://vcc.docs.vrchat.com/vpm/)
* https://vpm.varneon.com/ *(Dependencies will be included in the repository lists)*

### B) Import from [Unitypackage](https://docs.unity3d.com/2022.3/Documentation/Manual/AssetPackagesImport.html)
1. Download and import [dependencies](https://github.com/Varneon/VUdon-Logger/README.md#dependencies---1) from the respective repositories with their specified installation instructions
2. Download latest `com.varneon.vudon.logger.unitypackage` from [here](https://github.com/Varneon/VUdon-Logger/releases/latest)
3. Import the downloaded .unitypackage into your Unity project

<div align="center">

## Developed by Varneon with :hearts:

[![Twitter Follow](https://img.shields.io/static/v1?style=for-the-badge&label=@Varneon&message=8.4K&color=1b9df0&logo=x)](https://x.com/Varneon)
[![YouTube Channel Subscribers](https://img.shields.io/static/v1?style=for-the-badge&label=@Varneon&message=1.4K&color=%23FF0000&logo=YouTube)](https://www.youtube.com/Varneon)
[![GitHub followers](https://img.shields.io/github/followers/Varneon?color=%23303030&label=Varneon&logo=GitHub&style=for-the-badge)](https://github.com/Varneon)

</div>
