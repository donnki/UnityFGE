#!/bin/sh
shellpath=`dirname $0`
cd $shellpath
# if [[ ! -d "lua" ]]; then
# 	mkdir lua
# fi

# if [[ ! -d "java" ]]; then
# 	mkdir java
# fi

if [[ ! -d "cs" ]]; then
	mkdir cs
fi

for filename in `ls` ; do
	if [ "${filename##*.}" = "proto" ]; then
		# echo "protoc --lua_out=./lua $filename"
		# protoc --lua_out=./lua $filename
		# protoc --java_out=./java $filename
		protogen -i:$filename -o:cs/${filename%.*}.cs  
	fi 
done

# cp ./lua/* ../../client/src/game/proto/
# cp ./java/com/loy/mobile/server/projectslg/model/proto/* /Volumes/MacintoshHD/Users/Loy/Projects/GameServer/trunk/server/41_ProjectSLGLogicServer/src/com/loy/mobile/server/projectslg/model/proto/