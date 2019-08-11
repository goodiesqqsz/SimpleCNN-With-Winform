using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace SimpleCNN_With_Winform {
    public partial class ImageCut : Form {
        //截图状态
        bool cutStart = false;
        //图像数据的byte数组
        public byte[ ] bytes;
        //矩形选取框范围
        Rectangle rectangle;
        //记录选取的起始点坐标
        Point downPoint;
        public ImageCut() {
            InitializeComponent();
        }
        /// <summary>
        /// 获取截图之后产生的byte数组
        /// </summary>
        public byte[ ] GetImageBytes() {
            if (bytes.Length > 0)
                return bytes;
            return null;
        }
        private void ImageCut_Load(object sender, EventArgs e) {
            //暂时没想到在Load的时候可以做什么
        }
        /// <summary>
        /// 按钮按下时触发
        /// Esc、Enter可以退出截图
        /// </summary>
        private void ImageCut_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter) {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        /// <summary>
        /// 鼠标右键可以退出截图
        /// </summary>
        private void ImageCut_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                this.DialogResult = DialogResult.OK;
                this.Close();
                Form owner = (Main)this.Owner;
                owner.Show();
            }
        }
        /// <summary>
        /// 鼠标左键按下时开始截图
        /// </summary>
        private void ImageCut_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                //当前截图状态为false时，记录第一次按下时的坐标
                if (!cutStart) {
                    cutStart = true;
                    downPoint = new Point(e.X, e.Y);
                }
            }
        }
        /// <summary>
        /// 鼠标移动时画出选取的矩形框
        /// </summary>
        private void ImageCut_MouseMove(object sender, MouseEventArgs e) {
            if (cutStart) {
                //新建一个背景的副本
                using (Bitmap bitmap = new Bitmap((Image)BackgroundImage.Clone())) {
                    //用于定位矩形的第二个点
                    Point startPoint = new Point(downPoint.X, downPoint.Y);

                    using (Graphics graphics = Graphics.FromImage(bitmap)) {
                        //框选一个矩形区域
                        using (Pen pen = new Pen(Color.BurlyWood, 0.5f)) {
                            int width = Math.Abs(e.X - downPoint.X);
                            int height = Math.Abs(e.Y - downPoint.Y);

                            startPoint.X = e.X < downPoint.X ? e.X : downPoint.X;
                            startPoint.Y = e.Y < downPoint.Y ? e.Y : downPoint.Y;

                            rectangle = new Rectangle(startPoint, new Size(width, height));
                            //将矩形区域在副本上画出来画出来
                            graphics.DrawRectangle(pen, rectangle);
                        }
                    }
                    //将包含矩形区域的副本画出来
                    using (Graphics graphics = this.CreateGraphics())
                        graphics.DrawImage(bitmap, new Point(0, 0));
                }
            }
        }
        /// <summary>
        /// 松开鼠标
        /// </summary>
        private void ImageCut_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                if (cutStart) {
                    cutStart = false;
                    //若框选的矩形区域高或宽为0，则选取整个屏幕区域
                    if (rectangle.Width == 0 || rectangle.Height == 0) {
                        rectangle.Width = Screen.AllScreens[0].Bounds.Width;
                        rectangle.Height = Screen.AllScreens[0].Bounds.Height;
                        rectangle.Location = new Point(0, 0);
                    }
                    //根据框选的矩形区域创建图像
                    using (Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height)) {
                        //将矩形区域填充上图像
                        using (Graphics graphics = Graphics.FromImage(bitmap)) {
                            graphics.DrawImage(//画图
                                (Image)BackgroundImage.Clone(), //图像源
                                new Rectangle(0, 0, rectangle.Width, rectangle.Height),//在bitmap上的填充位置为(0,0)，范围与创建的大小相同
                                rectangle, //从图像源选取的部分由之前框选时的rectangle确定
                                GraphicsUnit.Pixel);//单位为像素
                            //将图像转为byte数组
                            MemoryStream stream = new MemoryStream();
                            bitmap.Save(stream, ImageFormat.Jpeg);
                            bytes = stream.GetBuffer();
                        }
                    }
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    //获取该截图窗口的拥有者，并且显示
                    Form owner = (Main)this.Owner;
                    owner.Visible = true;
                    //owner.Show();
                }
            }
        }
    }
}
