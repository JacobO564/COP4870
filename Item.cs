using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    internal class Item
    {
        public string? name
        {
            get;
            set; 
        }

        public string description { get;  set;}
        public double price { get;  set; }
        public int id { get;  set;  }
        public int stock
        {
            get;
            set;
        }


        public Item()
        {
            price = 0.00;
            id = 0;
            stock = 0;
            name = "no name";
            description = "no description";
        }

        public Item(string name ,string description, double price)
        {
            this.name = name;
            this.price = price;
            this.description = description;
            id = 0;
            stock = 0;
        }

        public Item(Item item)
        {
            name = item.name;
            price = item.price;
            description = item.description;
            id = item.id;
            stock = item.stock;
        }

        public void IncreaseStock(int increaseAmount)
        {
            stock += increaseAmount;
        }

        public override string ToString()
        {
            return $"[{id}] {name} ${price:F2} Amount in stock: {stock}\n\t- {description}";
        }

        public Item Copy()
        {
            return new Item(this);
        }
    }

}
