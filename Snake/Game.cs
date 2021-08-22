using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
  public class Game : MenuItem
  {
    public override void Run()
    {
      var wWidth = 20;
      var wHeight = 10;

      var wTop = $"┌{new string('─', wWidth - 2)}┐";
      var wMid = $"│{new string(' ', wWidth - 2)}│";
      var wBot = $"└{new string('─', wWidth - 2)}┘";
      var wClear = new string(' ', wWidth - 2);

      Console.Clear();
      Console.WriteLine(wTop);
      for (int i = 0; i < wHeight - 2; ++i)
      {
        Console.WriteLine(wMid);
      }
      Console.WriteLine(wBot);

      var wSnake = new Snake(wWidth / 2, wHeight / 2);
      var wDirection = Direction.Up;
      var wRandom = new Random();
      var wApplePosition = new Point()
      {
        X = wWidth / 2,
        Y = wHeight / 2 - 2
      };

      var wGameOver = false;
      while (!wGameOver)
      {
        Console.CursorTop = 1;
        for (int i = 0; i < wHeight - 2; ++i)
        {
          Console.CursorLeft = 1;
          Console.Write(wClear);
          Console.CursorTop++;
        }

        while (Console.KeyAvailable)
        {
          var wKey = Console.ReadKey(intercept: true);
          switch (wKey.Key)
          {
            case ConsoleKey.LeftArrow : wDirection = Direction.Left ; break;
            case ConsoleKey.RightArrow: wDirection = Direction.Right; break;
            case ConsoleKey.UpArrow   : wDirection = Direction.Up   ; break;
            case ConsoleKey.DownArrow : wDirection = Direction.Down ; break;
          }
        }

        wSnake.Move(wDirection);

        Console.SetCursorPosition(wApplePosition.X, wApplePosition.Y);
        Console.Write('#');

        wSnake.Draw();

        Thread.Sleep(200);

        if (wSnake.IsHitApple(wApplePosition.X, wApplePosition.Y))
        {
          wSnake.Grow();
          do
          {
            wApplePosition = new Point()
            {
              X = wRandom.Next(1, wWidth - 1),
              Y = wRandom.Next(1, wHeight - 1)
            };
          } while (wSnake.IsHitApple(wApplePosition.X, wApplePosition.Y));
        }

        wGameOver = wSnake.IsHitWall(wWidth, wHeight) || wSnake.IsHitItself();
      }

      for (int i = 0; i < 3; ++i)
      {
        Console.SetCursorPosition(0, 0);

        Console.WriteLine(wTop);
        for (int j = 0; j < wHeight - 2; ++j)
        {
          Console.WriteLine(wMid);
        }
        Console.WriteLine(wBot);

        Thread.Sleep(500);

        Console.SetCursorPosition(wApplePosition.X, wApplePosition.Y);
        Console.Write('#');

        wSnake.Draw();

        Thread.Sleep(500);
      }

      while (Console.ReadKey(intercept: true).Key is not (ConsoleKey.Enter or ConsoleKey.Spacebar)) ;
      Console.Clear();
    }
  }
}
