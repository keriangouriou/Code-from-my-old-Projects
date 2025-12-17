using System;
using System.Collections.Generic; // Pour List<T>
public class Grid
{
    // Taille de la grille
    private int _n;
    public int n { get { return _n; } set { _n = value; } }
    // Tableau 2D de cellules
    public Cell[,] TabCells;
    // ★★★ Constructeur : initialise la grille et les cellules
    public Grid(int nbCells, List<Game.Coords> AliveCellsCoords)
    {
        // TODO : initialiser n et TabCells
        n = nbCells;
        TabCells = new Cell[n,n];
        // TODO : remplissage du tableau avec à chaque emplacement une instance
        for (int x = 0; x < n; x++)
        {
            for (int y = 0; y < n; y++)
            {
                if (AliveCellsCoords.Contains(new Game.Coords(x, y)))
                { TabCells[x, y] = new Cell(true); }
                else { TabCells[x, y] = new Cell(false); }
            }
        }
        //d’une cellule Cell créée vivante(true) si les coordonnées sont dans la liste
        //AliveCellsCoords ou absente(false) sinon.
    }
    // ★★☆ Méthode : retourne le nombre de cellules vivantes autour d'une cellule
 public int getNbAliveNeighboor(int x, int y)
    {
        int count = 0;

        if (y > 0)
        {
            for (int i = -1; i < 2; i++) 
            {
                if (x > 0 && x + i < n) { if (TabCells[x + i, y - 1].isAlive == true) { count++; } }
            }
        }
        if (y < n -1)
        {
            for (int i = -1; i < 2; i++)
            {
                if (x > 0 && x + i < n) { if (TabCells[x + i, y + 1].isAlive == true) { count++; } }
            }
        }
        for (int i = -1; i < 2; i++)
        {
            if (i != 0) { if (x > 0 && x + i < n) { if (TabCells[x + i, y].isAlive == true) { count++; } } }
        }
        return count;
    }
    // ★★☆ Méthode : retourne les coordonnées valides autour d'une cellule
    public List<Game.Coords> getCoordsCellsAlive()
    {
        return new List<Game.Coords>();
    }
    // ★★☆ Méthode : afficher la grille en console (X pour cellule vivante)
    public void DisplayGrid()
    {
        Console.Clear();
        for (int y = 0; y < n ; y++)
        {
            Console.WriteLine("");
            for (int x = 0; x < n; x++)
            {
                if (TabCells[x, y].isAlive) { Console.Write("██"); }
                else { Console.Write(".."); }
            }
        }
    }
    // ★★★ Méthode : mettre à jour la grille selon les règles du jeu
    public void UpdateGrid()
    {
        /* Méthode qui parcourt chaque cellule et qui met à jour leur attribut
        _nextStep, via son accesseur en écriture, en fonction des règles de la simulation.
        L’attribut est mis à true si la cellule reste en vie ou apparaît et à false si la
   cellule à cet emplacement disparaît ou reste absente. Une fois toute la grille
        parcourue, une deuxième passe est effectué pour associer la valeur de nextStep à
   l’attribut isAlive de chaque cellule.*/
        // TODO : première passe : calculer nextState pour chaque cellule
        int aliveNeigboor;
        for (int y = 0; y < n ; y++)
        {
            for (int x = 0; x < n; x++)
            {
                aliveNeigboor = getNbAliveNeighboor(x, y);
                if (aliveNeigboor == 3) { TabCells[x, y].nextState = true;}
                else if (aliveNeigboor != 2) { TabCells[x, y].nextState = false;}
                
            }
        }
        // TODO : deuxième passe : appliquer nextState à _isAlive
        for (int y = 0; y < n; y++)
        {
            for (int x = 0; x < n; x++)
            {
               TabCells[x, y].Update();
            }
        }
    }
}

