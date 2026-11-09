using System;

namespace makdo_project
{
    internal class Program
    {
        // ===================== SETTINGS =====================

        // FOOD MENU - SANTOS
        // TODO: mag add ng food names, prices, and quantities using arrays

        const string RecordsFolder = "records";
        const string OrdersFileName = "orders.txt";

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

                string choice = Console.ReadLine() ?? "";

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

        static void CustomerView()
        {
            // TODO: mag add ng customer ordering process
            // CUSTOMER VIEW - UNASSIGNED
        }

        static void ShowFoodMenu()
        {
            // FOOD MENU - SANTOS
            // TODO: display food items and prices
        }

        static void TakeOrder()
        {
            // FOOD SELECTION - PACHECO
            // TODO: let customers select food items
        }

        static void AddItem()
        {
            // QUANTITY SELECTION - EDYRIE
            // TODO: iask ang customer kung ilang quantity needed for each item and add to order
        }

        static void ShowOrderSummary()
        {
            // ORDER SUMMARY - MHARA
            // TODO: display selected food items, quantities, and prices
        }

        static void CalculateTotal()
        {
            // TOTAL CALCULATION - VALMONTE
            // TODO: calculate the total price
        }

        static void ConfirmOrder()
        {
            // ORDER CONFIRMATION - KIEL
            // TODO: ask customer to confirm or cancel
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

        static void ReadOrders()
        {
            // CASHIER VIEW AND FILE HANDLING - SHIENA
            // TODO: read saved orders from the text file
        }

        // ===================== CASHIER VIEW =====================

        static void CashierView()
        {
            Console.Clear();

            Console.WriteLine("\n========== CASHIER VIEW ==========");
            Console.WriteLine("[1] View Order by ID");
            Console.WriteLine("[2] View Sales");
            Console.WriteLine("[3] Back to Main Menu");
            Console.WriteLine("==================================");
            Console.Write("Enter choice: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    FindOrder();
                    break;

                case "2":
                    ShowSales();
                    break;

                case "3":
                    return;

                default:
                    ShowError("Invalid choice. Please try again.");
                    break;
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

        // ===================== HELPER METHODS aka error reporting =====================

        static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
