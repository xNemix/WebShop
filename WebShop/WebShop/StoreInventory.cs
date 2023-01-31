namespace WebShop;

public class StoreInventory
{
    public List<GameItem> InventoryList { get; }
    
    public StoreInventory()
    {
        InventoryList = new List<GameItem>
        {
            new PUBG(),
            new PokemonLetsGoEevee(),
            new CyberPunk(),
            new BattleField()
        };
    }
    
    public void PrintInventory(string command)
    {           
        if (command == "1")
        {
            Print(InventoryList);
        }
        else if(command == "2")
        {
            Print(ListPhysicalItems());
        }
        else
        {
            Print(ListDownLoadable());
        }
    }
    
    private List<GameItem> ListPhysicalItems() 
    {
        return InventoryList.Where(gameItem => gameItem is IPhysicalCopy).ToList();
    }

    private List<GameItem> ListDownLoadable()
    {
        return InventoryList.Where(gameItem => gameItem is IDownloadableCopy).ToList();

    }
    
    private static void Print(List<GameItem> gameItems)
    {
        foreach (var item in gameItems)
        {
            item.PrintGameNameAndPrice();
        }
    }
}