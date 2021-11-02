using System;
using System.Collections.Generic;
using System.Text;

namespace Bar
{
    /// <summary>
    /// Состояние контейнера
    /// </summary>
    public enum MSolutionState
    {
        /// <summary>
        /// Unknown state
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Engine is opened and ready to work
        /// </summary>
        Ready = 1,
        /// <summary>
        /// Engine is inactive
        /// </summary>
        Closed = 2,
        /// <summary>
        /// Engine is brocken
        /// </summary>
        Broken = 3,
        /// <summary>
        /// Engine executes some operations. Wait for Open state.
        /// </summary>
        Executing = 4,
    }
}
