using System;
using System.Globalization;
using System.IO;
/*===================================
            GROUP 3 MAKDO
=====================================
*/
namespace makdo_project
{
    internal class Program
    {
        // ===================== SETTINGS =====================

        // FOOD MENU - SANTOS

        static string[] foodmenu = ["Chicken Meal", "Burger", "Fries", "Soft Drink", "Sundae"];
        static decimal[] foodprices = [120.00m, 60.00m, 45.00m, 30.00m, 35.00m];
        static int[] foodqty = new int[foodmenu.Length];

        static string[] drinksmenu = ["Coca Cola", "McFlurry", "Sprite", "Iced Tea", "Lemonade"];
        static decimal[] drinksprices = [35.00m, 60.00m, 35.00m, 30.00m, 35.00m];
        static int[] drinksqty = new int[drinksmenu.Length];

        const string RestaurantName = "MAKDO";
        const string RecordsFolder = "records";
        const string OrdersFileName = "orders.txt";
        const int MaxQtyPerItem = 99;
        const int ScreenWidth = 50;

        static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

        // ---------- Shared order information (used by many methods) ----------
        static int selectedItem = 0;              // item number picked in TakeOrder (0 = none)
        static string reviewChoice = "";          // set by ConfirmOrder: "Y", "A" or "N"
        static bool paymentSuccessful = false;    // set by ProcessPayment
        static bool orderSaved = false;           // set by SaveOrder
        static decimal orderTotal = 0;            // total of the current order
        static decimal cashReceived = 0;          // set by ProcessPayment
        static decimal changeAmount = 0;          // set by ProcessPayment
        static string currentOrderId = "";        // set by GenerateOrderID
        static string currentOrderDate = "";      // set by GenerateOrderID

        // ===================== MAIN =====================

        static void Main()
        {
            bool running = true;

            while (running)
            {
                ShowMainMenu();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter choice: ");
                Console.ResetColor();

                string choice = (Console.ReadLine() ?? "").Trim();

                switch (choice)
                {
                    case "1":
                        CustomerView();
                        break;

                    case "2":
                        CashierView();
                        break;

                    case "3":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nThank you for using MAKDO!");
                        Console.ResetColor();

                        running = false;
                        break;

                    default:
                        ShowError("Invalid choice. Please enter 1, 2, or 3.");
                        PressAnyKey();   
                        break;
                }
            }
        }

        // ===================== MAIN MENU =====================

