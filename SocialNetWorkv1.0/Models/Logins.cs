namespace SocialNetWorkv1._0.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ������ ��������� ������� LOGINS � ���� ������ 
    /// </summary>
    public partial class Logins
    {
        /// <summary>
        /// ����������� �� ���������
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Logins()
        {
            FriendRequest = new HashSet<FriendRequest>();
            FriendRequest1 = new HashSet<FriendRequest>();
            UserFriends = new HashSet<UserFriends>();
            UserListFriend = new HashSet<UserListFriend>();
            UserMessege = new HashSet<UserMessege>();
            UserMessege1 = new HashSet<UserMessege>();
        }

        /// <summary>
        /// id ������
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        /// <summary>
        /// ����� ������������
        /// </summary>
        [Required]
        [StringLength(15)]
        public string LoginUser { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        [Required]
        [StringLength(20)]
        public string PasswordUser { get; set; }

        /// <summary>
        /// Id  ����
        /// </summary>
        public int? RoleUser { get; set; }

        /// <summary>
        /// ������ �� ������ �������
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FriendRequest> FriendRequest { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FriendRequest> FriendRequest1 { get; set; }

        public virtual Roles Roles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserFriends> UserFriends { get; set; }

        public virtual UserInfo UserInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserListFriend> UserListFriend { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserMessege> UserMessege { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserMessege> UserMessege1 { get; set; }
    }
}
