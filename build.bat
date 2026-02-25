@echo off
echo ========================================
echo 青少年打字练习软件 - 编译脚本
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

echo 正在还原 NuGet 包...
dotnet restore
if errorlevel 1 (
    echo 错误: NuGet 包还原失败
    pause
    exit /b 1
)

echo.
echo 正在编译项目...
dotnet build --configuration Release
if errorlevel 1 (
    echo 错误: 编译失败
    pause
    exit /b 1
)

echo.
echo ========================================
echo 编译成功！
echo.
echo 输出目录: bin\Release\net8.0-windows\
echo.
echo 运行程序: dotnet run --configuration Release
echo 或直接运行: bin\Release\net8.0-windows\TypingPractice.exe
echo ========================================
pause
