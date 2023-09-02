rm -r -fo PotableInstaller
rm -r -fo Installer\bin
rm -r -fo Installer\obj
# dotnet build .\Installer\Installer.csproj -c Release -r win10-x64
dotnet publish .\Installer\Installer.csproj -c Release -r win-x64
cp -r Installer\bin\x64\Release\net7.0-windows10.0.19041.0\win-x64\publish PotableInstaller

rm -r -fo Setup.exe
rm -r -fo Setup\bin
rm -r -fo Setup\obj
dotnet publish .\Setup\Setup.csproj -c Release -r win-x64
cp .\Setup\bin\Release\net7.0\win-x64\publish\Setup.exe .
