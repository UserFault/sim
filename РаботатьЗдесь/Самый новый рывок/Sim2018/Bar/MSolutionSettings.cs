using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;
using Bar.Container;
using Bar.DatabaseAdapters;
using System.Xml.Serialization;

namespace Bar
{
    //Обязанности класса:
    //* [//МенеджерСолюшена/ПропертиКаталогСолюшена]
    //* [//МенеджерСолюшена/ПропертиКаталогСнимков]
    //* [//МенеджерСолюшена/ПропертиКаталогЛогов]
    //* [//МенеджерСолюшена/ПропертиКаталогМетодов]
    //* [//МенеджерСолюшена/ПропертиКаталогРесурсов]
    //* [//МенеджерСолюшена/ПропертиКаталогБазыДанныхСолюшена]
    //* [//МенеджерСолюшена/ПропертиКаталогНастроекСолюшена]
    //* [//МенеджерСолюшена/ПропертиКаталогИконокСолюшена]
    //* [//МенеджерСолюшена/ПропертиКаталогДокументацииСолюшена]
    //* [//МенеджерСолюшена/КонстантаНазваниеКаталогаСнимков]
    //* [//МенеджерСолюшена/КонстантаНазваниеКаталогаЛогов]
    //* [//МенеджерСолюшена/КонстантаНазваниеКаталогаМетодов]
    //* [//МенеджерСолюшена/КонстантаНазваниеКаталогаРесурсов]
    //* [//МенеджерСолюшена/КонстантаНазваниеКаталогаБазыДанных]
    //* [//МенеджерСолюшена/КонстантаНазваниеКаталогаНастроек]
    //* [//МенеджерСолюшена/КонстантаНазваниеКаталогаИконок]
    //* [//МенеджерСолюшена/КонстантаНазваниеКаталогаДокументации]
    //* [//МенеджерСолюшена/КонстантаРасширениеФайлаСолюшена]
    //* [//МенеджерСолюшена/КонстантаРасширениеФайлаСнимка]
    //* [//МенеджерСолюшена/КонстантаРасширениеФайлаЛога]
    //* [//МенеджерСолюшена/КонстантаСигнатураФайлаПолногоСнимка]

    //* [//Контейнер/ПолеИдентификаторСолюшена]
    //* [//Контейнер/ПолеНазваниеСолюшена]
    //* [//Контейнер/ПолеОписаниеСолюшена]
    //* [//Контейнер/ПолеФлагАктивностиСолюшена]
    //* [//Контейнер/ПолеСервисноеЗначениеСолюшена]
    //* [//Контейнер/ПолеФлагТолькоЧтениеСолюшена]
    //* [//Контейнер/ПолеСостояниеСолюшена]
    //* [//Контейнер/ПолеРежимСозданияЯчейки]
    //* [//Контейнер/ПолеСостояниеДвижка]

    /// <summary>
    /// NR-Представляет ФайлСолюшена, хранит настройки, константы и статистику Солюшена.
    /// </summary>
    /// <remarks>
    /// В старой вики: wiki:///V:/МоиПроекты/ДвижокТапп/Справочники/ДвижокТаппВики/ДвижокТаппВики.wiki?page=НастройкиСолюшена
    /// </remarks>
    public class MSolutionSettings: MStatistics
    {
        //TODO: перенести статистику солюшена как член класса, а не наследованием. 
        //Так как записи данных стаистики в файле настроек при наследовании рассыпаются по алфавиту а не группированы в четкий раздел.
 
        #region *** Constants and Solution settings fields ***
        //Константы реализовать как поля класса:
        //В связи с мультиконтейнерностью Тапп Бар решено превратить константы-настройки Движка в поля объекта движка.
        //Поскольку константы это статические члены класса и не получится иметь два объекта Движка с разными значениями констант.
        //Поэтому в Движке теперь не будет констант-параметров Движка. Все переводится в изменяемые настройки.
        //TODO: В конце разработки: Добавить проперти для констант здесь.
        
