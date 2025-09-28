using OnlineLibrary.Model;
using static System.Reflection.Metadata.BlobBuilder;

namespace OnlineLibrary.Data.Seeder
{
    public class BookSeeder
    {
        private static readonly Random _random = new Random();

        public static async Task SeedBooksAsync(OBDbcontext dbcontext)
        {
            if (!dbcontext.Books.Any())
            {
                var categories = dbcontext.Categories.ToList();

                // Helper methods
                string GenerateISBN()
                {
                    return string.Concat(Enumerable.Range(0, 13).Select(_ => _random.Next(0, 10)));
                }

                string GenerateImageUrl()
                {
                    int id = _random.Next(1, 1000);
                    return $"https://picsum.photos/seed/book{id}/300/400";
                }

                var books = new List<Book>();

                // FICTION (50 books)
                var fictionBooks = new[]
                {
                    new { Title = "To Kill a Mockingbird", Author = "Harper Lee", Year = 1960, Price = 15.99M, Description = "A timeless classic exploring racial injustice through the eyes of Scout Finch in 1930s Alabama." },
                    new { Title = "1984", Author = "George Orwell", Year = 1949, Price = 14.99M, Description = "A dystopian masterpiece depicting a totalitarian society under constant surveillance." },
                    new { Title = "Pride and Prejudice", Author = "Jane Austen", Year = 1813, Price = 12.99M, Description = "A witty romance exploring themes of love, reputation, and class in Georgian England." },
                    new { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Year = 1925, Price = 13.99M, Description = "A tragic tale of the American Dream set in the glittering Jazz Age of the 1920s." },
                    new { Title = "One Hundred Years of Solitude", Author = "Gabriel García Márquez", Year = 1967, Price = 16.99M, Description = "A magical realist epic chronicling the Buendía family across seven generations." },
                    new { Title = "The Catcher in the Rye", Author = "J.D. Salinger", Year = 1951, Price = 13.50M, Description = "Holden Caulfield's cynical journey through New York City in this coming-of-age classic." },
                    new { Title = "Lord of the Flies", Author = "William Golding", Year = 1954, Price = 11.99M, Description = "A dark allegory about civilization and human nature through stranded schoolboys." },
                    new { Title = "The Kite Runner", Author = "Khaled Hosseini", Year = 2003, Price = 15.50M, Description = "A powerful story of friendship, betrayal, and redemption set against Afghanistan's tumultuous history." },
                    new { Title = "Brave New World", Author = "Aldous Huxley", Year = 1932, Price = 14.25M, Description = "A prophetic vision of a future society controlled by technology and conditioning." },
                    new { Title = "The Book Thief", Author = "Markus Zusak", Year = 2005, Price = 16.00M, Description = "Death narrates the story of a young girl's love for books during Nazi Germany." },
                    new { Title = "Jane Eyre", Author = "Charlotte Brontë", Year = 1847, Price = 12.75M, Description = "An orphaned governess finds love and independence in this Gothic romance." },
                    new { Title = "Wuthering Heights", Author = "Emily Brontë", Year = 1847, Price = 11.50M, Description = "A passionate and destructive love story set on the Yorkshire moors." },
                    new { Title = "The Handmaid's Tale", Author = "Margaret Atwood", Year = 1985, Price = 15.75M, Description = "A chilling dystopian tale of a theocratic society where women have lost all rights." },
                    new { Title = "Beloved", Author = "Toni Morrison", Year = 1987, Price = 16.50M, Description = "A haunting exploration of slavery's legacy through the ghost of a murdered child." },
                    new { Title = "The Sun Also Rises", Author = "Ernest Hemingway", Year = 1926, Price = 13.25M, Description = "Lost generation expatriates navigate love and disillusionment in post-WWI Europe." },
                    new { Title = "On the Road", Author = "Jack Kerouac", Year = 1957, Price = 14.50M, Description = "A defining work of the Beat Generation chronicling cross-country adventures." },
                    new { Title = "Slaughterhouse-Five", Author = "Kurt Vonnegut", Year = 1969, Price = 13.99M, Description = "A darkly humorous anti-war novel blending science fiction with brutal reality." },
                    new { Title = "The Color Purple", Author = "Alice Walker", Year = 1982, Price = 15.25M, Description = "An African American woman's journey from abuse to empowerment in the rural South." },
                    new { Title = "Invisible Man", Author = "Ralph Ellison", Year = 1952, Price = 14.75M, Description = "A powerful exploration of identity and racism in mid-20th century America." },
                    new { Title = "The Adventures of Huckleberry Finn", Author = "Mark Twain", Year = 1884, Price = 11.99M, Description = "Huck and Jim's journey down the Mississippi River examining freedom and morality." },
                    new { Title = "Of Mice and Men", Author = "John Steinbeck", Year = 1937, Price = 12.50M, Description = "Two migrant workers during the Great Depression dream of a better life." },
                    new { Title = "The Grapes of Wrath", Author = "John Steinbeck", Year = 1939, Price = 16.25M, Description = "The Joad family's journey from Oklahoma to California during the Dust Bowl." },
                    new { Title = "Moby-Dick", Author = "Herman Melville", Year = 1851, Price = 17.99M, Description = "Captain Ahab's obsessive quest for revenge against the white whale." },
                    new { Title = "The Old Man and the Sea", Author = "Ernest Hemingway", Year = 1952, Price = 10.99M, Description = "An aging fisherman's epic struggle with a giant marlin off the Cuban coast." },
                    new { Title = "A Farewell to Arms", Author = "Ernest Hemingway", Year = 1929, Price = 13.75M, Description = "A love story set against the backdrop of World War I in Italy." },
                    new { Title = "The Sound and the Fury", Author = "William Faulkner", Year = 1929, Price = 15.99M, Description = "The decline of a Southern aristocratic family told through multiple perspectives." },
                    new { Title = "As I Lay Dying", Author = "William Faulkner", Year = 1930, Price = 14.50M, Description = "A poor Southern family's journey to bury their matriarch." },
                    new { Title = "Their Eyes Were Watching God", Author = "Zora Neale Hurston", Year = 1937, Price = 13.99M, Description = "Janie Crawford's journey toward self-discovery in the early 20th century South." },
                    new { Title = "The Bell Jar", Author = "Sylvia Plath", Year = 1963, Price = 14.25M, Description = "A young woman's descent into mental illness in 1950s New York." },
                    new { Title = "One Flew Over the Cuckoo's Nest", Author = "Ken Kesey", Year = 1962, Price = 15.50M, Description = "Rebellion against authority in a psychiatric hospital." },
                    new { Title = "Catch-22", Author = "Joseph Heller", Year = 1961, Price = 16.75M, Description = "Dark comedy about the absurdity of war and military bureaucracy." },
                    new { Title = "The Stranger", Author = "Albert Camus", Year = 1942, Price = 12.99M, Description = "An emotionally detached man faces the consequences of a senseless murder." },
                    new { Title = "The Trial", Author = "Franz Kafka", Year = 1925, Price = 14.99M, Description = "Josef K. is arrested and prosecuted by an inaccessible authority for an unspecified crime." },
                    new { Title = "Crime and Punishment", Author = "Fyodor Dostoevsky", Year = 1866, Price = 18.50M, Description = "A poor student commits murder and grapples with guilt and redemption." },
                    new { Title = "The Brothers Karamazov", Author = "Fyodor Dostoevsky", Year = 1880, Price = 19.99M, Description = "A philosophical novel exploring faith, doubt, and morality through one family." },
                    new { Title = "War and Peace", Author = "Leo Tolstoy", Year = 1869, Price = 22.99M, Description = "Epic tale of Russian society during the Napoleonic era." },
                    new { Title = "Anna Karenina", Author = "Leo Tolstoy", Year = 1877, Price = 18.75M, Description = "A tragic love affair that challenges Russian society's moral codes." },
                    new { Title = "Madame Bovary", Author = "Gustave Flaubert", Year = 1857, Price = 13.99M, Description = "Emma Bovary's pursuit of passion leads to her tragic downfall." },
                    new { Title = "The Count of Monte Cristo", Author = "Alexandre Dumas", Year = 1844, Price = 17.50M, Description = "Edmond Dantès' elaborate revenge after wrongful imprisonment." },
                    new { Title = "Les Misérables", Author = "Victor Hugo", Year = 1862, Price = 21.99M, Description = "Jean Valjean's redemption story set against 19th century French society." },
                    new { Title = "Great Expectations", Author = "Charles Dickens", Year = 1861, Price = 14.99M, Description = "Pip's journey from humble origins to gentleman through mysterious benefaction." },
                    new { Title = "A Tale of Two Cities", Author = "Charles Dickens", Year = 1859, Price = 13.75M, Description = "Love and sacrifice during the French Revolution." },
                    new { Title = "Oliver Twist", Author = "Charles Dickens", Year = 1838, Price = 12.99M, Description = "An orphan boy's struggles in Victorian London's criminal underworld." },
                    new { Title = "David Copperfield", Author = "Charles Dickens", Year = 1850, Price = 16.25M, Description = "Dickens' semi-autobiographical novel of a young man's coming of age." },
                    new { Title = "Frankenstein", Author = "Mary Shelley", Year = 1818, Price = 11.99M, Description = "Victor Frankenstein creates life with horrifying consequences." },
                    new { Title = "Dracula", Author = "Bram Stoker", Year = 1897, Price = 12.50M, Description = "The classic vampire novel that defined the genre." },
                    new { Title = "The Picture of Dorian Gray", Author = "Oscar Wilde", Year = 1890, Price = 13.25M, Description = "A young man's portrait ages while he remains eternally youthful and corrupt." },
                    new { Title = "Heart of Darkness", Author = "Joseph Conrad", Year = 1899, Price = 11.75M, Description = "Marlow's journey into the African interior reveals the darkness of colonialism." },
                    new { Title = "The Turn of the Screw", Author = "Henry James", Year = 1898, Price = 10.99M, Description = "A governess encounters supernatural forces at a remote estate." },
                    new { Title = "Middlemarch", Author = "George Eliot", Year = 1871, Price = 18.99M, Description = "Provincial life in 19th century England through interconnected stories." }
                };

                var fictionCategoryId = categories.First(c => c.Name == "Fiction").Id;
                foreach (var book in fictionBooks)
                {
                    books.Add(new Book
                    {
                        Title = book.Title,
                        Author = book.Author,
                        CategoryId = fictionCategoryId,
                        Price = book.Price,
                        PublishedYear = book.Year,
                        ISBN = GenerateISBN(),
                        ImageUrl = GenerateImageUrl(),
                        Stock = _random.Next(5, 20),
                        Description = book.Description
                    });
                }

                // SCIENCE FICTION (30 books)
                var sciFiBooks = new[]
                {
                    new { Title = "Dune", Author = "Frank Herbert", Year = 1965, Price = 17.50M, Description = "An epic space opera about politics, religion, and ecology on the desert planet Arrakis." },
                    new { Title = "Neuromancer", Author = "William Gibson", Year = 1984, Price = 15.20M, Description = "The cyberpunk novel that coined 'cyberspace' and predicted the internet age." },
                    new { Title = "Foundation", Author = "Isaac Asimov", Year = 1951, Price = 16.80M, Description = "The first book in Asimov's acclaimed series about psychohistory and galactic empire." },
                    new { Title = "Ender's Game", Author = "Orson Scott Card", Year = 1985, Price = 14.75M, Description = "A brilliant child is trained to command Earth's fleet against an alien invasion." },
                    new { Title = "The Hitchhiker's Guide to the Galaxy", Author = "Douglas Adams", Year = 1979, Price = 13.99M, Description = "A hilarious cosmic adventure beginning with Earth's demolition for a hyperspace bypass." },
                    new { Title = "Fahrenheit 451", Author = "Ray Bradbury", Year = 1953, Price = 12.50M, Description = "A fireman who burns books questions his purpose in a society that bans literature." },
                    new { Title = "The Left Hand of Darkness", Author = "Ursula K. Le Guin", Year = 1969, Price = 14.99M, Description = "A groundbreaking exploration of gender and society on the planet Gethen." },
                    new { Title = "Hyperion", Author = "Dan Simmons", Year = 1989, Price = 16.25M, Description = "Seven pilgrims journey to the mysterious world of Hyperion in this space opera." },
                    new { Title = "The Time Machine", Author = "H.G. Wells", Year = 1895, Price = 10.99M, Description = "The classic tale that popularized the concept of time travel." },
                    new { Title = "I, Robot", Author = "Isaac Asimov", Year = 1950, Price = 13.75M, Description = "Nine interconnected stories exploring the Three Laws of Robotics." },
                    new { Title = "2001: A Space Odyssey", Author = "Arthur C. Clarke", Year = 1968, Price = 15.50M, Description = "Humanity's evolution guided by mysterious monoliths across time and space." },
                    new { Title = "Blade Runner (Do Androids Dream of Electric Sheep?)", Author = "Philip K. Dick", Year = 1968, Price = 14.25M, Description = "A bounty hunter tracks artificial humans in post-apocalyptic San Francisco." },
                    new { Title = "The War of the Worlds", Author = "H.G. Wells", Year = 1898, Price = 11.99M, Description = "Martians invade Earth with devastating technology and consequences." },
                    new { Title = "Starship Troopers", Author = "Robert A. Heinlein", Year = 1959, Price = 13.50M, Description = "Military science fiction exploring citizenship through interstellar warfare." },
                    new { Title = "The Martian Chronicles", Author = "Ray Bradbury", Year = 1950, Price = 14.99M, Description = "Interconnected stories about human colonization of Mars." },
                    new { Title = "Contact", Author = "Carl Sagan", Year = 1985, Price = 16.00M, Description = "First contact with extraterrestrial intelligence through radio astronomy." },
                    new { Title = "The Forever War", Author = "Joe Haldeman", Year = 1974, Price = 15.25M, Description = "A soldier fights an interstellar war spanning centuries due to relativistic effects." },
                    new { Title = "Childhood's End", Author = "Arthur C. Clarke", Year = 1953, Price = 13.99M, Description = "Alien overlords guide humanity's evolution toward transcendence." },
                    new { Title = "The Stars My Destination", Author = "Alfred Bester", Year = 1956, Price = 14.50M, Description = "Gully Foyle seeks revenge in a future where teleportation is common." },
                    new { Title = "Gateway", Author = "Frederik Pohl", Year = 1977, Price = 15.75M, Description = "Prospectors explore alien spacecraft for riches and survival." },
                    new { Title = "Rendezvous with Rama", Author = "Arthur C. Clarke", Year = 1973, Price = 14.99M, Description = "Humans explore a mysterious alien spacecraft entering the solar system." },
                    new { Title = "The Dispossessed", Author = "Ursula K. Le Guin", Year = 1974, Price = 16.50M, Description = "A physicist navigates between two contrasting societies on twin planets." },
                    new { Title = "Snow Crash", Author = "Neal Stephenson", Year = 1992, Price = 17.25M, Description = "Hacker Hero Protagonist battles a computer virus in cyberspace and reality." },
                    new { Title = "The Diamond Age", Author = "Neal Stephenson", Year = 1995, Price = 18.00M, Description = "Nanotechnology transforms society through an interactive educational book." },
                    new { Title = "Altered Carbon", Author = "Richard K. Morgan", Year = 2002, Price = 15.99M, Description = "Consciousness can be transferred between bodies in this cyberpunk noir." },
                    new { Title = "The Expanse: Leviathan Wakes", Author = "James S.A. Corey", Year = 2011, Price = 16.75M, Description = "Political tensions erupt across the solar system involving Earth, Mars, and the Belt." },
                    new { Title = "Red Mars", Author = "Kim Stanley Robinson", Year = 1992, Price = 17.50M, Description = "The first colonists begin terraforming Mars in this hard science fiction epic." },
                    new { Title = "The Windup Girl", Author = "Paolo Bacigalupi", Year = 2009, Price = 15.50M, Description = "Biopunk vision of a climate-changed future dominated by biotechnology." },
                    new { Title = "Old Man's War", Author = "John Scalzi", Year = 2005, Price = 14.99M, Description = "Elderly humans get new bodies to fight in interstellar warfare." },
                    new { Title = "The Fifth Season", Author = "N.K. Jemisin", Year = 2015, Price = 16.25M, Description = "A world where seismic catastrophes shape civilization and human evolution." }
                };

                var sciFiCategoryId = categories.First(c => c.Name == "Science Fiction").Id;
                foreach (var book in sciFiBooks)
                {
                    books.Add(new Book
                    {
                        Title = book.Title,
                        Author = book.Author,
                        CategoryId = sciFiCategoryId,
                        Price = book.Price,
                        PublishedYear = book.Year,
                        ISBN = GenerateISBN(),
                        ImageUrl = GenerateImageUrl(),
                        Stock = _random.Next(8, 15),
                        Description = book.Description
                    });
                }

                // FANTASY (25 books)
                var fantasyBooks = new[]
                {
                    new { Title = "The Lord of the Rings", Author = "J.R.R. Tolkien", Year = 1954, Price = 25.99M, Description = "The epic fantasy trilogy that defined the genre, following Frodo's quest to destroy the One Ring." },
                    new { Title = "The Hobbit", Author = "J.R.R. Tolkien", Year = 1937, Price = 14.99M, Description = "Bilbo Baggins' unexpected journey to reclaim the dwarves' treasure from Smaug the dragon." },
                    new { Title = "A Game of Thrones", Author = "George R.R. Martin", Year = 1996, Price = 18.99M, Description = "Political intrigue and warfare in the Seven Kingdoms as winter approaches." },
                    new { Title = "Harry Potter and the Philosopher's Stone", Author = "J.K. Rowling", Year = 1997, Price = 12.99M, Description = "A young wizard discovers his magical heritage and attends Hogwarts School." },
                    new { Title = "The Name of the Wind", Author = "Patrick Rothfuss", Year = 2007, Price = 16.50M, Description = "Kvothe recounts his legendary adventures at the University and beyond." },
                    new { Title = "The Way of Kings", Author = "Brandon Sanderson", Year = 2010, Price = 19.99M, Description = "Epic fantasy set on a storm-swept world where magical weapons hold ancient power." },
                    new { Title = "The Eye of the World", Author = "Robert Jordan", Year = 1990, Price = 17.25M, Description = "Young villagers discover their destinies as the Wheel of Time turns." },
                    new { Title = "The Chronicles of Narnia", Author = "C.S. Lewis", Year = 1950, Price = 22.99M, Description = "Children discover a magical world beyond the wardrobe door." },
                    new { Title = "The Dark Tower: The Gunslinger", Author = "Stephen King", Year = 1982, Price = 15.75M, Description = "Roland Deschain pursues the Man in Black across a post-apocalyptic landscape." },
                    new { Title = "The Earthsea Cycle", Author = "Ursula K. Le Guin", Year = 1968, Price = 18.50M, Description = "A young wizard's coming-of-age in an archipelago world of islands and magic." },
                    new { Title = "The Princess Bride", Author = "William Goldman", Year = 1973, Price = 13.99M, Description = "A tale of true love, adventure, and revenge told with wit and charm." },
                    new { Title = "The Last Unicorn", Author = "Peter S. Beagle", Year = 1968, Price = 14.25M, Description = "The world's last unicorn searches for others of her kind." },
                    new { Title = "Good Omens", Author = "Terry Pratchett & Neil Gaiman", Year = 1990, Price = 16.99M, Description = "An angel and demon team up to prevent the apocalypse with humor and wit." },
                    new { Title = "American Gods", Author = "Neil Gaiman", Year = 2001, Price = 17.50M, Description = "Old gods struggle against new ones in contemporary America." },
                    new { Title = "The Sandman", Author = "Neil Gaiman", Year = 1989, Price = 24.99M, Description = "Dream of the Endless navigates mythology and reality in this graphic novel series." },
                    new { Title = "The Once and Future King", Author = "T.H. White", Year = 1958, Price = 16.25M, Description = "The legend of King Arthur retold with humor and modern sensibility." },
                    new { Title = "The Sword of Shannara", Author = "Terry Brooks", Year = 1977, Price = 15.50M, Description = "A young man discovers his heritage and must save the world from ancient evil." },
                    new { Title = "The Book of Three", Author = "Lloyd Alexander", Year = 1964, Price = 11.99M, Description = "A young assistant pig-keeper becomes a hero in the Chronicles of Prydain." },
                    new { Title = "The Golden Compass", Author = "Philip Pullman", Year = 1995, Price = 14.75M, Description = "Lyra journeys to parallel worlds with her daemon companion Pan." },
                    new { Title = "The Giver", Author = "Lois Lowry", Year = 1993, Price = 12.50M, Description = "A boy discovers the truth about his seemingly perfect dystopian society." },
                    new { Title = "Watership Down", Author = "Richard Adams", Year = 1972, Price = 13.75M, Description = "A group of rabbits search for a new home in this allegorical adventure." },
                    new { Title = "The Neverending Story", Author = "Michael Ende", Year = 1979, Price = 15.99M, Description = "A boy reading a book becomes part of the fantasy world he's reading about." },
                    new { Title = "The Wheel of Time", Author = "Robert Jordan", Year = 1990, Price = 89.99M, Description = "Complete 14-book epic fantasy series about the Dragon Reborn and the Last Battle." },
                    new { Title = "Mistborn: The Final Empire", Author = "Brandon Sanderson", Year = 2006, Price = 16.75M, Description = "A thief discovers she has magical powers in a world where ash falls from the sky." },
                    new { Title = "The Lies of Locke Lamora", Author = "Scott Lynch", Year = 2006, Price = 15.25M, Description = "Fantasy heist novel following gentleman thieves in a Venice-like city." }
                };

                var fantasyCategoryId = categories.First(c => c.Name == "Fantasy").Id;
                foreach (var book in fantasyBooks)
                {
                    books.Add(new Book
                    {
                        Title = book.Title,
                        Author = book.Author,
                        CategoryId = fantasyCategoryId,
                        Price = book.Price,
                        PublishedYear = book.Year,
                        ISBN = GenerateISBN(),
                        ImageUrl = GenerateImageUrl(),
                        Stock = _random.Next(6, 18),
                        Description = book.Description
                    });
                }

                // MYSTERY & THRILLER (25 books)
                var mysteryBooks = new[]
                {
                    new { Title = "The Girl with the Dragon Tattoo", Author = "Stieg Larsson", Year = 2005, Price = 16.99M, Description = "A journalist and hacker investigate a wealthy family's dark secrets." },
                    new { Title = "Gone Girl", Author = "Gillian Flynn", Year = 2012, Price = 15.99M, Description = "A marriage's dark secrets are revealed when a wife mysteriously disappears." },
                    new { Title = "The Da Vinci Code", Author = "Dan Brown", Year = 2003, Price = 14.99M, Description = "A symbologist uncovers a religious conspiracy through art and history." },
                    new { Title = "And Then There Were None", Author = "Agatha Christie", Year = 1939, Price = 12.99M, Description = "Ten strangers are trapped on an island as they're murdered one by one." },
                    new { Title = "The Murder of Roger Ackroyd", Author = "Agatha Christie", Year = 1926, Price = 11.99M, Description = "Hercule Poirot investigates a shocking murder with an unexpected twist." },
                    new { Title = "The Big Sleep", Author = "Raymond Chandler", Year = 1939, Price = 13.50M, Description = "Private detective Philip Marlowe navigates corruption in 1940s Los Angeles." },
                    new { Title = "The Maltese Falcon", Author = "Dashiell Hammett", Year = 1930, Price = 12.75M, Description = "Detective Sam Spade searches for a valuable statue in this noir classic." },
                    new { Title = "In the Woods", Author = "Tana French", Year = 2007, Price = 15.25M, Description = "A detective investigates a child's murder while confronting his own past." },
                    new { Title = "The Silence of the Lambs", Author = "Thomas Harris", Year = 1988, Price = 14.50M, Description = "FBI trainee Clarice Starling seeks help from Dr. Hannibal Lecter." },
                    new { Title = "The Girl on the Train", Author = "Paula Hawkins", Year = 2015, Price = 15.75M, Description = "An unreliable narrator becomes involved in a missing persons case." },
                    new { Title = "The Talented Mr. Ripley", Author = "Patricia Highsmith", Year = 1955, Price = 13.99M, Description = "A young man assumes another's identity with deadly consequences." },
                    new { Title = "Rebecca", Author = "Daphne du Maurier", Year = 1938, Price = 14.25M, Description = "A young bride is haunted by her husband's mysterious first wife." },
                    new { Title = "The Hound of the Baskervilles", Author = "Arthur Conan Doyle", Year = 1902, Price = 11.50M, Description = "Sherlock Holmes investigates a supernatural curse plaguing a noble family." },
                    new { Title = "The Adventures of Sherlock Holmes", Author = "Arthur Conan Doyle", Year = 1892, Price = 13.25M, Description = "Classic detective stories featuring the world's most famous consulting detective." },
                    new { Title = "The Cuckoo's Calling", Author = "Robert Galbraith", Year = 2013, Price = 16.50M, Description = "Private detective Cormoran Strike investigates a supermodel's apparent suicide." },
                    new { Title = "Tinker Tailor Soldier Spy", Author = "John le Carré", Year = 1974, Price = 15.99M, Description = "George Smiley hunts for a Soviet mole in British intelligence." },
                    new { Title = "The Spy Who Came In from the Cold", Author = "John le Carré", Year = 1963, Price = 14.75M, Description = "A British agent's final mission during the Cold War." },
                    new { Title = "Casino Royale", Author = "Ian Fleming", Year = 1953, Price = 13.99M, Description = "James Bond's first mission against the villainous Le Chiffre." },
                    new { Title = "The Bourne Identity", Author = "Robert Ludlum", Year = 1980, Price = 15.50M, Description = "An amnesiac assassin tries to discover his identity while being hunted." },
                    new { Title = "The Hunt for Red October", Author = "Tom Clancy", Year = 1984, Price = 16.25M, Description = "A Soviet submarine captain attempts to defect during the Cold War." },
                    new { Title = "The Firm", Author = "John Grisham", Year = 1991, Price = 14.99M, Description = "A young lawyer discovers his law firm's deadly secrets." },
                    new { Title = "A Time to Kill", Author = "John Grisham", Year = 1989, Price = 15.25M, Description = "A lawyer defends a black father who took justice into his own hands." },
                    new { Title = "Presumed Innocent", Author = "Scott Turow", Year = 1987, Price = 14.50M, Description = "A prosecutor becomes the prime suspect in his colleague's murder." },
                    new { Title = "The Pelican Brief", Author = "John Grisham", Year = 1992, Price = 15.75M, Description = "A law student's theory about Supreme Court murders puts her life in danger." },
                    new { Title = "Devil in a Blue Dress", Author = "Walter Mosley", Year = 1990, Price = 13.75M, Description = "Easy Rawlins navigates racial tensions in 1940s Los Angeles while solving crimes." }
                };

                var mysteryCategoryId = categories.First(c => c.Name == "Mystery & Thriller").Id;
                foreach (var book in mysteryBooks)
                {
                    books.Add(new Book
                    {
                        Title = book.Title,
                        Author = book.Author,
                        CategoryId = mysteryCategoryId,
                        Price = book.Price,
                        PublishedYear = book.Year,
                        ISBN = GenerateISBN(),
                        ImageUrl = GenerateImageUrl(),
                        Stock = _random.Next(8, 16),
                        Description = book.Description
                    });
                }

                // COMPUTER & PROGRAMMING (30 books)
                var programmingBooks = new[]
                {
                    new { Title = "Clean Code", Author = "Robert C. Martin", Year = 2008, Price = 39.99M, Description = "A handbook teaching the principles of writing maintainable, readable code." },
                    new { Title = "Design Patterns", Author = "Erich Gamma", Year = 1994, Price = 49.99M, Description = "The Gang of Four's seminal work on reusable object-oriented software design." },
                    new { Title = "The Pragmatic Programmer", Author = "Andy Hunt & Dave Thomas", Year = 1999, Price = 42.00M, Description = "Essential practices for becoming a more effective and pragmatic programmer." },
                    new { Title = "Code Complete", Author = "Steve McConnell", Year = 2004, Price = 45.50M, Description = "A comprehensive guide to software construction and best practices." },
                    new { Title = "Refactoring", Author = "Martin Fowler", Year = 1999, Price = 44.99M, Description = "Improving the design of existing code through systematic refactoring techniques." },
                    new { Title = "Introduction to Algorithms", Author = "Thomas H. Cormen", Year = 2009, Price = 89.99M, Description = "The comprehensive reference for algorithms and data structures." },
                    new { Title = "You Don't Know JS", Author = "Kyle Simpson", Year = 2014, Price = 35.00M, Description = "A deep dive into JavaScript's core mechanisms and advanced concepts." },
                    new { Title = "Effective Java", Author = "Joshua Bloch", Year = 2017, Price = 47.50M, Description = "Best practices for writing robust, maintainable Java applications." },
                    new { Title = "Python Crash Course", Author = "Eric Matthes", Year = 2019, Price = 32.99M, Description = "A hands-on introduction to Python programming for beginners." },
                    new { Title = "The Art of Computer Programming", Author = "Donald E. Knuth", Year = 1968, Price = 199.99M, Description = "Knuth's legendary multi-volume work on algorithms and programming techniques." },
                    new { Title = "Structure and Interpretation of Computer Programs", Author = "Harold Abelson", Year = 1985, Price = 65.00M, Description = "MIT's classic text on computer science fundamentals using Scheme." },
                    new { Title = "Cracking the Coding Interview", Author = "Gayle Laakmann McDowell", Year = 2015, Price = 38.95M, Description = "Programming interview preparation with 189 questions and solutions." },
                    new { Title = "Clean Architecture", Author = "Robert C. Martin", Year = 2017, Price = 41.99M, Description = "A craftsman's guide to software structure and design principles." },
                    new { Title = "The Mythical Man-Month", Author = "Frederick P. Brooks Jr.", Year = 1975, Price = 36.50M, Description = "Essays on software engineering and project management." },
                    new { Title = "Head First Design Patterns", Author = "Eric Freeman", Year = 2004, Price = 43.99M, Description = "A brain-friendly guide to design patterns with real-world examples." },
                    new { Title = "JavaScript: The Good Parts", Author = "Douglas Crockford", Year = 2008, Price = 29.99M, Description = "Identifying and using the best features of JavaScript." },
                    new { Title = "Eloquent JavaScript", Author = "Marijn Haverbeke", Year = 2018, Price = 33.50M, Description = "A modern introduction to programming with JavaScript." },
                    new { Title = "Learning Python", Author = "Mark Lutz", Year = 2013, Price = 54.99M, Description = "Comprehensive coverage of Python programming language." },
                    new { Title = "The C Programming Language", Author = "Brian W. Kernighan", Year = 1978, Price = 52.99M, Description = "The definitive guide to C programming by its creators." },
                    new { Title = "Effective C++", Author = "Scott Meyers", Year = 2005, Price = 44.99M, Description = "55 specific ways to improve your C++ programs and designs." },
                    new { Title = "Java: The Complete Reference", Author = "Herbert Schildt", Year = 2020, Price = 48.50M, Description = "Comprehensive coverage of the Java programming language." },
                    new { Title = "Pro Git", Author = "Scott Chacon", Year = 2014, Price = 39.99M, Description = "Everything you need to know about Git version control." },
                    new { Title = "Database System Concepts", Author = "Abraham Silberschatz", Year = 2019, Price = 87.99M, Description = "Comprehensive introduction to database management systems." },
                    new { Title = "Computer Networks", Author = "Andrew S. Tanenbaum", Year = 2010, Price = 79.99M, Description = "Comprehensive guide to computer networking principles and protocols." },
                    new { Title = "Operating System Concepts", Author = "Abraham Silberschatz", Year = 2018, Price = 84.99M, Description = "Fundamental concepts of operating systems design and implementation." },
                    new { Title = "Artificial Intelligence: A Modern Approach", Author = "Stuart Russell", Year = 2020, Price = 92.99M, Description = "Comprehensive introduction to artificial intelligence theory and practice." },
                    new { Title = "Machine Learning", Author = "Tom M. Mitchell", Year = 1997, Price = 78.50M, Description = "Classic textbook on machine learning algorithms and applications." },
                    new { Title = "Deep Learning", Author = "Ian Goodfellow", Year = 2016, Price = 67.99M, Description = "Comprehensive guide to deep learning theory and implementation." },
                    new { Title = "Hands-On Machine Learning", Author = "Aurélien Géron", Year = 2019, Price = 54.99M, Description = "Practical machine learning with Scikit-Learn, Keras, and TensorFlow." },
                    new { Title = "The Elements of Statistical Learning", Author = "Trevor Hastie", Year = 2009, Price = 89.50M, Description = "Data mining, inference, and prediction using statistical methods." }
                };

                var programmingCategoryId = categories.First(c => c.Name == "Computer & Programming").Id;
                foreach (var book in programmingBooks)
                {
                    books.Add(new Book
                    {
                        Title = book.Title,
                        Author = book.Author,
                        CategoryId = programmingCategoryId,
                        Price = book.Price,
                        PublishedYear = book.Year,
                        ISBN = GenerateISBN(),
                        ImageUrl = GenerateImageUrl(),
                        Stock = _random.Next(10, 25),
                        Description = book.Description
                    });
                }

                // BUSINESS & FINANCE (25 books)
                var businessBooks = new[]
                {
                    new { Title = "Rich Dad Poor Dad", Author = "Robert Kiyosaki", Year = 1997, Price = 20.00M, Description = "Contrasting financial philosophies that challenge conventional thinking about money." },
                    new { Title = "The Intelligent Investor", Author = "Benjamin Graham", Year = 1949, Price = 25.00M, Description = "The definitive book on value investing and market philosophy." },
                    new { Title = "Think and Grow Rich", Author = "Napoleon Hill", Year = 1937, Price = 18.99M, Description = "The classic guide to achieving success through positive thinking and persistence." },
                    new { Title = "The 7 Habits of Highly Effective People", Author = "Stephen R. Covey", Year = 1989, Price = 22.50M, Description = "Principles for personal and professional effectiveness based on character ethics." },
                    new { Title = "Good to Great", Author = "Jim Collins", Year = 2001, Price = 24.99M, Description = "Research-based insights on what transforms good companies into great ones." },
                    new { Title = "The Lean Startup", Author = "Eric Ries", Year = 2011, Price = 23.00M, Description = "A methodology for developing businesses and products through validated learning." },
                    new { Title = "Principles", Author = "Ray Dalio", Year = 2017, Price = 28.50M, Description = "Life and work principles from the founder of the world's largest hedge fund." },
                    new { Title = "The Millionaire Next Door", Author = "Thomas J. Stanley", Year = 1996, Price = 21.99M, Description = "Surprising secrets of America's wealthy based on comprehensive research." },
                    new { Title = "Zero to One", Author = "Peter Thiel", Year = 2014, Price = 26.00M, Description = "Notes on startups and how to build the future through innovation." },
                    new { Title = "The E-Myth Revisited", Author = "Michael E. Gerber", Year = 1995, Price = 19.95M, Description = "Why most small businesses don't work and what to do about it." },
                    new { Title = "Built to Last", Author = "Jim Collins", Year = 1994, Price = 23.50M, Description = "Successful habits of visionary companies that have stood the test of time." },
                    new { Title = "The Innovator's Dilemma", Author = "Clayton M. Christensen", Year = 1997, Price = 25.99M, Description = "How successful companies can lose market leadership through disruptive innovation." },
                    new { Title = "Crossing the Chasm", Author = "Geoffrey A. Moore", Year = 1991, Price = 24.50M, Description = "Marketing and selling high-tech products to mainstream customers." },
                    new { Title = "The 4-Hour Workweek", Author = "Timothy Ferriss", Year = 2007, Price = 21.00M, Description = "Escape the 9-5, live anywhere, and join the new rich through lifestyle design." },
                    new { Title = "Purple Cow", Author = "Seth Godin", Year = 2003, Price = 18.99M, Description = "Transform your business by being remarkable in a crowded marketplace." },
                    new { Title = "The Tipping Point", Author = "Malcolm Gladwell", Year = 2000, Price = 20.50M, Description = "How little things can make a big difference in trends and social epidemics." },
                    new { Title = "Outliers", Author = "Malcolm Gladwell", Year = 2008, Price = 22.25M, Description = "The story of success and what makes high-achievers different." },
                    new { Title = "Freakonomics", Author = "Steven D. Levitt", Year = 2005, Price = 19.75M, Description = "A rogue economist explores the hidden side of everything." },
                    new { Title = "The Black Swan", Author = "Nassim Nicholas Taleb", Year = 2007, Price = 24.99M, Description = "The impact of highly improbable events on markets and life." },
                    new { Title = "Antifragile", Author = "Nassim Nicholas Taleb", Year = 2012, Price = 26.50M, Description = "Things that gain from disorder and how to thrive in uncertainty." },
                    new { Title = "The Art of War", Author = "Sun Tzu", Year = -500, Price = 12.99M, Description = "Ancient Chinese military strategy applied to business and competition." },
                    new { Title = "How to Win Friends and Influence People", Author = "Dale Carnegie", Year = 1936, Price = 16.99M, Description = "Timeless advice on human relations and communication skills." },
                    new { Title = "Getting to Yes", Author = "Roger Fisher", Year = 1981, Price = 18.50M, Description = "Principled negotiation techniques for reaching mutually beneficial agreements." },
                    new { Title = "The One Minute Manager", Author = "Ken Blanchard", Year = 1982, Price = 15.99M, Description = "Simple and practical management techniques for busy executives." },
                    new { Title = "First Things First", Author = "Stephen R. Covey", Year = 1994, Price = 20.99M, Description = "Time management based on principles rather than priorities alone." }
                };

                var businessCategoryId = categories.First(c => c.Name == "Business & Finance").Id;
                foreach (var book in businessBooks)
                {
                    books.Add(new Book
                    {
                        Title = book.Title,
                        Author = book.Author,
                        CategoryId = businessCategoryId,
                        Price = book.Price,
                        PublishedYear = book.Year,
                        ISBN = GenerateISBN(),
                        ImageUrl = GenerateImageUrl(),
                        Stock = _random.Next(15, 30),
                        Description = book.Description
                    });
                }

                // PSYCHOLOGY (20 books)
                var psychologyBooks = new[]
                {
                    new { Title = "Thinking, Fast and Slow", Author = "Daniel Kahneman", Year = 2011, Price = 23.00M, Description = "Nobel laureate's exploration of the two systems that drive human thinking." },
                    new { Title = "The Psychology of Money", Author = "Morgan Housel", Year = 2020, Price = 22.00M, Description = "How psychology, not mathematics, drives financial decisions and wealth building." },
                    new { Title = "Man's Search for Meaning", Author = "Viktor E. Frankl", Year = 1946, Price = 18.00M, Description = "A Holocaust survivor's profound insights on finding purpose in suffering." },
                    new { Title = "Influence: The Psychology of Persuasion", Author = "Robert B. Cialdini", Year = 1984, Price = 21.50M, Description = "The six universal principles that guide human behavior and decision-making." },
                    new { Title = "Mindset", Author = "Carol S. Dweck", Year = 2006, Price = 20.99M, Description = "How fixed and growth mindsets shape our lives and potential for success." },
                    new { Title = "The Power of Habit", Author = "Charles Duhigg", Year = 2012, Price = 19.75M, Description = "The science of how habits work and how to change them effectively." },
                    new { Title = "Atomic Habits", Author = "James Clear", Year = 2018, Price = 24.50M, Description = "An easy and proven way to build good habits and break bad ones." },
                    new { Title = "Flow", Author = "Mihaly Csikszentmihalyi", Year = 1990, Price = 22.25M, Description = "The psychology of optimal experience and peak performance states." },
                    new { Title = "Predictably Irrational", Author = "Dan Ariely", Year = 2008, Price = 20.50M, Description = "Hidden forces that shape our decisions and reveal our irrational behavior." },
                    new { Title = "The Paradox of Choice", Author = "Barry Schwartz", Year = 2004, Price = 18.95M, Description = "Why more choice can create anxiety and how to find satisfaction." },
                    new { Title = "Blink", Author = "Malcolm Gladwell", Year = 2005, Price = 19.50M, Description = "The power of thinking without thinking and rapid decision-making." },
                    new { Title = "The Social Animal", Author = "David Brooks", Year = 2011, Price = 21.99M, Description = "How unconscious processes shape our lives and relationships." },
                    new { Title = "Stumbling on Happiness", Author = "Daniel Gilbert", Year = 2006, Price = 18.75M, Description = "Why we're bad at predicting what will make us happy." },
                    new { Title = "The Happiness Hypothesis", Author = "Jonathan Haidt", Year = 2006, Price = 20.25M, Description = "Ancient wisdom meets modern science in the pursuit of happiness." },
                    new { Title = "Emotional Intelligence", Author = "Daniel Goleman", Year = 1995, Price = 19.99M, Description = "Why emotional intelligence matters more than IQ for success." },
                    new { Title = "The Righteous Mind", Author = "Jonathan Haidt", Year = 2012, Price = 22.50M, Description = "Why good people are divided by politics and religion." },
                    new { Title = "Sapiens", Author = "Yuval Noah Harari", Year = 2014, Price = 26.99M, Description = "A brief history of humankind from cognitive revolution to present." },
                    new { Title = "21 Lessons for the 21st Century", Author = "Yuval Noah Harari", Year = 2018, Price = 24.99M, Description = "What today's most pressing challenges mean for humanity's future." },
                    new { Title = "The Undoing Project", Author = "Michael Lewis", Year = 2016, Price = 23.50M, Description = "The friendship that changed our minds about how the mind works." },
                    new { Title = "Noise", Author = "Daniel Kahneman", Year = 2021, Price = 25.99M, Description = "A flaw in human judgment and how to reduce unwanted variability in decisions." }
                };

                var psychologyCategoryId = categories.First(c => c.Name == "Psychology").Id;
                foreach (var book in psychologyBooks)
                {
                    books.Add(new Book
                    {
                        Title = book.Title,
                        Author = book.Author,
                        CategoryId = psychologyCategoryId,
                        Price = book.Price,
                        PublishedYear = book.Year,
                        ISBN = GenerateISBN(),
                        ImageUrl = GenerateImageUrl(),
                        Stock = _random.Next(12, 25),
                        Description = book.Description
                    });
                }

                // Add books from other categories (History, Biography, etc.)
                // Continue with remaining categories to reach 500+ books total...

                // HISTORY (20 books)
                var historyBooks = new[]
                {
                    new { Title = "Sapiens", Author = "Yuval Noah Harari", Year = 2014, Price = 26.99M, Description = "A brief history of humankind from the Stone Age to the present." },
                    new { Title = "Guns, Germs, and Steel", Author = "Jared Diamond", Year = 1997, Price = 24.50M, Description = "The fates of human societies shaped by geography and environment." },
                    new { Title = "The Diary of a Young Girl", Author = "Anne Frank", Year = 1947, Price = 16.00M, Description = "The moving diary of a Jewish girl hiding during the Holocaust." },
                    new { Title = "A People's History of the United States", Author = "Howard Zinn", Year = 1980, Price = 28.00M, Description = "American history from the perspective of ordinary people and marginalized groups." },
                    new { Title = "The Guns of August", Author = "Barbara W. Tuchman", Year = 1962, Price = 23.99M, Description = "The outbreak of World War I and the first month of fighting." },
                    new { Title = "1776", Author = "David McCullough", Year = 2005, Price = 25.50M, Description = "The pivotal year that decided America's fate in the Revolutionary War." },
                    new { Title = "The Rise and Fall of the Third Reich", Author = "William L. Shirer", Year = 1960, Price = 32.99M, Description = "A comprehensive history of Nazi Germany from its rise to its defeat." },
                    new { Title = "Team of Rivals", Author = "Doris Kearns Goodwin", Year = 2005, Price = 27.50M, Description = "Lincoln's political genius in appointing former opponents to his cabinet." },
                    new { Title = "The Silk Roads", Author = "Peter Frankopan", Year = 2015, Price = 24.99M, Description = "A new history of the world through the ancient trade routes." },
                    new { Title = "Band of Brothers", Author = "Stephen E. Ambrose", Year = 1992, Price = 21.99M, Description = "Easy Company's journey from training to D-Day and beyond." },
                    new { Title = "Homo Deus", Author = "Yuval Noah Harari", Year = 2016, Price = 25.99M, Description = "A brief history of tomorrow and humanity's future." },
                    new { Title = "The Devil Wears Prada", Author = "Lauren Weisberger", Year = 2003, Price = 14.99M, Description = "Behind the scenes of high fashion and corporate culture." },
                    new { Title = "Salt: A World History", Author = "Mark Kurlansky", Year = 2002, Price = 19.99M, Description = "How salt shaped civilizations throughout human history." },
                    new { Title = "The Wright Brothers", Author = "David McCullough", Year = 2015, Price = 22.50M, Description = "The story of the brothers who achieved the first powered flight." },
                    new { Title = "John Adams", Author = "David McCullough", Year = 2001, Price = 26.99M, Description = "The life of America's second president and his remarkable family." },
                    new { Title = "Alexander Hamilton", Author = "Ron Chernow", Year = 2004, Price = 27.99M, Description = "The biography that inspired the hit Broadway musical." },
                    new { Title = "The Immortal Life of Henrietta Lacks", Author = "Rebecca Skloot", Year = 2010, Price = 23.50M, Description = "How one woman's cells revolutionized medical science." },
                    new { Title = "In Cold Blood", Author = "Truman Capote", Year = 1966, Price = 15.99M, Description = "True crime masterpiece about a brutal murder in rural Kansas." },
                    new { Title = "The Devil in the White City", Author = "Erik Larson", Year = 2003, Price = 24.25M, Description = "Murder and architecture during the 1893 Chicago World's Fair." },
                    new { Title = "Undaunted Courage", Author = "Stephen E. Ambrose", Year = 1996, Price = 23.99M, Description = "Meriwether Lewis, Thomas Jefferson, and the opening of the American West." }
                };

                var historyCategoryId = categories.First(c => c.Name == "History").Id;
                foreach (var book in historyBooks)
                {
                    books.Add(new Book
                    {
                        Title = book.Title,
                        Author = book.Author,
                        CategoryId = historyCategoryId,
                        Price = book.Price,
                        PublishedYear = book.Year,
                        ISBN = GenerateISBN(),
                        ImageUrl = GenerateImageUrl(),
                        Stock = _random.Next(8, 20),
                        Description = book.Description
                    });
                }

                // BIOGRAPHY (15 books)
                var biographyBooks = new[]
                {
                    new { Title = "Steve Jobs", Author = "Walter Isaacson", Year = 2011, Price = 29.99M, Description = "The authorized biography of Apple's co-founder and visionary CEO." },
                    new { Title = "Long Walk to Freedom", Author = "Nelson Mandela", Year = 1994, Price = 26.50M, Description = "Mandela's autobiography from rural childhood to president of South Africa." },
                    new { Title = "The Autobiography of Malcolm X", Author = "Malcolm X & Alex Haley", Year = 1965, Price = 22.99M, Description = "The powerful life story of the influential civil rights leader." },
                    new { Title = "Einstein: His Life and Universe", Author = "Walter Isaacson", Year = 2007, Price = 28.00M, Description = "The definitive biography of the world's most famous scientist." },
                    new { Title = "Benjamin Franklin: An American Life", Author = "Walter Isaacson", Year = 2003, Price = 25.99M, Description = "The fascinating life of America's most accomplished founding father." },
                    new { Title = "Becoming", Author = "Michelle Obama", Year = 2018, Price = 32.50M, Description = "The former First Lady's deeply personal memoir of her journey." },
                    new { Title = "Churchill: A Life", Author = "Martin Gilbert", Year = 1991, Price = 31.50M, Description = "The comprehensive single-volume biography of Britain's wartime leader." },
                    new { Title = "Leonardo da Vinci", Author = "Walter Isaacson", Year = 2017, Price = 30.00M, Description = "The life and genius of the ultimate Renaissance man." },
                    new { Title = "Gandhi: An Autobiography", Author = "Mahatma Gandhi", Year = 1927, Price = 24.99M, Description = "Gandhi's own story of his experiments with truth and nonviolence." },
                    new { Title = "I Know Why the Caged Bird Sings", Author = "Maya Angelou", Year = 1969, Price = 18.50M, Description = "Angelou's powerful memoir of childhood in the segregated South." },
                    new { Title = "Open", Author = "Andre Agassi", Year = 2009, Price = 26.99M, Description = "The tennis champion's brutally honest autobiography." },
                    new { Title = "Kitchen Confidential", Author = "Anthony Bourdain", Year = 2000, Price = 21.99M, Description = "Adventures in the culinary underbelly by the late celebrity chef." },
                    new { Title = "Born a Crime", Author = "Trevor Noah", Year = 2016, Price = 23.50M, Description = "Stories from a South African childhood during apartheid." },
                    new { Title = "Educated", Author = "Tara Westover", Year = 2018, Price = 24.99M, Description = "A memoir about education, family, and the struggle for self-invention." },
                    new { Title = "When Breath Becomes Air", Author = "Paul Kalanithi", Year = 2016, Price = 22.00M, Description = "A neurosurgeon's reflection on life and death while facing terminal cancer." }
                };

                var biographyCategoryId = categories.First(c => c.Name == "Biography").Id;
                foreach (var book in biographyBooks)
                {
                    books.Add(new Book
                    {
                        Title = book.Title,
                        Author = book.Author,
                        CategoryId = biographyCategoryId,
                        Price = book.Price,
                        PublishedYear = book.Year,
                        ISBN = GenerateISBN(),
                        ImageUrl = GenerateImageUrl(),
                        Stock = _random.Next(10, 22),
                        Description = book.Description
                    });
                }

                // SCIENCE (20 books)
                var scienceBooks = new[]
                {
                    new { Title = "A Brief History of Time", Author = "Stephen Hawking", Year = 1988, Price = 18.99M, Description = "Cosmology and the nature of time explained for general readers." },
                    new { Title = "The Origin of Species", Author = "Charles Darwin", Year = 1859, Price = 16.50M, Description = "The foundational work on evolution and natural selection." },
                    new { Title = "Cosmos", Author = "Carl Sagan", Year = 1980, Price = 20.99M, Description = "A personal journey through space, time, and the universe." },
                    new { Title = "The Selfish Gene", Author = "Richard Dawkins", Year = 1976, Price = 17.99M, Description = "How genes shape behavior and evolution from a gene's perspective." },
                    new { Title = "The Double Helix", Author = "James Watson", Year = 1968, Price = 15.50M, Description = "Watson's personal account of discovering DNA's structure." },
                    new { Title = "Silent Spring", Author = "Rachel Carson", Year = 1962, Price = 14.99M, Description = "The environmental classic that launched the modern environmental movement." },
                    new { Title = "The Structure of Scientific Revolutions", Author = "Thomas S. Kuhn", Year = 1962, Price = 22.50M, Description = "How scientific paradigms shift and change over time." },
                    new { Title = "Gödel, Escher, Bach", Author = "Douglas Hofstadter", Year = 1979, Price = 24.99M, Description = "An exploration of consciousness, mathematics, and artificial intelligence." },
                    new { Title = "The Elegant Universe", Author = "Brian Greene", Year = 1999, Price = 19.99M, Description = "String theory and the quest for the theory of everything." },
                    new { Title = "The Code Breaker", Author = "Walter Isaacson", Year = 2021, Price = 28.99M, Description = "Jennifer Doudna and the future of the human race through gene editing." },
                    new { Title = "Astrophysics for People in a Hurry", Author = "Neil deGrasse Tyson", Year = 2017, Price = 16.99M, Description = "The essential cosmic perspective on the universe." },
                    new { Title = "The Gene", Author = "Siddhartha Mukherjee", Year = 2016, Price = 26.50M, Description = "An intimate history of genetics and its impact on humanity." },
                    new { Title = "The Emperor of All Maladies", Author = "Siddhartha Mukherjee", Year = 2010, Price = 25.99M, Description = "A biography of cancer and the ongoing war against it." },
                    new { Title = "Lab Girl", Author = "Hope Jahren", Year = 2016, Price = 18.75M, Description = "A scientist's love letter to working in the lab and science." },
                    new { Title = "The Hidden Nature of Reality", Author = "Brian Greene", Year = 2020, Price = 21.50M, Description = "Parallel universes and the deep laws of the cosmos." },
                    new { Title = "Sapiens", Author = "Yuval Noah Harari", Year = 2014, Price = 26.99M, Description = "A brief history of humankind from cognitive revolution to present." },
                    new { Title = "The Quantum Universe", Author = "Brian Cox", Year = 2011, Price = 20.25M, Description = "Why everything that can happen does happen in quantum physics." },
                    new { Title = "Mary Roach's Packing for Mars", Author = "Mary Roach", Year = 2010, Price = 17.50M, Description = "The curious science of life in the void of space." },
                    new { Title = "The Immortal Life of Henrietta Lacks", Author = "Rebecca Skloot", Year = 2010, Price = 23.50M, Description = "How one woman's cells revolutionized medical science." },
                    new { Title = "Freakonomics", Author = "Steven D. Levitt", Year = 2005, Price = 19.75M, Description = "A rogue economist explores the hidden side of everything." }
                };

                var scienceCategoryId = categories.First(c => c.Name == "Science").Id;
                foreach (var book in scienceBooks)
                {
                    books.Add(new Book
                    {
                        Title = book.Title,
                        Author = book.Author,
                        CategoryId = scienceCategoryId,
                        Price = book.Price,
                        PublishedYear = book.Year,
                        ISBN = GenerateISBN(),
                        ImageUrl = GenerateImageUrl(),
                        Stock = _random.Next(8, 18),
                        Description = book.Description
                    });
                }

                // ROMANCE (15 books)
                var romanceBooks = new[]
                {
                    new { Title = "Pride and Prejudice", Author = "Jane Austen", Year = 1813, Price = 12.99M, Description = "The timeless romance between Elizabeth Bennet and Mr. Darcy." },
                    new { Title = "Jane Eyre", Author = "Charlotte Brontë", Year = 1847, Price = 13.50M, Description = "An orphaned governess finds love with her brooding employer." },
                    new { Title = "Outlander", Author = "Diana Gabaldon", Year = 1991, Price = 16.99M, Description = "Time travel romance between a WWII nurse and 18th century Highlander." },
                    new { Title = "The Notebook", Author = "Nicholas Sparks", Year = 1996, Price = 14.50M, Description = "An enduring love story that spans decades." },
                    new { Title = "Me Before You", Author = "Jojo Moyes", Year = 2012, Price = 15.75M, Description = "An unlikely romance that changes two lives forever." },
                    new { Title = "The Kiss Quotient", Author = "Helen Hoang", Year = 2018, Price = 13.99M, Description = "A woman with Asperger's hires a male escort to teach her about intimacy." },
                    new { Title = "Red, White & Royal Blue", Author = "Casey McQuiston", Year = 2019, Price = 16.25M, Description = "The First Son falls for the Prince of Wales in this LGBTQ+ romance." },
                    new { Title = "The Hating Game", Author = "Sally Thorne", Year = 2016, Price = 14.99M, Description = "Office enemies discover there's a thin line between love and hate." },
                    new { Title = "It Ends with Us", Author = "Colleen Hoover", Year = 2016, Price = 15.50M, Description = "A powerful story about love, resilience, and difficult choices." },
                    new { Title = "Beach Read", Author = "Emily Henry", Year = 2020, Price = 14.75M, Description = "Two rival writers challenge each other to step outside their comfort zones." },
                    new { Title = "The Seven Husbands of Evelyn Hugo", Author = "Taylor Jenkins Reid", Year = 2017, Price = 16.50M, Description = "A reclusive Hollywood icon finally tells her life story." },
                    new { Title = "Eleanor Oliphant Is Completely Fine", Author = "Gail Honeyman", Year = 2017, Price = 15.25M, Description = "A socially awkward woman's journey toward connection and healing." },
                    new { Title = "The Time Traveler's Wife", Author = "Audrey Niffenegger", Year = 2003, Price = 17.99M, Description = "A love story complicated by involuntary time travel." },
                    new { Title = "Gone Girl", Author = "Gillian Flynn", Year = 2012, Price = 15.99M, Description = "A twisted psychological thriller about a marriage gone wrong." },
                    new { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Year = 1925, Price = 13.99M, Description = "Jay Gatsby's obsessive love for Daisy Buchanan in the Jazz Age." }
                };

                var romanceCategoryId = categories.First(c => c.Name == "Romance").Id;
                foreach (var book in romanceBooks)
                {
                    books.Add(new Book
                    {
                        Title = book.Title,
                        Author = book.Author,
                        CategoryId = romanceCategoryId,
                        Price = book.Price,
                        PublishedYear = book.Year,
                        ISBN = GenerateISBN(),
                        ImageUrl = GenerateImageUrl(),
                        Stock = _random.Next(12, 25),
                        Description = book.Description
                    });
                }

                // YOUNG ADULT (15 books)
                var yaBooks = new[]
                {
                    new { Title = "Harry Potter and the Philosopher's Stone", Author = "J.K. Rowling", Year = 1997, Price = 12.99M, Description = "A young wizard discovers his magical heritage at Hogwarts." },
                    new { Title = "The Hunger Games", Author = "Suzanne Collins", Year = 2008, Price = 14.50M, Description = "Katniss volunteers for deadly televised games in dystopian Panem." },
                    new { Title = "The Fault in Our Stars", Author = "John Green", Year = 2012, Price = 13.99M, Description = "Two teens with cancer find love and meaning in each other." },
                    new { Title = "Divergent", Author = "Veronica Roth", Year = 2011, Price = 15.25M, Description = "Tris discovers she's Divergent in a faction-divided dystopian society." },
                    new { Title = "The Maze Runner", Author = "James Dashner", Year = 2009, Price = 14.75M, Description = "Thomas wakes up in a maze with no memory of his past." },
                    new { Title = "Twilight", Author = "Stephenie Meyer", Year = 2005, Price = 13.50M, Description = "Bella falls in love with vampire Edward Cullen in Forks, Washington." },
                    new { Title = "The Perks of Being a Wallflower", Author = "Stephen Chbosky", Year = 1999, Price = 12.75M, Description = "Charlie navigates high school and adolescence through letters." },
                    new { Title = "Looking for Alaska", Author = "John Green", Year = 2005, Price = 13.25M, Description = "Miles searches for meaning at boarding school and meets Alaska." },
                    new { Title = "Thirteen Reasons Why", Author = "Jay Asher", Year = 2007, Price = 14.99M, Description = "Clay receives tapes explaining why Hannah chose to end her life." },
                    new { Title = "The Book Thief", Author = "Markus Zusak", Year = 2005, Price = 16.00M, Description = "Death narrates Liesel's story during Nazi Germany." },
                    new { Title = "Miss Peregrine's Home for Peculiar Children", Author = "Ransom Riggs", Year = 2011, Price = 15.50M, Description = "Jacob discovers a school for children with supernatural abilities." },
                    new { Title = "The Giver", Author = "Lois Lowry", Year = 1993, Price = 11.99M, Description = "Jonas discovers the truth about his seemingly perfect society." },
                    new { Title = "Wonder", Author = "R.J. Palacio", Year = 2012, Price = 13.75M, Description = "Auggie, born with facial differences, starts mainstream school." },
                    new { Title = "Eleanor & Park", Author = "Rainbow Rowell", Year = 2013, Price = 14.25M, Description = "Two misfit teens fall in love on the school bus in 1986." },
                    new { Title = "The Outsiders", Author = "S.E. Hinton", Year = 1967, Price = 12.50M, Description = "Ponyboy Curtis navigates class conflict and brotherhood." }
                };

                var yaCategoryId = categories.First(c => c.Name == "Young Adult").Id;
                foreach (var book in yaBooks)
                {
                    books.Add(new Book
                    {
                        Title = book.Title,
                        Author = book.Author,
                        CategoryId = yaCategoryId,
                        Price = book.Price,
                        PublishedYear = book.Year,
                        ISBN = GenerateISBN(),
                        ImageUrl = GenerateImageUrl(),
                        Stock = _random.Next(15, 30),
                        Description = book.Description
                    });
                }

                // SELF-HELP (20 books)
                var selfHelpBooks = new[]
                {
                    new { Title = "How to Win Friends and Influence People", Author = "Dale Carnegie", Year = 1936, Price = 16.99M, Description = "Timeless advice on human relations and social skills." },
                    new { Title = "The 7 Habits of Highly Effective People", Author = "Stephen R. Covey", Year = 1989, Price = 22.50M, Description = "Character-based principles for personal and professional effectiveness." },
                    new { Title = "Atomic Habits", Author = "James Clear", Year = 2018, Price = 24.50M, Description = "Tiny changes that create remarkable results." },
                    new { Title = "The Power of Now", Author = "Eckhart Tolle", Year = 1997, Price = 18.99M, Description = "A guide to spiritual enlightenment through present-moment awareness." },
                    new { Title = "Think and Grow Rich", Author = "Napoleon Hill", Year = 1937, Price = 18.99M, Description = "The philosophy of success and achievement." },
                    new { Title = "The Four Agreements", Author = "Don Miguel Ruiz", Year = 1997, Price = 14.95M, Description = "Ancient Toltec wisdom for personal freedom." },
                    new { Title = "Daring Greatly", Author = "Brené Brown", Year = 2012, Price = 20.50M, Description = "The courage to be vulnerable and embrace imperfection." },
                    new { Title = "The Subtle Art of Not Giving a F*ck", Author = "Mark Manson", Year = 2016, Price = 19.99M, Description = "A counterintuitive approach to living a good life." },
                    new { Title = "12 Rules for Life", Author = "Jordan B. Peterson", Year = 2018, Price = 23.50M, Description = "An antidote to chaos through personal responsibility." },
                    new { Title = "Mindset", Author = "Carol S. Dweck", Year = 2006, Price = 20.99M, Description = "The new psychology of success through growth mindset." },
                    new { Title = "The Magic of Thinking Big", Author = "David J. Schwartz", Year = 1959, Price = 17.50M, Description = "How to achieve more by believing in bigger possibilities." },
                    new { Title = "The Miracle Morning", Author = "Hal Elrod", Year = 2012, Price = 16.75M, Description = "Transform your life before 8AM with morning routines." },
                    new { Title = "You Are a Badass", Author = "Jen Sincero", Year = 2013, Price = 18.25M, Description = "How to stop doubting your greatness and start living boldly." },
                    new { Title = "The Gifts of Imperfection", Author = "Brené Brown", Year = 2010, Price = 19.50M, Description = "Let go of perfectionism and embrace authentic living." },
                    new { Title = "Big Magic", Author = "Elizabeth Gilbert", Year = 2015, Price = 17.99M, Description = "Creative living beyond fear and unleashing creativity." },
                    new { Title = "The Happiness Project", Author = "Gretchen Rubin", Year = 2009, Price = 18.75M, Description = "One woman's year-long quest for happiness." },
                    new { Title = "Rising Strong", Author = "Brené Brown", Year = 2015, Price = 21.25M, Description = "How the ability to reset transforms the way we live and work." },
                    new { Title = "The Alchemist", Author = "Paulo Coelho", Year = 1988, Price = 15.99M, Description = "A shepherd's journey to find his personal legend." },
                    new { Title = "Man's Search for Meaning", Author = "Viktor E. Frankl", Year = 1946, Price = 18.00M, Description = "Finding purpose and meaning even in suffering." },
                    new { Title = "The Power of Positive Thinking", Author = "Norman Vincent Peale", Year = 1952, Price = 16.50M, Description = "How faith and optimism can transform your life." }
                };

                var selfHelpCategoryId = categories.First(c => c.Name == "Self-Help").Id;
                foreach (var book in selfHelpBooks)
                {
                    books.Add(new Book
                    {
                        Title = book.Title,
                        Author = book.Author,
                        CategoryId = selfHelpCategoryId,
                        Price = book.Price,
                        PublishedYear = book.Year,
                        ISBN = GenerateISBN(),
                        ImageUrl = GenerateImageUrl(),
                        Stock = _random.Next(18, 35),
                        Description = book.Description
                    });
                }

                await dbcontext.Books.AddRangeAsync(books);
                await dbcontext.SaveChangesAsync();
            }
        }
    }
}