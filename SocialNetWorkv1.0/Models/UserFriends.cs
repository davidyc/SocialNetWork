namespace SocialNetWorkv1._0.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Опиисывает таблицу лист
    /// </summary>
    public partial class UserFriends
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserFriends()
        {
            UserListFriend = new HashSet<UserListFriend>();
        }
        /// <summary>
        /// ШВ пользователея
        /// </summary>
        public int IdUser { get; set; }

        /// <summary>
        /// id списка друзей
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDListFriend { get; set; }

        public virtual Logins Logins { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserListFriend> UserListFriend { get; set; }
    }
}
