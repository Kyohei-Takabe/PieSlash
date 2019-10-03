///////////////////////////////////////////////////////////////////////////////////////
"Interactive Water"は主にVRChatでの使用を想定したUnity5.6.3pのパッケージです。
以下の利用規約に従って使用してください。

※利用規約
・内容物は自由に使用・改変していただいて構いません。
・再配布の場合はtwitterまで連絡を下さい(@rakuraku_vtube)
・動作の安定性は保証できません。

///////////////////////////////////////////////////////////////////////////////////////
"Interactive Water" is Unity 5.6.3p package mainly assumed using in VRChat.
Follow terms of use:

※Terms of Use
・You are allowed use and modify items of this package.　
・If you want to share this package or modified item of this package, please contact my twitter acount(@rakuraku_vtube).
・Operational stability can not be guaranteed.


///////////////////////////////////////////////////////////////////////////////////////
＊使用方法
(VRChatでの使用でなければ1,2を飛ばす)
1.VRCSDKをインポート
2.本パッケージをインポート
3.VRChat SDK --> Show Build Control Panel --> Set Up Layer
4."SimuBoard"-->Layer-->AddLayerから、UserLayer22に"Quad"を追加
5."SimuBoard"と"DrawBoard"のLayerを"Quad"に設定
6."SimCamera"のCullingMaskを"Quad"に設定
7."InterCamera"のCullingMaskを水に影響を与えるレイヤーに設定。(LocalPlayer,Player等)

＊使用のポイント
SimBoardのInspectorからシェーダーのパラメーターがいくつか設定できます。
減衰係数や水の伝搬速度を変えながら良い見た目になるように調整して下さい。
場合によっては解が安定せず水が振動してしまう場合があるので、そのときは伝搬速度を落として下さい。

水面を広げたい場合は"DrawBoard"の大きさを大きくして、それに合わせて"InterCamera"のSizeを大きくして下さい。
その際に水の伝搬速度を多少落とすと良さそうです。


///////////////////////////////////////////////////////////////////////////////////////////
＊HOW TO USE
1.Import VRCSDK 
2.Import this package.
3.VRChat SDK --> Show Build Control Panel --> Set Up Layer
4."SimuBoard"-->Layer-->AddLayer-->Add "Quad" on "User Layer22"
5.Set layer "Quad" to "SimuBoard" and "DrawBoard"
6.Set "Quad" to culling mask of "SimCamera"
7.Set the layer you want to use to interact water to culling mask of "InterCamera" 

You can tune simulation parameter throught inspector of "SimBoard".
If the stability of the calculation result of the water surface is lost, try lowering the propagation speed.

If you want to expand the surface of the water, increase the size of "DrawBoard" and increase the size of "InterCamera" accordingly.
In that case, it seems good to decrease the propagation speed of water.

/////////////////////////////////////////////////////////////////////////////////////////////

2018/7/04 らくとあいす
Contact (Twitter:@rakuraku_vtube)