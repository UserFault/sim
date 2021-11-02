using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Container
{
    //TODO: наполнить класс тут
    /// <summary>
    /// NR-представляет коллекцию связей ячейки
    /// </summary>
    public class MLinkCollection
    {
        public MLinkCollection()
        {
            throw new System.NotImplementedException();
        }

        public List<MLink> Items
        {
            get {throw new NotImplementedException(); } //TODO: Add code here...
        }

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
        /// NR-Добавить связь в коллекцию
        /// </summary>
        /// <param name="li">Добавляемая связь</param>
        internal void AddLink(MLink li)
        {
            throw new NotImplementedException();
        }
    }
}
