using System;

namespace se_training.Data
{
    public interface IBaseModel
    {
        int Id { get; init; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}