        /// <summary>
        /// Максимальная длина названия контейнера, ячейки, связи.
        /// </summary>
        /// <remarks>
        /// Зависит от ограничений СУБД на текстовые поля в таблицах
        /// TODO: может, перенести ее в адаптер БД как переменную-свойство применяемой бд?
        /// но надо посмотреть ее использование
        /// </remarks>
        internal int Constant_ElementNameLengthMax;

            //TODO: Перенести константы Движка сюда

        /// <summary>
        /// Константа Выбрасывать исключение если модифицируется элемент с ReadOnly=true.
        /// </summary>
        /// <remarks>True - выбрасывать исключение, False - не модифицировать и не выбрасывать исключение.</remarks>
        internal bool Constant_ThrowIfReadOnly;

        #endregion

        #region *** Fields ***
        //TODO: Проверить список полей, убрать ненужные
        //TODO: Добавить поле и проперти для Идентификатор Начальной ячейки структуры мест навигатора
        //TODO: Добавить поле и проперти для Идентификатор Ячейки типа связи структуры мест навигатора
        
        
        //****** Solution properties ***********
        /// <summary>
        /// Project creation date
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private DateTime m_creadate;
        /// <summary>
        /// Solution modification timestamp
        /// </summary>
        private DateTime m_modiDate;
        /// <summary>
        /// Solution reading timestamp
        /// </summary>
        private DateTime m_readDate;
        /// <summary>
        /// Project description
        /// </summary>
        private string m_SolutionDescr;
        /// <summary>
        /// Project name
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private string m_SolutionName;
        /// <summary>
        /// Project identifier
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private int m_SolutionId;
        /// <summary>
        /// Project file pathname
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private string m_SolutionFilePath;
        /// <summary>
        /// Solution version info
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private String m_solutionVersionString;
        /// <summary>
        /// Engine version info of Solution
        /// </summary>
        private String m_solutionEngineVersionString;
        /// <summary>
        /// Solution activity state
        /// </summary>
        private MSolutionState m_SolutionState;
        /// <summary>
        /// Солюшен в режиме Только чтение
        /// </summary>
        private bool m_SolutionReadOnly;
        /// <summary>
        /// Каталог БД Солюшена
        /// </summary>
        private String m_SolutionDatabaseDirectory;
        /// <summary>
        /// Каталог снимков Солюшена
        /// </summary>
        private String m_SolutionSnapshotDirectory;
        /// <summary>
        /// Каталог лога Солюшена
        /// </summary>
        private String m_SolutionLogDirectory;
        /// <summary>
        /// Каталог методов Солюшена
        /// </summary>
        private String m_SolutionMethodDirectory;
        /// <summary>
        /// Каталог ресурсов Солюшена
        /// </summary>
        private String m_SolutionResourceDirectory;
        /// <summary>
        /// Каталог документации Солюшена
        /// </summary>
        private String m_SolutionDocumentDirectory;
        /// <summary>
        /// Кодировка солюшена
        /// </summary>
        private string m_SolutionEncoding;

        //**** container properties *****
        /// <summary>
        /// Container IsActive flag value
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private bool m_containerIsActive;
        /// <summary>
        /// Container service flag value
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private int m_containerServiceFlag;
        /// <summary>
        /// Container state identifier
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private UInt64 m_containerStateId_U64;
        /// <summary>
        /// Класс контейнера
        /// </summary>
        private UInt64 m_containerClass_U64;

        /// <summary>
        /// Log file number
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private int m_logfileNumber;
        ///// <summary>
        ///// Log details flags
        ///// </summary>
        ///// <remarks></remarks>
        ///// <value></value>
        ///// <seealso cref=""/>
        //private MMessageClass m_logdetails;
        /// <summary>
        /// Container default cell mode
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private MCellMode m_defaultCellMode;

