using System;
using System.Collections.Generic;
using System.Text;

namespace JogoGourmet.Model
{
    public class Food
    {
        public string Name { set; get; }
        public string Feature { set; get; }


        public Food(string name, string feature)
        {
            Name = name;
            Feature = feature;
        }
    }
}
