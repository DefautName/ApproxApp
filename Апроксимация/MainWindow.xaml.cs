using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ScottPlot;

namespace Апроксимация
{//  <package id="ScottPlot" version="4.1.64" targetFramework="net472" />
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AppFunc func;
        double[] funMass;
        private void GetSig(TextBlock Sig,TextBlock block,double fun)
        {
            if(fun<0)
                Sig.Text = "";
            else
                Sig.Text = "+";
            block.Text = fun.ToString().Replace(',', '.');
        }

        public MainWindow()
        {
            InitializeComponent();
            buffButt.IsEnabled = false;
        }

        private void ButtonCalc_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                Point p1 = new Point(double.Parse(X1.Text.Replace('.',',')), double.Parse(Y1.Text.Replace('.', ',')));
                Point p2 = new Point(double.Parse(X2.Text.Replace('.', ',')), double.Parse(Y2.Text.Replace('.', ',')));
                Point p3 = new Point(double.Parse(X3.Text.Replace('.', ',')), double.Parse(Y3.Text.Replace('.', ',')));
                func = new AppFunc(p1, p2, p3);

                funMass = func.GetFunc();
                A.Text = Math.Round(funMass[0],4).ToString().Replace(',', '.');

                GetSig(bSig, B, Math.Round(funMass[1],4));
                GetSig(cSig,C, Math.Round(funMass[2],4));

                Point exPoint = func.GetExPoint();

                exX.Text=Math.Round(exPoint.X,4).ToString().Replace(',','.');
                exY.Text= Math.Round(exPoint.Y,4).ToString().Replace(',', '.');
                if (func.WhereIsExPoint())
                    exPos.Text = "ВНУТРИ";
                else
                    exPos.Text = "ВНЕ";
                double []valueX = new double[10];
                double []valueY = new double[10];
                int nOfStep = 10;
                double step = (func.X3 - func.X3) / nOfStep;
                for (int i = 0; i < nOfStep; i++)
                    valueX[i]=func.X1 + step * i;
                for (int i = 0; i < nOfStep; i++)
                    valueY[i]=funMass[0] * valueX[i] * valueX[i] + funMass[1] * valueX[i] + funMass[2];

                //FuncGraf.Render();
                buffButt.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonBuff_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string buff = $"{funMass[0].ToString().Replace(',', '.')} {funMass[1].ToString().Replace(',', '.')} {funMass[2].ToString().Replace(',', '.')}";
                Clipboard.SetText(buff);
                MessageBox.Show("Значения коэффициентов a,b,c функции скопированы в бувер обмена");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
