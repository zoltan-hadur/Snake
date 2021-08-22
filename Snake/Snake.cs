using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
  public enum Direction
  {
    Up,
    Right,
    Down,
    Left
  }

  public class Point
  {
    public int X { get; set; }
    public int Y { get; set; }
  }

  public class Element
  {
    public Point Position { get; set; }
    public Direction Direction { get; set; }
  }

  public class Snake
  {
    public List<Element> Elements = new();

    public Snake(int x, int y)
    {
      Elements.Add(new Element()
      {
        Position = new Point()
        {
          X = x,
          Y = y
        }
      });
    }

    public void Move(Direction direction)
    {
      Elements.First().Direction = direction;
      for (int i = Elements.Count - 1; i >= 0; --i)
      {
        if (i > 0 &&
            Elements[i].Position.X == Elements[i - 1].Position.X &&
            Elements[i].Position.Y == Elements[i - 1].Position.Y)
        {
          continue;
        }
        switch (Elements[i].Direction)
        {
          case Direction.Left : Elements[i].Position.X--; break;
          case Direction.Right: Elements[i].Position.X++; break;
          case Direction.Up   : Elements[i].Position.Y--; break;
          case Direction.Down : Elements[i].Position.Y++; break;
        }
      }
      for (int i = Elements.Count - 1; i > 0; --i)
      {
        Elements[i].Direction = Elements[i - 1].Direction;
      }
    }

    public void Grow()
    {
      Elements.Add(new Element()
      {
        Position = new Point()
        {
          X = Elements.Last().Position.X,
          Y = Elements.Last().Position.Y,
        },
        Direction = Elements.Last().Direction
      });
    }

    public void Draw()
    {
      foreach (var wElement in Elements)
      {
        Console.SetCursorPosition(wElement.Position.X, wElement.Position.Y);
        Console.Write('O');
      }
      Console.SetCursorPosition(Elements.First().Position.X, Elements.First().Position.Y);
      Console.Write('X');
    }

    public bool IsHitWall(int width, int height)
    {
      var wPosition = Elements.First().Position;
      return wPosition.X == 0 || wPosition.X == width  - 1 ||
             wPosition.Y == 0 || wPosition.Y == height - 1;
    }

    public bool IsHitItself()
    {
      var wPosition = Elements.First().Position;
      return Elements.Skip(1).Any(wElement => wElement.Position.X == wPosition.X && wElement.Position.Y == wPosition.Y) &&
             Elements.Count > 2;
    }

    public bool IsHitApple(int x, int y)
    {
      return Elements.Any(wElement => wElement.Position.X == x && wElement.Position.Y == y);
    }
  }
}
