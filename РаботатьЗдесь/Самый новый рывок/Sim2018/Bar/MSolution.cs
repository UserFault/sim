using System;
using System.Collections.Generic;
using System.Text;
using Bar.DatabaseAdapters;
using System.IO;

namespace Bar
{

    //Обязанности Солюшена:
    //* [//МенеджерСолюшена/Создать структуру каталогов Солюшена]   - должна быть приватная функция
    //* [//МенеджерСолюшена/Удалить каталог]                        - нету, это будет Удалить Солюшен. Удаление каталога надо реализовать в MUtility.
    //++++Поля-ссылки менеджеров
    //* [//Контейнер/ПолеФайлСолюшена]
    //* [//Контейнер/ПолеМенеджерСолюшена]
    //* [//Контейнер/ПолеМенеджерЛога]
    //* [//Контейнер/ПолеМенеджерСнимков]
    //* [//Контейнер/ПолеМенеджерРесурсов]
    //* [//Контейнер/ПолеМенеджерМетодов]
    //* [//Контейнер/ПолеМенеджерСети]
    //++++Операции солюшена - проверены



    /// <summary>
    /// NR-Представляет Солюшен и собственно Движок для клиентов Движка
    /// </summary>
    public class MSolution
    {

        ////Напоминание: MSolutionRefCollection.AddSolutionReference(..);//TODO: отправить сюда ссылку на солюшен при его создании или открытии.

        #region Fields



        /// <summary>
        /// Менеджер контейнера
        /// </summary>
        private MContainer m_Container;


        /// <summary>
        /// Менеджер подсистемы ресурсов
        /// </summary>
        private MResourceManager m_ResourceManager;


        /// <summary>
        /// Менеджер подсистемы методов
        /// </summary>
        private MMethodManager m_MethodManager;


        /// <summary>
        /// Менеджер подсистемы снимков
        /// </summary>
        private MSnapshotManager m_SnapshotManager;


        /// <summary>
        /// Менеджер подсистемы лога
        /// </summary>
        private MLogManager m_LogManager;


        /// <summary>
        /// Настройки  Солюшена и константы Движка
        /// </summary>
        private MSolutionSettings m_Settings;


        /// <summary>
        /// Менеджер подсистемы БД
        /// </summary>
        private Bar.DatabaseAdapters.BaseDbAdapter m_DbAdapter;

        #endregion

        /// <summary>
        /// Конструктор объекта Солюшена
        /// </summary>
        public MSolution()
        {
           
            //TODO: Add code here...
        }

        /// <summary>
        /// Деструктор
        /// </summary>
        ~MSolution()
        {
            //TODO: Add code here...
        }

        #region Properties
        /// <summary>
        /// Менеджер контейнера
        /// </summary>
        public MContainer Container
        {
            get { return m_Container; }
            set { m_Container = value; }
        }
        /// <summary>
        /// Менеджер подсистемы ресурсов
        /// </summary>
        public MResourceManager Resources
        {
            get { return m_ResourceManager; }
            set { m_ResourceManager = value; }
        }
        /// <summary>
        /// Менеджер подсистемы методов
        /// </summary>
        public MMethodManager Methods
        {
            get { return m_MethodManager; }
            set { m_MethodManager = value; }
        }
        /// <summary>
        /// Менеджер подсистемы снимков
        /// </summary>
        public MSnapshotManager Snapshots
        {
            get { return m_SnapshotManager; }
            set { m_SnapshotManager = value; }
        }
        /// <summary>
        /// Менеджер подсистемы лога
        /// </summary>
        public MLogManager Logs
        {
            get { return m_LogManager; }
            set { m_LogManager = value; }
        }
        /// <summary>
        /// Настройки  Солюшена и константы Движка
        /// </summary>
        public MSolutionSettings Settings
        {
            get { return m_Settings; }
            set { m_Settings = value; }
        }
        /// <summary>
        /// Менеджер подсистемы БД
        /// </summary>
        public Bar.DatabaseAdapters.BaseDbAdapter DbAdapter
        {
            get { return m_DbAdapter; }
            set { m_DbAdapter = value; }
        }
        /// <summary>
        /// Идентификатор солюшена 
        /// </summary>
        public int SolutionId
        {
            get { return this.m_Settings.SolutionId; } //TODO: если будет часто вызываться, надо будет кешировать значение в переменной класса.
        }
        /// <summary>
        /// Состояние Солюшена
        /// </summary>
        public MSolutionState SolutionState
        {
            get { return m_Settings.SolutionState; }
            set { this.m_Settings.SolutionState = value; }
        }
        /// <summary>
        /// Солюшен в режиме Только чтение
        /// </summary>
        /// <remarks>
        /// Проперти берет значение из Настроек Солюшена
        /// </remarks>
        public bool SolutionReadOnly
        {
            get { return m_Settings.SolutionReadOnly; } //TODO: если будет часто вызываться, надо будет кешировать значение в переменной класса.
            set { this.m_Settings.SolutionReadOnly = value; }
        }
        /// <summary>
        /// NT-Флаг что Солюшен готов к работе
        /// </summary>
        public bool isReady
        {
            get { return (this.m_Settings.SolutionState == MSolutionState.Ready); }
        }
        /// <summary>
        /// NT-Флаг что Солюшен без БД
        /// </summary>
        public bool isSolutionNoDb
        {
            get { return (this.m_Settings.DatabaseType == MDatabaseType.NoDatabase); }
        }

