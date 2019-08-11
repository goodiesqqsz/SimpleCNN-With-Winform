using System.IO;
using TensorFlow;

namespace SimpleCNN_With_Winform {
    //从别人那边顺过来的，稍微改了改
    public partial class ImageUtil {
        public static TFTensor CreateTensorFromImageFile(byte[ ] contents, int h, int w, int channel, TFDataType destinationDataType = TFDataType.Float) {
            var tensor = TFTensor.CreateString(contents);

            TFOutput input, output;

            using (var graph = ConstructGraphToNormalizeImage(out input, out output, h, w, channel, destinationDataType)) {
                using (var session = new TFSession(graph)) {
                    var normalized = session.Run(
                        inputs: new[ ] { input },
                        inputValues: new[ ] { tensor },
                        outputs: new[ ] { output });

                    return normalized[0];
                }
            }
        }
        public static TFTensor CreateTensorFromImageFile(string file, int h, int w, int channel, TFDataType destinationDataType = TFDataType.Float) {
            var contents = File.ReadAllBytes(file);
            var tensor = TFTensor.CreateString(contents);

            TFOutput input, output;

            using (var graph = ConstructGraphToNormalizeImage(out input, out output, h, w, channel, destinationDataType)) {
                using (var session = new TFSession(graph)) {
                    var normalized = session.Run(
                        inputs: new[ ] { input },
                        inputValues: new[ ] { tensor },
                        outputs: new[ ] { output });

                    return normalized[0];
                }
            }
        }
        private static TFGraph ConstructGraphToNormalizeImage(out TFOutput input, out TFOutput output, int h, int w, int channel, TFDataType destinationDataType = TFDataType.Float) {
            int W = w;
            int H = h;
            const float Mean = 0;
            const float Scale = 255f;

            var graph = new TFGraph();
            input = graph.Placeholder(TFDataType.String);

            output = graph.Cast(
                graph.Div(x: graph.Sub(x: graph.ResizeBilinear(images: graph.ExpandDims(input: graph.Cast(graph.DecodeJpeg(contents: input, channels: channel), DstT: TFDataType.Float),
                            dim: graph.Const(0, "make_batch")),
                        size: graph.Const(new int[ ] { W, H }, "size")),
                    y: graph.Const(Mean, "mean")),
                y: graph.Const(Scale, "scale")), destinationDataType);


            return graph;
        }
    }
}
