using System;
using System.Collections.Generic;
using System.Text;

namespace Mary
{
    /// <summary>
    /// Represent arguments for method
    /// </summary>
    public class MKta
    {
        /// <summary>
        /// List of arguments
        /// </summary>
        private List<MArg> m_argList;
        /// <summary>
        /// Method result
        /// </summary>
        private MArg m_result;

        /// <summary>
        /// Default constructor
        /// </summary>
        public MKta()
        {
            m_argList = new List<MArg>();
            m_result = new MArg();
        }

        /// <summary>
        /// List of arguments
        /// </summary>
        public List<MArg> argList
        {
            get
            {
                return m_argList;
            }
        }
        /// <summary>
        /// Result argument
        /// </summary>
        public MArg Result
        {
            get
            {
                return m_result;
            }
            set
            {
                m_result = value; //now use reference
            }
        }

    }
}
