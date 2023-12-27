using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace AbcMobil.Models
{
    public class ShelfInfo:ObservableCollection<StockUI>
    {
        public int Id { get; set; }
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public int Amount { get; set; }
        public bool IsVisible { get; set; }
        public Color HeaderColor { get; set; }

        public ShelfInfo(string title1,string title2,int amount,int id,Color color)
        {
            Title1 = title1;
            Title2 = title2;
            Amount = amount;
            Id = id;
            HeaderColor = color;

        }
    }
}
