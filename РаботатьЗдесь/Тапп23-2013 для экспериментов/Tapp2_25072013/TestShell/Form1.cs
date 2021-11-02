using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Mary;

namespace TestShell
{
    public partial class TestShellForm : Form
    {
        /// <summary>
        /// Container object
        /// </summary>
        internal MEngine m_container;

        public TestShellForm()
        {
            InitializeComponent();
            m_container = null;
        }


        /// <summary>
        /// NewProject menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                NewProjectForm npf = new NewProjectForm();
                if (npf.ShowDialog(this) == DialogResult.OK)
                {
                    //create new project
                    this.toolStripStatusLabel1.Text = String.Format("Creating new project {0}", npf.ProjectName);
                    Application.DoEvents();
                    MSolutionInfo info = new MSolutionInfo();
                    info.DatabaseType = MDatabaseType.Unknown;
                    info.DatabaseType = npf.DatabaseType;
                    info.SolutionName = npf.ProjectName;
                    info.SolutionDescription = npf.ProjectDescription;
                    info.DatabaseServerPath = npf.SqlServerPath;
                    info.DatabaseName = npf.DatabaseName;
                    info.UserName = npf.UserName;
                    info.UserPassword = npf.UserPassword;
                    info.DatabaseTimeout = 30;
                    MEngine en = new MEngine();
                    en.SolutionCreate(npf.ParentFolder, info);//TODO: TAGVERSIONNEW: - не все требуемые поля заполнены здесь!
                    en.SolutionClose(true);
                    //show any exception here...
                    this.toolStripStatusLabel1.Text = String.Format("Project {0} successfully created. You can open this project now.", npf.ProjectName);
                    playBeep();
                }
            }
            catch (Exception ex)
            {
                ShowExceptionMessageBox(ex);
            }
        }

        private void ShowExceptionMessageBox(Exception ex)
        {
            MessageBox.Show(this, ex.Message, String.Format("TestShell - {0}", ex.GetType().Name), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// Menu Project-Exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Menu Project-Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = createFileDialog("Select project file for Delete");
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    this.toolStripStatusLabel1.Text = String.Format("Delete project {0}", ofd.FileName);
                    Application.DoEvents();
                    MEngine.SolutionDelete(ofd.FileName);
                    this.toolStripStatusLabel1.Text = String.Format("Project {0} successfully deleted.", ofd.SafeFileName);
                    playBeep();
                }
            }
            catch (Exception ex)
            {
                ShowExceptionMessageBox(ex);
            }
            return;
        }

        /// <summary>
        /// Creates OpenFileDialog for project file opening
        /// </summary>
        /// <param name="Title"></param>
        /// <returns></returns>
        private OpenFileDialog createFileDialog(string Title)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ofd.Multiselect = false;
            ofd.RestoreDirectory = true;
            ofd.Title = Title;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.DereferenceLinks = true; //use shortcuts for project opening
            ofd.Filter = "Tapp project file(.tapj)|*.tapj|All files|*.*";
            ofd.FilterIndex = 1;
            return ofd;
        }

        /// <summary>
        /// Menu Project-SolutionOpen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = createFileDialog("Select project file for SolutionOpen");
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    this.toolStripStatusLabel1.Text = String.Format("SolutionOpen project {0}", ofd.FileName);
                    Application.DoEvents();
                    m_container = new MEngine(); 
                    m_container.SolutionOpen(ofd.FileName);
                    this.toolStripStatusLabel1.Text = String.Format("Project {0} successfully opened.", ofd.SafeFileName);
                    playBeep();
                }
            }
            catch (Exception ex)
            {
                ShowExceptionMessageBox(ex);
            }
            return;
        }

        /// <summary>
        /// Success sound
        /// </summary>
        public static void playBeep()
        {
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer(Properties.Resources.ding);
            sp.Play();
            
        }
        /// <summary>
        /// Error sound
        /// </summary>
        public static void playError()
        {
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer(Properties.Resources.chord);
            sp.Play();
        }

        /// <summary>
        /// Completion sound
        /// </summary>
        public static void playClick()
        {
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer(Properties.Resources.click);
            sp.Play();
        }

        /// <summary>
        /// Menu Project-Close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //check project opened
            if (m_container == null)
            {
                this.toolStripStatusLabel1.Text = "No opened projects";
                playError();
                return;
            }
            try
            {
                //closing project
                string prjname = m_container.SolutionSettings.SolutionName;
                this.toolStripStatusLabel1.Text = String.Format("Close project {0}", prjname);
                Application.DoEvents();
                //prompt user for save before close
                DialogResult dr = MessageBox.Show(this, "Do you want to save project before closing?", "Closing project", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if ((dr != DialogResult.No) && (dr != DialogResult.Yes))
                {
                    this.toolStripStatusLabel1.Text = String.Format("Project {0} closing cancelled", prjname);
                    return;
                }
                bool save = false;
                if (dr == DialogResult.Yes) save = true;
                //close project
                m_container.SolutionClose(save);
                this.toolStripStatusLabel1.Text = String.Format("Project {0} closed", prjname);
                m_container = null;
                playBeep();
            }
            catch (Exception ex)
            {
                ShowExceptionMessageBox(ex);
            }
            return;
        }

        /// <summary>
        /// Menu Project-Clear
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //check project opened
            if (m_container == null)
            {
                this.toolStripStatusLabel1.Text = "No opened projects";
                playError();
                return;
            }
            try
            {
                string prjname = m_container.SolutionSettings.SolutionName;
                //prompt confirmation to clear project
                DialogResult dr = MessageBox.Show(this, String.Format("Do you want to clear {0} project?", prjname), "Clear project", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    m_container.SolutionClear();
                    this.toolStripStatusLabel1.Text = String.Format("Project {0} cleared", prjname);
                    playBeep();

                }
            }
            catch (Exception ex)
            {
                ShowExceptionMessageBox(ex);
            }
        }


        /// <summary>
        /// Menu Test-LoadCsv
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadCsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //создаем структуру ячеек со связями
            //tableCell -> colsCell, rowsCell
            //colsCell -> columns
            //rowsCell -> rows
            //row -> row fields
            //column -> row fields
            //это будет много связей, и много ячеек.

            //установить здесь тип создаваемых ячеек - чтобы померить скорость для каждого типа
            MCellMode cellMode = MCellMode.DelaySave;
            //идентификаторы осей связей
            const int Axis1 = 1;
            const int Axis2 = 2;
            const int Axis3 = 3;
            const int Axis4 = 4;

            //open csv file
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "SolutionOpen csv file:";
            ofd.Filter = "csv|*.csv";
            if (ofd.ShowDialog() != DialogResult.OK) return;

            //parse file
            System.IO.StreamReader sr = new System.IO.StreamReader(ofd.OpenFile(), Encoding.Default);
            string row = sr.ReadLine();
            // ; is delimiter
            string[] headers = row.Split(new char[] { ';' });
            //this strings is column headers
            int colNum = headers.Length; //columns counter
            int rowNum = 0; //rows counter

            //create table cell
            MCell tableCell = this.m_container.CellCreate(cellMode);
            tableCell.Name = "Table";
            //create column collection cell
            MCell colsCell = this.m_container.CellCreate(cellMode);
            colsCell.Name = "Column collection";
            colsCell.S1_createLink(new MID(Axis1), MAxisDirection.Up, tableCell);
            //create row collection cell
            MCell rowsCell = this.m_container.CellCreate(cellMode);
            rowsCell.Name = "Row collection";
            rowsCell.S1_createLink(new MID(Axis1), MAxisDirection.Up, tableCell);
            //create array of column cells
            MCell[] columns = new MCell[colNum];
            for (int i = 0; i < colNum; i++)
            {
                MCell tt = this.m_container.CellCreate(cellMode);
                tt.Name = headers[i];
                tt.S1_createLink(new MID(Axis2), MAxisDirection.Up, colsCell);
                columns[i] = tt;
            }
            //create row cells
            while (!sr.EndOfStream)
            {
                rowNum++;
                //show rows number
                if ((rowNum & 1023) == 0)
                {
                    this.toolStripStatusLabel1.Text = String.Format("Import row {0}",rowNum.ToString());
                    Application.DoEvents();
                }
                row = sr.ReadLine();
                headers = row.Split(new char[] { ';' });
                if (headers.Length != colNum)
                {
                    MessageBox.Show(String.Format("Invalid record format: {0} fields in {1} \n {2}", headers.Length, colNum, row));
                    return; //exit from loop
                }
                else
                {
                    //create row cell
                    MCell rc = this.m_container.CellCreate(cellMode);
                    rc.Name = rowNum.ToString();
                    //link to row collection cell
                    rc.S1_createLink(new MID(Axis2), MAxisDirection.Up, rowsCell);
                    //create cells
                    for (int i = 0; i < colNum; i++)
                    {
                        //номер столбца в файле будет типом ячейки
                        MCell column = columns[i];
                        //Заголовки столбцов из ксв не пишем - их потом как нибудь...
                        //данные ячейки тоже пока не трогаем - нет конвертера для строк
                        //Надо бы сделать конвертер и задействовать еще и поле данных ячейки
                        MCell mc = this.m_container.CellCreate(cellMode);
                        mc.Name = headers[i];//Имя ячейки - данные
                        mc.TypeId = new MID(column.CellID.ID);  //Тип ячейки - cellid столбца, то есть тип данных в данном случае.

                        //create links to row cell
                        mc.S1_createLink(new MID(Axis3), MAxisDirection.Up, rc);
                        //create links to column cell
                        mc.S1_createLink(new MID(Axis4), MAxisDirection.Up, column);
                    }
                }
            }  //end while
            sr.Close();
            playBeep();
            this.toolStripStatusLabel1.Text = String.Format("File {0} imported", ofd.SafeFileName);

            //save cells - check time
            m_container.SolutionSave();

            return;
        }
        /// <summary>
        /// Menu Structure - Create initial system 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createBaseInfrastructureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TAG:RENEW-13112017
            //Эта новая функция должна создать базовую инфраструктуру ячеек в структуре сущностей
            //предполагается, что: проект открыт, ни одной ячейки нет.
            //Если это не так, функция выдает мессагобокс о том что проект не пустой.
            if (m_container == null)
            {
                this.toolStripStatusLabel1.Text = "No opened projects";
                playError();
                return;
            }
            if (m_container.Cells.Count > 0)
            {
                MessageBox.Show("Контейнер не пустой, нельзя создать базовую структуру ячеек. Очистите контейнер сначала.", "Ошибка при создании начальной структуры", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CreateInitialStructure();

            //success exit
            this.toolStripStatusLabel1.Text = "Initial structure created";
            playBeep();
            return;
        }

        private void CreateInitialStructure()
        {
            //выбираем режим создаваемых ячеек - с БД или без БД
            MCellMode cellMode = MCellMode.Normal;
            //if (m_container.SolutionManager.UsesDatabase == false)
            //    cellMode = MCellMode.Temporary;

            #region *** Создание ячеек ***

            //1) теперь создаем начальную структуру ячеек
            //1.1) Функция создает ячейку указанного типа. А если нет БД, то сама переводит тип и создает только временные ячейки
            //а еще хорошо бы перенести создание ячеек в это проперти Cells - так оно логично получается, на взгляд неискушенного пользователя. 
            //Это как бы категория функциональности получается, в объекте контейнера.
            MCell world = m_container.CellCreate(cellMode, "World", "Начальный класс структуры сущностей");
            MCell sys = m_container.CellCreate(cellMode, "System", "Служебная часть структуры сущностей");
            MCell ct = m_container.CellCreate(cellMode, "CellTypes", "Коллекция ячеек типов ячеек");
            MCell bct = m_container.CellCreate(cellMode, "BaseCellType", "Базовая ячейка типа ячейки");
            MCell cdt = m_container.CellCreate(cellMode, "CellDataTypes", "Коллекция ячеек типов данных ячеек");
            MCell bcdt = m_container.CellCreate(cellMode, "BaseCellDataType", "Базовая ячейка типа данных ячейки");
            MCell cs = m_container.CellCreate(cellMode, "CellStates", "Коллекция ячеек состояния ячеек");
            MCell bcs = m_container.CellCreate(cellMode, "BaseCellState", "Базовая ячейка состояния ячейки");
            MCell la = m_container.CellCreate(cellMode, "LinkAxises", "Коллекция ячеек типов связи");
            MCell bla = m_container.CellCreate(cellMode, "BaseLinkAxis", "Базовая ячейка типа связи");
            MCell ls = m_container.CellCreate(cellMode, "LinkStates", "Коллекция ячеек состояния связи");
            MCell bls = m_container.CellCreate(cellMode, "BaseLinkState", "Базовая ячейка состояния связи");
            MCell n = m_container.CellCreate(cellMode, "Nothing", "Специальная ячейка Nothing. Представляет идентификатор для использования в элементах, где нельзя назначить идентификатор, но он необходим");

            //вот вроде бы простая работа - а нет, сложная. 
            //Теперь надо создать связи ячеек. Но для них нужны типы связей.
            //А чтобы создать типы связей, нужно создать ячейки типов связей.
            //А не эти болванки-заготовки, что я выше наклепал.
            //А для этих ячеек тоже нужны связи, и для них тоже нужны ячейки типов.
            //Вот какая кручень получается. Итеративная ручная работа.
            //Хорошо еще, что эти типы не требуются сразу при создании, их можно позже дописывать.

            //методика получается такая: сначала описываем свойства ячеек и связей, которые надо вписать в уже созданные ячейки и связи.
            //Затем создаем требуемые для этого ячейки.
            //Затем описываем требуемые связи между этими новыми ячейками, типы ячеек, связи итд.
            //Затем создаем требуемые для этого ячейки.
            //... повторяем, пока не окажется, что создавать ячейки более не нужно.
            //Теперь все созданные ячейки выстраиваем в один ряд и вписываем в их свойства идентификаторы соответствующих ячеек.
            //А потом создаем связи между ячейками, описывая их в комментариях. 
            //Эти связи сейчас древовидные, поэтому группируем их по корню дерева.

            //1.2) типы ячеек - тут я уже увяз в классах и их отношениях, тут надо много прорабатывать эту тему. Сейчас просто напишу что придумается.
            //тут хорошо, что типы представлены полями ячейки, а не связями. Иначе я бы тут завис надолго.
            //World имеет тип Сущность-НачальнаяСущность
            //System имеет тип Сущность-СлужебнаяСущность
            //Nothing имеет тип Сущность-СлужебнаяСущность
            //CellTypes имеет тип Коллекция Сущностей - Коллекция Однотипных Сущностей. Коллекция чего именно - это вроде бы тоже класс, но потом разберемся, как с ним быть.
            //CellDataTypes  имеет тип Коллекция Сущностей - Коллекция Однотипных Сущностей. 
            //CellStates  имеет тип Коллекция Сущностей - Коллекция Однотипных Сущностей.
            //LinkAxises  имеет тип Коллекция Сущностей - Коллекция Однотипных Сущностей. 
            //LinkStates  имеет тип Коллекция Сущностей - Коллекция Однотипных Сущностей.
            //BaseCellType имеет тип ЯчейкаТипаЯчейки
            //BaseCellDataType имеет тип ЯчейкаТипаДанныхЯчейки
            //CellDataTypeNoData  имеет тип ЯчейкаТипаДанныхЯчейки
            //BaseCellState имеет тип ЯчейкаСостоянияЯчейки
            //BaseLinkAxis имеет тип ЯчейкаТипаСвязи
            //BaseLinkState имеет тип ЯчейкаСостоянияСвязи

            //создаем ячейки типов ячеек
            MCell cellCellTypeEntity = m_container.CellCreate(cellMode, "CellTypeEntity", "Ячейка типа ячейки - Сущность");
            MCell cellCellTypeEntityInitial = m_container.CellCreate(cellMode, "CellTypeEntityInitial", "Ячейка типа ячейки - Начальная Сущность");
            MCell cellCellTypeEntityService = m_container.CellCreate(cellMode, "CellTypeEntityService", "Ячейка типа ячейки - Служебная Сущность");
            MCell cellCellTypeEntityCollection = m_container.CellCreate(cellMode, "CellTypeEntityCollection", "Ячейка типа ячейки - Коллекция Сущностей");
            MCell cellCellTypeEntityCollectionTyped = m_container.CellCreate(cellMode, "CellTypeEntityCollectionTyped", "Ячейка типа ячейки - Коллекция однотипных Сущностей");
            MCell cellCellTypeEntityCollectionCellTypeCell = m_container.CellCreate(cellMode, "CellTypeEntityCollectionCellTypeCell", "Ячейка типа ячейки - Коллекция ЯчейкаТипаЯчейки");
            MCell cellCellTypeEntityCollectionCellDataTypeCell = m_container.CellCreate(cellMode, "CellTypeEntityCollectionCellDataTypeCell", "Ячейка типа ячейки - Коллекция ЯчейкаТипаДанныхЯчейки");
            MCell cellCellTypeEntityCollectionCellStateCell = m_container.CellCreate(cellMode, "CellTypeEntityCollectionCellStateCell", "Ячейка типа ячейки - Коллекция ЯчейкаСостоянияЯчейки");
            MCell cellCellTypeEntityCollectionLinkTypeCell = m_container.CellCreate(cellMode, "CellTypeEntityCollectionLinkTypeCell", "Ячейка типа ячейки - Коллекция ЯчейкаТипаСвязи");
            MCell cellCellTypeEntityCollectionLinkStateCell = m_container.CellCreate(cellMode, "CellTypeEntityCollectionLinkStateCell", "Ячейка типа ячейки - Коллекция ЯчейкаСостоянияСвязи");
            MCell cellCellType_CellTypeCell = m_container.CellCreate(cellMode, "CellType_CellTypeCell", "Ячейка типа ячейки - ЯчейкаТипаЯчейки");//сам себя описывает
            MCell cellCellType_CellDataTypeCell = m_container.CellCreate(cellMode, "CellType_CellDataTypeCell", "Ячейка типа ячейки - ЯчейкаТипаДанныхЯчейки");
            MCell cellCellType_CellStateCell = m_container.CellCreate(cellMode, "CellType_CellStateCell", "Ячейка типа ячейки - ЯчейкаСостоянияЯчейки");
            MCell cellCellType_LinkTypeCell = m_container.CellCreate(cellMode, "CellType_LinkTypeCell", "Ячейка типа ячейки - ЯчейкаТипаСвязи");
            MCell cellCellType_LinkStateCell = m_container.CellCreate(cellMode, "CellType_LinkStateCell", "Ячейка типа ячейки - ЯчейкаСостоянияСвязи");

            //1.3) тип данных ячеек
            //все ячейки здесь не хранят данные, поэтому только один тип данных - без данных.
            MCell cellCellDataTypeNodata = m_container.CellCreate(cellMode, "CellDataTypeNoData", "Ячейка типа данных ячейки - Ячейка не должна содержать данные");

            //1.4) состояние ячейки
            //все ячейки в нормальном состоянии, поэтому только одно состояние - нормальное.
            MCell cellCellStateNormal = m_container.CellCreate(cellMode, "CellStateNormal", "Ячейка состояния ячейки - Ячейка в обычном состоянии");


            //1.5) типы связей
            //объектов тут пока нет вроде бы
            //связанность ячеек
            //1.5.1) Агрегация: Входит в состав или Является частью 
            //System входит в состав World
            //Nothing входит в состав System
            //CellTypes входит в состав System 
            //CellDataTypes входит в состав System 
            //CellStates входит в состав System 
            //LinkAxises входит в состав System 
            //LinkStates входит в состав System
            //BaseCellType входит в состав CellTypes
            //BaseCellDataType входит в состав CellDataTypes
            //BaseCellState входит в состав CellStates
            //BaseLinkAxis входит в состав LinkAxises
            //BaseLinkState входит в состав LinkStates
            //1.5.2) Абстракция: Является подклассом или Является надклассом

            //агрегация - Входит в состав или Является частью
            MCell cellLinkType_Aggregation = m_container.CellCreate(cellMode, "LinkType_Aggregation", "Ячейка типа связи - Агрегация: Входит в состав или Является частью");
            //абстракция - Является подклассом или Является надклассом
            MCell cellLinkType_Abstraction = m_container.CellCreate(cellMode, "LinkType_Abstraction", "Ячейка типа связи - Абстракция: Является подклассом или Является надклассом ");

            //1.6) состояние связей
            ////все связи в нормальном состоянии, поэтому только одно состояние - нормальное.
            MCell cellLinkStateNormal = m_container.CellCreate(cellMode, "LinkStateNormal", "Ячейка состояния связи - Связь в обычном состоянии");
            #endregion
            #region *** Заполнение свойств ячеек ***
            ////1.7) создаем общий список созданных ячеек
            ////базовая структура ячеек
            //world
            //sys
            //ct
            //bct
            //cdt
            //bcdt
            //cs 
            //bcs
            //la 
            //bla
            //ls
            //bls
            //n 
            ////ячейки типов ячеек
            //cellCellTypeEntity 
            //cellCellTypeEntityInitial 
            //cellCellTypeEntityService 
            //cellCellTypeEntityCollection 
            //cellCellTypeEntityCollectionTyped 
            //cellCellTypeEntityCollectionCellTypeCell 
            //cellCellTypeEntityCollectionCellDataTypeCell 
            //cellCellTypeEntityCollectionCellStateCell 
            //cellCellTypeEntityCollectionLinkTypeCell 
            //cellCellTypeEntityCollectionLinkStateCell 
            //cellCellType_CellTypeCell 
            //cellCellType_CellDataTypeCell 
            //cellCellType_CellStateCell 
            //cellCellType_LinkTypeCell 
            //cellCellType_LinkStateCell 
            ////ячейки типов данных ячеек
            //cellCellDataTypeNodata 
            ////ячейки состояния ячеек
            //cellCellStateNormal 
            ////ячейки типов связи
            //cellLinkType_Aggregation 
            //cellLinkType_Abstraction 
            ////ячейки состояния связи
            //cellLinkStateNormal 

            //1.8) пропишем типы данных всех ячеек здесь
            //вписываем копии идентификаторов, чтобы изменение поля в одном из них не повлияло на все использующие его ячейки
            //базовая структура ячеек
            world.ValueTypeId = cellCellDataTypeNodata.CellID;
            sys.ValueTypeId = cellCellDataTypeNodata.CellID;
            ct.ValueTypeId = cellCellDataTypeNodata.CellID;
            bct.ValueTypeId = cellCellDataTypeNodata.CellID;
            cdt.ValueTypeId = cellCellDataTypeNodata.CellID;
            bcdt.ValueTypeId = cellCellDataTypeNodata.CellID;
            cs.ValueTypeId = cellCellDataTypeNodata.CellID;
            bcs.ValueTypeId = cellCellDataTypeNodata.CellID;
            la.ValueTypeId = cellCellDataTypeNodata.CellID;
            bla.ValueTypeId = cellCellDataTypeNodata.CellID;
            ls.ValueTypeId = cellCellDataTypeNodata.CellID;
            bls.ValueTypeId = cellCellDataTypeNodata.CellID;
            n.ValueTypeId = cellCellDataTypeNodata.CellID;
            //ячейки типов ячеек
            cellCellTypeEntity.ValueTypeId = cellCellDataTypeNodata.CellID;
            cellCellTypeEntityInitial.ValueTypeId = cellCellDataTypeNodata.CellID;
            cellCellTypeEntityService.ValueTypeId = cellCellDataTypeNodata.CellID;
            cellCellTypeEntityCollection.ValueTypeId = cellCellDataTypeNodata.CellID;
            cellCellTypeEntityCollectionTyped.ValueTypeId = cellCellDataTypeNodata.CellID;
            cellCellTypeEntityCollectionCellTypeCell.ValueTypeId = cellCellDataTypeNodata.CellID;
            cellCellTypeEntityCollectionCellDataTypeCell.ValueTypeId = cellCellDataTypeNodata.CellID;
            cellCellTypeEntityCollectionCellStateCell.ValueTypeId = cellCellDataTypeNodata.CellID;
            cellCellTypeEntityCollectionLinkTypeCell.ValueTypeId = cellCellDataTypeNodata.CellID;
            cellCellTypeEntityCollectionLinkStateCell.ValueTypeId = cellCellDataTypeNodata.CellID;
            cellCellType_CellTypeCell.ValueTypeId = cellCellDataTypeNodata.CellID;
            cellCellType_CellDataTypeCell.ValueTypeId = cellCellDataTypeNodata.CellID;
            cellCellType_CellStateCell.ValueTypeId = cellCellDataTypeNodata.CellID;
            cellCellType_LinkTypeCell.ValueTypeId = cellCellDataTypeNodata.CellID;
            cellCellType_LinkStateCell.ValueTypeId = cellCellDataTypeNodata.CellID;
            //ячейки типов данных ячеек
            cellCellDataTypeNodata.ValueTypeId = cellCellDataTypeNodata.CellID;
            //ячейки состояния ячеек
            cellCellStateNormal.ValueTypeId = cellCellDataTypeNodata.CellID;
            //ячейки типов связи
            cellLinkType_Aggregation.ValueTypeId = cellCellDataTypeNodata.CellID;
            cellLinkType_Abstraction.ValueTypeId = cellCellDataTypeNodata.CellID;
            //ячейки состояния связи
            cellLinkStateNormal.ValueTypeId = cellCellDataTypeNodata.CellID;

            //1.9) пропишем типы ячеек здесь - они разные!
            //базовая структура ячеек
            world.TypeId = cellCellTypeEntityInitial.CellID; //World имеет тип Сущность-НачальнаяСущность
            sys.TypeId = cellCellTypeEntityService.CellID;//System имеет тип Сущность-СлужебнаяСущность
            ct.TypeId = cellCellTypeEntityCollectionCellTypeCell.CellID;//CellTypes имеет тип Коллекция Сущностей - Коллекция Однотипных Сущностей.
            bct.TypeId = cellCellType_CellTypeCell.CellID;//BaseCellType имеет тип ЯчейкаТипаЯчейки
            cdt.TypeId = cellCellTypeEntityCollectionCellDataTypeCell.CellID;//CellDataTypes  имеет тип Коллекция Сущностей - Коллекция Однотипных Сущностей.
            bcdt.TypeId = cellCellType_CellDataTypeCell.CellID;//BaseCellDataType имеет тип ЯчейкаТипаДанныхЯчейки
            cs.TypeId = cellCellTypeEntityCollectionCellStateCell.CellID; //CellStates  имеет тип Коллекция Сущностей - Коллекция Однотипных Сущностей.
            bcs.TypeId = cellCellType_CellStateCell.CellID;//BaseCellState имеет тип ЯчейкаСостоянияЯчейки
            la.TypeId = cellCellTypeEntityCollectionLinkTypeCell.CellID; //LinkAxises  имеет тип Коллекция Сущностей - Коллекция Однотипных Сущностей.
            bla.TypeId = cellCellType_LinkTypeCell.CellID;//BaseLinkAxis имеет тип ЯчейкаТипаСвязи
            ls.TypeId = cellCellTypeEntityCollectionLinkStateCell.CellID;//LinkStates  имеет тип Коллекция Сущностей - Коллекция Однотипных Сущностей.
            bls.TypeId = cellCellType_LinkStateCell.CellID;//BaseLinkState имеет тип ЯчейкаСостоянияСвязи
            n.TypeId = cellCellTypeEntityService.CellID;//Nothing имеет тип Сущность-СлужебнаяСущность
            //ячейки типов ячеек - имеют тип ЯчейкаТипаЯчейки
            cellCellTypeEntity.TypeId = cellCellType_CellTypeCell.CellID;
            cellCellTypeEntityInitial.TypeId = cellCellType_CellTypeCell.CellID;
            cellCellTypeEntityService.TypeId = cellCellType_CellTypeCell.CellID;
            cellCellTypeEntityCollection.TypeId = cellCellType_CellTypeCell.CellID;
            cellCellTypeEntityCollectionTyped.TypeId = cellCellType_CellTypeCell.CellID;
            cellCellTypeEntityCollectionCellTypeCell.TypeId = cellCellType_CellTypeCell.CellID;
            cellCellTypeEntityCollectionCellDataTypeCell.TypeId = cellCellType_CellTypeCell.CellID;
            cellCellTypeEntityCollectionCellStateCell.TypeId = cellCellType_CellTypeCell.CellID;
            cellCellTypeEntityCollectionLinkTypeCell.TypeId = cellCellType_CellTypeCell.CellID;
            cellCellTypeEntityCollectionLinkStateCell.TypeId = cellCellType_CellTypeCell.CellID;
            cellCellType_CellTypeCell.TypeId = cellCellType_CellTypeCell.CellID;
            cellCellType_CellDataTypeCell.TypeId = cellCellType_CellTypeCell.CellID;
            cellCellType_CellStateCell.TypeId = cellCellType_CellTypeCell.CellID;
            cellCellType_LinkTypeCell.TypeId = cellCellType_CellTypeCell.CellID;
            cellCellType_LinkStateCell.TypeId = cellCellType_CellTypeCell.CellID;
            //ячейки типов данных ячеек - имеет тип ЯчейкаТипаДанныхЯчейки
            cellCellDataTypeNodata.TypeId = cellCellType_CellDataTypeCell.CellID;
            //ячейки состояния ячеек - имеет тип ЯчейкаСостоянияЯчейки
            cellCellStateNormal.TypeId = cellCellType_CellStateCell.CellID;
            //ячейки типов связи - имеет тип ЯчейкаТипаСвязи
            cellLinkType_Aggregation.TypeId = cellCellType_LinkTypeCell.CellID;
            cellLinkType_Abstraction.TypeId = cellCellType_LinkTypeCell.CellID;
            //ячейки состояния связи - имеет тип ЯчейкаСостоянияСвязи
            cellLinkStateNormal.TypeId = cellCellType_LinkStateCell.CellID;

            //1.10) пропишем состояние ячеек здесь - оно у всех одинаковое = cellCellStateNormal.CellID;
            //базовая структура ячеек
            world.State = cellCellStateNormal.CellID;
            sys.State = cellCellStateNormal.CellID;
            ct.State = cellCellStateNormal.CellID;
            bct.State = cellCellStateNormal.CellID;
            cdt.State = cellCellStateNormal.CellID;
            bcdt.State = cellCellStateNormal.CellID;
            cs.State = cellCellStateNormal.CellID;
            bcs.State = cellCellStateNormal.CellID;
            la.State = cellCellStateNormal.CellID;
            bla.State = cellCellStateNormal.CellID;
            ls.State = cellCellStateNormal.CellID;
            bls.State = cellCellStateNormal.CellID;
            n.State = cellCellStateNormal.CellID;
            //ячейки типов ячеек
            cellCellTypeEntity.State = cellCellStateNormal.CellID;
            cellCellTypeEntityInitial.State = cellCellStateNormal.CellID;
            cellCellTypeEntityService.State = cellCellStateNormal.CellID;
            cellCellTypeEntityCollection.State = cellCellStateNormal.CellID;
            cellCellTypeEntityCollectionTyped.State = cellCellStateNormal.CellID;
            cellCellTypeEntityCollectionCellTypeCell.State = cellCellStateNormal.CellID;
            cellCellTypeEntityCollectionCellDataTypeCell.State = cellCellStateNormal.CellID;
            cellCellTypeEntityCollectionCellStateCell.State = cellCellStateNormal.CellID;
            cellCellTypeEntityCollectionLinkTypeCell.State = cellCellStateNormal.CellID;
            cellCellTypeEntityCollectionLinkStateCell.State = cellCellStateNormal.CellID;
            cellCellType_CellTypeCell.State = cellCellStateNormal.CellID;
            cellCellType_CellDataTypeCell.State = cellCellStateNormal.CellID;
            cellCellType_CellStateCell.State = cellCellStateNormal.CellID;
            cellCellType_LinkTypeCell.State = cellCellStateNormal.CellID;
            cellCellType_LinkStateCell.State = cellCellStateNormal.CellID;
            //ячейки типов данных ячеек
            cellCellDataTypeNodata.State = cellCellStateNormal.CellID;
            //ячейки состояния ячеек
            cellCellStateNormal.State = cellCellStateNormal.CellID;
            //ячейки типов связи
            cellLinkType_Aggregation.State = cellCellStateNormal.CellID;
            cellLinkType_Abstraction.State = cellCellStateNormal.CellID;
            //ячейки состояния связи
            cellLinkStateNormal.State = cellCellStateNormal.CellID;
            #endregion
            #region *** Создание связей ***

            //2) теперь надо создать связи между ячейками.
            //2.1) главное дерево World
            MLink link1 = world.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, sys);
            MLink link2 = sys.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, n);
            MLink link3 = sys.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, ct);
            MLink link4 = sys.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cdt);
            MLink link5 = sys.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cs);
            MLink link6 = sys.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, la);
            MLink link7 = sys.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, ls);

            //2.2) дерево коллекции типов ячеек
            //- собираем коллекцию всех классов в один ряд
            MLink link8 = ct.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, bct);
            MLink link9 = ct.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellCellTypeEntity);
            MLink link10 = ct.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellCellTypeEntityInitial);
            MLink link11 = ct.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellCellTypeEntityService);
            MLink link12 = ct.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellCellTypeEntityCollection);
            MLink link13 = ct.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellCellTypeEntityCollectionTyped);
            MLink link14 = ct.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellCellTypeEntityCollectionCellTypeCell);
            MLink link15 = ct.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellCellTypeEntityCollectionCellDataTypeCell);
            MLink link16 = ct.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellCellTypeEntityCollectionCellStateCell);
            MLink link17 = ct.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellCellTypeEntityCollectionLinkTypeCell);
            MLink link18 = ct.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellCellTypeEntityCollectionLinkStateCell);
            MLink link19 = ct.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellCellType_CellTypeCell);
            MLink link20 = ct.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellCellType_CellDataTypeCell);
            MLink link21 = ct.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellCellType_CellStateCell);
            MLink link22 = ct.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellCellType_LinkTypeCell);
            MLink link23 = ct.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellCellType_LinkStateCell);
            // - строим дерево классов если возможно. Главным классом будет базовая ячейка, хотя она ничего не делает 
            //-- Сущность - Начальная сущность и Служебная сущность
            MLink link24 = bct.S1_createLink(cellLinkType_Abstraction.CellID, MAxisDirection.Down, cellCellTypeEntity);
            MLink link25 = cellCellTypeEntity.S1_createLink(cellLinkType_Abstraction.CellID, MAxisDirection.Down, cellCellTypeEntityInitial);
            MLink link26 = cellCellTypeEntity.S1_createLink(cellLinkType_Abstraction.CellID, MAxisDirection.Down, cellCellTypeEntityService);
            //-- Коллекция сущностей - однотипная коллекция сущностей - коллекции ячеек типов итд
            MLink link27 = bct.S1_createLink(cellLinkType_Abstraction.CellID, MAxisDirection.Down, cellCellTypeEntityCollection);
            MLink link28 = cellCellTypeEntityCollection.S1_createLink(cellLinkType_Abstraction.CellID, MAxisDirection.Down, cellCellTypeEntityCollectionTyped);
            MLink link29 = cellCellTypeEntityCollectionTyped.S1_createLink(cellLinkType_Abstraction.CellID, MAxisDirection.Down, cellCellTypeEntityCollectionCellTypeCell);
            MLink link30 = cellCellTypeEntityCollectionTyped.S1_createLink(cellLinkType_Abstraction.CellID, MAxisDirection.Down, cellCellTypeEntityCollectionCellDataTypeCell);
            MLink link31 = cellCellTypeEntityCollectionTyped.S1_createLink(cellLinkType_Abstraction.CellID, MAxisDirection.Down, cellCellTypeEntityCollectionCellStateCell);
            MLink link32 = cellCellTypeEntityCollectionTyped.S1_createLink(cellLinkType_Abstraction.CellID, MAxisDirection.Down, cellCellTypeEntityCollectionLinkTypeCell);
            MLink link33 = cellCellTypeEntityCollectionTyped.S1_createLink(cellLinkType_Abstraction.CellID, MAxisDirection.Down, cellCellTypeEntityCollectionLinkStateCell);
            //TODO: тут я забыл про иерархию классов типов ячеек? Для коллекций написал, а для простых типов - нет.
            //они должны же быть соединены с ячейкой bct все. Или нет? Я тут запутался уже в этих отношениях ячеек.

            //2.3) дерево коллекции типов данных ячеек
            //-собираем коллекцию всех классов в один ряд
            MLink link34 = cdt.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, bcdt);
            MLink link35 = cdt.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellCellDataTypeNodata);
            //-строим дерево классов если возможно. Главным классом будет базовая ячейка, хотя она ничего не делает 
            MLink link36 = bcdt.S1_createLink(cellLinkType_Abstraction.CellID, MAxisDirection.Down, cellCellDataTypeNodata);


            //2.4) дерево коллекции состояний ячейки
            //-собираем коллекцию всех классов в один ряд
            MLink link37 = cs.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, bcs);
            MLink link38 = cs.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellCellStateNormal);
            //-строим дерево классов если возможно. Главным классом будет базовая ячейка, хотя она ничего не делает 
            MLink link39 = bcs.S1_createLink(cellLinkType_Abstraction.CellID, MAxisDirection.Down, cellCellStateNormal);

            //2.5) дерево коллекции типов связи
            //-собираем коллекцию всех классов в один ряд
            MLink link40 = la.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, bla);
            MLink link41 = la.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellLinkType_Aggregation);
            MLink link42 = la.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellLinkType_Abstraction);
            //-строим дерево классов если возможно. Главным классом будет базовая ячейка, хотя она ничего не делает 
            MLink link43 = bla.S1_createLink(cellLinkType_Abstraction.CellID, MAxisDirection.Down, cellLinkType_Aggregation);
            MLink link44 = bla.S1_createLink(cellLinkType_Abstraction.CellID, MAxisDirection.Down, cellLinkType_Abstraction);


            //2.6) дерево коллекции состояний связи
            //-собираем коллекцию всех классов в один ряд
            MLink link45 = ls.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, bls);
            MLink link46 = ls.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellLinkStateNormal);
            //-строим дерево классов если возможно. Главным классом будет базовая ячейка, хотя она ничего не делает 
            MLink link47 = bls.S1_createLink(cellLinkType_Abstraction.CellID, MAxisDirection.Down, cellLinkStateNormal);

            #endregion
            #region *** Заполнение свойств связей ***
            link1.State = cellLinkStateNormal.CellID;
            link2.State = cellLinkStateNormal.CellID;
            link3.State = cellLinkStateNormal.CellID;
            link4.State = cellLinkStateNormal.CellID;
            link5.State = cellLinkStateNormal.CellID;
            link6.State = cellLinkStateNormal.CellID;
            link7.State = cellLinkStateNormal.CellID;
            link8.State = cellLinkStateNormal.CellID;
            link9.State = cellLinkStateNormal.CellID;
            link10.State = cellLinkStateNormal.CellID;
            link11.State = cellLinkStateNormal.CellID;
            link12.State = cellLinkStateNormal.CellID;
            link13.State = cellLinkStateNormal.CellID;
            link14.State = cellLinkStateNormal.CellID;
            link15.State = cellLinkStateNormal.CellID;
            link16.State = cellLinkStateNormal.CellID;
            link17.State = cellLinkStateNormal.CellID;
            link18.State = cellLinkStateNormal.CellID;
            link19.State = cellLinkStateNormal.CellID;
            link20.State = cellLinkStateNormal.CellID;
            link21.State = cellLinkStateNormal.CellID;
            link22.State = cellLinkStateNormal.CellID;
            link23.State = cellLinkStateNormal.CellID;
            link24.State = cellLinkStateNormal.CellID;
            link25.State = cellLinkStateNormal.CellID;
            link26.State = cellLinkStateNormal.CellID;
            link27.State = cellLinkStateNormal.CellID;
            link28.State = cellLinkStateNormal.CellID;
            link29.State = cellLinkStateNormal.CellID;
            link30.State = cellLinkStateNormal.CellID;
            link31.State = cellLinkStateNormal.CellID;
            link32.State = cellLinkStateNormal.CellID;
            link33.State = cellLinkStateNormal.CellID;
            link34.State = cellLinkStateNormal.CellID;
            link35.State = cellLinkStateNormal.CellID;
            link36.State = cellLinkStateNormal.CellID;
            link37.State = cellLinkStateNormal.CellID;
            link38.State = cellLinkStateNormal.CellID;
            link39.State = cellLinkStateNormal.CellID;
            link40.State = cellLinkStateNormal.CellID;
            link41.State = cellLinkStateNormal.CellID;
            link42.State = cellLinkStateNormal.CellID;
            link43.State = cellLinkStateNormal.CellID;
            link44.State = cellLinkStateNormal.CellID;
            link45.State = cellLinkStateNormal.CellID;
            link46.State = cellLinkStateNormal.CellID;
            link47.State = cellLinkStateNormal.CellID;

            #endregion

