FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Expone el puerto en el que se ejecutará la aplicación
EXPOSE 8080

# Copia los archivos del proyecto y restaura las dependencias
COPY *.csproj .
RUN dotnet restore

# Copia el resto del código fuente y compila la aplicación
COPY . .
RUN dotnet publish -c Release -o out

# Imagen base para .NET 6 ASP.NET Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Inicia la aplicación cuando el contenedor se ejecute
ENTRYPOINT ["dotnet", "Parcial2.dll"]
