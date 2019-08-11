using System;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SimpleCNN_With_Winform {
    public partial class Main : Form {
        public Main() {
            InitializeComponent();
        }
        ImageCut imageCut;

        bool flag = false;
        private void Main_Load(object sender, EventArgs e) {
            Thread th = new Thread(LoadModel);
            th.IsBackground = true;
            th.Start();
        }

        private void LoadModel() {
            CNN.LoadModel();
        }

        private void CutBtn_Click(object sender, EventArgs e) {
            if (HideWindow.Checked) {
                Visible = false;
            }
            //等待窗口隐藏，根据电脑性能的不同，这个数值可能需要修改
            Thread.Sleep(200);
            //截取当前屏幕(窗口隐藏之后)
            Bitmap bitmap = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);

            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height));
            //创建新的截图窗口，将刚才的屏幕作为背景
            imageCut = new ImageCut();

            imageCut.Owner = this;
            imageCut.BackgroundImage = bitmap;
            flag = true;

            imageCut.ShowDialog();
        }

        private void Main_Activated(object sender, EventArgs e) {
            //第一次激活窗口时不会运行
            if (flag) {
                StringBuilder sb = new StringBuilder();
                var bytes = imageCut.GetImageBytes();
                if (bytes == null) {
                    return;
                }

                var list = CNN.GetResult(bytes);
                if (list != null) {
                    foreach (var item in list) {
                        sb.Append(item);
                        sb.Append("\n");
                    }
                    MessageBox.Show(sb.ToString(),"结果");
                }
                imageCut.Dispose();
                flag = false;
            }
            Show();
        }
    }
}