#region *** Создание пользовательского каталога ***
            //ячейки каталога пользователей
            //КоллекцияАккаунтовПользователей-АккаунтПользователя
            MCell cellUserAccountCollection = m_container.CellCreate(cellMode, "CellTypeEntity", "Ячейка КоллекцияАккаунтовПользователей");
            MCell cellUserAccountRoot = m_container.CellCreate(cellMode, "CellTypeEntity", "Ячейка АккаунтПользователя");
            //ячейки типов ячеек
            MCell cellCellTypeUserAccountRootCell = m_container.CellCreate(cellMode, "CellTypeUserAccountRootCell", "Ячейка типа ячейки - АккаунтПользователя");
            MCell cellCellTypeEntityCollectionUserAccountRootCell = m_container.CellCreate(cellMode, "CellTypeEntityCollectionUserAccountRootCell", "Ячейка типа ячейки - КоллекцияАккаунтовПользователей");
            //свойства ячеек
            cellUserAccountCollection.State = cellCellStateNormal.CellID;
            cellUserAccountRoot.State = cellCellStateNormal.CellID;
            cellCellTypeUserAccountRootCell.State = cellCellStateNormal.CellID;
            cellCellTypeEntityCollectionUserAccountRootCell.State = cellCellStateNormal.CellID;
            //
            cellUserAccountCollection.TypeId = cellCellTypeEntityCollectionUserAccountRootCell.CellID;
            cellUserAccountRoot.TypeId = cellCellTypeUserAccountRootCell.CellID; 
            cellCellTypeUserAccountRootCell.TypeId = cellCellType_CellTypeCell.CellID;
            cellCellTypeEntityCollectionUserAccountRootCell.TypeId = cellCellType_CellTypeCell.CellID;
            //
            cellUserAccountCollection.ValueTypeId = cellCellDataTypeNodata.CellID;
            cellUserAccountRoot.ValueTypeId = cellCellDataTypeNodata.CellID;
            cellCellTypeUserAccountRootCell.ValueTypeId = cellCellDataTypeNodata.CellID;
            cellCellTypeEntityCollectionUserAccountRootCell.ValueTypeId = cellCellDataTypeNodata.CellID;
            //создание связей
            //основные связи
            MLink link100 = world.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellUserAccountCollection);
            MLink link101 = cellUserAccountCollection.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellUserAccountRoot);
            //связи ячеек типов ячеек с их коллекциями
            MLink link102 = ct.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellCellTypeUserAccountRootCell);
            MLink link103 = ct.S1_createLink(cellLinkType_Aggregation.CellID, MAxisDirection.Down, cellCellTypeEntityCollectionUserAccountRootCell);
            //и в иерархии типов
            MLink link104 = cellCellTypeEntityCollectionTyped.S1_createLink(cellLinkType_Abstraction.CellID, MAxisDirection.Down, cellCellTypeEntityCollectionUserAccountRootCell);
            MLink link105 = bct.S1_createLink(cellLinkType_Abstraction.CellID, MAxisDirection.Down, cellCellTypeUserAccountRootCell);
            //установить свойства связей
            link100.State = cellLinkStateNormal.CellID;
            link101.State = cellLinkStateNormal.CellID;
            link102.State = cellLinkStateNormal.CellID;
            link103.State = cellLinkStateNormal.CellID;
            link104.State = cellLinkStateNormal.CellID;
            link105.State = cellLinkStateNormal.CellID;


            
 #endregion

            //У этого проекта должна быть иконка паутины - я уже запутался в описании ячеек и связей.
            //Как можно быстро и просто строить тут структуры данных, 
            //если добавление первых 13 сущностей потребовало 33 ячейки и более 50 связей.
            //А добавление еще двух сущностей здесь - пользовательский аккаунт - 4 ячеек и 6 связей.
            //Это просто какой-то кошмар.
            //Что тут можно сделать?
            //- эти ячейки и связи описывают базовую инфраструктуру солюшена - и реализацию и структуру типов. 
            //Поэтому их так много получилось сейчас.
            //Если в будущем и дальше будет много новых типов, то и ячеек и связей будет много.
            //Это будет сложная работа, хотя ее можно автоматизировать - написать функции создания классов:
            //сразу и создавать ячейки типов ячеек и помещать их в коллекцию, и связывать с иерархией ячеек типов ячеек.
            //А если работа будет в основном с объектами, то сначала немного новых классов приплести, а потом просто объекты к ним прицеплять, даже без связей.

            //Но все равно, я вижу, что тут нужна теория и навыки построения таких структур.
            //Ее разработка займет время, конечно, но нужен опыт использования.

            //Выводы:
            //- нужен опыт использования, нужно создать методику создания и строения структуры сущностей, типовые решения.
            //- нужно способ автоматизировать операции создания структуры сущностей. Хотя они зависят от строения Структуры сущностей.
            //   Это уже конфигурация получается, как в 1С.
            //- Концентрировать это устройство системы на работу с объектами. Так меньше ячеек надо создавать иможно использовать уже существующие.
            //- Развивать Структуру сущностей - чем больше в ней применимых классов, тем меньше их придется создавать каждый раз.


        }


        /// <summary>
        /// Menu Snapshot-Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createSnapshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //check project opened
            if (m_container == null)
            {
                this.toolStripStatusLabel1.Text = "No opened projects";
                playError();
                return;
            }
            try
            {
                this.toolStripStatusLabel1.Text = "Creating snapshot...";
                Application.DoEvents();
                m_container.SnapshotFullCreate();

                this.toolStripStatusLabel1.Text = "Snapshot created";
                playBeep();
            }
            catch (Exception ex)
            {
                ShowExceptionMessageBox(ex);
            }
            return;



        }

        /// <summary>
        /// Menu Snapshot-Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadSnapshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //check project opened
            if (m_container == null)
            {
                this.toolStripStatusLabel1.Text = "No opened projects";
                playError();
                return;
            }
            try
            {
                //Show dialog to open snapshot file
                OpenFileDialog ofd = new OpenFileDialog();
                //ofd.Multiselect = false; by default
                ofd.Title = "Select snapshot file";
                ofd.InitialDirectory = m_container.SolutionManager.SnapshotFolderPath;
                ofd.DereferenceLinks = true; //use shortcuts for opening
                ofd.Filter = "Tapp snapshot file(.step)|*.step|All files|*.*";
                ofd.FilterIndex = 1;
                if (ofd.ShowDialog(this) != DialogResult.OK) return;

                this.toolStripStatusLabel1.Text = String.Format("Loading snapshot {0}", ofd.FileName);
                Application.DoEvents();
                m_container.SnapshotFullLoad(ofd.FileName);

                this.toolStripStatusLabel1.Text = String.Format("Snapshot {0} loaded", ofd.FileName);
                playBeep();
            }
            catch (Exception ex)
            {
                ShowExceptionMessageBox(ex);
            }
            return;
        }

        private void formsTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NavigatorForm.LaunchNavigator(m_container);
        }



    }
}
