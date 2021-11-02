using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Bar.Container;
using Bar.Utility;

namespace Bar
{
    //обязанности класса:
    //++++Инфраструктура
    //* [//Контейнер/Конструктор]
    //* [//Контейнер/Получить описание для отладчика]
    //* [//Контейнер/ПолеМенеджерИдентификаторовЯчеек]
    //* [//Контейнер/ПолеМенеджерИдентификаторовСвязей]
    //* [//Контейнер/ПолеКоллекцияЯчеекКонтейнера]
    //* [//Контейнер/ПолеКоллекцияСвязейКонтейнера]
    //* [//Контейнер/ПолеАдаптерБД]
    //++++Операции контейнера
    //* [//Контейнер/Получить объект Контейнера]
    //* [//Контейнер/Получить список связей ячейки из контейнера]
    //* [//Контейнер/Получить начальную ячейку Солюшена]
    //* [//Контейнер/Создать ячейку указанного класса]
    //* [//Контейнер/Получить ячейку]
    //* [//Контейнер/Удалить ячейку]
    //* [//Контейнер/Выгрузить ячейку]
    //* [//Контейнер/Проверить уникальность названия ячейки]
    //* [//Контейнер/Загрузить ячейку из БД]
    //* [//Контейнер/Удалить связи ячейки из контейнера и всех ячеек]
    //* [//Контейнер/Найти ячейки по шаблону]



    /// <summary>
    /// NR-Представляет контейнер Солюшена
    /// </summary>
    /// <remarks>
    /// Значения для интерфейса MObject берутся из объекта MSolutionSettings.
    /// </remarks>
    public class MContainer : MElement
    {

        #region *** Fields ***
        /// <summary>
        /// Обратная ссылка на Солюшен
        /// </summary>
        private MSolution m_Solution;

        /// <summary>
        /// Адаптер БД - кэш ссылки из MSolution, если он потребуется. Но лучше пользоваться им из MSolution
        /// </summary>
        private Bar.DatabaseAdapters.BaseDbAdapter m_DbAdapter;

        /// <summary>
        /// Коллекция ячеек контейнера
        /// </summary>
        private MCellCollection m_Cells;

        /// <summary>
        /// Коллекция связей контейнера
        /// </summary>
        private MContainerLinkCollection m_Links;
        /// <summary>
        /// Менеджер идентификаторов ячеек
        /// </summary>
        private MCellIdManager m_CellIdManager;

        /// <summary>
        /// Менеджер идентификаторов связей
        /// </summary>
        private MLinkIdManager m_LinkIdManager;


        #endregion

        /// <summary>
        /// NR-Конструктор
        /// </summary>
        /// <param name="solutionRef">Ссылка на объект Солюшена</param>
        public MContainer(MSolution solutionRef)
        {
            //сохранить ссылку на Солюшен
            m_Solution = solutionRef;
            //тут не брать ссылку на адаптер БД - брать в функции Open

            //создать менеджеры идентификаторов ячеек и связей
            //адаптер БД здесь сейчас не используется
            this.m_CellIdManager = new MCellIdManager(this);
            this.m_LinkIdManager = new MLinkIdManager(this);
            //коллекции ячеек и связей - не используют БД
            this.m_Cells = new MCellCollection(solutionRef);
            this.m_Links = new MContainerLinkCollection(solutionRef);

            //TODO: Add code here...
        }

        #region *** Properties ***

        /// <summary>
        /// Идентификатор солюшена 
        /// </summary>
        public int SolutionId
        {
            //берется из объекта Солюшена
            get { return this.m_Solution.SolutionId; } //TODO: если будет часто вызываться, надо будет кешировать значение в переменной класса.
        }

        /// <summary>
        /// Режим ячеек, создаваемых автоматически.
        /// </summary>
        /// <remarks>
        /// Код операций может изменять режим ячеек во время работы, настройки контейнера сохраняются в файл проекта по закрытии проекта.
        /// Readonly: Не требуется, так как нет изменения Солюшена.
        /// </remarks>
        public MCellMode DefaultCellMode
        {
            get
            {
                return this.m_Solution.Settings.ContainerDefaultCellMode;
            }
            set
            {
                this.m_Solution.Settings.ContainerDefaultCellMode = value;
            }
        }

