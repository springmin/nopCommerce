#!/bin/sh
# 一键启动 nopCommerce (Nop.Web) —— OpenHarmony dotnet 11 SDK 运行环境
# 用法: ./run-nop.sh            # 默认监听 http://localhost:5000
#       ./run-nop.sh --urls "http://0.0.0.0:8080"   # 自定义监听地址
set -e

# 脚本所在目录即仓库根
SCRIPT_DIR=$(cd "$(dirname "$0")" && pwd)
WEB_DIR="$SCRIPT_DIR/src/Presentation/Nop.Web"

# ---- OHOS 沙箱运行必需环境 ----
export DOTNET_ROOT="$HOME/.dotnet"
export PATH="$DOTNET_ROOT:$DOTNET_ROOT/tools:$PATH"
export TMPDIR=/data/storage/el2/base/tmp
export DOTNET_ROLL_FORWARD=LatestMajor          # net10.0 应用跑在 11.0 共享框架上
export DOTNET_EnableWriteXorExecute=0           # W^X 兼容
export LD_LIBRARY_PATH="$HOME/.harmonybrew/lib" # ICU (非 invariant) 全球化
export TZDIR="$HOME/.dotnet/test-tools/zoneinfo"
unset DOTNET_SYSTEM_GLOBALIZATION_INVARIANT

# ---- 启动 (用托管 DLL，不用未 codesign 的 apphost) ----
cd "$WEB_DIR"
exec dotnet bin/Release/net10.0/Nop.Web.dll --urls "http://localhost:5000" "$@"