        //******** database properties **********
        /// <summary>
        /// Project database type
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private MDatabaseType m_dbtype;
        /// <summary>
        /// Database name
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private string m_dbname;
        /// <summary>
        /// Database server path
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private string m_dbServerPath;
        /// <summary>
        /// User name of database server account
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private string m_username;
        /// <summary>
        /// User password of database server account
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private string m_userpass;
        /// <summary>
        /// Таймаут соединения с СУБД, в секундах
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private int m_dbtimeout;
        /// <summary>
        /// Database server connection port, 0 for default port
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private int m_dbport;
        /// <summary>
        /// Server uses integrated security mode (for mssql2005)
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        private bool m_useIntegratedSecurity;
        /// <summary>
        /// Таймаут запросов к СУБД, в секундах
        /// </summary>
        private Int32 m_databaseQueryTimeout;

        #endregion



        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public MSolutionSettings() : base()
        {
            //TODO: после завершения проектирования класса группировать тут поля по темам 
            //settings
            //Solution properties
            //container properties
            //database properties

            //псевдоконстанты
            this.Constant_ThrowIfReadOnly = false;
            this.Constant_ElementNameLengthMax = 128;

            //
            this.m_containerIsActive = true;
            this.m_containerServiceFlag = 0;
            this.m_containerStateId_U64 = 0;
            this.m_creadate = DateTime.Now;
            this.m_dbname = String.Empty;
            this.m_dbport = 0;
            this.m_dbServerPath = String.Empty;
            this.m_dbtimeout = 60;
            this.m_dbtype = MDatabaseType.Unknown;
            this.m_defaultCellMode = MCellMode.Temporary;//для совместимости с СолюшенБезБД
            this.m_logfileNumber = 0;
            this.m_SolutionDescr = String.Empty;
            this.m_solutionEngineVersionString = MVersion.getCurrentAssemblyVersion().ToString();
            this.m_SolutionFilePath = String.Empty;
            this.m_SolutionId = 1;
            this.m_SolutionName = String.Empty;
            this.m_SolutionState = MSolutionState.Unknown;
            this.m_solutionVersionString = "1.0.0.0";
            this.m_useIntegratedSecurity = false;
            this.m_username = String.Empty;
            this.m_userpass = String.Empty;

            this.m_SolutionReadOnly = false;//by default
            this.m_SolutionDatabaseDirectory = String.Empty;
            this.m_SolutionDocumentDirectory = String.Empty;
            this.m_SolutionLogDirectory = String.Empty;
            this.m_SolutionMethodDirectory = String.Empty;
            this.m_SolutionResourceDirectory = String.Empty;
            this.m_SolutionSnapshotDirectory = String.Empty;
            
            this.m_containerClass_U64 = 0;
            this.m_modiDate = DateTime.Now;
            this.m_readDate = DateTime.Now;

            this.m_SolutionEncoding = "windows-1251";//default encoding
            m_databaseQueryTimeout = 30;//default
            //TODO: Add code here...
        }

        /// <summary>
        /// NT-Конструктор с путем файла солюшена
        /// </summary>
        /// <param name="solutionFilePath"></param>
        public MSolutionSettings(String solutionFilePath) : this()
        {
            this.SolutionFilePath = solutionFilePath;
        }

//TODO: Проверить список проперти, переделать и убрать ненужные

