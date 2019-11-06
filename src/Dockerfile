FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o output -r linux-x64

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
RUN mkdir /data
VOLUME /data
WORKDIR /app
COPY --from=build-env /app/output .
ENTRYPOINT ["dotnet", "Subscriber.dll"]