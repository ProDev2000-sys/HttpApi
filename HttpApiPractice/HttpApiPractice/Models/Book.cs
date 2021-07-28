using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpApiPractice.Models
{
 
        public partial class book
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Author { get; set; }
            public string Isbn { get; set; }
            public int price { get; set; }
        }
    
}
