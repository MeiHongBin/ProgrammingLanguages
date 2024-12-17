using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProgrammingLanguages.ProgrammingLanguageModels;

public partial class User
{
    public int UserId { get; set; }
    [Display(Name = "使用者名稱")]
    public string UserName { get; set; } = null!;
    [Display(Name = "郵件地址")]
    public string Email { get; set; } = null!;
    [Display(Name = "密碼")]
    public string? PasswordHash { get; set; }
    [Display(Name = "註冊日期")]
    public DateTime? RegisterDate { get; set; }
    [Display(Name = "其他描述")]
    public string? Profile { get; set; }

    public string? Us1 { get; set; }

    public string? Us2 { get; set; }

    public string? Us3 { get; set; }

    public virtual ICollection<LearningProgress> LearningProgresses { get; set; } = new List<LearningProgress>();
}
