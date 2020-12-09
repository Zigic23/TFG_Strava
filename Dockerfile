#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /Proyecto
COPY ["Proyecto/StravaTrainingGenerator/StravaTrainingGenerator.csproj", "StravaTrainingGenerator/"]
COPY ["Proyecto/BussinessLogicLayer/BussinessLogicLayer.csproj", "BussinessLogicLayer/"]
COPY ["Proyecto/DatabaseAccessLayer/DatabaseAccessLayer.csproj", "DatabaseAccessLayer/"]
RUN dotnet restore "StravaTrainingGenerator/StravaTrainingGenerator.csproj"
COPY Proyecto .
WORKDIR "/Proyecto/StravaTrainingGenerator"
RUN dotnet build "StravaTrainingGenerator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StravaTrainingGenerator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StravaTrainingGenerator.dll"]