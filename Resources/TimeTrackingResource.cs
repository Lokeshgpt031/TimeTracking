namespace TimeTracking.Resources;

public record Employee(int Id, string Name, DateOnly StartDate);
public record Project(int Id, string Name, DateTime StartDate, DateTime? EndDate);

public record TimeEntry(Guid Id, int EmployeeId, int ProjectId, DateOnly DateWorked, decimal HoursWorked);


public record ProjectAssignment (int EmployeeId, int ProjectId, string? EmployeeName, string? ProjectName);

public record Resource(string Name,string Url);

public record LinkedResource<T>
{
    public LinkedResource(T resource)
    {
        Data = resource;
        Links = new List<Resource>();
    }
    public T Data { get; set; }
    public List<Resource> Links { get; set; }
}