        #region *** Solution properties ***
        /// <summary>
        /// NT-Каталог Солюшена
        /// </summary>
        /// <remarks>
        /// Предварительно должен быть установлен путь к Файлу Солюшена!
        /// </remarks>
        [Category("Solution folders"), Description("Solution directory path")]
        public String SolutionDirectory
        {
            get { return this.getCurrentSolutionDirectory(); }
        }
        /// <summary>
        /// NT-Каталог БД Солюшена
        /// </summary>
        /// <remarks>
        /// Предварительно должен быть установлен путь к Файлу Солюшена!
        /// </remarks>
        [Category("Solution folders"), Description("Solution database folder path")]
        public String SolutionDatabaseDirectory
        {
            get { return Bar.Utility.MUtility.makeAbsolutePath(this.getCurrentSolutionDirectory(), m_SolutionDatabaseDirectory); }
            set { m_SolutionDatabaseDirectory = Bar.Utility.MUtility.makeRelativePath(this.getCurrentSolutionDirectory(), value); }
        }
        //TODO: добавить так же другие пути каталогов Солюшена
        /// <summary>
        /// NT-Каталог Снимков Солюшена
        /// </summary>
        /// <remarks>
        /// Предварительно должен быть установлен путь к Файлу Солюшена!
        /// </remarks>
        [Category("Solution folders"), Description("Solution snapshot folder path")]
        public String SolutionSnapshotDirectory
        {
            get { return Bar.Utility.MUtility.makeAbsolutePath(this.getCurrentSolutionDirectory(), m_SolutionSnapshotDirectory); }
            set { m_SolutionSnapshotDirectory = Bar.Utility.MUtility.makeRelativePath(this.getCurrentSolutionDirectory(), value); }
        }
        /// <summary>
        /// NT-Каталог Лога Солюшена
        /// </summary>
        /// <remarks>
        /// Предварительно должен быть установлен путь к Файлу Солюшена!
        /// </remarks>
        [Category("Solution folders"), Description("Solution log folder path")]
        public String SolutionLogDirectory
        {
            get { return Bar.Utility.MUtility.makeAbsolutePath(this.getCurrentSolutionDirectory(), m_SolutionLogDirectory); }
            set { m_SolutionLogDirectory = Bar.Utility.MUtility.makeRelativePath(this.getCurrentSolutionDirectory(), value); }
        }
        /// <summary>
        /// NT-Каталог Методов Солюшена
        /// </summary>
        /// <remarks>
        /// Предварительно должен быть установлен путь к Файлу Солюшена!
        /// </remarks>
        [Category("Solution folders"), Description("Solution method folder path")]
        public String SolutionMethodDirectory
        {
            get { return Bar.Utility.MUtility.makeAbsolutePath(this.getCurrentSolutionDirectory(), m_SolutionMethodDirectory); }
            set { m_SolutionMethodDirectory = Bar.Utility.MUtility.makeRelativePath(this.getCurrentSolutionDirectory(), value); }
        }
        /// <summary>
        /// NT-Каталог Ресурсов Солюшена
        /// </summary>
        /// <remarks>
        /// Предварительно должен быть установлен путь к Файлу Солюшена!
        /// </remarks>
        [Category("Solution folders"), Description("Solution resource folder path")]
        public String SolutionResourceDirectory
        {
            get { return Bar.Utility.MUtility.makeAbsolutePath(this.getCurrentSolutionDirectory(), m_SolutionResourceDirectory); }
            set { m_SolutionResourceDirectory = Bar.Utility.MUtility.makeRelativePath(this.getCurrentSolutionDirectory(), value); }
        }
        /// <summary>
        /// NT-Каталог Документации Солюшена
        /// </summary>
        /// <remarks>
        /// Предварительно должен быть установлен путь к Файлу Солюшена!
        /// </remarks>
        [Category("Solution folders"), Description("Solution document folder path")]
        public String SolutionDocumentDirectory
        {
            get { return Bar.Utility.MUtility.makeAbsolutePath(this.getCurrentSolutionDirectory(), m_SolutionDocumentDirectory); }
            set { m_SolutionDocumentDirectory = Bar.Utility.MUtility.makeRelativePath(this.getCurrentSolutionDirectory(), value); }
        }

        /// <summary>
        /// NT-Солюшен в режиме Только чтение
        /// </summary>
        [Category("Solution properties"), Description("Solution Read-Only state")]
        public bool SolutionReadOnly
        {
            get { return m_SolutionReadOnly; }
            set { m_SolutionReadOnly = value; }
        }
        /// <summary>
        /// Solution identifier
        /// </summary>
        /// <remarks>Same as container identifier</remarks>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        [Category("Solution properties"), Description("Solution Id number")]
        public int SolutionId
        {
            get
            {
                return m_SolutionId;
            }
            set
            {
                m_SolutionId = value;
            }
        }

