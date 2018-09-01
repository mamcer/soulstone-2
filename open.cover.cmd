echo off

@REM author: m4mc3r@gmail.com

echo open cover script

pushd %~dp0
set start_time=%time%
set msbuild_bin="C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe"
set solution_name=Soulstone.sln
set solution_path=%CD%
set opencover_bin="..\tool\open.cover\OpenCover.Console.exe"
set mstest_bin="C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\mstest.exe"
set opencover_proj=open.cover.proj

@REM  ------------------------------------------------
@REM  Shorten the command prompt for making the output
@REM  easier to read.
@REM  ------------------------------------------------
set savedPrompt=%prompt%
set prompt=$$$g$s

@REM Rebuild solution
cd %solution_path%
call %msbuild_bin% /m %solution_name% /t:Rebuild /p:Configuration=Debug
@if %errorlevel% NEQ 0 GOTO error

@REM Run tests
call %msbuild_bin% /p:WorkingDirectory=%solution_path% /p:OpenCoverBinPath=%opencover_bin% /p:MSTestBinPath=%mstest_bin% %opencover_proj%
@if %errorlevel% NEQ 0 goto error
goto success

:error
echo an error has occurred.
GOTO finish

:success
echo process successfully finished.
echo start time: %start_time%
echo end time: %Time%

:finish
popd
set prompt=%savedPrompt%
pause

echo on