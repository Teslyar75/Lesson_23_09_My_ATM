using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_23_09
{

    class InsufficientFundsException : Exception
    {
        public InsufficientFundsException(string message) : base(message)
        {
        }
    }

    // Класс для банковского счета
    class BankAccount
    {
        private double balance;

        public BankAccount(double initialBalance)
        {
            if (initialBalance < 0)
            {
                throw new ArgumentException("Начальный баланс не может быть отрицательным.");
            }

            balance = initialBalance;
        }

        public double Balance
        {
            get { return balance; }
        }

        public void Deposit(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Нельзя внести отрицательную сумму.");
            }

            balance += amount;
        }

        public void Withdraw(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Нельзя снять отрицательную сумму.");
            }

            if (amount > balance)
            {
                throw new InsufficientFundsException("Недостаточно средств на счете.");
            }

            balance -= amount;
        }
    }

    // Класс банкомата
    class ATM
    {
        public void DepositMoney(BankAccount account, double amount)
        {
            try
            {
                account.Deposit(amount);
                Console.WriteLine($"Внесено {amount} гривен. Баланс: {account.Balance} гривен.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public void WithdrawMoney(BankAccount account, double amount)
        {
            try
            {
                account.Withdraw(amount);
                Console.WriteLine($"Снято {amount} гривен. Баланс: {account.Balance} гривен.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }



    class Program
    {
        static void Main()
        {
            try
            {
                // Создание банковского счета с начальным балансом
                BankAccount account = new BankAccount(1000.0);

                // Вывод текущего баланса
                Console.WriteLine($"Текущий баланс: {account.Balance} гривен.");

                // Использование банкомата
                ATM atm = new ATM();

                bool exit = false;

                while (!exit)
                {
                    Console.WriteLine("\nВыберите операцию:");
                    Console.WriteLine("1. Внести средства");
                    Console.WriteLine("2. Снять средства");
                    Console.WriteLine("3. Выйти");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.Write("Введите сумму для внесения: ");
                            if (double.TryParse(Console.ReadLine(), out double depositAmount))
                            {
                                atm.DepositMoney(account, depositAmount);
                            }
                            else
                            {
                                Console.WriteLine("Некорректная сумма.");
                            }
                            break;

                        case "2":
                            Console.Write("Введите сумму для снятия: ");
                            if (double.TryParse(Console.ReadLine(), out double withdrawAmount))
                            {
                                atm.WithdrawMoney(account, withdrawAmount);
                            }
                            else
                            {
                                Console.WriteLine("Некорректная сумма.");
                            }
                            break;

                        case "3":
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Неверная команда.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Необработанное исключение: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}
