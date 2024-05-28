using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Entities;
using Task = TaskManagement.Entities.Task;

namespace TaskManagement.Service
{
    public class TaskManagementSystem
    {
        private List<Project> projects = new List<Project>();
        private List<Team> teams = new List<Team>();

        public void Start()
        {
            // Populate
            InsertInitialTeams();
            InsertInitialProjects();

            while (true)
            {
                Console.WriteLine("Menu Options:");
                Console.WriteLine("1. View All Projects");
                Console.WriteLine("2. Create New Project");
                Console.WriteLine("3. View All Tasks in a Project");
                Console.WriteLine("4. Create New Task");
                Console.WriteLine("5. Assign Task to User");
                Console.WriteLine("6. Update Task Status");
                Console.WriteLine("7. Search for Tasks");
                Console.WriteLine("8. View Tasks by User");
                Console.WriteLine("9. Calculate story points per current sprint");
                Console.WriteLine("10. Save Project ");
                Console.WriteLine("10. Save Project Data to txt file");
                Console.WriteLine("11. Exit");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ViewAllProjects();
                        break;
                    case "2":
                        CreateNewProject();
                        break;
                    case "3":
                        ViewAllTasksInProject();
                        break;
                    case "4":
                        CreateNewTask();
                        break;
                    case "5":
                        AssignTaskToTeam();
                        break;
                    case "6":
                        UpdateTaskStatus();
                        break;
                    case "7":
                        SearchForTasks();
                        break;
                    case "8":
                        ViewTasksByTeam();
                        break;
                    case "9":
                        CalculateTeamStoryPoints();
                        break;
                    case "10":
                        SaveAllData();
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private void InsertInitialTeams()
        {
            teams.Add(new Team { Id = 1, Name = TeamName.Nexus, Email = "nexus@connatix.com", CreatedDate = DateTime.Now });
            teams.Add(new Team { Id = 2, Name = TeamName.Hybrid, Email = "hybr@connatix.com", CreatedDate = DateTime.Now });
            Console.WriteLine("Teams added.");
        }


        private void InsertInitialProjects()
        {
            Project project1 = new Project { Id = 1, Name = "Project Nexus", Description = "Reporting Project" };
            Project project2 = new Project { Id = 2, Name = "Project Hybrid", Description = "Library Project" };

            project1.AddTask(new Task { Id = 1, Name = "New dimensions reporting", Description = "Penskie required two new dimensions in order to be able to see how many content 1% complete...", Status = Status.Backlog, DueDate = DateTime.Now.AddDays(7), storyPoints = 5 });
            project1.AddTask(new Task { Id = 2, Name = "Multiple file upload", Description = "We want to be able to add multiple videos at the same time...", Status = Status.Backlog, DueDate = DateTime.Now.AddDays(21), storyPoints = 8 });

            projects.Add(project1);
            projects.Add(project2);

            Console.WriteLine("Project are assigned to the teams.");
        }

        private void ViewAllProjects()
        {
            foreach (var project in projects)
            {
                project.DisplayDetails();
            }
        }

        private void CreateNewProject()
        {
            Console.WriteLine("Enter Project Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Project Description:");
            string description = Console.ReadLine();

            Project project = new Project { Id = projects.Count + 1, Name = name, Description = description };
            projects.Add(project);
        }

        private void ViewAllTasksInProject()
        {
            Console.WriteLine("Enter Project ID:");
            int projectId = int.Parse(Console.ReadLine());

            var project = projects.FirstOrDefault(p => p.Id == projectId);
            if (project != null && project.Tasks.Count != 0)
            {
                foreach (var task in project.Tasks)
                {
                    task.DisplayDetails();
                }
            }
            else
            {
                Console.WriteLine("Project not found or it has any tasks asigned");
            }
        }

        private void CreateNewTask()
        {
            Console.WriteLine("Enter Project ID:");
            int projectId = int.Parse(Console.ReadLine());

            var project = projects.FirstOrDefault(p => p.Id == projectId);
            if (project != null)
            {
                Console.WriteLine("Enter Task Name:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Task Description:");
                string description = Console.ReadLine();
                Console.WriteLine("Enter Task Due Date (yyyy-mm-dd):");
                DateTime dueDate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Insert Story Points");
                int storyPoints = int.Parse(Console.ReadLine());

                int maxTaskId = project.Tasks.Any() ? project.Tasks.Max(t => t.Id) : 0;
                Task task = new Task { Id = maxTaskId + 1, Name = name, Description = description, DueDate = dueDate, Status = Status.Backlog, storyPoints = storyPoints };
                project.AddTask(task);
            }
            else
            {
                Console.WriteLine("Project not found.");
            }
        }

        private void AssignTaskToTeam()
        {
            try
            {
                Console.WriteLine("Enter Task ID:");
                int taskId = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Project ID:");
                int projectId = int.Parse(Console.ReadLine());

                var projectSelected = (from project in projects
                                       where project.Id == projectId
                                       select project).FirstOrDefault(); ;

                if (projectSelected != null)
                {
                    var taskSelected = (from t in projectSelected.Tasks
                                        where t.Id == taskId
                                        select t).FirstOrDefault();
                    if (taskSelected != null)
                    {
                        Console.WriteLine("Enter User ID:");
                        int teamId = int.Parse(Console.ReadLine());

                        var teamSelected = (from team in teams
                                            where team.Id == teamId
                                            select team).FirstOrDefault(); ;
                        if (teamSelected != null)
                        {
                            taskSelected.AssignTeam(teamSelected);
                            Console.WriteLine("Task assigned successfully.");
                        }
                        else
                        {
                            Console.WriteLine("User not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Task not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Project not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error assigning task: {ex.Message}");
            }
        }

        private void UpdateTaskStatus()
        {
            Console.WriteLine("Enter Task ID:");
            int taskId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Project ID:");
            int projectId = int.Parse(Console.ReadLine());

            var project = projects.FirstOrDefault(p => p.Id == projectId);
            if (project != null)
            {
                var task = project.Tasks.FirstOrDefault(t => t.Id == taskId);
                if (task != null)
                {
                    Console.WriteLine("Enter New Status:");
                    string statusString = Console.ReadLine();
                    if (Enum.TryParse(statusString, true, out Status status))
                    {
                        task.UpdateStatus(status);
                        Console.WriteLine("Status updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid status value.");
                    }
                }
                else
                {
                    Console.WriteLine("Task not found.");
                }
            }
            else
            {
                Console.WriteLine("Project not found.");
            }
        }

        private void SearchForTasks()
        {
            Console.WriteLine("Enter search term (title, description, or status):");
            string searchTerm = Console.ReadLine().ToLower();

            var results = from project in projects
                          from task in project.Tasks
                          where task.Name.ToLower().Contains(searchTerm) ||
                                task.Description.ToLower().Contains(searchTerm) ||
                                task.Status.ToString().ToLower().Contains(searchTerm)
                          select task;

            foreach (var task in results)
            {
                task.DisplayDetails();
            }
        }

        private void ViewTasksByTeam()
        {
            Console.WriteLine("Enter User ID:");
            int teamId = int.Parse(Console.ReadLine());

            var team = teams.FirstOrDefault(t => t.Id == teamId);
            if (team != null)
            {
                var tasks = from project in projects
                            from task in project.Tasks
                            where task.AssignedTeam != null && task.AssignedTeam.Id == teamId
                            select task;

                foreach (var task in tasks)
                {
                    task.DisplayDetails();
                }
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }

        private void CalculateTeamStoryPoints()
        {
            try
            {
                Console.WriteLine("Enter Team ID:");
                int teamId = int.Parse(Console.ReadLine());

                var team = teams.FirstOrDefault(t => t.Id == teamId);

                if (team != null)
                {
                    DateTime currentDate = DateTime.Now;
                    DateTime twoWeeksFromNow = currentDate.AddDays(14);

                    int totalStoryPoints = projects
                        .SelectMany(project => project.Tasks)
                        .Where(task => task.AssignedTeam != null &&
                                       task.AssignedTeam.Id == teamId &&
                                       task.DueDate >= currentDate &&
                                       task.DueDate <= twoWeeksFromNow)
                        .Sum(task => task.storyPoints);

                    Console.WriteLine($"Total story points for team {team.Name} in the next two weeks: {totalStoryPoints}");
                }
                else
                {
                    Console.WriteLine("Team not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating story points: {ex.Message}");
            }
        }


        private void SaveAllData()
        {
            try
            {
                List<ISaveable> saveables1 = new List<ISaveable>();

                foreach (var item in projects)
                {
                    saveables1.Add(item as ISaveable);
                }

                Utilities.SaveToFile(saveables1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }
    }
}
