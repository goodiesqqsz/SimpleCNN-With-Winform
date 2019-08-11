# SimpleCNN-With-Winform

## 为什么要做这个

&emsp;&emsp;从前发现在 **dotnet framework** 的网络应用上， **tensorflow sharp** 没法正常通过编译，调了几天没调好，后来就放弃拓展。

&emsp;&emsp;最近发现 **winform** 创建 `graph` 的时候貌似可以正常编译，这又让我重新燃起了希望——做点简单的但是却有点意思的东西。

&emsp;&emsp;以前做过相应的控制台应用，有人嫌丑竟然！ ~~没错就是我本人！~~

&emsp;&emsp;为了方便使用，这次打算用 **winform** 美化一下！~~虽然还是很丑~~

## 项目结构

### 1 我用了啥

* **.net471**
* **tensorflowsharp1.13**
* **Newtonsoft.Json12.0.2**

### 2 我写了啥

* Model 模型相关
  * label.json 打标json
  * Model.pb 使用的网络模型文件，mobilenet2
* Util 工具
  * CNN 获取图像数据，导入模型，识别并给出结果
  * ImgeUtil 根据输入创建对应的输入tensor
* Main 程序的入口
* ImageCut 截图窗口

### 3 这能做啥

&emsp;&emsp;运行后点击Cut进行截图，模型对截图区域进行识别


## 其他链接

 [SimpleCNN](https://github.com/collapsenav/simplecnn)
 [SimpleCNNCore](https://github.com/collapsenav/simplecnncore)
