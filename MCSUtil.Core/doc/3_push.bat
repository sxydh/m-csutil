@echo off
setlocal

for /f "delims=" %%i in ('dir /b /o-d *.nupkg') do (
    set "latest_nupkg=%%i"
    goto :found
)

:found
if not defined latest_nupkg (
    echo "没有找到任何 nupkg 文件"
    exit /b 1
)

set /p api_key="请输入密钥："

nuget push "%latest_nupkg%" "%api_key%" -Source https://api.nuget.org/v3/index.json

endlocal
