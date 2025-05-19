using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeTracking.Models;

public class TimeTrackingDbContext : IdentityDbContext<IdentityUser>
{

    public TimeTrackingDbContext()
    {

    }
    public TimeTrackingDbContext(DbContextOptions<TimeTrackingDbContext> options) : base(options)
    {
        Database.EnsureCreated();
        SeedData();
        Database.Migrate();
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<TimeEntry> TimeEntries { get; set; }

    private void SeedData()
    {
        // Only seed data if the database is empty
        if (!Employees.Any() && !Projects.Any() && !TimeEntries.Any())
        {
            // Create employees
            var johnDoe = new Employee
            {
                Name = "John Doe",
                StartDate = new DateOnly(2023, 1, 15)
            };

            var janeDoe = new Employee
            {
                Name = "Jane Doe",
                StartDate = new DateOnly(2023, 3, 1)
            };

            var bobSmith = new Employee
            {
                Name = "Bob Smith",
                StartDate = new DateOnly(2024, 2, 10)
            };

            Employees.AddRange(johnDoe, janeDoe, bobSmith);
            SaveChanges();

            // Create projects
            var website = new Project
            {
                Name = "Website Redesign",
                StartDate = new DateTime(2023, 2, 1),
                EndDate = new DateTime(2023, 8, 30)
            };

            var mobileApp = new Project
            {
                Name = "Mobile App Development",
                StartDate = new DateTime(2023, 5, 15),
                EndDate = null // Ongoing project
            };

            var dataAnalytics = new Project
            {
                Name = "Data Analytics Platform",
                StartDate = new DateTime(2024, 1, 10),
                EndDate = new DateTime(2025, 12, 31)
            };

            Projects.AddRange(website, mobileApp, dataAnalytics);
            SaveChanges();

            // Add employees to projects
            website.Members.Add(johnDoe);
            website.Members.Add(janeDoe);

            mobileApp.Members.Add(janeDoe);
            mobileApp.Members.Add(bobSmith);

            dataAnalytics.Members.Add(johnDoe);
            dataAnalytics.Members.Add(bobSmith);

            SaveChanges();

            // Create time entries
            var timeEntries = new List<TimeEntry>
            {
                new TimeEntry
                {
                    EmployeeId = johnDoe.Id,
                    ProjectId = website.Id,
                    DateWorked = new DateOnly(2023, 3, 10),
                    HoursWorked = 8.0m
                },
                new TimeEntry
                {
                    EmployeeId = janeDoe.Id,
                    ProjectId = website.Id,
                    DateWorked = new DateOnly(2023, 3, 10),
                    HoursWorked = 6.5m
                },
                new TimeEntry
                {
                    EmployeeId = janeDoe.Id,
                    ProjectId = mobileApp.Id,
                    DateWorked = new DateOnly(2023, 6, 15),
                    HoursWorked = 7.0m
                },
                new TimeEntry
                {
                    EmployeeId = bobSmith.Id,
                    ProjectId = mobileApp.Id,
                    DateWorked = new DateOnly(2023, 6, 16),
                    HoursWorked = 8.0m
                },
                new TimeEntry
                {
                    EmployeeId = johnDoe.Id,
                    ProjectId = dataAnalytics.Id,
                    DateWorked = new DateOnly(2024, 2, 20),
                    HoursWorked = 5.5m
                }
            };

            TimeEntries.AddRange(timeEntries);
            SaveChanges();
        }
    }
}