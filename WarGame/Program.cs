using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarGame
{
    public class Program
    {
        enum Suits : byte { Diamonds = 1, Clubs, Spades, Hearts };
        enum Ranks : byte { Two = 1, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace };

        static List<Tuple<Suits, Ranks>> newCards = new List<Tuple<Suits, Ranks>>();
        static List<Tuple<Suits, Ranks>> shuffledCards = new List<Tuple<Suits, Ranks>>();
        static List<Tuple<Suits, Ranks>> playerOne = new List<Tuple<Suits, Ranks>>();
        static List<Tuple<Suits, Ranks>> playerTwo = new List<Tuple<Suits, Ranks>>();
        static List<Tuple<Suits, Ranks>> onTheTable = new List<Tuple<Suits, Ranks>>();

        public static void Main(string[] args)
        {
            // Create new deck
            CreateDeck();

            // Shuffle the deck
            ShuffleDeck();

            // Deal cards
            DealDeck();

            // Play game
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("PLAY GAME");

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("GAME OVER. PLAYER " + PlayGame(true) + " WON!");

            Console.ReadKey(true);
        }

        // Creates new deck and returns number of cards
        public static int CreateDeck()
        {
            newCards.Clear();

            foreach (Suits S in Suits.GetValues(typeof(Suits)))
                foreach (Ranks R in Ranks.GetValues(typeof(Ranks)))
                    newCards.Add(Tuple.Create(S, R));

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("NEW DECK");
            foreach (Tuple<Suits, Ranks> card in newCards)
                Console.Write(card + " ");

            return newCards.Count;
        }

        // Shuffles the deck and returns number of card
        public static int ShuffleDeck()
        {
            Random random = new Random();

            shuffledCards.Clear();

            while (newCards.Count > 0)
            {
                // Move random card from diminishing new deck to shuffled deck
                int I = random.Next(newCards.Count);
                shuffledCards.Add(newCards[I]);
                newCards.RemoveAt(I);
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("SHUFFLED DECK");
            foreach (Tuple<Suits, Ranks> card in shuffledCards)
                Console.Write(card + " ");

            return shuffledCards.Count;
        }

        // Deals iPairs card pairs from the deck. If iPair == 0 deals all cards (by default). Returns number of card dealt to each player.
        public static int DealDeck(int iPairs = 0)
        {
            playerOne.Clear();
            playerTwo.Clear();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("DEALING " + (iPairs == 0 ? "ALL" : (iPairs * 2).ToString()) + " CARDS");

            if (iPairs > 0)
                shuffledCards.RemoveRange(iPairs * 2 - 1, shuffledCards.Count - iPairs * 2);

            while (shuffledCards.Count > 0)
            {
                playerOne.Add(shuffledCards[0]);
                shuffledCards.RemoveAt(0);
                playerTwo.Add(shuffledCards[0]);
                shuffledCards.RemoveAt(0);
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("PLAYER 1: " + playerOne.Count);
            foreach (Tuple<Suits, Ranks> S in playerOne)
                Console.Write(S + " ");

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("PLAYER 2: " + playerTwo.Count);
            foreach (Tuple<Suits, Ranks> S in playerTwo)
                Console.Write(S + " ");

            return playerOne.Count;
        }

        // Plays game with pauses and console output if bPause == true. By default plays game to the end without output. 
        public static int PlayGame(bool bPause = false)
        {
            onTheTable.Clear();

            while (playerOne.Count > 0 && playerTwo.Count > 0)
            {
                if (bPause)
                {
                    Console.WriteLine();
                    Console.WriteLine("Press any key for the next move");
                    Console.ReadKey(true);
                }

                if (playerOne[0].Item2 > playerTwo[0].Item2)
                {
                    Console.Write("1: " + playerOne[0] + " > 2: " + playerTwo[0]);

                    playerOne.AddRange(onTheTable);
                    onTheTable.RemoveRange(0, onTheTable.Count);

                    playerOne.Add(playerOne[0]);
                    playerOne.Add(playerTwo[0]);
                    playerOne.RemoveAt(0);
                    playerTwo.RemoveAt(0);

                    Console.WriteLine(". Cards - Player 1: " + playerOne.Count + " Player 2: " + playerTwo.Count);
                }
                else if (playerOne[0].Item2 < playerTwo[0].Item2)
                {
                    Console.Write("1: " + playerOne[0] + " < 2: " + playerTwo[0]);

                    playerTwo.AddRange(onTheTable);
                    onTheTable.RemoveRange(0, onTheTable.Count);

                    playerTwo.Add(playerOne[0]);
                    playerTwo.Add(playerTwo[0]);
                    playerOne.RemoveAt(0);
                    playerTwo.RemoveAt(0);

                    Console.WriteLine(". Cards - Player 1: " + playerOne.Count + " Player 2: " + playerTwo.Count);
                }
                else
                {
                    Console.Write("1: " + playerOne[0] + " = 2: " + playerTwo[0]);

                    onTheTable.Add(playerOne[0]);
                    onTheTable.Add(playerTwo[0]);
                    playerOne.RemoveAt(0);
                    playerTwo.RemoveAt(0);

                    Console.WriteLine(". Cards - Player 1: " + playerOne.Count + " Player 2: " + playerTwo.Count + " Table: " + onTheTable.Count);

                    PlayGame(bPause);
                }
            }

            if (playerOne.Count == 0) return 2;

            return 1;
        }
    }
}
