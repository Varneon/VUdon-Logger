<div>

# [VUdon](https://github.com/Varneon/VUdon) - Logger [![GitHub Repo stars](https://img.shields.io/github/stars/Varneon/VUdon-Logger?style=flat&label=Stars)](https://github.com/Varneon/VUdon-Logger/stargazers) [![GitHub all releases](https://img.shields.io/github/downloads/Varneon/VUdon-Logger/total?color=blue&label=Downloads&style=flat)](https://github.com/Varneon/VUdon-Logger/releases) [![GitHub tag (latest SemVer)](https://img.shields.io/github/v/tag/Varneon/VUdon-Logger?color=blue&label=Release&sort=semver&style=flat)](https://github.com/Varneon/VUdon-Logger/releases/latest)

</div>

Runtime logger for UdonSharp

# UdonLogger class

[**UdonLogger**](https://github.com/Varneon/VUdon-Logger/blob/main/Packages/com.varneon.vudon.logger/Runtime/Udon%20Programs/Abstract/UdonLogger.cs) is an abstract class similar to `UnityEngine.ILogger` interface, which you can extend freely to suit your purposes.

# UdonConsole prefab

**UdonConsole** is an in-world console window for viewing the logged messages in game. Implements the **UdonLogger** class for use in a similar window design to the **ConsoleWindow** in the **Unity Editor**.

> `NOTE`: UdonConsole after `0.4.0` uses [TextMeshProUGUI](https://docs.unity3d.com/Packages/com.unity.textmeshpro@2.1/api/TMPro.TextMeshProUGUI.html) instead of native [Unity UI Text](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/script-Text.html) components! Not all rich color tags are supported anymore, such as `<color=silver>` and `<color=magenta>`. Read the wiki page to learn mode: [UdonConsole: Supported Rich Text Color Tags](https://github.com/Varneon/VUdon-Logger/wiki/UdonConsole:-Supported-Rich-Text-Color-Tags)

![image](https://github.com/Varneon/VUdon-Logger/assets/26690821/bf83f488-e6a5-41e0-9210-71612cfc194d)

# Installation

<details><summary>

### Dependencies - `1`</summary>

* [VUdon - Editors](https://github.com/Varneon/VUdon-Editors)

</details><details><summary>

### Import with [VRChat Creator Companion](https://vcc.docs.vrchat.com/vpm/packages#user-packages):</summary>

> 1. Download `com.varneon.vudon.logger.zip` from [here](https://github.com/Varneon/VUdon-Logger/releases/latest)
> 2. Unpack the .zip somewhere
> 3. In VRChat Creator Companion, navigate to `Settings` > `User Packages` > `Add`
> 4. Navigate to the unpacked folder, `com.varneon.vudon.logger` and click `Select Folder`
> 5. `VUdon - Logger` should now be visible under `Local User Packages` in the project view in VRChat Creator Companion
> 6. Click `Add`

</details><details><summary>

### Import from [Unitypackage](https://docs.unity3d.com/2019.4/Documentation/Manual/AssetPackagesImport.html):</summary>

> 1. Download latest `com.varneon.vudon.logger.unitypackage` from [here](https://github.com/Varneon/VUdon-Logger/releases/latest)
> 2. Import the downloaded .unitypackage into your Unity project

</details>

<div align="center">

## Developed by Varneon with :hearts:

![Twitter Follow](https://img.shields.io/twitter/follow/Varneon?color=%231c9cea&label=%40Varneon&logo=Twitter&style=for-the-badge)
![YouTube Channel Subscribers](https://img.shields.io/youtube/channel/subscribers/UCKTxeXy7gyaxr-YA9qGWOYg?color=%23FF0000&label=Varneon&logo=YouTube&style=for-the-badge)
![GitHub followers](https://img.shields.io/github/followers/Varneon?color=%23303030&label=Varneon&logo=GitHub&style=for-the-badge)

</div>
