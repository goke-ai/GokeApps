@echo off

set srcFolderX=C:\Users\gokel\OneDrive\zs\GokeApps\Goke\Core\Entities
set desFolderX=C:\Users\gokel\OneDrive\zs\GokeApps\Goke.Core.Entities

set desFolderPart=..\..\Goke.Core

set filename=Entities
set desFolder=%desFolderPart%.%filename%\Auto\
xcopy %filename%\*.cs "%desFolder%" /s /e /exclude:copyit-xlist.txt

rem xcopy Dtos\*.cs "..\..\..\FamilyMenu\Ark.FamilyMenu.Dtos\Auto" /s /e /exclude:copyit-xlist.txt
set filename=Dtos
set desFolder=%desFolderPart%.%filename%\Auto\
xcopy %filename%\*.cs "%desFolder%" /s /e /exclude:copyit-xlist.txt

set filename=Data
set desFolder=%desFolderPart%.%filename%\Auto\
xcopy %filename%\*.cs "%desFolder%" /s /e /exclude:copyit-xlist.txt

