FROM node:21

WORKDIR /app


COPY package.json package-lock.json* ./
RUN npm install


RUN npm install -g @angular/cli

COPY . .


EXPOSE 4200

CMD ["ng", "serve", "--host", "0.0.0.0", "--port", "4200"]
