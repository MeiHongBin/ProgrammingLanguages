using System;
using System.Collections.Generic;

namespace ProgrammingLanguages.ProgrammingLanguageModels;

public partial class LearningResource
{
    public int ResourceId { get; set; }

    public int LanguageId { get; set; }

    public string ResourceName { get; set; } = null!;

    public string? ResourceLink { get; set; }

    public string? ResourceType { get; set; }

    public virtual ProgrammingLanguage Language { get; set; } = null!;
}
