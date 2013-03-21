rmdir /Q /S .\buildoutput

C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe /t:ResolveReferences;_WPPCopyWebApplication /p:BuildingProject=true;OutDir=..\build\buildoutput\;configuration=Release ../GoodlyFere.Import.Web/GoodlyFere.Import.Web.csproj

cd ..\build
"C:\Program Files (x86)\WinZip\winzip32.exe" -a .\ImportWebPackage_%1.zip .\buildoutput\_PublishedWebsites\GoodlyFere.Import.Web\*

rmdir /Q /S .\buildoutput