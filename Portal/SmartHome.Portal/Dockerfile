FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["SmartHome.Portal/SmartHome.Portal.csproj", "SmartHome.Portal/"]
RUN dotnet restore "SmartHome.Portal/SmartHome.Portal.csproj"
COPY . .
WORKDIR "/src/SmartHome.Portal"
RUN dotnet build "SmartHome.Portal.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SmartHome.Portal.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SmartHome.Portal.dll"]