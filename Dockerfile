# Imagem base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Imagem para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src

# Copia os .csproj separadamente para melhor cache
COPY Isthmus.Produtos.Api/Isthmus.Produtos.Api.csproj Isthmus.Produtos.Api/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/

# Restaura as dependências
RUN dotnet restore Isthmus.Produtos.Api/Isthmus.Produtos.Api.csproj

# Copia todo o restante
COPY . .

# Compila
WORKDIR /src/Isthmus.Produtos.Api
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Publica
FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish /src/Isthmus.Produtos.Api/Isthmus.Produtos.Api.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Isthmus.Produtos.Api.dll"]