        /// <summary>
        /// Менеджер идентификаторов ячеек
        /// </summary>
        public MCellIdManager CellIdManager
        {
            get { return m_CellIdManager; }
        }
        /// <summary>
        /// Менеджер идентификаторов связей
        /// </summary>
        public MLinkIdManager LinkIdManager
        {
            get { return m_LinkIdManager; }
        }

        /// <summary>
        /// Коллекция ячеек контейнера
        /// </summary>
        public MCellCollection Cells
        {
            get { return m_Cells; }
        }

        /// <summary>
        /// Коллекция связей контейнера
        /// </summary>
        public MContainerLinkCollection Links
        {
            get { return m_Links; }
        }

        /// <summary>
        /// Адаптер БД
        /// </summary>
        public Bar.DatabaseAdapters.BaseDbAdapter DbAdapter
        {
            get { return m_DbAdapter; }
        }
        #endregion

        #region *** MElement property set ***
        //Значения для интерфейса MElement берутся из объекта MSolutionSettings
        /// <summary>
        /// Ссылка на объект Солюшена
        /// </summary>
        public override MSolution Solution
        {
            get { return this.m_Solution; }
        }
        /// <summary>
        /// Идентификатор элемента
        /// </summary>
        /// <remarks>
        /// Идентификатор должен быть приведен к типу MID.
        /// </remarks>
        public override MID ID
        {
            get
            {
                return new MID(this.m_Solution.Settings.SolutionId, MID.InvalidIdentifierOfElement); //See MID.InvalidIdentifierOfElement = 0
            }
        }


        /// <summary>
        /// Название элемента
        /// </summary>
        /// <remarks>
        /// Строка названия длиной до 128 символов.
        /// Readonly: сохраняется в файл солюшена, который не сохраняется если не может.
        /// </remarks>
        public override string Name
        {
            get { return this.m_Solution.Settings.SolutionName; }
            set { this.m_Solution.Settings.SolutionName = this.m_Solution.Settings.checkNameLength(value); }
        }

        /// <summary>
        /// Описание элемента
        /// String.Empty по умолчанию.
        /// </summary>
        /// <remarks>
        /// Readonly: сохраняется в файл солюшена, который не сохраняется если не может.
        /// </remarks>
        public override string Description
        {
            get { return this.m_Solution.Settings.SolutionDescription; }
            set { this.m_Solution.Settings.SolutionDescription = value; }
        }

        /// <summary>
        /// Flag is element active or deleted 
        /// Default true
        /// </summary>
        /// <remarks>
        /// Readonly: сохраняется в файл солюшена, который не сохраняется если не может.
        /// </remarks>
        public override bool isActive
        {
            get { return this.m_Solution.Settings.ContainerIsActiveFlag; }
            set { this.m_Solution.Settings.ContainerIsActiveFlag = value; }
        }

        /// <summary>
        /// Сервисное значение
        /// Сохраняется в ФайлСолюшена, не учитывает флаг ReadOnly, не изменяет таймштамп изменения.
        /// </summary>
        /// <remarks>
        /// Значение используемое для поиска в графе и подобных целях. 
        /// По умолчанию = 0.
        /// Readonly: сохраняется в файл солюшена, который не сохраняется если не может.
        /// </remarks>
        public override int ServiceFlag
        {
            get { return this.m_Solution.Settings.ContainerServiceFlag; }
            set { this.m_Solution.Settings.ContainerServiceFlag = value; }
        }

        /// <summary>
        /// Состояние элемента
        /// </summary>
        /// <remarks>
        /// Идентификатор ячейки, описывающей состояние этого элемента.
        /// Readonly: сохраняется в файл солюшена, который не сохраняется если не может.
        /// </remarks>
        public override MCellId ElementState
        {
            get { return MCellId.FromU64(this.m_Solution.Settings.ContainerState); }
            set { this.m_Solution.Settings.ContainerState = value.ToU64(); }
        }

        //новые члены интерфейса:

        /// <summary>
        /// NR-Класс элемента
        /// </summary>
        /// <remarks>Идентификатор ячейки, описывающей класс этого элемента</remarks>
        public override MCellId ElementClass
        {
            get { return MCellId.FromU64(this.m_Solution.Settings.ContainerClass); }

        }

