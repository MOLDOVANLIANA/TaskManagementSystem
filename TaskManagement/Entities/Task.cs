using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Entities
{
    public class Task : Entity, ISaveable
    {
        public string Description { get; set; }
        public Status Status { get; set; }
        public int ProjectId { get; set; }
        public Team AssignedTeam { get; set; }
        public DateTime DueDate { get; set; }
        public int storyPoints { get; set; }

        public void AssignTeam(Team team)
        {
            AssignedTeam = team;
        }

        public void UpdateStatus(Status status)
        {
            Status = status;
        }

        public override void DisplayDetails()
        {
            Console.WriteLine($"Task ID: {Id}, Name: {Name}, Description: {Description}, Status: {Status}, Due Date: {DueDate}, Story points: {storyPoints}");
            if (AssignedTeam != null)
            {
                Console.WriteLine($"Assigned to: {AssignedTeam.Name}");
            }
        }

        public string ConvertToStringForSaving()
        {
            return $"{Id};{Name};{Description};{Status};{storyPoints};{AssignedTeam?.Id};{DueDate};";
        }
    }
}
