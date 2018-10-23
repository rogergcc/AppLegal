using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace AppLegal.Models
{
    public class CustomMap: Map
    {
        public CustomCircle Circle { get; set; }
        public List<CustomPin> CustomPins { get; set; }
    }
}
