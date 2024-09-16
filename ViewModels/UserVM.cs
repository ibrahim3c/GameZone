namespace GameZone.ViewModels
{
    public class UserVM
    {
        [Required]
        public string UserName { get; set; } = default!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; } = default!;
        public string? Address { get; set; } = default!;
        
    }
}
