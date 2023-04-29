using ProjectDATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDATN.Data.ViewModels
{
    public class NewVM
    {
        public NewVM()
        {
        }

        public NewVM(int id, string name, string content)
        {
            Id = id;
            Name = name;
            Content = content;
            
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        List<NewImages>? Images { get; set; }

    }
}