        static void ShowMainMenu()
        {
            Console.Clear();

            // MAKDO LOGO
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine(@" _____ ______   ________  ___  __    ________  ________");
            Console.WriteLine(@"|\   _ \  _   \|\   __  \|\  \|\  \ |\   ___ \|\   __  \");
            Console.WriteLine(@"\ \  \\\__\ \  \ \  \|\  \ \  \/  /|\ \  \_|\ \ \  \|\  \");
            Console.WriteLine(@" \ \  \\|__| \  \ \   __  \ \   ___  \ \  \ \\ \ \  \\\  \");
            Console.WriteLine(@"  \ \  \    \ \  \ \  \ \  \ \  \\ \  \ \  \_\\ \ \  \\\  \");
            Console.WriteLine(@"   \ \__\    \ \__\ \__\ \__\ \__\\ \__\ \_______\ \_______\");
            Console.WriteLine(@"    \|__|     \|__|\|__|\|__|\|__| \|__|\|_______|\|_______|");

            Console.ResetColor();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("          MAKDO SELF-SERVICE KIOSK");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("          Bigger. Tastier. Better.");
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("========================================");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("               MAIN MENU");
            Console.ResetColor();

            Console.WriteLine("========================================");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[1] Customer View");
            Console.WriteLine("[2] Cashier View");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[3] Exit");
            Console.ResetColor();

            Console.WriteLine("========================================");
        }

        // ===================== CUSTOMER VIEW =====================

        // CUSTOMER VIEW - VILLEGAS
        static void CustomerView()
        {
            ResetOrder();   // start a fresh, empty order

            // ---------- STEP 1: order items, then review (repeats if the customer adds more) ----------
            while (true)
            {
                bool readyForReview = false;

                while (!readyForReview)
                {
                    Console.Clear();

                    ShowFoodMenu();   // display the menu
                    TakeOrder();      // customer picks an item number
                    AddItem();        // ask quantity and add it to the cart

                    string next = AskNextStep();

                    if (next == "X")
                    {
                        CancelAndReturn("Order cancelled.");
                        return;
                    }

                    if (next == "N")
                    {
                        orderTotal = CalculateTotal();

                        if (orderTotal <= 0)
                        {
                            ShowError("Your order is empty. Please add at least one item.");
                            PressAnyKey();
                        }
                        else
                        {
                            readyForReview = true;
                        }
                    }
                    // if next == "Y", the loop repeats and the customer adds another item
                }

                Console.Clear();
                ShowOrderSummary();   // item, unit price, quantity, subtotal, total
                ConfirmOrder();       // sets reviewChoice: Y = confirm, A = add more, N = cancel

                if (reviewChoice == "N")
                {
                    CancelAndReturn("Order cancelled. Nothing was saved.");
                    return;
                }

                if (reviewChoice == "Y")
                {
                    break;   
                }
           
            }

            // ---------- STEP 2: payment ----------
            ProcessPayment();     

            if (!paymentSuccessful)
            {
                CancelAndReturn("Payment cancelled. Order was not saved.");
                return;
            }

            // ---------- STEP 3: order ID, save, receipt ----------
            GenerateOrderID();    
            SaveOrder();          

            if (!orderSaved)
            {
                CancelAndReturn("The order could not be saved, so it was not completed.");
                return;
            }

            PrintReceipt();       
            ClearCart();          

            Console.WriteLine();
            PressAnyKey("Press any key to return to the main menu...");
        }

        
        static string AskNextStep()
        {
            while (true)
            {
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("[Y] Add another item");
                Console.WriteLine("[N] Done ordering, review my order");
                Console.WriteLine("[X] Cancel order and go back");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter choice: ");
                Console.ResetColor();

                string answer = (Console.ReadLine() ?? "").Trim().ToUpper();

                if (answer == "Y" || answer == "N" || answer == "X")
                {
                    return answer;
                }

                ShowError("Invalid choice. Please enter Y, N, or X.");
            }
        }

       
        static void CancelAndReturn(string message)
        {
            ResetOrder();
            Console.WriteLine();
            ShowInfo(message);
            PressAnyKey("Press any key to return to the main menu...");
        }

        // ===================== FOOD MENU (SANTOS) =====================

        static void ShowFoodMenu()
        {
            // FOOD MENU - SANTOS
            ShowTitle("MAKDO MENU");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" ITEMS");
            Console.ResetColor();

            for (int i = 0; i < foodmenu.Length; i++)
            {
                PrintMenuLine(i + 1, foodmenu[i], foodprices[i]);
            }

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" MORE DRINKS");
            Console.ResetColor();

            for (int i = 0; i < drinksmenu.Length; i++)
            {
                PrintMenuLine(foodmenu.Length + i + 1, drinksmenu[i], drinksprices[i]);
            }

            Console.WriteLine(Bar());
            Console.WriteLine(" Cart: " + CartCount() + " item(s)");
            Console.WriteLine(Bar());
        }

        
        static void PrintMenuLine(int number, string name, decimal price)
        {
            string tag = "[" + number + "]";
            Console.WriteLine("  " + tag.PadRight(5) + name.PadRight(25) + Money(price).PadLeft(12));
        }

        // ===================== FOOD SELECTION (PACHECO) =====================

        static void TakeOrder()
        {
            // FOOD SELECTION - PACHECO
            selectedItem = 0;

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter item number (0 = none): ");
                Console.ResetColor();

                string input = (Console.ReadLine() ?? "").Trim();

                int number;
                if (!int.TryParse(input, out number) || number < 0 || number > TotalItems())
                {
                    ShowError("Invalid item. Please enter a number from 0 to " + TotalItems() + ".");
                    continue;
                }

                selectedItem = number;
                return;
            }
        }

        // ===================== QUANTITY (EDYRIE) =====================

        static void AddItem()
        {
            // QUANTITY SELECTION - EDYRIE
            if (selectedItem == 0)
            {
                return;   
            }

            Console.WriteLine("Selected: " + ItemName(selectedItem) + " - " + Money(ItemPrice(selectedItem)));

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter quantity (1 to " + MaxQtyPerItem + "): ");
                Console.ResetColor();

                string text = (Console.ReadLine() ?? "").Trim();

                
                int quantity;
                if (!int.TryParse(text, out quantity) || quantity < 1 || quantity > MaxQtyPerItem)
                {
                    ShowError("Quantity must be a whole number from 1 to " + MaxQtyPerItem + ".");
                    continue;
                }

                
                if (ItemQty(selectedItem) + quantity > MaxQtyPerItem)
                {
                    ShowError("Maximum is " + MaxQtyPerItem + " per item. You already have " + ItemQty(selectedItem) + ".");
                    continue;
                }

                AddQty(selectedItem, quantity);
                ShowSuccess("Added: " + quantity + " x " + ItemName(selectedItem));
                return;
            }
        }

        // ===================== ORDER SUMMARY (MHARA) =====================

        static void ShowOrderSummary()
        {
            // ORDER SUMMARY - MHARA
            ShowTitle("YOUR ORDER");

            PrintCartLines();

            Console.WriteLine(Dash());
            PrintTotalLine("TOTAL", Money(orderTotal));
            Console.WriteLine(Bar());
        }

        // ===================== TOTAL (VALMONTE) =====================

        
        static decimal CalculateTotal()
        {
            // TOTAL CALCULATION - VALMONTE
            decimal total = 0;

            for (int number = 1; number <= TotalItems(); number++)
            {
                total += ItemQty(number) * ItemPrice(number);
            }

            return total;
        }

        // ===================== CONFIRMATION (KIEL) =====================

        static void ConfirmOrder()
        {
            // ORDER CONFIRMATION - KIEL
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[Y] Confirm  [A] Add more items  [N] Cancel: ");
                Console.ResetColor();

                string answer = (Console.ReadLine() ?? "").Trim().ToUpper();

                if (answer == "Y" || answer == "A" || answer == "N")
                {
                    reviewChoice = answer;
                    return;
                }

                ShowError("Please type Y, A, or N.");
            }
        }

