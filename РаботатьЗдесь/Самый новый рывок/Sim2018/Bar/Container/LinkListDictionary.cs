using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Container
{
    /// <summary>
    /// NT-Представляет словарь списков связей для реализации индекса объектов связей
    /// </summary>
    public class LinkListDictionary
    {
        /// <summary>
        /// Словарь списков связей
        /// </summary>
        private Dictionary<UInt64, List<MLink>> m_dict;
        
        /// <summary>
        /// NT-Default constructor
        /// </summary>
        public LinkListDictionary()
        {
            m_dict = new Dictionary<ulong, List<MLink>>();
        }

        /// <summary>
        /// NT-Добавить связь в коллекцию
        /// </summary>
        /// <param name="id">Идентификатор-ключ индекса</param>
        /// <param name="link">Объект связи</param>
        internal void Add(UInt64 id, MLink link)
        {
            if (m_dict.ContainsKey(id))
                m_dict[id].Add(link);
            else
            {
                List<MLink> li = new List<MLink>();
                li.Add(link);
                m_dict.Add(id, li);
            }
            return;
        }

        /// <summary>
        /// NT-remove link from collection
        /// </summary>
        /// <param name="key">Идентификатор - ключ индекса</param>
        /// <param name="link">Link object</param>
        /// <remarks>
        /// При удалении просматривается весь список по ключу. Это может быть очень долго.
        /// </remarks>
        public void Remove(UInt64 key, MLink link)
        {
            //если ключ есть в коллекции, получаем список по нему
            if(this.m_dict.ContainsKey(key))
            {
                List<MLink> list = m_dict[key];
                //если в списке есть связь с таким ИД, то удаляем ее из списка и выходим
                //она должна быть только одна
                UInt64 id64 = link.intGetIDasU64();
                for (int i = 0; i < list.Count; i++)
                {
                    MLink li = list[i];
                    if (li.intGetIDasU64() == id64)
                    {
                        list.RemoveAt(i);//удаляем связь из списка один раз
                        break;//выходим из цикла
                    }
                }
                //и если список будет пустой то можно удалить и этот итем словаря.
                if (list.Count == 0)
                    this.m_dict.Remove(key);
            }
            return;
        }

        /// <summary>
        /// NT-Получить массив связей по идентификатору. 
        /// Массив и основные поля связей нельзя изменять, иначе индекс будет разрушен. 
        /// </summary>
        /// <remarks>
        /// Функция возвращает массив связей. Массив нельзя модифицировать. 
        /// Для связи нельзя модифицировать поля ID, upCellId, downCellId, ElementClass (Axis).
        /// </remarks>
        /// <param name="id">Идентификатор ячейки - ключ индекса</param>
        /// <returns></returns>
        public MLink[] Get(UInt64 id)
        {
            if (!m_dict.ContainsKey(id))
                return new MLink[0];
            else 
                return m_dict[id].ToArray();
        }
        /// <summary>
        /// NT-Очистить коллекцию
        /// </summary>
        public void Clear()
        {
            this.m_dict.Clear();
        }

        /// <summary>
        /// NT-Получить описание для отладчика
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0} keys", m_dict.Count); 
        }

    }
}
