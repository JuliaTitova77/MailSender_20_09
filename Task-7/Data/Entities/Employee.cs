using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Task_7.Data.Entities
{
    public abstract class Entity
    {
        public int Id { get; set; }
    }
    public abstract class DataEntity : Entity
    {
        [Required]
        public string Name { get; set; }

    }
    public class Employee : DataEntity
    {
        public string Email { get; set; }
        [Required, MaxLength(20)]
        public string PhoneNumber { get; set; }
        public virtual Group Group { get; set; }// навигационное свойство
    }

    public class Group : DataEntity
    {
        public virtual ICollection<Employee> Employees { get; set; }// навигационное свойство
    }
}
