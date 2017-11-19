﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESR_Project.Models.SocialMedia.SocialMedia
{
    public class posts
    {
        public dynamic jsonObj { get; set; }
        public posts(dynamic json)
        {

            jsonObj = json;
            if (jsonObj != null)
            {
                id = jsonObj.id;
                name = jsonObj.name;

                if (jsonObj.groups!= null)
                {
                    feed = new feed(jsonObj.groups.data[0].feed);
                }
            }

        }

        public string id { get; set; }
        public string name { get; set; }

        public feed feed { get; set; }

    }

    public class feed
    {
        public dynamic jsonObj { get; set; }
        public feed(dynamic json)
        {
            jsonObj = json;

            if (jsonObj != null)
            {
                if (jsonObj.data != null)
                {
                    data = new post[jsonObj.data.Length];
                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i] = new post(jsonObj.data[i]);
                    }
                }
            }
        }

        public post[] data { get; set; }
        


    }

    public class post
    {
        public dynamic jsonObj { get; set; }
        public post(dynamic json)
        {
            jsonObj = json;

            if (jsonObj != null)
            {
                id = jsonObj.id;
                story = jsonObj.story;
                created_time = jsonObj.created_time;
                message = jsonObj.message;
                if (jsonObj.from != null)
                {
                    From = new From(jsonObj.From);
                }
                if (jsonObj.likes != null)
                {
                    likes = new likes(jsonObj.likes);


                }
                if (jsonObj.attachments != null)
                {
                    attachments = new attachments(jsonObj.attachments);


                }
                if (jsonObj.comments != null)
                {
                    comments = new comments(jsonObj.comments);
                }
                



            }
        }

        public string message { get; set; }
        public  From From{get;set;}

        public string id { get; set; }
        public string story { get; set; }
        public string created_time { get; set; }

        public likes likes { get; set; }
        public attachments attachments { get; set; }
        public comments comments { get; set; }

    }

    public class From
    {
        public dynamic jsonObj { get; set; }
        public From(dynamic json)
        {
            jsonObj = json;

            if (jsonObj != null)
            {
                id = jsonObj.id;
                name = jsonObj.name;
                


            }
        }

        public string name { get; set; }
        public string id { get; set; }
        public Profile profile { set; get; }
       

    }

    public class Profile
    {
        public dynamic jsonObj { get; set; }
        public Profile(dynamic json)
        {
            jsonObj = json;

            if (jsonObj != null)
             {
                profile = jsonObj.picture.data.url;
             }
         } 
        public string profile { get; set; }

    }

    public class likes
    {
        public dynamic jsonObj { get; set; }
        public likes(dynamic json)
        {
            jsonObj = json;

            if (jsonObj != null)
            {
                if (jsonObj.data != null)
                {
                    data = new like[jsonObj.data.Length];
                        for (int i = 0; i < data.Length; i++)
                    {
                        data[i] = new like(jsonObj.data[i]);
                    }
                }
            }
        }


        public like[] data { get; set; }
    }

    public class like
    {
        public dynamic jsonObj { get; set; }
        public like(dynamic json)
        {
            jsonObj = json;

           if (jsonObj != null)
            {
                id = jsonObj.id;
                name = jsonObj.name;
                pic = jsonObj.pic;
            }
        }
        public string pic { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }

    public class attachments
    {
        public dynamic jsonObj { get; set; }
        public attachments(dynamic json)
        {
            jsonObj = json;

            if (jsonObj != null)
            {
                if (jsonObj.data != null)
                {
                    data = new attachment[jsonObj.data.Length];
                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i] = new attachment(jsonObj.data[i]);
                    }
                }
            }
        }

       

        public attachment[] data { get; set; }
    }

    public class comments
    {
        public dynamic jsonObj { get; set; }
        public comments(dynamic json)
        {
            jsonObj = json;
             

            if (jsonObj != null)
            {
                if (jsonObj.data != null)
                {
                    count = jsonObj.data.Length;
                    data = new comment[jsonObj.data.Length];
                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i] = new comment(jsonObj.data[i]);
                        
                    }
                }
                
            }
        }
        public  int count{get;set;}

            public comment[] data { get; set; }
        }

        public class comment
        {
            public dynamic jsonObj { get; set; }
            public comment(dynamic json)
            {
                jsonObj = json;

                if (jsonObj != null)
                {
                    message = jsonObj.message;
                    id = jsonObj.id;
                created_time = jsonObj.created_time;
                    if (jsonObj.from != null)
                    {
                        from=new from(jsonObj.from);
                    }

                }
            }
            public string message { get; set; }
            public string id { get; set; }
            public from from { get; set; }
            public string created_time { get; set; }
           


        }

        public class from
        {
            public dynamic jsonObj { get; set; }
            public from(dynamic json)
            {
                jsonObj = json;

                if (jsonObj != null)
                {
                    name = jsonObj.name;
                    id = jsonObj.id;
                    

                }
            }
            
            public string name{ get; set; }
            public string id { get; set; }


        }

        public class attachment
    {

        public dynamic jsonObj { get; set; }
        public attachment(dynamic json)
        {
            jsonObj = json;

            if (jsonObj != null)
            {
                title = jsonObj.title;
                description = jsonObj.description;
                type = jsonObj.type;
                url = jsonObj.url;

                if (jsonObj.media != null)
                {
                    media = new media(jsonObj.media);
                }

                if (jsonObj.target != null)
                {
                    target = new target(jsonObj.target);
                }
            }
        }
        public string description { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string url { get; set; }

        public media media { get; set; }

        public target target{ get; set; }
    }

    public class media
    {
        public dynamic jsonObj { get; set; }
        public media(dynamic json)
        {
            jsonObj = json;

            if (jsonObj != null)
            {
                if (jsonObj.image != null)
                {
                    image= new image(jsonObj.image);
                }
            }
        }

        public image image { get; set; }

    }

    public class image
    {
        public dynamic jsonObj { get; set; }
        public image(dynamic json)
        {
            jsonObj = json;

            if (jsonObj != null)
            {
                height = jsonObj.height;
                width = jsonObj.width;
                src = jsonObj.src;

            }
        }
        public int height { get; set; }
        public int width { get; set; }
        public string src { get; set; }


    }

    public class target
    {

        public dynamic jsonObj { get; set; }
        public target(dynamic json)
        {
            jsonObj = json;

            if (jsonObj != null)
            {
                id = jsonObj.id;
                url = jsonObj.url;
               
            }
        }
        public string id { get; set; }
        public string url { get; set; }


    }




}