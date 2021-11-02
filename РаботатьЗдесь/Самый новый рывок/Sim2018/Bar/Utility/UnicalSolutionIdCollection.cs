using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Utility
{
    /// <summary>
    /// NT-Вспомогательный класс словаря счетчиков значений Int32
    /// </summary>
    public class UnicalInt32Collection
    {
        private Dictionary<int, int> m_dict;

        public UnicalInt32Collection()
        {
            this.m_dict = new Dictionary<int, int>();
        }

        /// <summary>
        /// Словарь счетчиков уникальных значений
        /// </summary>
        public Dictionary<int, int> DictionaryOfCounters
        {
            get { return this.m_dict; }
        }
        /// <summary>
        /// Список значений без счетчиков
        /// </summary>
        public List<int> Items
        {
            get { return new List<int>(this.m_dict.Values); }
        }
        /// <summary>
        /// NT-Очистить коллекцию
        /// </summary>
        public void Clear()
        {
            this.m_dict.Clear();
        }

        /// <summary>
        /// NT-Добавить значение
        /// </summary>
        /// <param name="value">Значение</param>
        public void Add(Int32 value)
        {
            if (this.m_dict.ContainsKey(value))
                this.m_dict[value]++;
            else
                this.m_dict.Add(value, 1);

            return;
        }
        /// <summary>
        /// NT-Добавить значение и количество
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="count">Количество</param>
        public void Add(Int32 value, Int32 count)
        {
            if (this.m_dict.ContainsKey(value))
                this.m_dict[value]+= count;
            else
                this.m_dict.Add(value, count);

            return;
        }


    }
}
