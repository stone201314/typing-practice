@echo off
echo ========================================
echo 青少年打字练习软件 - 打包脚本
echo ========================================
echo.

REM 检查 .NET SDK
dotnet --version >nul 2>&1
if errorlevel 1 (
    echo 错误: 未安装 .NET SDK
    echo 请从以下地址下载安装:
    echo https://dotnet.microsoft.com/download/dotnet/8.0
    pause
    exit /b 1
)

echo 正在打包发布版本...
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true

if errorlevel 1 (
    echo 错误: 打包失败
    pause
    exit /b 1
)

echo.
echo ========================================
echo 打包成功！
echo.
echo 输出目录: bin\Release\net8.0-windows\win-x64\publish\
echo.
echo 可执行文件: TypingPractice.exe
echo.
echo 你可以将 publish 目录复制到其他电脑直接运行
echo ========================================
pause
