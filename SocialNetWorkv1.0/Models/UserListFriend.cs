namespace SocialNetWorkv1._0.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// описывает модель списка дурезей
    /// </summary>
    [Table("UserListFriend")]
    public partial class UserListFriend
    {
        public int id { get; set; }
        
        //номер списка друзей пользователя совпадает с его id 
        public int? UserFrinds { get; set; }

        //id друга 
        public int? IdFriend { get; set; }

        public virtual Logins Logins { get; set; }

        public virtual UserFriends UserFriends { get; set; }
    }
}
