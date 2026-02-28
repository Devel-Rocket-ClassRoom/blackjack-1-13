using System;
using System.Collections.Generic;
using System.Text;



class Player
{
    // 플레이어 (딜러 혹은 플레이어)의 손에 있는 카드 배열
    public Card[] MyCard = new Card[11];  

    public int currentCardNum = 0;  // 현재 카드의 수 set to 0
    public int Score { get; set; }  // 점수 프로퍼티
    public string Name { get; set; }  // 플레이어 이름 프로퍼티

    public static int Chips { get; set; } = 1000;  // 플레이어 칩 프로퍼티

    public Player (string name)  // 플레이서 생성자 - 이름과 점수 초기화
    {
        Name = name;
        Score = 0;
    }

    public void AddCard(Card card)  // MyCard 배열에 카드를 추가하는 메서드
    {
        MyCard[currentCardNum] = card; 
        currentCardNum++;
    }

    public void ShowCard()  // 카드 표시 메서드
    {
        for(int i = 0; i< currentCardNum; i++)
        {
            Console.Write($"[{MyCard[i].CardShape}{MyCard[i].CardNumber}] ");
        }
        Console.WriteLine();
    }

    public int ScoreCounter()  // 점수 
    {
        int numA = 0;
        int sum = 0;
        int number;  // 후에 out으로 할당
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
                    numA++;
                }
            }
        }

        if (numA > 0)
        {
            if (sum + 11 + numA - 1 <= 21)
            {
                sum += 11 + numA - 1;
            }
            else
            {
                sum += numA;
            }
        }
        

        return sum;
    }
}
