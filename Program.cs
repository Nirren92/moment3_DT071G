
using moment3.models.Post;

using System;
using System.IO;
using System.Text.Json;


namespace moment3;

class Program
{

    static List<Post> postsArray = new List<Post>();

    static void Main(string[] args)
    { 

        //lägger en try för att fånga upp eventuella fel som uppstår under init funktionen. 
        try
        {
            initData();

            //loopar om programmet och lägger en till try som fångar upp fel som sker inder körning. 
            while(true)
            {

                try
                {                
                    //skirver ut alla tillgänliga poster efter att init körs men även när programmet startar om för att alltid visa de senaste. 
                    PrintPost();
                    //visar val för användaren
                    Console.WriteLine("1 = addera post. 2 = ta bort");
                    int inputData = int.Parse(Console.ReadLine());
                    if(inputData == 1)
                    {
                        addPost();
                        
                    }
                    else if(inputData == 2)
                    {
                        Console.WriteLine("skriv index som du vill ta bort");
                        DeletePost(int.Parse(Console.ReadLine()));
                    }
                    else
                    {
                        Console.WriteLine("felaktik indata");
                    }
            
                    }
                
                 catch (Exception ex)
                {
                    Console.WriteLine("felaktik indata"+ex.Message);
                }
            }

        }
            
            catch (Exception ex)
            {
                Console.WriteLine("felaktik indata"+ex.Message);
            }
    }

    //Hämtar från tempfil om den finns. 
    public static void initData()
    {
        //kontroll att filen finns 
        if(File.Exists(Path.GetTempPath()+"posts.json"))
        {
            //hämtar lokation för temp filen. 
            string initData = File.ReadAllText(Path.GetTempPath()+"posts.json");

            //gör om data i json till objekt av Post
            List<Post> initPosts = JsonSerializer.Deserialize<List<Post>>(initData);
            if (initPosts !=null)
            {
                postsArray.AddRange(initPosts);
               
            }

        }       
    } 

    //sparar i en tempfil på datorn
    public static void savePosts()
    {
        string tempFile = Path.GetTempPath()+"posts.json";
        //gör om det till json
        string jsonData = JsonSerializer.Serialize(postsArray);
        //skriver till tempfil
        File.WriteAllText(tempFile, jsonData);
    }


    public static void addPost()
    {
        //hämtar indata från användaren
        Console.WriteLine("Skriv ditt namn");
        string Name = Console.ReadLine();
        Console.WriteLine("Skriv din text");
        string text = Console.ReadLine();
        

        //kontroll att båda string inngehåller nåt
        if (text.Length > 0 && Name.Length > 0) 
        {
            postsArray.Add(new Post(Name, text));
            savePosts();
        }

        else
        {
            Console.WriteLine("Säkerställ att det står data i båda input");
        }
    }

    //radera objekt i listan
    public static void DeletePost(int index)
     {
        postsArray.RemoveAt(index);
        savePosts();
     }
     
    //funktion som skriver ut alla objekt i listan
    public static void PrintPost()
    {
        if(postsArray.Count > 0)   
        {
        Console.WriteLine("aktuella poster", postsArray.Count);
            int i = 0;
            foreach (var post in postsArray)
            {
                Console.WriteLine($"{i} - Namn: {post.Name} - {post.Text}");
                i++;
            }
        }
    }


}
