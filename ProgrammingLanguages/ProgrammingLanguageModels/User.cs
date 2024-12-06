using System;
using System.Collections.Generic;

namespace ProgrammingLanguages.ProgrammingLanguageModels;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PasswordHash { get; set; }

    public DateTime? RegisterDate { get; set; }

    public string? Profile { get; set; }

    public string? Us1 { get; set; }

    public string? Us2 { get; set; }

    public string? Us3 { get; set; }

    public virtual ICollection<LearningProgress> LearningProgresses { get; set; } = new List<LearningProgress>();
}
