using System;
using System.Collections.Generic;
using System.Text;
using Bar;
using Bar.DatabaseAdapters;
using System.IO;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //TestCreateDbAdapter();
                //TestAbsolutePathFunctions();
                
                //TestSolutionRefCollection();

                //TestSolutionSettings();
                
                //Test_SolutionInputGroup.Test();
                
                //TestDbAttributeSearch();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
            return;
        }

        private static void TestCreateDbAdapter()
        {
            MSolution s = new MSolution();
            BaseDbAdapter db = BaseDbAdapter.GetAdapter(MDatabaseType.MsJet40, s);
            Console.WriteLine(db.ToString());
        }

        private static void TestAbsolutePathFunctions()
        {
            //тест был успешен 24 декабря 2018г
            
            String[] basedirs = new String[] {"C:\\Temp", "C:\\Temp\\", "Local", "\\Local\\" };
            
            String[] argumentDirs = new String[] {"C:\\Absolute", "C:\\Absolute\\", "C:\\Temp", "C:\\Temp\\", "Lokal", "Lokal\\", "\\Lokal\\", "" };

            Console.WriteLine("");
            Console.WriteLine("s3 = Bar.Utility.MUtility.makeRelativePath(s1, s2)");
            Console.WriteLine("");
            foreach (String s1 in basedirs)
            {

                foreach (String s2 in argumentDirs)
                {
                    String s3 = "";
                    try
                    {
                        s3 = Bar.Utility.MUtility.makeRelativePath(s1, s2);
                    }
                    catch (Exception ex)
                    {
                        s3 = ex.GetType().Name;
                    }
                    String result = String.Format("makeRelPath({0}, {1}) = {2}", s1, s2, s3);
                    Console.WriteLine(result);
                }
            }

            Console.WriteLine("");
            Console.WriteLine("s3 = Bar.Utility.MUtility.makeAbsolutePath(s1, s2)");
            Console.WriteLine("");
            foreach (String s1 in basedirs)
            {

                foreach (String s2 in argumentDirs)
                {
                    String s3 = "";
                    try
                    {
                        s3 = Bar.Utility.MUtility.makeAbsolutePath(s1, s2);
                    }
                    catch (Exception ex)
                    {
                        s3 = ex.GetType().Name;
                    }
                    String result = String.Format("makeAbsPath({0}, {1}) = {2}", s1, s2, s3);
                    Console.WriteLine(result);
                }
            }

            return;
        }

        private static void TestSolutionRefCollection()
        {
            String file1 = "C:\\Temp\\sol1.txt";
            String file2 = "C:\\Temp\\sol2.txt";
            String file3 = "C:\\Temp\\sol3.txt";

            //1 create 3 new solution
            MSolutionSettings s1 = new MSolutionSettings(file1);
            s1.SolutionId = 1;
            s1.Save();
            MSolutionSettings s2 = new MSolutionSettings(file2);
            s2.SolutionId = 2;
            s2.Save();
            MSolutionSettings s3 = new MSolutionSettings(file3);
            s3.SolutionId = 3;
            s3.Save();
            s1 = null; s2 = null; s3 = null;
            //2 open 3 solution
            MSolution d1 = new MSolution();
            d1.SolutionOpen(file1);
            MSolution d2 = new MSolution();
            d2.SolutionOpen(file2);
            MSolution d3 = new MSolution();
            d3.SolutionOpen(file3);
            //3 check solution list and access
            MSolution da1 = MSolutionRefCollection.GetSolutionReference(1);
            MSolution da2 = MSolutionRefCollection.GetSolutionReference(2);
            MSolution da3 = MSolutionRefCollection.GetSolutionReference(3);
            //4 exit
            d1.SolutionClose(true);
            d2.SolutionClose(true);
            d3.SolutionClose(true);
            
            return;
        }

        private static void TestSolutionSettings()
        {
            String solutionFilePath = "C:\\Temp\\solutionTestFile.txt";

            //delete old file if exists 
            File.Delete(solutionFilePath);

            Console.WriteLine("Test Solution settings");
            //1 create new solution file
            MSolutionSettings s1 = new MSolutionSettings(solutionFilePath);
            //s1.SolutionFilePath = solutionFilePath;

            //2 save solution file
            s1.Save();
            
            //3 load solution file into new object
            MSolutionSettings s2 = MSolutionSettings.Load(solutionFilePath);
            
            //4 compare two files - manually
            if (s1.SolutionId != s2.SolutionId) Console.WriteLine("Solution test failed!");

            Console.WriteLine("Test Solution settings finished");
            
            return;
        }

        private static void TestDbAttributeSearch()
        {
            //test first function
            
            Console.WriteLine("Test Db attribute search");
            
            List<Bar.DatabaseAdapters.MDatabaseType> result =  BaseDbAdapter.GetAvailableDatabaseTypes();
            //print types
            foreach (Bar.DatabaseAdapters.MDatabaseType t in result)
                Console.WriteLine(t.ToString());

            Console.WriteLine("Test Db attribute search finished");

            //test second function
            Console.WriteLine("Test Db type is supported");
            bool result2 = BaseDbAdapter.IsSupportedDbType(Bar.DatabaseAdapters.MDatabaseType.NoDatabase);
            Console.WriteLine("MDatabaseType.NoDatabase is supported: " + result2.ToString());
            Console.WriteLine("Test Db type is supported finished");

            return;
        }




    }
}
/// <summary>
/// NT-Тест функций входной группы Солюшена
/// </summary>
public class Test_SolutionInputGroup
{
    public static void Test()
    {
        СоздатьТестовыйСолюшен("C:\\Temp");
        ОткрытьСолюшен("C:\\Temp\\ФайлСолюшена.ext");
        TestSolutionOperations("C:\\Temp\\ФайлСолюшена.ext");
    }

