using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    internal class Inventory
    {
        private List<Item>? items;

        public ReadOnlyCollection<Item>? Item
        {
            get
            {
                return items?.AsReadOnly();
            }
        }

        public int LastId
        {
            get
            {
                if (items?.Any() ?? false)
                {
                    return items?.Select(c => c.id)?.Max() ?? 0;
                }
                return 0;
            }
        }

        public Inventory()
        {
            items = new List<Item>();
        }

        public override string ToString()
        {
            if (items is null || items.Count == 0) return string.Empty;
            return string.Join("\n\n", items);
        }
        public Item Add(Item item)
        {
            if (item.id == 0){ 
                item.id = LastId + 1;
            }
            if (items == null)
            {
                items = new List<Item>();
            }
            items.Add(item);
            return item;
        }

        public Item? Update(Item item)
        {
            if (items == null)
            {
                return null;
            }
            int id = item.id;

            var itemToUpdate = items.FirstOrDefault(c => c.id == id);

            // to update the id must match, else it doesnt known which item to update
            if (itemToUpdate == null)
            {
                return null;
            }

            items.Remove(itemToUpdate);
            items.Add(item);
            return item;

        }

        public bool Delete(int id)
        {
            if (items == null)
            {
                return false;
            }

            var itemToDelete = items.FirstOrDefault(c => c.id == id);

            if (itemToDelete != null)
            {
                items.Remove(itemToDelete);
                return true;
            }
            // no matching id
            return false;

        }

        public Item? GetItem(int id)
        {
            if (items == null)
            {
                return null;
            }

            var itemToReturn = items.FirstOrDefault(c => c.id == id);

            return itemToReturn;

        }

        public Item? GetItemCopy(int id)
        {
            if (items == null)
            {
                return null;
            }

            var itemToReturn = items.FirstOrDefault(c => c.id == id);

            return itemToReturn;
        }

        public double TotalCost()
        {
            if (items == null)
            {
                return 0;
            }
            double totalCost = 0;

            // multiple each item in inventory with its price and amount in stock
            foreach ( var item in items)
            {
                totalCost += item.price * item.stock;
            }

            return totalCost;
        }

        public Inventory? SubtractInventories(Inventory inventory)
        {
            if (CanSubtractInventory(inventory))
            {
                if (inventory.items == null)
                {
                    return null;
                }

                foreach (var item in inventory.items)
                {
                    var matchingItem = items.FirstOrDefault(c => c.id == item.id);
                    if (matchingItem == null)
                    {
                        // no mathcing item with same id
                        return null;
                    }
                    if (matchingItem.stock < item.stock)
                    {
                        // tried to remove more items in stock
                        return null;
                    }

                    // subtract stock from inventory
                    matchingItem.stock -= item.stock;
                }

                return inventory;
            }
            return null;

        }

        public bool CanSubtractInventory(Inventory inventory)
        {
            if (inventory.items == null)
            {
                return false;
            }

            foreach( var item in inventory.items)
            {
                var matchingItem = items.FirstOrDefault(c => c.id == item.id);
                if (matchingItem == null)
                {
                    // no mathcing item with same id
                    return false;
                }
                if (matchingItem.stock < item.stock)
                {
                    // tried to remove more items in stock
                    return false;
                }
            }
            return true;
        }


    }
}
