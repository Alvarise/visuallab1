abstract class INotifyer
{
    public abstract void Notify(in decimal balance);
}

class Account
{
    decimal balance;
    List<INotifyer> notifyers;

    public Account()
    {
        balance = 0;
        notifyers = new List<INotifyer>();
    }

    public Account(in decimal balance)
    {
        this.balance = balance;
        notifyers = new List<INotifyer>();
    }

    public void AddNotifyer(in INotifyer notifyer)
    {
        notifyers.Add(notifyer);
    }

    public void ChangeBalance(in decimal balance)
    {
        this.balance = balance;
        Notification();
    }

    public decimal GetBalance()
    {
        return balance;
    }

    public void Notification()
    {
        foreach (INotifyer notifyer in notifyers)
        {
            notifyer.Notify(balance);
        }
    }
}

class SMSLowBalanceNotifyer : INotifyer
{
    string phone;
    decimal lowBalanceValue;

    public SMSLowBalanceNotifyer(string phone, decimal lowBalanceValue)
    {
        this.phone = phone;
        this.lowBalanceValue = lowBalanceValue;
    }

    public override void Notify(in decimal balance)
    {
        Console.WriteLine(phone);
        if (balance < lowBalanceValue)
        {
            Console.WriteLine(balance);
        }
    }
}

class EMailBalanceChangedNotifyer : INotifyer
{
    string email;

    public EMailBalanceChangedNotifyer(string email)
    {
        this.email = email;
    }

    public override void Notify(in decimal balance)
    {
        Console.WriteLine($"{email}\n{balance}");
    }
}

class Program
{
    static void Main()
    {
        Account acc = new(666);
        acc.AddNotifyer(new SMSLowBalanceNotifyer("88002000122", 132));
        acc.AddNotifyer(new EMailBalanceChangedNotifyer("zvere03@gmail.com"));
        acc.Notification();
        acc.ChangeBalance(777);
    }
}