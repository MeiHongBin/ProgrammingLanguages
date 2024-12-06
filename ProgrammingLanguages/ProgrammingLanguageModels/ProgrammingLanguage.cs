using System;
using System.Collections.Generic;

namespace ProgrammingLanguages.ProgrammingLanguageModels;

public partial class ProgrammingLanguage
{
    public int LanguageId { get; set; }

    public string LanguageName { get; set; } = null!;

    public int? CreatedYear { get; set; }

    public string? Creator { get; set; }

    public string? Paradigm { get; set; }

    public string? Description { get; set; }

    public string? OfficialWebsite { get; set; }

    public string? L1 { get; set; }

    public string? L2 { get; set; }

    public string? L3 { get; set; }

    public virtual ICollection<LanguageVersion> LanguageVersions { get; set; } = new List<LanguageVersion>();

    public virtual ICollection<LearningProgress> LearningProgresses { get; set; } = new List<LearningProgress>();

    public virtual ICollection<LearningResource> LearningResources { get; set; } = new List<LearningResource>();

    public virtual ICollection<ProgrammingLanguageFeature> ProgrammingLanguageFeatures { get; set; } = new List<ProgrammingLanguageFeature>();
}
