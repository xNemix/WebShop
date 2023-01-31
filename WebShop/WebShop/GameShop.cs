namespace WebShop;

public class GameShop
{
    private bool ActiveSession { get; set; }
    private StoreInventory Inventory { get; }

    private List<GameItem> ShoppingCart { get; }

    public GameShop()
    {
        Inventory = new StoreInventory();
        ShoppingCart = new List<GameItem>();
        ActiveSession = true;
    }

    public void StartShop()
    {
        while (ActiveSession)
        {
            Console.WriteLine("Welcome to the shop! ");
            ShowShoppingText();

            HandleCommand();
        }
    }
    

    private void ContinueShopping()
    {
        ActiveSession = true;
        while (ActiveSession)
        {
            var shoppingCartText = ShoppingCart.Count > 1 ? $"{ShoppingCart.Count} items" : $"{ShoppingCart.Count} item";
            
            Console.WriteLine($"You have {shoppingCartText} in your shopping cart");
            ShowShoppingText();
            Console.WriteLine("4: Show current shopping cart");

            HandleCommand();
        }
    }
    
    private static void ShowShoppingText()
    {
        Console.WriteLine("1: Show all available games");
        Console.WriteLine("2: Show only physical games");
        Console.WriteLine("3: Show only downloadable games");
    }


    private void HandleCommand()
    {
        //Read the users answer and show the selected list of games
        var optionAnswer = Console.ReadLine()!;
        if (optionAnswer == "4")
        {
            ShowShoppingCart();
            if (!CheckOutOrContinueShopping()) return;
            GameShopCheckout();
            return;
        }
        Inventory.PrintInventory(optionAnswer);
        
        //Gets the users selected game to buy by ID and tries to find it in the Inventory
        Console.WriteLine("input the ID of the game you want to buy");
        var gameId = Console.ReadLine()!;
        var gameToBuy = Inventory.InventoryList.Find(x => x.Id == gameId);
        
        //Check if the game exists
        if (gameToBuy == null)
        {
            Console.WriteLine("Something went wrong...");
            Console.WriteLine($"Could not find the selected game with gameId: {gameId}, or game is out of stock");
            return;
        }
        
        //Confirmation check
        Console.WriteLine($"Are you sure you would like to add {gameToBuy.GameName} to the shopping cart? Y/N");
        var addToShoppingCartAnswer = Console.ReadLine()!;
        var addToShoppingCart = addToShoppingCartAnswer == "Y";
        if (!addToShoppingCart) return;
        
        
        //Add to shopping cart and notify customer
        ShoppingCart.Add(gameToBuy);
        Console.WriteLine($"{gameToBuy.GameName} has been added to the shopping cart..");

        //Checkout or continue shopping
        var checkOut = CheckOutOrContinueShopping();
        if (checkOut)
        {
            ShowShoppingCart();
            GameShopCheckout();
            return;
        }
        ContinueShopping();
    }

    private void GameShopCheckout()
    {
        Console.WriteLine("Checking out...");
        Thread.Sleep(1000);
        Console.WriteLine("Checking out..");
        Thread.Sleep(1000);
        Console.WriteLine("Checking out.");
        Thread.Sleep(1000);
        foreach (var item in ShoppingCart)
        {
            HandlePrintShopCheckout(item);
        }
    }

    private void ShowShoppingCart()
    {
        Console.Clear();
        Console.WriteLine("*** Shopping Cart ***");
        foreach (var item in ShoppingCart)
        {
            item.PrintGameNameAndPrice();
        }
        Thread.Sleep(4000);
    }

    private void HandlePrintShopCheckout(GameItem gameToBuy)
    {
        if (gameToBuy is IPhysicalCopy and not IDownloadableCopy) PrintShippingMessage(gameToBuy.GameName);
        else if (gameToBuy is IDownloadableCopy and not IPhysicalCopy) PrintDownloadMessage(gameToBuy.GameName);
        else ChooseDownloadOrShipping(gameToBuy.GameName);
        
        ActiveSession = false;
    }

    private static bool CheckOutOrContinueShopping()
    {
        Console.WriteLine("Do you want to checkout or continue shopping?");
        Console.WriteLine("1: Checkout");
        Console.WriteLine("2: Continue Shopping");
        var choice = Console.ReadLine();
        if (choice == "1") return true;
        Console.Clear();
        return false;
    }

    private static void ChooseDownloadOrShipping(string gameName)
    {
        Console.WriteLine($"How would you like to receive {gameName}?");
        Console.WriteLine("1: Download now");
        Console.WriteLine("2: Ship to address");
        var choice = Console.ReadLine();
        if (choice == "1") PrintDownloadMessage(gameName);
        else PrintShippingMessage(gameName);

    }
    
    private static void PrintDownloadMessage(string gameName)
    {
        Console.WriteLine($"Game {gameName} will now be downloaded..");            
    }
    
    private static void PrintShippingMessage(string gameName)
    {
        Console.WriteLine($"Game {gameName} will be shipped shortly..");
    }
}