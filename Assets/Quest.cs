public class Quest
{
    public int Id { 
        get; 
        set; 
    }
    public string title { get; set; }
    public string shortDescription { get; set; }
    public string longDescription { get; set; }

    public Quest()
    {
        Id = 0;
    }
}