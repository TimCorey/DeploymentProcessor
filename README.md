# DeploymentProcessor
A simple .NET application to backup and restore deployment files.
## Usage ##
To use the application, run the Console application. It will prompt you for one of three actions: Deploy, Rollback, or Backup. It will then ask you which folders, in which order, should be backed up. You specify the folders by their given number (comma-delimited). The folders are currently identified as the ones that have a corresponding "_Staging" folder.
### Deploy ###
This action will zip the folders to be backed up, then copy the files to the new location, overwriting any it needs to.
### Restore ###
This action will take the latest zip backup file for the given folder, clear the target location and then extract the backup to the newly-cleared location.
### Backup ###
This just backs up the given folder(s). This action is performed as part of the deploy process as well.
## Roadmap ##
The following features/tasks need to be completed on this project:
1. Refactor the code to make it efficient and DRY.
2. Move most of the code to the class library.
3. Add a GUI front-end (probably WPF to take advantage of threading and progress bar functionality).
4. Remove magic strings and replace them with updateable config options.