using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.DatabaseAdapters
{
    // См пример: ms-help://MS.MSDNQTR.v90.en/fxref_mscorlib/html/bc3d7d70-886c-6572-18cd-3e82f6aeeabe.htm
    // ms-help://MS.VSCC.v90/MS.MSDNQTR.v90.en/dv_fxfund/html/30386922-1e00-4602-9ebf-526b271a8b87.htm
    
    /// <summary>
    /// Атрибут, описывающий тип адаптера БД в самом классе адаптера БД
    /// Для автоматической проверки доступности типов БД Солюшена.
    /// Экспериментальный класс
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class MDatabaseTypeAttribute: Attribute
    {
        protected MDatabaseType m_dbtype;

        protected bool m_active;

        public MDatabaseTypeAttribute(MDatabaseType dbType, Boolean ready)
        {
            this.m_dbtype = dbType;
            this.Active = ready;
        }
        /// <summary>
        /// Тип адаптера БД
        /// </summary>
        public MDatabaseType DatabaseType
        {
            get { return m_dbtype; }
            set { m_dbtype = value; }
        }

        /// <summary>
        /// Флаг готовности адаптера к использованию
        /// </summary>
        public bool Active
        {
            get { return m_active; }
            set { m_active = value; }
        }

    }
}
