//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Goke.Web.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    // using Microsoft.EntityFrameworkCore;
    
    public partial class Blog : BaseEntity
    {
        public Blog() : base()
        {
            this.BlogPosts = new HashSet<BlogPost>();
    		Initialize();
        }
    	partial void Initialize();
    
        [Required(ErrorMessage = "The Url is a mandatory Field.")]
    	[Display(Name = "Url")]
    	public string Url { get; set; }
    
        public virtual ICollection<BlogPost> BlogPosts { get; set; }
        
        public new string ToRecord()
        {
            var str = $@"Blog {{ 
                {base.ToRecord()},
                Url = {Url}, 
                BlogPosts = Count[{BlogPosts?.Count}]|{BlogPosts?.ToString()},
            ";
         
            OnToRecord(ref str);
    
            str += "}}";
    
            return str;
        }
    
        partial void OnToRecord(ref string str);
            
    
        string ToJson()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    
    
    }
}