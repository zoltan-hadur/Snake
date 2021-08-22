using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
  public class MenuItem
  {
    public string Title { get; set; }
    public MenuItem Parent { get; set; } = null;
    public List<MenuItem> SubMenuItems { get; } = new();

    public void AddSubMenuItem(MenuItem subMenuItem)
    {
      SubMenuItems.Add(subMenuItem);
      subMenuItem.Parent = this;
    }

    public virtual void Run()
    {
      var wStartPosition = Console.GetCursorPosition();
      var wSelectedMenuItemIndex = 0;
      var wGoBackToPreviousMenuOrQuit = false;

      while (!wGoBackToPreviousMenuOrQuit)
      {
        // Menu title and sub menu items including Go Back / Quit
        Console.WriteLine(Title);
        foreach (var wSubMenuItem in SubMenuItems)
        {
          Console.WriteLine($" {wSubMenuItem.Title}");
        }
        Console.WriteLine($" {(Parent == null ? "Quit" : "Go Back")}");

        // Indicate selected sub menu item with a '>' char
        Console.CursorLeft = 0;
        Console.CursorTop = Console.CursorTop - SubMenuItems.Count - 1 + wSelectedMenuItemIndex;
        Console.Write('>');
        Console.CursorLeft = 0;

        // Sub menu item selection
        var wUserHasSelectedTheSubMenuItem = false;
        while (!wUserHasSelectedTheSubMenuItem)
        {
          var wKey = Console.ReadKey(intercept: true);
          switch (wKey.Key)
          {
            case ConsoleKey.UpArrow:
              if (wSelectedMenuItemIndex > 0)
              {
                wSelectedMenuItemIndex--;
                Console.MoveBufferArea(Console.CursorLeft, Console.CursorTop--, 1, 1, Console.CursorLeft, Console.CursorTop);
              }
              break;
            case ConsoleKey.DownArrow:
              if (wSelectedMenuItemIndex < SubMenuItems.Count)
              {
                wSelectedMenuItemIndex++;
                Console.MoveBufferArea(Console.CursorLeft, Console.CursorTop++, 1, 1, Console.CursorLeft, Console.CursorTop);
              }
              break;
          }
          wUserHasSelectedTheSubMenuItem = wKey.Key is ConsoleKey.Enter or ConsoleKey.Spacebar;
        }

        // Clear the menu title, its sub menu item titles including Go back / Quit
        Console.SetCursorPosition(wStartPosition.Left, wStartPosition.Top);
        for (int i = 0; i < SubMenuItems.Count + 2; ++i)
        {
          Console.Write(new string(' ', Console.WindowWidth));
        }
        Console.SetCursorPosition(wStartPosition.Left, wStartPosition.Top);

        // Run the selected sub menu item
        if (wSelectedMenuItemIndex < SubMenuItems.Count)
        {
          SubMenuItems[wSelectedMenuItemIndex].Run();
        }
        else
        {
          wGoBackToPreviousMenuOrQuit = true;
        }
      }
    }
  }
}
