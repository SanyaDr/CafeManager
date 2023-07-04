using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManager3.Models
{
    /// <summary>
    /// Модель аккаунта пользователя
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Номер телефона пользователя
        /// </summary>
        public string mobileNumber { get; set; } = null!;
    }
}
