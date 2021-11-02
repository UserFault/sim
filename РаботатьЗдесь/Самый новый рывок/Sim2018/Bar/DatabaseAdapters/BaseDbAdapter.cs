using System;
using System.Collections.Generic;
using System.Text;
using Bar.Container;
using Bar.Utility;
using System.Reflection;

namespace Bar.DatabaseAdapters
{
    /// <summary>
    /// NR-Базовый класс адаптера БД
    /// </summary>
    [MDatabaseTypeAttribute(MDatabaseType.Unknown, false)] //атрибут для автоматической проверки доступных типов БД Солюшена
    public class BaseDbAdapter
    {
        //Аргумент true означает, что данный класс закончен и может быть использован для работы с соответствующей БД.
        //Аргумент false означает, что класс будет игнорироваться.
        //Атрибут из этого класса можно убрать, он тут только для шаблона. 
        //Он должен быть в производных классах адаптеров БД, а не здесь.
        //Если его оставить тут активным, в релизе будет светиться тип БД: Unknown. 
        //А это неправильно.

        //TODO: ServiceFlag не должен сохраняться в БД для ячеек и связей

        //TODO: Через технологию отражения проверять доступность указанного типа БД для Солюшена при его открытии.
        //Сейчас этот код находится в MUtility классе, и его надо еще доводить до рабочего состояния.    
        
        //все интерфейсные функции этого класса нужно объявить абстрактными или перегруженными или виртуальными. 
        //их реализация предполагается в классах-потомках. Но это надо разбираться комплексно со всеми функциями сразу.
        
        #region *** Fields ***
        /// <summary>
        /// Обратная ссылка на Солюшен
        /// </summary>
        private MSolution m_Solution;
        /// <summary>
        /// Флаг режима Read-only из объекта Настроек Солюшена
        /// </summary>
        private bool m_ReadOnly;


        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="solutionRef">Ссылка на объект Солюшена</param>
        public BaseDbAdapter(MSolution solutionRef)
        {
            m_Solution = solutionRef;
            
            //TODO: Add code here...
        }

        #region *** Properties ***
        /// <summary>
        /// Флаг режима Read-only
        /// </summary>
        public bool ReadOnly
        {
            get { return m_ReadOnly; }
            //set { m_ReadOnly = value; }
        }
        #endregion

        /// <summary>
        /// NT-Выбросить исключение если активен режим Только чтение
        /// </summary>
        /// <remarks>
        /// Использовать эту функцию для проверки флага "Только чтение" и завершения работы функций, изменяющих БД.
        /// </remarks>
        public void ThrowIfReadOnly()
        {
            if (this.m_ReadOnly == true)
                throw new SimEngineException("Database in Read-only mode");
            
            return;
        }

        /// <summary>
        /// NR-Открыть менеджер
        /// </summary>
        /// <param name="settings"></param>
        public void Open(MSolutionSettings settings)
        {
            //установить флаг read-only из настроек солюшена.
            //но это надо делать здесь а не в конструкторе, 
            //так как при создании адаптера еще неизвестно какой будет флаг read-only.
            //см. MSolution.SolutionOpen() функцию
            this.m_ReadOnly = this.m_Solution.SolutionReadOnly;
            //TODO: Add code here...
        }

        /// <summary>
        /// NR-Закрыть менеджер
        /// </summary>
        public void Close()
        {
            //TODO: Add code here...
        }

        public override string ToString()
        {
            return String.Format("Type={0}, State={1}", this.GetType().Name, getConnectionStateString());
        }

        /// <summary>
        /// NT-Получить строковое описание текущего состояния соединения адаптера 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Функция должна быть перегружена в производных классах.
        /// </remarks>
        protected virtual string getConnectionStateString()
        {
            return "Absent";//состояние соединения отсутствует для текущего класса
        }

        /// <summary>
        /// NR-Создать строку соединения с БД
        /// </summary>
        /// <param name="dbFile">Путь к файлу БД</param>
        /// <param name="ReadOnly">Открыть в режиме ReadOnly</param>
        public virtual string createConnectionString(string dbFile, bool ReadOnly)
        {
            ////Provider=Microsoft.Jet.OLEDB.4.0;Data Source="C:\Documents and Settings\salomatin\Мои документы\Visual Studio 2008\Projects\RadioBase\радиодетали.mdb"
            //OleDbConnectionStringBuilder b = new OleDbConnectionStringBuilder();
            //b.Provider = "Microsoft.Jet.OLEDB.4.0";
            //b.DataSource = dbFile;
            ////это только для БД на незаписываемых дисках
            //if (ReadOnly)
            //{
            //    b.Add("Mode", "Share Deny Write");
            //}
            ////user id and password can specify here
            //return b.ConnectionString;

            throw new NotImplementedException();//Этот код только для примера
        }



