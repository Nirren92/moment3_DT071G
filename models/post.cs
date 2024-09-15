using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moment3.models.Post
{
    public class Post
    {
       public string Name{get;set;}
       public string Text{get;set;}

     public Post(string name,string text )
     {
        Name = name;
        Text = text;
     }

    }
}