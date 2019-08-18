FROM mcr.microsoft.com/dotnet/core/sdk

WORKDIR app

COPY WebAPIExample/bin/Release/PublishOutput .

EXPOSE 12345

ENTRYPOINT ["dotnet", "WebAPIExample.dll"]