        /// <summary>
        /// Solution name
        /// </summary>
        /// <remarks>Same as container name</remarks>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        [Category("Solution properties"), Description("Solution name")]
        public string SolutionName
        {
            get
            {
                return m_SolutionName;
            }
            set
            {
                m_SolutionName = value;
            }
        }

        /// <summary>
        /// Описание Солюшена
        /// </summary>
        /// <remarks>Same as container description</remarks>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        [Category("Solution properties"), Description("Solution description")]
        public string SolutionDescription
        {
            get
            {
                return m_SolutionDescr;
            }
            set
            {
                m_SolutionDescr = value;
            }
        }

        /// <summary>
        /// Таймштамп создания солюшена
        /// </summary>
        [Category("Solution properties"), Description("Solution initial date")]
        public DateTime SolutionCreationDate
        {
            get
            {
                return m_creadate;
            }
            set
            {
                m_creadate = value;
            }
        }

        /// <summary>
        /// Таймштамп изменения солюшена
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        [Category("Solution properties"), Description("Solution last write timestamp")]
        public DateTime SolutionModificationDate
        {
            get
            {
                return m_modiDate;
            }
            set
            {
                m_modiDate = value;
            }
        }
        /// <summary>
        /// Таймштамп чтения солюшена
        /// </summary>
        [Category("Solution properties"), Description("Solution last reading timestamp")]
        public DateTime SolutionReadingDate
        {
            get
            {
                return m_readDate;
            }
            set
            {
                m_readDate = value;
            }
        }

        /// <summary>
        /// Solution engine version
        /// </summary>
        /// <remarks>Это проперти для кода только</remarks>
        [XmlIgnore]
        internal MVersion SolutionEngineVersion
        {
            get
            {
                return new MVersion(m_solutionEngineVersionString);
            }
            set
            {
                m_solutionEngineVersionString = value.ToString();
            }
        }
        /// <summary>
        /// Solution engine version
        /// </summary>
        /// <remarks></remarks>
        [Category("Solution properties"), Description("Version of engine where Solution is created")]
        public String SolutionEngineVersionString
        {
            get
            {
                return m_solutionEngineVersionString;
            }
            set
            {
                m_solutionEngineVersionString = value;
            }
        }

        /// <summary>
        /// Версия Солюшена
        /// </summary>
        /// <remarks>Это проперти для кода только</remarks>
        [XmlIgnore]
        internal MVersion SolutionVersion
        {
            get
            {
                return new MVersion(m_solutionVersionString);
            }
            set
            {
                m_solutionVersionString = value.ToString();
            }
        }
        /// <summary>
        /// Версия Солюшена
        /// </summary>
        [Category("Solution properties"), Description("Solution dataset version")]
        public String SolutionVersionString
        {
            get
            {
                return m_solutionVersionString;
            }
            set
            {
                m_solutionVersionString = value;
            }
        }


        /// <summary>
        /// Текущее состояние Солюшена
        /// </summary>
        [Category("Solution properties"), Description("Solution activity state")]
        public MSolutionState SolutionState
        {
            get { return m_SolutionState; }
            set { this.m_SolutionState = value; }
        }

        /// <summary>
        /// Путь файла Солюшена
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        [XmlIgnore]
        [Category("Solution properties"), Description("Solution file pathname")]
        public string SolutionFilePath
        {
            get
            {
                return m_SolutionFilePath;
            }
            set
            {
                m_SolutionFilePath = value;
            }
        }



        #endregion
        #region ** Database properties **
        /// <summary>
        /// Type of database for project
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        [Category("Database"), Description("Database server type")]
        public MDatabaseType DatabaseType
        {
            get
            {
                return m_dbtype;
            }
            set
            {
                m_dbtype = value;
            }
        }
        /// <summary>
        /// Database server path
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        [Category("Database"), Description("Database server path")]
        public string DatabaseServerPath
        {
            get
            {
                return m_dbServerPath;
            }
            set
            {
                m_dbServerPath = value;
            }
        }

