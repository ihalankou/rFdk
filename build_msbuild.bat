@echo off

set MsBuildFilePath="C:\Windows\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe"
set R_Exe="D:\apps\R\R-3.2.2\bin\R.exe"


cd Fdk2R
"%MsBuildFilePath%" RFdk.sln /target:Rebuild /p:Platform=x86 /p:Configuration=Release /maxcpucount /fl /flp:Verbosity=Normal;LogFile=build.Log /clp:NoItemAndPropertyList /verbosity:n /nologo
cd ..
%R_Exe% CMD BATCH build_r.r
move /Y rFdk_1.0.20151204.zip dist 
