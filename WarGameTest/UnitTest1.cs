using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WarGameUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod, Timeout(5000)]
        public void PlayFullDeck()
        {
            int Cards = WarGame.Program.CreateDeck();
            Assert.AreEqual(52, Cards, 0, "New deck is not full!");

            Cards = WarGame.Program.ShuffleDeck();
            Assert.AreEqual(52, Cards, 0, "Shuffled deck is not full!");

            Cards = WarGame.Program.DealDeck();
            Assert.AreEqual(26, Cards, 0, "Cards dealing error - all deck!");
        }

        [TestMethod, Timeout(50000)]
        public void PlayTenCards()
        {
            int Cards = WarGame.Program.CreateDeck();
            Assert.AreEqual(52, Cards, 0, "New deck is not full!");

            Cards = WarGame.Program.ShuffleDeck();
            Assert.AreEqual(52, Cards, 0, "Shuffled deck is not full!");

            Cards = WarGame.Program.DealDeck(5);
            Assert.AreEqual(5, Cards, 0, "Cards dealing error - 5 pairs!");

            int Winner = WarGame.Program.PlayGame(false);
            Assert.AreEqual(1, Winner, 1, "Wrong winner!");
        }
    }
}
