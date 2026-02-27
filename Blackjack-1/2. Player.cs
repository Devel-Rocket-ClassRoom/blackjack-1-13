using System;
using System.Collections.Generic;
using System.Text;



class Player
{
    public Card[] MyCard = new Card[11];

    public int currentCardNum = 0;
    public int Score { get; set; }
    public string Name { get; set; }

    public Player (string name)
    {
        Name = name;
        Score = 0;
        
    }

    public void AddCard(Card card)
    {
        MyCard[currentCardNum] = card;
        currentCardNum++;
    }

    public void ShowCard()
    {
        for(int i = 0; i< currentCardNum; i++)
        {
            Console.Write($"[{MyCard[i].CardShape}{MyCard[i].CardNumber}] ");
        }
        Console.WriteLine();
    }

    public int ScoreCounter()
    {
        int sum = 0;
        int number;
        for (int i = 0; i < currentCardNum; i++)
        {
            bool success = int.TryParse(MyCard[i].CardNumber, out number);
            if (success)
            {
                sum += number;
            }
            else
            {
                if (MyCard[i].CardNumber == "K" || MyCard[i].CardNumber == "Q" || MyCard[i].CardNumber == "J")
                {
                    sum += 10;
                }
                else
                {
                    sum += 11;
                }
            }
        }
        return sum;
    }
}
