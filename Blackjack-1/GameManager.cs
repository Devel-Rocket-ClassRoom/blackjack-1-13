using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

class GameManager
{
    private Player _player;
    private Player _dealer;
    private string _winner = null;
    private int _bet = 0;

    // 게임 매니저 생성자 플레이어 지정
    public GameManager(Player dealer, Player player)
    {
        _player = player;
        _dealer = dealer;
    }


    public void BettingCheck()    //  베팅금액 체크;
    {
        while (true)
        {
            Write("베팅 금액을 입력하세요: ");
            string input = Console.ReadLine();

            // 베팅금액 검사 조건문
            if (int.TryParse(input, out int number))  // out = 받은 변수 int number로 변환해서 출력
            {
                if (number < 0)  // 베팅금액 양수/음수 체크
                {
                    Console.WriteLine("유효한 금액을 베팅해주세요 (음수 감지됨)");
                    continue;  // 음수 continue로 현재 반복문 다시 실행
                }
                if (number == 0)
                {
                    Console.WriteLine("0은 베팅할 수 없습니다.");
                    continue;
                }
                if (Player.Chips >= number)  // 베팅 정상 작동 
                {
                    _bet = number;
                    break;
                }
                else  // 보유 칩보다 베팅금액이 많을 시
                {
                    WriteLine("보유한 칩보다 베팅금액이 많습니다.");
                }
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요");
            }
            
            
        }
    }
    



    public void Init()  // 초기 스테이지
    {
        Console.WriteLine("=== 블랙잭 게임 ===");
        WriteLine($"보유 칩: {Player.Chips}개");
        Console.WriteLine();
        BettingCheck();  // 베팅체크
        Console.WriteLine("카드를 섞는 중...");
        Console.WriteLine();
        WriteLine("=== 초기 패 ===");
        WriteLine($"딜러의 패: [??] [{_dealer.MyCard[1].CardShape}{_dealer.MyCard[1].CardNumber}]");
        WriteLine("딜러 점수: ?");
        WriteLine();

        Write("플레이어 패: ");
        _player.ShowCard();
        WriteLine($"플레이어 점수: {_player.ScoreCounter()}");
    }

    public void GameStart(Card[,] cards)  // 게임 시작 스테이지
    {
        while (true)
        {
            Write("H(Hit) 또는 S(Stand)를 선택하세요: ");
            string query = Console.ReadLine();
            if (string.Equals(query, "h", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(query, "s", StringComparison.OrdinalIgnoreCase))  // H or S 대소문자 구분 무
            {
                if (query == "h")  // 플레이어 힛 시
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


                    if (score > 21)  // 플레이어 버스트 ( 플레이어 힛 중 21 초과시 )
                    {
                        Console.WriteLine("버스트! 21을 초과했습니다.");
                        _winner = "딜러";
                        return;
                    }
                    // 요까지 (이슈 : 게임은 플레이서 승리는 표시하지만 게임이 안끝나고 힛스탠드 표시로 넘어감
                }
                else  // 플레이어 스탠드 시
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
                        drawCard = Card.DrawCard(cards);
                        Write($"딜러가 카드를 받았습니다: ");
                        _dealer.AddCard(drawCard);
                        WriteLine($"[{drawCard.CardShape}{drawCard.CardNumber}]");
                        Write("딜러의 패: ");
                        _dealer.ShowCard();
                        score = _dealer.ScoreCounter();
                        WriteLine($"딜러 점수: {score}");
                        WriteLine();
                        if (score >= 17 )  // 딜러는 카드의 합이 17이상이 될 때까지 드로우
                        {
                            if (score > 21)  // 딜러 버스트
                            {
                                Console.WriteLine("버스트! 21을 초과했습니다.");
                                _winner = "플레이어";
                                return;
                            }
                            return;
                        }
                       
                    }
                }
            }
            else  // 힛(H) 혹은 스탠드 (S)를 제외한 문자 입력시
            {
                Console.WriteLine("H 나 S를 입력해주세요");
                WriteLine();
            }
        }
    }


    public void GameResult()
    {
        int playerScore = _player.ScoreCounter();
        int dealerScore = _dealer.ScoreCounter();
        if ((playerScore > dealerScore) && _winner == null)
        {
            _winner = "플레이어";
        }
        else if ((playerScore < dealerScore) && _winner == null)
        {
            _winner = "딜러";
        }
        WriteLine("=== 게임 결과 ===");
        WriteLine($"플레이어: {playerScore}");
        WriteLine($"딜러: {dealerScore}");
        WriteLine();
        if (_winner == "플레이어")
        {
            Console.WriteLine($"플레이어 승리! (+{_bet}개)");
            Player.Chips += _bet;

        }
        else if(_winner == "딜러")
        {
            Console.WriteLine($"플레이어 패배! (-{_bet}개)");
            Player.Chips -= _bet;
        }
        else
        {
            Console.WriteLine("동점입니다");
        }
    }
}