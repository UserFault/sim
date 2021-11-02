using System;
using System.Text;
using System.IO;
using System.Reflection;

namespace Bar
{
    /// <summary>
    /// NT-Представляет объект версии, приспособленный для сериализации.
    /// </summary>
    /// <remarks> 
    /// См. wiki:///V:/МоиПроекты/ДвижокТапп/Справочники/ДвижокТаппВики/ДвижокТаппВики.wiki?page=MVersion
    /// </remarks>
    public class MVersion : MObject
    {
        /// <summary>
        /// Текстовый разделитель полей номера версии
        /// </summary>
        private const char delimiter = '.';

        #region *** Fields ***
        /// <summary>
        /// НомерВерсииДвижка обозначает изменения, несовместимые с предыдущими версиями.
        /// </summary>
        protected int m_Major;
        /// <summary>
        /// НомерПодверсииДвижка обозначает изменения, при которых сохраняется совместимость с предыдущими версиями
        /// </summary>
        protected int m_Minor;
        /// <summary>
        /// НомерРевизииДвижка - тут должны были перечисляться хотфиксы и всякие фишки, но, честно говоря, номер сборки вполне мог бы их заменить. Ну, пусть будет ревизия, для соответствия стандартам.
        /// </summary>
        protected int m_Revision;
        /// <summary>
        /// НомерСборкиДвижка - Порядковый номер сборки внутри версии.
        /// </summary>
        protected int m_Build;
        #endregion
        /// <summary>
        /// NT-Конструктор
        /// </summary>
        public MVersion()
        {
            this.m_Major = 0;
            this.m_Minor = 0;
            this.m_Revision = 0;
            this.m_Build = 0;
        }
        /// <summary>
        /// NT-Конструктор из строки версии
        /// </summary>
        /// <param name="text">Строка версии. Пример: 1.16.2.32</param>
        public MVersion(String text)
        {
            fromTextString(text);
        }

        /// <summary>
        /// NT-Конструктор из объекта версии
        /// </summary>
        /// <param name="ver"></param>
        public MVersion(Version ver)
        {
            this.m_Major = ver.Major;
            this.m_Minor = ver.Minor;
            this.m_Revision = ver.Revision;
            this.m_Build = ver.Build;
        }

        #region *** Properties ***
        /// <summary>
        /// обозначает изменения, несовместимые с предыдущими версиями.
        /// </summary>
        public int VersionNumber
        {
            get {   return this.m_Major; }
            set {   this.m_Major = value; }
        }

        /// <summary>
        /// обозначает изменения, при которых сохраняется совместимость с предыдущими версиями
        /// </summary>
        public int SubversionNumber
        {
            get { return this.m_Minor; }
            set { this.m_Minor = value; }
        }

        /// <summary>
        /// обозначает хотфиксы и всякие фишки, не влияющие на совместимость версий.
        /// </summary>
        public int RevisionNumber
        {
            get { return m_Revision; }
            set { this.m_Revision = value; }
        }

        /// <summary>
        /// Номер последовательного этапа развития
        /// </summary>
        public int BuildNumber
        {
            get { return this.m_Build; }
            set { this.m_Build = value; }
        }
        #endregion

        /// <summary>
        /// NT-Получить версию сборки Движка
        /// </summary>
        /// <returns>Возвращает версию текущей сборки</returns>
        public static MVersion getCurrentAssemblyVersion()
        {
            return new MVersion(Assembly.GetExecutingAssembly().GetName().Version);
        }

        /// <summary>
        /// NT-Check that specified engine version is compatible with current version
        /// </summary>
        /// <param name="ver">Engine version info object</param>
        /// <returns>Returns True if specified version compatible with current version. Return False otherwise.</returns>
        /// <remarks>сейчас совместимость версий проверяется по majorversion только</remarks>
        public bool isCompatibleVersion(MVersion ver)
        {
            //TODO: для нового релиза отредактируйте код, чтобы показать совместимость сверху вниз.
            return (this.m_Major == ver.m_Major);
        }

        /// <summary>
        /// NT-Проверить, что оба объекта одинаковы
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool isEqual(MVersion other)
        {
            return ((this.m_Build == other.m_Build) && (this.m_Major == other.m_Major)
                && (this.m_Minor == other.m_Minor) && (this.m_Revision == other.m_Revision));
        }


        /// <summary>
        /// NT-Распарсить строку версии
        /// </summary>
        /// <param name="text"></param>
        private void fromTextString(string text)
        {
            String[] sar = text.Split(delimiter);
            if (sar.Length != 4)
                throw new ArgumentException("Invalid VersionInfo value");
            this.m_Major = Int32.Parse(sar[0]);
            this.m_Minor = Int32.Parse(sar[1]);
            this.m_Revision = Int32.Parse(sar[2]);
            this.m_Build = Int32.Parse(sar[3]);

            return;
        }
        /// <summary>
        /// NT-Get string representation of object
        /// </summary>
        /// <returns>Return string representation of object, like "1.0.0.0"</returns>
        public override string ToString()
        {
            return this.toTextString(false);
        }

        #region MObject Members
        /// <summary>
        /// NT-Convert object data to binary stream
        /// </summary>
        /// <param name="writer">Binary stream writer</param>
        public override void toBinary(BinaryWriter writer)
        {
            writer.Write(this.m_Major);
            writer.Write(this.m_Minor);
            writer.Write(this.m_Revision);
            writer.Write(this.m_Build);

            return;
        }
        /// <summary>
        /// NT-Convert object data from binary stream
        /// </summary>
        /// <param name="reader">Binary stream reader</param>
        public override void fromBinary(BinaryReader reader)
        {
            this.m_Major = reader.ReadInt32();
            this.m_Minor = reader.ReadInt32();
            this.m_Revision = reader.ReadInt32();
            this.m_Build = reader.ReadInt32();

            return;
        }
        /// <summary>
        /// NT-Convert object data to byte array
        /// </summary>
        /// <returns></returns>
        public override byte[] toBinaryArray()
        {
            //create memory stream and writer
            MemoryStream ms = new MemoryStream(64);//initial size for cell data 
            BinaryWriter bw = new BinaryWriter(ms, Encoding.Unicode);
            //convert data
            this.toBinary(bw);
            //close memory stream and get bytes
            bw.Close();
            return ms.ToArray();
        }
        /// <summary>
        /// NT-Convert object data to text string
        /// </summary>
        /// <param name="withHex">True - include HEX representation of binary data</param>
        /// <returns></returns>
        public override string toTextString(bool withHex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(m_Major);
            sb.Append(delimiter);
            sb.Append(m_Minor);
            sb.Append(delimiter);
            sb.Append(m_Revision);
            sb.Append(delimiter);
            sb.Append(m_Build);

            return sb.ToString();
        }
        /// <summary>
        /// NT-Convert object data to text stream
        /// </summary>
        /// <param name="writer">text stream writer</param>
        /// <param name="withHex">True - include HEX representation of binary data</param>
        public override void toText(TextWriter writer, bool withHex)
        {
            writer.Write(this.toTextString(withHex));
        }
        /// <summary>
        /// NR-Convert object data from text stream
        /// </summary>
        /// <param name="reader">text stream reader</param>
        public override void fromText(TextReader reader)
        {
            throw new NotImplementedException();//TODO: Написать код разбора потока символов для MObject.fromText()
            //тут непонятно, как разбирать входной поток - нет разделителей данных
            //а городить парсер-автомат состояний мне не хочется без необходимости.
        }

        #endregion
    }
}
