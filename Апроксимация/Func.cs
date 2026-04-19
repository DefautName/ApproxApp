using System.Windows;

namespace Апроксимация
{
    internal class AppFunc
    {
        private double  x1,x2,x3,y1,y2,y3;
        private double[] coefsFunc;

        public double X1 { get { return x1; } }
        public double X2 { get { return x2; } }
        public double X3 { get { return x3; } }
        public double Y1 { get { return y1; } }
        public double Y2 { get { return y2; } }
        public double Y3 { get { return y3; } }

        public AppFunc(Point p1,Point p2,Point p3)
        {
            x1= p1.X;
            y1 = p1.Y;

            x2 = p2.X;
            y2 = p2.Y;

            x3 = p3.X;
            y3= p3.Y;

            SetFunc();
        }
        private void SetFunc()
        {
            coefsFunc = new double[3];

            double a1, a2, a3, d1, d2, d3;

            d1 = x1;
            d2 = x2;
            d3 = x3;

            a1 = y1 / (d1 - d2) / (d1 - d3);
            a2 = y2 / (d2 - d1) / (d2 - d3);
            a3 = y3 / (d3 - d1) / (d3 - d2);

            coefsFunc[0] = a1 + a2 + a3;
            coefsFunc[1] = (a1 * (d2 + d3) + a2 * (d1 + d3) + a3 * (d1 + d2)) * (-1);
            coefsFunc[2] = a1 * d2 * d3 + a2 * d1 * d3 + a3 * d1 * d2;
        }

        public double[] GetFunc()
        {
            return coefsFunc;
        }

    
        public Point GetExPoint()
        {
            Point rez = new Point();
            rez.X = -coefsFunc[1] / (2 * coefsFunc[0]);
            rez.Y = coefsFunc[0] * rez.X * rez.X + coefsFunc[1] * rez.X + coefsFunc[2];
            return rez;
        }
        public bool WhereIsExPoint()
        {
            Point ex = GetExPoint();
            if ((ex.X < x3 && ex.X > x1) ||(ex.X < x1 && ex.X > x3))
                return true;
            else
                return false;
        }

        public double[] GetY(int n)
        {
            double[] massY = new double[n];
            massY[0] = y1;
            massY[n - 1] = y3;
            for(int i=1;i<n-1;i++)
            {
                massY[i] = massY[i-1]+(y3 - y1) / n;
            }
            return massY;
        }
        public double[] GetX(int n)
        {
            double[] massY = GetY(n);
            double[] massX=new double[n];

            for (int i = 0; i < n; i++)
            {
                massX[i] = coefsFunc[0]*massY[i]* massY[i]+coefsFunc[1]* massY[i]+ coefsFunc[2];
            }
            
            return massX;
        }
    }
}
