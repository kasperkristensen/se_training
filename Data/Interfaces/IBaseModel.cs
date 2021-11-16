using System;

namespace se_training.Data
{
    public interface IBaseModel
    {
        int Id { get; set; }

        DateTime CreatedAt { get; set; }

        DateTime UpdatedAt { get; set; }
    }
}