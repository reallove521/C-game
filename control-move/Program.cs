using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_move
{
    class Map
    {
        public const int mapsize = 11;
        private int r;//行
        private int c;//列
        public string[,] map = new string[mapsize,mapsize];
       
        public void creat()//画边界
        {
            for(r=0;r<mapsize;r++)
            {
                for(c=0;c<mapsize;c++)
                {
                    if (r == 0 || r == 10)
                        map[r, c] = "█";
                    else map[r, c] = "  ";
                         map[r, 0] = "█";
                         map[r, 10] = "█";
                    Console.Write(map[r, c]);
                }
                Console.WriteLine();
            }     
        }
    }
    class body//物体属性
    {
        public int x = 1,y=1;//一开始位置
        public string  mark = "sb";//人物图标
    }
    class play//进行操作变化
    {
        body p = new body();
        public char move;//输入键
        public void movement(char move, Map map)
        {
            switch (move)
            {
                case 'a':
                case 'A':
                    {
                        if(map.map[p.y-1,p.x]!= "█")//不撞墙
                        {
                            map.map[p.y, p.x] = "  ";//将原本位置的人物标识，变空
                            Console.SetCursorPosition(2 * p.y, p.x);//这边真的不太懂，没有这个是不行的，会出错，会出现重复像
                            Console.Write(map.map[p.y, p.x]);//这边输出
                            p.y = p.y - 1;
                            map.map[p.y, p.x] = p.mark;//移动到的位置
                            Console.SetCursorPosition(2 * p.y, p.x);
                            Console.Write(map.map[p.y, p.x]);//输出该位置，以下同理
                        }
                        break;
                    }
                case 'd':
                case 'D':
                    {
                        if (map.map[p.y + 1, p.x] != "█")
                        {
                            map.map[p.y, p.x] = "  ";
                            Console.SetCursorPosition(2 * p.y, p.x);
                            Console.Write(map.map[p.y, p.x]);
                            p.y = p.y + 1;
                            map.map[p.y, p.x] = p.mark;
                            Console.SetCursorPosition(2 * p.y, p.x);
                            Console.Write(map.map[p.y, p.x]);
                        }
                        break;
                    }
                case 'w':
                case 'W':
                    {
                        if (map.map[p.y, p.x-1] != "█")
                        {
                            map.map[p.y, p.x] = "  ";
                            Console.SetCursorPosition(2 * p.y, p.x);
                            Console.Write(map.map[p.y, p.x]);
                            p.x = p.x - 1;
                            map.map[p.y, p.x] = p.mark;
                            Console.SetCursorPosition(2 * p.y, p.x);
                            Console.Write(map.map[p.y, p.x]);
                        }
                        break;
                    }
                case 's':
                case 'S':
                    {
                        if (map.map[p.y, p.x+1] != "█")
                        {
                            map.map[p.y, p.x] = "  ";
                            Console.SetCursorPosition(2 * p.y, p.x);
                            Console.Write(map.map[p.y, p.x]);
                            p.x = p.x + 1;
                            map.map[p.y, p.x] = p.mark;
                            Console.SetCursorPosition(2 * p.y, p.x);
                            Console.Write(map.map[p.y, p.x]);
                        }
                        break;
                    }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Map world = new Map();
            play player = new play();
            ConsoleKeyInfo input;//从键盘键入
            world.creat();    
            while(true)
            {
                input  = Console.ReadKey();//输入方向
                player.move = input.KeyChar;//进行转化
                player.movement(player.move, world);
                Console.SetCursorPosition(0,11);//重置光标位置
                Console.Write(' ');//
                Console.SetCursorPosition(0, 11);//没有以下这几个会使得你输入的键出现在游戏中
            }
        }
    }
}
