using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Bar.Container
{
    /// <summary>
    /// NR-Представляет ячейку Солюшена в разных видах поведения
    /// </summary>
    public class MCellB : MCell
    {
#region *** Fields ***
        /// <summary>
        /// Идентификатор ячейки
        /// </summary>
        private UInt64 m_cellid;
        /// <summary>
        /// Название ячейки
        /// String.Empty by default
        /// Max length = 128 chars
        /// </summary>
        private String m_name;
        /// <summary>
        /// Текстовое описание ячейки, String.Empty по умолчанию.
        /// </summary>
        private String m_description;
        /// <summary>
        /// flag is element active or deleted //default true
        /// </summary>
        private bool m_isActive;
        /// <summary>
        /// Идентификатор ячейки состояния ячейки //default 0
        /// </summary>
        private UInt64 m_elementState;
        /// <summary>
        /// Идентификатор ячейки типа ячейки
        /// </summary>
        private UInt64 m_elementClass;
        /// <summary>
        /// Таймштамп создания ячейки
        /// </summary>
        private DateTime m_lastCreate;
        /// <summary>
        /// Таймштамп изменения ячейки
        /// </summary>
        private DateTime m_lastChange;
        /// <summary>
        /// Флаг Только чтение ячейки
        /// </summary>
        private bool m_readonly;
        /// <summary>
        /// Данные ячейки
        /// </summary>
        private byte[] m_value;
        /// <summary>
        /// Тип данных ячейки
        /// </summary>
        private UInt64 m_valuetypeid;
        /// <summary>
        /// Коллекция связей ячейки
        /// </summary>
        private MLinkCollection m_links;
        /// <summary>
        /// Режим ячейки
        /// </summary>
        /// <remarks>
        /// Тут только из-за MCellBt MCellBds. Для MCellA режим всегда один и возвращается кодом.
        /// Поэтому это поле тут, а не в MCell классе.
        /// </remarks>
        private MCellMode m_cellMode;

#endregion

        /// <summary>
        /// NT-Конструктор с значениями по умолчанию
        /// </summary>
        /// <param name="sol">Объект Солюшена</param>
        public MCellB(MSolution sol) : base(sol)
        {
            this.m_cellid = 0;
            this.m_cellMode = MCellMode.Normal;
            this.m_description = String.Empty;
            this.m_elementClass = 0;
            this.m_elementState = 0;
            this.m_isActive = true;
            this.m_lastChange = DateTime.Now;
            this.m_lastCreate = DateTime.Now;
            this.m_links = new MLinkCollection();
            this.m_name = String.Empty;
            this.m_readonly = false;
            this.m_serviceflag = 0;
            this.m_value = new Byte[0];
            this.m_valuetypeid = 0;
            return;
        }

        /// <summary>
        /// NT-Конструктор создания существующей связи
        /// </summary>
        /// <param name="sol">Текущий солюшен</param>
        /// <param name="active">Флаг активности ячейки</param>
        /// <param name="cellClass">Идентификатор ячейки типа ячейки</param>
        /// <param name="cellmode">Режим ячейки</param>
        /// <param name="cellState">Идентификатор ячейки состояния ячейки</param>
        /// <param name="changed">Таймштамп изменения ячейки</param>
        /// <param name="created">Таймштамп создания ячейки</param>
        /// <param name="data">Массив данных ячейки</param>
        /// <param name="datatype">Идентификатор ячейки типа данных ячейки</param>
        /// <param name="descr">Описание ячейки</param>
        /// <param name="id">Идентификатор ячейки</param>
        /// <param name="name">Название ячейки, длиной до 128 символов</param>
        /// <param name="readOnly">Режим Только чтение</param>
        /// <param name="service">Сервисное значение</param>
        /// <remarks>
        /// 
        /// </remarks>
        public MCellB(MSolution sol, MCellId id, MCellMode cellmode, String name, 
            string descr, MCellId cellClass, MCellId cellState,
            bool readOnly,  MCellId datatype, byte[] data,
             Int32 service,  bool active, DateTime created, DateTime changed )
            : base(sol)
        {
            this.m_cellid = id.ToU64();
            this.m_cellMode = cellmode;
            this.m_description = descr;
            this.m_elementClass = cellClass.ToU64();
            this.m_elementState = cellState.ToU64();
            this.m_isActive = active;
            this.m_lastChange = changed;
            this.m_lastCreate = created;
            this.m_links = new MLinkCollection();
            this.m_name = name;
            this.m_readonly = readOnly;
            this.m_serviceflag = service;
            this.m_value = data;
            this.m_valuetypeid = datatype.ToU64();

            return;
        }

        /// <summary>
        /// NT-Конструктор создания новой связи
        /// </summary>
        /// <param name="sol">Текущий солюшен</param>
        /// <param name="cellClass">Идентификатор ячейки типа ячейки</param>
        /// <param name="cellmode">Режим ячейки</param>
        /// <param name="cellState">Идентификатор ячейки состояния ячейки</param>
        /// <param name="descr">Описание ячейки</param>
        /// <param name="id">Идентификатор ячейки</param>
        /// <param name="name">Название ячейки, длиной до 128 символов</param>
        /// <param name="readOnly">Режим Только чтение</param>
        /// <remarks>
        /// 
        /// </remarks>
        public MCellB(MSolution sol, MCellId id, MCellMode cellmode, String name,
            string descr, MCellId cellClass, MCellId cellState, bool readOnly )
            : base(sol)
        {
            this.m_cellid = id.ToU64();
            this.m_cellMode = cellmode;
            this.m_name = name;
            this.m_description = descr;
            this.m_elementClass = cellClass.ToU64();
            this.m_elementState = cellState.ToU64();
            this.m_readonly = readOnly;
            //set defaults
            this.m_isActive = true;
            this.m_lastCreate = DateTime.Now;
            this.m_lastChange = DateTime.Now;
            this.m_serviceflag = 0;
            this.m_value = new Byte[0];
            this.m_valuetypeid = 0;            
            this.m_links = new MLinkCollection();

            return;
        }


        #region *** MElement property set ***

        /// <summary>
        /// NT-Идентификатор элемента
        /// </summary>
        /// <remarks>
        /// Идентификатор должен быть приведен к типу MID.
        /// </remarks>
        public override MID ID
        {
            get {   return (MID) (MCellId.FromU64(m_cellid)); }
        }

        /// <summary>
        /// NT-Название элемента
        /// </summary>
        /// <remarks>
        /// Строка названия длиной до 128 символов.
        /// Readonly: Выбросить исключение, если не временная ячейка.
        /// </remarks>
        public override string Name
        {
            get { return m_name; }
            set
            { 
                
                if (checkReadOnly()) return;//check readonly here
                String name = this.m_Solution.Settings.checkNameLength(value);
                this.m_name = name;
                if (this.m_cellMode == MCellMode.Normal)
                    this.m_Solution.DbAdapter.CellSetName(this, name);
                this.updateLastChange();//update timestamp Changed
            }
        }

        /// <summary>
        /// NT-Описание элемента
        /// String.Empty по умолчанию.
        /// 
        /// </summary>
        /// <remarks>
        /// Readonly: Выбросить исключение, если не временная ячейка.
        /// </remarks>
        public override string Description
        {
            get { return m_description; }
            set 
            {
                if (checkReadOnly()) return;//check readonly here
                this.m_description = value;
                if (this.m_cellMode == MCellMode.Normal)
                    this.m_Solution.DbAdapter.CellSetDescription(this, value);
                this.updateLastChange();//update timestamp Changed
            }
        }

        /// <summary>
        /// NT-Flag is element active or deleted 
        /// Default true
        /// </summary>
        /// <remarks>
        /// Readonly: Выбросить исключение, если не временная ячейка.
        /// </remarks>
        public override bool isActive
        {
            get { return this.m_isActive; }
            set 
            {
                if (checkReadOnly()) return;//check readonly here
                this.m_isActive = value;
                if (this.m_cellMode == MCellMode.Normal)
                    this.m_Solution.DbAdapter.CellSetActive(this, value);
                this.updateLastChange();//update timestamp Changed
            }
        }



        /// <summary>
        /// NT-Состояние элемента
        /// Бывший State
        /// </summary>
        /// <remarks>
        /// Идентификатор ячейки, описывающей состояние этого элемента
        /// Readonly: Выбросить исключение, если не временная ячейка.
        /// </remarks>
        public override MCellId ElementState
        {
            get { return MCellId.FromU64(this.m_elementState); }
            set 
            {
                if (checkReadOnly()) return;//check readonly here
                this.m_elementState = value.ToU64();
                if (this.m_cellMode == MCellMode.Normal)
                    this.m_Solution.DbAdapter.CellSetState(this, value);
                this.updateLastChange();//update timestamp Changed 
            }
        }

        //новые члены интерфейса:

        /// <summary>
        /// NT-Класс элемента
        /// Бывший TypeId
        /// </summary>
        /// <remarks>
        /// Идентификатор ячейки, описывающей класс этого элемента.
        /// Для записи в поле можно использовать internalSetElementClass(..)
        /// Хотя это и нарушение концепции.
        /// </remarks>
        public override MCellId ElementClass
        {
            get { return MCellId.FromU64(this.m_elementClass); } 
        }
        /// <summary>
        /// NT-Internal set cell class id
        /// </summary>
        /// <param name="value">Идентификатор ячейки типа ячейки</param>
        /// <remarks>
        /// Readonly: Выбросить исключение, если не временная ячейка.
        /// </remarks>
        internal void internalSetElementClass(MCellId value)
        {
            
            if (checkReadOnly()) return;//check readonly here
            this.m_elementClass = value.ToU64();
            if (this.m_cellMode == MCellMode.Normal)
                this.m_Solution.DbAdapter.CellSetClass(this, value);
            this.updateLastChange();//update timestamp Changed

            return;
        }

        /// <summary>
        /// NT-Флаг только чтение
        /// </summary>
        /// <remarks>
        /// По умолчанию = false
        /// </remarks>
        public override bool isReadOnly
        {
            get { return this.m_readonly; } 
            set 
            {
                if (this.Solution.SolutionReadOnly == true) return;//check readonly for solution only
                this.m_readonly = value;
                if (this.m_cellMode == MCellMode.Normal)
                    this.m_Solution.DbAdapter.CellSetReadOnly(this, value);
                this.updateLastChange();//update timestamp Changed
            }
        }

        /// <summary>
        /// NT-Таймштамп создания элемента
        /// Бывший CreaTime
        /// </summary>
        /// <remarks>
        /// set() не нужна, так как ячейка должна создаваться или загружаться через конструктор.
        /// в таблицу БД же значение должно записываться при сохранении ячейки.
        /// </remarks>
        public override DateTime LastCreate
        {
            get { return this.m_lastCreate; }
        }

        /// <summary>
        /// NT-Таймштамп последнего изменения элемента
        /// Бывший ModiTime
        /// </summary>
        /// <remarks>
        /// set() не нужна, так как изменения отмечаются функцией updateLastChange()
        /// в БД они заносятся непосредственно при записи в таблицу БД 
        /// хотя таймштампы в памяти и в БД будут различаться немного
        /// или при сохранении ячейки
        /// </remarks>
        public override DateTime LastChange
        {
            get { return this.m_lastChange; }
        }

        /// <summary>
        /// NT-Таймштамп последнего чтения элемента
        /// </summary>
        /// <remarks>
        /// set() не нужна так как момент отмечается функцией thisCellUsed()
        /// А в БД поле не должно храниться.
        /// </remarks>
        public override DateTime LastUsed
        {
            get { return this.m_lastUsed; } 
        }
        #endregion

        #region *** MCell Property set ***

        /// <summary>
        /// NT-Cell data value
        /// </summary>
        /// <remarks>
        /// Readonly: Выбросить исключение, если не временная ячейка.
        /// </remarks>
        public override byte[] Value
        {
            get { return this.m_value; }
            set 
            {
                if (checkReadOnly()) return;//check readonly here
                this.m_value = value;
                if (this.m_cellMode == MCellMode.Normal)
                    this.m_Solution.DbAdapter.CellSetValue(this, value);
                this.updateLastChange();//update timestamp Changed  
            }
        }

        /// <summary>
        /// NT-Cell data value type id
        /// </summary>
        /// <remarks>
        /// Readonly: Выбросить исключение, если не временная ячейка.
        /// </remarks>
        public override MCellId ValueTypeId
        {
            get { return MCellId.FromU64(m_valuetypeid); }
            set 
            {
                if (checkReadOnly()) return;//check readonly here
                this.m_valuetypeid = value.ToU64();
                if (this.m_cellMode == MCellMode.Normal)
                    this.m_Solution.DbAdapter.CellSetValueType(this, value);
                this.updateLastChange();//update timestamp Changed 
            }
        }

        /// <summary>
        /// NT-Cell link collection. 
        /// Only for link reading!
        /// </summary>
        public override MLinkCollection Links
        {
            get { return this.m_links; }
        }

        /// <summary>
        /// NT-Cell (saving) mode: Compact, Normal, DelaySave, Temporary
        /// </summary>
        /// <remarks>
        /// Режим сохранения ячейки не хранится в БД.
        /// </remarks>
        public override MCellMode CellMode
        {
            get
            {
                return m_cellMode;  
            }
            internal set
            {
                if (checkReadOnly()) return;//check readonly here
                
                if (value == MCellMode.Compact) 
                    throw new SimEngineException("Try to set Compact cell mode for MCellB cell");
                //Check Temporary state restriction
                if (((m_cellMode == MCellMode.Temporary) && (value != MCellMode.Temporary))
                    || ((value == MCellMode.Temporary) && (m_cellMode != MCellMode.Temporary)))
                {
                    throw new SimEngineException("Cell save mode change fail");
                }
                else
                {
                    m_cellMode = value;
                    this.updateLastChange();//update timestamp Changed 
                }
            }
        }

        /// <summary>
        /// NT-Current cell is MCellA cell? 
        /// Бывшая isLargeCell
        /// </summary>
        public override bool isCompactCell
        {
            get { return false; }
        }

        #endregion


       #region *** MObject serialization functions ***
        /// <summary>
        /// NR-Convert object data to binary stream
        /// </summary>
        /// <param name="writer">Binary stream writer</param>
        public override void toBinary(BinaryWriter writer)
        {
            throw new NotImplementedException();//TODO: Add code here...
        }
        /// <summary>
        /// NR-Convert object data from binary stream
        /// </summary>
        /// <param name="reader">Binary stream reader</param>
        public override void fromBinary(BinaryReader reader)
        {
            throw new NotImplementedException();//TODO: Add code here...
        }
        /// <summary>
        /// NR-Convert object data to byte array
        /// </summary>
        /// <returns></returns>
        public override byte[] toBinaryArray()
        {
            //return this.toBinaryArraySub();
            throw new NotImplementedException();//TODO: Add code here...
        }
        /// <summary>
        /// NR-Convert object data to text string
        /// </summary>
        /// <param name="withHex">True - include HEX representation of binary data</param>
        /// <returns></returns>
        public override string toTextString(bool withHex)
        {
            throw new NotImplementedException();//TODO: Add code here...
        }
        /// <summary>
        /// NR-Convert object data to text stream
        /// </summary>
        /// <param name="writer">text stream writer</param>
        /// <param name="withHex">True - include HEX representation of binary data</param>
        public override void toText(TextWriter writer, bool withHex)
        {
            throw new NotImplementedException();//TODO: Add code here...
        }
        /// <summary>
        /// NR-Convert object data from text stream
        /// </summary>
        /// <param name="reader">text stream reader</param>
        public override void fromText(TextReader reader)
        {
            throw new NotImplementedException();//TODO: Add code here...
        }
        #endregion


        //TODO: Использовать thisCellUsed(); в каждой здесь функции, если есть обращение к полям, но не к проперти.

        //TODO: при изменении ячейки также изменять таймштамп изменений 
        //через  updateLastChange() для полей: всех кроме m_cellid m_links m_lastUsed

        //TODO: при изменении ячейки учитывать флаг m_ReadOnly!
        //поля : все кроме m_cellid m_links m_lastUsed m_readonly

        //TODO: Добавить MCellA функции здесь

        /// <summary>
        /// NT-Обновить таймштамп изменения связи
        /// </summary>
        private void updateLastChange()
        {
            this.m_lastChange = DateTime.Now;
        }

        /// <summary>
        /// NT-Получить текстовое представление ячейки
        /// </summary>
        /// <returns>Возвращает текстовое представление ячейки</returns>
        /// <remarks>
        /// Readonly: Не требуется, так как нет изменения Солюшена.
        /// </remarks>
        public override string ToString()
        {
            String s = null;
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("ID={0} Name={1} Mode={2} ", this.ID.ToString(), this.Name, this.CellMode.ToString());
                sb.AppendFormat("Type={0} Data", this.ElementClass.ToString());
                sb.Append("("); sb.Append(this.ValueTypeId.ToString()); sb.Append(")");
                sb.Append("["); sb.Append(this.Value.Length.ToString()); sb.Append("]");
                sb.AppendFormat(" Links={0}", this.Links.Items.Count);
                s = sb.ToString();
                sb.Length = 0;//clear string buider
            }
            catch (Exception ex)
            {
                s = ex.GetType().Name;
            }
            return s;
        }

        /// <summary>
        /// NR-Сохранить ячейку и связи в БД. 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public override void Save()
        {
            //Readonly: Выбросить исключение
            throw new NotImplementedException();//TODO: Add code here...
        }

        /// <summary>
        /// NR-помечает ячейку удаленной. 
        /// </summary>
        public override void Delete()
        {
            //Readonly: Выбросить исключение
            throw new NotImplementedException();//TODO: Add code here...
        }

        //public void S1_Unload() - Readonly: Зависит от реализации - должна быть определена в MCell






    }
}
