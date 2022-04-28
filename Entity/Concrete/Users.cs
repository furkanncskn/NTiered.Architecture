using System;
using Entity.Abstract;
using Entity.Attribute;

namespace Entity.Concrete
{
    [Table(Name = "users")]
    public class Users : IEntity
    {
        [Key]
        public int USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string USER_PASSWORD { get; set; }
        public string USER_EMAIL { get; set; }
        public DateTime? USER_REGISTER_DATE { get; set; }
        public bool USER_IS_ACTIVE { get; set; }
    } 
}
