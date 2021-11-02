using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.DatabaseAdapters
{
    /// <summary>
    /// Адаптер для БД MsJet4.0 Access database
    /// </summary>
    [MDatabaseTypeAttribute(MDatabaseType.MsJet40, true)] //атрибут для автоматической проверки доступных типов БД Солюшена
    public class MsJetDbAdapter: BaseDbAdapter
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="solutionRef">Ссылка на объект Солюшена</param>
        public MsJetDbAdapter(MSolution solutionRef)
            : base(solutionRef)
        {
        }

    }
}
