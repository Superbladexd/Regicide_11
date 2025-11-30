public class Card
{
    public string suit { get; set; } //花色
    public string point { get; set; } //点数
    public int att { get; set; } //攻击
    public Card(string suit, string point,int att)
    {
        this.suit = suit;
        this.point = point;
        this.att = att;
    }

    public Card(Card card)
    {
        this.suit = card.suit;
        this.point = card.point;
        this.att = card.att;
    }
}

public class NormalCard : Card
{
    public NormalCard(string suit, string point,int att) : base(suit, point, att)
    {

    }

    public NormalCard(NormalCard card) : base(card)
    {

    }
}

public class BossCard : Card   //Boss
{
    public int health { get; set; }
    public int currentvalue { get; set; }
    public int currenthealth { get; set; }
    public BossCard(string suit, string point, int att) : base(suit,point,att)
    {
        this.health = this.att * 2;
        this.currenthealth = this.health;
        this.currentvalue = this.att;
    }

    public BossCard(BossCard card) : base(card)
    {
        this.health = card.health;
        this.currenthealth = card.currenthealth;
        this.currentvalue = card.currentvalue;
    }
}

public class JokerCard : Card
{
    public JokerCard(string suit, string point, int att) : base(suit, point, att)
    {

    }

    public JokerCard(JokerCard card) : base(card)
    {

    }
}