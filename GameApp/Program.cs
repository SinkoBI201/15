using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //создаем объект "игра"
            Game game = new Game();
            //вывод на консоль игрового поля
            game.Print();

            //int val = game[0, 2]; //пример индексатора, получение значения в заданной ячейке

            int oper = 0;

            //выводим меню
            do
            {
                Console.WriteLine("1.Сделать след шаг");
                Console.WriteLine("2.Выход");

                oper = int.Parse(Console.ReadLine());

                switch (oper)
                {
                    case 1:
                        {
                            try
                            {
                                Console.WriteLine("Введите значение перемещаемого элемента ");
                                int val = int.Parse(Console.ReadLine());

                                //делаем ход (меняем местами 0 и то значение которое ввели, если они явл-ся соседями)
                                game.Shift(val);

                                //выводим новое состояние игрового поля
                                game.Print();
                            }
                            catch (Exception exc) //отлавливаем исключение
                            {
                                Console.WriteLine(exc.Message);
                            }
                        }
                        break;
                    case 2:
                        break;
                    default:
                        break;
                }
            }
            while (oper != 2);
        }
    }
}
