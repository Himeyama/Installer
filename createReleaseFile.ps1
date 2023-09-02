$Date = Get-Date
$Year = $Date.Year.ToString()[-2..-1] -join ""
$Month = "{0:D2}" -f $Date.Month
$Day = "{0:D2}" -f $Date.Day
Compress-Archive -Path PotableInstaller,AppConfig.json,Setup.exe -DestinationPath "PotableInstaller-$Year.$Month.$Day.zip"