        /// <summary>
        /// Database name
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        [Category("Database"), Description("Project database name")]
        public string DatabaseName
        {
            get
            {
                return m_dbname;
            }
            set
            {
                m_dbname = value;
            }
        }

        /// <summary>
        /// Database server port number, 0 for default
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        [Category("Database"), Description("Database server port number. Specify 0 for default value.")]
        public int DatabasePortNumber
        {
            get
            {
                return m_dbport;
            }
            set
            {
                m_dbport = value;
            }
        }
        /// <summary>
        /// Connection timeout, sec
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        [Category("Database"), Description("Database server connection timeout in seconds")]
        public int DatabaseTimeout
        {
            get
            {
                return m_dbtimeout;
            }
            set
            {
                m_dbtimeout = value;
            }
        }

        /// <summary>
        /// User name of database server account
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        [Category("Database"), Description("Database server account user name. For project creation, user must have dbcreator permission on SQL server")]
        public string UserName
        {
            get
            {
                return m_username;
            }
            set
            {
                m_username = value;
            }
        }

        /// <summary>
        /// User password of database server account
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        [Category("Database"), Description("Database server account user password")]
        public string UserPassword
        {
            get
            {
                return m_userpass;
            }
            set
            {
                m_userpass = value;
            }
        }

        /// <summary>
        /// Flag use integrated security for MsSql2005
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        [Category("Database"), Description("Database server uses IntegratedSecurity autorization mode. For MsSql2005 only.")]
        public bool UseIntegratedSecurity
        {
            get
            {
                return m_useIntegratedSecurity;
            }
            set
            {
                m_useIntegratedSecurity = value;
            }
        }
        #endregion
        #region ** Container Properties **
        /// <summary>
        /// Container state identifier as U64-packed value
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        [Category("Container properties"), Description("Container state identifier packed to U64")]
        public UInt64 ContainerState
        {
            get
            {
                return m_containerStateId_U64;
            }
            set
            {
                m_containerStateId_U64 = value;
            }
        }

        /// <summary>
        /// Container class identifier as U64-packed value
        /// </summary>
        [Category("Container properties"), Description("Container class identifier packed to U64")]
        public UInt64 ContainerClass
        {
            get
            {
                return m_containerClass_U64;
            }
            set
            {
                m_containerClass_U64 = value;
            }
        }

        /// <summary>
        /// Container Is Active flag
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        [Category("Container properties"), Description("Container is active flag value")]
        public bool ContainerIsActiveFlag
        {
            get
            {
                return m_containerIsActive;
            }
            set
            {
                m_containerIsActive = value;
            }
        }

        /// <summary>
        /// Container service flag value
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        [Category("Container properties"), Description("Container service flag value")]
        public int ContainerServiceFlag
        {
            get
            {
                return m_containerServiceFlag;
            }
            set
            {
                m_containerServiceFlag = value;
            }
        }

        /// <summary>
        /// Container default cell mode для неявно загружаемых из БД ячеек
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        [Category("Container properties"), Description("Container default cell mode")]
        public MCellMode ContainerDefaultCellMode
        {
            get
            {
                return m_defaultCellMode;
            }
            set
            {
                m_defaultCellMode = value;
            }
        }


        /// <summary>
        /// Log file number
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        [Category("Container properties"), Description("Log file incremental number")]
        public int LogfileNumber
        {
            get
            {
                return m_logfileNumber;
            }
            set
            {
                m_logfileNumber = value;
            }
        }

        ///// <summary>
        ///// Log details flas
        ///// </summary>
        ///// <remarks></remarks>
        ///// <value></value>
        ///// <seealso cref=""/>
        //[Category("Container properties"), Description("Log details flags")]
        //public MMessageClass LogDetailsFlags
        //{
        //    get
        //    {
        //        return m_logdetails;
        //    }
        //    set
        //    {
        //        m_logdetails = value;
        //    }
        //}
        #endregion

