using System;
using System.Collections.Generic;

namespace ProgrammingLanguages.Models;

public partial class LearningProgress
{
    public int ProgressId { get; set; }

    public int UserId { get; set; }

    public int LanguageId { get; set; }

    public string? ProficiencyLevel { get; set; }

    public int? LearningHours { get; set; }

    public string? Certification { get; set; }

    public DateTime? LastUpdated { get; set; }

    public string? Lp1 { get; set; }

    public string? Lp2 { get; set; }

    public string? Lp3 { get; set; }

    public virtual ProgrammingLanguage Language { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
