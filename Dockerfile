FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /app

# Step 1: Copy the solution and projects
COPY *.sln .
COPY ./*/*.csproj ./
RUN ls -1 *.csproj | sed -e 's/\.csproj$//' | xargs -I % sh -c 'mkdir -p %; mv %.csproj %'

# Step 2: Restore the nuget dependencies
RUN dotnet restore

# Step 3: Copy the code
COPY . .

# Step 4: Build the solution and (optionally) do the static analysis

RUN dotnet build /app/InterCom.Customer.API/InterCom.Customer.API.csproj -c Release -o /app/build

FROM build AS publish
WORKDIR /app
RUN dotnet test /app/Intercom.Customer.API.Tests/Intercom.Customer.API.Tests.csproj -c Release --logger:trx
RUN dotnet test /app/Intercom.Customer.Infrastructure.Tests/Intercom.Customer.Infrastructure.Tests.csproj -c Release --logger:trx
RUN dotnet publish /app/InterCom.Customer.API/InterCom.Customer.API.csproj -c Release -o /app/publish --no-restore


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InterCom.Customer.API.dll"]