        #region *** Solution settings properties ***
        /// <summary>
        /// NT-Текущая версия Движка,реализованная в коде этой сборки
        /// </summary>
        public MVersion CurrentEngineVersion
        {
            get { return MVersion.getCurrentAssemblyVersion(); }
        }

        #endregion



        /// <summary>
        /// Название кодировки Солюшена
        /// </summary>
        public String SolutionEncodingName
        {
            get
            {
                return this.m_SolutionEncoding;
            }
        }

        /// <summary>
        /// NT-Таймаут запросов к СУБД в секундах
        /// </summary>
        public int DatabaseQueryTimeout
        {
            get
            {
                return this.m_databaseQueryTimeout;
            }
            set
            {
                m_databaseQueryTimeout = value;
            }
        }

        /// <summary>
        /// NT-Создать или перезаписать Файл Солюшена, записать значения и закрыть файл.
        /// </summary>
        /// <remarks>
        /// Без записи в лог, или проверять его существование!
        /// Сохранение должно производиться при закрытии Солюшена, но если Солюшен в режиме Только чтение, то сохранение не должно производиться!
        /// Это уже реализовано в MSolution.Close(), но напоминаю здесь.
        /// 
        /// </remarks>
        public void Save()//TODO: тестировать выгрузку и загрузку данных
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(this.GetType());
            System.IO.StreamWriter file = new System.IO.StreamWriter(this.m_SolutionFilePath);
            writer.Serialize(file, this);
            file.Close();
        }

