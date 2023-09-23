﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

namespace CCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly Dictionary<string, double> Chemical = new Dictionary<string, double>();
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Calc(object sender, TextChangedEventArgs e)
        {
            Button_Click(sender, e);
        }
        double S;
        public static bool InRange(byte min, byte max, byte value) => (value >= min) && (value <= max);
        public static bool InRange(short min, short max, short value) => (value >= min) && (value <= max);
        public static bool InRange(ushort min, ushort max, ushort value) => (value >= min) && (value <= max);
        public static bool InRange(int min, int max, int value) => (value >= min) && (value <= max);
        public static bool InRange(uint min, uint max, uint value) => (value >= min) && (value <= max);
        public static bool InRange(long min, long max, long value) => (value >= min) && (value <= max);
        public static bool InRange(ulong min, ulong max, ulong value) => (value >= min) && (value <= max);

        public static bool InAscii(char from, char to, char letter) => (letter >= from) && (letter <= to);
        public static bool InAscii(byte from, byte to, char letter) => (letter >= from) && (letter <= to);
        private void ChangeConfig(object sender, SelectionChangedEventArgs e)
        {
            ChangeConfig(sender, (RoutedEventArgs)e);
        }

        private void ChangeConfig(object sender, RoutedEventArgs e)
        {
            if (!IsInitialized) return;
            if (C0.IsChecked == true)
            {
                if (C1.SelectedIndex == 0)
                {
                    CR.Text = Math.Round(S, (C2.SelectedIndex + 1), MidpointRounding.ToEven).ToString();
                }
                else
                {
                    CR.Text = Math.Round(S, (C2.SelectedIndex + 1), MidpointRounding.AwayFromZero).ToString();
                }
            }
            else
            {
                CR.Text = S.ToString();
            }
        }
        Thread a = new(new ParameterizedThreadStart((a) => { }));
        private void Button_Click(object sender, RoutedEventArgs e)
        {

           // List<string[]> parts = new List<string[]>();
            string[] strings = { "", "" };
            double all = 0;
            double n;
            int c;

            foreach (char item in CF.Text)
            {
                if (InAscii('A', 'Z', item))
                {
                    all += (Chemical.TryGetValue(strings[0], out n) ? n : 0) * (int.TryParse(strings[1], out c) ? c : 1);
                    strings[0] = item.ToString();
                    strings[1] = "";
                }
                else if (InAscii('a', 'z', item))
                    strings[0] += item.ToString();
                else if (InAscii('0', '9', item))
                    strings[1] += item.ToString();
            }
            all += (Chemical.TryGetValue(strings[0], out n) ? n : 0) * (int.TryParse(strings[1], out c) ? c : 1);

            CR.Text = all.ToString();
            S = all;
            ChangeConfig(sender, e);

        }

        private void Init(object sender, EventArgs e)
        {
            CF.IsEnabled= C0.IsEnabled = calc.IsEnabled = false;
            // UnpackData
            string data = "\t0.0\r\nH\t1.00794\r\nHe\t4.002602\r\nLi\t6.941\r\nBe\t9.012182\r\nB\t10.811\r\nC\t12.0107\r\nN\t14.0067\r\nO\t15.9994\r\nF\t18.9984032\r\nNe\t20.1797\r\nNa\t22.98976928\r\nMg\t24.305\r\nAl\t26.9815386\r\nSi\t28.0855\r\nP\t30.973762\r\nS\t32.065\r\nCl\t35.453\r\nAr\t39.948\r\nK\t39.0983\r\nCa\t40.078\r\nSc\t44.955912\r\nTi\t47.867\r\nV\t50.9415\r\nCr\t51.9961\r\nMn\t54.938045\r\nFe\t55.845\r\nCo\t58.933195\r\nNi\t58.6934\r\nCu\t63.546\r\nZn\t65.38\r\nGa\t69.723\r\nGe\t72.63\r\nAs\t74.9216\r\nSe\t78.96\r\nBr\t79.904\r\nKr\t83.798\r\nRb\t85.4678\r\nSr\t87.62\r\nY\t88.90585\r\nZr\t91.224\r\nNb\t92.90638\r\nMo\t95.96\r\nRu\t101.07\r\nRh\t102.9055\r\nPd\t106.42\r\nAg\t107.8682\r\nCd\t112.411\r\nIn\t114.818\r\nSn\t118.71\r\nSb\t121.76\r\nTe\t127.6\r\nI\t126.90447\r\nXe\t131.293\r\nCs\t132.9054519\r\nBa\t137.327\r\nLa\t138.90547\r\nCe\t140.116\r\nPr\t140.90765\r\nNd\t144.242\r\nSm\t150.36\r\nEu\t151.964\r\nGd\t157.25\r\nTb\t158.92535\r\nDy\t162.5\r\nHo\t164.93032\r\nEr\t167.259\r\nTm\t168.93421\r\nYb\t173.054\r\nLu\t174.9668\r\nHf\t178.49\r\nTa\t180.94788\r\nW\t183.84\r\nRe\t186.207\r\nOs\t190.23\r\nIr\t192.217\r\nPt\t195.084\r\nAu\t196.966569\r\nHg\t200.59\r\nPb\t207.2\r\nBi\t208.9804\r\nTh\t232.03806\r\nPa\t231.03588\r\nU\t238.02891\r\nMd\t202";
            string[] ds = data.Split("\r\n");
            string[] list;
            float a;
            foreach (string i in ds)
            {
                list = i.Split("\t");
                if (!Chemical.TryAdd(list[0], float.TryParse(list[1], out a) ? a : 1))
                    MessageBox.Show("ERROR! " + list.ToString());

            }
            CF.IsEnabled = C0.IsEnabled = calc.IsEnabled = true;
        }

    }

}
