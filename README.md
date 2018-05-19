# Shangrid

ソケット通信を使って別ツールからExcelで値の編集をできるようにするためのExcel Add-in  
ということにしているが実質Blenderのウェイト編集をするための仕組み

# 準備

## Excel側
1. Shangridをビルドする

## Blender側
1. BlenderAddonの中のshangridをディレクトリごとBlenderのAddonディレクトリに入れる

# 注意

BlenderがEditモードのままExcel上で値を編集するとウェイトの書き換えに失敗して通信が切れます  
Editモードで書き換えられないのはどうしようもないのでこれはもうそういう仕様

# 既知の不具合
- Excelのウィンドウを閉じてもExcelが終了せずにタスクが残ってしまう
    - タスクマネージャーから強制終了してください
    - VisualStudioからデバッグ実行で使うのが一番楽
