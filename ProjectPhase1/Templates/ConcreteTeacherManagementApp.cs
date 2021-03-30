using ProjectPhase1.Builders;
using ProjectPhase1.Repositories;
using ProjectPhase1.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPhase1.Templates
{
    public class ConcreteTeacherManagementApp : AbstractTeacherAppTemplate
    {
        protected override void loadTeachers()
        {
            var teachersRepository = new PipeDelimitedFileTeachersRepository("teachers.txt");
            var teachersAsList = teachersRepository.Load();
            _teachers = teachersAsList.ToDictionary(t => t.ID);
            Console.WriteLine($"Loaded {_teachers.Count} teachers");
        }

        protected override void saveTeachers(IEnumerable<Teacher> teachers)
        {            
            var teachersRepository = new PipeDelimitedFileTeachersRepository("teachers.txt");
            teachersRepository.Save(teachers);            
            Console.WriteLine($"Saved {_teachers.Count} teachers successfully");
        }

        protected override int getOption()
        {            
            var input = Console.ReadLine();
            return int.Parse(input);             
        }

        protected override void addTeacher()
        {
            var teacherBuilder = new ConcreteTeacherBuilder();
            var teacher = teacherBuilder.Build();
            _teachers[teacher.ID] = teacher;
        }

        protected override void deleteTeacher()
        {
            Console.WriteLine("Enter ID of techer to delete");
            var id = int.Parse(Console.ReadLine());
            
            if (!_teachers. ContainsKey(id))
            {
                Console.WriteLine($"Teacher with id {id} not found");
            }
            else
            {
                _teachers.Remove(id);
                Console.WriteLine("Removed teacher");
            }
        }

        protected override void findTeacher()
        {
            Console.WriteLine("Enter ID of teacher to find");
            var id = int.Parse(Console.ReadLine());
           
            if (!_teachers. ContainsKey(id))
            {
                Console.WriteLine($"Teacher with id {id} not found");
            }
            else
            {
                Console.WriteLine("Teacher Found");
            }           
        }

        protected override void listTeachers(IEnumerable<Teacher> teachers)
        {            
            foreach (var teacher in teachers)
            {
                Console.WriteLine(teacher);
            }
        }

        protected override void sortTeachers()
        {
            Console.WriteLine("You chose to sort teachers");
            Console.WriteLine("How would you like to sort them?");
            Console.WriteLine("1) ID");
            Console.WriteLine("2) Last Name");
            Console.WriteLine("3) First Name");

            var option = int.Parse(Console.ReadLine());
            ISortTeachersStrategy sortStrategy = null;
            switch (option)
            {
                case 1: sortStrategy = new SortTeachersByIdStrategy(); break; // create new strategy to sort by id
                case 2: sortStrategy = new SortTeachersByLastNameStrategy(); break;/* TODO... */ // create new strategy to sort by first name
                case 3: sortStrategy = new SortTeachersByFirstNameStrategy(); break;
            }

            var sorted = sortStrategy.Sort(_teachers.Values);
            listTeachers(sorted);
        }

        protected override void updateTeacher()
        {
            Console.WriteLine("Enter ID of teacher to update");
            var id = int.Parse(Console.ReadLine());
            if (!_teachers.ContainsKey(id))
            {
                Console.WriteLine($"Did not find teacher with id: {id}");
                return;
            }

            var teacher = _teachers[id];
            Console.WriteLine("You selected...");
            Console.WriteLine(teacher);            

            Console.WriteLine("Enter First name of teacher");
            string firstName = Console.ReadLine();
            teacher.FirstName = firstName; 
            
            Console.WriteLine("Enter Last name of teacher");
            string lastName = Console.ReadLine();
            teacher.LastName = lastName;
        }
    }
}
