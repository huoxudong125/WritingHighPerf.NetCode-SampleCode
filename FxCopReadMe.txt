In order to build this solution, you need to install the FxCop SDK.

If you have Visual Studio Professional or better, then it has been included and rebranded Code Analysis in the IDE, 
but it is still FxCop underneath. On my machine, the relevant files are located in C:\Program Files (x86)\Microsoft Visual Studio 11.0\Team Tools\Static Analysis Tools\FxCop. 
If you cannot get access to the right version of Visual Studio, there are still a few options. The easiest way is from CodePlex at http://www.writinghighperf.net/go/32. 
If that project has disappeared by the time you read this, then try the Windows 7.1 SDK, which appears to have a broken web installer now, 
but you can get the ISO image at http://www.writinghighperf.net/go/33 and extract the installer from \Setup\WinSDKNetFxTools\cab1.cab. 
There is a file inside that archive that begins with the name WinSDK_FxCopSetup.exe. Extract that file and rename it to FxCopSetup.exe and you are on your way.

You then need to edit FxCopRules.csproj to update this value:

<FxCopSdkDir>C:\Program Files (x86)\Microsoft Fxcop 10.0</FxCopSdkDir>

to point to the correct location of FxCop executable and libraries.