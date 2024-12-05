using System;
using System.Collections.Generic;

namespace ProgrammingLanguages.Models;

public partial class ProgrammingLanguageFeature
{
    public int LanguageId { get; set; }

    public int FeatureId { get; set; }

    public string? Pf1 { get; set; }

    public string? Pf2 { get; set; }

    public string? Pf3 { get; set; }

    public virtual LanguageFeature Feature { get; set; } = null!;

    public virtual ProgrammingLanguage Language { get; set; } = null!;
}
