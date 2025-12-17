using System;
namespace JeuDeLaVie
{
    static class Program
    {
        static void Main()
        {
            Game game = new Game(21, 100);
            game.RunGameConsole();
        }
    }
}