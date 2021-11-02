using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Bar.Container
{
    /// <summary>
    /// NR-Представляет оболочку для доступа к ячейке в БД
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public class MCellA : MCell
    {

        #region Fields
        /// <summary>
        /// Идентификатор ячейки. Остальные данные читаются из таблицы ячеек
        /// </summary>
        private UInt64 m_cellid;

        /// <summary>
        /// Флаг Только Чтение
        /// </summary>
        /// <remarks>
        /// Кеш значения из БД. Устроен как в MCellB.
        /// Здесь хранится, поскольку должен использоваться при каждом обращении к любому проперти.
        /// А по-старому это +1 запрос в таблицу ячеек - будет тормозить.
        /// </remarks>
        private bool m_ReadOnly;//TODO: Это значение должно быть загружено из БД явно при загрузке ячейки.
        #endregion

        /// <summary>
        /// NR-Конструктор 
        /// </summary>
        /// <param name="sol">Объект Солюшена</param>
        public MCellA(MSolution sol)
            : base(sol)
        {
            this.m_cellid = 0;
            this.m_ReadOnly = false;
        }


        #region *** MElement property set ***
        /// <summary>
        /// NT-Идентификатор элемента
        /// </summary>
        /// <remarks>
        /// Идентификатор должен быть приведен к типу MID.
        /// Readonly: не может быть изменено.
        /// </remarks>
        public override MID ID
        {
            get
            {
                return MCellId.FromU64(m_cellid);
            }
        }

        /// <summary>
        /// NR-Название элемента
        /// </summary>
        /// <remarks>
        /// Строка названия длиной до 128 символов.
        /// Readonly: Выбросить исключение
        /// </remarks>
        public override string Name
        {
            get
            {
                return this.m_Solution.DbAdapter.CellGetName(this);
            }
            set
            {
                if (checkReadOnly()) return;
                //check name length
                String name = this.m_Solution.Settings.checkNameLength(value);
                this.m_Solution.DbAdapter.CellSetName(this, name);
            }
        }

        /// <summary>
        /// NR-Описание элемента
        /// String.Empty по умолчанию.
        /// </summary>
        /// <remarks>
        /// Readonly: Выбросить исключение
        /// </remarks>
        public override string Description
        {
            get
            {
                return this.m_Solution.DbAdapter.CellGetDescription(this);
            }
            set
            {
                if (checkReadOnly()) return;
                this.m_Solution.DbAdapter.CellSetDescription(this, value);
            }
        }

        /// <summary>
        /// NR-Flag is element active or deleted 
        /// Default true
        /// </summary>
        /// <remarks>
        /// Readonly: Выбросить исключение
        /// </remarks>
        public override bool isActive
        {
            get
            {
                return this.m_Solution.DbAdapter.CellGetActive(this);
            }
            set
            {
                if (checkReadOnly()) return;
                this.m_Solution.DbAdapter.CellSetActive(this, value);
            }
        }

        /// <summary>
        /// NR-Состояние элемента
        /// Бывший State
        /// </summary>
        /// <remarks>
        /// Идентификатор ячейки, описывающей состояние этого элемента
        /// Readonly: Выбросить исключение
        /// </remarks>
        public override MCellId ElementState
        {
            get { return this.m_Solution.DbAdapter.CellGetState(this); }
            set
            {
                if (checkReadOnly()) return;
                this.m_Solution.DbAdapter.CellSetState(this, value);
            }
        }

        //новые члены интерфейса:

        /// <summary>
        /// NT-Класс элемента
        /// Бывший TypeId
        /// </summary>
        /// <remarks>Идентификатор ячейки, описывающей класс этого элемента</remarks>
        public override MCellId ElementClass
        {
            get { return this.m_Solution.DbAdapter.CellGetClass(this); }
        }

        /// <summary>
        /// NT-Флаг только чтение
        /// </summary>
        /// <remarks>
        /// По умолчанию = false
        /// Readonly: Проверить флаг солюшена
        /// </remarks>
        public override bool isReadOnly
        {
            get { return this.m_ReadOnly; }
            set
            {
                this.m_ReadOnly = value;
                //save changes
                if (this.m_Solution.SolutionReadOnly == false)//тут проверить только флаг солюшена
                    this.m_Solution.DbAdapter.CellSetReadOnly(this, value);
            }
        }

        /// <summary>
        /// NT-Таймштамп создания элемента
        /// Бывший CreaTime
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public override DateTime LastCreate
        {
            get { return this.m_Solution.DbAdapter.CellGetLastCreate(this); }
        }

        /// <summary>
        /// NT-Таймштамп последнего изменения элемента
        /// Бывший ModiTime
        /// </summary>
        /// <remarks>
        /// Не пишется в БД, ставится там сам по факту любой записи в БД.
        /// </remarks>
        public override DateTime LastChange
        {
            get { return this.m_Solution.DbAdapter.CellGetLastChange(this); }
        }

        /// <summary>
        /// NR-Таймштамп последнего чтения элемента
        /// </summary>
        /// <remarks>
        /// Это локальный объект, в БД не пишется
        /// </remarks>
        public override DateTime LastUsed
        {
            get { return this.m_lastUsed; }
        }

        #endregion

        #region *** MCell Property set ***


        /// <summary>
        /// NR-Cell data value
        /// </summary>
        /// <remarks>
        /// Readonly: Выбросить исключение
        /// </remarks>
        public override byte[] Value
        {
            get { return this.m_Solution.DbAdapter.CellGetValue(this); }
            set
            {
                if (checkReadOnly()) return;
                    this.m_Solution.DbAdapter.CellSetValue(this, value);
            }
        }

        /// <summary>
        /// NR-Cell data value type id
        /// </summary>
        /// <remarks>
        /// Readonly: Выбросить исключение
        /// </remarks>
        public override MCellId ValueTypeId
        {
            get { return this.m_Solution.DbAdapter.CellGetValueType(this); }
            set
            {
                if (checkReadOnly()) return;
                    this.m_Solution.DbAdapter.CellSetValueType(this, value);
            }
        }

        /// <summary>
        /// NT-Cell link collection. 
        /// Only for link reading!
        /// </summary>
        public override MLinkCollection Links
        {
            //Для MCellA это целое большое приключение - сборка одноразового списка.
            get { return this.AssemblyMCellALinks(); }
        }



        /// <summary>
        /// NT-Cell (saving) mode: Compact, Normal, DelaySave, Temporary
        /// </summary>
        /// <remarks>
        /// Readonly: нельзя изменить значение
        /// </remarks>
        public override MCellMode CellMode
        {
            get
            {
                return MCellMode.Compact;//other not supported  
            }
            internal set
            {
                if (value != MCellMode.Compact)
                    throw new SimEngineException("MCellA cannot change cell save mode");
            }
        }

        /// <summary>
        /// NT-Current cell is MCellA cell? 
        /// Бывшая isLargeCell
        /// </summary>
        public override bool isCompactCell
        {
            get { return true; }
        }

        #endregion

        #region *** MObject serialization functions ***
        /// <summary>
        /// Convert object data to binary stream
        /// </summary>
        /// <param name="writer">Binary stream writer</param>
        public override void toBinary(BinaryWriter writer)
        {
            throw new NotImplementedException();//TODO: Add code here...
        }
        /// <summary>
        /// Convert object data from binary stream
        /// </summary>
        /// <param name="reader">Binary stream reader</param>
        public override void fromBinary(BinaryReader reader)
        {
            throw new NotImplementedException();//TODO: Add code here...
        }
        /// <summary>
        /// Convert object data to byte array
        /// </summary>
        /// <returns></returns>
        public override byte[] toBinaryArray()
        {
            throw new NotImplementedException();//TODO: Add code here...
        }
        /// <summary>
        /// Convert object data to text string
        /// </summary>
        /// <param name="withHex">True - include HEX representation of binary data</param>
        /// <returns></returns>
        public override string toTextString(bool withHex)
        {
            throw new NotImplementedException();//TODO: Add code here...
        }
        /// <summary>
        /// Convert object data to text stream
        /// </summary>
        /// <param name="writer">text stream writer</param>
        /// <param name="withHex">True - include HEX representation of binary data</param>
        public override void toText(TextWriter writer, bool withHex)
        {
            throw new NotImplementedException();//TODO: Add code here...
        }
        /// <summary>
        /// Convert object data from text stream
        /// </summary>
        /// <param name="reader">text stream reader</param>
        public override void fromText(TextReader reader)
        {
            throw new NotImplementedException();//TODO: Add code here...
        }
        #endregion

        //TODO: Использовать thisCellUsed(); в каждой здесь функции, если есть обращение к полям, но не к проперти.

        //TODO: при изменении ячейки также изменять таймштамп изменений 
        //через  updateLastChange() для полей:

        //TODO: при изменении ячейки учитывать флаг m_ReadOnly!
        //поля : 

        //TODO: Добавить MCellA функции здесь

        /// <summary>
        /// NT- Сохранить ячейку и связи в БД. Для Compact ячеек ничего не делает.
        /// </summary>
        public override void Save()
        {
            //Readonly: Выбросить исключение
            
            //ничего не делать здесь
            return;
        }

        /// <summary>
        /// NR-помечает ячейку удаленной. 
        /// </summary>
        /// <remarks>
        /// Readonly: Выбросить исключение
        /// </remarks>
        public override void Delete()
        {
            //Readonly: Выбросить исключение

            //mark cell as deleted
            this.isActive = false;
            //еще чего-то?
        }

        //public void S1_Unload() - Readonly: Зависит от реализации - должна быть определена в MCell.

        /// <summary>
        /// NT-Получить текстовое представление ячейки
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Readonly: Не требуется, так как нет изменения Солюшена.
        /// </remarks>
        public override string ToString()
        {
            String s = null;
            MCellB tmpCell;
            try
            {
                //получить из таблицы всю ячейку разом.
                tmpCell = (MCellB)this.Solution.DbAdapter.CellSelect(this, true);//get cell values
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("ID={0} Name={1} Mode={2} ", this.ID.ToString(), tmpCell.Name, this.CellMode.ToString());
                sb.AppendFormat("Type={0} Data", tmpCell.ElementClass.ToString());
                sb.Append("("); sb.Append(tmpCell.ValueTypeId.ToString()); sb.Append(")");
                sb.Append("["); sb.Append(tmpCell.Value.Length.ToString()); sb.Append("]");
                //sb.AppendFormat(" Links{0}", this.Links.Items.Count); disable - too many work for MCellA
                s = sb.ToString();
                tmpCell = null;
            }
            catch (Exception ex)
            {
                s = ex.GetType().Name;
            }
            return s;
        }

        /// <summary>
        /// NR-
        /// </summary>
        /// <returns></returns>
        private MLinkCollection AssemblyMCellALinks()
        {
            throw new NotImplementedException();
        }


    }
}
