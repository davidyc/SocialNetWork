namespace SocialNetWorkv1._0.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ������ �������
    /// </summary>
    [Table("FriendRequest")]
    public partial class FriendRequest
    {

        public int ID { get; set; }

        //id ���� ��� �����������
        public int? UserID { get; set; }

        // iD ����� 
        public int? FriendID { get; set; }

        public virtual Logins Logins { get; set; }

        public virtual Logins Logins1 { get; set; }
    }
}