        // ===================== PAYMENT =====================

        static void ProcessPayment()
        {
            // PAYMENT AND CHANGE - ALYSSA
            // TODO: accept payment and calculate change
        }

        // ===================== RECEIPT =====================

        static void GenerateOrderID()
        {
            // RECEIPT AND ORDER ID - VAN
            // TODO: craete a unique order id
        }

        static void PrintReceipt()
        {
            // RECEIPT AND ORDER ID - VAN
            // TODO: display the receipt
        }

        // ===================== FILE HANDLING =====================

        
        static void SaveOrder()
        {
            // CASHIER VIEW AND FILE HANDLING - SHIENA
            // TODO: save mga paid orders into a text file
        }

        
        static string[] ReadOrders()
        {
            // CASHIER VIEW AND FILE HANDLING - SHIENA
            // TODO: read saved orders from the text file
            return new string[0];   // placeholder so the project compiles (this method must return string[])
        }

        static string GetOrdersPath()
        {
            return Path.Combine(RecordsFolder, OrdersFileName);
        }

        static string BuildRecord()
        {
            string items = "";

            for (int number = 1; number <= TotalItems(); number++)
            {
                if (ItemQty(number) > 0)
                {
                    if (items != "")
                    {
                        items += ";";
                    }

                    decimal subtotal = ItemQty(number) * ItemPrice(number);
                    items += ItemName(number) + "," + ItemPrice(number).ToString("F2", Inv) + "," +
                             ItemQty(number) + "," + subtotal.ToString("F2", Inv);
                }
            }

            return currentOrderId + "|PAID|" + items + "|" +
                   orderTotal.ToString("F2", Inv) + "|" +
                   cashReceived.ToString("F2", Inv) + "|" +
                   changeAmount.ToString("F2", Inv) + "|" +
                   currentOrderDate;
        }

        static bool IsPaidRecord(string[] fields)
        {
            return fields.Length >= 7 && fields[1] == "PAID";
        }

        // ===================== CASHIER VIEW =====================

        static void CashierView()
        {
            bool back = false;

            while (!back)
            {
                Console.Clear();

                ShowTitle("CASHIER VIEW");
                Console.WriteLine("[1] View Order by ID");
                Console.WriteLine("[2] View Sales");
                Console.WriteLine("[3] Back to Main Menu");
                Console.WriteLine(Bar());
                Console.Write("Enter choice: ");

                string choice = (Console.ReadLine() ?? "").Trim();

                switch (choice)
                {
                    case "1":
                        FindOrder();
                        break;

                    case "2":
                        ShowSales();
                        break;

                    case "3":
                        back = true;
                        break;

                    default:
                        ShowError("Invalid choice. Please enter 1, 2, or 3.");
                        PressAnyKey();
                        break;
                }
            }
        }

        static void FindOrder()
        {
            // CASHIER VIEW AND FILE HANDLING - SHIENA
            // TODO: search for an order using the order id
        }

