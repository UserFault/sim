using System;
using System.IO;
using Bar.Container;
using System.Text;

namespace Bar
{
    /// <summary>
    /// RT-Обеспечивает единообразие полей для Связей, Ячеек и Контейнера
    /// </summary>
    public abstract class MElement: MObject
    {
        #region *** MElement property set ***
        /// <summary>
        /// Ссылка на объект Солюшена
        /// </summary>
        public abstract MSolution Solution
        {
            get;
        }

        /// <summary>
        /// Идентификатор элемента
        /// </summary>
        /// <remarks>
        /// Идентификатор должен быть приведен к типу MID.
        /// </remarks>
        public abstract MID ID
        {
            get;
        }

        /// <summary>
        /// Название элемента
        /// </summary>
        /// <remarks>Строка названия длиной до 128 символов.</remarks>
        public abstract string Name
        {
            get;
            set;
        }
        
        /// <summary>
        /// Описание элемента
        /// String.Empty по умолчанию.
        /// </summary>
        public abstract string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Flag is element active or deleted 
        /// Default true
        /// </summary>
        public abstract bool isActive
        {
            get;
            set;
        }

        /// <summary>
        /// Сервисное значение
        /// </summary>
        /// <remarks>
        /// Значение используемое для поиска в графе и подобных целях. 
        /// По умолчанию = 0.
        /// </remarks>
        public abstract Int32 ServiceFlag
        {
            get;
            set;
        }

        /// <summary>
        /// Состояние элемента
        /// </summary>
        /// <remarks>Идентификатор ячейки, описывающей состояние этого элемента</remarks>
        public abstract MCellId ElementState
        {
            get;
            set;
        }

        /// <summary>
        /// Класс элемента
        /// </summary>
        /// <remarks>Идентификатор ячейки, описывающей класс этого элемента</remarks>
        public abstract MCellId ElementClass
        {
            get;
        }

        /// <summary>
        /// Флаг только чтение
        /// </summary>
        /// <remarks>
        /// По умолчанию = false
        /// </remarks>
        public abstract bool isReadOnly
        {
            get;
            set;
        }

        /// <summary>
        /// Таймштамп создания элемента
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public abstract DateTime LastCreate
        {
            get;
        }

        /// <summary>
        /// Таймштамп последнего изменения элемента
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public abstract DateTime LastChange
        {
            get;
        }

        /// <summary>
        /// Таймштамп последнего чтения элемента
        /// </summary>
        /// <remarks>
        /// Нужен чтобы выгружать давно не используемые ячейки из коллекции ячеек солюшена
        /// </remarks>
        public abstract DateTime LastUsed
        {
            get;
        }

        #endregion




        #region MObject Members
        /// <summary>
        /// NR-Convert object data to binary stream
        /// </summary>
        /// <param name="writer">Binary stream writer</param>
        public override void toBinary(BinaryWriter writer)
        {
            throw new NotImplementedException();//Определить в производных классах
        }
        /// <summary>
        /// NR-Convert object data from binary stream
        /// </summary>
        /// <param name="reader">Binary stream reader</param>
        public override void fromBinary(BinaryReader reader)
        {
            throw new NotImplementedException();//Определить в производных классах
        }
        /// <summary>
        /// NR-Convert object data to byte array
        /// </summary>
        /// <returns></returns>
        public override byte[] toBinaryArray()
        {
            throw new NotImplementedException();//Определить в производных классах
        }
        /// <summary>
        /// NR-Convert object data to text string
        /// </summary>
        /// <param name="withHex">True - include HEX representation of binary data</param>
        /// <returns></returns>
        public override string toTextString(bool withHex)
        {
            throw new NotImplementedException();//Определить в производных классах
        }
        /// <summary>
        /// NR-Convert object data to text stream
        /// </summary>
        /// <param name="writer">text stream writer</param>
        /// <param name="withHex">True - include HEX representation of binary data</param>
        public override void toText(TextWriter writer, bool withHex)
        {
            throw new NotImplementedException();//Определить в производных классах
        }
        /// <summary>
        /// NR-Convert object data from text stream
        /// </summary>
        /// <param name="reader">text stream reader</param>
        public override void fromText(TextReader reader)
        {
            throw new NotImplementedException();//Определить в производных классах
        }
        #endregion

        /// <summary>
        /// NT-Проверить что элемент временный. 
        /// Локальность элемента не проверяется.
        /// </summary>
        public bool isTemporary
        {
            get { return this.ID.isTemporaryId(); }
        }

        /// <summary>
        /// NT-Проверить что элемент принадлежит текущему контейнеру
        /// </summary>
        public bool isLocal
        {
            get { return (this.ID.ContainerId == this.Solution.SolutionId); }
        }

        /// <summary>
        /// NT-Проверить что элемент постоянный и локальный
        /// </summary>
        public bool isConstAndLocal
        {
            get
            {
                return ((!this.ID.isTemporaryId()) && (this.ID.ContainerId == this.Solution.SolutionId));
            }
        }


        /// <summary>
        /// NT-Вспомогательная функция для Mobject.toBinaryArray()
        /// </summary>
        /// <returns></returns>
        protected virtual byte[] toBinaryArraySub()
        {
            //create memory stream and writer
            MemoryStream ms = new MemoryStream(64);//initial size for cell data 
            BinaryWriter bw = new BinaryWriter(ms, Encoding.Unicode);
            //convert data
            this.toBinary(bw);
            //close memory stream and get bytes
            bw.Close();
            return ms.ToArray();
        }

        /// <summary>
        /// NT-Проверить флаг ReadOnly в объекте и в Солюшене и выбросить исключение если надо.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns>Возвращает true если запись запрещена, false если запись разрешена</returns>
        /// <exception cref="SimEngineException">Нельзя изменить read-only элемент.</exception>
        protected bool checkReadOnly()
        {
            
            ////fast return if writable
            //if ((this.isReadOnly == false) && (this.Solution.SolutionReadOnly == false))
            //    return false;
            //else
            //{
            //    //check and throw
            //    if (this.Solution.Settings.Constant_ThrowIfReadOnly == true)
            //        throw new SimEngineException("Нельзя изменить read-only элемент");
            //    else
            //        return true;
            //}

            //Решено только выбрасывать исключение, игнорировать не будем. Поэтому флаг Constant_ThrowIfReadOnly не нужен.
            //1 проверить флаг элемента
            if(this.isReadOnly == true)
                throw new SimEngineException("Нельзя изменить read-only элемент");
            //2 проверить флаг солюшена
            else if (this.Solution.SolutionReadOnly == true)
                throw new SimEngineException("Невозможно выполнить операцию, поскольку Солюшен в режиме ReadOnly");
            else
                return false;//выйти 
        }

    }
}