        /// <summary>
        /// NT-Флаг только чтение
        /// </summary>
        /// <remarks>
        /// По умолчанию = false.
        /// Readonly: сохраняется в файл солюшена, который не сохраняется если не может.
        /// </remarks>
        public override bool isReadOnly
        {
            get { return this.m_Solution.SolutionReadOnly; }
            set { this.m_Solution.SolutionReadOnly = value; }
        }

        /// <summary>
        /// NT-Таймштамп создания элемента
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public override DateTime LastCreate
        {
            get { return this.m_Solution.Settings.SolutionCreationDate; }

        }

        /// <summary>
        /// NT-Таймштамп последнего изменения элемента
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public override DateTime LastChange
        {
            get { return this.m_Solution.Settings.SolutionModificationDate; }
        }

        /// <summary>
        /// NT-Таймштамп последнего чтения элемента
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public override DateTime LastUsed
        {
            get { return this.m_Solution.Settings.SolutionReadingDate; }
        }


        #endregion

        /// <summary>
        /// NR-Открыть менеджер
        /// </summary>
        /// <param name="settings"></param>
        public void Open(MSolutionSettings settings)
        {
            //TODO: Add code here...
            //тут взять ссылку на адаптер БД, он уже должен быть создан и открыт.
            this.m_DbAdapter = this.m_Solution.DbAdapter;
            //clear and reload container variables and collections
            this.m_Cells.Clear();
            this.m_Links.Clear();
            this.m_CellIdManager.InitCacheValues();
            this.m_LinkIdManager.InitCacheValues();

            return;
        }

        /// <summary>
        /// NR-Закрыть менеджер
        /// </summary>
        public void Close()
        {
            //TODO: Add code here...
            this.m_Cells.Clear();
            this.m_Links.Clear();
            this.m_LinkIdManager.ClearIdCache();
            this.m_CellIdManager.ClearIdCache();

            return;
        }

        /// <summary>
        /// NT-Получить текстовое описаие объекта
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //Readonly: Не требуется, так как нет изменения Солюшена.
            return String.Format("{0} ID:{1} Cells:{2} Links:{3}", this.Name, this.ID.ToString(), this.Cells.Count, this.Links.Count);
        }

        #region *** Cell public functions ***

        //TODO: Эти заголовки функций взяты из старого Движка и не соответствуют архитектуре Тапп Бар.
        //TODO: Добавить проверки состояния солюшена в каждую функцию здесь.
        /// <summary>
        /// NT-Сохранить все временные ячейки Солюшена и их связи
        /// </summary>
        /// <exception cref="SimEngineException">Выбросить исключение, если солюшен в режиме ReadOnly.</exception>
        public void SaveAllTemporaryCells()
        {
            //Readonly: Выбросить исключение.
            if (this.m_Solution.SolutionReadOnly)
                throw new SimEngineException("Невозможно выполнить операцию, поскольку Солюшен в режиме ReadOnly");

            //сохранить ячейки
            this.m_Cells.SaveCells(MCellMode.Temporary);

            return;
        }

        /// <summary>
        /// NT-Сохранить все ячейки отложенной записи Солюшена (MCellBds).
        /// Временные ячейки не сохраняются, связи с временными ячейками не сохраняются, остаются временными.
        /// </summary>
        /// <exception cref="SimEngineException">Выбросить исключение, если солюшен в режиме ReadOnly.</exception>
        public void SaveAllDelaySavedCells()
        {
            //Readonly: Выбросить исключение.
            if (this.m_Solution.SolutionReadOnly)
                throw new SimEngineException("Невозможно выполнить операцию, поскольку Солюшен в режиме ReadOnly");

            //сохранить ячейки
            this.m_Cells.SaveCells(MCellMode.DelaySave);

            return;
        }
        
