using System.Diagnostics.CodeAnalysis;

namespace Domains
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime СompletionDate { get; set; }
        public bool Complited { get; set; }
        [AllowNull]
        public string Description { get; set; }
        public virtual List<Subtask> Subtasks { get; set; } = new List<Subtask>();

    }
}
