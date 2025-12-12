//Data Access Layer
namespace Data
{
    public class DataClass
    {
        // File path to the CSV file containing household goods and tableware data
        public static readonly string filePath = "Data\\household-goods.csv";

        // Method to display personal data and application information
        public static void Mydata()
        {
            IEnumerable<string> myself = new List<string>() {
            "My full name: Serhii Shevchenko Tarasovich",
            "Tableware and Household Goods Warehouse: 1.2.0",
            "",
            "Press any button to enter the Menu.",
            };
            foreach (var item in myself)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
            Console.Clear();
        }

        // Method to display the main menu options
        public static void Info()
        {
            List<string> showMenu = new List<string>()
            {
                "1. Get all goods",
                "2. Search for goods across multiple categories",
                "3. Search for goods in the selected category",
                "4. Current balance",
                "5. Exit",
                "-----------------------------------------------"
            };
            foreach (var item in showMenu)
            {
                Console.WriteLine(item);
            }
        }

        // Method to prompt the user to retry an action
        public void ReTry(Action searchAction)
        {
            Console.Write("Try again? (y/n): ");
            var input = Console.ReadLine()?.Trim().ToLower();

            if (input == "y")
            {
                Console.Clear();
                searchAction();
            }
            else if (input != "n")
            {
                Console.WriteLine("Invalid input!");
                Console.ReadKey();
            }
        }
    }
}