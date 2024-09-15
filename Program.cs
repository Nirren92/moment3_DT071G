
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
        while(true)
        {
            
            try
            {
                initData();
                PrintPost();
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

    //Hämtar från tempfil om den finns. 
    public static void initData()
    {
        string tempFile = Path.GetTempPath()+"posts.json";


        if(File.Exists(tempFile))
        {
            string initData = File.ReadAllText(tempFile);
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
        string jsonData = JsonSerializer.Serialize(postsArray);
        File.WriteAllText(tempFile, jsonData);
        Console.WriteLine(tempFile);
    }


    public static void addPost()
    {
        Console.WriteLine("Skriv ditt namn");
        string Name = Console.ReadLine();
        Console.WriteLine("Skriv din text");
        string text = Console.ReadLine();
        postsArray.Add(new Post(Name, text));
        savePosts();
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
        Console.WriteLine("aktuella poster");
            int i = 0;
            foreach (var post in postsArray)
            {
                Console.WriteLine($"{i} - Namn: {post.Name} - {post.Text}");
                i++;
            }
        }
    }


}
