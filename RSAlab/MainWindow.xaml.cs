using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.Numerics;

namespace RSAlab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int n, phi, e, d, p ,q;
        ArrayList zaszyfrowanaLista = new ArrayList();
        ArrayList deszyfrowanaLista = new ArrayList();

        private ArrayList TurnMessageIntoArrayOfASCIICode(String msg)
        {
            ArrayList lista = new ArrayList();
            Byte[] temp = Encoding.ASCII.GetBytes(msg);

            foreach(Byte x  in temp)
            {
                lista.Add((int)x);
            }

            return lista;

        }

        private String deszyfruj(ArrayList lista)
        {
            string output = "";
            
            for(int i = 0; i < lista.Count; i++)
            {
                char character = (char)((int)lista[i]);
                output += character;
            }

            return output;
        }

        private void deszyfrujButton_Click(object sender, RoutedEventArgs en)
        {
            deszyfrowanaLista = new ArrayList();
            outputMessageTextBox.Clear();
            BigInteger power = 0;
            foreach (int liczba in zaszyfrowanaLista)
            {
                power = BigInteger.Pow(liczba, d);
                int wynik = (int)(power % n);

                deszyfrowanaLista.Add(wynik);
            }


            outputMessageTextBox.Text = deszyfruj(deszyfrowanaLista);
           
        }

        private void szyfrujButton_Click(object sender, RoutedEventArgs en)
        {
            outputMessageTextBox.Clear();
            ArrayList listOfInt = TurnMessageIntoArrayOfASCIICode(messageTextBox.Text);
            zaszyfrowanaLista = new ArrayList();

            BigInteger power = 0;

            foreach(int liczba in listOfInt)
            {
                power = BigInteger.Pow(liczba, e);
                int wynik = (int)(power % n);

                zaszyfrowanaLista.Add(wynik);
            }

            foreach(int liczba in zaszyfrowanaLista)
            {
                outputMessageTextBox.Text += liczba + " ";
            }

        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerujButton_Click(object sender, RoutedEventArgs evt)
        {
            p = Int32.Parse(pTextBox.Text);
            q = Int32.Parse(qTextBox.Text);
            n = p * q;
            phi = (p - 1) * (q - 1);
            

            int cnt = 2;
            while(true)
            {
                if(IsPrime(cnt) &&  CalculateGreatestCommonDivisor(cnt, phi) == 1)
                {
                    e = cnt;
                    break;
                }

                cnt++;
            }

            d = 1;

            while(true)
            {
                if((e*d-1)%phi == 0)
                {
                    break;
                }

                d++;
            }

            nTextBlock.Text = "n = " + n;
            phiTextBlock.Text = "phi = " + phi;
            eTextBlock.Text = "e = " + e;
            dTextBlock.Text = "d = " + d;

        }

        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }

        private int CalculateGreatestCommonDivisor(int firstNumber, int secondNumber)
        {
            while (firstNumber != 0 && secondNumber != 0)
            {
                if (firstNumber > secondNumber)
                    firstNumber %= secondNumber;
                else
                    secondNumber %= firstNumber;
            }

            return firstNumber == 0 ? secondNumber : firstNumber;
        }
    }
}
