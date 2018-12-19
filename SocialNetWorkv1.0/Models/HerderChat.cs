using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetWorkv1._0.Models
{
    /// <summary>
    ///Класс описывает заголовки в чатах для INDEX
    /// </summary>
    public class HerderChat
    {
        /// <summary>
        /// ID собесеника
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// Логин собеседника
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        ////последнее сообщение
        /// </summary>
        public string LastMassege { get; set; }

        /// <summary>
        /// флаг для отправленых сообщений
        /// </summary>
        public bool SenderFlag { get; set; }

    }
}