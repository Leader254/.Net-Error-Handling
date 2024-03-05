using ErrorOr;

namespace CrudBreakfast.ServiceErrors
{
    public static class Errors
    {
        public static class Breakfast
        {
            public static Error NotFound => Error.NotFound(
            code: "Breakfast.NotFound",
            description: "Breakfast not found");

            public static Error NameLength => Error.Validation(
            code: "Breakfast.NameLength",
            description: "Name must be between 3 and 50 characters");

            public static Error DescriptionLength => Error.Validation(
            code: "Breakfast.DescriptionLength",
            description: "Description must be between 3 and 100 characters");

            public static Error StartDateTime => Error.Validation(
            code: "Breakfast.StartDateTime",
            description: "Start date time must be in the future");

            public static Error EndDateTime => Error.Validation(
            code: "Breakfast.EndDateTime",
            description: "End date time must be after start date time");

            public static Error Savory => Error.Validation(
            code: "Breakfast.Savory",
            description: "Savory must have at least one item"); 

            public static Error Sweet => Error.Validation(
            code: "Breakfast.Sweet",
            description: "Sweet must have at least one item");

        }
    }
}
