namespace WebApplication2.Models;

public static class StoreWarehouse
{
    public static List<Phone> Phones { get; set; }

    static StoreWarehouse()
    {
        Phones = new List<Phone>
            {
                new Phone
                {
                    Id = 1,
                    Colour = "White",
                    Description = "Fast just got faster with Nexus S.",
                    Name = "Nexus S",
                    Price = 700
                },

                new Phone
                {
                    Id = 2,
                    Colour = "Black",
                    Description = "The Next, Next Generation phone.",
                    Name = "MOTOROLA XOOM™",
                    Price = 150
                },

                new Phone
                {
                    Id = 3,
                    Colour = "Gray",
                    Description = "Iphone 7, something special",
                    Name = "IPhone 7",
                    Price = 800

                },

                new Phone
                {
                    Id = 4,
                    Colour = "Gray",
                    Description = "Free your smartphone",
                    Name = "Galaxy S8",
                    Price = 740

                }
            };
    }
}

public static class LibraryWarehouse
{
    public static List<Book> Library { get; set; }

    static LibraryWarehouse()
    {
        Library = new List<Book>
            {
                new Book { Author = "Ray Bradbury",Title = "Fahrenheit 451" },
                new Book { Author = "Gabriel García Márquez", Title = "One Hundred years of Solitude" },
                new Book { Author = "George Orwell", Title = "1984" },
                new Book { Author = "Anais Nin", Title = "Delta of Venus" }
            };
    }
}
