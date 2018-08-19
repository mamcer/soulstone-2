IF NOT EXIST coverage.report\NUL GOTO NoCoverageReport
rmdir /s /q coverage.report
:NoCoverageReport
md coverage.report
C:\root\bin\report.generator\ReportGenerator.exe -reports:%CD%\opencover.xml -targetdir:.\coverage.report -reporttypes:Html