        /// <summary>
        /// NT-Проверить, может ли данный адаптер работать, если Солюшен находится на компакт-диске
        /// </summary>
        /// <returns>Возвращает true, если данный адаптер может работать с read-only, false - если нет.</returns>
        /// <remarks>
        /// Функция должна быть перегружена в производных классах.
        /// Текущий код только для примера, для файловых БД вроде MSAccess
        /// </remarks>
        public virtual bool isCanWorkAtReadOnly()
        {
            //1 проверить каталог БД на запись.
            //это потому, что каталог БД в солюшене может назначаться отдельно от каталога Солюшена
            //поэтому может оказаться доступным для записи, то есть, БД работать может.
            bool res1 = Bar.Utility.MUtility.isFolderWritable(this.m_Solution.Settings.SolutionDatabaseDirectory);
            //2 если каталог БД все же read-only, возвращаем значение
            //может ли мы его заставить работать здесь в read-only.
            if (res1 == true)
                return true;
            else
            {
                return false;//Я не знаю как заставить текущую БД работать в read-only режиме
            }
        }









        #region *** Link Table functions ***

        /// <summary>
        /// NR-Получить из БД макс ИД связей текущего солюшена
        /// </summary>
        /// <returns>Возвращает максимальный ИД связей текущего солюшена или 0 если нет таких связей</returns>
        internal int getMaxLinkId()
        {
            //Взять идентификатор текущего солюшена из объекта солюшена в адаптере
            throw new NotImplementedException();//TODO: Add code here...
        }

        /// <summary>
        /// NR-Получить количество связей в таблице связей
        /// </summary>
        /// <returns>Возвращает количество связей в таблице связей</returns>
        protected int getNumberOfLinks()
        {
            throw new NotImplementedException();
        }

        internal void LinkUpdateDescription(MLink link)
        {
            throw new NotImplementedException();
        }

        internal void LinkUpdateActive(MLink link)
        {
            throw new NotImplementedException();
        }

        ////ServiceFlag не сохраняется в БД
        //internal void LinkUpdateService(MLink link)
        //{
        //    throw new NotImplementedException();
        //}


        internal void LinkUpdateState(MLink link)
        {
            throw new NotImplementedException();
        }

        internal void LinkUpdateReadOnly(MLink link)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region *** Cell Table Get Functions ***

        internal string CellGetName(MCell cell)
        {
            throw new NotImplementedException();
        }

        internal string CellGetDescription(MCell cell)
        {
            throw new NotImplementedException();
        }

        internal bool CellGetActive(MCell cell)
        {
            throw new NotImplementedException();
        }
        //ServiceFlag не сохраняется в БД
        //internal int CellGetServiceFlag(MCell cell)
        //{
        //    throw new NotImplementedException();
        //}

        internal MCellId CellGetState(MCell cell)
        {
            throw new NotImplementedException();
        }

        internal MCellId CellGetClass(MCell cell)
        {
            throw new NotImplementedException();
        }

        internal bool CellGetReadOnly(MCell cell)
        {
            throw new NotImplementedException();
        }

        internal DateTime CellGetLastCreate(MCell cell)
        {
            throw new NotImplementedException();
        }

        internal DateTime CellGetLastChange(MCell cell)
        {
            throw new NotImplementedException();
        }

        internal byte[] CellGetValue(MCell cell)
        {
            throw new NotImplementedException();
        }

        internal MCellId CellGetValueType(MCell cell)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region *** Cell Table Set Functions ***
        //TODO: Тут в функциях установить новый таймштамп LastWrite в объекте ячейки
        //если она не Compact только! 
        //TODO: Тут  в функциях проверить флаг Только чтение адаптера и если не разрешено, выдать исключение.
        //if(cell.readonly || adapter.readonly) throw new exception...
        
        internal void CellSetName(MCell cell, string name)
        {
            throw new NotImplementedException();
        }

        internal void CellSetDescription(MCell cell, string value)
        {
            throw new NotImplementedException();
        }

        internal void CellSetActive(MCell cell, bool value)
        {
            throw new NotImplementedException();
        }

        internal void CellSetClass(MCellB cell, MCellId value)
        {
            throw new NotImplementedException();
        }

        //ServiceFlag не сохраняется в БД
        //internal void CellSetServiceFlag(MCell cell, int value)
        //{
        //    throw new NotImplementedException();
        //}

        internal void CellSetState(MCell cell, MCellId value)
        {
            throw new NotImplementedException();
        }

        internal void CellSetReadOnly(MCell cell, bool value)
        {
            throw new NotImplementedException();
        }

        internal void CellSetValue(MCell cell, byte[] value)
        {
            throw new NotImplementedException();
        }

