Import-Module WebAdministration

Write-Host **** Publishing Local Website ****
$site = "cloud-native.local"
$pool = (Get-Item "IIS:\Sites\$site"| Select-Object applicationPool).applicationPool
Restart-WebAppPool $pool
&dotnet publish .\local\api\cloud.native.local.api.csproj

Write-Host **** Running the tests ****
&dotnet test .\tests\cloud.native.tests.csproj