using System;
using System.Collections.Generic;
using System.Linq;

namespace Join
{
    class Person
    {
        public string Name, Email;

        public Person(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }

    class Record
    {
        public string Mail, SkypeId;

        public Record(string mail, string skypeId)
        {
            Mail = mail;
            SkypeId = skypeId;
        }
    }

    internal class Program
    {
        public static void PrintEnumerable<T>(IEnumerable<T> container)
        {
            foreach (var item in container)
            {
                Console.Write(item);
                Console.Write(" ");
            }

            Console.WriteLine();
        }
        public static void Main(string[] args)
        {
            var people = new Person[]
            {
                new Person("John", "john@foo.com"),
                new Person("Jane", "jane@foo.com"),
                new Person("Chris", string.Empty),
            };

            var records = new Record[]
            {
                new Record("john@foo.com", "John888"),
                new Record("jane@foo.com", "JaneAtHome"),
                new Record("jane@foo.com", "JaneAtFoo"),
            };

            var query = people.Join(records,
                x => x.Email,
                y => y.Mail,
                ((person, record) => new {Name = person.Name, SkypeId = record.SkypeId}));

            query.ToList().ForEach(item => Console.WriteLine($"{item.Name} {item.SkypeId}"));
            Console.WriteLine();

            var groupQuery = people.GroupJoin(records,
                x => x.Email,
                y => y.Mail,
                (
                    (person, recs) =>
                        new
                        {
                            Name = person.Name,
                            SkypeIds = recs.Select(r => r)
                        })
                );
            groupQuery.ToList().ForEach(item =>
            {
                Console.WriteLine($"{item.Name}");
                PrintEnumerable(item.SkypeIds);
            });
            Console.WriteLine();
        }
    }
}