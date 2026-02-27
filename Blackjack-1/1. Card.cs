using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

class Card
{
    
    public static string[] Number = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    public static string[] Suit = { "♥", "♠", "♣", "♦" };

    public string CardShape { get; set; }
    public string CardNumber { get; set; }

    public bool IsDrawn = false;

    public Card (string shape, string number)
    {
        CardShape = shape;
        CardNumber = number;
    }

    public static Card DrawCard(Card[,] cards)
    {
        Random rand = new Random();
        while (true)
        {
            int row = rand.Next(0, 4);
            int col = rand.Next(0, 13);
            if (cards[row, col].IsDrawn == false)
            {
                cards[row, col].IsDrawn = true;
                return cards[row, col];
            }
        }
    }
}