    private static void СоздатьТестовыйСолюшен(string rootfolder)
    {
        //создаем объект движка. Тут просто инициализация движка без солюшена.
        MSolution d = new MSolution();
        //заполняем свойства нового солюшена
        //здесь закомментированы поля, использующие значения по умолчанию
        MSolutionSettings info = new MSolutionSettings();
        info.DatabaseType = MDatabaseType.NoDatabase;
        info.DatabaseName = String.Empty;

        info.SolutionName = "TestSolution";
        info.SolutionDescription = "Test project in 2017";
        //info.SolutionId = 1;//by default
        //info.SolutionVersion = new MVersion("1.0.0.0");//by default

        info.ContainerDefaultCellMode = Bar.Container.MCellMode.Temporary;//для СолюшенБезБД другого не может быть 

        //создаем солюшен и инициализируем его данными движок.
        d.SolutionCreate(rootfolder, info);//должен быть создан, но не открыт

        //... some work here

        //d.SolutionClose(true); - солюшен не открыт - нечего и закрывать
        d = null;
        return;
    }

    private static void ОткрытьСолюшен(string solutionFilePath)
    {
        //создаем объект движка. Тут просто инициализация движка без солюшена.
        MSolution d = new MSolution();
        //открываем солюшен и инициализируем его данными движок.
        d.SolutionOpen(solutionFilePath);

        //...some work here

        d.SolutionClose(true);
        d = null;
        return;
    }

    private static void TestSolutionOperations(string solutionFilePath)
    {
        try
        {
            Console.WriteLine("Solution operations test started");
            //создаем объект движка.
            MSolution d = new MSolution();
            //открываем солюшен и инициализируем его данными движок.
            d.SolutionOpen(solutionFilePath);
            Console.WriteLine("Solution opened");

            MStatistics stat = d.SolutionGetStatistics();
            PrintSolutionStatistics(stat);
            Console.WriteLine("Optimizing solution..");
            d.SolutionOptimize();
            stat = d.SolutionGetStatistics();
            PrintSolutionStatistics(stat);
            Console.WriteLine("Saving solution..");
            d.SolutionSave();
            Console.WriteLine("Clearing solution..");
            d.SolutionClear();
            stat = d.SolutionGetStatistics();
            PrintSolutionStatistics(stat);
            Console.WriteLine("Closing solution..");
            d.SolutionClose(false);
            d = null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());

        }
        Console.WriteLine("Solution operations test finished");
        return;
    }

    public static void PrintSolutionStatistics(Bar.MStatistics info)
    {
        Console.WriteLine("");
        Console.WriteLine("Solution info:");
        Console.WriteLine(String.Format("Stat_CellsInMemory: {0}", info.Stat_CellsInMemory));
        Console.WriteLine(String.Format("Stat_ConstantCells: {0}", info.Stat_ConstantCells));
        Console.WriteLine(String.Format("Stat_TemporaryCells: {0}", info.Stat_TemporaryCells));
        Console.WriteLine(String.Format("Stat_ExternalCells: {0}", info.Stat_ExternalCells));
        Console.WriteLine(String.Format("Stat_LinksInMemory: {0}", info.Stat_LinksInMemory));
        Console.WriteLine(String.Format("Stat_ConstantLinks: {0}", info.Stat_ConstantLinks));
        Console.WriteLine(String.Format("Stat_TemporaryLinks: {0}", info.Stat_TemporaryLinks));
        Console.WriteLine(String.Format("Stat_ExternalLinks: {0}", info.Stat_ExternalLinks));
        Console.WriteLine(String.Format("Stat_ResourceFiles: {0}", info.Stat_ResourceFiles));
        Console.WriteLine(String.Format("Stat_ResourceSize: {0}", info.Stat_ResourceSize));
        Console.WriteLine("");

        return;
    }


}