using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.DatabaseAdapters
{
    /// <summary>
    /// Адаптер для Солюшена без БД
    /// </summary>
    [MDatabaseTypeAttribute(MDatabaseType.NoDatabase, true)] //атрибут для автоматической проверки доступных типов БД Солюшена
    public class NoDbAdapter: BaseDbAdapter
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="solutionRef">Ссылка на объект Солюшена</param>
        public NoDbAdapter(MSolution solutionRef)
            : base(solutionRef)
        {
        }

    }
}
