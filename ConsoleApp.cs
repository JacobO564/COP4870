using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    internal class HumanInterface
    {
        private Shop shop;
        public HumanInterface() 
        {
            shop = new Shop();
        }

        public void DisplayMain()
        {
            do
            {
                PageBreak();
                Console.WriteLine("Welcome To Jacob O'Neal's virtual, imaginary, and amazing shop\n\n\n");
                Console.WriteLine("Please select what mode to enter:");
                Console.WriteLine("[1] Inventory Managment");
                Console.WriteLine("[2] Shop");
                Console.WriteLine("[3] Exit");
                string? userInput = Console.ReadLine();
                // only accept input if values is int 1, 2, 3
                if (int.TryParse(userInput, out int number))
                {
                    if (number == 1)
                    {
                        // inventory managment
                        DisplayInventory();
                    }
                    if (number == 2)
                    {
                        //Shopping screen
                        DisplayShop();
                    }
                    if (number == 3)
                    {
                        return;
                    }
                }

            } while (true);
            
        }

        public void DisplayShop()
        {
            // bad things happen when ids change while shopping, right now ids can only change in the invenotry screen
            // so clearing cart on shop entry should be safe 
            shop.ClearCart();
            do
            {
                PageBreak();
                Console.WriteLine("Welcome to the Shop");
                Console.WriteLine("We are selling the following products");
                Console.WriteLine(shop.InventoryString());
                Console.WriteLine("\nChoose from the following options:");
                Console.WriteLine("[1] Cart View");
                Console.WriteLine("[2] Add item to cart");
                Console.WriteLine("[3] Check Out");
                Console.WriteLine("[4] Exit");
                string? userInput = Console.ReadLine();
                // only accept input if values is int 1-4
                if (int.TryParse(userInput, out int number))
                {
                    if (number == 1)
                    {
                        // view cart
                        ViewCartView();
                    }
                    if (number == 2)
                    {
                        // Add item to cart
                        AddCartDisplay();
                    }
                    if (number == 3)
                    {
                        PurchuseCart();
                    }
                    if (number == 4)
                    {
                        return;
                    }
                }
            } while (true);
               
        }

        public void PurchuseCart()
        {
            double totalCost = shop.CartCost() * 1.07;
            do
            {
                PageBreak();
                Console.WriteLine(shop.CartString());
                Console.WriteLine("Buy the above items following items?");
                Console.WriteLine($"Total Cost (Plus Tax): {totalCost:F2}");
                Console.WriteLine("[1] Yes");
                Console.WriteLine("[2] No");
                string? userInput = Console.ReadLine();
                if (int.TryParse(userInput,out int number))
                {
                    if (number == 1)
                    {
                        bool ret = shop.BuyCart();
                        if (ret)
                        {
                            Console.WriteLine("Purchase successful");
                        }
                        else
                        {
                            Console.WriteLine("Purchase unsuccessful, please try again");
                        }
                        Console.WriteLine("Press any key to continue");
                        Console.ReadLine();
                        break;
                    }
                    if(number == 2)
                    {
                        break;
                    }
                }
            } while (true);
        }

        public void AddCartDisplay()
        {
            Item? gotitem;
            do
            {
                PageBreak();
                Console.WriteLine(shop.InventoryString());
                Console.WriteLine("Select id of item you want to buy, enter q to quit");
                string? userInput = Console.ReadLine();
                if(userInput == "q")
                {
                    return;
                }
                if (int.TryParse(userInput, out int number))
                {
                    gotitem = shop.GetItemInventory(number);
                    if (gotitem != null)
                    {
                        AddItemToCart(gotitem);
                    }
                }

            } while (true);
        }
        public void ViewCartView()
        {
            do
            {
                PageBreak();
                Console.WriteLine(shop.CartString());
                Console.WriteLine("\nChoose action");
                Console.WriteLine("[1] edit");
                Console.WriteLine("[2] delete");
                Console.WriteLine("[3] buy");
                Console.WriteLine("[4] exit");
                string? userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int number))
                {
                    if (number == 1)
                    {
                        ChangeItemAmountCart();
                    }
                    if (number == 2)
                    {
                        RemoveItemFromCart();
                    }
                    if (number == 3)
                    {
                        PurchuseCart();
                    }
                    if (number == 4)
                    {
                        // exit
                        return;
                    }
                }
                    
            } while (true);
        }

        public void AddItemToCart(Item item)
        {
            Item? cartitem = shop.GetItemFromCart(item.id);

            do
            {
                PageBreak();
                Console.WriteLine("Add the following item to the cart?");
                if (cartitem  != null)
                {
                    Console.WriteLine($"You already have {cartitem.stock} in your cart");
                }
                Console.WriteLine(item.ToString());
                Console.WriteLine("[1] Yes");
                Console.WriteLine("[2] No");
                string? userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int number))
                {
                    if (number == 1)
                    {
                        break;
                    }
                    if (number == 2)
                    {
                        return;
                    }
                }
            } while (true);

            // ask how many to add
            int stock;
            do
            {
                PageBreak();
                if( cartitem == null)
                {
                    Console.WriteLine("How many do you want to buy, cant be greater then whats in stock?");
                }
                else
                {
                    Console.WriteLine($"How many more do you want to buy, cant be greater then whats in stock?, you already have {cartitem.stock} in yur cart.");
                }
                
                Console.WriteLine(item.ToString());
                string? userInput = Console.ReadLine();
                if (cartitem == null)
                {
                    if (int.TryParse(userInput, out int number))
                    {
                        if (number > 0 && number <= item.stock)
                        {
                            stock = number;
                            break;
                        }
                    }
                }
                else
                {
                    if (int.TryParse(userInput, out int number))
                    {
                        if (number + cartitem.stock > 0 && number + cartitem.stock <= item.stock)
                        {
                            stock = number;
                            break;
                        }
                    }
                }
                
            } while (true);

            shop.AddToCart(item.id, stock);
        }

        public void RemoveItemFromCart()
        {
            do
            {
                PageBreak();
                Console.WriteLine(shop.CartString());
                Console.WriteLine("\nSelect id of item to delete, enter q to quit");
                string? userInput = Console.ReadLine();
                if (userInput == "q")
                {
                    return;
                }
                // only accept input if values is int 1-4
                if (int.TryParse(userInput, out int number))
                {
                    shop.RemoveFromCart(number);
                }

            } while (true);
        }

        public void ChangeItemAmountCart()
        {
            Item? gotitem;
            do
            {
                PageBreak();
                Console.WriteLine(shop.CartString());
                Console.WriteLine("Select id of item you want to c,hange enter q to quit");
                string? userInput = Console.ReadLine();
                if (userInput == "q")
                {
                    return;
                }
                if (int.TryParse(userInput, out int number))
                {
                    gotitem = shop.GetItemFromCart(number);
                    if (gotitem != null)
                    {
                        AddItemToCart(gotitem);
                    }
                }

            } while (true);
        }
        public void NotAdded()
        {
            // temporary stub
            Console.Clear();
            Console.WriteLine("Not added");
            Console.ReadLine();
            return;
        }

        public void ChangeItemFromCart(Item item)
        {

        }

        public void DisplayInventory()
        {
            // MAIN/Inventory
            do
            {
                PageBreak();
                Console.WriteLine("Inventory Mangement Screen");
                Console.WriteLine("Current Inventory");
                Console.WriteLine(shop.InventoryString());
                Console.WriteLine("\n choose from the following items");
                Console.WriteLine("[1] Add");
                Console.WriteLine("[2] Update");
                Console.WriteLine("[3] Delete");
                Console.WriteLine("[4] Demo - set inventory to default values");
                Console.WriteLine("[5] Exit");
                string? userInput = Console.ReadLine();
                // only accept input if values is int 1-5
                if (int.TryParse(userInput, out int number))
                {
                    if (number == 1)
                    {
                        // add
                        AddToInventory();
                    }
                    if (number == 2)
                    {
                        // update
                        UpdateItemInventory();
                    }
                    if (number == 3)
                    {
                        // delete
                        RemoveItemInventory();
                    }
                    if (number == 4)
                    {
                        // adds some items to the inventory, for demo purposes
                        DemoDefaultInventory();
                    }
                    if (number == 5)
                    {
                        // exits
                        return;
                    }
                }
            } while (true);
        }

        public void AddToInventory()
        {
            //Main/Inventory/AddItem
            PageBreak();
            Console.WriteLine("Adding item");
            Console.WriteLine("Give Name");
            string name = Console.ReadLine() ?? "No Name";

            PageBreak();
            Console.WriteLine("Adding item");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine("Give Description");
            string description = Console.ReadLine() ?? "No Description";
            double price;
            do
            {
                PageBreak();
                Console.WriteLine("Adding item");
                Console.WriteLine($"Name: {name}"); ;
                Console.WriteLine($"Description: {description}");
                Console.WriteLine("Give Price");
                string? userInput = Console.ReadLine();
                // 
                if (double.TryParse(userInput, out double number))
                {
                    price = number;
                    break;
                }
            } while (true);
            int stock;

            do
            {
                PageBreak();
                Console.WriteLine("Adding item");
                Console.WriteLine($"Name: {name}");
                Console.WriteLine($"Description: {description}");
                Console.WriteLine($"Price: ${price}");
                Console.WriteLine("Give Stock");
                string? userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int number))
                {
                    if (number > 0)
                    {
                        stock = number;
                        break;
                    }
                }
            } while (true);
            Item item = new Item(name, description, price);
            item.stock = stock;

            do
            {
                PageBreak();
                Console.WriteLine($"Add item to inventory?\n{item.ToString()}");
                Console.WriteLine("[1] Yes");
                Console.WriteLine("[2] No");
                string? userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int number))
                {
                    if (number == 1)
                    {
                        //add item
                        shop.AddToInventory(item);
                        return;
                    }
                    if (number == 0)
                    {
                        return;
                    }
                }
            } while (true);
            


        }

        public void RemoveItemInventory()
        {
            do
            {
                PageBreak();
                Console.WriteLine(shop.InventoryString());
                Console.WriteLine("\nSelect id of item to delete, enter q to quit");
                string? userInput = Console.ReadLine();
                if (userInput == "q")
                {
                    return;
                }
                // only accept input if values is int 1-4
                if (int.TryParse(userInput, out int number))
                {
                    shop.RemoveItemInventroy(number);
                }

            } while (true);
        }

        public void UpdateItemInventory()
        {
            Item? gotitem;
            do
            {
                PageBreak();
                Console.WriteLine(shop.InventoryString());
                Console.WriteLine("\nSelect id of item to update, q to quit");
                string? userInput = Console.ReadLine();
                if (userInput == "q")
                {
                    return;
                }
                // only accept input if values is int 1-4
                if (int.TryParse(userInput, out int number))
                {
                    gotitem = shop.GetItemInventory(number);
                    if (gotitem != null)
                    {
                        UpdateItem(gotitem);
                    }
                }

            } while (true);
        }

        public void UpdateItem(Item item)
        {
            // show and ask if user wants to update selected item
            do
            {
                PageBreak();
                Console.WriteLine("Update this item?");
                Console.WriteLine(item.ToString());
                Console.WriteLine("\n[1] Yes");
                Console.WriteLine("[2] No");
                string? userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int number))
                {
                    if (number == 1)
                    {
                        break;
                    }
                    if (number == 2)
                    {
                        return;
                    }
                }
            } while (true);

            // go ahead with the update

            //get name
            PageBreak();
            Console.WriteLine("Updating item, press only enter to leave value the same");
            Console.WriteLine($"Name: {item.name}");
            string? name = Console.ReadLine() ?? string.Empty;
            if (name == string.Empty)
            {
                name = item.name;
            }

            // get description
            PageBreak();
            Console.WriteLine("Updating item, press only enter to leave value the same");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Description: {item.description}");
            string? description = Console.ReadLine() ?? string.Empty;
            if (description == string.Empty)
            {
                description = item.description;
            }

            // get price
            double price;
            do
            {
                PageBreak();
                Console.WriteLine("Updating item, press only enter to leave value the same");
                Console.WriteLine($"Name: {name}");
                Console.WriteLine($"Description: {description}");
                Console.WriteLine($"Price: ${item.price}");
                string? userInput = Console.ReadLine() ?? string.Empty;
                if (userInput == string.Empty)
                {
                    price = item.price;
                    break;
                }
                if (double.TryParse(userInput, out double number))
                {
                    if (number > 0)
                    {
                        price = number;
                        break;
                    }
                }
            } while (true);

            // get stock
            int stock;
            do
            {
                PageBreak();
                Console.WriteLine("Updating item, press only enter to leave value the same");
                Console.WriteLine($"Name: {name}");
                Console.WriteLine($"Description: {description}");
                Console.WriteLine($"Price: ${price}");
                Console.WriteLine($"Stock: {item.stock}");
                string? userInput = Console.ReadLine() ?? string.Empty;
                if (userInput == string.Empty)
                {
                    stock = item.stock;
                    break;
                }
                if (int.TryParse(userInput, out int number))
                {
                    if (number > 0)
                    {
                        stock = number;
                        break;
                    }
                }
            } while (true);

            Item updateitem = new Item(name, description, price);
            updateitem.stock = stock;
            updateitem.id = item.id;

            shop.UpdateItemInventory(updateitem);
        }

        public void DemoDefaultInventory()
        {
            //MAIN/Inventory/Demo
            // add some items to the inventory
            shop.ClearInventory();

            Item sock = new Item("Socks", "Pair of white ankle socks", 2.99);
            sock.stock = 10;
            shop.AddToInventory(sock);

            Item gloves = new Item("Gloves", "Pair of green gloves", 5.99);
            gloves.stock = 7;
            shop.AddToInventory(gloves);

            Item tv = new Item("tv", "32\" flatscreen tv", 199.99);
            tv.stock = 3;
            shop.AddToInventory(tv);
        }

        private void PageBreak()
        {
            Console.Clear();
        }
        
        public void Main()
        {   
        }


    }
}
