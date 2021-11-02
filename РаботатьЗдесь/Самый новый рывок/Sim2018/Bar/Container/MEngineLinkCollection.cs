using System;
using System.Collections.Generic;
using System.Text;
using Bar.Utility;

namespace Bar.Container
{

    //Тут полезны три индекса: по srcid, destid, axis.
    //Но индексы не уникальные, Dictionary для них не годится.
    //Сейчас все делается перебором всего списка связей. Это медленно.
    //В NET нет ничего подходящего с неуникальными ключами.
    //Так что пока можно оставить полный перебор, а потом после релиза оптимизировать
    //а) Словарь с списками
    //б) Нечто совсем самодельное.

    //TODO: класс нуждается в переделке под новые идентификаторы ячеек.
    //TODO: Класс добавлен предварительно, его функциональность должна быть подтверждена.

    /* Это экспериментальный вариант коллекции, только для списка связей контейнера.
     * Тут введены индексы по srcid, destid, axis.
     * Хотя они усложняют код и занимают больше места: по 60 байт на каждый элемент коллекции.
     * И я не знаю, что хуже: перебирать весь список связей, или рыться в 4 словарях, три из которых - словари со списками.
     * Надо посмотреть, будет ли выгода от этой новой конструкции.
     * 
     * Устройство:
     * Введены три объекта индекса. Каждый объект это словарь списков. 
     * Ключом словаря соответственно является одно из индексируемых свойств - идентификатор ячейки в U64.
     * Удобно что все они одного типа данных - класс только один для них используется.
     * Есть главный словарь - словарь с ключом по идентификатору связи.
     * Во всех трех индексах и главном словаре объект связи должен присутствовать только один раз.
     * Поэтому вроде бы все просто. Пока что.
     * Удалять объекты связей тоже приходится из всех словарей.
     * Изменять значения в объектах связей можно, но только не основные.
     * Основные свойства связи это Идентификатор связи, Класс связи, идентификаторы верхней и нижней ячеек.
     * Если их изменить, получится уже совершенно другая связь, поэтому это делать запрещается по концепции SIM.
     * Иначе словари будут рассогласованы, и надо будет произвести пересоздание индексов. 

    */
    /// <summary>
    /// NR-Представляет коллекцию связей в контейнере Солюшена.
    /// </summary>
    public class MContainerLinkCollection
    {
        #region *** Fields ***
        /// <summary>
        /// Обратная ссылка на Солюшен
        /// </summary>
        private MSolution m_Solution;
        /// <summary>
        /// Основной словарь связей коллекции
        /// </summary>
        private Dictionary<UInt64, MLink> m_dictionary;
        /// <summary>
        /// Индекс по Link.Src
        /// </summary>
        private LinkListDictionary m_SrcIdIndex;
        /// <summary>
        /// Индекс по Link.Dest
        /// </summary>
        private LinkListDictionary m_DestIdIndex;
        /// <summary>
        /// Индекс по Link.Axis
        /// </summary>
        private LinkListDictionary m_AxisIdIndex;
        #endregion

        /// <summary>
        /// NT-Конструктор
        /// </summary>
        /// <param name="solutionRef">Ссылка на объект Солюшена</param>
        public MContainerLinkCollection(MSolution solutionRef)
        {
            m_Solution = solutionRef;
            m_dictionary = new Dictionary<ulong, MLink>();
            m_AxisIdIndex = new LinkListDictionary();
            m_DestIdIndex = new LinkListDictionary();
            m_SrcIdIndex = new LinkListDictionary();
            //TODO: Add code here...
        }

        #region *** Properties ***
        /// <summary>
        /// NT-Получить список связей коллекции
        /// </summary>
        public IEnumerable<MLink> Items
        {
            get { return m_dictionary.Values; }
        }
        /// <summary>
        /// NT-Получить количество связей в коллекции
        /// </summary>
        public int Count
        {
            get { return m_dictionary.Count; }
        }

        #endregion

