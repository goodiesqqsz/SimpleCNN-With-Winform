using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TensorFlow;

namespace SimpleCNN_With_Winform {
    public class CNN {
        static Dictionary<int, string> Dir = null;

        static TFGraph Graph = null;

        public static void LoadModel() {
            if (Graph == null) {
                Graph = new TFGraph();
                string modelpath = "Model/Model.pb";
                Graph.Import(File.ReadAllBytes(modelpath));
            }
        }
        static void LoadDir() {
            string jsonFilePath = "Model/label.json";
            using (StreamReader file = File.OpenText(jsonFilePath)) {
                using (JsonTextReader reader = new JsonTextReader(file)) {
                    JObject obj = (JObject)JToken.ReadFrom(reader);
                    foreach (var i in obj) {
                        Dir.Add(int.Parse(i.Key), i.Value.ToString());
                    }
                }
            }
        }
        static string GetMax(float[ ] numbers) {
            int maxIndex = 0;
            for (int i = maxIndex; i < numbers.Length; i++) {
                if (numbers[i] > numbers[maxIndex]) {
                    maxIndex = i;
                }
            }
            return Dir[maxIndex];
        }
        /* 暂时还不能用
        public static List<string> GetResult(string[] imagePaths)
        {
            if (Dir == null)
            {
                Dir = new Dictionary<int, string>();
                LoadDir();
            }
            List<string> paths = new List<string>();
            //获取图片路径
            //反正路径什么的根据自己的情况改一下就好了
            if (imagePaths.Length != 0)
            {
                paths.AddRange(imagePaths);
            }
            //需要的tensor的尺寸参数，暂时只支持3通道的
            int h = 224, w = 224, channel = 3;
            List<string> results = new List<string>();
            using (var session = new TFSession(Graph))
            {
                foreach (var i in paths)
                {
                    var runner = session.GetRunner();
                    var tensor = ImageUtil.CreateTensorFromImageFile(i, h, w, channel);
                    //前者为输入，后者为输出，改成自己的层就行了
                    runner.AddInput(Graph["input_1"][0], tensor).Fetch(Graph["output_1"][0]);
                    var r = runner.Run();
                    var v = (float[])r[0].GetValue();
                    results.Add(GetMax(v));
                }
            }
            return results;
        }
        */
        public static List<string> GetResult(byte[ ] bytes) {
            if (Dir == null) {
                Dir = new Dictionary<int, string>();
                LoadDir();
            }
            if (bytes == null || bytes.Length == 0) {
                return null;
            }
            //需要的tensor的尺寸参数，暂时只支持3通道的
            int h = 224, w = 224, channel = 3;
            List<string> results = new List<string>();
            using (var session = new TFSession(Graph)) {
                var runner = session.GetRunner();
                var tensor = ImageUtil.CreateTensorFromImageFile(bytes, h, w, channel);
                //前者为输入，后者为输出，改成自己的层就行了
                runner.AddInput(Graph["input_1"][0], tensor).Fetch(Graph["output_1"][0]);
                var r = runner.Run();
                var v = (float[ ])r[0].GetValue();
                results.Add(GetMax(v));
            }
            return results;
        }


    }
}