        #endregion



        /// <summary>
        /// NR-Создать новый Солюшен
        /// </summary>
        /// <param name="rootFolder">Родительский каталог для каталога Солюшена</param>
        /// <param name="info">Информация о создаваемом солюшене</param>
        public void SolutionCreate(string rootFolder, MSolutionSettings info)
        {
            //Readonly: Выбросить исключение.

            //throw new NotImplementedException();//TODO: Add code here...
        }


        /// <summary>
        /// NR-Открыть Солюшен
        /// </summary>
        /// <param name="solutionFilePath">Путь к Файлу Солюшена</param>
        /// <param name="readOnly">Открыть Солюшен в режиме Только чтение</param>
        public void SolutionOpen(string solutionFilePath, bool readOnly)
        {
            //ReadOnly: Открыть Солюшен в режиме Только Чтение.
            
            //1) load settings file
            MSolutionSettings sett = MSolutionSettings.Load(solutionFilePath);
            //2) Создать то, что нельзя было создать в конструкторе
            this.m_Settings = sett;
            this.m_Container = new MContainer(this);
            this.m_DbAdapter = BaseDbAdapter.GetAdapter(sett.DatabaseType, this);
            this.m_LogManager = new MLogManager(this);
            this.m_MethodManager = new MMethodManager(this);
            this.m_ResourceManager = new MResourceManager(this);
            this.m_SnapshotManager = new MSnapshotManager(this);

            //3) Добавить ссылку на солюшен в MSolutionRefCollection при открытии солюшена
            MSolutionRefCollection.AddSolutionReference(this);

            //Поддержка режима Только Чтение в Солюшене
            //сохранить текущий ReadOnly режим в настройках Солюшена и открывать солюшен дальше.
            this.m_Settings.SolutionReadOnly = this.checkReadOnlyMode(readOnly);    

            this.m_DbAdapter.Open(sett);//тут уже надо знать режим только-чтение

            //throw new NotImplementedException();//TODO: Add code here...
        }
 
        /// <summary>
        /// NR-Открыть Солюшен
        /// </summary>
        /// <param name="solutionFilePath">Путь к Файлу Солюшена</param>
        public void SolutionOpen(string solutionFilePath)
        {
            SolutionOpen(solutionFilePath, false);
        }

        /// <summary>
        /// NR-Закрыть Солюшен
        /// </summary>
        /// <remarks></remarks>
        /// <param name="withSave">Выполнять сохранение Солюшена перед закрытием</param>
        public void SolutionClose(bool withSave)
        {
            //Readonly: реализована в вызываемых функциях.
            
            //Удалить ссылку на солюшен из MSolutionRefCollection при закрытии солюшена
            MSolutionRefCollection.RemoveSolutionReference(this, true);//false - игнорировать отсутствие, true - исключение при отсутствии.
            
            
            //Сохранить файл настроек Солюшена, если каталог Солюшена допускает Сохранение
            if (Bar.Utility.MUtility.isFolderWritable(this.m_Settings.getCurrentSolutionDirectory()))
            {
                //обновить статистику солюшена перед закрытием ?
                intGetStatistics(this.m_Settings);//вписать статистику в текущие настройки солюшена
                //сохранить настройки и статистику солюшена
                this.m_Settings.Save();
            }
            //throw new NotImplementedException();//TODO: Add code here...

            GC.Collect();//освободить память принудительно
            return;
        }

