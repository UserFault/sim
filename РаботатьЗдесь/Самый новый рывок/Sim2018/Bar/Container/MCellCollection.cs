using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Bar.Utility;

namespace Bar.Container
{
    //TODO: класс нуждается в переделке под новую концепцию.
    //TODO: Класс добавлен предварительно, его функциональность должна быть подтверждена.
    //TODO: расставить тут проверки типов подклассов ИД - где это нужно. И заменить сами аргументы ИД на новые.
    //TODO: перепроверить код с учетом многоконтейнерности - тут могут быть ячейки разных Солюшенов.
    /// <summary>
    /// NT-Представляет коллекцию ячеек Солюшена в памяти. 
    /// </summary>
    public class MCellCollection
    {
        #region *** Fields ***
        /// <summary>
        /// Обратная ссылка на Солюшен
        /// </summary>
        private MSolution m_Solution;

        /// <summary>
        /// Словарь ячеек
        /// </summary>
        /// <remarks>Основное хранилище объектов коллекции.</remarks>
        private Dictionary<UInt64, MCell> m_items;

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="solutionRef">Ссылка на объект Солюшена</param>
        public MCellCollection(MSolution solutionRef)
        {
            m_Solution = solutionRef;
            m_items = new Dictionary<ulong, MCell>();
        }

        #region *** Properties ***
        /// <summary>
        ///  NT-Получить список ячеек в коллекции, только для чтения
        /// </summary>
        /// <remarks>Возможно, список ячеек создается заново при каждом обращении, так что лучше его не использовать, или кешировать в локальной переменной.</remarks>
        public IEnumerable Items
        {
            get
            {
                return m_items.Values;
            }
        }

