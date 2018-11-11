using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterableTreeView
{ 
    public class CategoryClass
    {
        public string Name { get; set; }
        public List<CategoryClass> SubCategories { get; set; }
    }
}
