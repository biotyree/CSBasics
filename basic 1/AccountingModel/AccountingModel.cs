using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccounting
{
    class AccountingModel : ModelBase
    {
        private double price { get; set; }
        private int nightsCount { get; set; }
        private double discount { get; set; }
        private double total { get; set; }


        public double Price
        {
            get => price;
            set
            {
                if (value < 0)
                    throw new ArgumentException();

                price = value;
                Notify(nameof(Price));

                updateTotal();
            }
        }

        public int NightsCount
        {
            get => nightsCount;
            set
            {
                if (value <= 0)
                    throw new ArgumentException();

                nightsCount = value;
                Notify(nameof(NightsCount));
                
                updateTotal();
            }
        }
        public double Discount
        {
            get => discount;
            set
            {
                if (value > 100)
                    throw new ArgumentException();

                discount = value;
                Notify(nameof(Discount));

                updateTotal();
            }
        }
        public double Total
        {
            get => total;
            set
            {
                if (value < 0)
                    throw new ArgumentException();

                total = value;
                Notify(nameof(Total));

                updateDiscount();
            }
        }

        private void updateTotal()
        {
            total =  Price * NightsCount * (1 - Discount / 100);
            Notify(nameof(Total));
        }

        private void updateDiscount()
        {
            discount = (-total / Price / NightsCount + 1) * 100;
            Notify(nameof(Discount));
        }
    }
}