        /// <summary>
        /// Get count of cells in collection
        /// </summary>
        /// <remarks></remarks>
        /// <value></value>
        /// <seealso cref=""/>
        public int Count
        {
            get { return m_items.Count; }
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
        /// NT-Добавить ячейку в словарь
        /// </summary>
        /// <exception cref="ArgumentException">Cell already exists</exception>
        public void AddCell(MCell cell)
        {
            m_items.Add(cell.ID.ToU64(), cell);
        }

        /// <summary>
        /// NT-Удалить ячейку из словаря
        /// </summary>
        public void RemoveCell(MCellId cellid)
        {
            MID.checkID(cellid);
            m_items.Remove(cellid.ToU64());
        }
        /// <summary>
        /// NT-Удалить ячейку из словаря
        /// </summary>
        public void RemoveCell(MCell cell)
        {
            RemoveCell((MCellId) cell.ID);
        }

        /// <summary>
        /// NT-удалить все ячейки
        /// </summary>
        public void Clear()
        {
            m_items.Clear();
        }

        /// <summary>
        /// NT-получить ячейку по идентификатору, null if not exists
        /// </summary>
        public MCell getCell(MCellId cellid)
        {
            MCellId.checkID(cellid);

            MCell t;
            m_items.TryGetValue(cellid.ToU64(), out t);
            return t;
        }

        /// <summary>
        /// NT-Return first cell in collection or null if no cells
        /// </summary>
        /// <returns></returns>
        internal MCell getFirstCell()
        {
            //return first cell in dictionary
            foreach (KeyValuePair<UInt64, MCell> kvp in m_items)
                return kvp.Value;
            //return null if no cells
            return null;
        }

        /// <summary>
        /// NT-Выбрать ячейки по их названию
        /// </summary>
        /// <param name="cellTitle">Название ячейки</param>
        /// <param name="comparisonType">Тип сравнения строк</param>
        /// <returns>Возвращает список найденных ячеек или пустой список.</returns>
        public List<MCell> getCellsByTitle(String cellTitle, StringComparison comparisonType)
        {
            List<MCell> result = new List<MCell>();
            
            foreach (MCell t in m_items.Values)
            {
                if (String.Equals(cellTitle, t.Name, comparisonType) == true)
                    result.Add(t);
            }
            return result;


        }

        /// <summary>
        /// NT-Получить список ячеек по значению свойств, описанных в ячейке-шаблоне. 
        /// Все поля в шаблоне, не содержащие null, используются для отбора ячеек. 
        /// </summary>
        /// <param name="template">Шаблон ячейки. Если ни одно поле не указано, все ячейки будут включены в результат</param>
        /// <returns>Cell collection object</returns>
        /// <remarks>Эта универсальная функция работает медленно, и может быть заменена на упрощенные версии при оптимизации движка. Но для прототипа она хороша.</remarks>
        public MCellCollection getCells(MCellTemplate template)
        {
            MCellCollection mc = new MCellCollection(this.m_Solution);
            foreach (MCell t in m_items.Values)
            {
                if (template.isMatch(t) == true)
                    mc.AddCell(t);
            }
            return mc;
        }

        /// <summary>
        /// NT-Проверяет, что список содержит ячейку с указанным именем. 
        /// Аналогично можно запросить список ячеек по имени. 
        /// Только тут быстрее, поскольку не надо формировать список возвращаемых ячеек.
        /// Нужна для имен, которым положено быть уникальными (имена типов, например).
        /// </summary>
        /// <param name="name"></param>
        public bool containsName(string name, StringComparison comparisonType)
        {
            foreach (MCell ce in m_items.Values)
            {
                if (String.Equals(name, ce.Name, comparisonType) == true) 
                    return true;
            }
            return false;
        }

        /// <summary>
        /// NT-Проверяет, что Коллекция содержит ячейку с указанным идентификатором. 
        /// Для проверки наличия ячейки в списке перед загрузкой из БД.
        /// </summary>
        public bool containsCell(MCellId cellid)
        {
            MCellId.checkID(cellid);
            return m_items.ContainsKey(cellid.ToU64());
        }
        /// <summary>
        /// NT-Проверяет, что Коллекция содержит хотя бы одну временную ячейку
        /// </summary>
        /// <returns></returns>
        public bool containsTemporaryCells()
        {
            foreach (MCell ce in m_items.Values)
            {
                if (ce.CellMode == MCellMode.Temporary) return true;
            }
            return false;
        }

        /// <summary>
        /// NT-Save all cells with specified cell type
        /// </summary>
        /// <param name="mCellMode"></param>
        internal void SaveCells(MCellMode cellMode)
        {
            foreach (MCell ce in m_items.Values)
                if (ce.CellMode == cellMode)
                    ce.Save();
            return;
        }

        /// <summary>
        /// NT-получить имя ячейки по идентификатору. 
        /// Возвращает нуль если ячейки нет в списке.
        /// </summary>
        /// <remarks>
        /// Все равно надо сначала получить ячейку. 
        /// Эта функция для SQL нужна - там долго ячейку доставать,
        /// а в памяти оно нафиг.
        /// </remarks>
        public string getNameByID(MCellId cellid)
        {
            MCellId.checkID(cellid);
            
            MCell m = getCell(cellid);
            if (m != null) return m.Name;
            else return null;
        }


        /// <summary>
        /// NT-get number of anysort temp cells in dictionary
        /// </summary>
        /// <returns></returns>
        public int getNumberOfTemporaryCells()
        {
            int cnt = 0;
            foreach (MCell ce in m_items.Values)
            {
                if (ce.CellMode == MCellMode.Temporary)
                    cnt++;
            }
            return cnt;
        }

        /// <summary>
        /// NT-Получить из коллекции максимальный ИД временных ячеек текущего контейнера
        /// </summary>
        /// <returns>Возвращает максимальный ИД временных ячеек текущего солюшена или 0 если нет таких ячеек</returns>
        internal int getMaxTempCellID()
        {
            //теперь в коллекции могут быть ячейки и других контейнеров (anysort), 
            //поэтому проверяем ид контейнера на соответствие.           
            // Временные идентификаторы меньше 0 и растут в отрицательную сторону. 
            //Поэтому ищем минимальный идентификатор среди ячеек, принадлежащих текущему контейнеру.           
            
            //взять ид солюшена из объекта контейнера здесь
            int containerId = this.m_Solution.SolutionId;
            //выбрать ячейки по ид текущего солюшена и с отрицательным ИД,
            //потом определить максимум (по модулю) из этих ид (все ид отрицательные, поэтому минимум)
            int min = 0;
            MID id;
            int elemid;

            foreach (KeyValuePair<UInt64, MCell> kvp in m_items)
            {
                //разделить ид контейнера и ид ячейки
                id = MID.FromU64(kvp.Key);
                if (id.ContainerId == containerId)//if local id
                {
                    elemid = id.ElementId;//optimize access
                    if (min > elemid) min = elemid;
                }
            }
            //вернуть 0 если таких ячеек нет, иначе вернуть найденный ИД.
            return min;
        }

        /// <summary>
        /// NT-Get string representation of object.
        /// </summary>
        /// <returns>Return string representation of object.</returns>
        public override string ToString()
        {
            return String.Format("Cells: {0}", m_items.Count);
        }

        /// <summary>
        /// NR-Выгрузить давно не используемые ячейки из памяти
        /// </summary>
        /// <param name="delta">Интервал времени неиспользования ячеек для отбора</param>
        /// <remarks>
        /// Функция медленная. Интервал пока не известен, нельзя задать автоматически.
        /// Функция также вызывает сборщик мусора.
        /// Функция использует свойство ячейки MCell.LastUsed
        /// </remarks>
        public void UnloadUnusedCells(TimeSpan delta)
        {
            //перебирать все ячейки в памяти
            //через проперти MCell.LastUsed найти постоянные ячейки (MCellA, MCellB, MCellBds),
            //локальные или внешние,
            //которые старее от DatetTime.Now на delta и более интервал времени
            //записать их в отдельный список
            //и выгрузить их затем из памяти

            List<MCell> cells = new List<MCell>();
            DateTime now = DateTime.Now;
            foreach (MCell cell in this.m_items.Values)
            {
                if (cell.isTemporary) continue;
                TimeSpan ts = now.Subtract(cell.LastUsed);
                if (ts > delta)
                    cells.Add(cell);
            }
            //выгрузить ячейки из памяти
            foreach (MCell c in cells)
                c.Unload();
            //вызвать функцию сборщика мусора
            GC.Collect();

            return;
        }


        /// <summary>
        /// NT-Выявить внешние солюшены и собрать их идентификаторы в переданный словарь
        /// </summary>
        /// <param name="solids">Словарь для сбора идентификаторов солюшенов</param>
        internal void grabExternalSolutions(UnicalInt32Collection solids)
        {
            //find id in link
            foreach (MCell cell in this.Items)
            {
                //если ячейка постоянная и локальная, то ее проверять будем в адаптере БД
                if (cell.isConstAndLocal) continue;
                //считаем только активные ячейки
                if (!cell.isActive) continue;
                //а если нет, то тут
                solids.Add(cell.ElementClass.ContainerId);
                solids.Add(cell.ElementState.ContainerId);
                solids.Add(cell.ID.ContainerId);
                solids.Add(cell.ValueTypeId.ContainerId);
            }

            return;
        }
    }
}