        internal void CellSetValueType(MCell cell, MCellId value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region *** Cell Table Functions ***
        /// <summary>
        /// NR-
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        internal MCellB CellSelect(MCell cell, bool p)
        {
            //ServiceFlag не сохраняется в БД
            throw new NotImplementedException();
        }

        /// <summary>
        /// NR-Получить количество ячеек в таблице ячеек
        /// </summary>
        /// <returns>Возвращает количество ячеек в таблице ячеек</returns>
        protected int getNumberOfCells()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// NR-Получить из БД макс ИД ячеек текущего солюшена
        /// </summary>
        /// <returns>Возвращает максимальный ИД ячеек текущего солюшена или 0 если нет таких ячеек</returns>
        internal int getMaxCellId()
        {
            //Взять идентификатор текущего солюшена из объекта солюшена в адаптере
            throw new NotImplementedException();//TODO: Add code here...
        }

        internal bool isCellExists(string cellName)
        {
            throw new NotImplementedException();
        }
        #endregion



        #region функции для статистики 
        /// <summary>
        /// NT-Вписать статистику для подсистемы
        /// </summary>
        /// <param name="stat">Бланк статистики</param>
        internal virtual void getStatistics(MStatistics stat)
        {
            //для NoDb просто вписываем нули в статистику, так как БД пустая
            
            stat.Stat_ConstantCells = this.getNumberOfCells();
            stat.Stat_ConstantLinks = this.getNumberOfLinks();
        }


        /// <summary>
        /// NT-Выявить внешние солюшены и собрать их идентификаторы в переданный словарь
        /// </summary>
        /// <param name="solids">Словарь для сбора идентификаторов солюшенов</param>
        internal void grabExternalSolutions(UnicalInt32Collection solids)
        {
            //для NoDb просто выходим...
            
            this.grabExternalSolutionsFromCellsTable(solids);
            this.grabExternalSolutionsFromLinksTable(solids);
            return;
        }
        /// <summary>
        /// NR-Выявить солюшены и собрать их идентификаторы в переданный словарь
        /// </summary>
        /// <param name="solids">Словарь для сбора идентификаторов солюшенов</param>
        private void grabExternalSolutionsFromLinksTable(UnicalInt32Collection solids)
        {
            //надо получить все идентификаторы из каждой связи
            //если связь активная
            //и выделить ид солюшена и вписать их сюда
            //Теперь подсчитываем также и количество таких ссылок
            //solids.AddMID(curid, cell.ElementClass);
            
            throw new NotImplementedException();//TODO: Add code here...
        }
        /// <summary>
        /// NR-Выявить солюшены и собрать их идентификаторы в переданный словарь
        /// </summary>
        /// <param name="solids">Словарь для сбора идентификаторов солюшенов</param>
        private void grabExternalSolutionsFromCellsTable(UnicalInt32Collection solids)
        {
            //надо получить все идентификаторы из каждой ячейки  (4 шт в ячейке)
            //если ячейка активная
            //и выделить ид солюшена и вписать их сюда
            //Теперь подсчитываем также и количество таких ссылок
            //solids.AddMID(curid, cell.ElementClass);
            
            throw new NotImplementedException();//TODO: Add code here...
        }
        #endregion

        #region *** Функции контроля типов БД через атрибут адаптера БД ***
        /// <summary>
        /// RT-Проверить, поддерживает ли Движок указанный тип БД.
        /// </summary>
        /// <param name="dbType">Проверяемый тип БД</param>
        /// <returns>Возвращает true или false</returns>
        public static bool IsSupportedDbType(MDatabaseType dbType)
        {
            //get list of db types
            List<MDatabaseType> availableDbTypes = GetAvailableDatabaseTypes();
            //find specified db type
            return availableDbTypes.Contains(dbType);
        }


        /// <summary>
        /// NT-Получить список поддерживаемых типов БД
        /// </summary>
        /// <returns>Возвращает список поддерживаемых типов БД</returns>
        /// <remarks>Значение MDatabaseType.Unknown никогда не попадет в возвращаемый список.</remarks>
        public static List<MDatabaseType> GetAvailableDatabaseTypes()
        {
            return new List<MDatabaseType>(BaseDbAdapter.getAvailableDatabaseTypesDictionary().Keys);
        }
        /// <summary>
        /// RT-Создать объект Адаптера БД для указанного типа БД
        /// </summary>
        /// <param name="dbt">Тип базы данных</param>
        /// <param name="solutionRef">Ссылка на объект Солюшена</param>
        /// <returns>Возвращает объект Адаптера БД для указанного типа БД</returns>
        /// <remarks>
        /// Используется .NET Reflection для создания объекта Адаптера БД
        /// </remarks>
        public static BaseDbAdapter GetAdapter(MDatabaseType dbt, MSolution solutionRef)
        {
            //надо создать объект класса соответствующего типа БД
            
            Dictionary<MDatabaseType, Type> dbtypes = BaseDbAdapter.getAvailableDatabaseTypesDictionary();
            //выдать исключение если этого типа нет в списке разрешенных
            if (!dbtypes.ContainsKey(dbt))
                throw new SimEngineException(String.Format("Неподдерживаемый тип БД: {0}", dbt.ToString()));
            //извлечь тип из словаря
            Type t = dbtypes[dbt];
            Object result = t.Assembly.CreateInstance(t.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, new Object[] { solutionRef }, null, new Object[0]);
            if (result == null)
                throw new SimEngineException("Не удалось создать объект АдаптерБД");

            return (BaseDbAdapter)result;
        }

        /// <summary>
        /// RT-Получить словарь поддерживаемых типов БД
        /// </summary>
        /// <returns>Возвращает словарь поддерживаемых типов БД</returns>
        /// <remarks>Значение MDatabaseType.Unknown никогда не попадет в возвращаемый список.</remarks>
        private static Dictionary<MDatabaseType, Type> getAvailableDatabaseTypesDictionary()
        {
            //Можно было просто искать классы, производные непосредственно от BaseDbAdapter.
            //Но с атрибутами чуть удобнее - тут им можно отключать незаконченные классы адаптеров от участия в работе.
            //Поэтому решено именно атрибутом это делать. Все равно этот поиск редко проводится.

            Dictionary<MDatabaseType, Type> dict = new Dictionary<MDatabaseType, Type>();
            try
            {
                //get adapter base class type
                Type ty = typeof(BaseDbAdapter);
                //get attribute class type
                Type at = typeof(MDatabaseTypeAttribute);
                //нельзя получить производные классы, только родительские.
                //поэтому берем всю сборку и в ней перебираем все классы, ищем классы с атрибутом
                //и его значение добавляем в список
                Assembly a = ty.Assembly;
                Type[] tar = a.GetTypes();//получаем всякие типы из текущей! сборки
                foreach (Type t in tar)
                {
                    //только классы ищем, остальное игнорируем
                    if (!t.IsClass) continue;
                    //извлечь и обработать атрибут, если он есть у текущего типа (класса)
                    //если при работе с некоторым типом выбрасывается исключение, эта функция изолирует его.
                    //В результате, такой ошибконесущий тип просто игнорируется и не прерывает процесс поиска 
                    MDatabaseType mdt = extractDatabaseTypeAttribute(t, at);
                    //добавляем значение в словарь, чтобы только один раз появлялось.
                    //если вдруг значение повторится, тут словарем будет выброшено исключение.
                    //Значит, я неправильно установил атрибут у какого-то класса в движке - он повторяется.
                    if (mdt != MDatabaseType.Unknown)
                        dict.Add(mdt, t);
                }

            }
            catch (Exception ex)
            {
                //переоформить и перезапустить исключение чтобы донести факт ошибки до вызывающего кода.
                throw new SimEngineException("Ошибка при просмотре типов из сборки Движка", ex);
            }
            //тут перекидываем итемы в список и возвращаем их списком, а не словарем. 
            return dict;
        }
        /// <summary>
        /// RT-Подсобная функция извлечения и проверки атрибута из класса
        /// </summary>
        /// <param name="classType">Объект типа проверяемого класса</param>
        /// <param name="atributeType">Объект типа атрибута</param>
        /// <returns>
        /// Возвращает MDatabaseType.Unknown, если атрибут не найден или неактивен, или при работе с проверяемым классом возникла ошибка.
        /// Иначе возвращает значение MDatabaseType из атрибута.
        /// </returns>
        /// <remarks>
        /// Если при работе с некоторым типом выбрасывается исключение, эта функция изолирует его.
        /// В результате, такой ошибконесущий тип просто игнорируется и не прерывает процесс поиска. 
        /// </remarks>
        private static MDatabaseType extractDatabaseTypeAttribute(Type classType, Type atributeType)
        {
            try
            {
                //извлечь атрибут адаптера БД, если есть.
                Object[] attrs = classType.GetCustomAttributes(atributeType, false);
                foreach (Object ob in attrs)
                {
                    if (ob == null) continue;
                    //get attribute data
                    MDatabaseTypeAttribute mdta = (MDatabaseTypeAttribute)ob;
                    //add to list if Active value = true
                    if (mdta.Active == true)
                        return mdta.DatabaseType;
                    else return MDatabaseType.Unknown;
                }
            }
            catch (Exception ex)
            {
                ;//тут надо только перехватить исключение и ничего не делать с ним.
            }
            return MDatabaseType.Unknown;
        }
        #endregion



    }
}
