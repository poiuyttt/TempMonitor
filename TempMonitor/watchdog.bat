@echo off
title TempMonitor Watchdog

set "EXE_PATH=%~dp0bin\Debug\TempMonitor.exe"

:restart
echo [%date% %time%] Starting TempMonitor...
start /wait "" "%EXE_PATH%"
echo [%date% %time%] Crashed or closed. Restarting in 5 seconds...
timeout /t 5 /nobreak >nul
goto restart
