using System.Text.Json;

namespace Test1 // Note: actual namespace depends on the project name.
{
    public class Menu
    {
        private readonly List<Transaction> _transactionList;

        public Menu()
        {
            _transactionList = new List<Transaction>();
        }

        public void StartMenu()
        {
            while (true)
            {
                Console.WriteLine($"Для создания транзакции введите команду {Enum.GetName(Command.add)}");
                Console.WriteLine($"Для получения транзакции по id введите команду {Enum.GetName(Command.get)}");
                Console.WriteLine($"Для выхода из программы введите команду {Enum.GetName(Command.exit)}");
                var command = Console.ReadLine();
                
                if (command == Enum.GetName(Command.add))
                {
                    AddTransaction();
                }
                else if (command == Enum.GetName(Command.get))
                {
                    GetTransaction();
                }
                else if (command == Enum.GetName(Command.exit))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Команды {command} не существует");
                }
            }
        }

        private void AddTransaction()
        {
            var transactionId = GetTransactionId(true);
            var transactionDate = GetTransactionDate();
            var transactionAmount = GetTransactionAmount();


            _transactionList.Add(new Transaction()
            {
                Id = transactionId,
                TransactionDate = transactionDate,
                Amount = transactionAmount
            });
            Console.WriteLine("[OK]");
        }

        private void GetTransaction()
        {
            var id = GetTransactionId(false);
            var transaction = _transactionList.SingleOrDefault(x => x.Id == id);
            
            if (transaction == null)
            {
                Console.WriteLine($"Транзакции с id {id} несуществует");
            }
            else
            {
                Console.WriteLine(JsonSerializer.Serialize(transaction));
            }
            Console.WriteLine("[OK]");
        }

        private int GetTransactionId(bool isCreate)
        {
            int transactionId;

            while (true)
            {
                Console.Write("Введите Id:");
                var inputId = Console.ReadLine();
                var isInt = int.TryParse(inputId, out transactionId);

                if (!isInt || transactionId <= 0) 
                {
                    Console.WriteLine("id транзакции должно быть числом больше 0");
                }                    
                else if (isCreate && _transactionList.SingleOrDefault(x => x.Id == transactionId) != null)
                {
                    Console.WriteLine("Транзакция с тким id уже существует");
                }
                else
                {
                    return transactionId;
                }                    
            }
        }

        private DateTime GetTransactionDate()
        {
            DateTime transactionDate;

            while (true)
            {
                Console.Write("Введите дату:");
                var inputDate = Console.ReadLine();
                var isDate = DateTime.TryParse(inputDate, out transactionDate);
                if (!isDate)
                    Console.WriteLine("Дата введена некоректно");
                else
                    return transactionDate;
            }
        }

        private decimal GetTransactionAmount()
        {
            decimal transactionAmount;

            while (true)
            {
                Console.Write("Введите сумму:");
                var inputAmount = Console.ReadLine();
                var isDouble = decimal.TryParse(inputAmount, out transactionAmount);

                if (!isDouble || transactionAmount <= 0)
                    Console.WriteLine("Сумма транзакции должно быть числом больше 0");
                else
                    return transactionAmount;
            }
        }
    }

    public enum Command
    {
        add,
        get,
        exit
    }
}