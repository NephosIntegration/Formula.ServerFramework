#!/bin/bash
DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
cd $DIR/..

function listCommands() {
    echo ""
    echo "==========================="
    echo "Parameter - Description"
    echo "==========================="
	echo "install       - Install the Formula framework template"
	echo "uninstall     - Removes the Formula framework template"
    echo ""
}

function install() {
    dotnet new -i ./templates/Formula.MyApi/
    dotnet new -i ./templates/Formula.Make/
    echo ""
    echo ""
    echo "==========================="
    echo "Installed"
    echo "==========================="
    echo ""
    echo "Examples"
    echo "dotnet new formula-api -n HelloWorld.Api"
    echo "cd HelloWorld.Api"
    echo "dotnet new formula-resource -na HelloWorld.Api -r Hello"
}

function uninstall() {
    uninstallCmd=$(dotnet new -u | grep Formula.Make | grep dotnet)
    eval "$uninstallCmd"
    uninstallCmd=$(dotnet new -u | grep Formula.MyApi | grep dotnet)
    eval "$uninstallCmd"
    echo "==========================="
    echo "Uninstalled"
    echo "==========================="
}

if [ "install" = "${1,,}" ]; then
    install
elif [ "uninstall" = "${1,,}" ]; then
    uninstall
else
    listCommands
fi
