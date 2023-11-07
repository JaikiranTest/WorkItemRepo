using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace WorkItem.Task.Models
{
    [ExcludeFromCodeCoverage]
    public partial class Task
    {
        [Key]
        [DisplayName("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Title")]
        [Required(ErrorMessage = "Field 'Title' cannot be empty.")]
        public string? Title { get; set; }

        [DisplayName("Assigned To")]
        [Required(ErrorMessage = "Field 'Assigned To' cannot be empty.")]
        [EmailAddress(ErrorMessage = "Field 'Assign To' is not a valid e-mail address")]
        public string? Assigned { get; set; }

        [DisplayName("Status")]
        public int? StatusId { get; set; }

        [DisplayName("Discription")]
        [DataType(DataType.MultilineText)]
        public string? Discription { get; set; }

        [DisplayName("Comment")]
        [DataType(DataType.MultilineText)]
        public string? Comments { get; set; }

        [JsonIgnore]
        public virtual TaskStatus? Status { get; set; }
    }
}
