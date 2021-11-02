using System;
using System.Collections.Generic;
using System.Text;

namespace Bar
{
    /// <summary>
    /// NR- Представляет подсистему методов Солюшена
    /// </summary>
    public class MMethodManager
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
        public MMethodManager(MSolution solutionRef)
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
            //TODO: какая статистика у подсистемы Методов?
        }

    }
}
