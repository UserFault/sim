using System;
using System.Collections.Generic;
using System.Text;

namespace Bar
{
    //Обязанности класса:
    //++++Операции со снимками
    //* [//Контейнер/Загрузить из полного снимка]
    //* [//Контейнер/Сохранить в полный снимок]
    //* [//Контейнер/ПолучитьСчетчикВнешнихСолюшенов]
    //* [//МенеджерСнимков/Создать полный снимок]
    //* [//МенеджерСнимков/Загрузить солюшен из полного снимка]
    //* [//МенеджерСнимков/Создать уникальное имя для нового полного снимка]
    //* [//МенеджерСнимков/Проверить наличие снимка в каталоге снимков]
    //* [//МенеджерСнимков/Удалить снимок по номеру снимка]
    //* [//МенеджерСнимков/Создать путь для полного снимка по его номеру]
    //* [//МенеджерСнимков/Получить самый свежий снимок в каталоге снимков]
    //* [//МенеджерСнимков/КонстантаВерсияПодсистемыСнимков]
    //* [//МенеджерСнимков/ПроверитьВерсиюПодсистемыСнимков]
    
    /// <summary>
    /// NR-Представляет подсистему снимков Солюшена
    /// </summary>
    public class MSnapshotManager
    {
        #region *** Fields ***
        /// <summary>
        /// Обратная ссылка на Солюшен
        /// </summary>
        private MSolution m_Solution;

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="solutionRef">Ссылка на объект Солюшена</param>
        public MSnapshotManager(MSolution solutionRef)
        {
            m_Solution = solutionRef;
            //TODO: Add code here...
        }

        #region *** Properties ***
        #endregion

        /// <summary>
        /// NR-Открыть менеджер
        /// </summary>
        /// <param name="settings"></param>
        public void Open(MSolutionSettings settings)
        {
            //TODO: Add code here...
        }

        /// <summary>
        /// NR-Закрыть менеджер
        /// </summary>
        public void Close()
        {
            //TODO: Add code here...
        }
        /// <summary>
        /// NR-Вписать статистику для подсистемы
        /// </summary>
        /// <param name="stat">Бланк статистики</param>
        internal void getStatistics(MStatistics stat)
        {
            //TODO: какая статистика у подсистемы Снимков?
        }
        /// <summary>
        /// NR-Загрузить полный снимок Солюшена из файла
        /// </summary>
        /// <param name="snapshotFilePathName">Путь к файлу полного снимка Солюшена</param>
        public void SnapshotFullLoad(string snapshotFilePathName)
        {
            //сначала надо проверить что Солюшен не закрыт, проверив флаг СостояниеСолюшена.
            if (!this.m_Solution.isReady)//выбросить исключение, если солюшен не готов к работе
                throw new SimEngineException(String.Format("Солюшен не готов к работе: SolutionState={0}", this.m_Solution.SolutionState.ToString()));
            
            //Readonly: Выбросить исключение.
            if (this.m_Solution.SolutionReadOnly)
                throw new SimEngineException("Невозможно загрузить полный снимок, поскольку Солюшен в режиме ReadOnly");

            throw new NotImplementedException();//TODO: Add code here...
        }

        /// <summary>
        /// NR-Создать файл полного снимка Солюшена в каталоге Солюшена
        /// </summary>
        public void SnapshotFullCreate()
        {
            throw new NotImplementedException();//TODO: Add code here...
            //1 создать имя файла полного снимка и путь к нему в Каталоге Солюшена
            string snapshotFilePath = "";
            //2 вызвать функцию создания полного снимка Солюшена
            this.SnapshotFullCreate(snapshotFilePath);
        }
        /// <summary>
        /// NR-Создать файл полного снимка Солюшена
        /// </summary>
        /// <param name="snapshotFilePath">Путь создаваемого файла полного снимка Солюшена</param>
        public void SnapshotFullCreate(string snapshotFilePath)
        {
            //сначала надо проверить что Солюшен не закрыт, проверив флаг СостояниеСолюшена.
            if (!this.m_Solution.isReady)//выбросить исключение, если солюшен не готов к работе
                throw new SimEngineException(String.Format("Солюшен не готов к работе: SolutionState={0}", this.m_Solution.SolutionState.ToString()));
            
            //Readonly: если каталог для файла снимка доступен на запись, то выполнять. а если недоступен, то выбрасывать исключение.

            throw new NotImplementedException();//TODO: Add code here...

        }


        public override string ToString()
        {
            //Readonly: Не требуется, так как нет изменения Солюшена.
            throw new NotImplementedException();//TODO: Add code here...
            
        }
    }
}
