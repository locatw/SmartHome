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
ARG GOOGLE_APPLICATION_CREDENTIALS
ENV GOOGLE_APPLICATION_CREDENTIALS=$GOOGLE_APPLICATION_CREDENTIALS
RUN rm $GOOGLE_APPLICATION_CREDENTIALS
RUN dotnet build "SmartHome.Portal.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SmartHome.Portal.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ARG GOOGLE_APPLICATION_CREDENTIALS
ARG PROJECT_ID
ARG IOT_CORE_REGION
ARG IOT_CORE_REGISTRY_ID
ENV GOOGLE_APPLICATION_CREDENTIALS=$GOOGLE_APPLICATION_CREDENTIALS
ENV PROJECT_ID=$PROJECT_ID
ENV IOT_CORE_REGION=$IOT_CORE_REGION
ENV IOT_CORE_REGISTRY_ID=$IOT_CORE_REGISTRY_ID
COPY "SmartHome.Portal/${GOOGLE_APPLICATION_CREDENTIALS}" .
ENTRYPOINT ["dotnet", "SmartHome.Portal.dll"]