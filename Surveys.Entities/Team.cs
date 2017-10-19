namespace Surveys.Entities
{
    [System.Diagnostics.DebuggerDisplay("{Name}")]
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public byte[] Logo { get; set; }
    }
}