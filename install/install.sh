echo "EiOuDonkey安装脚本,v0.0.1 , for linux , 使用 "sudo" ";
echo -n "输入要安装/发布的版本:"
read -r version;
echo -n "重新为当前平台发布?[Y/n]";
read -r choice;
if [[ "$choice" == "Y" ]] || [[ "$choice" == "y" ]];then
    echo "正在为当前平台发布...";
    dotnet publish -c Release -o "./bin/Release/specific/""$version""/";
    echo "正在复制资源文件..."
    mkdir "./bin/Release/specific/""$version""/Resources" ;
    cp -i -a ./Resources ./bin/Release/specific/"$version"/;
fi

echo "正在清除链接..."
rm -f /bin/eodon

echo "正在复制已编译的文件..."
mkdir -p /bin/EiOuDonkey/""$version""
cp -i -a ./bin/Release/specific/""$version"" /bin/EiOuDonkey/""$version""

echo "正在创建新的链接..."
ln -s /bin/EiOuDonkey/""$version""/EiOuDonkey /bin/eodon

echo "正在设置权限..."
chmod +x /bin/eodon