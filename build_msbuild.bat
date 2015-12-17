@echo off

set MsBuildFilePath="C:\Windows\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe"
set R_Exe="C:\Program Files\R\R-3.2.1\bin\R.exe"

if not exist %MsBuildFilePath% (
  echo Please set variable MsBuildFilePath to point to your MSBuild tool
  pause
)


if not exist %R_Exe% (
  echo Please set variable R_Exe to point to your R distribution
  pause
)


cd Fdk2R
"%MsBuildFilePath%" RFdk.sln /target:Rebuild /p:Platform=x86 /p:Configuration=Release /maxcpucount /fl /flp:Verbosity=Normal;LogFile=build.Log /clp:NoItemAndPropertyList /verbosity:n /nologo
cd ..
%R_Exe% CMD BATCH build_r.r
move /Y rFdk_1.0.20151204.zip dist 
pause