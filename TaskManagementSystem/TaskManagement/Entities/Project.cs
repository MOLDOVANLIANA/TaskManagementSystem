using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Entities
{
    public class Project : Entity, ISaveable
    {
        public string Description { get; set; }
        public List<Task> Tasks { get; set; } = new List<Task>();

        public void AddTask(Task task)
        {
            task.ProjectId = Id;
            Tasks.Add(task);
        }

        public string ConvertToStringForSaving()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Id};{Name};{Description};");
            foreach (var task in Tasks)
            {
                sb.Append($"{task.ConvertToStringForSaving()}|");
            }
            return sb.ToString().TrimEnd('|');
        }

        public override void DisplayDetails()
        {
            Console.WriteLine($"Project ID: {Id}, Name: {Name}, Description: {Description}");
            foreach (var task in Tasks)
            {
                task.DisplayDetails();
            }
        }
    }
}
