using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace freemove
{
    class set_x_y//坐标类
    {
        public int x, y;
        public set_x_y(int x1 = 0, int y1 = 0)
        {
            x = x1;
            y = y1;
        }
    }

    class Game
    {

        public const int mapsize = 20;//地图尺寸
        public void draw()//绘制地图
        {
            for (int i = 0; i < mapsize; i++)
            {
                for (int j = 0; j < mapsize; j++)
                {
                    if (i == 0 || i == mapsize - 1 || j == 0 || j == mapsize - 1)
                        Console.Write("█");
                    else Console.Write("  ");

                }
                Console.WriteLine();
            }
        }
        public const char head = 'O';
        public const char body = 'o';
        
        public void set(int x, int y, char b) //坐标以及输出
        {
            Console.SetCursorPosition(2 * y, x);
            Console.Write(b);
            Console.SetCursorPosition(0, mapsize);
        }
    }
    class control//游戏控制类
    {
        public  bool gameover = false;//游戏结束的判断
        int dir1 = 3;//初始方向
        Game weiz = new Game();
        List<set_x_y> snake = new List<set_x_y>();//定义蛇身数组
        set_x_y food = new set_x_y();//食物坐标
        
        public void move()
        {
            bool hitwall = false;
            bool hitbody = false;
            snake.Add(new set_x_y(5, 6));//初始化身体位置和长度。
            snake.Add(new set_x_y(4, 6));//注 我这边设置了两身体，一个头，然而运行时只出现一身体
            snake.Add(new set_x_y(3, 6));
            weiz.set(snake[1].x, snake[1].y, Game.body);
            weiz.set(snake[2].x, snake[2].y, Game.body);
            weiz.set(snake[0].x, snake[0].y, Game.head);
            Thread th = new Thread(new ThreadStart(dir));//这条命名不太懂
            th.Start();
            eatfood();//初始生成食物
            while (!gameover)
            {
             
              
                    if (food.x == snake[0].x && food.y == snake[0].y)//判断是否吃到食物
                    {
                    snake.Add(new set_x_y(snake[snake.Count - 1].x, snake[snake.Count - 1].y));//增加长度
                    eatfood();//食物产生
                    }

               
                else//没有吃到食物保持末尾空白 
                {
                    weiz.set(snake[snake.Count-1 ].x, snake[snake.Count- 1].y, ' ');
                }
                for (int i = snake.Count - 1; i >= 1; i--)//使身体位置跟着头的坐标变化
                {
                    snake[i].x = snake[i - 1].x;//后一个到前一个的坐标
                    snake[i].y = snake[i - 1].y;//后一个到前一个的坐标
                }
                for(int i=1;i<snake.Count;i++)//厉遍身体，可能边长
                weiz.set(snake[i].x, snake[i].y, Game.body);//产生身体
                weiz.set(snake[0].x, snake[0].y, Game.head);//初始头位置
                switch (dir1)//头位置坐标根据方向变换
                {
                    case 1: snake[0].x--; break;
                    case 2: snake[0].y--; break;
                    case 3: snake[0].x++; break;
                    case 4: snake[0].y++; break;
                }
                if (snake[0].x == 0 || snake[0].y == 0 || snake[0].x == Game.mapsize-1|| snake[0].y == Game.mapsize-1)//判断是否撞墙
                {
                    Console.Clear();
                    hitwall = true;
                   
                }
                for (int i = 1; i <= snake.Count - 1; i++)//厉遍判断
                {
                  
                    if (snake[0].x == snake[i].x && snake[0].y == snake[i].y)//检验头是否与身体碰撞
                    {
                        Console.Clear();
                        hitbody = true;
                    
                    }
                }
                if (hitbody || hitwall)//判断是否游戏结束
                    gameover = true;
                Thread.Sleep(300);//保证显示效果设置成300ms
            }
            if (gameover)//如果游戏结束
            {
                Console.Clear();//情况之前的所有函数
                Console.Write("游戏结束");//输出
            }
            
        }
          void dir()//操作定义
             {

                for (;;)
                {
                    char  c = Console.ReadKey(true).KeyChar;//输入方向
                    switch (c)
                    {

                        case 'w': if (dir1 != 3) dir1 = 1; break;
                        case 'a': if (dir1 != 4) dir1 = 2; break;
                        case 's': if (dir1 != 1) dir1 = 3; break;
                        case 'd': if (dir1 != 2) dir1 = 4; break;
                    }
                }

             }
        void eatfood()//食物的出现
        {
            
            Random coor = new Random();//随机数的定义
            while(!gameover)//判断游戏是否结束，不结束则出现食物
            {
                food.x = coor.Next(1, Game.mapsize - 1);//食物x坐标
                food.y = coor.Next(1, Game.mapsize - 1);//食物y坐标
                bool eat = false;//判断是否被吃
                for (int i=0;i<snake.Count;i++)//厉遍蛇身体的位置是否有出现食物位置
                {
                    if(snake[i].x==food.x&&snake[i].y==food.y)
                    {
                        eat = true;
                        break;
                    }
                }
                if (eat == false)//若没有被吃，则生成食物
                    break;
            }
            weiz.set(food.x, food.y, '*');//生成食物
        }
        
    }
    class start//启动
    {
       public  control game = new control();
       public Game map = new Game();

    }       
    class Program
    {
        static void Main(string[] args)
        {
            start g = new start();//定义一个启动类
            Console.CursorVisible = false;//隐藏光标
            g.map.draw();//做出地图
            g.game.move();//游戏运行
            Console.ReadLine();

        }
    }
}
