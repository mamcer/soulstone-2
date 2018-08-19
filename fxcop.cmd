echo off

echo fxcop
echo author: Mario Moreno (mario.moreno@live.com)

rem set local variables
set start_time=%time%
set current_dir=%cd%
set fxcop_cmd_path=..\tool\fxcop\FxCopCmd.exe
set ruleset_file_path=FXCop.ruleset
set consolexls_path=..\tool\fxcop\Xml\VSConsoleOutput.xsl
 
rem assembly path could be a file or a folder

echo execute analysis
"%fxcop_cmd_path%" /c /rs:="%ruleset_file_path%" /consolexsl:"%consolexls_path%" /f:%CD%\Common\CrossCutting.MainModule\bin\Debug\CrossCutting.Core.dll /f:%CD%\Common\CrossCutting.MainModule\bin\Debug\CrossCutting.MainModule.dll
@if %ERRORLEVEL% NEQ 0 GOTO Error

echo process successfully finished.
GOTO End

:Error
echo an error has ocurred. 

:End
cd %current_dir%
echo start time: %start_time%
echo end time: %time%
pause

echo on