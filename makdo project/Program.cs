
using System;

namespace makdo-project
{
    internal class Program
    {
        // ===================== SETTINGS =====================

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
        }

        static void ShowFoodMenu()
        {
            // TODO: display food items and prices
        }

        static void TakeOrder()
        {
            // TODO: let customers select food items
        }

        static void AddItem()
        {
            // TODO: iask ang customer kung ilang quantity needed for each item and add to order
        }

        static void CalculateTotal()
        {
            // TODO: calculate the total price
        }

        static void ConfirmOrder()
        {
            // TODO: ask customer to confirm or cancel
        }

        // ===================== PAYMENT =====================

        static void ProcessPayment()
        {
            // TODO: accept payment and calculate change
        }

        // ===================== RECEIPT =====================

        static void GenerateOrderID()
        {
            // TODO: craete a unique order id
        }

        static void PrintReceipt()
        {
            // TODO: display the receipt
        }

        // ===================== FILE HANDLING =====================

        static void SaveOrder()
        {
            // TODO: save mga paid orders into a text file
        }

        static void ReadOrders()
        {
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
            // TODO: search for an order using the order id
        }

        static void ShowSales()
        {
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
