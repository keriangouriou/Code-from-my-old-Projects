using System;
using System.Net.Http.Headers;
using System.Linq;
using System.ComponentModel;
using System.Numerics;

namespace JeuDeCombat
{
    class Program
    {
        static int width = 60;
        static string windowBorder = "-";

        static void DrawGame(Dictionary<string, int> player, Dictionary<string, int> opponent, string choice_p, int choice_ai)
        {
            Console.Clear(); // On each call, function erases the previous image

            // First window border
            for (int i = 0; i < width; i++) Console.Write(windowBorder);

            Console.Write("\n");

            // #### MIN GAME DRAWING ####
            //
            DrawCurrentStats(player, opponent);

            // Last window border
            for (int i = 0; i < width; i++) Console.Write(windowBorder);

            Console.Write("\n");

            // Choose action
            switch (choice_p)
            {
                case "1": Console.WriteLine("Vous attaquez."); break;
                case "2": Console.WriteLine("Vous vous protégez."); break;
                case "3": Console.WriteLine("Vous utilisez votre capacité spéciale !"); break;
                default: break;
            }
            switch (choice_ai)
            {
                case 1: Console.WriteLine("Votre opposant vous a attaqué."); break;
                case 2: Console.WriteLine("Votre opposant s'est protégé."); break;
                case 3: Console.WriteLine("Votre opposant a fait sa capacité spéciale !"); break;
                default: break;
            }
            for (int i = 0; i < width; i++)
                Console.Write(windowBorder);
            Console.WriteLine();
            Console.WriteLine("Choissisez votre action:");
            Console.WriteLine("1 -> Attaquer");
            Console.WriteLine("2 -> Défendre");
            Console.WriteLine("3 -> Action spéciale");
            Console.Write("Choix: ");
        }

        // Draws current stats of characters
        static void DrawCurrentStats(Dictionary<string, int> player, Dictionary<string, int> opponent)
        {
            Console.Write("VOUS");
            int temp = "VOUS".Length; // Temporary variable to save the number of used symbols in the line

            AlignOnRight("ADVERSAIRE", temp);

            Console.WriteLine();

            // Player 1 health
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{new string('O', player["health"])}");
            temp = $"{new string('O', player["health"])}".Length;
            AlignOnRight($"{new string('O', opponent["health"])}", temp);

            Console.WriteLine();
            // Player 1 attack points
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{new string('§', player["attk"])}");
            temp = $"{new string('§', player["attk"])}".Length;

            AlignOnRight($"{new string('§', opponent["attk"])}", temp);

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("\n");
        }

        // Alignes the text on the right with the right edge of the game area
        static void AlignOnRight(string text, int usedSymbols)
        {
            for (int i = 0; i < width - text.Length - usedSymbols; i++) { Console.Write(" "); } // Inserts spaces to make the text align on the right
            Console.Write(text);
        }

