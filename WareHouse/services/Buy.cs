using System.Globalization;
using System.IO;
using CsvPar;
using Data;
//Functional Buy Layer
namespace Buy.goods
{
    public class BuyClass : DataClass
    {
        // File path to the balance file
        private static readonly string balanceFilePath = "Data\\balance.txt";
        private int balance;

        public BuyClass()
        {
            balance = LoadBalance();
        }

        private int LoadBalance()
        {
            if (File.Exists(balanceFilePath))
            {
                var text = File.ReadAllText(balanceFilePath);
                if (int.TryParse(text, out int savedBalance))
                    return savedBalance;
            }
            return 10000;
        }
        // Save the current balance to the file
        private void SaveBalance()
        {
            File.WriteAllText(balanceFilePath, balance.ToString());
        }

        // Purchase item method
        public void BuyItem()
        {
            List<string> BuyInfo = new List<string>()
            {
                "Buy a goods? (y/n):",
                "Enter the goods ID to purchase:",
                "Goods with this identifier not found!",
                "The goods is out of stock.",
                "Error in the price of the goods.",
                "Enter the quantity to purchase:",
                "Incorrect quantity.",
                "There are not enough goods in stock.",
                "Insufficient funds to purchase."
            };
            Console.Write(BuyInfo[0]);
            string choice = Console.ReadLine();
            NumberFormatInfo provider = new NumberFormatInfo()
            {
                NumberDecimalSeparator = "."
            };
            if (choice == "y")
            {
                Console.Write(BuyInfo[1]);
                string id = Console.ReadLine();
                Console.Clear();
                var goodsList = CsvParhhouseholdgoods.GetAllGoods();
                var item = goodsList.FirstOrDefault(g => g.Id == id);
                int quantity;
                double price;
                int buyCount;
                if (item == null)
                {
                    Console.WriteLine(BuyInfo[2]);
                    return;
                }

                if (!int.TryParse(item.Quantity, provider, out quantity) || quantity <= 0)
                {
                    Console.WriteLine(BuyInfo[3]);
                    return;
                }

                if (!double.TryParse(item.Price, provider, out price))
                {
                    Console.WriteLine(BuyInfo[4]);
                    return;
                }

                Console.WriteLine($"Goods: {item.Name}, Price: {price}, Quantity: {quantity}");
                Console.Write(BuyInfo[5]);

                if (!int.TryParse(Console.ReadLine(), out buyCount) || buyCount <= 0)
                {
                    Console.WriteLine(BuyInfo[6]);
                    return;
                }

                if (buyCount > quantity)
                {
                    Console.WriteLine(BuyInfo[7]);
                    return;
                }

                double totalCost = price * buyCount;
                if (totalCost > balance)
                {
                    Console.WriteLine(BuyInfo[8]);
                    return;
                }

                balance -= (int)totalCost;
                SaveBalance(); 
                quantity -= buyCount;
                item.Quantity = quantity.ToString();

                Console.WriteLine($"Purchase successful! Balance remaining: {balance}");
                Console.WriteLine($"There are items left in stock: {quantity}");
                Console.ReadKey();
            }
            else if (choice == null || choice != "y" && choice != "n")
            {
                Console.WriteLine("Invalid input !");
                BuyItem();
            }
            Console.Clear();
        }

        public void CurrentBalance()
        {
            Console.WriteLine($"Your current balance: {balance}");
            Console.ReadKey();
            Console.Clear();
        }
    }
}