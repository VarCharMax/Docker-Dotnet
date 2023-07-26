#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine-arm64v8 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine-arm64v8 AS build
WORKDIR /src
COPY ["ExampleApp.csproj", "."]
RUN dotnet restore "./ExampleApp.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ExampleApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExampleApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY Scripts/ ./

RUN chmod +x installscript.sh
RUN chmod +x create-dotnet-user.sh

RUN ./installscript.sh

ENTRYPOINT ["dotnet", "ExampleApp.dll"]
