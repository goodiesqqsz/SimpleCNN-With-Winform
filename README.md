# SimpleCNN-With-Winform

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
