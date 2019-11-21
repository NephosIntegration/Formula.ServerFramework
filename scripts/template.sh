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
    dotnet new -i ./templates/
    echo ""
    echo ""
    echo "==========================="
    echo "Installed"
    echo "==========================="
    echo "You can now create a new server project by running"
    echo "dotnet new formulaserver -n HelloWorld.Web"
}

function uninstall() {
    uninstallCmd=$(dotnet new -u | grep Formula.ServerFramework | grep dotnet)
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