        /// <summary>
        /// NR-Сохранить состояние Солюшена
        /// </summary>
        /// <remarks>
        /// Сохранять Солюшен можно только в устойчивых состояниях процесса.
        /// Ксли Солюшен не использует БД, сохранить Солюшен можно только при помощи моментального снимка.
        /// </remarks>
        /// <exception cref="SimEngineException">Выбросить исключение, если солюшен в режиме ReadOnly.</exception>
        /// <exception cref="SimEngineException">Выбросить исключение, если солюшен не готов к работе.</exception>
        public void SolutionSave()
        {
            //сначала надо проверить что Солюшен не закрыт, проверив флаг СостояниеСолюшена.
            if (!this.isReady)//выбросить исключение, если солюшен не готов к работе
                throw new SimEngineException(String.Format("Солюшен не готов к работе: SolutionState={0}", this.SolutionState.ToString()));

            //Readonly: если ячеек отложенной записи нет, то сохранять нечего и можно игнорировать операцию. 
            //Если же есть, то надо выбрасывать исключение. 
            //Версия данных Солюшена не изменится, но и изменений в Солшене не было ведь?
            if (this.SolutionReadOnly)
                throw new SimEngineException("Невозможно сохранить Солюшен, поскольку он в режиме ReadOnly");

            //throw new NotImplementedException();//TODO: Add code here...
        }

        /// <summary>
        /// NR-Очистить содержимое Солюшена.
        /// </summary>
        /// <remarks>
        /// Удалить ячейки и связи из памяти и таблиц БД. 
        /// Название Солюшена, описание, идентификатор и другие значения не изменятся.
        /// </remarks>
        /// <exception cref="SimEngineException">Выбросить исключение, если солюшен в режиме ReadOnly.</exception>
        /// <exception cref="SimEngineException">Выбросить исключение, если солюшен не готов к работе.</exception>
        public void SolutionClear()
        {
            //throw new NotImplementedException();//TODO: Add code here...
            //сначала надо проверить что Солюшен не закрыт, проверив флаг СостояниеСолюшена.
            if (!this.isReady)//выбросить исключение, если солюшен не готов к работе
                throw new SimEngineException(String.Format("Солюшен не готов к работе: SolutionState={0}", this.SolutionState.ToString()));
            
            //Readonly: Выбросить исключение.
            if (this.SolutionReadOnly)
                throw new SimEngineException("Невозможно очистить Солюшен, поскольку он в режиме ReadOnly");

            GC.Collect();//освободить память принудительно
            return;
        }

        /// <summary>
        /// NR-Удалить Солюшен с диска и из сервера БД
        /// </summary>
        /// <param name="solutionFilePath">Путь к Файлу Солюшена</param>
        /// <returns>Возвращает true, если Солюшен успешно удален или его каталог не существует.
        /// Возвращает false, если удалить Солюшен не удалось по какой-либо причине.</returns>
        public static bool SolutionDelete(string solutionFilePath)
        {
            //Readonly: Выбросить исключение.
            //проверить что Каталог Солюшена доступен на запись.
            string solutionFolder = Path.GetDirectoryName(solutionFilePath);
            bool result = false;
            //TODO: но еще надо удалить БД из сервера СУБД, если она серверная.
            //Если это возможно сделать с текущими правами пользователя, что вряд ли и неправильно.
            
            //удалить папку солюшена в корзину! чтобы ее можно было восстановить если что.
            
            result = Bar.Utility.ShellFileOperations.DeleteFile(solutionFolder);

            return result;
        }

        /// <summary>
        /// NR-Оптимизировать Солюшен
        /// </summary>
        /// <remarks>Основная функция запуска оптимизатора в процессе работы. Пока неясно, что она делает.</remarks>
        /// <exception cref="SimEngineException">Выбросить исключение, если солюшен не готов к работе.</exception>
        public void SolutionOptimize()
        {

            //сначала надо проверить что Солюшен не закрыт, проверив флаг СостояниеСолюшена.
            if (!this.isReady)//выбросить исключение, если солюшен не готов к работе
                throw new SimEngineException(String.Format("Солюшен не готов к работе: SolutionState={0}", this.SolutionState.ToString()));

            //Readonly: Выбросить исключение.
            if (this.SolutionReadOnly)
                throw new SimEngineException("Невозможно оптимизировать Солюшен, поскольку он в режиме ReadOnly");


            //Тут надо вызвать сборку мусора, чтобы очистить память от удаленных объектов. Это долго.

            //Тут можно удалить из памяти неактивные ячейки и их связи, если они есть. 
            //Они не должны загружаться в память, но они могли возникнуть в процессе работы.

            //Тут также можно удалять неактивные ячейки и связи из БД, если пользователь это затребовал.
            //Дефрагментацию пространства идентификаторов делать нельзя, так как концепция мультиконтейнерности 
            //не позволяет установить, используется ли каждая конкретная ячейка в других контейнерах.

            //throw new NotImplementedException();//TODO: Add code here...
            return;
        }

