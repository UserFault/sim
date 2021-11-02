﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Container
{


    //TODO: класс должен соответствовать строению ячейки (MCell)

    /// <summary>
    /// NT-Шаблон ячейки для поиска ячеек в Контейнере
    /// </summary>
    public class MCellTemplate
    {

        #region Fields
        //поля по MElement
        /// <summary>
        /// Cell identifier
        /// </summary>
        private MCellId m_cellid;
        /// <summary>
        /// Cell name
        /// </summary>
        private String m_name;
        /// <summary>
        /// текстовое описание, null по умолчанию.
        /// </summary>
        private String m_description;
        /// <summary>
        /// flag is element active or deleted //default true
        /// </summary>
        private bool? m_isactive;
        /// <summary>
        /// Поле для значения, используемого в сервисных операциях (поиск в графе,  обслуживание и так далее) //default 0
        /// </summary>
        private int? m_serviceflag;
        /// <summary>
        /// Link state id. //default 0
        /// </summary>
        private MCellId m_elementState;
        /// <summary>
        /// Cell type id
        /// </summary>
        private MCellId m_elementClass;
        /// <summary>
        /// Cell is read-only flag
        /// </summary>
        private bool? m_readonly;
        /// <summary>
        /// Cell creation timestamp
        /// </summary>
        private DateTime? m_creatime;
        /// <summary>
        /// Last modification timestamp
        /// </summary>
        private DateTime? m_moditime;


        //поля по MCell
        /// <summary>
        /// Cell data value
        /// </summary>
        private byte[] m_value;
        /// <summary>
        /// Cell data value type id
        /// </summary>
        private MCellId m_valuetypeid;
        /// <summary>
        /// Cell mode
        /// </summary>
        private MCellMode? m_cellmode;
        ///// <summary>
        ///// Cell link collection
        ///// </summary>
        //private MLinkCollection m_links;//Связи ячейки не копируем и по ним не ищем
        #endregion

        /// <summary>
        /// NT-Normal constructor
        /// </summary>
        public MCellTemplate()
        {
            m_cellid = null;
            m_creatime = null;
            m_moditime = null;
            m_name = null;
            m_readonly = null;
            m_cellmode = null;
            m_elementClass = null;
            m_value = null;
            m_valuetypeid = null;
            m_description = null;
            m_elementState = null;
            m_serviceflag = null;
            m_isactive = null;
        }

        /// <summary>
        /// NT-Create template as full copy of cell 
        /// </summary>
        /// <param name="c">Образцовая ячейка</param>
        public MCellTemplate(MCell c)
        {
            //делаем копии объектов
            this.CellID = new MCellId(c.ID);
            this.LastCreate = c.LastCreate;
            this.LastChange = c.LastChange;
            this.Name = c.Name;
            this.ReadOnly = c.isReadOnly;
            this.CellMode = c.CellMode;
            this.ElementClass = new MCellId(c.ElementClass);
            this.Value = Utility.MUtility.CopyArray(c.Value);
            this.ValueTypeId = new MCellId(c.ValueTypeId);
            this.Description = c.Description;
            this.ElementState = new MCellId(c.ElementState);
            this.ServiceFlag = c.ServiceFlag;
            this.isActive = c.isActive;
        }

        #region Properties

        /// <summary>
        /// текстовое описание, null по умолчанию.
        /// </summary>
        public string Description
        {
            get
            {
                return m_description;
            }
            set
            {
                m_description = value;
            }
        }

        /// <summary>
        /// flag is element active or deleted //default true
        /// </summary>
        public bool? isActive
        {
            get
            {
                return m_isactive;
            }
            set
            {
                m_isactive = value;
            }
        }

        /// <summary>
        /// Поле для значения, используемого в сервисных операциях (поиск в графе,  обслуживание и так далее) //default 0
        /// </summary>
        public int? ServiceFlag
        {
            get
            {
                return m_serviceflag;
            }
            set
            {
                m_serviceflag = value;
            }
        }

        /// <summary>
        /// Вынесен из подклассов как общее свойство. Link state id. //default 0
        /// </summary>
        public MCellId ElementState
        {
            get
            {
                return m_elementState;
            }
            set
            {
                m_elementState = value;
            }
        }


        /// <summary>
        /// Cell name
        /// </summary>
        public String Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }

        /// <summary>
        /// Cell type id
        /// </summary>
        public MCellId ElementClass
        {
            get
            {
                return m_elementClass;
            }
            set
            {
                m_elementClass = value;
            }
        }

        /// <summary>
        /// Cell creation timestamp
        /// </summary>
        public DateTime? LastCreate
        {
            get
            {
                return m_creatime;
            }
            set
            {
                m_creatime = value;
            }
        }

        /// <summary>
        /// Last modification timestamp
        /// </summary>
        public DateTime? LastChange
        {
            get
            {
                return m_moditime;
            }
            set
            {
                m_moditime = value;
            }
        }

        /// <summary>
        /// Cell is read-only flag
        /// </summary>
        public bool? ReadOnly
        {
            get
            {
                return m_readonly;
            }
            set
            {
                m_readonly = value;
            }
        }

        /// <summary>
        /// Cell data value
        /// </summary>
        public byte[] Value
        {
            get
            {
                return m_value;
            }
            set
            {
                m_value = value;
            }
        }

        /// <summary>
        /// Cell data value type id
        /// </summary>
        public MCellId ValueTypeId
        {
            get
            {
                return m_valuetypeid;
            }
            set
            {
                m_valuetypeid = value;
            }
        }

        ///// <summary>
        ///// Cell link collection
        ///// </summary>
        //public MLinkCollection Links - не ищем по связям 
        //{
        //    get
        //    {
        //        return m_links;
        //    }
        //    set
        //    { m_links = value; }
        //}

        /// <summary>
        /// Cell save mode, for memory search only
        /// </summary>
        public MCellMode? CellMode
        {
            get
            {
                return m_cellmode;
            }
            set
            {
                m_cellmode = value;
            }
        }

        /// <summary>
        /// идентификатор ячейки в контейнере.
        /// </summary>
        public MCellId CellID
        {
            get
            {
                return m_cellid;
            }
            set
            {
                m_cellid = value;
            }
        }

        #endregion


        /// <summary>
        /// NT-Соответствует ли ячейка заданным условиям
        /// </summary>
        /// <param name="cell">Сравнивемая ячейка</param>
        /// <returns>True if cell match template, False otherwise</returns>
        public bool isMatch(MCell cell)
        {
            //TODO: Переделать последовательность проверок для более быстрого поиска.
            //Для этого нужна куча разнообразных ячеек в Солюшене и считать статистику по каждому полю.  
            if ((m_cellid != null) && (!m_cellid.isEqual(cell.ID))) return false;
            else if ((m_isactive.HasValue) && (m_isactive.Value != cell.isActive)) return false;
            else if ((m_elementClass != null) && (!m_elementClass.isEqual(cell.ElementClass))) return false;
            else if ((m_valuetypeid != null) && (!m_valuetypeid.isEqual(cell.ValueTypeId))) return false;
            else if ((m_elementState != null) && (!m_elementState.isEqual(cell.ElementState))) return false;
            else if ((m_name != null) && (!m_name.Equals(cell.Name))) return false;
            else if (m_readonly.HasValue && (m_readonly.Value != cell.isReadOnly)) return false;
            else if ((m_value != null) && (!Bar.Utility.MUtility.ArrayEqual(m_value, cell.Value))) return false; //??? проверить работу
            else if (m_serviceflag.HasValue && (m_serviceflag.Value != cell.ServiceFlag)) return false;
            else if ((m_description != null) && (!m_description.Equals(cell.Description))) return false;
            else if (m_creatime.HasValue && (m_creatime.Value != cell.LastCreate)) return false;
            else if (m_moditime.HasValue && (m_moditime.Value != cell.LastChange)) return false;
            else if (m_cellmode.HasValue && (m_cellmode.Value != cell.CellMode)) return false;

            else return true;
        }






    }
}
