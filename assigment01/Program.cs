using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Provide the full path to the CSV file
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "random_data.csv");

        // Read data from CSV file
        var records = ReadCsvFile(filePath);

        // Calculate average age of all people
        double averageAge = CalculateAverageAge(records);
        Console.WriteLine($"Average Age of All People: {averageAge}");

        // Find the total number of people weighing between 120lbs and 140lbs
        int peopleInWeightRange = CountPeopleInWeightRange(records, 120, 140);
        Console.WriteLine($"Total Number of People Weighing Between 120lbs and 140lbs: {peopleInWeightRange}");

        // Find the average age of people weighing between 120lbs and 140lbs
        double averageAgeInWeightRange = CalculateAverageAgeInWeightRange(records, 120, 140);
        Console.WriteLine($"Average Age of People Weighing Between 120lbs and 140lbs: {averageAgeInWeightRange}");
        Console.ReadLine();
    }

    static List<Person> ReadCsvFile(string filePath)
    {
        try
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                return csv.GetRecords<Person>().ToList();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading CSV file: {ex.Message}");
            return new List<Person>();
        }
    }

    static double CalculateAverageAge(List<Person> people)
    {
        if (people.Count == 0)
            return 0;

        double totalAge = people.Sum(person => person.Age);
        return totalAge / people.Count;
    }

    static int CountPeopleInWeightRange(List<Person> people, double minWeight, double maxWeight)
    {
        return people.Count(person => person.Weight >= minWeight && person.Weight <= maxWeight);
    }

    static double CalculateAverageAgeInWeightRange(List<Person> people, double minWeight, double maxWeight)
    {
        var peopleInWeightRange = people.Where(person => person.Weight >= minWeight && person.Weight <= maxWeight).ToList();
        return CalculateAverageAge(peopleInWeightRange);
    }
}

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public double Weight { get; set; }
    public string Gender { get; set; }
}
