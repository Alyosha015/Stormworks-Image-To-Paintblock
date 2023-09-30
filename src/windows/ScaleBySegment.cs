using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageConverter {
    public partial class ScaleBySegment : Form {
        private MainWindow MainWindowData;
        private Bitmap Image;
        private Point LineStartPos;
        private Point LineEndPos;
        private Point MouseCursorPos;
        private bool FirstPositionRecorded;
        private bool SecondPositionRecorded;
        private bool[] KeysDown = new bool[1000];

        public ScaleBySegment(MainWindow mainWindow, Bitmap image) {
            InitializeComponent();
            MainWindowData = mainWindow;
            Bitmap bitmap = new Bitmap(image.Width, image.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(new SolidBrush(Color.White), new RectangleF(0, 0, image.Width, image.Height));
            graphics.DrawImage(image, 0, 0, image.Width, image.Height);
            Image = bitmap;
        }

        private void ScaleBySegment_Load(object sender, EventArgs e) {
            Icon = Icon.FromHandle(Properties.Resources.IconInverted.GetHicon());
            ImageBox.Width = ClientSize.Width;
            ImageBox.Height = ClientSize.Height;
            ImageBox.Image = Image;

            double aspectRatio = (double)Image.Width / Image.Height;
            double imageWidth = Math.Min(ImageBox.ClientSize.Width, ImageBox.ClientSize.Height * aspectRatio);
            double imageHeight = Math.Min(ImageBox.ClientSize.Height, ImageBox.ClientSize.Width / aspectRatio);

            Bitmap resizedImage = new Bitmap((int)imageWidth, (int)imageHeight);
            using (Graphics graphics = Graphics.FromImage(resizedImage)) {
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.DrawImage(Image, 0, 0, (int)imageWidth, (int)imageHeight);
            }
            ImageBox.Image = resizedImage;

            Timer timer = new Timer();
            timer.Interval = 16;
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e) {
            Refresh();
        }

        private void MouseCursorMove(object sender, MouseEventArgs e) {
            MouseCursorPos = e.Location;
        }

        private void MouseCursorDown(object sender, MouseEventArgs e) {
            if (!FirstPositionRecorded && !SecondPositionRecorded) {
                LineStartPos = MouseCursorPos;
                SecondPositionRecorded = false;
                FirstPositionRecorded = true;
            }
        }

        private void MouseCursorUp(object sender, MouseEventArgs e) {
            if(!SecondPositionRecorded) {
                LineEndPos = MouseCursorPos;
                if (LineStartPos == LineEndPos) {
                    FirstPositionRecorded = false;
                    return;
                }
                SecondPositionRecorded = true;
                ResetLine.Enabled = true;
                LengthUDC.Enabled = true;
                Apply.Enabled = true;
            }
        }

        private void OnPaint(object sender, PaintEventArgs e) {
            if (KeysDown[(int)Keys.ShiftKey] || KeysDown[(int)Keys.LShiftKey] || KeysDown[(int)Keys.RShiftKey] && FirstPositionRecorded) {
                double angle = -(Math.Atan2(LineStartPos.Y - MouseCursorPos.Y, LineStartPos.X - MouseCursorPos.X) - Math.PI);
                if (Util.EqualWithin(angle, 0, 0.08) || Util.EqualWithin(angle, Math.PI * 2, 0.08) || Util.EqualWithin(angle, Math.PI, 0.08)) {
                    MouseCursorPos.Y = LineStartPos.Y;
                } else if(Util.EqualWithin(angle, Math.PI * 0.5, 0.08) || Util.EqualWithin(angle, Math.PI * 1.5, 0.08)) {
                    MouseCursorPos.X = LineStartPos.X;
                }
            }
            if (SecondPositionRecorded) {
                e.Graphics.DrawLine(new Pen(Color.Black, 8f), LineStartPos, LineEndPos);
                e.Graphics.DrawLine(new Pen(Color.FromArgb(192, Color.White), 4f), LineStartPos, LineEndPos);
            } else if (FirstPositionRecorded) {
                e.Graphics.DrawLine(new Pen(Color.Black, 8f), LineStartPos, MouseCursorPos);
                e.Graphics.DrawLine(new Pen(Color.FromArgb(192, Color.White), 4f), LineStartPos, MouseCursorPos);
            }
        }

        private void ScaleBySegment_KeyDown(object sender, KeyEventArgs e) {
            KeysDown[(int)e.KeyCode] = true;
        }

        private void ScaleBySegment_KeyUp(object sender, KeyEventArgs e) {
            KeysDown[(int)e.KeyCode] = false;
        }

        private void Apply_Click(object sender, EventArgs e) {
            double aspectRatio = (double)Image.Width / Image.Height;
            double imageWidth = Math.Min(ImageBox.ClientSize.Width, ImageBox.ClientSize.Height * aspectRatio);
            double imageHeight = Math.Min(ImageBox.ClientSize.Height, ImageBox.ClientSize.Width / aspectRatio);

            int x1 = LineStartPos.X, x2 = LineEndPos.X, y1 = LineStartPos.Y, y2 = LineEndPos.Y;
            double lineWidth = Math.Abs(x1 - x2);
            double lineHeight = Math.Abs(y1 - y2);
            double lineLength = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));

            double widthToLineWidthRatio = imageWidth / lineWidth;
            double blocksPerXPixel = lineWidth / lineLength;
            double widthBlocks = Math.Max(widthToLineWidthRatio * blocksPerXPixel * (double)LengthUDC.Value, 1);

            double heightToLineHeightRatio = imageHeight / lineHeight;
            double blocksPerYPixel = lineHeight / lineLength;
            double heightBlocks = Math.Max(heightToLineHeightRatio * blocksPerYPixel * (double)LengthUDC.Value, 1);

            if (lineWidth > lineHeight) {
                MainWindowData.newWidth = (int)Math.Max(widthBlocks, 1);
                if (MainWindowData.modeSelectIndex == 3) {
                    double newHeight = ((double)Image.Height / Image.Width) * widthBlocks * 9;
                    MainWindowData.newHeight = Math.Max((int)Math.Ceiling(Math.Floor(newHeight) / 9), 1);
                }
            } else if (lineHeight > lineWidth) {
                MainWindowData.newHeight = (int)Math.Max(heightBlocks, 1);
                if(MainWindowData.modeSelectIndex == 2) {
                    double newWidth = ((double)Image.Width / Image.Height) * heightBlocks * 9;
                    MainWindowData.newWidth = Math.Max((int)Math.Ceiling(Math.Floor(newWidth) / 9), 1);
                }
            }

            Close();
        }

        private void ResetLine_Click(object sender, EventArgs e) {
            FirstPositionRecorded = false;
            SecondPositionRecorded = false;
            ResetLine.Enabled = false;
            LengthUDC.Enabled = false;
            Apply.Enabled = false;
        }

        private void LengthKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) e.SuppressKeyPress = true;
        }
    }
}
