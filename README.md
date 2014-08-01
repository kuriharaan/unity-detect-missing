unity-detect-missing
====================

UnityでPrefabの参照がMissingになっているものを検出するeditorスクリプトです。

## Overview

Unityで作業しているとなんらかの理由でInspectorのさしている参照先が  
missingになってしまうことがあります。
プロジェクト内のprefabファイルからmissing参照を探して警告を出力します。

## プロジェクト設定

Editor Settingsの  
[Version Control]を[Visible Meta Files]に  
[Asset Serialization]を[Force Text]にしてください。  

## 使い方

メニューの[Tools] > [MissingReference]を選択してください。  
missingを見つけた場合、Consoleに警告として該当のprefabのパスを表示します。

## 仕組み

Editor/MissingDetection.csがソースファイルです。
Assetフォルダ以下のprefabファイルの中をテキストとして開き、調べます。
アセットの参照がある場合、guidが記述されています。
missingの場合はこのguidに対応するアセットがプロジェクトに存在しません。
(noneの場合はguidは無い)
AssetDatabase.GUIDToAssetPath(guid)を呼んでパスが取得できない場合、  
そのアセットへの参照はmissingとなっています。



