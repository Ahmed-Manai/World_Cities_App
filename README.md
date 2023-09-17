World Cities API 
============================================================

## Secrets Strorage 
(Manage User Secrets VS)
secrets.json file

## Entity FrameWork

### initial migration 

dotnet tool install --global dotnet-ef
dotnet ef migrations add "Initial" -o "Data/Migrations"
dotnet ef database update

### to undo creation 
ef migrations remove

### to import Cities and Countries 
use api/Seed/Import endpoint 

## Package for xlsx files
Install-Package EPPlus -Version 4.5.3.3

## Data Base Creation 

CREATE DATABASE DBtempWC

DROP LOGIN woorld2

DROP DATABASE testDBWorldCities


CREATE LOGIN tempWC
WITH PASSWORD = 'hfdkHG1235*+'

CREATE USER tempWC FOR LOGIN tempWC
WITH DEFAULT_SCHEMA = DBtempWC

-- not working 
USE DBtempWC
ALTER ROLE db_owner ADD MEMBER tempWC


World Cities FrontEnd 
============================================================

ng generate component Cities --module=app --skip-tests

# Client-Side Application 
# Server-Side Application
# Sorting