        ///// <summary>
        ///// NR-Открыть менеджер
        ///// </summary>
        ///// <param name="settings"></param>
        //public void Open(MSolutionSettings settings)
        //{
        //    //TODO: Add code here...
        //}

        ///// <summary>
        ///// NR-Закрыть менеджер
        ///// </summary>
        //public void Close()
        //{
        //    //TODO: Add code here...
        //}

        /// <summary>
        /// NT-Присвоить всем элементам коллекции новое значение ServiceFlag
        /// </summary>
        /// <param name="val">Новое значение ServiceFlag</param>
        public void SetServiceFlags(Int32 val)
        {
            foreach (MElement el in this.Items)
                el.ServiceFlag = val;

            return;
        }

        /// <summary>
        /// NT-Очистить коллекцию
        /// </summary>
        public void Clear()
        {
            this.m_dictionary.Clear();
            this.m_AxisIdIndex.Clear();
            this.m_DestIdIndex.Clear();
            this.m_SrcIdIndex.Clear();

            return;
        }

        /// <summary>
        /// NT-Добавить связь в коллекцию
        /// </summary>
        /// <param name="link">Добавляемая связь</param>
        public void AddLink(MLink link)
        {
            //add to main dictionary
            this.m_dictionary.Add(link.intGetIDasU64(), link); 
            //add to indexes
            this.m_AxisIdIndex.Add(link.intGetClassIDasU64(), link);
            this.m_DestIdIndex.Add(link.intGetDownIDasU64(), link);
            this.m_SrcIdIndex.Add(link.intGetUpIDasU64(), link);

            return;
        }

        /// <summary>
        /// NT-Перестроить индексы коллекции
        /// </summary>
        /// <remarks>Это очень медленная функция. Возможно, не пригодится.</remarks>
        public void RecreateIndexes()
        {
            //очистить индексы
            this.m_AxisIdIndex.Clear();
            this.m_DestIdIndex.Clear();
            this.m_SrcIdIndex.Clear();
            //освободить память от старых объектов
            GC.Collect();
            //перестроить индексы коллекции по главному словарю
            foreach (MLink link in this.m_dictionary.Values)
            {
                //add to indexes
                this.m_AxisIdIndex.Add(link.intGetClassIDasU64(), link);
                this.m_DestIdIndex.Add(link.intGetDownIDasU64(), link);
                this.m_SrcIdIndex.Add(link.intGetUpIDasU64(), link);
            }

            return;
        }

