#!/bin/bash

echo "EiOuDonkey发布脚本,v0.0.2";
echo -n "输入此次发布的版本号:";
read -r version;
echo -n "清空之前的发布文件?[N/y]";
read -r if_clear;
if [[ $if_clear == "Y" ]] || [[ $if_clear == "y" ]] ;then 
    echo "清除中..."
    rm -r ./bin/Release;
fi;
echo -n "只发布当前平台?[Y/n]";
read -r choice;
if [[ "$choice" == "Y" ]] || [[ "$choice" == "y" ]];then
    echo "正在为当前平台发布...";
    dotnet publish -c Release -o "./bin/Release/specific/""$version""/";
    echo "正在复制资源文件..."
    mkdir "./bin/Release/specific/""$version""/Resources" ;
    cp -i -a ./Resources ./bin/Release/specific/"$version"/;
else
    echo -n "是否发布32位程序(仅Windows-x86)?[N/y]";
    read -r release_x86;
    if [[ "$release_x86" == "Y"  ]] || [[ "$release_x86" == "y" ]] ;then
        echo "正在发布win-x86..."
        dotnet publish -c Release -r win-x86 -o ./bin/Release/win-x86/net7.0/"$version" --self-contained true;
        echo "正在复制资源文件..."
        mkdir ./bin/Release/win-x86/net7.0/"$version"/Resources;
        cp -i -a ./Resources ./bin/Release/win-x86/net7.0/"$version"/;
    fi;
    echo "正在发布win-x64..."
    dotnet publish -c Release -r win-x64 -o ./bin/Release/win-x64/net7.0/"$version" --self-contained true;
    echo "正在复制资源文件..."
    mkdir ./bin/Release/win-x64/net7.0/"$version"/Resources;
    cp -i -a ./Resources ./bin/Release/win-x64/net7.0/"$version"/Resources;

    echo "正在发布linux-x64..."
    dotnet publish -c Release -r linux-x64 -o ./bin/Release/linux-x64/net7.0/"$version" --self-contained true;
    echo "正在复制资源文件..."
    mkdir ./bin/Release/linux-x64/net7.0/"$version"/Resources;
    cp -i -a ./Resources ./bin/Release/linux-x64/net7.0/"$version"/;

    echo "正在发布osx-x64..."
    dotnet publish -c Release -r osx-x64 -o ./bin/Release/osx-x64/net7.0/"$version" --self-contained true;
    echo "正在复制资源文件..."
    mkdir ./bin/Release/osx-x64/net7.0/"$version"/Resources;
    cp -i -a ./Resources ./bin/Release/osx-x64/net7.0/"$version"/;

    echo "正在清除多余文件..."
    rm  -r ./bin/Release/net7.0
fi;