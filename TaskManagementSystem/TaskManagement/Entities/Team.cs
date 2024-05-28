using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Entities
{
    public class Team : Entity
    {
        public TeamName Name { get; set; }
        public string Email { get; set; }

        public override void DisplayDetails()
        {
            Console.WriteLine($"Team ID: {Id}, Name: {Name}, Email: {Email}, CreatedDate: {CreatedDate}");
        }
    }
}