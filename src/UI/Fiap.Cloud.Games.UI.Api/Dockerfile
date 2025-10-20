# ============================
# 🔹 BASE RUNTIME
# ============================
FROM joaomesquitademetrio/aspnet8-jammy-with-fonts-timezone-excel:latest AS base
WORKDIR /app
EXPOSE 80
ENV TZ=America/Sao_Paulo
ENV ASPNETCORE_HTTP_PORTS=80


# ============================
# 🔹 BUILD & RESTORE
# ============================
FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
WORKDIR /src

# Define cache NuGet compartilhado entre etapas
ENV NUGET_PACKAGES=/root/.nuget/packages

# Copia apenas os csproj (melhor cache de dependências)
COPY ["utility/Sample.Utils/Sample.Utils.csproj", "utility/Sample.Utils/"]
COPY ["src/Core/Fiap.Cloud.Games.Core.Infra/Fiap.Cloud.Games.Core.Infra.csproj", "src/Core/Fiap.Cloud.Games.Core.Infra/"]
COPY ["src/Core/Fiap.Cloud.Games.Core.Domain/Fiap.Cloud.Games.Core.Domain.csproj", "src/Core/Fiap.Cloud.Games.Core.Domain/"]
COPY ["src/Core/Fiap.Cloud.Games.Core.Ioc/Fiap.Cloud.Games.Core.Ioc.csproj", "src/Core/Fiap.Cloud.Games.Core.Ioc/"]
COPY ["src/Core/Fiap.Cloud.Games.Core.Application/Fiap.Cloud.Games.Core.Application.csproj", "src/Core/Fiap.Cloud.Games.Core.Application/"]
COPY ["src/UI/Fiap.Cloud.Games.UI.Api/Fiap.Cloud.Games.UI.Api.csproj", "src/UI/Fiap.Cloud.Games.UI.Api/"]

# Restore (com cache e path consistente)
RUN dotnet restore "src/UI/Fiap.Cloud.Games.UI.Api/Fiap.Cloud.Games.UI.Api.csproj" --disable-parallel --force-evaluate

# Copia o restante do código
COPY . .

# Compila e publica em uma única etapa
WORKDIR /src/src/UI/Fiap.Cloud.Games.UI.Api
RUN dotnet publish "Fiap.Cloud.Games.UI.Api.csproj" -c Release -p:Platform=x64 -o /app/publish


# ============================
# 🔹 FINAL IMAGE
# ============================
FROM base AS final
WORKDIR /app
ENV TZ=America/Sao_Paulo
ENV ASPNETCORE_HTTP_PORTS=80

# Copia apenas os artefatos publicados
COPY --from=build /app/publish .

# Entrada do container
ENTRYPOINT ["dotnet", "Fiap.Cloud.Games.UI.Api.dll"]
