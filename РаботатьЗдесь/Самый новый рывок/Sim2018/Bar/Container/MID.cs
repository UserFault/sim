using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Container
{
    //Функция public bool isLocalID(MID id) доступна в классе MBaseIdManager и производных, 
    //так как в нем есть объект Контейнера с Идентификатором Солюшена, и он управляет идентификаторами элементов. 

    //Функция internal static MID CreateLocalID(int t) IsLocalId() упразднена. Если надо, ее можно реализовать в классе MBaseIdManager
    
    /// <summary>
    /// NR-Базовый класс Идентификатора связи или ячейки
    /// </summary>
    public class MID
    {
        //Задачи:
        //TODO: сделать функции:
        //* Создать локальный идентификатор элемента
        //  - тут нужен ид солюшена, а его здесь неоткуда взять. Это проблема.
        //* Проверить что идентификатор локального элемента
        //  - тут нужен ид солюшена, а его здесь неоткуда взять.  Это проблема.
        //* Сериализация в двоичный поток - сейчас используется перевод в ulong, сериализация не используется. 
        //* Сериализация из двоичного потока - сейчас используется перевод из ulong, сериализация не используется.

        /// <summary>
        /// Константа Неправильный идентификатор элемента
        /// </summary>
        public const int InvalidIdentifierOfElement = 0;//Не переносить константу в настройки солюшена

        #region *** Fields ***
        /// <summary>
        /// Element identifier number
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private int m_elementid;
        /// <summary>
        /// Container identifier number
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private int m_containerid;
        #endregion

        /// <summary>
        /// NT-Конструктор по умолчанию
        /// </summary>
        public MID()
        {
            m_elementid = 0;
            m_containerid = 0;
        }

        /// <summary>
        /// NT-Конструктор с параметрами
        /// </summary>
        /// <param name="containerId">Container ID number</param>
        /// <param name="elementId">Element ID number</param>
        public MID(int containerId, int elementId)
        {
            m_containerid = containerId;
            m_elementid = elementId;
        }

        /// <summary>
        /// NT-Конструктор копии
        /// </summary>
        /// <param name="copy">Source object</param>
        public MID(MID copy)
        {
            m_elementid = copy.m_elementid;
            m_containerid = copy.m_containerid;
        }

        #region *** Properties ***
        /// <summary>
        /// Container identifier number
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        public int ContainerId
        {
            get { return m_containerid; }
            set { m_containerid = value; }
        }
        /// <summary>
        /// Element identifier number
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        public int ElementId
        {
            get { return m_elementid; }
            set { m_elementid = value; }
        }
        

        //если надо определять ИД ячейки или связи, то можно сделать в классах override проперти IsLinkID IsCellID
        //и в них возвращать true false. Чтобы не тратить память на хранение флага и не проверять типы объектов в коде.
        
        /// <summary>
        /// Флаг, что объект - Идентификатор связи
        /// </summary>
        /// <remarks>Значение флага не хранится в памяти</remarks>
        public virtual bool IsLinkID
        {
            get { return false; }
        }
        /// <summary>
        /// Флаг, что объект - Идентификатор ячейки
        /// </summary>
        /// <remarks>Значение флага не хранится в памяти</remarks>
        public virtual bool IsCellID
        {
            get { return false; }
        }
        #endregion

        /// <summary>
        /// NT-Получить неправильный идентификатор элемента.
        /// </summary>
        /// <returns>Возвращает объект - неправильный идентификатор элемента.</returns>
        public static MID getInvalidIdentifier()
        {
            return new MID(MID.InvalidIdentifierOfElement, MID.InvalidIdentifierOfElement);
        }

        /// <summary>
        /// NT-проверить допустимость идентификатора, выбросить исключение при недопустимости.
        /// </summary>
        /// <param name="cellid">cell id for check</param>
        public static void checkID(MID id)
        {
            //1 проверить что ИД не нуль
            if (id == null) 
                throw new ArgumentNullException("id");
            //2 проверить допустимость идентификатора
            id.checkID();
        }
        /// <summary>
        /// NT-проверить допустимость идентификатора, выбросить исключение при недопустимости.
        /// </summary>
        public void checkID()
        {
            //1 проверить что ИД солюшена допустимый, но не обязательно локальный.
            //ИД солюшена должен быть положительным целым числом, больше 0.
            if ((this.m_containerid < 1) || (this.m_containerid == Int32.MaxValue))
                throw new SimEngineException("Недопустимый идентификатор контейнера в идентификаторе элемента: " + this.ToString());
            //2 проверить что ид элемента допустимый
            if (this.m_elementid == 0)
                throw new SimEngineException("Недопустимый идентификатор элемента");
            else if ((this.m_elementid == Int32.MaxValue) || (this.m_elementid == Int32.MinValue))
                throw new SimEngineException("Предельный идентификатор элемента");

            return;
        }

        /// <summary>
        /// NT-Check that current Id is Temporary
        /// </summary>
        /// <returns>Returns True if Id is temporary, False otherwise</returns>
        public bool isTemporaryId()
        {
            return (m_elementid < 0);
        }

        /// <summary>
        /// NT-Check that specified element identifier is Temporary
        /// </summary>
        /// <param name="elementid">element id value</param>
        /// <returns>Returns True if specified element id is Temporary element, False otherwise.</returns>
        public static bool isTemporaryId(int elementid)
        {
            return (elementid < 0);
        }

        //TODO: Рассмотреть, можно ли заменить эти функции getNewXxId() или перенести или объединить с менеджерами идентификаторов Контейнера.
        //Где они используются, есть ли там доступ к контейнеру.

        /// <summary>
        /// NT-Get new element id for new constant element
        /// </summary>
        /// <param name="maxId">max of existing constant element id</param>
        /// <returns>Returns new element id for new constant element</returns>
        public static int getNewConstId(int maxId)
        {
            return maxId + 1;
        }

        /// <summary>
        /// NT-Get new element id for new temporary element
        /// </summary>
        /// <param name="maxId">max of existing temporary element id</param>
        /// <returns>Returns new element id for new temporary element</returns>
        public static int getNewTempId(int maxId)
        {
            return maxId - 1;
        }

        /// <summary>
        /// NTReturn true if identifiers is equal
        /// </summary>
        /// <param name="tid">Compared identifier</param>
        /// <returns>Returns True if identifiers is equals, False otherwise</returns>
        public bool isEqual(MID tid)
        {
            return ((tid.m_elementid == this.m_elementid) && (tid.m_containerid == this.m_containerid));
        }


        /// <summary>
        /// NT-Unpack element identifier from U64 value
        /// </summary>
        /// <param name="id">Packed to U64 identifier</param>
        /// <returns>Returns unpacked element identifier.</returns>
        public static MID FromU64(UInt64 id)
        {
            Int32 elementid = (Int32)(id & 0xFFFFFFFF);
            Int32 containerid = (Int32)(id >> 32);
            return new MID(containerid, elementid);
        }
        /// <summary>
        /// NT-Pack element identifier to U64 value
        /// </summary>
        /// <returns>Returns packed element identifier as U64 value.</returns>
        public UInt64 ToU64()
        {
            UInt64 res = 0;
            res = res | ((UInt64)((UInt32)this.m_containerid));
            res = res << 32;
            res = res | ((UInt64)((UInt32)this.m_elementid));
            return res;
        }

        /// <summary>
        /// NT-Get string representation of object
        /// </summary>
        /// <returns>Returns string representation of object, like 0:0</returns>
        public override string ToString()
        {
            return String.Format("{0}:{1}", m_containerid, m_elementid);
        }
    }


    /// <summary>
    /// NT-Класс Идентификатора ячейки
    /// </summary>
    public class MCellId : MID
    {
        /// <summary>
        /// NT-Конструктор по умолчанию
        /// </summary>
        public MCellId(): base()
        {
        }
        /// <summary>
        /// NT-Конструктор с параметрами
        /// </summary>
        /// <param name="containerId">Container ID number</param>
        /// <param name="elementId">Element ID number</param>
        public MCellId(int containerId, int elementId)
            :base(containerId, elementId)
        {
        }

        /// <summary>
        /// NT-Конструктор копии
        /// </summary>
        /// <param name="copy">Source object</param>
        public MCellId(MID copy): base(copy)
        {
        }


        /// <summary>
        /// Флаг, что объект - Идентификатор связи
        /// </summary>
        /// <remarks>Значение флага не хранится в памяти</remarks>
        public override bool IsLinkID
        {
            get { return false; }
        }
        /// <summary>
        /// Флаг, что объект - Идентификатор ячейки
        /// </summary>
        /// <remarks>Значение флага не хранится в памяти</remarks>
        public override bool IsCellID
        {
            get { return true; }
        }

        /// <summary>
        /// NT-Unpack element identifier from U64 value
        /// </summary>
        /// <param name="id">Packed to U64 identifier</param>
        /// <returns>Returns unpacked element identifier.</returns>
        public static new MCellId FromU64(UInt64 id)
        {
            Int32 elementid = (Int32)(id & 0xFFFFFFFF);
            Int32 containerid = (Int32)(id >> 32);
            return new MCellId(containerid, elementid);
        }

    }


    /// <summary>
    /// NT-Класс Идентификатора связи 
    /// </summary>
    public class MLinkId : MID
    {
        /// <summary>
        /// NT-Конструктор по умолчанию
        /// </summary>
        public MLinkId() :base()
        {
        }
        /// <summary>
        /// NT-Конструктор с параметрами
        /// </summary>
        /// <param name="containerId">Container ID number</param>
        /// <param name="elementId">Element ID number</param>
        public MLinkId(int containerId, int elementId): base(containerId, elementId)
        {
        }

        /// <summary>
        /// NT-Конструктор копии
        /// </summary>
        /// <param name="copy">Source object</param>
        public MLinkId(MID copy): base(copy)
        {
        }
        /// <summary>
        /// Флаг, что объект - Идентификатор связи
        /// </summary>
        /// <remarks>Значение флага не хранится в памяти</remarks>
        public override bool IsLinkID
        {
            get { return true; }
        }
        /// <summary>
        /// Флаг, что объект - Идентификатор ячейки
        /// </summary>
        /// <remarks>Значение флага не хранится в памяти</remarks>
        public override bool IsCellID
        {
            get { return false; }
        }

        /// <summary>
        /// NT-Unpack element identifier from U64 value
        /// </summary>
        /// <param name="id">Packed to U64 identifier</param>
        /// <returns>Returns unpacked element identifier.</returns>
        public static new MLinkId FromU64(UInt64 id)
        {
            Int32 elementid = (Int32)(id & 0xFFFFFFFF);
            Int32 containerid = (Int32)(id >> 32);
            return new MLinkId(containerid, elementid);
        }
    }
}
