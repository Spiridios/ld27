@echo off
setlocal

IF [%1] == [] (
	set CWD=%CD%
) ELSE (
	set CWD=%1
)

set PROJECTNAME=SnapEncounters

cd C:\Development\libraries\jsil\JSIL\bin

del %CWD%\%PROJECTNAME%Jsil\jsil\*.* /s /q
jsilc -o %CWD%\%PROJECTNAME%Jsil\jsil %CWD%\%PROJECTNAME%.sln 1> %CWD%\build_jsil.log 2>&1 

endlocal