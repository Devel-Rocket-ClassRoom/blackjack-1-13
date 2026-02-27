using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

class Card
{
    // 카드 넘버와 모양 초기화 // static field
    public static string[] Number = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    public static string[] Suit = { "♥", "♠", "♣", "♦" };


    public string CardShape { get; set; }  //  카드 모양 property
    public string CardNumber { get; set; }  //  카드 넘버 property

    public bool IsDrawn = false;  // 카드 중복 확인 bool 

    public Card (string shape, string number)  // 카드 생성자
    {
        CardShape = shape;
        CardNumber = number;
    }

    public static Card DrawCard(Card[,] cards)  //  카드 랜덤 드로우 함수;
    {
        Random rand = new Random();  
        while (true)
        {
            // 카드를 랜덤하게 뽑고, 뽑은 카드는 다시 안뽑게하는 코드
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
