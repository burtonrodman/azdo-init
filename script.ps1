winget install Microsoft.AzureCLI
az extension add --name azure-devops
az login
az devops configure --defaults organization=https://dev.azure.com/burtonrodman project=scratch
$repositoryId = az repos create --name azcli-test | Convert-FromJson | Select-Object -First 1 -ExpandProperty id 

mkdir azcli-test
cd azcli-test
git init
git remote add origin https://burtonrodman@dev.azure.com/burtonrodman/scratch/_git/azcli-test

echo . >> README.md
git add .
git commit -m "add README.md"
git push --set-upstream origin master

git checkout -b dev
git push --set-upstream origin dev

az repos update --repository azcli-test --default-branch dev

az repos policy approver-count create --allow-downvotes false --blocking true --branch dev --creator-vote-counts false --enabled true --minimum-approver-count 1 --repository-id $repositoryId --reset-on-source-push true --branch-match-type exact
az repos policy work-item-linking create --repository-id $repositoryId --branch dev --branch-match-type exact --blocking true --enabled true
az repos policy comment-required create --repository-id $repositoryId --branch dev --branch-match-type exact --blocking true --enabled true