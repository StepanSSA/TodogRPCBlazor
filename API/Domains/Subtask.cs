
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Domains
{
    public class Subtask
    {
        [NotNull]
        [Required]
        [Key]
        public Guid Id { get; set; }
        [NotNull]
        public string Name { get; set; } = null!;
        public bool Complited { get; set; }
        [NotNull]
        public Todo Todo { get; set; } = null!;
    }
}
