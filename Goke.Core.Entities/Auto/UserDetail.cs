//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Goke.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    // using Microsoft.EntityFrameworkCore;
    
    public partial class UserDetail : NameEntity
    {
        public UserDetail() : base()
        {
            this.Cards = new HashSet<Card>();
    		Initialize();
        }
    	partial void Initialize();
    
        [Required(ErrorMessage = "The Email is a mandatory Field.")]
    	[EmailAddress]/* [DataType(DataType.EmailAddress)] */
    	[Display(Name = "Email")]
    	public string Email { get; set; }
        [Display(Name = "Location")]
    	public string? Location { get; set; }
        [ForeignKey("Person")]
    	[Display(Name = "Person")]
    	public System.Guid? PersonId { get; set; }
    
        public virtual Person Person { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        
        public override string ToRecord()
        {
            var str = $@"UserDetail {{ 
                {base.ToRecord()},
                Email = {Email}, 
                Location = {Location}, 
                PersonId = {PersonId}, 
                Person = {Person.ToRecord()},
                Cards = Count[{Cards?.Count}]|{Cards?.ToString()},
            ";
         
            OnToRecord(ref str);
    
            str += "}}";
    
            return str;
        }
    
        partial void OnToRecord(ref string str);
            
    
        public override string ToJson()
        {
            OnToJson();
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
        partial void OnToJson();   
    
    }
}