using System;
using System.Collections.Generic;
using System;
public class Cell
{
    // Attribut privé représentant l'état actuel de la cellule
    private bool _isAlive;
    // Accesseur pour lire/modifier _isAlive
    public bool isAlive { get { return _isAlive; } set { _isAlive = value; } }
    // Attribut privé pour stocker l'état futur de la cellule
    private bool _nextState;
    // Accesseur pour lire/modifier _nextState
    public bool nextState
    {
        get { return _nextState; }
        set { _nextState = value; }
    }
    // ★☆☆ Constructeur : initialise la cellule avec un état donné
    public Cell(bool state)
    {
        _isAlive = state;
        nextState = state;
    }
    // ★☆☆ Méthode : fait vivre la cellule au prochain pas de simulation
    public void ComeAlive()
    {
        nextState = true;
    }
    // ★☆☆ Méthode : tue la cellule au prochain pas de simulation
    public void PassAway()
    {
        nextState = false;
    }
    // ★☆☆ Méthode : met à jour _isAlive avec _nextState
    public void Update()
    {
        _isAlive = nextState;
    }
}

