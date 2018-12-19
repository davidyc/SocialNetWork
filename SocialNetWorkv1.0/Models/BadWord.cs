namespace SocialNetWorkv1._0.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BadWord")]
    public partial class BadWord
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string word { get; set; }
    }
}
