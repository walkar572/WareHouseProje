using CsvPar;
using Data;
using Intro;

//Startup Layer
namespace WareHouse
{
    public static class Program
    {
        static void Main(string[] args)
        {
            //Intro Section
            DataClass.Mydata();
            CsvParhhouseholdgoods.DispReadInfo();
            HelpMenu.Menu();
        }
    }
}
