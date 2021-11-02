using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.IO;

namespace Bar
{
    /// <summary>
    /// NT-Представляет статистику Солюшена
    /// </summary>
    /// <remarks>
    /// Разгружает класс настроек Солюшена от свойств статистики Солюшена
    /// </remarks>
    public class MStatistics : MObject
    {
        #region *** MStatistic fields ***
        /// <summary>
        /// Number of const and temp cells in memory
        /// </summary>
        protected int m_cellsmem;
        /// <summary>
        /// Number of const cells in database
        /// </summary>
        protected int m_cellsconst;
        /// <summary>
        /// Number of temp cells in memory
        /// </summary>
        protected int m_cellstemp;
        /// <summary>
        /// Number of used external cells  in any other containers
        /// </summary>
        protected int m_cellsext;
        /// <summary>
        /// Number of const and temp links in memory
        /// </summary>
        protected int m_linksmem;
        /// <summary>
        /// Number of const links in database
        /// </summary>
        protected int m_linksconst;
        /// <summary>
        /// Number of temp links in memory
        /// </summary>
        protected int m_linkstemp;
        /// <summary>
        /// Number of links to any external cells
        /// </summary>
        protected int m_linksext;
        /// <summary>
        /// Number of resource files in project
        /// </summary>
        protected int m_resourcefiles;
        /// <summary>
        /// Size of resource files in bytes
        /// </summary>
        protected long m_resourcesize;
        /// <summary>
        /// Список идентификаторов связанных внешних Солюшенов
        /// </summary>
        private List<Int32> m_externalSolutionList;

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public MStatistics()
        {
            this.m_cellsconst = 0;
            this.m_cellsext = 0;
            this.m_cellsmem = 0;
            this.m_cellstemp = 0;
            this.m_externalSolutionList = new List<int>();
            this.m_linksconst = 0;
            this.m_linksext = 0;
            this.m_linksmem = 0;
            this.m_linkstemp = 0;
            this.m_resourcefiles = 0;
            this.m_resourcesize = 0;
        }

        #region *** MStatistic properties ***
        /// <summary>
        /// Number of const and temp cells in memory
        /// </summary>
        [Category("Statistics"), Description("Number of temporary and constant cells in memory")]
        public int Stat_CellsInMemory
        {
            get
            {
                return m_cellsmem;
            }
            set
            {
                m_cellsmem = value;
            }
        }

        /// <summary>
        /// Number of const cells in database
        /// </summary>
        [Category("Statistics"), Description("Number of constant cells in database")]
        public int Stat_ConstantCells
        {
            get
            {
                return m_cellsconst;
            }
            set
            {
                m_cellsconst = value;
            }
        }

        /// <summary>
        /// Number of temp cells in memory
        /// </summary>
        [Category("Statistics"), Description("Number of temporary cells")]
        public int Stat_TemporaryCells
        {
            get
            {
                return m_cellstemp;
            }
            set
            {
                m_cellstemp = value;
            }
        }

        /// <summary>
        /// Number of used external cells  in any other containers
        /// </summary>
        [Category("Statistics"), Description("Number of external linked cells")]
        public int Stat_ExternalCells
        {
            get
            {
                return m_cellsext;
            }
            set
            {
                m_cellsext = value;
            }
        }

        /// <summary>
        /// Number of const links in database
        /// </summary>
        [Category("Statistics"), Description("Number of constant links in database")]
        public int Stat_ConstantLinks
        {
            get
            {
                return m_linksconst;
            }
            set
            {
                m_linksconst = value;
            }
        }

        /// <summary>
        /// Number of const and temp links in memory
        /// </summary>
        [Category("Statistics"), Description("Number of constant and temporary links in memory")]
        public int Stat_LinksInMemory
        {
            get
            {
                return m_linksmem;
            }
            set
            {
                m_linksmem = value;
            }
        }

        /// <summary>
        /// Number of temp links in memory
        /// </summary>
        [Category("Statistics"), Description("Number of temporary links")]
        public int Stat_TemporaryLinks
        {
            get
            {
                return m_linkstemp;
            }
            set
            {
                m_linkstemp = value;
            }
        }

        /// <summary>
        /// Number of links to any external cells
        /// </summary>
        [Category("Statistics"), Description("Number of links to any external cells")]
        public int Stat_ExternalLinks
        {
            get
            {
                return m_linksext;
            }
            set
            {
                m_linksext = value;
            }
        }

        /// <summary>
        /// Number of resource files in project
        /// </summary>
        [Category("Statistics"), Description("Number of resource files in project")]
        public int Stat_ResourceFiles
        {
            get
            {
                return m_resourcefiles;
            }
            set
            {
                m_resourcefiles = value;
            }
        }

        /// <summary>
        /// Size of resource files in bytes
        /// </summary>
        [Category("Statistics"), Description("Size of resource files in bytes")]
        public long Stat_ResourceSize
        {
            get
            {
                return m_resourcesize;
            }
            set
            {
                m_resourcesize = value;
            }
        }

        /// <summary>
        /// NT-Список идентификаторов связанных внешних Солюшенов
        /// </summary>
        [Category("Statistics"), Description("List of linked external Solutions")]
        public List<Int32> Stat_ExternalSolutionList
        {
            get { return m_externalSolutionList; }
            set { m_externalSolutionList = value; }
        }
        #endregion
        /// <summary>
        /// NR-Get string representation of object.
        /// </summary>
        /// <returns>Return string representation of object.</returns>
        public override string ToString()
        {
            return base.ToString();
        }

        #region *** MObject serialization functions ***
        /// <summary>
        /// NR-Convert object data to binary stream
        /// </summary>
        /// <param name="writer">Binary stream writer</param>
        public override void toBinary(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// NR-Convert object data from binary stream
        /// </summary>
        /// <param name="reader">Binary stream reader</param>
        public override void fromBinary(BinaryReader reader)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// NT-Convert object data to byte array
        /// </summary>
        /// <returns></returns>
        public override byte[] toBinaryArray()
        {
            return this.toBinaryArraySub();
        }
        /// <summary>
        /// NR-Convert object data to text string
        /// </summary>
        /// <param name="withHex">True - include HEX representation of binary data</param>
        /// <returns></returns>
        public override string toTextString(bool withHex)
        {
            throw new NotImplementedException();//TODO: Добавить обобщенный наследуемый код сериализации здесь
        }
        /// <summary>
        /// NR-Convert object data to text stream
        /// </summary>
        /// <param name="writer">text stream writer</param>
        /// <param name="withHex">True - include HEX representation of binary data</param>
        public override void toText(TextWriter writer, bool withHex)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// NR-Convert object data from text stream
        /// </summary>
        /// <param name="reader">text stream reader</param>
        public override void fromText(TextReader reader)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// NT-вспомогательная функция
        /// </summary>
        /// <returns></returns>
        protected byte[] toBinaryArraySub()
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

        #endregion
    }
}
