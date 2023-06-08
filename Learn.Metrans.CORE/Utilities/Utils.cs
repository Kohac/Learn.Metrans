using Learn.Metrans.PERSISTANCE.Entities;
using System;

namespace Learn.Metrans.CORE.Utilities;

public static class Utils
{
    private static Random _random = new Random();
    /// <summary>
    /// Create Employyes and insert them into in memory DB
    /// </summary>
    /// <param name="countOfEmployyes"></param>
    /// <returns>IEnumerable<Employyes> that are randomly generated with all theirs data</returns>
    public static IEnumerable<Employees> CreateInitEmployees(int countOfEmployyes)
    {
        var employees = new List<Employees>();
        for (int i = 1; i < countOfEmployyes; i++)
        {
            var employedFrom = new DateTime(_random.Next(1950, 2005), _random.Next(1, 12), _random.Next(1, 28));
            employees.Add(new Employees
            {
                Id = i,
                Name = GenerateRandomStringName(),
                Surname = GenerateRandomStringName(),
                DateOfBirth = new DateTime(_random.Next(1950, 2005), _random.Next(1, 12), _random.Next(1, 28)),
                EmployedFrom = employedFrom,
                EmployedTo = employedFrom.AddYears(_random.Next(1, 92)).AddMonths(_random.Next(1, 12)).AddDays(_random.Next(1, 28))
            });
        }
        return employees;
    }
    /// <summary>
    /// generate random name in string
    /// </summary>
    /// <returns>Randomly generated string (Can be used for naming) in random length 1 - 20</returns>
    private static string GenerateRandomStringName()
    {
        string result = string.Empty;
        string vowels = "aeiouy";
        string consonants = "bpdtďťgkhchfvzsžšcdzdžčřjlmnň";

        int randomNumber = _random.Next(1, 20);

        for (int i = 0; i < randomNumber; i++)
        {
            int chance = _random.Next(1, 5);
            if (string.IsNullOrEmpty(result))
                result += chance > 3 ? vowels[_random.Next(vowels.Length)].ToString().ToUpper() : consonants[_random.Next(consonants.Length)].ToString().ToUpper();
            else
                result += chance > 3 ? vowels[_random.Next(vowels.Length)].ToString() : consonants[_random.Next(consonants.Length)].ToString();
        }

        return result;
    }
}
