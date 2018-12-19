namespace SocialNetWorkv1._0.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// модель описывает сообщение
    /// </summary>
    [Table("UserMessege")]
    public partial class UserMessege
    {
        //id cjj,otybz
        public int ID { get; set; }

        //id отправителя
        public int? IDrSender { get; set; }

        //шв порлучателя
        public int? IDCatcher { get; set; }

        //текст сообщения
        [StringLength(2500)]
        public string MessageText { get; set; }

        //времмя отправкит
        public DateTime? SendTime { get; set; }

        public virtual Logins Logins { get; set; }

        public virtual Logins Logins1 { get; set; }
    }
}
