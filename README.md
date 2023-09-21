# NiceHash

This is a blazor webassembly application which can be used to monitor and manage your crypto mining from the popular hash power broker marketplace.

The application has Docker Compose support and has full gitlab CI/CD support

Notes: This app was designed primarily for mobile. As well as this, it is still under developments and some errors might not be handled correctly.

#### Dark mode Overview:
![alt text](https://github.com/Joe85gr/nicehash/blob/main/imgs/dark-mode.png?raw=true)

#### Light mode Overview:
![alt text](https://github.com/Joe85gr/nicehash/blob/main/imgs/light-mode.png?raw=true)


## Requirements
---
- A [NiceHash Account](https://www.nicehash.com/)
- NiceHash api keys, secret and org id (you can generate these from your nicehash account)

Additionally if you want Docker and full gitlab CI/CD support, you need docker installed, and a gitlab account and runner setup (refer to official gitlab documentation on how to setup the runner).

Remember to setup the Environmental variables on your local machine (to run locally) and in your gitlab account (to use gitlab CI/CD)!

## Run Locally with Docker
---
Install Docker following the [official guide](https://docs.docker.com/engine/install/).

On Windows:
- Press START then start typing "env"
- Click on "edit the system environment variables"
- On the next window, click on "Environment Variables" at the bottom
- On the "System variables" section click "New..."
- Add: NICEHASH_API_SECRET (your api secret, from NiceHash)
- Add: NICEHASH_API_KEY (your api key, from NiceHash)
- Add: NICEHASH_ORG_ID (your org id, from NiceHash)
- Click Ok, then Apply, then Ok

On Mac:
On the terminal, type:
```
~/.bash-profile
nano .bash_profile
```
and add the following lines:
- export NICEHASH_API_SECRET=[your api secret, from NiceHash]
- NICEHASH_API_KEY=[your api key, from NiceHash]
- NICEHASH_ORG_ID=[your org id, from NiceHash]

Next, open the command line
cd to where the project is located and type:

```
docker network create nicehash
```
and then:
```
docker run -d -p 5002:5002 --env NICEHASH_API_SECRET=$NICEHASH_API_SECRET --env NICEHASH_API_KEY=$NICEHASH_API_KEY --env NICEHASH_ORG_ID=$NICEHASH_ORG_ID --network nicehash --name NiceHash nicehash
```

The website should be up and running and you'll be able to reach it at localhost:5002 from your browser.

If this fails, ensure the environmental variables have been created correctly and that the values are correct.
