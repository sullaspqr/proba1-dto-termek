# This ASP.NET WEB API backend only for educational purposes!
# Known issues:
 - ConnectionStrings removed from appsettings.json for security reasons! XD
# For local dev: 
- appsettings.json:
{
    "ConnectionStrings": {
      "DefaultConnection": "server=localhost;port=3306;database=cartdb;user=root;password="
    },
    "Logging": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Warning"
      }
    },
    "AllowedHosts": "*"
  }
  # Check DB, cartdb:
  - File included in cartdb.sql, non-production version
