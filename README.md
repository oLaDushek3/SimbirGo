# SimbirGo

1 Сreate a database using the script from simbir_go dbScript.txt  
2 Change сonnectionString in appsettings.json  
3 cd [project path]  
4 dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 7.0.13  
5 dotnet add package Microsoft.AspNetCore.OpenApi --version 7.0.13  
6 dotnet add package Microsoft.EntityFrameworkCore --version 7.0.13  
7 dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0-rc.2.23480.1  
8 dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.0-rc.2.23480.1  
9 dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 8.0.0-rc.2  
10 dotnet add package Swashbuckle.AspNetCore --version 6.5.0  
11 dotnet run --launch-profile "http"  

## URL: http://localhost:5256/swagger/index.html
