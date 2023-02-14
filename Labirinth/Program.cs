using System;
using .Collections.Generic;
using .Linq;
using .Text;
using .Threading.Tasks;
using .IO;
using System.Net.NetworkInformation;

namespace Labirinth
{
    class
    {
        static void Main(string[] args)
    {

        string path = @"C:\Users\burla\OneDrive\Рабочий стол\Lab.txt";

        int[][] labirinth = GetLabirinthFromFile(path);

        int x = 0;
        int y = 0;

        int way = 2;
        int wall = 1;
        int character = 0;
        int win = 3;


        PrintArr(labirinth, way, wall, character, win);
        FindPlayerPosition(ref x, ref y, labirinth, character);

        Console.WriteLine($"way = {way}");
        Console.WriteLine($"wall = {wall}");
        Console.WriteLine($"character = {character}");
        Console.WriteLine($"win = {win}");

        do
        {
            Win(labirinth, win);
            Wasd(ref x, ref y, labirinth, way, wall, character, win);

        } while (true);

    }

    static void Win(int[][] labirinth, int win)
    {
        int dollarcount = DollarCount(labirinth, win);
        //Console.WriteLine(dollarcount);
        if (dollarcount == 0)
            Console.WriteLine("Ты выиграл!");
    }

    static int[][] GetLabirinthFromFile(string path)
    {
        string[] textlabirinth = File.ReadAllLines(path);
        int[][] labirinth = new int[textlabirinth.Length][];

        for (int a = 0; a < textlabirinth.Length; a++)
        {
            char[] eachline = textlabirinth[a].ToCharArray();
            labirinth[a] = new int[eachline.Length];

            for (int b = 0; b < eachline.Length; b++)
            {
                labirinth[a][b] = Convert.ToInt32(eachline[b].ToString());
            }
        }
        return labirinth;
    }

    static void PrintArr(int[][] labirinth, int way, int wall, int character, int win)
    {
        int a;
        int b;

        for (a = 0; a < labirinth.Length; a++)
        {
            for (b = 0; b < labirinth[a].Length; b++)
            {
                if (labirinth[a][b] == wall)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(DefineWallSymbol(labirinth, a, b, wall));
                    Console.ResetColor();
                }

                else if (labirinth[a][b] == character)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(DefineCharacterSymbol());
                }

                else if (labirinth[a][b] == win)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(DefineWinSymbol());
                }

                else if (labirinth[a][b] == way)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(DefineWaySymbol());
                    Console.ResetColor();
                }
            }

            Console.WriteLine();
        }
    }


    static char DefineWallSymbol(int[][] labirinth, int a, int b, int wall)
    {
        if (((a - 1 >= 0 && labirinth[a - 1][b] == wall) || (a + 1 < labirinth.Length && labirinth[a + 1][b] == wall))
        && ((b - 1 >= 0 && labirinth[a][b - 1] == wall) || (b + 1 < labirinth[a].Length && labirinth[a][b + 1] == wall)))
        {
            return '+';
        }

        else if ((a - 1 >= 0 && labirinth[a - 1][b] == wall) || (a + 1 < labirinth.Length && labirinth[a + 1][b] == wall))
        {
            return '|';
        }

        else if ((b - 1 >= 0 && labirinth[a][b - 1] == wall) || (b + 1 < labirinth[a].Length && labirinth[a][b + 1] == wall))
        {
            return '-';
        }

        else
        {
            return '*';
        }
    }

    static char DefineCharacterSymbol()
    {
        return '@';
    }

    static char DefineWinSymbol()
    {
        return '$';
    }

    static char DefineWaySymbol()
    {
        return ' ';
    }

    static void FindPlayerPosition(ref int x, ref int y, int[][] labirinth, int character)
    {
        for (; x < labirinth.Length; x++)
        {
            bool stop_loop = false;

            for (y = 0; y < labirinth[x].Length; y++)
            {
                if (labirinth[x][y] == character)
                {
                    stop_loop = true;
                    break;
                }
            }
            if (stop_loop == true)
                break;
        }
    }

    static int DollarCount(int[][] labirinth, int win)
    {
        int dollar = 0;

        for (int x = 0; x < labirinth.Length; x++)
        {
            for (int y = 0; y < labirinth[x].Length; y++)
            {
                if (labirinth[x][y] == win)
                {
                    dollar += 1;
                }
            }
        }
        return dollar;
    }


    static void Wasd(ref int x, ref int y, int[][] labirinth, int way, int wall, int character, int win)
    {
        char wasd = Console.ReadKey().KeyChar;

        if (wasd != 'w' && wasd != 'a' && wasd != 's' && wasd != 'd')
        {
            Console.WriteLine("");
            Console.Write("Введите корректный символ");
            Console.WriteLine("");
            return;
        }

        switch (wasd)
        {
            case 'w':

                if (labirinth[x - 1][y] == win)
                {
                    x -= 1;
                    labirinth[x][y] = character;
                    labirinth[x + 1][y] = way;
                }

                else if (labirinth[x - 1][y] == way)
                {
                    x -= 1;
                    labirinth[x][y] = character;
                    labirinth[x + 1][y] = way;
                }

                else { }

                break;

            case 'a':

                if (labirinth[x][y - 1] == win)
                {
                    y -= 1;
                    labirinth[x][y] = character;
                    labirinth[x][y + 1] = way;
                }

                else if (labirinth[x][y - 1] == way)
                {
                    y -= 1;
                    labirinth[x][y] = character;
                    labirinth[x][y + 1] = way;
                }

                else { }

                break;

            case 's':

                if (labirinth[x + 1][y] == win)
                {
                    x += 1;
                    labirinth[x][y] = character;
                    labirinth[x - 1][y] = way;
                }

                else if (labirinth[x + 1][y] == way)
                {
                    x += 1;
                    labirinth[x][y] = character;
                    labirinth[x - 1][y] = way;
                }

                else { }

                break;

            case 'd':

                if (labirinth[x][y + 1] == win)
                {
                    y += 1;
                    labirinth[x][y] = character;
                    labirinth[x][y - 1] = way;
                }

                else if (labirinth[x][y + 1] == way)
                {
                    y += 1;
                    labirinth[x][y] = character;
                    labirinth[x][y - 1] = way;
                }

                else { }

                break;
        }
        Restart(labirinth, way, wall, character, win);
    }

    static void Restart(int[][] labirinth, int way, int wall, int character, int win)
    {
        Console.Clear();
        PrintArr(labirinth, way, wall, character, win);
    }
}
}
