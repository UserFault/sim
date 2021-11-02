using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Bar.Container
{
    //о старом поле TableId:
    //Номер в таблице показывает, что текущая связь записана в таблицу, а значит, не временная.
    //Но не означает, что связь находится в собственном контейнере, а не в чужом.
    //Поэтому смысл этого номера только в том, чтобы отличать постоянную связь от временной,
    //и искать ее в таблице БД быстрее чем по двум полям linkId.
    //И это совсем неправильно теперь.
    //Проще уж в каждом объекте ячейки и связи хранить ссылку на объект контейнера или солюшена.
    //Больше расход памяти, зато быстрее.


    /// <summary>
    /// NR-Класс связи ячеек контейнера
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public class MLink : MElement
    {
        #region *** Fields ***
        /// <summary>
        /// Обратная ссылка на Солюшен
        /// </summary>
        private MSolution m_Solution;
        /// <summary>
        /// Таймштамп создания связи
        /// </summary>
        private DateTime m_lastCreate;
        /// <summary>
        /// Таймштамп изменения связи
        /// </summary>
        private DateTime m_lastChange;

        /// <summary>
        /// link id
        /// </summary>
        private UInt64 m_linkId;
        /// <summary>
        /// cell id
        /// </summary>
        private UInt64 m_upcellid;
        /// <summary>
        /// cell id
        /// </summary>
        private UInt64 m_downcellid;
        /// <summary>
        /// cell reference
        /// </summary>
        private MCell m_upcell;
        /// <summary>
        /// cell reference
        /// </summary>
        private MCell m_downcell;
        /// <summary>
        /// link axis
        /// </summary>
        private UInt64 m_linkClass;
        /// <summary>
        /// текстовое описание, null по умолчанию.
        /// </summary>
        private String m_description;
        /// <summary>
        /// flag is element active or deleted //default true
        /// </summary>
        private bool m_isactive;
        /// <summary>
        /// Поле для значения, используемого в сервисных операциях (поиск в графе,  обслуживание и так далее) //default 0
        /// </summary>
        private int m_serviceflag;
        /// <summary>
        /// Link state id. //default 0
        /// </summary>
        private UInt64 m_linkState;
        /// <summary>
        /// Флаг только чтение
        /// </summary>
        private bool m_ReadOnly;
        #endregion

        /// <summary>
        /// NT- Constructor
        /// Нигде не может пригодиться.
        /// </summary>
        public MLink()
        {
            this.m_description = String.Empty;
            this.m_downcell = null;
            this.m_downcellid = 0;
            this.m_isactive = true;
            this.m_linkClass = 0;
            this.m_linkId = 0;
            this.m_linkState = 0;
            this.m_serviceflag = 0;
            this.m_upcell = null;
            this.m_upcellid = 0;
            this.m_ReadOnly = false;
            this.m_lastChange = DateTime.MinValue;
            this.m_lastCreate = DateTime.MaxValue;
            this.m_Solution = null;
        }
        /// <summary>
        /// NT-Конструктор создания существующей связи
        /// </summary>
        /// <param name="sol">Текущий солюшен</param>
        /// <param name="id">Идентификатор связи</param>
        /// <param name="downCellID">Идентификатор нижней ячейки</param>
        /// <param name="upCellID">Идентификатор верхней ячейки</param>
        /// <param name="active">Флаг активности связи</param>
        /// <param name="linkClass">Идентификатор ячейки типа (оси) связи</param>
        /// <param name="linkState">Идентификатор ячейки состояния связи</param>
        /// <param name="service">Сервисное значение</param>
        /// <param name="description">Описание связи</param>
        /// <param name="readOnly">Режим Только чтение</param>
        /// <param name="created">Таймштамп создания связи</param>
        /// <param name="changed">Таймштамп изменения связи</param>
        public MLink(MSolution sol, MLinkId id, MCellId downCellID, MCellId upCellID,
             MCellId linkClass, MCellId linkState, bool readOnly,
             String description, Int32 service, bool active, DateTime created, DateTime changed)
        {
            this.m_Solution = sol;
            this.m_linkId = id.ToU64();
            this.m_downcellid = downCellID.ToU64();
            this.m_upcellid = upCellID.ToU64();
            this.m_linkClass = linkClass.ToU64();
            this.m_linkState = linkState.ToU64();
            this.m_ReadOnly = readOnly;
            this.m_description = description;
            this.m_serviceflag = service;
            this.m_isactive = active;
            //set timestamp
            this.m_lastCreate = created;
            this.m_lastChange = changed;
            //set defaults
            this.m_downcell = null;
            this.m_upcell = null;

            return;
        }

        /// <summary>
        /// NT-Конструктор создания новой связи
        /// </summary>
        /// <param name="sol">Текущий солюшен</param>
        /// <param name="id">Идентификатор связи</param>
        /// <param name="downCellID">Идентификатор нижней ячейки</param>
        /// <param name="upCellID">Идентификатор верхней ячейки</param>
        /// <param name="linkClass">Идентификатор ячейки типа (оси) связи</param>
        /// <param name="linkState">Идентификатор ячейки состояния связи</param>
        /// <param name="readOnly">Режим Только чтение</param>
        public MLink(MSolution sol, MLinkId id, MCellId downCellID, MCellId upCellID,
            MCellId linkClass, MCellId linkState, bool readOnly)
        {
            this.m_Solution = sol;
            this.m_linkId = id.ToU64();
            this.m_downcellid = downCellID.ToU64();
            this.m_upcellid = upCellID.ToU64();
            this.m_linkClass = linkClass.ToU64();
            this.m_linkState = linkState.ToU64();
            this.m_ReadOnly = readOnly;
            //set defaults
            this.m_description = String.Empty;
            this.m_downcell = null;
            this.m_upcell = null;
            this.m_isactive = true;
            this.m_serviceflag = 0;
            //set timestamp
            this.m_lastCreate = DateTime.Now;
            this.m_lastChange = DateTime.Now;

            return;
        }

        #region *** MElement property set ***
        /// <summary>
        /// NT-Ссылка на объект Солюшена
        /// </summary>
        public override MSolution Solution
        {
            get { return m_Solution; }
        }

        /// <summary>
        /// NT-Идентификатор элемента
        /// </summary>
        /// <remarks>
        /// Идентификатор должен быть приведен к типу MID.
        /// </remarks>
        public override MID ID
        {
            get { return (MID)(MLinkId.FromU64(this.m_linkId)); }
        }

        /// <summary>
        /// NT-Название элемента
        /// </summary>
        /// <remarks>Для связи не хранится название. Проперти всегда возвращает пустую строку.</remarks>
        public override string Name
        {
            get { return String.Empty; }
            set { }
        }

        /// <summary>
        /// NT-Описание элемента
        /// String.Empty по умолчанию.
        /// При изменении записывается в БД
        /// Действует флаг ReadOnly
        /// </summary>
        /// <remarks>
        /// Readonly: Выбросить исключение, если не временная связь.
        /// </remarks>
        public override string Description
        {
            get { return this.m_description; }
            set
            {
                if (this.checkReadOnly()) return;//read-only flag check
                m_description = value;
                //Если связь записана в БД, то записать все данные в БД соответствующего солюшена
                if (this.isConstAndLocal)
                    this.m_Solution.DbAdapter.LinkUpdateDescription(this);
                this.updateLastChange();//update timestamp Changed
            }
        }




        /// <summary>
        /// NT-Flag is element active or deleted 
        /// Default true
        /// При изменении записывается в БД
        /// Действует флаг ReadOnly
        /// </summary>
        /// <remarks>
        /// Readonly: Выбросить исключение, если не временная связь.
        /// </remarks>
        public override bool isActive
        {
            get { return m_isactive; }
            set
            {
                if (this.checkReadOnly()) return;//read-only flag check
                m_isactive = value;
                //Если связь записана в БД, то записать изменения в БД соответствующего солюшена
                if (this.isConstAndLocal)
                    this.m_Solution.DbAdapter.LinkUpdateActive(this);
                this.updateLastChange();//update timestamp Changed
            }
        }

        /// <summary>
        /// NT-Сервисное значение
        /// Не сохраняется в БД, не учитывает флаг ReadOnly, не изменяет таймштамп изменения.
        /// </summary>
        /// <remarks>
        /// Значение используемое для поиска в графе и подобных целях. 
        /// По умолчанию = 0.
        /// Readonly: Не требуется, так как нет изменения Солюшена.
        /// </remarks>
        public override Int32 ServiceFlag
        {
            get { return m_serviceflag; }
            set { m_serviceflag = value; }
        }

        /// <summary>
        /// NT-Состояние элемента
        /// При изменении записывается в БД
        /// Действует флаг ReadOnly
        /// </summary>
        /// <remarks>
        /// Идентификатор ячейки, описывающей состояние этого элемента
        /// Readonly: Выбросить исключение, если не временная связь.
        /// </remarks>
        public override MCellId ElementState
        {
            get { return MCellId.FromU64(this.m_linkState); }
            set
            {
                if (checkReadOnly()) return;//read-only flag check
                m_linkState = value.ToU64();
                //Если связь записана в БД, то записать изменения в БД соответствующего солюшена
                if (this.isConstAndLocal)
                    this.m_Solution.DbAdapter.LinkUpdateState(this);
                this.updateLastChange();//update timestamp Changed
            }
        }



        //новые члены интерфейса:

        /// <summary>
        /// NT-Класс связи, бывший Ось связи, Axis
        /// Не записывается в БД при изменении.
        /// Нельзя изменять из пользовательского кода! Так как будет нарушение концепции связи.
        /// </summary>
        /// <remarks>
        /// Идентификатор ячейки, описывающей класс этого элемента.
        /// </remarks>
        public override MCellId ElementClass
        {
            get { return MCellId.FromU64(m_linkClass); }
        }

        /// <summary>
        /// NT-Флаг только чтение
        /// При изменении записывается в БД
        /// </summary>
        /// <remarks>
        /// По умолчанию = false
        /// 
        /// </remarks>
        public override bool isReadOnly
        {
            get { return this.m_ReadOnly; }
            set
            {
                this.m_ReadOnly = value;
                //Если связь записана в БД, то записать изменения в БД соответствующего солюшена
                if (this.isConstAndLocal)
                    this.m_Solution.DbAdapter.LinkUpdateReadOnly(this);
                this.updateLastChange();//update timestamp Changed
            }
        }

        /// <summary>
        /// NT-Таймштамп создания элемента
        /// Не записывается в БД при изменении
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public override DateTime LastCreate
        {
            get { return this.m_lastCreate; }
        }

        /// <summary>
        /// NT-Таймштамп последнего изменения элемента
        /// Сам не записывается в БД
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public override DateTime LastChange
        {
            get { return m_lastChange; }
        }

        /// <summary>
        /// NT-Таймштамп последнего чтения элемента
        /// </summary>
        /// <remarks>
        /// Для связи это значение не хранится. Возвращает всегда DateTime.MinValue.
        /// </remarks>
        public override DateTime LastUsed
        {
            get { return DateTime.MinValue; }
        }

        #endregion
        #region MLink собственные проперти



        /// <summary>
        /// NT-Up cell id. 
        /// Changes not saved in database
        /// </summary>
        public MCellId upCellId
        {
            get { return MCellId.FromU64(m_upcellid); }
        }
        /// <summary>
        /// NT-Down cell id. 
        /// Changes not saved in database
        /// </summary>
        public MCellId downCellId
        {
            get { return MCellId.FromU64(m_downcellid); }
        }

        /// <summary>
        /// NT-Reference to cell or null. 
        /// Changes not saved in database
        /// </summary>
        public MCell upCell
        {
            get { return m_upcell; }
            internal set { m_upcell = value; }
        }
        /// <summary>
        /// NT-Reference to cell or null. 
        /// Changes not saved in database
        /// </summary>
        public MCell downCell
        {
            get { return m_downcell; }
            internal set { m_downcell = value; }
        }

        #endregion

        /// <summary>
        /// NT-Обновить таймштамп изменения связи
        /// </summary>
        private void updateLastChange()
        {
            this.m_lastChange = DateTime.Now;
        }



        #region Fast access functions for link search - violate MID concept!
        //предназначены для быстрого чтения в обход типового механизма
        //Он уж очень тормозной намечается

        internal UInt64 intGetIDasU64()
        {
            return this.m_linkId;
        }
        /// <summary>
        /// Get pure link axis
        /// </summary>
        internal UInt64 intGetClassIDasU64()
        {
            return this.m_linkClass;
        }
        /// <summary>
        /// Get pure link downcell id
        /// </summary>
        internal UInt64 intGetDownIDasU64()
        {
            return this.m_downcellid;
        }
        /// <summary>
        /// Get pure link upcell id
        /// </summary>
        internal UInt64 intGetUpIDasU64()
        {
            return this.m_upcellid;
        }
        /// <summary>
        /// Get pure link state
        /// </summary>
        internal UInt64 intGetStateIDasU64()
        {
            return this.m_linkState;
        }
        #endregion

        /// <summary>
        /// NT-Проверить что связь является подобной.
        /// </summary>
        /// <param name="li">Образцовая связь</param>
        /// <returns>Возвращает true если связь совпадает с образцом по идентификаторам ячеек, оси связи, флагу активности.</returns>
        public bool isEqualLink(MLink li)
        {
            if (this.m_isactive != li.m_isactive) return false;
            if (this.m_upcellid != li.m_upcellid) return false;
            if (this.m_downcellid != li.m_downcellid) return false;
            if (this.m_linkClass != li.m_linkClass) return false;

            return true;
        }
        /// <summary>
        /// NT-Установить ссылки на ячейку если ее идентификаторы есть в связи
        /// </summary>
        /// <param name="cellid64">идентификатор ячейки</param>
        /// <param name="cell">объект ячейки</param>
        internal void setCellRefsIfExists(UInt64 cellid64, MCell cell)
        {
            if (m_upcellid == cellid64)
                m_upcell = cell;
            if (m_downcellid == cellid64)
                m_downcell = cell;

            return;
        }

        /// <summary>
        /// NT-Represent link for debug view
        /// </summary>
        /// <remarks>
        /// Readonly: Не требуется, так как нет изменения Солюшена.
        /// </remarks>
        public override string ToString()
        {
            return String.Format("Up={0} Dn={1} Ax={2} St={3}", this.upCellId, this.downCellId, this.ElementClass, this.ElementState);
        }

        /// <summary>
        /// NT-Возвращает True если указанный объект MLink класса
        /// </summary>
        /// <param name="obj">Проверяемый объект</param>
        /// <returns>Возвращает True если указанный объект MLink класса</returns>
        public static bool IsLinkType(Object obj)
        {
            //Readonly: Не требуется, так как нет изменения Солюшена.
            Type t = obj.GetType();
            return (t.Equals(typeof(MLink)));
        }

        //TODO: при изменении связи через внутренние переменные также изменять таймштамп изменений 
        //через  updateLastChange() для полей:
        //m_description m_isactive m_serviceflag m_linkState m_ReadOnly 

        //TODO: при изменении связи через внутренние переменные учитывать флаг m_ReadOnly!
        //поля : m_description m_isactive m_serviceflag m_linkState

        //TODO: Добавить функции MLink здесь...
        
        /// <summary>
        /// NT-Возвращает направление связи для указанного идентификатора ячейки. 
        /// Если указанный идентификатор ячейки отсутствует в этой связи, возвращается MLinkDirection.None.
        /// Если указанный идентификатор ячейки есть в обеих концах связи, возвращается MLinkDirection.Any.
        /// </summary>
        /// <param name="cellid">Идентификатор ячейки</param>
        /// <returns>Возвращает направление связи для указанного идентификатора ячейки.</returns>
        public MLinkDirection getLinkDirection(MCellId cellid)
        {
            //Readonly: Не требуется, так как нет изменения Солюшена.
            //TODO: можно оптимизировать проверки условий, после релиза.
            //а сейчас все наглядно.
            UInt64 id = cellid.ToU64();
            if ((m_upcellid == id) && (m_downcellid == id)) return MLinkDirection.Any;
            else if (m_upcellid == id) return MLinkDirection.Down;
            else if (m_downcellid == id) return MLinkDirection.Up;
            else return MLinkDirection.None;
        }

        /// <summary>
        /// NT-Возвращает направление связи, обратное переданному.
        /// Up => Down, Down => Up, Any => Any, None => None.
        /// </summary>
        /// <param name="linkDirection">Исходное направление связи</param>
        /// <returns>Возвращает направление связи, обратное переданному.</returns>
        public static MLinkDirection inverseLinkDirection(MLinkDirection linkDirection)
        {
            //Readonly: Не требуется, так как нет изменения Солюшена.
            switch (linkDirection)
            {
                case MLinkDirection.Down:
                    return MLinkDirection.Up;
                case MLinkDirection.Up:
                    return MLinkDirection.Down;
                case MLinkDirection.Any:
                    return MLinkDirection.Any;
                default:
                    return MLinkDirection.None;
            }
        }




        #region *** MObject serialization functions ***
        /// <summary>
        /// Convert object data to binary stream
        /// </summary>
        /// <param name="writer">Binary stream writer</param>
        public override void toBinary(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Convert object data from binary stream
        /// </summary>
        /// <param name="reader">Binary stream reader</param>
        public override void fromBinary(BinaryReader reader)
        {
            throw new NotImplementedException();
        }
        ///// <summary>
        ///// Convert object data to byte array
        ///// </summary>
        ///// <returns></returns>
        //public override byte[] toBinaryArray()
        //{
        //    //create memory stream and writer
        //    MemoryStream ms = new MemoryStream(64);//initial size for cell data 
        //    BinaryWriter bw = new BinaryWriter(ms, Encoding.Unicode);
        //    //convert data
        //    this.toBinary(bw);
        //    //close memory stream and get bytes
        //    bw.Close();
        //    return ms.ToArray();
        //}
        /// <summary>
        /// Convert object data to text string
        /// </summary>
        /// <param name="withHex">True - include HEX representation of binary data</param>
        /// <returns></returns>
        public override string toTextString(bool withHex)
        {
            throw new NotImplementedException();//TODO: Добавить обобщенный наследуемый код сериализации здесь
        }
        /// <summary>
        /// Convert object data to text stream
        /// </summary>
        /// <param name="writer">text stream writer</param>
        /// <param name="withHex">True - include HEX representation of binary data</param>
        public override void toText(TextWriter writer, bool withHex)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Convert object data from text stream
        /// </summary>
        /// <param name="reader">text stream reader</param>
        public override void fromText(TextReader reader)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