        static void ShowSales()
        {
            // CASHIER VIEW AND FILE HANDLING - SHIENA
            // TODO: display completed orders and total sales
        }

        
        static void PrintSavedOrder(string[] f)
        {
            Console.WriteLine(" Restaurant : " + RestaurantName);
            Console.WriteLine(" Order ID   : " + f[0]);
            Console.WriteLine(" Status     : " + f[1]);
            Console.WriteLine(" Date       : " + f[6]);
            Console.WriteLine(Dash());
            PrintItemHeader();

            string[] parts = f[2].Split(';');

            for (int i = 0; i < parts.Length; i++)
            {
                string[] cols = parts[i].Split(',');   
                if (cols.Length != 4)
                {
                    continue;
                }

                decimal unitPrice;
                int qty;
                decimal subtotal;
                if (!decimal.TryParse(cols[1], NumberStyles.Number, Inv, out unitPrice) ||
                    !int.TryParse(cols[2], out qty) ||
                    !decimal.TryParse(cols[3], NumberStyles.Number, Inv, out subtotal))
                {
                    continue;
                }

                PrintItemRow(cols[0], unitPrice, qty, subtotal);
            }

            Console.WriteLine(Dash());
            PrintTotalLine("TOTAL", MoneyText(f[3]));
            PrintTotalLine("CASH", MoneyText(f[4]));
            PrintTotalLine("CHANGE", MoneyText(f[5]));
            Console.WriteLine(Bar());
        }

        // ===================== MENU HELPERS (work with item numbers 1..TotalItems) =====================

        static int TotalItems()
        {
            return foodmenu.Length + drinksmenu.Length;
        }

        static string ItemName(int number)
        {
            if (number <= foodmenu.Length)
            {
                return foodmenu[number - 1];
            }
            return drinksmenu[number - foodmenu.Length - 1];
        }

        static decimal ItemPrice(int number)
        {
            if (number <= foodmenu.Length)
            {
                return foodprices[number - 1];
            }
            return drinksprices[number - foodmenu.Length - 1];
        }

        static int ItemQty(int number)
        {
            if (number <= foodmenu.Length)
            {
                return foodqty[number - 1];
            }
            return drinksqty[number - foodmenu.Length - 1];
        }

        static void AddQty(int number, int amount)
        {
            if (number <= foodmenu.Length)
            {
                foodqty[number - 1] += amount;
            }
            else
            {
                drinksqty[number - foodmenu.Length - 1] += amount;
            }
        }

        static void ClearCart()
        {
            Array.Clear(foodqty);
            Array.Clear(drinksqty);
        }

        
        static void ResetOrder()
        {
            ClearCart();
            selectedItem = 0;
            reviewChoice = "";
            paymentSuccessful = false;
            orderSaved = false;
            orderTotal = 0;
            cashReceived = 0;
            changeAmount = 0;
        }

        static int CartCount()
        {
            int count = 0;

            for (int number = 1; number <= TotalItems(); number++)
            {
                count += ItemQty(number);
            }

            return count;
        }

        
        static void PrintCartLines()
        {
            PrintItemHeader();

            for (int number = 1; number <= TotalItems(); number++)
            {
                if (ItemQty(number) > 0)
                {
                    PrintItemRow(ItemName(number), ItemPrice(number), ItemQty(number), ItemQty(number) * ItemPrice(number));
                }
            }
        }

        static void PrintItemHeader()
        {
            Console.WriteLine(" " + "Item".PadRight(18) + "Price".PadLeft(12) + "Qty".PadLeft(5) + "Subtotal".PadLeft(13));
        }

        static void PrintItemRow(string name, decimal unitPrice, int qty, decimal subtotal)
        {
            Console.WriteLine(" " + name.PadRight(18) + Money(unitPrice).PadLeft(12) +
                              qty.ToString().PadLeft(5) + Money(subtotal).PadLeft(13));
        }

        static void PrintTotalLine(string label, string value)
        {
            Console.WriteLine(" " + label.PadRight(35) + value.PadLeft(13));
        }

        // ===================== HELPER METHODS aka error reporting =====================

        static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void ShowInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void ShowTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Bar());
            Console.WriteLine(Center(title, ScreenWidth));
            Console.WriteLine(Bar());
            Console.ResetColor();
        }

        static string Bar()
        {
            return new string('=', ScreenWidth);
        }

        static string Dash()
        {
            return new string('-', ScreenWidth);
        }

        static string Center(string text, int width)
        {
            if (text.Length >= width)
            {
                return text;
            }

            int spaces = (width - text.Length) / 2;
            return new string(' ', spaces) + text;
        }

        
        static string Money(decimal value)
        {
            return "PHP " + value.ToString("N2", Inv);
        }

        
        static string MoneyText(string text)
        {
            decimal value;
            if (decimal.TryParse(text, NumberStyles.Number, Inv, out value))
            {
                return Money(value);
            }
            return text;
        }

        static void PressAnyKey()
        {
            PressAnyKey("Press any key to continue...");
        }

        static void PressAnyKey(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.ReadKey(true);
        }
    }
}
