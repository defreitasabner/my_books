using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models;

public class Author
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    public virtual ICollection<Book> Books { get; set; }
}