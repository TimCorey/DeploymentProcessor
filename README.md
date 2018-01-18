# Deployment Processor
A simple .NET application to backup and restore deployment files.
## Usage ##
To use the application, run the WinForm application. It will tell you which files can be acted upon (ones who have a corresponding "_Staging" folder). You select the folders first, in the order you want them processed, and then choose the action you want performed.
### Deploy ###
This action will zip the folders to be backed up, then copy the files to the new location, overwriting any it needs to.
### Restore ###
This action will take the latest zip backup file for the given folder, clear the target location and then extract the backup to the newly-cleared location.
### Backup ###
This just backs up the given folder(s). This action is performed as part of the deploy process as well.
## Roadmap ##
The following features/tasks need to be completed on this project:
 1. Refactor the code to make it efficient and DRY.
 2. Remove magic strings and replace them with updateable config options.
 3. Log whenever there is an error.
 4. Alert the user when files have been changed outside of the release process.