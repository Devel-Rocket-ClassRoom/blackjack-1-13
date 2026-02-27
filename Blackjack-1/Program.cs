using System;
using System.ComponentModel;
using System.Security.Authentication;
using static System.Console;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// Black Jack

while (true)
{

    Console.WriteLine("=== 블랙잭 게임 ===");
    Console.WriteLine();
    Console.WriteLine("카드를 섞는 중...");
    Console.WriteLine();

    // 2차원 배열 카드 초기화 cards
    Card[,] cards = new Card[4, 13];   
    for (int i = 0; i < 4; i++)
    {
        for (int j = 0; j < 13; j++)
        {
            cards[i, j] = new Card(Card.Suit[i], Card.Number[j]);
        }
    }

 
    Player dealer = new Player("딜러");
    dealer.AddCard(Card.DrawCard(cards));
    dealer.AddCard(Card.DrawCard(cards));


    Player player = new Player("플레이어");
    player.AddCard(Card.DrawCard(cards));
    player.AddCard(Card.DrawCard(cards));
    GameManager GM = new GameManager(dealer, player);
    GM.Init();
    GM.GameStart(cards);
    GM.GameResult();
    WriteLine();

    while (true)
    {
        Write("새 게임을 하시겠습니까? (Y/N): ");
        string query = Console.ReadLine();

        if (string.Equals(query, "n", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("게임 종료");
            return;
        }
        else if (string.Equals(query, "y", StringComparison.OrdinalIgnoreCase))
        {
            break;
        }
        else
        {
            Console.WriteLine("Y/N을 입력하세요");
        }
    }
    Console.Clear();
}




//for (int i = 0; i < 4; i++)
//{
//    for (int j = 0; j < 13; j++)
//    {
//        writeline($"{cards[i, j].cardshape} {cards[i, j].cardnumber}");
//    }
//}

