using System;
using System.Collections.Generic;

namespace ProgrammingLanguages.ProgrammingLanguageModels;

public partial class LanguageFeature
{
    public int FeatureId { get; set; }

    public string FeatureName { get; set; } = null!;

    public string? Description { get; set; }

    public string? F1 { get; set; }

    public string? F2 { get; set; }

    public string? F3 { get; set; }

    public virtual ICollection<ProgrammingLanguageFeature> ProgrammingLanguageFeatures { get; set; } = new List<ProgrammingLanguageFeature>();
}