        /// <summary>
        /// NR-Получить первую же исправную ячейку Солюшена.
        /// Возвращает null если нет ячеек в Солюшене.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Устарело: Функция вспомогательная, вызывается только из навигатора, для получения начальной позиции при запуске навигатора.
        /// </remarks>
        public MCell CellGetAny()
        {
            //Устаревшая функция. Сделаю когда потребуется - сейчас гадать тяжело.
            //Readonly: Не требуется, так как нет изменения Солюшена.
            throw new NotImplementedException();//TODO: Add code here...
        }
        /// <summary>
        /// NR-Получить ячейку по ее идентификатору.
        /// Если ячейка не загружена в контейнер, то она загружается с типом, указанным в DefaultCellMode контейнера.
        /// Функция возвращает null, если ячейки с таким идентификатором нет в солюшене.
        /// </summary>
        /// <param name="cellID">Идентификатор ячейки</param>
        /// <returns>Возвращает объект ячейки или null если ячейки с таким идентификатором нет в солюшене.</returns>
        public MCell CellGet(MCellId cellID)
        {
            //Readonly: Не требуется, так как нет изменения Солюшена.
            return intGetCell(cellID);
        }


        /// <summary>
        /// NR-Получить первую же ячейку с указанным именем.
        /// Если ячейка не загружена в контейнер, то она загружается с типом, указанным в DefaultCellMode контейнера.
        /// Функция возвращает null, если ячейки с таким идентификатором нет в солюшене.
        /// </summary>
        /// <param name="title">Название ячейки</param>
        /// <returns>Возвращает объект ячейки или null если ни одной ячейки с таким названием нет в солюшене.</returns>
        public MCell CellGet(string title)
        {
            //Readonly: Не требуется, так как нет изменения Солюшена.
            return intGetCell(title);
        }



        /// <summary>
        /// NR-Загрузить ячейку строго указанного типа.
        ///- если ячейки с таким идентификатором нет в контейнере и в БД, функция возвращает нуль.
        ///- если ячейка уже загружена в контейнер, но не указанного типа, функция возвращает нуль.
        ///- если ячейка уже загружена в контейнер и она указанного типа, функция возвращает объект ячейки.
        ///- если ячейка не загружена в контейнер, функция загружает ячейку под указанным типом и возвращает объект ячейки.
        ///- если в процессе работы возникает ошибка, функция выбрасывает исключение.
        /// </summary>
        /// <param name="cellID">Идентификатор ячейки</param>
        /// <param name="cellMode">Режим ячейки</param>
        /// <returns>Возвращает объект ячейки или null, если не удалось получить ячейку требуемого типа.</returns>
        public MCell CellLoad(MCellId cellID, MCellMode cellMode)
        {
            //Readonly: Не требуется, так как нет изменения Солюшена.
            throw new NotImplementedException();//TODO: Add code here...
        }
        /// <summary>
        /// NR-Загрузить ячейку строго указанного типа.
        ///- если ячейки с таким идентификатором нет в контейнере и в БД, функция возвращает нуль.
        ///- если ячейка уже загружена в контейнер, но не указанного типа, функция возвращает нуль.
        ///- если ячейка уже загружена в контейнер и она указанного типа, функция возвращает объект ячейки.
        ///- если ячейка не загружена в контейнер, функция загружает ячейку под указанным типом и возвращает объект ячейки.
        ///- если в процессе работы возникает ошибка, функция выбрасывает исключение.
        /// </summary>
        /// <param name="cellTitle">Название ячейки</param>
        /// <param name="cellMode">Режим ячейки</param>
        /// <returns>Возвращает объект ячейки или null, если не удалось получить ячейку требуемого типа.</returns>
        public MCell CellLoad(string cellTitle, MCellMode cellMode)
        {
            //Readonly: Не требуется, так как нет изменения Солюшена.
            throw new NotImplementedException();//TODO: Add code here...
        }
        /// <summary>
        /// NR-Создать ячейку с указанным режимом ячейки. 
        /// Вернуть ссылку на ячейку или исключение.
        /// Если Солюшен без БД, попытка создать не временную ячейку выдает исключение.
        /// </summary>
        /// <param name="mode">Режим ячейки для создаваемой ячейки</param>
        /// <returns></returns>
        /// <remarks>
        /// Публичная функция, должна обрабатывать ошибки и выдавать пользователю правильные исключения. Сейчас просто набросок.  
        /// </remarks>
        public MCell CellCreate(MCellMode mode)
        {
            //Readonly: Выбросить исключение, если ячейка не временная. Иначе - выполнить функцию.
            if (this.m_Solution.SolutionReadOnly && (mode != MCellMode.Temporary))
                throw new SimEngineException("Невозможно выполнить операцию, поскольку Солюшен в режиме ReadOnly");
            //если солюшен без БД, то нельзя создавать постоянные ячейки
            if(this.m_Solution.isSolutionNoDb && (mode != MCellMode.Temporary))
                throw new SimEngineException("Невозможно выполнить операцию, поскольку Солюшен без БД");
            
            return intCreateCell(mode);
        }


