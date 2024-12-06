using System;
using System.Collections.Generic;

namespace ProgrammingLanguages.ProgrammingLanguageModels;

public partial class LanguageVersion
{
    public int VersionId { get; set; }

    public int LanguageId { get; set; }

    public string VersionName { get; set; } = null!;

    public DateOnly? ReleaseDate { get; set; }

    public string? Changes { get; set; }

    public string? Lv1 { get; set; }

    public string? Lv2 { get; set; }

    public string? Lv3 { get; set; }

    public virtual ProgrammingLanguage Language { get; set; } = null!;
}
