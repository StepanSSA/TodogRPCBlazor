namespace BlazorTodoClient.Models
{
	public class DropItem
	{
		public string id { get; set; }
		public string Name { get; init; }
		public string Selector { get; set; }
		public string Description { get; set; }
		public DateTime? CompletionDate { get; set; }
		public DateTime? CreationDate { get; set; }
		public bool Complited { get; set; }
		public List<SubtaskMessage> Subtasks { get; set; } = new List<SubtaskMessage>();
	}
}
