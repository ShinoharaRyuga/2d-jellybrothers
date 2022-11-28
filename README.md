# リポジトリについて

## 開発環境
Unity 2021.3.0f1<br>
Visual Studio 2022<br>
Sourcetree<br>

## 概要
協力型パズルアクションゲーム<br>
プレイ人数 2人  
ステージ制 (５ステージ)<br>
[プレイ動画](https://youtu.be/PYXJHTZMtz0)

## 命名規則・コーディング規約
・メンバー変数には変数名の先頭に_(アンダースコア)をつける  
・メンバー変数の参照にはプロパティか関数を使用する   
・変数・関数にはTooltipやsummaryを使ってコメントをつける  
・クラスの説明を書く  
・コメントを適度にのこす 

[こんな感じ](https://github.com/ShinoharaRyuga/2d_jellybrothers/blob/master/Assets/Shinohara/Scripts/RespawnManager.cs)

## Issues・Pull requests
・Issuesを作成したらProjectsに追加する  
・Pull requests作成時にPull requests, commits, Issuesをひとまとまりにする　↓以下画像 イメージ

![githubイメージ画像](https://user-images.githubusercontent.com/86392648/178971064-3bb6d023-1e97-45d7-83ba-3d87d4eec8ec.png)
参考資料  
[Pull requestsをIssuesとリンクさせる](https://tonari-it.com/github-issue-close/)  
[Pull requestでIssueをcloseする方法](https://qumeru.com/magazine/617)

## フォルダについて
Assetsフォルダの直下に以下フォルダを作成しました。  
作成したゲームオブジェクトや使用する画像、BGM、SEなどは該当するフォルダに入れて下さい。  
○○Tmpというプレハブがフォルダに入っていたら削除して下さい。

**・Animations**  
　アニメーションとアニメーションコントローラーを入れて下さい。  

**・Audios**  
　Audiosフォルダ直下にBGMとSEを入れるフォルダがあるので該当するフォルダに入れて下さい。

**・Images**  
　ゲームで使用する画像(スプライト)を入れて下さい。  

**・Prefabs**    
　ステージギミックなどの**動的生成しない**プレハブを入れて下さい。

**・Resources**  
　プレイヤーや敵などの**動的生成する**プレハブを入れて下さい。  

**・Scene**  
　実際にゲームで使用するシーンを入れて下さい。


## 要素案
[要素案](https://drive.google.com/drive/folders/1EzZpdk03KjzjG029Iuoia5yaIyrEV4Nw)

## App Id PUN
669b79b7-082d-4a9b-b7f1-0ca39ab605d6
