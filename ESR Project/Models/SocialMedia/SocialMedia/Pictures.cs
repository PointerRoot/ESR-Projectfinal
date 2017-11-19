using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESR_Project.Models.SocialMedia.SocialMedia
{
    public class Pictures
    {
        
            public Pictures(List<string> pictures,List<string> comments)
            {
                 picture = pictures;
                 comments_pictures = comments;

            }
        public List<string> picture { get; set; }
        public List<string> comments_pictures { get; set; }

        
    }
}