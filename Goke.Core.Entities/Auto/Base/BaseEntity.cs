using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goke.Core.Entities;

public partial class BaseEntity
{
    [ScaffoldColumn(false)]
    [Display(Name = "Last  By")]
    [MaxLength(50), StringLength(50, ErrorMessage = "The Last  By must be a string with a maximum length of 50 characters.")]
    public string? LastBy { get; set; }

    [ScaffoldColumn(false)]
    [DataType(DataType.Date)]
    [Display(Name = "Last  Date")]
    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy hh:mm:ss}")]
    public DateTime LastDate { get; set; } = DateTime.UtcNow;

    [ScaffoldColumn(false)]
    [Display(Name = "Added By")]
    [MaxLength(50), StringLength(50, ErrorMessage = "The Added By must be a string with a maximum length of 50 characters.")]
    public string? AddedBy { get; set; }

    [ScaffoldColumn(false)]
    [DataType(DataType.Date)]
    [Display(Name = "Added Date")]
    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy hh:mm:ss}")]
    public DateTime? AddedDate { get; set; }

    [ScaffoldColumn(false)]
    [Display(Name = "Edited By")]
    [MaxLength(50), StringLength(50, ErrorMessage = "The Edited By must be a string with a maximum length of 50 characters.")]
    public string? EditedBy { get; set; }

    [ScaffoldColumn(false)]
    [DataType(DataType.Date)]
    [Display(Name = "Edited Date")]
    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy hh:mm:ss}")]
    public DateTime? EditedDate { get; set; }

    [ScaffoldColumn(false)]
    [Timestamp]
    public byte[]? Version { get; set; }

}
