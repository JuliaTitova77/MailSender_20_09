using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConsoleTests.Data.Entityes
{
    public class Entity
    {
        public int Id { get; set; }
        
    }
    public abstract class NamedEnyity:Entity// именнованная сущность
    {
        public string Name { get; set; }
    }
    public class Student:NamedEnyity
    {
        //[Key]
        //public int Primary Key{get;set;}

        [Required, MaxLength(120)]// ограничения на мак длину фамилии пропишет в БД
        public string Surname { get; set; }
        [MaxLength(120)]
        public string Patronymic { get; set; }

        public virtual Group Group { get; set; }// навигационные свойства через них осущ навигацию из объекта в БД и наоборот

    }
    public class Group:NamedEnyity
    {

        public string Description { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
