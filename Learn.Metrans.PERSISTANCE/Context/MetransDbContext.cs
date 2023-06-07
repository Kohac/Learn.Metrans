using Learn.Metrans.PERSISTANCE.Entities;
using Microsoft.EntityFrameworkCore;

namespace Learn.Metrans.PERSISTANCE.Context;

public class MetransDbContext : DbContext
{
    private Random _random = new Random();
    public DbSet<Employees> Employyes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employees>()
            .Property(p => p.Name)
            .HasMaxLength(20);
        modelBuilder.Entity<Employees>()
            .Property(p => p.Surname)
            .HasMaxLength(20);
        modelBuilder.Entity<Employees>()
            .HasKey(p => p.Id);
        //Create employyes while inicilization
        modelBuilder.Entity<Employees>()
            .HasData(CreateInitEmployees(100));
    }
    /// <summary>
    /// Create Employyes and insert them into in memory DB
    /// </summary>
    /// <param name="countOfEmployyes"></param>
    /// <returns>IEnumerable<Employyes> that are randomly generated with all theirs data</returns>
    private IEnumerable<Employees> CreateInitEmployees(int countOfEmployyes)
    {
        var employees = new List<Employees>();
        for (int i = 0; i < countOfEmployyes; i++)
        {
            var employedFrom = new DateOnly(_random.Next(1950, 2005), _random.Next(1, 12), _random.Next(1, 28));
            employees.Add(new Employees
            {
                Name = GenerateRandomStringName(),
                Surname = GenerateRandomStringName(),
                DateOfBirth = new DateOnly(_random.Next(1950, 2005), _random.Next(1, 12), _random.Next(1, 28)),
                EmployedFrom = employedFrom,
                EmployedTo = employedFrom.AddYears(_random.Next(1, 92)).AddMonths(_random.Next(1, 12)).AddDays(_random.Next(1, 28))
            });
        }
        return employees;
    }
    /// <summary>
    /// generate random name in string
    /// </summary>
    /// <returns></returns>
    private string GenerateRandomStringName()
    {
        string result = string.Empty;
        bool firstRun = true;
        string vowels = "aeiouy";
        string consonants = "bpdtďťgkhchfvzsžšcdzdžčřjlmnň";

        int randomNumber = _random.Next(1, 20);

        for (int i = 0; i < randomNumber; i++)
        {
            int chance = _random.Next(1, 5);
            if (!firstRun)
                result.Insert(
                    result.Length,
                    chance > 3 ? vowels[_random.Next(vowels.Length)].ToString() : consonants[_random.Next(consonants.Length)].ToString());
            else
                result.Insert(
                    result.Length,
                    chance > 3 ? vowels[_random.Next(vowels.Length)].ToString().ToUpper() : consonants[_random.Next(consonants.Length)].ToString().ToUpper());
        }

        return result;
    }
}