        /// <summary>
        /// NR-Создать ячейку с указанным режимом ячейки. 
        /// Вернуть ссылку на ячейку или исключение.
        /// Если Солюшен без БД, попытка создать не временную ячейку выдает исключение.
        /// </summary>
        /// <param name="mode">Режим ячейки для создаваемой ячейки</param>
        /// <param name="title">Название ячейки, длиной до 128 символов</param>
        /// <param name="description">Описание ячейки</param>
        /// <returns></returns>
        /// <remarks>
        /// Это более удобная перегруженная функция для потребителя.
        /// Публичная функция, должна обрабатывать ошибки и выдавать пользователю правильные исключения. Сейчас просто набросок.  
        /// </remarks>
        public MCell CellCreate(MCellMode mode, String title, String description)
        {
            //Проверить длину названия ячейки.
            //Readonly: Выбросить исключение, если ячейка не временная. Иначе - выполнить функцию.
            throw new NotImplementedException();//TODO: Add code here...

        }
        /// <summary>
        /// NR-Получить ячейку и пометить ее неактивной. Ячейка не удаляется.
        /// </summary>
        /// <param name="cellID">Идентификатор ячейки</param>
        public void CellDelete(MCellId cellID)
        {
            //Readonly: Выбросить исключение, если ячейка не временная. Иначе - выполнить функцию.
            intDeleteCell(cellID);

            return;
        }


        /// <summary>
        /// NR-Выгрузить ячейку из памяти контейнера
        /// См. MCell.Unload()
        /// </summary>
        /// <param name="cellID">Идентификаторр ячейки</param>
        public void CellUnload(MCellId cellID)
        {
            //Readonly: Зависит от реализации
            intUnloadCell(cellID);
        }


        /// <summary>
        /// NR-Проверить что название ячейки уникальное для Солюшена
        /// </summary>
        /// <param name="cellName">Название для ячейки</param>
        /// <returns>Возвращает True если ячейки с таким именем не существует в Солюшене, False в противном случае.</returns>
        /// <remarks>
        /// Функция не изменяет ничего.
        /// Поиск имени в памяти идет по StringComparison.OrdinalIgnoreCase, а в таблице БД - определяется используемым движком СУБД.
        /// Поэтому тут нельзя выбирать способ сравнения строк.
        /// </remarks>
        public bool CellIsUniqueName(string cellName)
        {
            //Readonly: Не требуется, так как нет изменения Солюшена.
            //Нельзя задать тип сравнения строк при поиске в БД - движок СУБД ищет как умеет.
            //проверить коллекцию ячеек в памяти - быстрее
            if (m_Cells.containsName(cellName, StringComparison.OrdinalIgnoreCase)) return false;
            //проверить таблицу ячеек БД
            if (m_DbAdapter.isCellExists(cellName)) return false;

            return true;
        }

        #endregion

