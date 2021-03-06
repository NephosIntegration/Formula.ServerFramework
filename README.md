# Formula.ServerFramework
Templates for .Net core server side Formula framework

# Getting Started

```bash
git clone https://github.com/NephosIntegration/Formula.SimpleRepo.git
git clone https://github.com/NephosIntegration/Formula.SimpleAPI.git
git clone https://github.com/NephosIntegration/Formula.ServerFramework.git
mkdir App
./Formula.ServerFramework/scripts/template.sh install
cd App
dotnet new formulaserver -n HelloWorld.Web
cd HelloWorld.Web/HelloWorld.Web
dotnet run
```
Browse to https://localhost:5001/check

# Notes for building Formula.MyApi
[Create a web API](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-3.0&tabs=visual-studio-code)

```bash
cd ./templates
dotnet new webapi -o Formula.MyApi
```

- Remove weatherforcast files and replace references with myapi (./Properties/launchSettings.json)
- Add project references to Formula.SimpleAPI in Formula.MyApi.csproj


# Packages / Projects Used
- [Microsoft.VisualStudio.Web.CodeGeneration.Tools](https://github.com/aspnet/scaffolding)
- [Microsoft.EntityFrameworkCore.Tools.DotNet](https://github.com/aspnet/EntityFramework.Tools)


# Relavant Links
- [Custom templates for dotnet new](https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates)
- [Templates for the CLI](https://docs.microsoft.com/en-us/dotnet/core/tutorials/cli-templates-create-item-template)
- [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)
- [dotnet/templating GitHub repo Wiki](https://github.com/dotnet/templating/wiki)
- [How to create your own templates for dotnet new](https://devblogs.microsoft.com/dotnet/how-to-create-your-own-templates-for-dotnet-new/)
