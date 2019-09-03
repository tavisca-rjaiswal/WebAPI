FROM mcr.microsoft.com/dotnet/core/sdk

ARG PublishPath
ARG SOLUTION_DLL
ENV SOLUTION_DLL=${SOLUTION_DLL}

WORKDIR app

COPY ${PublishPath} .

EXPOSE 12345

ENTRYPOINT dotnet ${SOLUTION_DLL}