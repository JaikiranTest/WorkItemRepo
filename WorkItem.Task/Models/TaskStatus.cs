using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WorkItem.Task.Models
{
    [ExcludeFromCodeCoverage]
    public partial class TaskStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Status { get; set; }

        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
