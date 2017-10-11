Import-Module WebAdministration

$site = "cloud-native.local"
$pool = (Get-Item "IIS:\Sites\$site"| Select-Object applicationPool).applicationPool
Restart-WebAppPool $pool

&dotnet publish .\local\api\cloud.native.local.api.csproj