using System.Collections.Generic;
using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    //Efficient underlying data structures!!!
    private Dictionary<string, Person> personsByEmail = new Dictionary<string, Person>();
    private Dictionary<string, SortedSet<Person>> personsByEmailDomain = new Dictionary<string, SortedSet<Person>>();
    private Dictionary<string, SortedSet<Person>> personsByNameAndTown = new Dictionary<string, SortedSet<Person>>();
    private OrderedDictionary<int, SortedSet<Person>> personsByAge = new OrderedDictionary<int, SortedSet<Person>>();
    private Dictionary<string, OrderedDictionary<int, SortedSet<Person>>> personsByTownAndAge = new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (this.FindPerson(email) != null) //Person alreaady exists
        {
            return false;
        }

        Person person = new Person
        {
            Email = email,
            Name = name,
            Age = age,
            Town = town
        };

        //Add by email
        this.personsByEmail.Add(email, person);

        //Add by email domain
        string domain = this.ExtractEmailDomain(email);
        this.personsByEmailDomain.AppendValueToKey(domain, person);

        //Add by name and town
        string nameAndTown = this.CombineNameAndTown(name, town);
        this.personsByNameAndTown.AppendValueToKey(nameAndTown, person);

        //Add by age
        this.personsByAge.AppendValueToKey(age, person);

        //Add by town + age
        this.personsByTownAndAge.EnsureKeyExists(town);
        this.personsByTownAndAge[town].AppendValueToKey(age, person);

        return true;
    }

    public int Count => this.personsByEmail.Count;

    public Person FindPerson(string email)
    {
        Person person;
        bool personExists = this.personsByEmail.TryGetValue(email, out person);
        return person;
    }

    public bool DeletePerson(string email)
    {
        Person person = this.FindPerson(email);
        if (person == null) //Person does not exist
        {
            return false;
        }

        //Delete from personsByEmail
        var personDeleted = this.personsByEmail.Remove(email);

        //Delete from personsByEmailDomain
        string domain = this.ExtractEmailDomain(email);
        this.personsByEmailDomain[domain].Remove(person);

        //Delete from personsByNameAndTown
        string nameAndTown = this.CombineNameAndTown(person.Name, person.Town);
        this.personsByNameAndTown[nameAndTown].Remove(person);

        //Delete from personsByAge
        this.personsByAge[person.Age].Remove(person);

        //Delete from personsByTownAndAge
        this.personsByTownAndAge[person.Town][person.Age].Remove(person);

        return true;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        return this.personsByEmailDomain.GetValuesForKey(emailDomain);
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        string nameAndTown = this.CombineNameAndTown(name, town);
        return this.personsByNameAndTown.GetValuesForKey(nameAndTown);
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        var personsInRange = this.personsByAge.Range(startAge, true, endAge, true);
        foreach (var personByAge in personsInRange)
        {
            foreach (Person person in personByAge.Value)
            {
                yield return person;
            }
        }
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
    {
        if (!this.personsByTownAndAge.ContainsKey(town))
        {
            yield break; //returns an empty sequence
        }

        var personsInRange = this.personsByTownAndAge[town].Range(startAge, true, endAge, true);
        foreach (var personByTownAndAge in personsInRange)
        {
            foreach (Person person in personByTownAndAge.Value)
            {
                yield return person;
            }
        }
    }

    private string ExtractEmailDomain(string email)
    {
        string domain = email.Split('@')[1];
        return domain;
    }

    private string CombineNameAndTown(string name, string town)
    {
        const string separator = "|!|";
        return name + separator + town;
    }
}
