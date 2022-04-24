using System;
using System.ComponentModel.DataAnnotations;
using Entity.Abstract;
using Entity.Attribute;

namespace Entity.Concrete
{
    [Class(Name = "[dbo].[Users]")]
    public class Users : IEntity
    {
        [Attribute.Key]
        public int USER_ID { get; set; }

        [Required(ErrorMessage = "İsim alanı boş geçilemez")]
        [StringLength(150, ErrorMessage = "Lütfen geçerli bir kullanıcı ismi giriniz.")]
        public string USER_NAME { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş geçilemez")]
        public string USER_PASSWORD { get; set; }

        [Required(ErrorMessage = "Email alanı boş geçilemez")]
        [EmailAddress(ErrorMessage = "Geçersiz email girişi!")]
        public string USER_EMAIL { get; set; }

        public DateTime USER_REGISTER_DATE { get; set; }

        public bool USER_IS_ACTIVE { get; set; }
    } 
}
