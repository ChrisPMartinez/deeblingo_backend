using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class CardViewModel
    {
        public List<CardRow> Rows { get; set; }
    }
    public class CardRow
    {
        public List<Square> Squares { get; set; }
    }

    public class Square
    {
        public string Text { get; set; }
    }
}
