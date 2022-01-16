# NiceHash

This is a blazor webassembly application which can be used to monitor and manage your crypto mining from the popular hash power broker marketplace.

The application has Docker Compose support and has full gitlab CI/CD support


## Requirements
---
- A [NiceHash Account](https://www.nicehash.com/)
- NiceHash api keys, secret and org id (you can generate these from your nicehash account)

Additionally if you want Docker and full gitlab CI/CD support, you need docker installed, and a gitlab account and runner setup (refer to official gitlab documentation on how to setup the runner).

Remember to setup the Environmental variables on your local machine (to run locally) and in your gitlab account (to use gitlab CI/CD)!

## Run Locally with Docker
---

On Windows:
- Press START then start typing "env"
- Click on "edit the system environment variables"
- On the next window, click on "Environment Variables" at the bottom
- On the "System variables" section click "New..."
- Add: NICEHASH_API_SECRET (your api secret, from NiceHash)
- Add: NICEHASH_API_KEY (your api key, from NiceHash)
- Add: NICEHASH_ORG_ID (your org id, from NiceHash)
- Click Ok, then Apply, then Ok

Next, open the command line
cd to where the project is located and type:

```
docker network create nicehash
```
and then:
```
docker run -d -p 5002:5002 --env HOMEASSISTANT_TOKEN=$HOMEASSISTANT_TOKEN --env NICEHASH_API_SECRET=$NICEHASH_API_SECRET --env NICEHASH_API_KEY=$NICEHASH_API_KEY --env NICEHASH_ORG_ID=$NICEHASH_ORG_ID --network nicehash --name NiceHash nicehash
```

The website should be up and running and you'll be able to reach it at localhost:5002 from your browser.

If this fails, ensure the environmental variables have been created correctly and that the values are correct.

## Deploy Using Gitlab CI/CD
Note: this requires the gitlab runner to run on the server where you will deploy, and the runner to have docker-in-docker enabled! Refer to official Gitlab documentation to enable this.

The easiest way to deploy to your own server, is to create a new gitlab repository, and change the solution remote repository to match the new one

### Configure gitlab
- Create a new empty repository
- Click on "Settings" 
- Expand the "Variables" section
- Add the following variables, ensured you check "protected variable" and "mask variable":  
  - NICEHASH_API_SECRET (your api secret, from NiceHash)
  - NICEHASH_API_KEY (your api key, from NiceHash)
  - NICEHASH_ORG_ID (your org id, from NiceHash)

### Change git remote repository
Then, you need to change the git repo url so that it will push to your personal repository.

The easiest and quickest way is to:
- Delete the .git folder within the NiceHash solution folder
- Create a new empty repository in gitlab
- Clone the empty repository somewhere
- Copy the .git folder from the new repo to the NiceHash solution folder
- Push

Now you should be able to see the CI progress within the CI/CD section :)


