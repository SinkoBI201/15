using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp
{
    //вспомогательная структура содержит координаты
    public struct Point
    {
        public int X, Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Game
    {
        private int[,] _arr; //данные игрового поля
        private int _n; //размер стороны поля

        //индексатор
        public int this[int x, int y]
        {
            get
            {
                return _arr[x, y];
            }
        }

        //вывод игрового поля на консоль
        public void Print()
        {
            Console.WriteLine();

            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    Console.Write("{0}\t", _arr[i, j]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        //получение координат переданного значения
        public Point GetLocation(int value)
        {
            int i_value = -1;
            int j_value = -1;

            //ищем значение в массиве
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    if (_arr[i, j] == value)
                    {
                        i_value = i;
                        j_value = j;
                        break;
                    }
                }
            }

            if (i_value == -1)
                throw new Exception("Нет такого значения!");

            Point point = new Point(i_value, j_value);
            return point;
        }

        //делаем ход (меняем местами 0 и то значение которое ввели, если они явл-ся соседями)
        public void Shift(int value)
        {
            //получаем расположение того значения которое ввели
            Point point = GetLocation(value);

            int i_val = point.X;
            int j_val = point.Y;

            //проверяем явл-ся ли он соседом с нулем
            if ((i_val != 0 && _arr[i_val - 1, j_val] == 0) || //проверяем соседа сверху
                (i_val != _n - 1 && _arr[i_val + 1, j_val] == 0) || //проверяем соседа снизу
                 (j_val != 0 && _arr[i_val, j_val - 1] == 0) || //проверяем соседа слева
                 (j_val != _n - 1 && _arr[i_val, j_val + 1] == 0)) //проверяем соседа справа
            {
                //получаем расположение нуля
                Point point0 = GetLocation(0);

                //меняем их местами
                _arr[point0.X, point0.Y] = value;
                _arr[point.X, point.Y] = 0;
            }
            else
            {
                //если он не сосед то выкидываем сообщение (возникает исключение)
                throw new Exception("Такой ход невозможен!");
            }
        }

        private void ReadFromFile()
        {
            StreamReader sr = new StreamReader("data.csv");

            int n = int.Parse(sr.ReadLine()); //считываем размер поля (стороны)
            var arr = sr.ReadLine().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); //считываем данные игрового поля и преобразуем в массив данных

            sr.Close();

            _arr = new int[n, n];
            _n = n;

            int index = 0;
            //данные преобразуем в целочисленный массив
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    _arr[i, j] = int.Parse(arr[index]);
                    index++;
                }
            }
        }

        //конструктов (вызывается при создании игры)
        public Game()
        {
            //считываем данные из файла
            ReadFromFile();
        }

        //конструктор принимающий переменное число параметров
        //new Game(1,2,3,4,5,6,7,8,0) - например
        public Game(params int[] values)
        {
            _n = (int)Math.Sqrt(values.Length);
            _arr = new int[_n, _n];

            int index = 0;
            //данные преобразуем в целочисленный массив
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    _arr[i, j] = values[index];
                    index++;
                }
            }
        }
    }
}
