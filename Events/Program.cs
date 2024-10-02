namespace Events
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var stock = new Stock("Amazon");
            stock.Price = 100;
            stock.OnPriceChanged += Stock_OnPriceChanged; // Event subscribe
            stock.OnPriceChanged += (Stock s, decimal oldPrice) => // Event by lambada expression
            {
                string result = "";
                if (stock.Price > oldPrice)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    result = "up";
                }
                else if (stock.Price < oldPrice)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    result = "down";
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    result = "no change";
                }
                Console.WriteLine($"{stock.Name}: ${stock.Price} - {result}");
            };

            //Console.WriteLine($"Stock before changing: ${stock.Price}");
            //stock.ChangeStockPriceBy(0.05m);
            //Console.WriteLine($"Stock after changing: ${stock.Price}");

            stock.ChangeStockPriceBy(0.05m);
            stock.ChangeStockPriceBy(-0.02m);
            stock.ChangeStockPriceBy(0.00m);

            stock.OnPriceChanged -= Stock_OnPriceChanged; // Event unsubscribe
            stock.ChangeStockPriceBy(-0.07m);
            stock.ChangeStockPriceBy(-0.03m);


            Console.ReadKey();
        }

        private static void Stock_OnPriceChanged(Stock stock, decimal oldPrice)
        {
            string result = "";
            if(stock.Price > oldPrice)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                result = "up";
            }
            else if(stock.Price < oldPrice)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                result = "down";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                result = "no change";
            }
            Console.WriteLine($"{stock.Name}: ${stock.Price} - {result}");
        }
    }

    public delegate void StockPriceChangeHandler(Stock stock, decimal oldPrice);

    public class Stock
    {
        private string name;
        private decimal price;

        public event StockPriceChangeHandler OnPriceChanged;

        public string Name => this.name;

        public decimal Price { get => this.price; set => this.price = value; }

        public Stock(string stockName)
        {
            this.name = stockName;
        }

        public void ChangeStockPriceBy(decimal percent)
        {
            decimal oldPrice = this.Price;
            this.price += Math.Round(this.price * percent, 2);

            if(OnPriceChanged != null)
            {
                OnPriceChanged(this, oldPrice);
            }
        }
    } 

}
