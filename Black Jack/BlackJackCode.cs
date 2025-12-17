// See https://aka.ms/new-console-template for more information
using System.Threading.Tasks.Sources;

//Initialisation des variables
///Dictionnaires
Dictionary<string, int> valeurCartes = new Dictionary<string, int>();
valeurCartes.Add("1", 11);
valeurCartes.Add("2", 2);
valeurCartes.Add("3", 3);
valeurCartes.Add("4", 4);
valeurCartes.Add("5", 5);
valeurCartes.Add("6", 6);
valeurCartes.Add("7", 7);
valeurCartes.Add("8", 8);
valeurCartes.Add("9", 9);
valeurCartes.Add("10", 10);
valeurCartes.Add("V", 10);
valeurCartes.Add("D", 10);
valeurCartes.Add("R", 10);

///Entiers
int scoreActuelO = 0;
int scoreActuelH = 0;
int nbrDePaquet;

///Booléennes
bool stopJoueur = false;
bool stopOrdinateur = false;
bool finDePartie = false;

///Chaînes de caractères
string nomDuJoueur ="";
string nomOrdi = "Ordinateur";

///Listes
List<string> joueurH = new List<string>();
List<string> joueurO = new List<string>();
List<string> paquet = new List<string>();

//Nommage du joueur Humain
while (nomDuJoueur.Length < 3)
{
    Console.WriteLine("Comment vous apppelez-vous ?");
    nomDuJoueur = Console.ReadLine();
    if (nomDuJoueur.Length < 3)
    {
        Console.WriteLine("Veuiller selectioner un nom de 3 lettres ou plus.");
    }
    Console.WriteLine("");
}

//Création du paquet de cartes
Console.WriteLine("Combien de paquet(s) de cartes voulez vous dans la partie, {0} ?",nomDuJoueur);
while (true)
{
    if(int.TryParse(Console.ReadLine(), out nbrDePaquet))
    {
        if (nbrDePaquet <= 0)
        {
            Console.WriteLine("Entrer un nombre valide de paquet(s).");
            continue;
        }
        else
        {
            break;
        }
    }
    else
    {
        Console.WriteLine("Entrer un nombre valide de paquet(s).");
    }
}
foreach (var carte in valeurCartes)
{
    for (int i = 0; i < nbrDePaquet*4; i++)
    {
        paquet.Add(carte.Key);
    }
}
Console.WriteLine("Vous allez jouer avec {0} paquet(s) pour un total de {1} cartes.",nbrDePaquet,13*4*nbrDePaquet);
Console.WriteLine("");

//Mélange du paquet de cartes
var paquetShuffled = paquet.OrderBy(x => Guid.NewGuid()).ToList();

//Distribution des cartes initiales
for (int i = 0;i < 2;i++)
{
    joueurH.Add(paquetShuffled[0]);
    paquetShuffled.RemoveAt(0);
    joueurO.Add(paquetShuffled[0]);
    paquetShuffled.RemoveAt(0);
}

Score(valeurCartes, nomDuJoueur, joueurH, out scoreActuelH, stopJoueur);
Score(valeurCartes, nomOrdi, joueurO, out scoreActuelO, stopOrdinateur);

