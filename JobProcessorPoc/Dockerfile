FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["JobProcessorPoc/JobProcessorPoc.csproj", "JobProcessorPoc/"]
RUN dotnet restore "JobProcessorPoc/JobProcessorPoc.csproj"
COPY . .
WORKDIR "/src/JobProcessorPoc"
RUN dotnet build "JobProcessorPoc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JobProcessorPoc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobProcessorPoc.dll"]