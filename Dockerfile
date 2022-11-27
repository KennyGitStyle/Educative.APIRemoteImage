FROM gcr.io/educative-exec-env/gui-app:1.0

ARG DEBIAN_FRONTEND=noninteractive

RUN apt-get update && apt-get install -y wget && \
    apt-get install -y libgconf-2-4 libatk1.0-0 libatk-bridge2.0-0 libgdk-pixbuf2.0-0 libgtk-3-0 libgbm-dev libnss3-dev libxss-dev

RUN wget https://dl.pstmn.io/download/latest/linux64 -O postman.tar.gz

RUN tar -xzf postman.tar.gz -C /opt

RUN ln -s /opt/Postman/Postman /usr/bin/postman

COPY ./postman.desktop /.local/share/applications/

RUN apt-get update && apt-get install wget -y

RUN wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && dpkg -i packages-microsoft-prod.deb && rm packages-microsoft-prod.deb

RUN apt-get update && apt-get install -y apt-transport-https && apt-get update && apt-get install -y dotnet-sdk-6.0 aspnetcore-runtime-6.0 dotnet-runtime-6.0

RUN apt-get update && apt-get install -y apt-transport-https && apt-get update && apt-get install -y dotnet-sdk-7.0 aspnetcore-runtime-7.0 dotnet-runtime-7.0

# Copy everything else and build
COPY . .

RUN DEBIAN_FRONTEND="noninteractive" apt-get -y install tzdata &&\
    apt-get install curl git software-properties-common -y &&\
    curl -sL https://deb.nodesource.com/setup_16.x -o nodesource_setup.sh &&\
    bash nodesource_setup.sh &&\
    apt-get install nodejs -y && npm install -g http-server && git clone https://github.com/swagger-api/swagger-editor.git

RUN curl -sL https://aka.ms/InstallAzureCLIDeb | bash
