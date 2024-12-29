using System.ComponentModel.DataAnnotations;

namespace ProgrammingLanguages.ProgrammingLanguageModels.LoginPartialVM;

public partial class Loginpartial
{
    public int UserId { get; set; }
    [Display(Name = "使用者名稱")]
    public string UserName { get; set; } = null!;
    [Display(Name = "郵件地址")]
    public string Email { get; set; } = null!;
    //DB有設定PasswordHash不允許null，若null會擲回例外
    [Display(Name = "密碼")]
    public string Password { get; set; }
    [Display(Name = "註冊日期")]
    public DateTime? RegisterDate { get; set; }
    [Display(Name = "其他描述")]
    public string? Profile { get; set; }

}
