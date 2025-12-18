# Known issues:
 - ConnectionStrings removed from appsettings.json for security reasons!
# For local dev: 
- check DB, if non-exists:
appsettings.json:
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
