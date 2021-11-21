namespace se_training.Data
{
    public record CommentCreateDTO
    {
        public string Text { get; init; }
        public string UserId { get; init; }
        public int MaterialId { get; init; }
        public int? ParentId { get; init; }

        public string UserName { get; init; }
    }
    public record CommentDTO : CommentCreateDTO
    {
        public int Id { get; init; }
    }
}