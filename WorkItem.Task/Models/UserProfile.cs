using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WorkItem.Task.Models
{
    [ExcludeFromCodeCoverage]
    public partial class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? EmailId { get; set; }
    }
}
