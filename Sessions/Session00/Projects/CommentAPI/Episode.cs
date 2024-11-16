namespace C01_CommentAPI
{
    public class Episode
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime ReleaseDateTime { get; set; }
        public string Duration { get; set; }


        public Episode(int id, string title, string description, string duration, string image, DateTime releaseDateTime)
        {
            Id = id;
            Title = title;
            Description = description;
            Duration = duration;
            Image = image;
            ReleaseDateTime = releaseDateTime;
        }
    }
}
