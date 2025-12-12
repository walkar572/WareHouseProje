using Data;
using Microsoft.VisualBasic.FileIO;
using Models;

//Data Access Layer
namespace CsvPar
{
    public static class CsvParhhouseholdgoods
    {
        //In-memory storage for household goods
        static readonly List<HouseholdGoods> household_goods = new List<HouseholdGoods>();

        //Method to read data from CSV and populate the in-memory list
        public static void DispReadInfo()
        {

            using (TextFieldParser par = new TextFieldParser(DataClass.filePath))
            {
                par.SetDelimiters(",");
                while (!par.EndOfData)
                {
                    string[]? household_table = par.ReadFields();
                    if (household_table != null)
                    {
                        household_goods.Add(new HouseholdGoods(
                        household_table[0],
                        household_table[1],
                        household_table[2],
                        household_table[3],
                        household_table[4],
                        household_table[5]
                        ));
                    }
                }
            }

        }
        //Array with CSV data 
        public static List<HouseholdGoods> GetAllGoods()
        {
            return household_goods;
        }
    }
        
}