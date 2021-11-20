namespace se_training.Data
{
    public record CommentCreateDTO {
        public string Text { get; set; }
        public string UserId { get; set; }
        public int MaterialId { get; set; }
        public int? ParentId { get; set; }
    }
    public record CommentDTO : CommentCreateDTO
    {
        public int Id { get; set; }
    }


}