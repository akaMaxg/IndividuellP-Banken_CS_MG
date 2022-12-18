﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace IndiduelltP_Banken_CS_MG
{
    public class BankFunctions
    {
        public static bool RunMenu(User currentUser, bool toRun) //Main program
        {
            bool runProgram = true; //Runs program until exited by user
            while (runProgram)
            {
                // If the login is successful, present the user with a menu
                if (currentUser != null)
                {
                    Console.WriteLine("Please choose an option from the menu below: \n"); //Presents the menu
                    Console.WriteLine("     1. View Accounts and funds.");
                    Console.WriteLine("     2. Transfer funds between accounts.");
                    Console.WriteLine("     3. Withdraw funds.");
                    Console.WriteLine("     4. Log out");

                    // Read the user's menu selection
                    int menuOption = int.Parse(Console.ReadLine());

                    // Handle the user's menu selection
                    switch (menuOption)
                    {
                        case 1: //View accounts
                            Console.Clear();
                            ViewAccounts(currentUser);
                            MessagesInformations.enterToContinue();
                            Console.Clear();
                            break;
                        case 2: //Transfer funds
                            Console.Clear();
                            TransferFunds(currentUser);
                            MessagesInformations.enterToContinue();
                            Console.Clear();
                            break;
                        case 3: //Withdraw Funds
                            Console.Clear();
                            WithdrawFunds(currentUser);
                            MessagesInformations.enterToContinue();
                            Console.Clear();
                            break;
                        case 4://Logging out
                            string firstName = MessagesInformations.PresentableName(currentUser);
                            Console.WriteLine("Logging out, goodbye " + firstName);
                            //Console.WriteLine("Logging out: " + usernameFirst + currentUser.username.Substring(1) + "."); //Farewell message

                            runProgram = false; //sets bool to false and exists while loop
                            Console.Clear();

                            Console.WriteLine("Would you like to login with a different user or exit the program?");
                            Console.WriteLine("     1. Log in with another user.");
                            Console.WriteLine("     2. Exit the bank");
                            int exitOrLogIn = int.Parse(Console.ReadLine());
                            switch (exitOrLogIn)
                            {
                                case 1:
                                    Console.WriteLine("     1. Log in with another user.");
                                    Console.Clear();
                                    break;
                                case 2:
                                    Console.WriteLine("     2. Exit the bank.");
                                    toRun = false;
                                    break;
                            }
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid user, returning to start");
                    return toRun; //Exits loop and re-prompts login
                }
            }
            return toRun; //Exits loop and re-prompts login
        }
        static public void PopulateAccounts(User[] user) //Function that generates balances and adds it to an array
        {
            Random rnd = new Random();
            for (int i = 0; i < user.Length; i++)
            {
                //Console.WriteLine(user[i].username);
                for (int j = 0; j < user[i].balances.Length; j++)
                {
                    double randomBalance = rnd.NextDouble() * 100000;
                    user[i].balances[j] = Math.Round(randomBalance, 3);
                    //Console.Write(user[i].accountNames[j] + ": ");
                    //Console.WriteLine(user[i].balances[j]);
                }
            }
        }
        public static void ViewAccounts(User currentUser) //Function that presents accountnames
        {
            for (int i = 1; i <= currentUser.accountNames.Length; i++)
            {
                Console.WriteLine(i + ". " + currentUser.accountNames[i-1] + ": " + currentUser.balances[i-1]);
            }
        }
        public static int ChooseAccount(User currentUser) //Funtion that returns which account is being selected
        {
            int i = 0;

            while (i != 1 && i != 2 && i != 3)
            {
                Console.WriteLine("Enter 1, 2 or 3.");
                i = int.Parse(Console.ReadLine());
            }
            return i;
        }
        public static void ViewBalance(User currentUser) //Function that returns the balances for the users accounts
        {
            double y = 0;
            for (int k = 0; k < currentUser.balances.Length; k++)
            {
                Console.WriteLine(currentUser.balances[k]);
            }
        }
        public static void TransferFunds(User currentUser) //function that allows transfers between account
        {
            Console.WriteLine("Type the corresponding number to access the account: ");
            Console.WriteLine("Which account would you like to transfer from?");
            int fromAccount = BankFunctions.ChooseAccount(currentUser);
            Console.WriteLine("Which account would you like to transfer to? ");
            int toAccount = BankFunctions.ChooseAccount(currentUser);
            Console.Write("Enter amount: ");
            double amountTransfer = double.Parse(Console.ReadLine());
            if (amountTransfer > currentUser.balances[fromAccount - 1])
            {
                Console.WriteLine("Insufficient funds.");
            }
            else
            {

                currentUser.balances[fromAccount - 1] = currentUser.balances[fromAccount - 1] - amountTransfer;
                currentUser.balances[toAccount - 1] = currentUser.balances[toAccount - 1] + amountTransfer;
                Console.WriteLine("Transfered " + amountTransfer + " from " + currentUser.accountNames[fromAccount - 1] + " account to " + currentUser.accountNames[toAccount - 1] + " account.");
                BankFunctions.ViewAccounts(currentUser);
            }
        }
        public static void WithdrawFunds(User currentUser) //Function that allows withdrawals from account
        {
            Console.WriteLine("From which Account?");
            int fromAccount = BankFunctions.ChooseAccount(currentUser);
            Console.WriteLine("How much funds would you like to withdraw?");
            Console.Write("Enter amount: ");
            double amountWithdraw = double.Parse(Console.ReadLine());
            if (amountWithdraw > currentUser.balances[fromAccount - 1])
            {
                Console.WriteLine("Insufficient funds.");
            }
            else
            {

            Console.Write("Enter your pin again to confirm that you want to withdraw funds: ");
            int tempPin = int.Parse(Console.ReadLine());
            if (currentUser.pincode == tempPin) //Check to confirm that the correct users is attempting withdrawal
            {
                
                Console.WriteLine("Withdrawing " + amountWithdraw + " SEK " + "from account: " + currentUser.accountNames[fromAccount - 1]);
                currentUser.balances[fromAccount - 1] = currentUser.balances[fromAccount - 1] - amountWithdraw;
                Console.WriteLine("Balance remaining: " + currentUser.balances[fromAccount - 1] + " SEK");
            }
            else
            {
                Console.WriteLine("Incorrect pincode");
                Console.ReadLine();
            }

            }
        }
    }

}


