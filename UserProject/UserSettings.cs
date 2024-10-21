using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProject
{
    public class UserSettings
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Theme { get; set; }
        public bool Notifications { get; set; }
    }
}
