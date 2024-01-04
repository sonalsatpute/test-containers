FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_VER=0.0.0-alphagit 
ENV BUILD_VER=$BUILD_VER

# # Install Docker
# RUN apt-get update && \
#     apt-get install -y apt-transport-https ca-certificates curl gnupg lsb-release && \
#     curl -fsSL https://download.docker.com/linux/debian/gpg | apt-key add - && \
#     echo "deb [arch=amd64] https://download.docker.com/linux/debian $(lsb_release -cs) stable" | tee /etc/apt/sources.list.d/docker.list > /dev/null && \
#     apt-get update && \
#     apt-get install -y docker-ce docker-ce-cli containerd.io

RUN dotnet tool install -g dotnet-gcdump && \
   dotnet tool install -g dotnet-dump && \
   dotnet tool install -g dotnet-trace && \
   dotnet tool install -g dotnet-counters && \
   dotnet tool install --global dotnet-stack

WORKDIR /src

COPY ["./**/*.csproj", "./movies.sln",   "./"]

RUN for FILE in `ls *.csproj`; \
    do \
      directory_name=${FILE%.*}; \
#      echo $directory_name; \
      mkdir $directory_name; >/dev/null 2>&1; \
      mv $FILE $directory_name/$FILE; \
    done

RUN dotnet restore --source https://api.nuget.org/v3/index.json movies.sln

COPY . .

RUN dotnet build /p:Version=${BUILD_VER}  ./movies.sln && \
    dotnet publish /src/movies-api/movies-api.csproj -c Release -o /app/movies-api
#ENV TESTCONTAINERS_HOST_OVERRIDE=tcp://192.168.1.4:2375
COPY ./.testcontainers.properties /root/.testcontainers.properties
RUN dotnet test ./movies-api-integration-tests/movies-api-integration-tests.csproj --logger "console;verbosity=detailed"

FROM base AS final
WORKDIR /app

# Copy the dotnet tools from the build stage
COPY --from=build /root/.dotnet/tools/ /root/.dotnet/tools
ENV PATH="$PATH:/root/.dotnet/tools"

COPY --from=build /app/movies-api .
ENTRYPOINT ["dotnet", "movies-api.dll"]