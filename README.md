# SplitLogs
Elite Dangerous writes player events to journal files in the C:\Users\%username%\Saved Games\Frontier Developments\Elite Dangerous folder.
If you have multiple ED cmdrs, but only use one Windows account, the logs for all cmdrs goes into this folder. 

Usually this is not a problem, but if you want to upload your travel history to EdAstro.com to create a travel video,
you'd want to pick the correct journalfiles to upload.

This console application splits a zipped journal log into separate folders for each cmdr name found in the logs.

[![Build Status](https://quest-for-raxxla.visualstudio.com/_apis/public/build/definitions/2/badge)](https://quest-for-raxxla.visualstudio.com/EDNavigator/_build/index?definitionId=2)
