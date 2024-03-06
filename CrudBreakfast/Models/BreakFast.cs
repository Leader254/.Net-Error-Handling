using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CrudBreakfast.ServiceErrors;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CrudBreakfast.Models
{
    public class BreakFast
    {
        public const int MinNameLength = 3;
        public const int MaxNameLength = 50;
        public const int MinDescriptionLength = 3;
        public const int MaxDescriptionLength = 100;

        public BreakFast()
        {
            Savory = new List<string>();
            Sweet = new List<string>();
        }

        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
        public List<string> Savory { get; set; }
        public List<string> Sweet { get; set; }

        public BreakFast(Guid id, string name, string description, DateTime startDateTime, DateTime endDateTime, DateTime lastModifiedDateTime, List<string> savory, List<string> sweet)
        {
            Id = id;
            Name = name;
            Description = description;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            LastModifiedDateTime = lastModifiedDateTime;
            Savory = savory;
            Sweet = sweet;
        }

        public static ErrorOr<BreakFast> Create(
            string name,
            string description,
            DateTime startDateTime,
            DateTime endDateTime,
            List<string> savory,
            List<string> sweet,
            Guid? id = null
        )
        {
            List<Error> errors = new();
            if (name.Length < MinNameLength || name.Length > MaxNameLength)
            {
                errors.Add(Errors.Breakfast.NameLength);
            }

            if (description.Length < MinDescriptionLength || description.Length > MaxDescriptionLength)
            {
                errors.Add(Errors.Breakfast.DescriptionLength);
            }

            if (startDateTime < DateTime.UtcNow)
            {
                errors.Add(Errors.Breakfast.StartDateTime);
            }

            if (endDateTime < startDateTime)
            {
                errors.Add(Errors.Breakfast.EndDateTime);
            }

            if (savory.Count == 0)
            {
                errors.Add(Errors.Breakfast.Savory);
            }

            if (sweet.Count == 0)
            {
                errors.Add(Errors.Breakfast.Sweet);
            }

            if (errors.Count > 0)
            {
                return errors;
            }

            var breakfast = new BreakFast(
                id ?? Guid.NewGuid(),
                name,
                description,
                startDateTime,
                endDateTime,
                DateTime.UtcNow,
                savory,
                sweet
            );

            return breakfast;
        }
    }
}
