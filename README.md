
# HydraLauncher - opensource launcher for minecraft

HydraLauncher - opensource launcher for minecraft written in .NET C# using CmlLib.Core library

## ATTENTION
``` All versions of Minecraft in this launcher come with Forge by default```
## Installation

Install HydraLauncher packages with NuGET in your IDE (Visual Studio/MonoDevelop):

```nuget
CmlLib.Core
CmlLib.Core.Installer.Forge
SharpZipLib
Newtonsoft.Json
Microsoft.CSharp (MonoDevelop)
LZMA-SDK
HtmlAgilityPack
```

### You also can LAUNCH executable file in ```minecraft_launcher/bin/Debug/minecraft_launcher.exe```

#### If you have ```LINUX```:

```
mono minecraft_launcher/bin/Debug/minecraft_launcher.exe
```
## Launcher Configuration

```csharp
    var process = await launcher.CreateProcessAsync(forge_version, new MLaunchOption)
    {
        Session = MSession.CreateOfflineSession(username),
        MaximumRamMb = 2048,
        VersionType = "HydraLauncher",
        GameLauncherName = "HydraLauncher",
        GameLauncherVersion = "v.1.0",
        FullScreen = false,
    }
``` 

### * CMLauncher Configuration
```csharp
System.Net.ServicePointManager.DefaultConnectionLimit = 256;
var path = new MinecraftPath();
var launcher = new CMLauncher(path);
```

### * Path Configuration

```csharp
static string profileDirectory = "profile";
static string profileFilePath = Path.Combine(profileDirectory, "profile.json");
```
## Screenshots

![App Screenshot](https://imgur.com/E2MQCs6.png)
![App Screenshot](https://imgur.com/XlJ4m9V.png)
![App Screenshot](https://imgur.com/P3s395Z.png)
![App Screenshot](https://imgur.com/H24FJfB.png)