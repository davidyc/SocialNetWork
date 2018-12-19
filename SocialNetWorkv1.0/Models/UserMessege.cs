namespace SocialNetWorkv1._0.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ������ ��������� ���������
    /// </summary>
    [Table("UserMessege")]
    public partial class UserMessege
    {
        //id cjj,otybz
        public int ID { get; set; }

        //id �����������
        public int? IDrSender { get; set; }

        //�� �����������
        public int? IDCatcher { get; set; }

        //����� ���������
        [StringLength(2500)]
        public string MessageText { get; set; }

        //������ ���������
        public DateTime? SendTime { get; set; }

        public virtual Logins Logins { get; set; }

        public virtual Logins Logins1 { get; set; }
    }
}
