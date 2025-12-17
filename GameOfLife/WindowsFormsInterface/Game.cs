using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JeuDeLaVieInterfaceGraphique
{
    internal class Game
    {
        // Taille de la grille
        private int n;
        // Nombre d'itérations de la simulation
        private int iter;
        // Grille contenant toutes les cellules
        public Grid grid;
        // Liste des coordonnées des cellules vivantes au départ
        public List<Coords> AliveCellsCoords;
        // ★★★ Constructeur : initialise la simulation
        public Game(int nbCells, int nbIterations)
        {
            // TODO : initialiser n et iter
            n = nbCells;
            iter = nbIterations;
            // TODO : initialiser AliveCellsCoords avec une configuration initiale
            AliveCellsCoords = new List<Coords>
        {
            new Coords(1, 3),
            new Coords(2, 4),
            new Coords(3, 4),
            new Coords(3, 3),
            new Coords(3, 2)
        };

            // TODO : créer une nouvelle grille Grid(n, AliveCellsCoords)
            grid = new Grid(n, AliveCellsCoords);
            // (quelques exemples de configuration de départ sont fournis en fin de
            //sujet)
        }
        // ★★★ Méthode : exécute la simulation dans la console
        public void RunGameConsole()
        {
            // TODO : afficher la grille initiale avec grid.DisplayGrid()
            grid.DisplayGrid();
            // Boucle sur le nombre d'itérations
            for (int i = 0; i < iter; i++)
            {
                // TODO : mettre à jour la grille avec grid.UpdateGrid()
                grid.UpdateGrid();
                // TODO : afficher la grille après mise à jour avec grid.DisplayGrid()
                grid.DisplayGrid();
                // TODO : mettre en pause 1 seconde avec Thread.Sleep(1000)
                Thread.Sleep(600);
            }
        }
        public struct Coords
        {
            public Coords(double x, double y)
            {
                X = x;
                Y = y;
            }

            public double X { get; }
            public double Y { get; }

            public override string ToString() => $"({X}, {Y})";
        }
    }
}
