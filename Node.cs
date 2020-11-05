using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3 {
    abstract class Node {
        protected static int size;
        protected static Color color;
        protected int posX;
        protected int posY;

        public Node(int x = 0, int y = 0) {
            posX = x;
            posY = y;
        }
        static Node() {
            color = Color.Green;
            size = 50;
        }

        public int X {
            get {
                return posX;
            }
            set {
                posX = value;
            }
        }
        public int Y {
            get {
                return posY;
            }
            set {
                posY = value;
            }
        }
        public int radius {
            get {
                return size;
            }
            set {
                size = value;
            }
        }

        abstract public void DrawFigure(Graphics g);
        abstract public bool Contains(int Mx, int My);
    }

    class TriangleNode : Node {
        public TriangleNode(int x = 0, int y = 0) : base(x, y) { }

        override public void DrawFigure(Graphics g) {
            Point top = new Point(posX, posY - size / 2);
            Point left = new Point(posX - size / 2, posY + size);
            Point right = new Point(posX + size / 2, posY + size);

            Point[] trianglePoints = { top, left, right };

            g.FillPolygon(new SolidBrush(Color.Green), trianglePoints);
        }

        public override bool Contains(int Mx, int My) {
            Point point1 = new Point(posX, posY - size / 2);
            Point point2 = new Point(posX - size / 2, posY + size);
            Point point3 = new Point(posX + size / 2, posY + size);

            int product1 = (point1.X - Mx) * (point2.Y - point1.Y) - (point2.X - point1.X) * (point1.Y - My);
            int product2 = (point2.X - Mx) * (point3.Y - point2.Y) - (point3.X - point2.X) * (point2.Y - My);
            int product3 = (point3.X - Mx) * (point1.Y - point3.Y) - (point1.X - point3.X) * (point3.Y - My);
            if ((product1 >= 0 && product2 >= 0 && product3 >= 0) || (product1 < 0 && product2 < 0 && product3 < 0)) {
                return true;
			}
            return false;
        }
    }

    class CircleNode : Node {
        public CircleNode(int x = 0, int y = 0) : base(x, y) { }

        override public void DrawFigure(Graphics g) {
            g.FillEllipse(new SolidBrush(Color.Green), new Rectangle(posX - size / 2, posY - size / 2, size, size));
        }
        public override bool Contains(int Mx, int My) {
            double length = (Math.Sqrt((Mx - (X)) * (Mx - (X)) + (My - (Y)) * (My - (Y))))*2;
            if (length < size) return true;
            else return false;
        }
    }

    class SquareNode : Node {
        public SquareNode(int x = 0, int y = 0) : base(x, y) { }

        override public void DrawFigure(Graphics g) {
            g.FillRectangle(new SolidBrush(Color.Green), new Rectangle(posX - size / 2, posY - size / 2, size, size));
        }
 
        public override bool Contains(int Mx, int My) {
            Rectangle n = new Rectangle(posX - size / 2, posY - size / 2, size, size);
            if (n.Contains(Mx, My)) return true;
            else return false;
        }
    }
}