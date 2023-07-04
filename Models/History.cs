using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManager3.Models
{
    /// <summary>
    /// Модель для истории покупок
    /// </summary>
    public class History
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор аккаунта. К какому пользователю привязана история
        /// </summary>
        public int AccountId { set; get; }
        /// <summary>
        /// Дата покупки
        /// </summary>
        public string Date { set; get; } = null!;
        /// <summary>
        /// Сумма покупки
        /// </summary>
        public int Amount { set; get; }
    }
}
