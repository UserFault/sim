using System;

namespace Bar.Container
{
    /// <summary>
    /// NT-Базовый класс менеджера кэша идентификаторов ячеек и связей в контейнере
    /// </summary>
    /// <remarks> См wiki:///V:/МоиПроекты/ДвижокТапп/Справочники/ДвижокТаппВики/ДвижокТаппВики.wiki?page=МенеджерИдентификаторовЭлементов</remarks>
    public class MBaseIdManager
    {
        
        #region *** Fields ***
        /// <summary>
        /// Обратная ссылка на контейнер
        /// </summary>
        /// <remarks>
        /// Нужна для доступа к коллекции ячеек контейнера и коллекции связей контейнера и Адаптеру БД.
        /// Должна инициализироваться при создании объекта.
        /// </remarks>
        protected MContainer m_Container;

        /// <summary>
        /// Максимальный существующий временный идентификатор
        /// </summary>
        /// <remarks>Кеширует идентификатор для ускорения выдачи идентификаторов</remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private int m_maxTempId;
        /// <summary>
        /// Максимальный существующий постоянный идентификатор
        /// </summary>
        /// <remarks>Кеширует идентификатор для ускорения выдачи идентификаторов</remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private int m_maxConstId;

        #endregion
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="container">Ссылка на объект контейнера</param>
        public MBaseIdManager(MContainer container)
        {
            this.m_Container = container;
            this.m_maxConstId = 0;
            this.m_maxTempId = 0;
        }

        /// <summary>
        /// NT-Clear all id cash variables
        /// </summary>
        /// <remarks>For use in ProjectClear function</remarks>
        internal void ClearIdCache()
        {
            m_maxConstId = 0;
            m_maxTempId = 0;
        }

        /// <summary>
        /// NT-Загрузить существующие значения для кеша из БД, когда контейнер открывается
        /// </summary>
        internal void InitCacheValues()
        {
            m_maxConstId = 0;
            m_maxTempId = 0;
            intGetMaxConstId(); //получить макс постоянный ид из таблицы
            intGetMaxTempId(); //получить макс временный ид из коллекции элементов в памяти
        }

        /// <summary>
        /// NT-Получить максимальный ИД постоянных элементов из БД
        /// </summary>
        /// <remarks>Вызывает перегруженную функцию потомка, которая и читает из правильного источника</remarks>
        /// <returns></returns>
        protected int intGetMaxConstId()
        {
            if (m_maxConstId == 0)
                m_maxConstId = OnGetMaxConstElementId();
            return m_maxConstId;
        }

        /// <summary>
        /// NT-Получить максимальный ИД постоянных элементов
        /// </summary>
        /// <remarks>Вызывает перегруженную функцию потомка, которая и читает из правильного источника</remarks>
        /// <returns></returns>
        protected int intGetMaxTempId()
        {
            if (m_maxTempId == 0)
                m_maxTempId = OnGetMaxTempElementId();
            return m_maxTempId;
        }

