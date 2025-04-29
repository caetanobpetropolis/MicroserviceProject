# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o csproj e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia tudo, incluindo o appsettings.json
COPY . ./

# Publica a aplicação em modo Release
RUN dotnet publish -c Release -o /app

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app

# Copia os arquivos publicados da etapa de build
COPY --from=build /app .

# Define o ambiente (opcional, se você quiser usar appsettings.Production.json)
ENV DOTNET_ENVIRONMENT=Production

# Comando que o container irá rodar
ENTRYPOINT ["dotnet", "OrderProcessor.dll"]