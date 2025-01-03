using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PrjBankATM
{
    internal class Bank
    {
        private const string FILE_PATH = "clients.txt";
        private const Int16 MIN_ACCNUM = 10000;
        private const Int16 MAX_ACCNUM = 10100;
        private const Int16 MAX_ACC = (MAX_ACCNUM - MIN_ACCNUM); //100 accounts
        struct Client
        {
            public Int16 accNum;
            public string fname;
            public string lname;
            public decimal balance;
        }
        static Int16 nbclient, choice, nAccNum;
        static Client[] tabclient = new Client[MAX_ACC];
        public static void run()
        {
            bool program = true;
            while (program)
            {
                readfromfile();
                Console.Clear();
                disptitle("Bank", "     Home Page");
                getChoice();
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        disptitle("Bank", "Add A New Account");
                        addAcc();
                        Console.WriteLine("Click any key to continue");
                        Console.ReadKey();
                        break;
                    case 2:
                        disptitle("Bank", "Remove An Account");
                        removeAcc();
                        Console.WriteLine("Click any key to continue");
                        Console.ReadKey();
                        break;
                    case 3:
                        disptitle("Bank", "Display Information");
                        displayOneAccount();
                        Console.WriteLine("Click any key to continue");
                        Console.ReadKey();
                        break;
                    case 4:
                        disptitle("Bank", "Account Deposit");
                        deposit();
                        Console.WriteLine("Click any key to continue");
                        Console.ReadKey();
                        break;
                    case 5:
                        disptitle("Bank", "Account Withdraw");
                        withdraw();
                        Console.WriteLine("Click any key to continue");
                        Console.ReadKey();
                        break;
                    case 6:
                        disptitle("Bank", "Sort clients");
                        sortClients();
                        break;
                    case 7:
                        disptitle("Bank", "Display Average");
                        displayAvg();
                        Console.WriteLine("Click any key to continue");
                        Console.ReadKey();
                        break;
                    case 8:
                        disptitle("Bank", "Display Total");
                        displayTotal();
                        Console.WriteLine("Click any key to continue");
                        Console.ReadKey();
                        break;
                    case 9:
                        goodbye();
                        program = false;
                        break;
                }
            }
        } 
        public static void displayAvg()
        {
            decimal totalavg = 0;
            if (nbclient == 0)
            {
                Console.WriteLine("There are no account in the system");
                return;
            }
            for (int i = 0; i < nbclient; i++)
            {
                totalavg = tabclient[i].balance + totalavg;
            }
            decimal avgBal = totalavg / nbclient;
            Console.WriteLine("The average balance of " + nbclient + " account is: " + avgBal);
        } 
        public static void displayTotal()
        {
            decimal total = 0;
            for (int i = 0; i < nbclient; i++)
            {
                total = tabclient[i].balance + total;
            }
            Console.WriteLine("The total balance of " + nbclient + " account is: " + total + "$");
        } 
        public static void sortClients()
        {
            Console.WriteLine(" 1 - By balance (Ascending)");
            Console.WriteLine(" 2 - By balance (Descending)");
            Console.WriteLine(" 3 - By family name (Ascending)");
            Console.WriteLine(" 4 - By family name (Descending)");
            Console.WriteLine(" 5 - By given name (Ascending)");
            Console.WriteLine(" 6 - By given name (Descending)");
            Console.WriteLine(" 7 - Quit sorting");
            Int16 SDchoice = Validate.validInt16("Enter your choice (1 - 7): ");
            Console.Clear();
            switch (SDchoice)
            {
                case 1:
                    Client[] sorted1 = tabclient.OrderBy(client => client.balance)
                           .ThenBy(client => client.lname)
                           .ThenBy(client => client.fname) 
                           .ToArray();
                    Console.WriteLine("Order by balance (Ascending)");
                    if (sorted1.Length == 0)
                    {
                        Console.WriteLine("No clients to display.");
                    }
                    else
                    {
                        for (int i = 0; i < sorted1.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(sorted1[i].fname))
                            {
                                Console.WriteLine("\nAccount Number: " + sorted1[i].accNum);
                                Console.WriteLine("First Name: " + sorted1[i].fname);
                                Console.WriteLine("Last Name: " + sorted1[i].lname);
                                Console.WriteLine("Balance: " + sorted1[i].balance + "$");
                            }
                        }
                    }
                    Console.WriteLine("\n\nClick any key to continue");
                    Console.ReadKey();
                    break;
                case 2:
                    Client[] sorted2 = tabclient.OrderByDescending(client => client.balance)
                           .ThenBy(client => client.lname)
                           .ThenBy(client => client.fname)
                           .ToArray();
                    Console.WriteLine("Order by balance (Descending)");
                    if (sorted2.Length == 0)
                    {
                        Console.WriteLine("No clients to display.");
                    }
                    else
                    {
                        for (int i = 0; i < sorted2.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(sorted2[i].fname))
                            {
                                Console.WriteLine("\nAccount Number: " + sorted2[i].accNum);
                                Console.WriteLine("First Name: " + sorted2[i].fname);
                                Console.WriteLine("Last Name: " + sorted2[i].lname);
                                Console.WriteLine("Balance: " + sorted2[i].balance + "$");
                            }
                        }
                    }
                    Console.WriteLine("\n\nClick any key to continue");
                    Console.ReadKey();
                    break;
                case 3:
                    Client[] sorted3 = tabclient.OrderBy(client => client.fname)
                           .ThenBy(client => client.lname)
                           .ThenBy(client => client.balance)
                           .ToArray();
                    Console.WriteLine("Order by Family name (Ascending)");

                    if (sorted3.Length == 0)
                    {
                        Console.WriteLine("No clients to display.");
                    }
                    else
                    {
                        for (int i = 0; i < sorted3.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(sorted3[i].fname))
                            {
                                Console.WriteLine("\nAccount Number: " + sorted3[i].accNum);
                                Console.WriteLine("First Name: " + sorted3[i].fname);
                                Console.WriteLine("Last Name: " + sorted3[i].lname);
                                Console.WriteLine("Balance: " + sorted3[i].balance + "$");
                            }
                        }
                    }
                    Console.WriteLine("\n\nClick any key to continue");
                    Console.ReadKey();
                    break;
                case 4:
                    Client[] sorted4 = tabclient.OrderByDescending(client => client.fname)
                           .ThenBy(client => client.lname)
                           .ThenBy(client => client.balance)
                           .ToArray();
                    Console.WriteLine("Order by Family name (Descending)");
                    if (sorted4.Length == 0)
                    {
                        Console.WriteLine("No clients to display.");
                    }
                    else
                    {
                        for (int i = 0; i < sorted4.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(sorted4[i].fname))
                            {
                                Console.WriteLine("\nAccount Number: " + sorted4[i].accNum);
                                Console.WriteLine("First Name: " + sorted4[i].fname);
                                Console.WriteLine("Last Name: " + sorted4[i].lname);
                                Console.WriteLine("Balance: " + sorted4[i].balance + "$");
                            }
                        }
                    }
                    Console.WriteLine("\n\nClick any key to continue");
                    Console.ReadKey();
                    break;
                case 5:
                    Client[] sorted5 = tabclient.OrderBy(client => client.lname)
                           .ThenBy(client => client.fname)
                           .ThenBy(client => client.balance)
                           .ToArray();
                    Console.WriteLine("Order by Given name (Ascending)");

                    if (sorted5.Length == 0)
                    {
                        Console.WriteLine("No clients to display.");
                    }
                    else
                    {
                        for (int i = 0; i < sorted5.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(sorted5[i].fname))
                            {
                                Console.WriteLine("\nAccount Number: " + sorted5[i].accNum);
                                Console.WriteLine("First Name: " + sorted5[i].fname);
                                Console.WriteLine("Last Name: " + sorted5[i].lname);
                                Console.WriteLine("Balance: " + sorted5[i].balance + "$");
                            }
                        }
                    }
                    Console.WriteLine("\n\nClick any key to continue");
                    Console.ReadKey();
                    break;
                case 6:
                    Client[] sorted6 = tabclient.OrderByDescending(client => client.lname)
                           .ThenBy(client => client.fname)
                           .ThenBy(client => client.balance)
                           .ToArray();
                    Console.WriteLine("Order by Given name (Descending)");
                    if (sorted6.Length == 0)
                    {
                        Console.WriteLine("No clients to display.");
                    }
                    else
                    {
                        for (int i = 0; i < sorted6.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(sorted6[i].fname))
                            {
                                Console.WriteLine("\nAccount Number: " + sorted6[i].accNum);
                                Console.WriteLine("First Name: " + sorted6[i].fname);
                                Console.WriteLine("Last Name: " + sorted6[i].lname);
                                Console.WriteLine("Balance: " + sorted6[i].balance + "$");
                            }
                        }
                    }
                    Console.WriteLine("\n\nClick any key to continue");
                    Console.ReadKey();
                    break;
                case 7:
                    return;
                default:
                    Console.WriteLine("Invalid input");
                    Console.WriteLine("Click any key to continue");
                    Console.ReadKey();
                    break;
            }
        }  
        public static void withdraw()
        {
            decimal withd;
            bool found = false, correctAmount = true;
            nAccNum = Validate.validInt16("Enter an account number you want to do a withdrawal: ");
            for (Int16 i = 0; i < nbclient; i++)
            {
                if (nAccNum == tabclient[i].accNum)
                {
                    while (correctAmount)
                    {
                        found = true;
                        Console.WriteLine("\nAccount currently has " + tabclient[i].balance + "$");
                        withd = Validate.validDec("\nEnter an amount you want to withdraw: ");
                        // if the condition is false, continue to test the input
                        if (withd < 0)
                        {
                            Console.WriteLine("Transaction failed, amount cannot be less than 0$");
                            Console.ReadKey();
                        }
                        else if (withd > tabclient[i].balance)
                        {
                            Console.WriteLine("Transaction failed, you do not have that amount of money to withdraw");
                            Console.ReadKey();
                        }
                        else //(withd >= 0 && withd <= tabclient[i].balance)
                        {
                            tabclient[i].balance = tabclient[i].balance - withd;
                            writetofile();
                            Console.WriteLine("\n___________________");
                            Console.WriteLine("Transaction succeed");
                            Console.WriteLine("Account now have " + tabclient[i].balance + "$");
                            return;
                        }
                    }
                }
            }
            if (found == false)
            {
                Console.WriteLine("\n___________________");
                Console.WriteLine("Transaction failed");
                Console.WriteLine("Cannot find your account.");
            }
        }
        public static void deposit()
        {
            decimal depos;
            bool found = false;
            nAccNum = Validate.validInt16("Enter an account number you want to do a deposit: ");
            for (Int16 i = 0; i < nbclient; i++)
            {
                if (nAccNum == tabclient[i].accNum)
                {
                    found = true;
                    Console.WriteLine("Account currently has " + tabclient[i].balance + "$");
                    depos = Validate.validDec("\nEnter an amount you want to deposit: ");
                    tabclient[i].balance = tabclient[i].balance + depos;
                    writetofile();
                    Console.WriteLine("\n___________________");
                    Console.WriteLine("Transaction succeed");
                    Console.WriteLine("Account now have " + tabclient[i].balance + "$");
                }
            }
            if (found == false)
            {
                Console.WriteLine("\n___________________");
                Console.WriteLine("Transaction failed");
                Console.WriteLine("Cannot find your account.");
            }
        }
        public static void displayOneAccount()
        {
            bool found = false;
            nAccNum = Validate.validInt16("Enter an account number to show information (" + MIN_ACCNUM + " - " + MAX_ACCNUM + "): ");
            for (Int16 i = 0; i < nbclient; i++)
            {
                if (nAccNum == tabclient[i].accNum)
                {
                    found = true;
                    Console.WriteLine("\nAccount Number: " + tabclient[i].accNum);
                    Console.WriteLine("First Name: " + tabclient[i].fname);
                    Console.WriteLine("Last Name: " + tabclient[i].lname);
                    Console.WriteLine("Balance: " + tabclient[i].balance + "$");
                }
            }
            if (found == false)
            {
                Console.WriteLine("Your account does not exist. Please try again.");
            }
        }
        public static void removeAcc()
        {
            Int16 num2d;
            char ans;
            bool found = false;
            num2d = Validate.validInt16("Enter an account number to delete: ");
            for (Int16 i = 0; i < nbclient; i++)
            {
                if (num2d == tabclient[i].accNum)
                {
                    found = true;
                    Console.WriteLine("The account " + tabclient[i].accNum + " belongs to " + tabclient[i].fname + " " + tabclient[i].lname);
                    Console.Write("Do you want to delete (Y/N): ");
                    if (!Char.TryParse(Console.ReadLine(), out ans))
                    {
                        Console.WriteLine("Invalid input. Please try again later.");
                        return;
                    }
                    else if (ans == 'Y' || ans == 'y')
                    {
                        for (Int16 j = i; j < (nbclient - 1); j++)
                        {
                            tabclient[j] = tabclient[j + 1];
                        }
                        nbclient--;
                        Console.WriteLine("\n___________________");
                        Console.WriteLine("Account deleted successfully");
                    }
                    else
                    {
                        Console.WriteLine("\n___________________");
                        Console.WriteLine("Removal failed.");
                        return;
                    }
                }
            }
            if (found == false)
            {
                Console.WriteLine("Your account does not exist. Please try again.");
            }
            writetofile();

        }  
        public static void addAcc()
        {
            Random rnd = new Random();
            Int16 tempInput;
            decimal bal;
            bool numUnique;
            if (nbclient < MAX_ACC)
            {
                do
                {
                    tempInput = Convert.ToInt16(rnd.Next(MIN_ACCNUM, MAX_ACCNUM));
                    numUnique = checkNewAccExist(tempInput);
                    //if account existed in the system the function checkNewAccExist will return true
                    //and when the value returns is true, the loop starts.
                } while (numUnique);
                tabclient[nbclient].accNum = tempInput;
                Console.WriteLine("Your account number is: " + tempInput);
                do
                {
                    Console.Write("Enter your first name: ");
                    tabclient[nbclient].fname = Console.ReadLine().Trim();
                } while (tabclient[nbclient].fname == "");
                do
                {
                    Console.Write("Enter your last name: ");
                    tabclient[nbclient].lname = Console.ReadLine().Trim();
                } while (tabclient[nbclient].lname == "");
                do
                {
                    bal = Validate.validDec("Enter your initial balance: ");
                    tabclient[nbclient].balance = bal;
                } while (tabclient[nbclient].balance < 0);
                nbclient++;
                Console.WriteLine("\n___________________");
                Console.WriteLine("\nNew client added successfully");
                writetofile();
            }
            else
            {
                Console.WriteLine("The list is full");
                Console.WriteLine("Cannot add anyone right now.");
            }
        } 
        public static Int16 getChoice()
        {
            Console.WriteLine(" 1 - Add a bank account");
            Console.WriteLine(" 2 - Remove a bank account");
            Console.WriteLine(" 3 - Display client's account");
            Console.WriteLine(" 4 - Deposit");
            Console.WriteLine(" 5 - Withdraw");
            Console.WriteLine(" 6 - Sort and display");
            Console.WriteLine(" 7 - Show average of all balances");
            Console.WriteLine(" 8 - Show total of all balances");
            Console.WriteLine(" 9 - Exit");
            do
            {
                choice = Validate.validInt16("Enter your choice (1 - 9): ");
            } while (choice < 1 || choice > 9);
            return choice;
        } 
        public static bool checkNewAccExist(Int16 input)
        {
            for (Int16 i = 0; i < nbclient; i++)
            {
                if (input == tabclient[i].accNum)
                {
                    return true;
                }
            }
            return false;
        }  
        public static void writetofile()
        {
            StreamWriter myfile2 = new StreamWriter(FILE_PATH);
            for (Int16 i = 0; i < nbclient; i++)
            {
                myfile2.WriteLine(tabclient[i].accNum);
                myfile2.WriteLine(tabclient[i].fname);
                myfile2.WriteLine(tabclient[i].lname);
                myfile2.WriteLine(tabclient[i].balance);
            }
            myfile2.Close();
        }         
        public static void readfromfile()
        {
            StreamReader myfile = new StreamReader(FILE_PATH);
            Int16 i = 0;
            while (myfile.EndOfStream == false)
            {
                tabclient[i].accNum = Convert.ToInt16(myfile.ReadLine());
                tabclient[i].fname = myfile.ReadLine();
                tabclient[i].lname = myfile.ReadLine();
                tabclient[i].balance = Convert.ToDecimal(myfile.ReadLine());
                i++;
            }
            nbclient = i;
            myfile.Close();
        }          
        public static void disptitle(string title, string subtitle)
        {
            Console.WriteLine("\t\t" + title.ToUpper());
            Console.WriteLine("\t" + subtitle);
            Console.WriteLine();
        }  
        public static void goodbye()
        {
            Console.WriteLine("\t _____           _ _            ");
            Console.WriteLine("\t|   __|___ ___ _| | |_ _ _ ___ ");
            Console.WriteLine("\t|  |  | . | . | . | . | | | -_|");
            Console.WriteLine("\t|_____|___|___|___|___|_  |___|");
            Console.WriteLine("\t                      |___|    ");
            writetofile();

        }      
    }
}
