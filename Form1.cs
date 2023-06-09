using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RectangleKursova
{
    public partial class Form1 : Form
    {
        private Rectangle rectangle1;
        private Rectangle rectangle2;
        private Rectangle intersectionRectangle;
        private Rectangle boundingRectangle;

        private Button moveButton;
        private Button resizeButton;
        private Button intersectionButton;
        private Button boundingButton;

        public Form1()
        {
            InitializeComponent();

            rectangle1 = new Rectangle(new Point(50, 50), 100, 100);
            rectangle2 = new Rectangle(new Point(150, 100), 150, 100);

            moveButton = new Button();
            moveButton.Text = "Move";
            moveButton.Location = new Point(30, 30);
            moveButton.Click += MoveButton_Click;
            moveButton.FlatStyle = FlatStyle.Flat;
            moveButton.BackColor = Color.LightGray;
            moveButton.ForeColor = Color.Black;

            resizeButton = new Button();
            resizeButton.Text = "Resize";
            resizeButton.Location = new Point(110, 30);
            resizeButton.Click += ResizeButton_Click;
            resizeButton.FlatStyle = FlatStyle.Flat;
            resizeButton.BackColor = Color.LightGray;
            resizeButton.ForeColor = Color.Black;

            intersectionButton = new Button();
            intersectionButton.Text = "Intersection";
            intersectionButton.Location = new Point(180, 30);
            intersectionButton.Click += IntersectionButton_Click;
            intersectionButton.FlatStyle = FlatStyle.Flat;
            intersectionButton.BackColor = Color.LightGray;
            intersectionButton.ForeColor = Color.Black;

            boundingButton = new Button();
            boundingButton.Text = "Bounding";
            boundingButton.Location = new Point(250, 30);
            boundingButton.Click += BoundingButton_Click;
            boundingButton.FlatStyle = FlatStyle.Flat;
            boundingButton.BackColor = Color.LightGray;
            boundingButton.ForeColor = Color.Black;


            Controls.Add(moveButton);
            Controls.Add(resizeButton);
            Controls.Add(intersectionButton);
            Controls.Add(boundingButton);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graphics = e.Graphics;
            Pen pen = new Pen(Color.Black);

            graphics.DrawRectangle(pen, rectangle1.GetSystemRectangle());
            graphics.DrawRectangle(pen, rectangle2.GetSystemRectangle());

            if (intersectionRectangle != null)
                graphics.DrawRectangle(pen, intersectionRectangle.GetSystemRectangle());

            if (boundingRectangle != null)
                graphics.DrawRectangle(pen, boundingRectangle.GetSystemRectangle());
        }

        private void MoveButton_Click(object sender, EventArgs e)
        {
            rectangle1.Move(10, 10);
            rectangle2.Move(-10, -10);

            Refresh();
        }

        private void ResizeButton_Click(object sender, EventArgs e)
        {
            rectangle1.Resize(150, 150);
            rectangle2.Resize(200, 150);

            Refresh();
        }

        private void IntersectionButton_Click(object sender, EventArgs e)
        {
            intersectionRectangle = Rectangle.GetIntersection(rectangle1, rectangle2);
            boundingRectangle = null;

            Refresh();
        }

        private void BoundingButton_Click(object sender, EventArgs e)
        {
            boundingRectangle = Rectangle.GetMinimumBoundingRectangle(rectangle1, rectangle2);
            intersectionRectangle = null;

            Refresh();
        }

        public class Rectangle
        {
            public Point Location { get; set; } // Положення верхнього лівого кута прямокутника
            public int Width { get; set; } // Ширина прямокутника
            public int Height { get; set; } // Висота прямокутника

            public Rectangle(Point location, int width, int height)
            {
                Location = location;
                Width = width;
                Height = height;
            }

            public void Move(int dx, int dy)
            {
                Location = new Point(Location.X + dx, Location.Y + dy);
            }

            public void Resize(int width, int height)
            {
                Width = width;
                Height = height;
            }

            public System.Drawing.Rectangle GetSystemRectangle()
            {
                return new System.Drawing.Rectangle(Location.X, Location.Y, Width, Height);
            }

            public static Rectangle GetMinimumBoundingRectangle(Rectangle rect1, Rectangle rect2)
            {
                int minX = Math.Min(rect1.Location.X, rect2.Location.X);
                int minY = Math.Min(rect1.Location.Y, rect2.Location.Y);
                int maxX = Math.Max(rect1.Location.X + rect1.Width, rect2.Location.X + rect2.Width);
                int maxY = Math.Max(rect1.Location.Y + rect1.Height, rect2.Location.Y + rect2.Height);

                int newWidth = maxX - minX;
                int newHeight = maxY - minY;

                return new Rectangle(new Point(minX, minY), newWidth, newHeight);
            }

            public static Rectangle GetIntersection(Rectangle rect1, Rectangle rect2)
            {
                int minX = Math.Max(rect1.Location.X, rect2.Location.X);
                int minY = Math.Max(rect1.Location.Y, rect2.Location.Y);
                int maxX = Math.Min(rect1.Location.X + rect1.Width, rect2.Location.X + rect2.Width);
                int maxY = Math.Min(rect1.Location.Y + rect1.Height, rect2.Location.Y + rect2.Height);

                int newWidth = Math.Max(0, maxX - minX);
                int newHeight = Math.Max(0, maxY - minY);

                return new Rectangle(new Point(minX, minY), newWidth, newHeight);
            }
        }

       
    }
}
