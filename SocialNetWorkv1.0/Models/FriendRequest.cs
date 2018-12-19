namespace SocialNetWorkv1._0.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// модель запроса
    /// </summary>
    [Table("FriendRequest")]
    public partial class FriendRequest
    {

        public int ID { get; set; }

        //id того что запрошивает
        public int? UserID { get; set; }

        // iD друга 
        public int? FriendID { get; set; }

        public virtual Logins Logins { get; set; }

        public virtual Logins Logins1 { get; set; }
    }
}
