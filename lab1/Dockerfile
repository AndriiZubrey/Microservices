FROM node:19-alpine3.16

WORKDIR /app

COPY ./package.json ./package.json

COPY ./package-lock.json ./package-lock.json

RUN npm i

COPY ./src ./src

CMD ["npm", "run", "start"]