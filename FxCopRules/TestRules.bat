@ECHO OFF

set FxCopDir=C:\Program Files (x86)\Microsoft Fxcop 10.0\
set FxCopExe=FxCopCmd.Exe
set FxCopCmd=%FxCopDir%%FxCopExe%

set CustomRuleAssembly=.\FxCopRules.dll
set TargetFile=.\FxCopViolator.dll

ECHO CommandLine: %FxCopCmd%

"%FxCopCmd%" /out:.\FxCopOutput.xml /rule:%CustomRuleAssembly% /file:%TargetFile%