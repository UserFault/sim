using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.DatabaseAdapters
{
    
    /// <summary>
    /// Обозначает тип БД проекта.
    /// </summary>
    public enum MDatabaseType
    {
        /// <summary>
        /// Unknown database type
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// No database in project
        /// </summary>
        NoDatabase = 1,
        /// <summary>
        /// Ms Access database
        /// </summary>
        MsJet40 = 2,

        //Добавить типы БД здесь только если есть адаптеры для них 
        ///// <summary>
        ///// MsSql2005 database
        ///// </summary>
        //MicrosoftSqlServer2005 = 3,
        ///// <summary>
        ///// MySql5 database
        ///// </summary>
        //MySql5 = 4,
        
        //Sqlite3 = 5,

    }

}
