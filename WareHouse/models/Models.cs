using System;
using Interfaces;
namespace Models;
//Domain Layer
public class HouseholdGoods:IDisplay
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Material { get; set; }
    public string Price { get; set; }
    public string Category { get; set; }
    public string Quantity { get; set; } 
    public HouseholdGoods(string id, string name, string material, string price, string category, string quantity)
    {
        Id = id;
        Name = name;
        Material = material;
        Price = price;
        Category = category;
        Quantity = quantity;
    }
    public override string ToString()
    {
        return $"{Id} - {Name} - {Material} - ${Price} - {Category} - {Quantity}";
    }
}