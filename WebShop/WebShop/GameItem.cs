namespace WebShop;

public class GameItem
{
    public string Id { get; }

    public string GameName { get; }
    private int Price { get; }


    protected GameItem(string id, string gameName, int price)
    {
        Id = id;
        GameName = gameName;
        Price = price;
    }

    public void PrintGameNameAndPrice()
    {
        Console.WriteLine($"ID: {Id} | Item: {GameName} | Price: {Price}Kr");
    }
    
}