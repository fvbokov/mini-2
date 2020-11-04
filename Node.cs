using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3 {

    abstract class Node {
        protected static int size;
        protected int posX;
        protected int posY;

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
                return radius;
            }
            set {
                size = value;
            }
        }

        abstract public void DrawFigure(Graphics g);
        abstract public bool Contains(int Mx, int My);
        public Node(int x = 0, int y = 0) {
            posX = x;
            posY = y;
        }
    }

    class TriangleNode : Node {
        override public void DrawFigure(Graphics g) {
            Point top = new Point(posX, posY - size / 2);
            Point left = new Point(posX - size / 2, posY + size);
            Point right = new Point(posX + size / 2, posY + size);

            Point[] trianglePoints = { top, left, right };

            g.FillPolygon(new SolidBrush(Color.Green), trianglePoints);
        }

        public bool IsInPolygon(Point[] poly, Point p) {//https://stackoverflow.com/questions/4243042/c-sharp-point-in-polygon
            Point p1, p2;
            bool inside = false;

            if (poly.Length < 3) {
                return inside;
            }

            Point oldPoint = new Point(poly[poly.Length - 1].X, poly[poly.Length - 1].Y);

            for (int i = 0; i < poly.Length; i++) {
                Point newPoint = new Point(poly[i].X, poly[i].Y);

                if (newPoint.X > oldPoint.X) {
                    p1 = oldPoint;
                    p2 = newPoint;
                }
                else {
                    p1 = newPoint;
                    p2 = oldPoint;
                }

                if ((newPoint.X < p.X) == (p.X <= oldPoint.X) && (p.Y - (long)p1.Y) * (p2.X - p1.X) < (p2.Y - (long)p1.Y) * (p.X - p1.X)) {
                    inside = !inside;
                }

                oldPoint = newPoint;
            }

            return inside;
        }

        public override bool Contains(int Mx, int My) {
            Point top = new Point(posX, posY - size / 2);
            Point left = new Point(posX - size / 2, posY + size);
            Point right = new Point(posX + size / 2, posY + size);

            Point[] trianglePoints = { top, left, right,  };
            if (IsInPolygon(trianglePoints, new Point(Mx, My))) return true;
            else return false;
        }

        public TriangleNode(int x = 0, int y = 0) : base(x, y) { }
    }

    class CircleNode : Node {
        override public void DrawFigure(Graphics g) {
            g.FillEllipse(new SolidBrush(Color.Green), new Rectangle(posX - size / 2, posY - size / 2, size, size));
        }
        public override bool Contains(int Mx, int My) {
            double length = Math.Sqrt((Mx - (X)) * (Mx - (X)) + (My - (Y)) * (My - (Y)));
            if (length < size) return true;
            else return false;
        }
        public CircleNode(int x = 0, int y = 0) : base(x, y) { }
    }

    class SquareNode : Node {
        override public void DrawFigure(Graphics g) {
            g.FillRectangle(new SolidBrush(Color.Green), new Rectangle(posX - size / 2, posY - size / 2, size, size));
        }
 
        
        public override bool Contains(int Mx, int My) {
            Rectangle n = new Rectangle(posX - size / 2, posY - size / 2, size, size);
            if (n.Contains(Mx, My)) return true;
            else return false;
        }
        public SquareNode(int x = 0, int y = 0) : base(x, y) { }
    }
}