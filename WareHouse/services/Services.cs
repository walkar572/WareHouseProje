using Models;
using CsvPar;
using Data;
using SearchSystem.Models;
using Buy.goods;
// Service Layer
namespace Intro
{
    // Control menu enumeration
    enum ControlMenu
    {
        GetAllInfo = 1,
        GetAllÑategories = 2,
        GetSpecialÑategories = 3,
        CurrentBalance = 4,
        Exit = 5

    }
    // Exit application class
    static class ExitApp
    {
        public static bool Exit(ref bool exit)
        {
            return exit = false;
        }
    }
    // Help menu class
    static class HelpMenu
    {
        static bool exit = true;
        public static void GetAllInfo()
        {
            BuyClass buy = new BuyClass();
            IEnumerable<HouseholdGoods> household = CsvParhhouseholdgoods.GetAllGoods();
            foreach (var item in household)
            {
                Console.WriteLine(item.ToString());
            }
            buy.BuyItem();
        }
        // Main
        public static void Menu()
        {
            while (exit)
            {
                DataClass.Info();
                ÑheckType();
            }
        }
        // Choice of command method
        public static void ChoiceOfCommand()
        {
            BuyClass buy = new BuyClass();
            Console.Write("Enter application number:");
            int? userInput = int.Parse(Console.ReadLine());
            Console.Clear();
            switch ((ControlMenu)userInput)
            {
                case ControlMenu.Exit:
                    ExitApp.Exit(ref exit);
                    break;
                case ControlMenu.GetSpecialÑategories:
                    SearchHouseholdGoods.SearchService.SearchServ_by_selected_category();
                    break;
                case ControlMenu.GetAllInfo:
                    GetAllInfo();
                    break;
                case ControlMenu.GetAllÑategories:
                    SearchHouseholdGoods.SearchService.SearchServ_by_multiple_categories();
                    break;
                case ControlMenu.CurrentBalance:
                    buy.CurrentBalance();
                    break;
                default:
                    Console.WriteLine("Invalid input. Please enter a number corresponding to the menu options.");
                    Console.Clear();
                    break;
            }
        }
        public static void ÑheckType()
        {
            try
            {
                ChoiceOfCommand();
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input. Please enter a number corresponding to the menu options.");
                Console.Clear();
            }
        }
    }
}