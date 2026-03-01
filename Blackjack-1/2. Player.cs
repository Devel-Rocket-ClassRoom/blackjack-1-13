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

    public Player (string name)  // 플레이어 생성자 - 이름과 점수 초기화
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

    public int ScoreCounter()  // 카드 점수 계산기
    {
        int numA = 0;  // Ace 갯수 누적 변수
        int sum = 0;  // 카드 점수 합계 변수
        int number;  // 후에 out으로 할당
        for (int i = 0; i < currentCardNum; i++)
        {
            // 카드 넘버 스트링을 int 형 으로 바꿀 수 있을시 success = true
            bool success = int.TryParse(MyCard[i].CardNumber, out number);  // out 여기서 할당
            if (success) 
            {
                sum += number; // 카드 넘버가 숫자일 시 
            }
            else
            {
                // 카드 넘버가 문자 (K,J,Q)
                if (MyCard[i].CardNumber == "K" || MyCard[i].CardNumber == "Q" || MyCard[i].CardNumber == "J")
                {
                    sum += 10;
                }
                else  // 카드가 Ace일시 
                {
                    numA++;
                }
            }
        }

        if (numA > 0)  // 플레이어가 Ace를 보유하고 있을시 Ace점수 1, 11 사용 조건문)
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