        /// <summary>
        /// NR-Получить Статистику Солюшена
        /// </summary>
        /// <returns>
        /// Возвращает статистику Солюшена.
        /// Статистика Солюшена входит в состав Настроек Солюшена 
        /// и обновляется при закрытии Солюшена или при вызове этой функции.
        /// Статистика Солюшена входит в состав Файла Полного Снимка Солюшена и обновляется перед его созданием.
        /// </returns>
        /// <exception cref="SimEngineException">Выбросить исключение, если солюшен не готов к работе.</exception>
        public MStatistics SolutionGetStatistics()
        {
            //Readonly: Не требуется, так как нет изменения Солюшена.
            
            MStatistics stat = new MStatistics();
            //Можно было бы перенести этот код в сам объект статистики.
            //Но тут быстрее и проще заполнять это все.
            //Так как все под рукой и в контексте Солюшена.
            this.intGetStatistics(stat);

            return stat; 
        }

        /// <summary>
        /// NR-Вписать статистику для подсистемы
        /// </summary>
        /// <param name="stat">Бланк статистики</param>
        /// <exception cref="SimEngineException">Выбросить исключение, если солюшен не готов к работе.</exception>
        internal void intGetStatistics(MStatistics stat)
        {
            //Readonly: Не требуется, так как нет изменения Солюшена.
            
            //сначала надо проверить что Солюшен не закрыт, проверив флаг СостояниеСолюшена.
            if (!this.isReady)//выбросить исключение, если солюшен не готов к работе
                throw new SimEngineException(String.Format("Солюшен не готов к работе: SolutionState={0}", this.SolutionState.ToString()));

            //затем надо подсчитать эту статистику и вписать ее в переданный объект
            this.m_Container.getStatistics(stat);
            this.m_DbAdapter.getStatistics(stat);
            this.Resources.getStatistics(stat);
            this.Methods.getStatistics(stat);
            this.Snapshots.getStatistics(stat);
            this.m_LogManager.getStatistics(stat);

            return;
        }
        /// <summary>
        /// NT-Получить строковое описание для отладчика
        /// </summary>
        /// <returns>Возвращает строковое описание для отладчика</returns>
        public override string ToString()
        {
            //Readonly: Не требуется, так как нет изменения Солюшена.

            return String.Format("Id={0}; State={1};", this.SolutionId, this.SolutionState.ToString());
        }

        #region *** Служебные функции ***
        /// <summary>
        /// NT-Провести проверку режима ТолькоЧтение Солюшена при открытии Солюшена
        /// </summary>
        /// <param name="readOnly">Флаг режима ТолькоЧтение, желаемый пользователем</param>
        /// <returns>Возвращает флаг режима ТолькоЧтение Солюшена</returns>
        private bool checkReadOnlyMode(bool readOnly)
        {
            //Если пользователь указал флаг Только чтение, то проверка, может ли солюшен изменяться, не делается.
            if (readOnly == true) return true;
            //Если пользователь не указал флаг Только чтение, то проверка делается. 
            //По ее результатам устанавливается флаг Только чтение.
            bool result = false;
            //* Если Каталог Солюшена не позволяет создать файл.
            bool res1 = Bar.Utility.MUtility.isFolderWritable(this.m_Settings.getCurrentSolutionDirectory());
            //* или Если БД Солюшена не может быть открыта в режим только чтение.
            bool res2 = this.m_DbAdapter.isCanWorkAtReadOnly();
            //поскольку каталог с БД может находиться вне каталога Солюшена, то она может оказаться вполне работоспособной.
            //но сейчас это неважно - если каталог Солюшена недоступен для записи, то весь солюшен считается Read-only.
            //потом это можно будет переделать, если потребуется.

            result = (!(res1 && res2));
            return result;
        }


        #endregion

    }
}