        #region *** Prototype for override ***
        //TODO: Тестировать, убедиться что перегруженные функции вызываются как ожидается.
        /// <summary>
        /// Override this function in child classes to get max const id
        /// </summary>
        /// <returns></returns>
        protected virtual int OnGetMaxConstElementId()
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }
        /// <summary>
        /// Override this function in child classes to get max temp id
        /// </summary>
        /// <returns></returns>
        protected virtual int OnGetMaxTempElementId()
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }
        #endregion


        /// <summary>
        /// NT-Изменяет значение соответствующего кеша для временной или постоянной ячейки
        /// </summary>
        /// <param name="elementid">Identifier of new element</param>
        /// <remarks>
        /// Для временной ячейки должен вызываться при создании ячейки около добавления в список ячеек.
        /// Для постоянной ячейки после записи ячейки в БД
        /// </remarks>
        internal void ChangeIdCashOnCreateElement(int elementid)
        {
            if (MID.isTemporaryId(elementid))
                m_maxTempId = elementid;
            else
                m_maxConstId = elementid;
        }

        /// <summary>
        /// NT-Изменяет значение соответствующего кеша для временного или постоянного элемента
        /// </summary>
        /// <param name="newId">Identifier of new element</param>
        /// <remarks>
        /// Для временной ячейки должен вызываться при создании ячейки около добавления в список ячеек.
        /// Для постоянной ячейки после записи ячейки в БД
        /// </remarks>
        internal void ChangeIdCashOnCreateElement(MID newId)
        {
            this.ChangeIdCashOnCreateElement(newId.ElementId);
        }

        /// <summary>
        /// NT-Изменяет значение соответствующего кеша для временного элемента.
        /// </summary>
        /// <param name="elementid">Identifier of new element</param>
        internal void ChangeIdCashOnRemoveTempElement(int elementid)
        {
            //Проверка что ид временных ячеек - вообще-то не должно быть вызовов для постоянных ячеек, поэтому TODO: после отладки движка убрать проверку.
            if (!MID.isTemporaryId(elementid)) throw new Exception("Invalid cell identifier");
            //Если удаляемая ячейка имеет наибольший ИД, сбрасываем кеш для последующего пересчета ИД.
            if (m_maxTempId == elementid) m_maxTempId = 0;
        }

        /// <summary>
        /// NT-Изменяет значение соответствующего кеша для временного элемента.
        /// </summary>
        /// <param name="oldId">Identifier of element</param>
        internal void ChangeIdCashOnRemoveTempElement(MID oldId)
        {
            this.ChangeIdCashOnRemoveTempElement(oldId.ElementId);
        }
        /// <summary>
        /// NT-Изменяет значение соответствующего кеша для постоянного элемента.
        /// </summary>
        /// <param name="elementid">Identifier of new element</param>
        /// <remarks>Use by Optimizer only</remarks>
        internal void ChangeIdCashOnRemoveConstElement(int elementid)
        {
            //Проверка что ид постоянных ячеек - вообще-то не должно быть вызовов для временных ячеек, поэтому TODO: после отладки движка убрать проверку.
            if (MID.isTemporaryId(elementid)) throw new Exception("Invalid cell identifier");
            //Если удаляемая ячейка имеет наибольший ИД, сбрасываем кеш для последующего пересчета ИД.
            if (m_maxConstId == elementid) m_maxConstId = 0;
        }

        /// <summary>
        /// NT-Returns id for new element without update cash values
        /// Cash values must be updated after succesful element creation
        /// </summary>
        /// <param name="forTempElement">True for temporary element, False for constant element.</param>
        /// <returns></returns>
        /// <remarks>
        /// Напрямую не должна вызываться, поэтому объявлена приватной.
        /// (И вообще она тут в качестве шаблона для подклассов)
        /// Вместо нее должны вызываться подобные функции классов MCellId и MLinkId
        /// </remarks>
        private MID getNewElementId(bool forTempElement)
        {
            int t = 0;
            if (forTempElement)
            {
                t = intGetMaxTempId();
                t = MID.getNewTempId(t);
            }
            else
            {
                t = intGetMaxConstId();
                t = MID.getNewConstId(t);
            }
            //return local identifier
            return new MID(this.m_Container.SolutionId, t);
        }
        /// <summary>
        /// NT-Проверить что Идентификатор элемента принадлежит текущему Солюшену
        /// </summary>
        /// <param name="id">Идентификатор элемента</param>
        /// <returns>Возвращает true или false</returns>
        public bool isLocalID(MID id)
        {
            int solutionId = this.m_Container.SolutionId;//get local solution id number
            return (id.ContainerId == solutionId);
        }


        /// <summary>
        /// NT-Get string representation of object.
        /// </summary>
        /// <returns>Return string representation of object.</returns>
        /// <remarks></remarks>
        /// <seealso cref=""/>
        public override string ToString()
        {
            return String.Format("cst={0} tmp={1}", m_maxConstId, m_maxTempId);
        }

    }
    
    /// <summary>
    /// NT-Класс менеджера кэша идентификаторов связей в контейнере
    /// </summary>
    public class MLinkIdManager: MBaseIdManager
    {
        /// <summary>
        /// NT-Конструктор
        /// </summary>
        /// <param name="container"></param>
        public MLinkIdManager(MContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// NT-Returns id for new element without update cash values
        /// Cash values must be updated after succesful element creation
        /// </summary>
        /// <param name="forTempElement">True for temporary element, False for constant element.</param>
        /// <returns></returns>
        internal MLinkId getNewLinkId(bool forTempElement)
        {
            int t = 0;
            if (forTempElement)
            {
                t = intGetMaxTempId();
                t = MID.getNewTempId(t);
            }
            else
            {
                t = intGetMaxConstId();
                t = MID.getNewConstId(t);
            }
            //return local identifier
            return new MLinkId(this.m_Container.SolutionId, t);
        }


        /// <summary>
        /// NT-Get max ID of existing constant links (from links table)
        /// </summary>
        /// Постоянные связи всегда существуют в таблице БД, поэтому там их и считаем.
        /// Если связей нет, возвращает 0.
        /// <returns></returns>
        protected override int OnGetMaxConstElementId()
        {
            //Получить из БД макс ИД связей
            return this.m_Container.DbAdapter.getMaxLinkId();
        }
        /// <summary>
        /// NT-Get max ID of existing temporary links (from links collection in memory)
        /// </summary>
        /// <remarks>
        /// Временные связи существуют только в памяти, поэтому там их и считаем.
        /// Если связей нет, возвращает 0.
        /// </remarks>
        /// <returns></returns>
        protected override int OnGetMaxTempElementId()
        {
            //Получить из коллекции связей контейнера макс ИД временных связей
            return this.m_Container.Links.getMaxTempLinkID();
        }



    }

    /// <summary>
    /// NT-Класс менеджера кэша идентификаторов ячеек в контейнере
    /// </summary>
    public class MCellIdManager : MBaseIdManager
    {
        /// <summary>
        /// NT-Конструктор
        /// </summary>
        /// <param name="container"></param>
        public MCellIdManager(MContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// NT-Returns id for new element without update cash values
        /// Cash values must be updated after succesful element creation
        /// </summary>
        /// <param name="forTempElement">True for temporary element, False for constant element.</param>
        /// <returns></returns>
        internal MCellId getNewCellId(bool forTempElement)
        {
            int t = 0;
            if (forTempElement)
            {
                t = intGetMaxTempId();
                t = MID.getNewTempId(t);
            }
            else
            {
                t = intGetMaxConstId();
                t = MID.getNewConstId(t);
            }
            //return local identifier
            return new MCellId(this.m_Container.SolutionId, t);
        }

        /// <summary>
        /// NT-Get max ID of existing constant cells (from cells table)
        /// </summary>
        /// Постоянные ячейки всегда существуют в таблице, поэтому там их и считаем.
        /// Если ячеек нет, возвращает 0.
        /// <returns></returns>
        protected override int OnGetMaxConstElementId()
        {
            //Получить из БД макс ИД ячеек
            return this.m_Container.DbAdapter.getMaxCellId();
        }
        /// <summary>
        /// NT-Get max ID of existing temporary cells (from cells collection in memory)
        /// </summary>
        /// <remarks>
        /// Временные ячейки существуют только в памяти, поэтому там их и считаем.
        /// Если ячеек нет, возвращает 0.
        /// </remarks>
        /// <returns></returns>
        protected override int OnGetMaxTempElementId()
        {
            //Получить из коллекции ячеек контейнера макс ИД временных ячеек
            return this.m_Container.Cells.getMaxTempCellID();
        }

    }
}
