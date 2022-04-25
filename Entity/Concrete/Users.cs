﻿using System;
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
        public string USER_NAME { get; set; }
        public string USER_PASSWORD { get; set; }
        public string USER_EMAIL { get; set; }
        public DateTime USER_REGISTER_DATE { get; set; }
        public bool USER_IS_ACTIVE { get; set; }
    } 
}