        /// <summary>
        /// NT-Открыть Файл Солюшена, загрузить значения и закрыть файл.
        /// </summary>
        /// <remarks>Без записи в лог, или проверять его существование!</remarks>
        /// <returns>Возвращает информацию о проекте, хранящуюся в файле проекта.</returns>
        public static MSolutionSettings Load(string filename)
        {
            //load file
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(MSolutionSettings));
            System.IO.StreamReader file = new System.IO.StreamReader(filename);
            MSolutionSettings result = (MSolutionSettings)reader.Deserialize(file);
            file.Close();
            //установить текущий путь к файлу солюшена вместо старого
            result.m_SolutionFilePath = filename;
            return result;
        }

        //Эти функции принесены из старой версии и работоспособны.

        /// <summary>
        /// Get current solution directory from Solution file pathname
        /// </summary>
        /// <returns>Возвращает текущий каталог, в котором находится сейчас файл солюшена.</returns>
        /// <remarks></remarks>
        /// <seealso cref=""/>
        public string getCurrentSolutionDirectory()
        {
            return Path.GetDirectoryName(this.m_SolutionFilePath);
        }

        ///// <summary>
        ///// Получить имя проекта, укороченное до 16 символов.
        ///// </summary>
        ///// <returns>Возвращает строку имени проекта, обрезанную до первых 16 символов.</returns>
        ///// <remarks></remarks>
        ///// <seealso cref=""/>
        //public string getSolutionName16()
        //{
        //    string res = this.m_SolutionName.Trim();
        //    if (res.Length > 16)
        //        res = res.Remove(16);
        //    return res;
        //}
        ///// <summary>
        ///// Is project uses database?
        ///// </summary>
        ///// <returns>Returns true if project use database. False otherwise.</returns>
        ///// <remarks></remarks>
        ///// <seealso cref=""/>
        //public bool IsDBused()
        //{
        //    return (m_dbtype != MDatabaseType.NoDatabase);
        //}

        ///// <summary>
        ///// NT-Проверяет данные проекта и выбрасывает исключения при ошибках
        ///// </summary>
        //internal void checkValues()
        //{
        //    //check project name exists
        //    m_SolutionName = MUtility.processString(m_SolutionName, 256);
        //    if (String.IsNullOrEmpty(this.m_SolutionName))
        //        throw new Exception("Invalid project name");
        //    //check project description
        //    m_SolutionDescr = MUtility.processString(m_SolutionDescr, 8192);
        //    //check engine version
        //    //if (MVersionInfo.IsCompatibleVersion(getProjectVersion()) == false)//TODO: TAGVERSIONNEW: - переделано
        //    if (!this.m_currentEngineVersion.isCompatibleVersion(this.m_solutionEngineVersion))
        //        throw new Exception("Несовместимая версия движка проекта");
        //    //check database fields
        //    if (m_dbtype == MDatabaseType.Unknown)
        //        throw new Exception("Invalid database type");
        //    if (m_dbtype != MDatabaseType.NoDatabase)
        //    {
        //        m_dbServerPath = MUtility.processString(m_dbServerPath, 8192);  //TODO: set correct length here
        //        if (String.IsNullOrEmpty(m_dbServerPath))
        //            throw new Exception("Invalid path to database server");
        //        //Если имя БД не задано, используем 16 символов имени проекта
        //        m_dbname = MUtility.processString(m_dbname, 256); //TODO: set correct length here
        //        if (String.IsNullOrEmpty(m_dbname))
        //            m_dbname = getSolutionName16();
        //        //process user name. Имя пользователя и пароль могут быть пустыми.
        //        //В этом случае они вводятся в момент создания строки подключения.
        //        //пароль не обрабатываем, чтобы не вырезать пробельные символы
        //        //todo: тут надо дополнительно проработать процесс
        //        m_username = MUtility.processString(m_username, 256); //TODO: set correct length here
        //        //таймаут значение не менее 10 секунд
        //        if (m_dbtimeout < 10) m_dbtimeout = 10;
        //    }
        //    else
        //    {
        //        //проверки специфичные для проекта без БД
        //        if (m_defaultCellMode != MCellMode.Temporary)
        //            throw new Exception("Тип ячеек в ContainerDefaultCellMode не подходит для проекта");
        //    }
        //    return;
        //}


        /// <summary>
        /// NT-Проверить, что длина имени не превышает установленного предела.
        /// Если это не так, выбрасывается исключение.
        /// </summary>
        /// <param name="elementName">Имя элемента</param>
        /// <returns>Возвращает имя элемента. Пробелы на концах обрезаются функцией String.Trim()</returns>
        /// <exception cref="NullReferenceException">Когда имя элемента = null</exception>
        /// <exception cref="SimEngineException">Когда длина имени элемента больше установленного предела</exception>
        public String checkNameLength(String elementName)
        {
            if (elementName == null)
                throw new NullReferenceException("Element name is null");

            String t = elementName.Trim();
            if (t.Length > this.Constant_ElementNameLengthMax)
                throw new SimEngineException("Длина имени элемента больше установленного предела");

            return t;
        }

        /// <summary>
        /// NR-Get string representation of object.
        /// </summary>
        /// <returns>Return string representation of object.</returns>
        public override string ToString()
        {
            return base.ToString();//TODO: Add code here...
        }


       #region *** MObject serialization functions ***
        /// <summary>
        /// NR-Convert object data to binary stream
        /// </summary>
        /// <param name="writer">Binary stream writer</param>
        public override void toBinary(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// NR-Convert object data from binary stream
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
        /// NR-Convert object data to text string
        /// </summary>
        /// <param name="withHex">True - include HEX representation of binary data</param>
        /// <returns></returns>
        public override string toTextString(bool withHex)
        {
            throw new NotImplementedException();//TODO: Добавить обобщенный наследуемый код сериализации здесь
        }
        /// <summary>
        /// NR-Convert object data to text stream
        /// </summary>
        /// <param name="writer">text stream writer</param>
        /// <param name="withHex">True - include HEX representation of binary data</param>
        public override void toText(TextWriter writer, bool withHex)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// NR-Convert object data from text stream
        /// </summary>
        /// <param name="reader">text stream reader</param>
        public override void fromText(TextReader reader)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
