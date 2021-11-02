using System;
using System.Collections.Generic;
using System.Text;

namespace TestShell
{
    /// <summary>
    /// Record for custom structure places
    /// </summary>
    public class PlaceRecord
    {
        private String m_placeName;
        private String m_placeDescr;
        private UInt64 m_placeId;

        public PlaceRecord()
        {
        }

        public PlaceRecord(UInt64 id, string name, string descr)
        {
            m_placeId = id;
            m_placeName = name;
            m_placeDescr = descr;
        }

        /// <summary>
        /// Place name
        /// </summary>
        public String Name
        {
            get { return m_placeName; }
            set { m_placeName = value; }
        }
        /// <summary>
        /// Place description
        /// </summary>
        public String Description
        {
            get { return m_placeDescr; }
            set { m_placeDescr = value; }
        }
        /// <summary>
        /// Place identificator
        /// </summary>
        public UInt64 ID
        {
            get { return m_placeId; }
            set { m_placeId = value; }
        }

    }
}
