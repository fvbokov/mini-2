using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3 {
	public partial class Form1 : Form {

		bool draw, drag;
		int x, y, shiftX, shiftY;
		static int radius;
		static Color color;
		string which;

		Node figure;

		public Form1() {
			InitializeComponent();
			draw = false;
			figure = new CircleNode();
			which = "circle";
			drag = false;
			shiftX = 0;
			shiftY = 0;
		}

		static Form1() {
			radius = 50;
			color = Color.Green;
		}

		private void Form1_Paint(object sender, PaintEventArgs e) {
			Graphics g = e.Graphics;
			//g.Clear(Color.Beige);
			if (draw) {
				figure.DrawFigure(e.Graphics);
			}
		}

		private void Form1_Load(object sender, EventArgs e) {
			figure.radius = radius;
			Invalidate();
		}

		private void квадратToolStripMenuItem_Click(object sender, EventArgs e) {
			which = "square";
			x = figure.X;
			y = figure.Y;
			figure = new SquareNode(x, y);
			Invalidate();
		}

		private void кругToolStripMenuItem_Click(object sender, EventArgs e) {
			which = "circle";
			x = figure.X;
			y = figure.Y;
			figure = new CircleNode(x, y);
			Invalidate();
		}

		private void треугольникToolStripMenuItem_Click(object sender, EventArgs e) {
			which = "tri";
			x = figure.X;
			y = figure.Y;
			figure = new TriangleNode(x, y);
			Invalidate();
		}

		private void Form1_MouseUp(object sender, MouseEventArgs e) {
			if (drag) {
				drag = false;
				Invalidate();
			}
		}

		private void Form1_MouseMove(object sender, MouseEventArgs e) {
			if (drag) {
				figure.X = e.X - shiftX;
				figure.Y = e.Y - shiftY;
				Invalidate();
			}
		}

		private void Form1_MouseDown(object sender, MouseEventArgs e) {
			if (figure.Contains(e.X, e.Y) && draw) {
				if (MouseButtons.Right == e.Button) {
					draw = false;
					drag = false;
				}

				if (MouseButtons.Left == e.Button) {
					shiftX = e.X - figure.X;
					shiftY = e.Y - figure.Y;

					drag = true;
				}
			}

			else {
				if (MouseButtons.Left == e.Button) {
					if (which == "circle") figure = new CircleNode(e.X, e.Y);
					if (which == "square") figure = new SquareNode(e.X, e.Y);
					if (which == "tri") figure = new TriangleNode(e.X, e.Y);
					figure.X = e.X;
					figure.Y = e.Y;
					draw = true;
				}
			}
			Invalidate();
		}
	}
}