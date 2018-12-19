namespace SocialNetWorkv1._0.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Модель хранит информацию о UserInfo из базы
    /// </summary>
    [Table("UserInfo")]
    public partial class UserInfo
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        /// <summary>
        /// путь адреса где лежит фото
        /// </summary>
        [StringLength(50)]
        public string ImageUser { get; set; }

        /// <summary>
        /// Имя пользовтаеля
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Firstname { get; set; }
        
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Lastname { get; set; }
        
        /// <summary>
        /// Возарст пользовтеля
        /// </summary>
        [Required]
        [Range(0,10000)]
        public int? Age { get; set; }
        
        /// <summary>
        /// Почта пользователя
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3)]
        // с нета взял 
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")  ]
        public string Email { get; set; }

        /// <summary>
        /// адерсс пользователя
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Adress { get; set; }

        /// <summary>
        /// ссылка
        /// </summary>
        public virtual Logins Logins { get; set; }
    }
}
