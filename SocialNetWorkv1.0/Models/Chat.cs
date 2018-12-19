using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetWorkv1._0.Models
{
    /// <summary>
    /// мольдь описывает переписку
    /// </summary>
    public class Chat
    {
        /// <summary>
        /// ID собеседника
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        ////логин собесежнка
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// сообщения
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ////дата отправки соробщения
        /// </summary>
        public DateTime? SendTime { set; get; }

        /// <summary>
        /// флаг для отправленых сообщений
        /// </summary>
        public bool SenderFlag { get; set; }
       
    }
}