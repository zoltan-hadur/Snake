using System;
using System.Collections.Generic;

namespace Snake
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var wGame = new Game()
      {
        Title = "Game"
      };

      var wHighScores = new MenuItem()
      {
        Title = "High Scores"
      };

      var wSettings = new MenuItem()
      {
        Title = "Settings"
      };

      var wMainMenu = new MenuItem()
      {
        Title = "Main Menu"
      };
      wMainMenu.AddSubMenuItem(wGame);
      wMainMenu.AddSubMenuItem(wHighScores);
      wMainMenu.AddSubMenuItem(wSettings);

      Console.CursorVisible = false;
      wMainMenu.Run();
    }
  }
}
