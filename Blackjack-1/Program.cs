using System;
using System.ComponentModel;
using System.Security.Authentication;
using static System.Console;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// Black Jack

while (true)
{
    // 2차원 배열 카드 초기화 cards
    Card[,] cards = new Card[4, 13];   
    for (int i = 0; i < 4; i++)
    {
        for (int j = 0; j < 13; j++)
        {
            cards[i, j] = new Card(Card.Suit[i], Card.Number[j]);
        }
    }
    Player player = new Player("플레이어");
    Player dealer = new Player("딜러");
    GameManager GM = new GameManager(dealer, player);


    dealer.AddCard(Card.DrawCard(cards));
    dealer.AddCard(Card.DrawCard(cards));
    player.AddCard(Card.DrawCard(cards));
    player.AddCard(Card.DrawCard(cards));
   
    GM.Init();  
    GM.GameStart(cards); 
    GM.GameResult();
    WriteLine();

   

    // 파산 엔딩 지정자
    if (Player.Chips == 0)  
    {
        while (true)
        {
            // 파산시 새 게임 시작 여부 질문
            Write("파산했습니다. 새 게임을 하시겠습니까? (Y/N): ");
            string query = Console.ReadLine();

            if (string.Equals(query, "n", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("게임 종료");
                Console.Write("아무키나 누르세요");
                Console.ReadKey();
                return;
            }
            else if (string.Equals(query, "y", StringComparison.OrdinalIgnoreCase))
            {
                Player.Chips = 1000;  // 계속 플레이 할 경우 칩 재분배
                break;
            }
            else
            {
                Console.WriteLine("Y/N을 입력하세요");
            }
        }
        Console.Clear();
       
    }

    else
    {
        // 게임 리스타트 지정자
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

   
}