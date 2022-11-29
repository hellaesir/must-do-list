using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MustDoList.Dto.Todo
{
    public class TodoDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }
    }
}