        static (Dictionary<string, int>, Dictionary<string, int>, string choice_p) TurnCalculation(Dictionary<string, int> Classes, Dictionary<string, int> player, Dictionary<string, int> opponent, string choice_p, int choice_ai)
        {
            bool protectedP = false;
            bool protectedAI = false;
            bool damageReverseP = false;
            bool damageReverseAI = false;
            int startHealthP = player["health"];
            int startHealthAI = opponent["health"];
        //Player Turn
        retry: choice_p = Console.ReadLine();
            switch (choice_p)
            {
                case "1": opponent["health"] -= player["attk"]; break;
                case "2": protectedP = true; break;
                case "3":
                    if (player["class"] == 1) { damageReverseP = true; }
                    if (player["class"] == 2) { player["health"] += 2; }
                    if (player["class"] == 3) { player["health"] -= 1; opponent["health"] -= 2; }
                    if (player["class"] == 4) { protectedP = true; }
                    break;
                default:
                    Console.WriteLine("Veuillez entrer une commande valide :");
                    goto retry;
            }

            //AI Turn
            if (choice_ai == 1)
            {
                player["health"] -= opponent["attk"];
            }
            if (choice_ai == 2)
            {
                protectedAI = true;
            }
            if (choice_ai == 3)
            {
                switch (opponent["class"])
                {
                    case 1: damageReverseAI = true; break;
                    case 2: opponent["health"] += 2; break;
                    case 3: opponent["health"] -= 1; player["health"] -= 2; break;
                    case 4: protectedAI = true; break;
                }
            }
            //Damager special attack

            if (damageReverseP == true) { opponent["health"] -= startHealthP - player["health"]; }
            if (damageReverseAI == true) { player["health"] -= startHealthAI - opponent["health"]; }
            // Health Calculation + Protect feature

            if (protectedP == true)
            {
                if (player["class"] == 4 && choice_p == "3") { if (player["health"] < startHealthP) { opponent["health"] -= 1; } }
                player["health"] = startHealthP;
                if (opponent["class"] == 3 && choice_ai == 2) { player["health"] -= 1; }
            }
            if (protectedAI == true)
            {
                if (opponent["class"] == 4 && choice_ai == 3) { if (opponent["health"] < startHealthAI) { player["health"] -= 1; } }
                opponent["health"] = startHealthAI;
                if (player["class"] == 3 && choice_p == "2") { opponent["health"] -= 1; }
            }
            if (player["health"] > player["maxHealth"]) { player["health"] = player["maxHealth"]; }
            if (opponent["health"] > opponent["maxHealth"]) { opponent["health"] = opponent["maxHealth"]; }
            return (player, opponent, choice_p);
        }
        public static void Main(string[] args)
        {

            Dictionary<string, int> player = new Dictionary<string, int>{
                {"class", 0},
                {"health", 0},
                {"attk", 0},
                {"maxHealth", 0},
            };

            Dictionary<string, int> opponent = new Dictionary<string, int>{
                {"class", 0},
                {"health", 0},
                {"attk", 0},
                {"maxHealth", 0},
            };

            Dictionary<string, int> classes = new Dictionary<string, int>{
                {"damager", 1},
                {"healer", 2},
                {"tank", 3},
                {"duelist", 4 },
                {"damager_health", 3},
                {"damager_attk", 2},
                {"healer_health", 4},
                {"healer_attk", 1},
                {"tank_health", 5},
                {"tank_attk", 1},
                {"duelist_health", 3},
                {"duelist_attk", 1},
            };

            Console.Clear();

            //Choice of the class of the player
            Console.WriteLine("Veuillez choisir la difficulté de l'IA:");
            Console.WriteLine("1 -> Facile");
            Console.WriteLine("2 -> Moyen");
            Console.WriteLine("3 -> Difficile");
            Console.Write("Choix: ");

            int aiLevel = 0;

        aiError: aiLevel = int.Parse(Console.ReadLine());

            if (!(new int[] { 1, 2, 3 }.Contains(aiLevel)))
            {
                Console.WriteLine("Veuillez entrer un niveau valide");
                goto aiError;
            }

            //Choice of the class by the player
            Console.WriteLine("\nVeuillez choisir votre personnage:");
            Console.WriteLine("1 -> Damager");
            Console.WriteLine("2 -> Healer");
            Console.WriteLine("3 -> Tank");
            Console.WriteLine("4 -> Duelist");
            Console.Write("Choix: ");

            string p_classe = "";
            string choice_p = "";
            int choice_ai = 0;


        //User Input to choose a class in the list

        error: p_classe = Console.ReadLine();

            switch (p_classe)
            {
                case "1":
                    player["class"] = int.Parse(p_classe);
                    player["health"] = classes["damager_health"];
                    player["attk"] = classes["damager_attk"];
                    player["maxHealth"] = classes["damager_health"];
                    break;
                case "2":
                    player["class"] = int.Parse(p_classe);
                    player["health"] = classes["healer_health"];
                    player["attk"] = classes["healer_attk"];
                    player["maxHealth"] = classes["healer_health"];
                    break;
                case "3":
                    player["class"] = int.Parse(p_classe);
                    player["health"] = classes["tank_health"];
                    player["attk"] = classes["tank_attk"];
                    player["maxHealth"] = classes["tank_health"];
                    break;
                case "4":
                    player["class"] = int.Parse(p_classe);
                    player["health"] = classes["duelist_health"];
                    player["attk"] = classes["duelist_attk"];
                    player["maxHealth"] = classes["duelist_health"];
                    break;
                default:
                    Console.WriteLine("Veuillez entrer une commande valide :");
                    goto error;
            }


            int aiChoice = AI.ChooseClass(aiLevel, int.Parse(p_classe));

            string aiChoiceString = (aiChoice == 1) ? "damager" : (aiChoice == 2) ? "healer" : (aiChoice == 3) ? "tank" : "duelist";



            opponent["class"] = classes[aiChoiceString];
            opponent["health"] = classes[aiChoiceString + "_health"];
            opponent["attk"] = classes[aiChoiceString + "_attk"];
            opponent["maxHealth"] = classes[aiChoiceString + "_health"];

            Console.WriteLine("\nVous êtes un {0}, vous avez {1} point(s) de vie et {2} point(s) d'attaque", classes.Keys.ElementAt(player["class"] - 1), player["health"], player["attk"]);
            Console.WriteLine("Votre adversaire est un {0}, il a {1} point(s) de vie et {2} point(s) d'attaque", classes.Keys.ElementAt(opponent["class"] - 1), opponent["health"], opponent["attk"]);
            Thread.Sleep(3000);

            bool gameO = false;
            choice_ai = 9;
            int turnCount = 0;
            while (gameO == false)
            {
                turnCount++;
                DrawGame(player, opponent, choice_p, choice_ai); // Drawing of the game and the possible actions


                choice_ai = AI.ChooseMove(aiLevel, opponent, player);
                (player, opponent, choice_p) = TurnCalculation(classes, player, opponent, choice_p, choice_ai);
                if (player["health"] < 1 || opponent["health"] < 1 || turnCount >= 15)
                {
                    if (player["health"] < 0) { player["health"] = 0; }
                    if (opponent["health"] < 0) { opponent["health"] = 0; }
                    DrawGame(player, opponent, choice_p, choice_ai);
                    gameO = true;
                }
            }

            Console.WriteLine("\n------------------------ Game end --------------------------");

            if (player["health"] <= 0 && opponent["health"] <= 0) { Console.WriteLine("Égalité."); }
            else if (opponent["health"] <= 0) { Console.WriteLine("Vous avez gagné !"); }
            else if (player["health"] <= 0) { Console.WriteLine("Vous avez perdu."); }
            else { Console.WriteLine("Nombre de tour dépassé, match nul."); }

        }
    }


