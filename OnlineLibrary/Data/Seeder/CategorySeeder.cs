using OnlineLibrary.Model;

namespace OnlineLibrary.Data.Seeder
{
    public class CategorySeeder
    {
        public static async Task SeedCategoriesAsync(OBDbcontext dbcontext)
        {
            if (!dbcontext.Categories.Any())
            {
                var categories = new List<Category>
                {
                    // Main Fiction Categories
                    new Category { Name = "Fiction", Description = "Literary works of imagination including novels, short stories, and narratives" },
                    new Category { Name = "Science Fiction", Description = "Speculative fiction dealing with futuristic concepts and advanced technology" },
                    new Category { Name = "Fantasy", Description = "Fiction featuring magical or supernatural elements" },
                    new Category { Name = "Mystery & Thriller", Description = "Suspenseful stories involving crime, detection, and intrigue" },
                    new Category { Name = "Romance", Description = "Stories focusing on love relationships and romantic entanglements" },
                    new Category { Name = "Historical Fiction", Description = "Fiction set in the past, recreating historical periods" },
                    new Category { Name = "Horror", Description = "Fiction intended to frighten, unsettle, or create suspense" },
                    new Category { Name = "Adventure", Description = "Action-packed stories featuring exciting journeys and quests" },
                    new Category { Name = "Young Adult", Description = "Fiction targeted at teenage and young adult readers" },
                    new Category { Name = "Children's Books", Description = "Literature written specifically for children of various ages" },

                    // Non-Fiction Categories
                    new Category { Name = "Biography", Description = "Life stories of real people written by others or themselves" },
                    new Category { Name = "History", Description = "Books about past events, civilizations, and historical analysis" },
                    new Category { Name = "Science", Description = "Books exploring scientific concepts, discoveries, and theories" },
                    new Category { Name = "Medicine & Health", Description = "Medical knowledge, health advice, and wellness information" },
                    new Category { Name = "Psychology", Description = "Study of mind, behavior, and mental processes" },
                    new Category { Name = "Philosophy", Description = "Exploration of fundamental questions about existence and knowledge" },
                    new Category { Name = "Religion & Spirituality", Description = "Religious texts, spiritual guidance, and theological studies" },
                    new Category { Name = "Self-Help", Description = "Personal development and improvement guidance" },

                    // Business & Professional
                    new Category { Name = "Business & Finance", Description = "Business strategies, financial advice, and economic principles" },
                    new Category { Name = "Entrepreneurship", Description = "Starting and managing businesses, startup culture" },
                    new Category { Name = "Leadership & Management", Description = "Leading teams, organizational behavior, and management skills" },
                    new Category { Name = "Marketing & Sales", Description = "Promoting products, sales techniques, and market analysis" },
                    new Category { Name = "Economics", Description = "Economic theory, market analysis, and financial systems" },
                    new Category { Name = "Investing", Description = "Investment strategies, market analysis, and wealth building" },

                    // Technology & Computing
                    new Category { Name = "Computer & Programming", Description = "Software development, programming languages, and computer science" },
                    new Category { Name = "Web Development", Description = "Creating websites, web applications, and online platforms" },
                    new Category { Name = "Data Science", Description = "Data analysis, machine learning, and statistical methods" },
                    new Category { Name = "Artificial Intelligence", Description = "AI development, machine learning, and intelligent systems" },
                    new Category { Name = "Cybersecurity", Description = "Information security, network protection, and digital privacy" },
                    new Category { Name = "Software Engineering", Description = "Software design, development methodologies, and system architecture" },

                    // Arts & Culture
                    new Category { Name = "Art & Design", Description = "Visual arts, design principles, and creative expression" },
                    new Category { Name = "Music", Description = "Musical theory, composition, and music history" },
                    new Category { Name = "Photography", Description = "Photographic techniques, equipment, and artistic vision" },
                    new Category { Name = "Film & Theater", Description = "Cinema studies, theatrical arts, and performance" },
                    new Category { Name = "Literature & Criticism", Description = "Literary analysis, criticism, and scholarly interpretation" },

                    // Lifestyle & Practical
                    new Category { Name = "Cooking", Description = "Recipes, culinary techniques, and food culture" },
                    new Category { Name = "Travel", Description = "Travel guides, cultural exploration, and adventure stories" },
                    new Category { Name = "Sports & Fitness", Description = "Physical fitness, sports techniques, and athletic performance" },
                    new Category { Name = "Home & Garden", Description = "Home improvement, gardening, and domestic life" },
                    new Category { Name = "Parenting", Description = "Child-rearing advice, family dynamics, and parenting strategies" },
                    new Category { Name = "Relationships", Description = "Relationship advice, communication, and social skills" },

                    // Academic & Educational
                    new Category { Name = "Education", Description = "Teaching methods, learning theories, and educational systems" },
                    new Category { Name = "Mathematics", Description = "Mathematical concepts, theories, and applications" },
                    new Category { Name = "Physics", Description = "Physical laws, quantum mechanics, and universe studies" },
                    new Category { Name = "Chemistry", Description = "Chemical processes, molecular science, and laboratory techniques" },
                    new Category { Name = "Biology", Description = "Life sciences, genetics, and biological systems" },
                    new Category { Name = "Environmental Science", Description = "Ecology, climate change, and environmental protection" },
                    new Category { Name = "Engineering", Description = "Engineering principles, design, and technical innovation" },

                    // Social Sciences
                    new Category { Name = "Sociology", Description = "Society, social behavior, and cultural phenomena" },
                    new Category { Name = "Anthropology", Description = "Human cultures, evolution, and social development" },
                    new Category { Name = "Political Science", Description = "Government systems, politics, and civic engagement" },
                    new Category { Name = "Law", Description = "Legal systems, jurisprudence, and legal practice" },
                    new Category { Name = "Criminology", Description = "Crime studies, criminal behavior, and justice systems" },

                    // Reference & Language
                    new Category { Name = "Reference", Description = "Dictionaries, encyclopedias, and reference materials" },
                    new Category { Name = "Language Learning", Description = "Foreign language instruction and linguistic studies" },
                    new Category { Name = "Poetry", Description = "Poetic works, verse collections, and literary poetry" },
                    new Category { Name = "Essays", Description = "Essay collections, personal reflections, and opinion pieces" },
                    new Category { Name = "Humor", Description = "Comedy, satirical works, and humorous observations" }
                };

                await dbcontext.Categories.AddRangeAsync(categories);
                await dbcontext.SaveChangesAsync();
            }
        }
    }
}