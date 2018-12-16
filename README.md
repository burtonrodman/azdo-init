# dotnet azdo-init
A dotnet global tool to initialize (and update) "all the things" in Azure DevOps (TFS, VSTS) in a code as configuration / desired state manner.

# Project non-goals:
1. Reproduce any existing tools.

# Project goals:
1. [ ] create a dotnet global tool that...
2. [ ] if the current directory is not a git repo calls git init
3. [ ] if the current directory does not have a azdo-init template file creates one
4. [ ] FUTURE:  if the specified Team Project does not exist, create it
5. [ ] if the specified (remote) Repository does not exist in the specified Azure DevOps account URL, create it
6. [ ] if the specified (remote) Repository is not set as a remote for the local repo, add it
7. [ ] if the remote repository is empty, push the contents of the current directory
8. [ ] if a master.branch file exists, apply policies specified within.
9. [ ] if other \*.branch files exists, create the named branches and apply policies specified within.
10. [ ] (for TFS 2017) if .vsts-ci.yml exists, create or update a build for each branch.
11. [ ] if .vsts-cd.yml exists, create or update a release.