        #region Cell internal functions 
        /// <summary>
        /// NR-Получить ячейку по ее идентификатору.
        /// Если ячейка не загружена в контейнер, то она загружается с типом, указанным в DefaultCellMode контейнера.
        /// Функция возвращает null, если ячейки с таким идентификатором нет в солюшене.
        /// </summary>
        /// <param name="cellID">Идентификатор ячейки</param>
        /// <returns>Возвращает объект ячейки или null если ячейки с таким идентификатором нет в солюшене.</returns>
        internal MCell intGetCell(MCellId cellID)
        {
            //тут проверить что солюшен готов к работе
            throw new NotImplementedException();
        }
        /// <summary>
        /// NR-Получить первую же ячейку с указанным именем.
        /// Если ячейка не загружена в контейнер, то она загружается с типом, указанным в DefaultCellMode контейнера.
        /// Функция возвращает null, если ячейки с таким идентификатором нет в солюшене.
        /// </summary>
        /// <param name="title">Название ячейки</param>
        /// <returns>Возвращает объект ячейки или null если ни одной ячейки с таким названием нет в солюшене.</returns>
        internal MCell intGetCell(string title)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// NR-Создать ячейку с указанным режимом ячейки. 
        /// Вернуть ссылку на ячейку или исключение.
        /// Если Солюшен без БД, попытка создать не временную ячейку выдает исключение.
        /// </summary>
        /// <param name="mode">Режим ячейки для создаваемой ячейки</param>
        /// <returns></returns>
        internal MCell intCreateCell(MCellMode mode)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// NR-Получить ячейку и пометить ее неактивной. Ячейка не удаляется.
        /// </summary>
        /// <param name="cellID">Идентификатор ячейки</param>
        internal void intDeleteCell(MCellId cellID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// NT-Выгрузить ячейку из памяти по идентификатору. Если ячейка не загружена, ничего не происходит. См. MCell.Unload().
        /// </summary>
        /// <remarks>Этот код лучше исполнять здесь из-за приватных функций контейнера, а MCell.Unload() будет его вызывать. </remarks>
        internal void intUnloadCell(MCellId cellID)
        {
            //получить ячейку из коллекции ячеек контейнера 
            MCell cell = this.m_Cells.getCell(cellID);
            //если она есть, то она загружена в память и ее надо выгрузить.
            if (cell != null)
                this.intUnloadCell(cell);

            return;
        }
        /// <summary>
        /// NR-Выгрузить ячейку из памяти. См. MCell.Unload().
        /// </summary>
        /// <remarks>Этот код лучше исполнять здесь из-за приватных функций контейнера, а MCell.Unload() будет его вызывать. </remarks>
        internal void intUnloadCell(MCell cell)
        {
            throw new NotImplementedException();
        }

        #endregion

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
        /// <summary>
        /// NT-Convert object data to byte array
        /// </summary>
        /// <returns></returns>
        public override byte[] toBinaryArray()
        {
            return this.toBinaryArraySub();
        }
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

        #region *** Функции статистики контейнера ***
        /// <summary>
        /// NT-Вписать статистику для подсистемы
        /// </summary>
        /// <param name="stat">Бланк статистики</param>
        internal void getStatistics(MStatistics stat)
        {
            stat.Stat_CellsInMemory = this.m_Cells.Count;
            //TODO: пока неясно как работать с внешними ячейками, пока 0
            stat.Stat_ExternalCells = 0;
            stat.Stat_ExternalLinks = 0;
            //И надо собрать список внешних солюшенов - из БД и из Списка ячеек контейнера. Или списка связей ?
            stat.Stat_ExternalSolutionList = this.getExternalSolutionList();
            stat.Stat_LinksInMemory = this.m_Links.Count;
            stat.Stat_TemporaryCells = this.m_Cells.getNumberOfTemporaryCells();
            stat.Stat_TemporaryLinks = this.m_Links.getNumberOfTemporaryLinks();

            return;
        }
        /// <summary>
        /// NT-Собрать список внешних солюшенов из БД и из Списка ячеек контейнера и Списка связей контейнера
        /// Медленная функция
        /// </summary>
        /// <returns></returns>
        private List<int> getExternalSolutionList()
        {
            //создать коллекцию - словарь счетчиков
            UnicalInt32Collection solids = new UnicalInt32Collection();
            //подсчитываем упоминания внешних солюшенов
            //-в самом контейнере
            solids.Add(this.ElementClass.ContainerId);
            solids.Add(this.ElementState.ContainerId);
            solids.Add(this.ID.ContainerId);
            //- в содержимом контейнера
            this.m_Links.grabExternalSolutions(solids);
            this.m_Cells.grabExternalSolutions(solids);
            this.m_DbAdapter.grabExternalSolutions(solids);
            //возвращаем список. 
            //TODO: Сейчас счетчики не используются, но надо их добавить в статистику как элементы списка.
            List<int> result = solids.Items;
            solids.Clear();

            return result;
        }

        #endregion



    }//end class
}
