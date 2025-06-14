using System.Collections.ObjectModel;

namespace TimeTracking.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public virtual ICollection<Employee> Members { get; set; }
    
    public Project()
    {
        Members = new Collection<Employee>();
    }
}