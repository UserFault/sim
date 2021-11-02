using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TestShell
{
    /// <summary>
    /// Custom places collection
    /// </summary>
    public class PlacesCollection
    {
        private List<PlaceRecord> m_placeList;
        /// <summary>
        /// Category name
        /// </summary>
        private string m_name;
        private string m_descr;

        public PlacesCollection()
        {
            m_placeList = new List<PlaceRecord>();
        }

        public PlacesCollection(string name, string description)
        {
            m_placeList = new List<PlaceRecord>();
            m_name = name;
            m_descr = description;
        }
        /// <summary>
        /// Records category name
        /// </summary>
        public string CategoryName
        {
            get { return m_name; }
            set { m_name = value; }
        }
        /// <summary>
        /// Records category description
        /// </summary>
        public string CategoryDescription
        {
            get { return m_descr; }
            set { m_descr = value; }
        }

        /// <summary>
        /// List of custom places
        /// </summary>
        public List<PlaceRecord> Records
        {
            get { return m_placeList; }
        }

        
        /// <summary>
        /// Get record by place name. Return null if no records found.
        /// </summary>
        /// <param name="name">Place name ignore case</param>
        /// <returns></returns>
        public PlaceRecord GetByName(string name)
        {
            foreach (PlaceRecord pr in m_placeList)
                if (String.Equals(name, pr.Name, StringComparison.OrdinalIgnoreCase))
                    return pr;
            //if nothing found
            return null; 
        }

        /// <summary>
        /// Add record to list. Returns false if name already exists.
        /// </summary>
        /// <param name="id">place id</param>
        /// <param name="name">place name</param>
        /// <param name="description">place description</param>
        public bool Add(UInt64 id, String name, String description)
        {
            PlaceRecord pr = new PlaceRecord();
            pr.ID = id;
            pr.Name = name;
            pr.Description = description;
            return Add(pr);
        }

        /// <summary>
        /// Add record to list. Returns false if name already exists.
        /// </summary>
        /// <param name="rec"></param>
        /// <returns></returns>
        public bool Add(PlaceRecord rec)
        {
            //check unical name
            if (GetByName(rec.Name) != null)
                return false;
            //add record
            m_placeList.Add(rec);
            return true;
        }

        /// <summary>
        /// Remove record from list.
        /// </summary>
        /// <param name="name"></param>
        public void Remove(String name)
        {
            PlaceRecord pr = this.GetByName(name);
            if (pr != null) m_placeList.Remove(pr);
        }

        /// <summary>
        /// Save colection to XML file
        /// </summary>
        /// <param name="pathname">file pathname</param>
        public void Save(string pathname)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(this.GetType());
            System.IO.StreamWriter file = new System.IO.StreamWriter(pathname);
            writer.Serialize(file, this);
            file.Close();
        }
        /// <summary>
        /// Load collection from XML file
        /// </summary>
        /// <param name="pathname">file pathname</param>
        /// <returns></returns>
        public static PlacesCollection Load(string pathname)
        {
            //if File exists load file
            PlacesCollection set;
            if (File.Exists(pathname))
            {
                //load file
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(PlacesCollection));
                System.IO.StreamReader file = new System.IO.StreamReader(pathname);
                set = (PlacesCollection)reader.Deserialize(file);
                file.Close();
            }
                //else create dummy collection
            else set = new PlacesCollection();
            return set;
        }


    }
}
