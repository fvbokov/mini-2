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
		int shiftX, shiftY;
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

		private void Form1_Paint(object sender, PaintEventArgs e) {
			Graphics g = e.Graphics;
			if (draw) {
				figure.DrawFigure(e.Graphics);
			}
		}

		private void Form1_Load(object sender, EventArgs e) {
			DoubleBuffered = true;
			Refresh();
		}

		private void квадратToolStripMenuItem_Click(object sender, EventArgs e) {
			which = "square";
			figure = new SquareNode(figure.X, figure.Y);
			Refresh();
		}

		private void кругToolStripMenuItem_Click(object sender, EventArgs e) {
			which = "circle";
			figure = new CircleNode(figure.X, figure.Y);
			Refresh();
		}

		private void треугольникToolStripMenuItem_Click(object sender, EventArgs e) {
			which = "tri";
			figure = new TriangleNode(figure.X, figure.Y);
			Refresh();
		}

		private void Form1_MouseUp(object sender, MouseEventArgs e) {
			if (drag) {
				drag = false;
				Refresh();
			}
		}

		private void Form1_MouseMove(object sender, MouseEventArgs e) {
			if (drag) {
				figure.X = e.X - shiftX;
				figure.Y = e.Y - shiftY;
				Refresh();
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
					switch (which) {
						case "circle":
							figure = new CircleNode(e.X, e.Y);
							break;
						case "square":
							figure = new SquareNode(e.X, e.Y);
							break;
						case "tri":
							figure = new TriangleNode(e.X, e.Y);
							break;
						default:
							figure = new CircleNode(e.X, e.Y);
							break;
					}
					draw = true;
				}
			}
			Refresh();
		}
	}
}