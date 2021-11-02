using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Bar.Container
{
    public abstract class MCell : MElement
    {
        /// <summary>
        /// Порог числа связей ячейки при выборе: поиск в памяти или в таблице.
        /// </summary>
        /// <remarks>Зависит от производительности сервера БД, сети и состония БД. Подобрать значение опытным путем в процессе тестов.</remarks>
        public static int LinksTreshold = 512;

        /// <summary>
        /// Ссылка на объект Солюшена
        /// </summary>
        protected MSolution m_Solution;//вынесен сюда, так как востребован в MCellA и MCellB
        /// <summary>
        /// Таймштамп последнего чтения
        /// </summary>
        protected DateTime m_lastUsed;//вынесен сюда, так как востребован в MCellA и MCellB
        /// <summary>
        /// Сервисное значение (поиск в графе,  обслуживание и так далее) //default 0
        /// </summary>
        protected int m_serviceflag;//вынесен сюда, так как востребован в MCellA и MCellB

        /// <summary>
        /// Protected constructor
        /// </summary>
        /// <param name="sol">Объект Солюшена</param>
        protected MCell(MSolution sol)
        {
            this.m_Solution = sol;
            this.m_lastUsed = DateTime.Now;
            this.m_serviceflag = 0;
            //TODO: Добавить только MCell код инициализации здесь
            //для других полей инициализацию в производном классе делать

        }

        #region *** Properties ***
        //*** MElement property set *** - так как класс абстрактный, тут переопределять их не надо. Поэтому они удалены.

        /// <summary>
        /// Ссылка на объект Солюшена
        /// </summary>
        public override MSolution Solution
        {
            get { return m_Solution; }
        }

        /// <summary>
        /// NT-Сервисное значение
        /// Не сохраняется в БД, не учитывает флаг ReadOnly, не изменяет таймштамп изменения.
        /// </summary>
        /// <remarks>
        /// Значение используемое для поиска в графе и подобных целях. 
        /// По умолчанию = 0.
        /// Не сохраняется в БД, не учитывает флаг ReadOnly, не изменяет таймштамп изменения.
        /// Readonly: Не требуется, так как нет изменения Солюшена.
        /// </remarks>
        public override int ServiceFlag
        {
            get { return this.m_serviceflag; }
            set { this.m_serviceflag = value; }
        }

        //Дополнительные абстрактные проперти впридачу к MElement, для производных классов.

        /// <summary>
        /// Cell data value
        /// </summary>
        public abstract byte[] Value
        {
            get;
            set;
        }

        /// <summary>
        /// Cell data value type id
        /// </summary>
        public abstract MCellId ValueTypeId
        {
            get;
            set;
        }

        /// <summary>
        /// Cell link collection. 
        /// Only for link reading!
        /// </summary>
        public abstract MLinkCollection Links
        {
            get;
        }

        /// <summary>
        /// Cell (saving) mode: Compact, Normal, DelaySave, Temporary
        /// </summary>
        public abstract MCellMode CellMode
        {
            get;
            internal set;
        }

        /// <summary>
        /// Current cell is MCellB cell? 
        /// (This property is read only)
        /// </summary>
        public abstract bool isCompactCell
        {
            get;
        }
        #endregion

        /// <summary>
        /// NT-Сохраняет DelaySave или Temporary ячейку и ее связи в БД. Ячейка становится Normal типа.
        /// Для Normal или Compact ячеек ничего не делает.
        /// </summary>
        /// <remarks>
        /// Readonly: Выбросить исключение
        /// </remarks>
        public abstract void Save();

        /// <summary>
        /// Помечает ячейку удаленной. 
        /// Если ячейка не временная, не отложенной записи, то флаг удаления автоматически записывается в таблицу БД.
        /// </summary>
        /// <remarks>
        /// Readonly: Выбросить исключение, если не временная ячейка.
        /// </remarks>
        public abstract void Delete();

        #region *** MObject serialization functions ***
        // осмотрены для первой компиляции проекта 
        /// <summary>
        /// Convert object data to binary stream
        /// </summary>
        /// <param name="writer">Binary stream writer</param>
        public override void toBinary(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Convert object data from binary stream
        /// </summary>
        /// <param name="reader">Binary stream reader</param>
        public override void fromBinary(BinaryReader reader)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Convert object data to byte array
        /// </summary>
        /// <returns></returns>
        public override byte[] toBinaryArray()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Convert object data to text string
        /// </summary>
        /// <param name="withHex">True - include HEX representation of binary data</param>
        /// <returns></returns>
        public override string toTextString(bool withHex)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Convert object data to text stream
        /// </summary>
        /// <param name="writer">text stream writer</param>
        /// <param name="withHex">True - include HEX representation of binary data</param>
        public override void toText(TextWriter writer, bool withHex)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Convert object data from text stream
        /// </summary>
        /// <param name="reader">text stream reader</param>
        public override void fromText(TextReader reader)
        {
            throw new NotImplementedException();
        }
        #endregion


        /// <summary>
        /// NT-Получить описание для отладчика
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //Readonly: Не требуется, так как нет изменения Солюшена.
            return "MCell object";
        }

        /// <summary>
        /// NT-Возвращает True если указанный объект MCell или MCellA или MCellB класса
        /// </summary>
        /// <param name="obj">Проверяемый объект</param>
        /// <returns>Возвращает True если указанный объект MCell или MCellA или MCellB класса</returns>
        public static bool IsCellType(Object obj)
        {
            //Readonly: Не требуется, так как нет изменения Солюшена.
            Type t = obj.GetType();
            return ((t.Equals(typeof(MCell))) || ((t.Equals(typeof(MCellA)))) || ((t.Equals(typeof(MCellB)))));
        }

        /// <summary>
        /// NT-Обновить таймштамп LastUsed для поддержки счетчика использования ячейки
        /// </summary>
        /// <remarks>
        /// Ранее я хотел просто вызывать ее при каждом обращении к любому проперти get.
        /// Но это неправильно - ведь поле LastUsed нужно для того, чтобы выгружать неиспользуемые ячейки из коллекции ячеек контейнера.
        /// А значит, таймштамп должен устанавливаться при реальном использовании ячейки, а не всегда.
        /// Иначе поиск в коллекции ячеек установит таймштампы всех ячеек, и они все будут выглядеть востребованными.
        /// TODO: Поэтому надо определить список операций, при которых ячейки (не) затребуются с пользой,
        /// и в них вызывать эту функцию для пометки ячейки востребованной.
        /// </remarks>
        public void thisCellUsed()
        {
            this.m_lastUsed = DateTime.Now;
        }

        //TODO: Использовать thisCellUsed(); в каждой здесь функции, если есть обращение к полям, но не к проперти.

        //TODO: при изменении ячейки также изменять таймштамп изменений 
        //через  updateLastChange() для полей:

        //TODO: при изменении ячейки учитывать флаг m_ReadOnly!
        // Сейчас в солюшене Read-Only нельзя изменять временные ячейки, такой код. А по концепции - должно быть можно.
        // Сейчас я код не хочу исправлять, так как слишком сложно будет отлаживать все эти заковыки по всему Движку.
        //Но после первого релиза надо попробовать реализовать эту возможность. 
        //Изменения ожидаются в проперти классов MCellB MLink.
        //это функция проверки ридонли в MElement ? checkReadOnly()
        //- для постоянной ячейки и связи: если ячейка.РО или солюшен.РО то выбросить исключение иначе продолжать (return false)
        //- для временной ячейки и связи: если ячейка.РО то выбросить исключение, иначе продолжать (return false).
        //надо просто переписать эту функцию и проверить, что все везде работает правильно, и в других местах.
        //Но сейчас это слишком сложно проверять и отлаживать - весь движок полон подобных выкрутасов. 


        //TODO: Добавить функции MCell здесь

        /// <summary>
        /// NR-Выгрузить эту ячейку из памяти и из списка ячеек контейнера
        /// </summary>
        /// <remarks>
        /// Readonly: Зависит от реализации
        /// </remarks>
        public void Unload()
        {
            //Readonly: Зависит от реализации
            throw new NotImplementedException();//TODO: Add code here...
        }
        /// <summary>
        /// NR-
        /// </summary>
        /// <param name="Axis"></param>
        /// <param name="axisDirection"></param>
        /// <param name="targetCell"></param>
        /// <returns></returns>
        /// <remarks>
        /// Связь создается только временной, если хотя бы одна из ячеек временная. 
        /// Нельзя создать постоянную связь с временными ячейками. 
        /// Но можно создать временную связь между двумя постоянными ячейками.
        /// </remarks>
        public MLink CreateLink(MCellId Axis, MLinkDirection axisDirection, MCell targetCell)
        {
            //Readonly: Выбросить исключение, если не временная связь.
            throw new NotImplementedException();//TODO: Add code here...
        }
        /// <summary>
        /// NR-
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="axisDirection"></param>
        /// <param name="targetCell"></param>
        /// <returns></returns>
        public int DeleteLink(MCellId axis, MLinkDirection axisDirection, MCell targetCell)
        {
            //Readonly: Выбросить исключение, если не временная связь.
            throw new NotImplementedException();//TODO: Add code here...
        }






    }
}
