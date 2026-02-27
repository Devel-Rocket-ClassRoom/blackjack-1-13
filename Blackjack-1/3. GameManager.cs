using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

class GameManager
{
    private Player _player;
    private Player _dealer;
    private string winner = null;

    public GameManager(Player dealer, Player player)
    {
        _player = player;
        _dealer = dealer;
    }

    public void Init()
    {
        WriteLine("=== 초기 패 ===");
        WriteLine($"딜러의 패: [??] [{_dealer.MyCard[1].CardShape}{_dealer.MyCard[1].CardNumber}]");
        WriteLine("딜러 점수: ?");
        WriteLine();

        Write("플레이어 패: ");
        _player.ShowCard();
        WriteLine($"플레이어 점수: {_player.ScoreCounter()}");
    }

    public void GameStart(Card[,] cards)
    {
        while (true)
        {
            Write("H(Hit) 또는 S(Stand)를 선택하세요: ");
            string query = Console.ReadLine();
            if (string.Equals(query, "h", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(query, "s", StringComparison.OrdinalIgnoreCase))
            {
                if (query == "h")
                {
                    Card drawCard = Card.DrawCard(cards);
                    WriteLine();
                    Write($"플레이어가 카드를 받았습니다: ");
                    _player.AddCard(drawCard);
                    WriteLine($"[{drawCard.CardShape}{drawCard.CardNumber}]");

                    int score = _player.ScoreCounter();
                    Write("플레이어의 패: ");
                    _player.ShowCard();
                    WriteLine($"플레이어 점수: {score}");
                    WriteLine();

                    if (score > 21)
                    {
                        Console.WriteLine("버스트! 21을 초과했습니다.");
                        winner = "딜러";
                        return;
                    }
                }
                else
                {
                    Card drawCard;
                    int score = _dealer.ScoreCounter();

                    WriteLine("플레이어가 Stand를 선택했습니다.");
                    WriteLine();
                    WriteLine($"딜러의 숨겨진 카드: {_dealer.MyCard[0].CardShape}{_dealer.MyCard[0].CardNumber}");
                    Write("딜러의 패: ");
                    _dealer.ShowCard();
                    WriteLine($"딜러 점수: {score}");
                    WriteLine();

                    while (true)
                    {
                        if(score < 17)
                        {
                            drawCard = Card.DrawCard(cards);
                            Write($"딜러가 카드를 받았습니다: ");
                            _dealer.AddCard(drawCard);
                            WriteLine($"[{drawCard.CardShape}{drawCard.CardNumber}]");
                            Write("딜러의 패: ");
                            _dealer.ShowCard();
                            score = _dealer.ScoreCounter();
                            WriteLine($"딜러 점수: {score}");
                            WriteLine();
                        }
                        else
                        {
                            if(score > 21)
                            {
                                Console.WriteLine("버스트! 21을 초과했습니다.");
                                winner = "플레이어";
                                return;
                            }

                            return;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("H 나 S를 입력해주세요");
            }
        }
    }

    public void GameResult()
    {
        int playerScore = _player.ScoreCounter();
        int dealerScore = _dealer.ScoreCounter();
        if ((playerScore > dealerScore) && winner == null)
        {
            winner = "플레이어";
        }
        else if (playerScore < dealerScore)
        {
            winner = "딜러";
        }
        WriteLine("=== 게임 결과 ===");
        WriteLine($"플레이어: {playerScore}");
        WriteLine($"딜러: {dealerScore}");
        WriteLine();
        if (winner == "플레이어")
        {
            Console.WriteLine("플레이어 승리!");
        }
        else if(winner == "딜러")
        {
            Console.WriteLine("플레이어 패배!");
        }
        else
        {
            Console.WriteLine("동점입니다");
        }
    }
}