    public class AI()
    {
        //This gets the AI to return whiwh hero it would like to play as
        public static int ChooseClass(int aiLevel, int humanChoice)
        {
            //1 = DMGR, 2 = HLR, 3 = TNK
            Random rand = new Random();

            //Depending on the level of the AI it will make different choices
            switch (aiLevel)
            {

                //RandomAI just picks one randomly
                case 1: return rand.Next(1, 5);

                //TacticalAI just picks one randomly
                case 2: return rand.Next(1, 5);

                //StrategicAI picks the hero best suited against the player's choice
                case 3:
                    switch (humanChoice)
                    {
                        //We get the hero that the player's choice loses the most with
                        case 1: return 2;
                        case 2: return 1;
                        case 3: return 2;
                        case 4: return 2;
                        default: return 0;
                    }
                default: return 6;
            }
        }

        //Here the AI choses a move to make based on the current state of their and their enemy's stats
        public static int ChooseMove(int aiLevel, Dictionary<string, int> opponent, Dictionary<string, int> player)
        {
            //1 = Attack, 2 = Defend, 3 = SpAttack
            Random rand = new Random();
            int res = 0;
            //Depending on the level the move is chosen differently
            switch (aiLevel)
            {

                //RandomAI always picks randomly ofc
                case 1:
                    return rand.Next(1, 4);

                //TacticalAI and StrategicAI both use the same flowchart
                case >= 2:
                    switch (opponent["class"])
                    {
                        //The Damager always attacks unless the enemy's attack is more powerfull than our HP, else he defends. If the enemy is in critical range (2HP or less), we attack regardless of our HP
                        case 1:
                            if (opponent["health"] == 3 || player["health"] <= 2) { res = 1; break; }
                            else if (player["attk"] >= opponent["health"]) { res = 2; break; }
                            else { res = 1; break; }

                        //The Healer attacks until their HP drops below 3, then he heals 
                        case 2:
                            if (opponent["health"] < 3) { res = 3; break; }
                            else { res = 1; break; }

                        //The Tank will defend against enemy attacks of 2 power, else he uses his Special until his HP == 1 then he uses standard attack
                        case 3:
                            if (player["attk"] == 2) { res = 2; break; }
                            else if (opponent["health"] == 1) { res = 1; break; }
                            else { res = 3; break; }
                        //The Duelist will try to pary every turn against damager and tank and pary only if the opponent has 2
                        case 4:
                            if (player["class"] == 1 || player["class"] == 3 || player["class"] == 4) { res = 3; break; }
                            else if (player["class"] == 2 && player["health"] >= 3) { res = 3; break; }
                            else { res = 1; break; }
                    }
                    return res;
                default:
                    return 1;
            }
        }
    }
}