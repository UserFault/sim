using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Container
{
    //TODO: непонятное название. Что это значит для связи? 

    /// <summary>
    /// NT-Направление связи
    /// Бывший MAxisDirection
    /// </summary>
    public enum MLinkDirection
    {
        /// <summary>
        /// Направление отсутствует
        /// </summary>
        None = 0,
        /// <summary>
        /// Направление снизу вверх
        /// </summary>
        Up = 1,
        /// <summary>
        /// Направление сверху вниз
        /// </summary>
        Down = 2,
        /// <summary>
        /// Направление любое
        /// </summary>
        Any = 3,
    }
}
