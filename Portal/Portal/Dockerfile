FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Portal/Portal.csproj", "Portal/"]
RUN dotnet restore "Portal/Portal.csproj"
COPY . .
WORKDIR "/src/Portal"
RUN dotnet build "Portal.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Portal.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Portal.dll"]