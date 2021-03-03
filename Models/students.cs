using System.ComponentModel.DataAnnotations;

namespace Back.Models
{ 
  public class students 
  { 
    [Key]
    public int id { get; set; }

    public string grade { get; set; }
  }
}