        /// <summary>
        /// NT-Проверить существование подобной связи по cellId, Axis, Active
        /// Раньше называлась containsLink(..)
        /// </summary>
        /// <param name="link">link example</param>
        /// <returns></returns>
        public bool содержитПодобнуюСвязь(MLink link)
        {
            //ищем связь, совпадающую по ячейкам, флагу активности и оси связи (подобную связь)

            //если по индексам, то я могу быстро получить три массива связей
            //и искать в наименьшем из них
            //если связь существует, то она должна быть во всех 3 индексах
            //и надо просто найти наименьший список связей. 
            //axis обычно содержит много связей, а src и dest - меньше.
            MLink[] ar1 = this.m_SrcIdIndex.Get(link.intGetUpIDasU64());
            MLink[] ar2 = this.m_DestIdIndex.Get(link.intGetDownIDasU64());
            //выбрать наименьший массив
            if (ar1.Length > ar2.Length)
                ar1 = ar2;
            //искать перебором
            foreach (MLink li in ar1)
            {
                if (link.isEqualLink(li) == true)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// NT-Удалить указанный объект связи из коллекции
        /// </summary>
        /// <param name="link">Объект связи</param>
        /// <returns>Возвращает true, если связь содержалась в коллекции, иначе возвращает false.</returns>
        public bool Remove(MLink link)
        {
            //удалить из индексов
            this.m_AxisIdIndex.Remove(link.intGetClassIDasU64(), link);
            this.m_DestIdIndex.Remove(link.intGetDownIDasU64(), link);
            this.m_SrcIdIndex.Remove(link.intGetUpIDasU64(), link);
            //удалить из главного словаря
            UInt64 key = link.intGetIDasU64();
            if (this.m_dictionary.ContainsKey(key))
            {
                this.m_dictionary.Remove(key);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// NT-Remove one link item from list
        /// </summary>
        /// <param name="id">Идентификатор связи</param>
        /// <returns>Возвращает true, если связь содержалась в коллекции, иначе возвращает false.</returns>
        public bool Remove(MLinkId id)
        {
            //TODO: тут если связи нет в коллекции, хорошо бы вернуть false.
            MLink link = this.Get(id);
            return this.Remove(link);
        }
        /// <summary>
        /// NT-Получить связь по идентификатору связи
        /// </summary>
        /// <param name="id">Идентификатор связи</param>
        /// <returns>Возвращает объект связи. Выбрасывает исключение SimEngineException если связь не найдена в коллекции</returns>
        /// <exception cref="SimEngineException">Выбрасывает исключение SimEngineException если связь не найдена в коллекции</exception>
        public MLink Get(MLinkId id)
        {
            UInt64 key = id.ToU64();
            if (m_dictionary.ContainsKey(key))
                return this.m_dictionary[key];
            else throw new SimEngineException(String.Format("Связь с ID={0} не найдена в MEngineLinkCollection", id.ToString()));
        }
        /// <summary>
        /// NT-Содержит ли коллекция связь с указанным идентификатором
        /// </summary>
        /// <param name="id">Идентификатор связи</param>
        /// <returns>Возвращает true, если коллекция содержит связь с указанным идентификатором, false в противном случае.</returns>
        public bool Contains(MLinkId id)
        {
            UInt64 key = id.ToU64();
            return m_dictionary.ContainsKey(key);
        }

        /// <summary>
        /// NT-Проверить, что связь с такими параметрами существует в списке.
        /// </summary>
        /// <remarks>Долго будет искать.</remarks>
        public bool Сontains(MLinkTemplate template)
        {
            foreach (MLink li in this.m_dictionary.Values)
            {
                if (template.isMatch(li) == true)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// NT-Получить список связей по значению свойств, описанных в связи-шаблоне. 
        /// Все поля в шаблоне, не содержащие null, используются для отбора связей.
        /// </summary>
        /// <param name="template">Шаблон связи. X AND Y. Если ни одно поле не указано, все связи будут включены в результат</param>
        /// <returns>Возвращает коллекцию отбранных связей.</returns>
        public MLinkCollection getLinks(MLinkTemplate template)
        {
            MLinkCollection col = new MLinkCollection();
            foreach (MLink li in m_dictionary.Values)
            {
                if (template.isMatch(li) == true)
                    col.AddLink(li);
            }
            return col;

        }

        /// <summary>
        /// NT-Set cell reference for specified cell
        /// </summary>
        /// <param name="cellid">cell id</param>
        /// <param name="cell">cell reference or null</param>
        /// <returns></returns>
        internal void setCellReferences(MID cellid, MCell cell)
        {
            //использовать индекс по cellid - два индекса, по src и dest
            UInt64 id = cellid.ToU64();
            //1 получить связи по ид= upId
            MLink[] ar1 = this.m_SrcIdIndex.Get(id);
            foreach (MLink li in ar1)
                li.setCellRefsIfExists(id, cell);
            //2 downId
            ar1 = this.m_DestIdIndex.Get(id);
            foreach (MLink li in ar1)
                li.setCellRefsIfExists(id, cell);
            ar1 = null;

            return;
        }

        /// <summary>
        /// NT-Получить из коллекции связей контейнера макс ИД временных связей текущего контейнера
        /// </summary>
        /// <returns>Возвращает максимальный ИД временных связей текущего солюшена или 0 если нет таких связей</returns>
        internal int getMaxTempLinkID()
        {
            //взять ид солюшена из объекта контейнера здесь
            int solutionId = this.m_Solution.SolutionId;
            //выбрать связи по ид текущего солюшена и с отрицательным ИД,
            //потом определить максимум (по модулю) из этих ид (все ид отрицательные, поэтому минимум)
            //вернуть 0 если таких связей нет, иначе вернуть найденный ИД.
            int result = 0;
            foreach (MLink link in this.m_dictionary.Values)
            {
                if (link.ID.ContainerId != solutionId)
                    continue;
                if (link.ID.ElementId < result)
                    result = link.ID.ElementId;
            }

            return result;
        }

        /// <summary>
        /// Проверить что связь - временная и локальная.
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        internal bool isLinkLocalTemporary(MLink link)
        {
            //check link is local
            return ((link.ID.ContainerId == this.m_Solution.SolutionId) && link.ID.isTemporaryId());
        }

        /// <summary>
        /// NT-Проверить что связь - локальная.
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        internal bool isLinkLocal(MLink link)
        {
            //check link is local
            return (link.ID.ContainerId == this.m_Solution.SolutionId);
        }

        /// <summary>
        /// NT-Получить число локальных временных связей в памяти.
        /// Внешние временные связи не учитываются
        /// </summary>
        /// <returns></returns>
        internal int getNumberOfLocalTempLinks()
        {
            int cnt = 0;
            foreach (MLink li in this.m_dictionary.Values)
                if (this.isLinkLocalTemporary(li))
                    cnt++;
            return cnt;
        }

        /// <summary>
        /// NT-Получить число временных связей в памяти.
        /// Локальных и внешних.
        /// </summary>
        /// <returns></returns>
        internal int getNumberOfTemporaryLinks()
        {
            int cnt = 0;
            foreach (MLink li in this.m_dictionary.Values)
                if (li.isTemporary)
                    cnt++;
            return cnt;
        }

        /// <summary>
        /// NT-Get all links for specified cell
        /// </summary>
        /// <param name="cellid">cell id</param>
        /// <returns></returns>
        internal MLinkCollection getCellLinks(MCellId cellid)
        {
            MLinkCollection col = new MLinkCollection();

            UInt64 id = cellid.ToU64();
            //1 получить связи по ид= upId
            MLink[] ar1 = this.m_SrcIdIndex.Get(id);
            //2 downId
            MLink[] ar2 = this.m_DestIdIndex.Get(id);
            //3 собрать воедино два массива и удалить дубликаты
            //дубликаты могут появиться, если есть ссылки ячейки на себя саму
            col.Items.AddRange(this.Merge(ar1, ar2));

            return col;
        }


        /// <summary>
        /// NT-Собрать два массива связей в один, удалив дубликаты связей с одинаковыми ИД.
        /// </summary>
        /// <param name="ar1">Массив связей</param>
        /// <param name="ar2">Массив связей</param>
        /// <returns>Возвращает список связей без дубликатов</returns>
        private List<MLink> Merge(MLink[] ar1, MLink[] ar2)
        {
            List<MLink> lout = new List<MLink>(ar1);//output list
            //1 Если второй список пустой, выходим
            if (ar2.Length == 0) return lout;
            //2 Если первый список пустой, копируем все связи из второго и выходим
            lout.AddRange(ar2);
            if (ar1.Length == 0)
                return lout;
            //а если оба не пустые, то надо удалить дубликаты если есть
            //поиск через словарь
            Dictionary<UInt64, MLink> dic = new Dictionary<ulong, MLink>();
            foreach (MLink li1 in lout)
            {
                UInt64 key = li1.intGetIDasU64();
                if (!dic.ContainsKey(key))
                    dic.Add(key, li1);
            }
            lout.Clear();
            lout = new List<MLink>(dic.Values);
            dic.Clear();
            //output list must contains unical links only
            return lout;
        }
        /// <summary>
        /// NT-Получить словарь счетчиков для классов связей
        /// </summary>
        /// <returns>Возвращает словарь счетчиков  классов связей</returns>
        /// <remarks>
        /// Словарь собирается перебором всех связей коллекции. Это будет медленно.
        /// </remarks>
        internal Dictionary<UInt64, int> getLinkAxises()
        {

            Dictionary<UInt64, int> dic = new Dictionary<UInt64, int>();
            foreach (MLink li in this.m_dictionary.Values)
            {
                //считаем только активные связи
                if (li.isActive)
                {
                    UInt64 mid = li.intGetClassIDasU64();
                    if (dic.ContainsKey(mid))
                        dic[mid] += 1;
                    else
                        dic.Add(mid, 1);
                }
            }
            return dic;
        }



        //---------- недоделки функций --------------------------


        




        ///// <summary> - TODO: Это надо переделать так как проверка связей идет по трем свойствам а не по ид связи, так как ид тогда не было у связи.
        ///// NT-Получить связи из указанного списка, которых нет в текущем списке.
        ///// </summary>
        ///// <param name="colD">Список добавляемых связей</param>
        ///// <remarks>
        ///// Это почти аналог S1_Merge, только здесь копирует связи в возвращаемый список,
        ///// а там сразу в общий список, это быстрее, но нет разностного списка.
        ///// </remarks>
        //internal List<MLink> getUnicalLinks(MLinkCollection colD)
        //{
        //    List<MLink> lout = new List<MLink>();//output list
        //    //Если  список-аргумент пустой, выходим
        //    if (colD.m_items.Count == 0) return lout;
        //    //Если текущий список пустой, копируем все связи из аргумента и выходим
        //    if (m_items.Count == 0)
        //    {
        //        lout.AddRange(colD.Items);
        //        return lout;
        //    }
        //    //поиск перебором
        //    bool flag;
        //    //Отсутствующие связи поместить во временный список.
        //    //в среднем получается N * M/2 проходов-сравнений
        //    foreach (MLink li in colD.m_items)
        //    {
        //        flag = false;
        //        foreach (MLink lm in this.m_items)
        //        {
        //            if (lm.isEqual(li)) { flag = true; break; } //Связь есть в обеих списках, незачем дальше искать. проверить!
        //        }
        //        if (flag == false) // нет совпадения
        //            lout.Add(li);
        //    }
        //    //output list must contains unical links only
        //    return lout;
        //}

        ///// <summary> - TODO: Нельзя это делать в Тапп Бар. Надо удалять связь и заводить новую, а не менять идентификаторы в существующей.
        ///// NT-Replace id in cell links.
        ///// При сохранении временных ячеек меняется идентификатор, его надо заменить и в связях ячейки
        ///// </summary>
        ///// <param name="oldId">Old id</param>
        ///// <param name="newId">New id</param>
        //internal void replaceCellId(MID oldId, MID newId)
        //{
        //    //20112016 - Тут хорошо бы использовать индекс по cellid - два индекса, по src и dest
        //    //а сейчас это делается перебором всех связей коллекции. 
        //    foreach (MLink li in this.m_items)
        //        li.intReplaceID(oldId, newId);
        //}













        /// <summary>
        /// NT-Выявить внешние солюшены и собрать их идентификаторы в переданный словарь
        /// </summary>
        /// <param name="solids">Словарь счетчиков для сбора идентификаторов солюшенов</param>
        internal void grabExternalSolutions(UnicalInt32Collection solids)
        {
            //find id in link
            foreach (MLink link in this.Items)
            {
                //если связь постоянная и локальная, то ее проверять будем в адаптере БД
                if (link.isConstAndLocal) continue;
                //считаем только активные
                if (!link.isActive) continue;
                //а если нет, то тут
                solids.Add(link.downCellId.ContainerId);
                solids.Add(link.ElementClass.ContainerId);
                solids.Add(link.ElementState.ContainerId);
                solids.Add(link.ID.ContainerId);
                solids.Add(link.upCellId.ContainerId);
            }

            return;
        }




    }
}
