

List<Product> products = new List<Product>()
    {
        new Product()
        {
            Name = "Football",
            Price = 15.00M,
            Sold = false,
            StockDate = new DateTime(2023, 8, 1),
            ManufactureYear = 2010,
            Condition = 4.2
        },
        new Product()
        {
            Name = "Hockey Stick",
            Price = 12.15M,
            Sold = false,
            StockDate = new DateTime(2024, 10, 1),
            ManufactureYear = 2022,
            Condition = 3.8
        },
        new Product()
        {
            Name = "Tennis Racket",
            Price = 20.50M,
            Sold = true,
            StockDate = new DateTime(2024, 7, 1),
            ManufactureYear = 2019,
            Condition = 4.5
        }
    };

string greeting = @"Welcome to Thrown For a Loop
Your one-stop shop for used sporting equipment";

Console.WriteLine(greeting);
string choice = null;

while (choice != "0")
{
    Console.WriteLine(@"Choose an option:
                                0. Exit
                                1. View All Products
                                2. View Product Details
                                3. View Latest Additions");
    choice = Console.ReadLine();

    if (choice == "0")
    {
        Console.WriteLine("Goodbye!");
    }
    else if (choice == "1")
    {
        ListProducts();
    }
    else if (choice == "2")
    {
        ViewProductDetails();
    }
    else if (choice == "3")
    {
        ViewLatestProducts();
    }
}

void ListProducts()
{
    decimal totalValue = 0.0M;
    foreach (Product product in products)
    {
        if (!product.Sold)
        {
            totalValue += product.Price;
        }
    }

    Console.WriteLine($"Total inventory value: ${totalValue}");
    Console.WriteLine("Products:");
    for (int i = 0; i < products.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {products[i].Name}");
    }
}

void ViewProductDetails()
{
    ListProducts();

    Product chosenProduct = null;

    while (chosenProduct == null)
    {
        Console.WriteLine("Please enter a product number: ");
        try
        {
            int response = int.Parse(Console.ReadLine().Trim());
            chosenProduct = products[response - 1];
        }
        catch (FormatException)
        {
            Console.WriteLine("Please type only integers!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please choose an existing item only!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("Do Better!");
        }
    }

    DateTime now = DateTime.Now;
    TimeSpan timeInStock = now - chosenProduct.StockDate;
    Console.WriteLine(@$"You chose: 
{chosenProduct.Name}, which costs ${chosenProduct.Price}.
It is {now.Year - chosenProduct.ManufactureYear} years old. 
It {(chosenProduct.Sold ? "is not available." : $"has been in stock for {timeInStock.Days} days.")}");
}

void ViewLatestProducts()
{
    List<Product> latestProducts = new List<Product>();
    DateTime threeMonthsAgo = DateTime.Now - TimeSpan.FromDays(90);

    foreach (Product product in products)
    {
        if (product.StockDate > threeMonthsAgo && !product.Sold)
        {
            latestProducts.Add(product);
        }
    }
    for (int i = 0; i < latestProducts.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {latestProducts[i].Name}");
    }
}
