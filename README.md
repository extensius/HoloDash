### HoloDash


### Initial Steps

- Clone the repository:
  git clone https://github.com/Qazebulon/HoloDash.git

- Open folder in Unity

### UNITY Steps (Export the project from Unity to Visual Studio)

https://developer.microsoft.com/en-us/windows/mixed-reality/holograms_101e

In Unity select File > Build Settings.
Select Windows Store in the Platform list and click Switch Platform.
Set SDK to Universal 10 and Build Type to D3D.
Check Unity C# Projects.
Click Add Open Scenes to add the scene.
Click Player Settings....
In the Inspector Panel select the Windows Store logo. Then select Publishing Settings.
In the Capabilities section, select the Microphone and SpatialPerception capabilities.
Back in the Build Settings window, click Build.
Create a New Folder named "App".
Single click the App Folder.
Press Select Folder.
When Unity is done, a File Explorer window will appear.
Open the App folder.
Open the Visual Studio Solution.
Using the top toolbar in Visual Studio, change the target from Debug to Release and from ARM to X86.
Click on the arrow next to the Device button, and select HoloLens Emulator.
Click Debug -> Start Without debugging or press Ctrl + F5.
After some time the emulator will start the project. When first launching the emulator, it can take as long as 15 minutes for the emulator to start up. Once it starts, do not close it.
