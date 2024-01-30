copy "input.txt" "C:\\Git\\WordBinder\\WordBinder.Console"
cd WordBinder.Console
dotnet build && dotnet run
if %errorlevel% neq 0 pause && exit /b
