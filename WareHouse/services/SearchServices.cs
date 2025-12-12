using CsvPar;
using Data;
using Interfaces;
using Models;
using System.Linq;
using System.Xml.Linq;
using Buy.goods;

namespace SearchSystem.Models;
public static class SearchHouseholdGoods
{
    public static class SearchService
    {
        enum Category
        {
            Id = 1,
            Name,
            Material,
            Price,
            Category,
            Quantity
        }
        public static List<HouseholdGoods> Search(
        string id = null,
        string name = null,
        string material = null,
        string category = null,
        string price = null,
        string quantity = null)
        {
            var allGoods = CsvParhhouseholdgoods.GetAllGoods();
            var results = new List<HouseholdGoods>();

            //Search by each criterion and add results to the list
            if (!string.IsNullOrEmpty(id))
                results.AddRange(allGoods.Where(g => g.Id == id));
            if (!string.IsNullOrEmpty(name))
                results.AddRange(allGoods.Where(g => g.Name.Contains(name, StringComparison.OrdinalIgnoreCase)));
            if (!string.IsNullOrEmpty(material))
                results.AddRange(allGoods.Where(g => g.Material.Contains(material, StringComparison.OrdinalIgnoreCase)));
            if (!string.IsNullOrEmpty(category))
                results.AddRange(allGoods.Where(g => g.Category.Contains(category, StringComparison.OrdinalIgnoreCase)));
            if (!string.IsNullOrEmpty(price))
                results.AddRange(allGoods.Where(g => g.Price.Contains(price, StringComparison.OrdinalIgnoreCase)));
            if (!string.IsNullOrEmpty(quantity))
                results.AddRange(allGoods.Where(g => g.Quantity.Contains(quantity, StringComparison.OrdinalIgnoreCase)));

            //If no criteria are specified, return all items
            if (string.IsNullOrEmpty(id) && string.IsNullOrEmpty(name) && string.IsNullOrEmpty(material) && string.IsNullOrEmpty(category))
                return allGoods.ToList();

            // Óäàëÿåì äóáëèêàòû ïî Id
            var uniqueResults = results.GroupBy(g => g.Id).Select(g => g.First()).ToList();

            return uniqueResults;
        }
        //Search by multiple categories works by searching for matches, that is, if you specify ID 1 and name plate, the product number by ID 1 and all products by name plate will be displayed.
        public static void SearchServ_by_multiple_categories()
        {
            Console.WriteLine("=Important: If you don't know the goods category, leave the field blank.=");
            BuyClass buy = new BuyClass();
            var foundItems = Search(id: MapCategory(1), name: MapCategory(2), material: MapCategory(3), category: MapCategory(5));
            if (foundItems.Count == 0)
            {
                Console.WriteLine("No goods found !!!");
                new DataClass().ReTry(SearchServ_by_multiple_categories);
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Goods found:");
                foreach (var item in foundItems)
                {
                    Console.WriteLine(item);
                }
                buy.BuyItem();
            }
        }
        public static string MapCategory(int category)
        {
            switch ((Category)category)
            {
                case Category.Id:
                    Console.Write("Enter id:");
                    return Console.ReadLine();
                case Category.Name:
                    Console.Write("Enter name:");
                    return Console.ReadLine();
                case Category.Material:
                    Console.Write("Enter material:");
                    return Console.ReadLine();
                case Category.Price:
                    Console.Write("Enter price:");
                    return Console.ReadLine();
                case Category.Category:
                    Console.Write("Enter category:");
                    return Console.ReadLine();
                case Category.Quantity:
                    Console.Write("Enter quantity:");
                    return Console.ReadLine();
                default:
                    return null;
            }
        }
        //Search by selected category works by searching for matches in a specific category.
        public static void SearchServ_by_selected_category()
        {
            Console.WriteLine("1:id 2:name 3:material 4:price 5:category 6:quantity");
            Console.Write("Enter category number: ");
            ÑheckTypeForSearch();

        }
        public static void ÑheckTypeForSearch()
        {
            try
            {
                ChoiceOfCategory();
            }
            catch (Exception)
            {
                Console.WriteLine("Error: invalid input");
                new DataClass().ReTry(ÑheckTypeForSearch);
                Console.Clear();
            }
        }
        public static void ChoiceOfCategory()
        {
            BuyClass buy = new BuyClass();
            int maincategory = Convert.ToInt32(Console.ReadLine());
            string value = MapCategory(maincategory);

            List<HouseholdGoods> foundItems = null;
            switch ((Category)maincategory)
            {
                case Category.Id:
                    foundItems = Search(id: value);
                    break;
                case Category.Name:
                    foundItems = Search(name: value);
                    break;
                case Category.Material:
                    foundItems = Search(material: value);
                    break;
                case Category.Price:
                    foundItems = Search(price: value);
                    break;
                case Category.Category:
                    foundItems = Search(category: value);
                    break;
                case Category.Quantity:
                    foundItems = Search(quantity: value);
                    break;
                default:
                    Console.WriteLine("Invalid category number.");
                    new DataClass().ReTry(SearchServ_by_selected_category);
                    Console.Clear();
                    return;
            }

            if (foundItems == null || foundItems.Count == 0)
            {
                Console.WriteLine("No products found !!!");
                new DataClass().ReTry(SearchServ_by_selected_category);
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Goods found:");
                foreach (var item in foundItems)
                {
                    Console.WriteLine(item);
                }
                buy.BuyItem();
            }

        }
    }
}