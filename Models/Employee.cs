using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace TimeTracking.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }

    public DateOnly StartDate { get; set; }

    // public DateOnly EndDate { get; set; }

    public virtual Collection<Project> Projects { get; set; }

    public Employee()
    {
        Projects = new Collection<Project>();
    }


}