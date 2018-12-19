namespace SocialNetWorkv1._0.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ������ ��������� ���� �� ������� ����
    /// </summary>
    public partial class Roles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Roles()
        {
            Logins = new HashSet<Logins>();
        }

        /// <summary>
        /// ID 
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ////�������� ����
        /// </summary>
        [Required]
        [StringLength(10)]
        public string NameRole { get; set; }

        /// <summary>
        /// ������ �� �����
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Logins> Logins { get; set; }
    }
}
