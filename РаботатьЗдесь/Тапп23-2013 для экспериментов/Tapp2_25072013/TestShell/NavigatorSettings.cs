using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace TestShell
{
    public class NavigatorSettings
    {
        private List<PlacesCollection> m_CategoryList;

        private bool m_NavigatorLayoutVertical;
        private const string m_CategoryDefault = "Defaults";
        private const string m_CategorySystem = "SystemItems";
        private Size m_placeEditorFormSize;


        public NavigatorSettings()
        {
            m_CategoryList = new List<PlacesCollection>();

        }
        /// <summary>
        /// Get list of categories of places
        /// </summary>
        public List<PlacesCollection> CategoryList
        {
            get { return m_CategoryList; }
        }
        /// <summary>
        /// Navigator form layout
        /// </summary>
        public bool NavigatorLayoutVertical
        {
            get { return m_NavigatorLayoutVertical; }
            set { m_NavigatorLayoutVertical = value; }
        }

        /// <summary>
        /// Size of places editor form
        /// </summary>
        public Size PlacesEditorFormSize
        {
            get { return m_placeEditorFormSize; }
            set { m_placeEditorFormSize = value; }
        }

        /// <summary>
        /// Returns category by name or null if not exists
        /// </summary>
        /// <param name="name">Category name</param>
        /// <returns></returns>
        public PlacesCollection getCategory(string name)
        {
            foreach (PlacesCollection p in m_CategoryList)
                if (String.Equals(p.CategoryName, name, StringComparison.OrdinalIgnoreCase))
                    return p;
            return null;
        }

        /// <summary>
        /// Add category to list. Return false if this name already exists.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool Add(PlacesCollection p)
        {
            //if category already exists
            if (getCategory(p.CategoryName) != null)
                return false;
            m_CategoryList.Add(p);
            return true;
        }
        /// <summary>
        /// Remove first category match specified name
        /// </summary>
        /// <param name="name"></param>
        public void Remove(string name)
        {
            PlacesCollection p = getCategory(name);
            if (p != null) m_CategoryList.Remove(p);
            return;
        }

        /// <summary>
        /// Get category Default
        /// </summary>
        /// <returns></returns>
        public PlacesCollection GetCategoryDefault()
        {
            return this.getCategory(m_CategoryDefault);
        }

        /// <summary>
        /// Get category System
        /// </summary>
        /// <returns></returns>
        public PlacesCollection GetCategorySystem()
        {
            return this.getCategory(m_CategorySystem);
        }

        /// <summary>
        /// Returns place from Defaults category or return null
        /// </summary>
        /// <param name="placename"></param>
        /// <returns></returns>
        public PlaceRecord getDefaultPlace(string placename)
        {
            PlacesCollection def = this.getCategory(m_CategoryDefault);
            if(def == null) return null;
            return def.GetByName(placename);
        }


        /// <summary>
        /// Returns place from System category or return null
        /// </summary>
        /// <param name="placename"></param>
        /// <returns></returns>
        public PlaceRecord getSystemPlace(string placename)
        {
            PlacesCollection def = this.getCategory(m_CategorySystem);
            if (def == null) return null;
            return def.GetByName(placename);
        }

        /// <summary>
        /// Save colection to XML file
        /// </summary>
        /// <param name="path">project directory</param>
        public void Save(string path)
        {
            string pathname = Path.Combine(path, "NaviSett.xml");

            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(this.GetType());
            System.IO.StreamWriter file = new System.IO.StreamWriter(pathname);
            writer.Serialize(file, this);
            file.Close();
        }
        /// <summary>
        /// Load collection from XML file
        /// </summary>
        /// <param name="path">project directory</param>
        /// <returns></returns>
        public static NavigatorSettings Load(string path)
        {
            string pathname = Path.Combine(path, "NaviSett.xml");
            //if File exists load file
            NavigatorSettings set;
            if (File.Exists(pathname))
            {
                //load file
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(NavigatorSettings));
                System.IO.StreamReader file = new System.IO.StreamReader(pathname);
                set = (NavigatorSettings)reader.Deserialize(file);
                file.Close();
            }
            //else create dummy collection
            else
            {
                set = new NavigatorSettings();
                set.Reinitialize();
            }
            return set;
        }

        /// <summary>
        /// Add default items to cleared collection
        /// </summary>
        public void Reinitialize()
        {
            //clear items
            if (m_CategoryList.Count != 0)
                m_CategoryList.Clear();
            //add Default items
            PlacesCollection p;
            p = new PlacesCollection(m_CategoryDefault, "Default identifiers for new cells and links");
            p.Records.Add(new PlaceRecord(0, "DefaultCellType", "Default cell type for new cells"));
            p.Records.Add(new PlaceRecord(0, "DefaultCellState", "Default cell state for new cells"));
            p.Records.Add(new PlaceRecord(0, "DefaultCellDatatype", "Default cell datatype for new cells"));
            p.Records.Add(new PlaceRecord(0, "DefaultLinkAxis", "Default link axis for new cells"));
            p.Records.Add(new PlaceRecord(0, "DefaultLinkState", "Default link state for new cells"));
            m_CategoryList.Add(p);
            //add System items
            p = new PlacesCollection(m_CategorySystem, "Places in System part of structure");
            p.Records.Add(new PlaceRecord(0, "World", "Starting point of entire structure"));
            p.Records.Add(new PlaceRecord(0, "System", "Starting point of system part of structure"));
            m_CategoryList.Add(p);
            //add user category
            p = new PlacesCollection("UserCategory", "Some custom places");
            p.Records.Add(new PlaceRecord(0, "ExamplePlace", "Example of user places"));
            m_CategoryList.Add(p);

            //init places editor size
            m_placeEditorFormSize = new Size(340, 270);
            return;
        }

        /// <summary>
        /// Get places by name. Search in all categories.
        /// </summary>
        /// <param name="placeName"></param>
        /// <returns></returns>
        public PlaceRecord GetByName(string placeName)
        {
            PlaceRecord p;
            foreach (PlacesCollection pc in m_CategoryList)
            {
                p = pc.GetByName(placeName);
                if (p != null) return p;
            }
            return null;
        }
    }
}