//Boucle de gameplay
while (finDePartie == false && scoreActuelH != 21)
{
    Console.WriteLine("");
    ///Tour du joueur humain
    if (stopJoueur == false)
    {
        if (scoreActuelH != 21)
        {
            Console.WriteLine("Voulez-vous piocher une nouvelle carte ?");
            Console.WriteLine("o - Oui");
            Console.WriteLine("n - Non");
            Console.WriteLine("Que voulez-vous répondre ?");
            while (true)
            {
                string choixJoueur = Console.ReadLine();
                if (choixJoueur == "o" || choixJoueur == "O")
                {
                    Console.WriteLine("");
                    Console.WriteLine("{0} : Je pioche.", nomDuJoueur);
                    joueurH.Add(paquetShuffled[0]);
                    paquetShuffled.RemoveAt(0);
                    break;
                }
                else if (choixJoueur == "n" || choixJoueur == "N")
                {
                    Console.WriteLine("{0} : Je m'arrête là.", nomDuJoueur);
                    stopJoueur = true;
                    break;
                }
                else
                {
                    Console.WriteLine("Veuillez entrer une réponse possible :");
                    Console.WriteLine("o - Oui");
                    Console.WriteLine("n - Non");
                    Console.WriteLine("Que voulez-vous répondre ?");
                }
            }
        }
        else { stopJoueur = true; }

        Score(valeurCartes, nomDuJoueur, joueurH, out scoreActuelH, stopJoueur);
       
        if (scoreActuelH > 21)
        {
            break;
        }
    }

    ///Tour du joueur ordinateur
    if (stopOrdinateur == false)
    {
        if (scoreActuelO <= 15)
        {
            Console.WriteLine("Ordinateur : Je pioche.");
            joueurO.Add(paquetShuffled[0]);
            paquetShuffled.RemoveAt(0);
        }
        else
        {
            Console.WriteLine("Ordinateur : Je m'arrête là.");
            stopOrdinateur = true;
        }
    }
    ///Affichage des scores
    Score(valeurCartes, nomOrdi, joueurO, out scoreActuelO, stopOrdinateur);
    ///Vérfication des conditions de fin de partie
    if (stopJoueur == true && stopOrdinateur == true)
    {
        finDePartie = true;
    }
    if (scoreActuelO >= 21 || scoreActuelH >= 21)
    {
        finDePartie = true;
    }
}
Console.WriteLine("");
//Choix du message de fin selon la situation
if (scoreActuelH > 21) { Console.WriteLine("Bust, vous avez dépassé 21pts et perdez donc la partie.");}
if (scoreActuelO > 21) { Console.WriteLine("Bust, l'ordinateur a dépassé 21pts et perd donc la partie.");}
if (scoreActuelH == 21 && joueurH.Count == 2 && scoreActuelO == 21 && joueurO.Count == 2) { Console.WriteLine("Wow, Double Black Jack ! Match nul."); }
else
{
    if (scoreActuelH == 21 && joueurH.Count == 2) { Console.WriteLine("Black Jack ! Vous remportez la partie."); }
    if (scoreActuelO == 21 && joueurO.Count == 2) { Console.WriteLine("Black Jack ! L'Ordinateur remporte la partie."); }
}
if (scoreActuelH == 21 && joueurH.Count != 2 && scoreActuelO == 21 && joueurO.Count != 2) { Console.WriteLine("Double 21 ! Match nul."); }
else
{
    if (scoreActuelH == 21 && joueurH.Count != 2 && scoreActuelO != 21) { Console.WriteLine("Vous avez pile 21, Vous remportez la partie."); }
    if (scoreActuelO == 21 && joueurO.Count != 2 && scoreActuelH !=21) { Console.WriteLine("L'Ordinateur a pile 21, il remporte la partie."); }
}
if (scoreActuelH < 21 && scoreActuelO < 21 && scoreActuelH > scoreActuelO) { Console.WriteLine("Vous avez {0}pts et l'Ordinateur a {1}pts. Vous remportez la victoire.", scoreActuelH, scoreActuelO); }
if (scoreActuelH < 21 && scoreActuelO < 21 && scoreActuelH < scoreActuelO) { Console.WriteLine("Vous avez {0}pts et l'Ordinateur a {1}pts. L'ordinateur remporte la victoire.", scoreActuelH, scoreActuelO); }
if (scoreActuelH < 21 && scoreActuelO < 21 && scoreActuelH == scoreActuelO) { Console.WriteLine("Vous et l'Ordinateur avez {0}pts. Match nul.", scoreActuelH, scoreActuelO); }
Console.WriteLine("Scores finaux :");
Score(valeurCartes, nomDuJoueur, joueurH, out scoreActuelH, stopJoueur);
Score(valeurCartes, nomOrdi, joueurO, out scoreActuelO, stopOrdinateur);
Console.WriteLine("  -- Fin de partie --");


static void Score(Dictionary<string,int> valeurCartes, string nomDuParticipant, List<string> mainDuParticipant, out int scoreParticipant, bool stopDuParticipant)
{
    int scoreActuel = 0;
    foreach (var carte in mainDuParticipant)
    {
        scoreActuel += valeurCartes[carte];
    }
    if (scoreActuel > 21)
    {
        foreach (var carte in mainDuParticipant)
        {
            if (carte == "1")
            {
                scoreActuel -= 10;
                if (scoreActuel <= 21)
                {
                    break;
                }
            }
        }
    }
    scoreParticipant = scoreActuel;
    if (nomDuParticipant == "Ordinateur")
    {

        if (mainDuParticipant.Count == 2 && stopDuParticipant == false)
        {
            Console.WriteLine("({1}pts + ?) Ordinateur : ? {0}", mainDuParticipant[1], valeurCartes[mainDuParticipant[1]]);
        }
        else
        {
            Console.Write("({1}pts) {0} :", nomDuParticipant, scoreActuel);
            foreach (var carte in mainDuParticipant)
            {
                Console.Write(" {0}", carte);
            }
            Console.WriteLine("");
        }
    }
    else
    {
        Console.Write("({1}pts) {0} :", nomDuParticipant, scoreActuel);
        foreach (var carte in mainDuParticipant)
        {
            Console.Write(" {0}", carte);
        }
        Console.WriteLine("");
    }
}