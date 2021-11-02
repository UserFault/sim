using System;
using System.Collections.Generic;
using System.Reflection;
using Bar.DatabaseAdapters;
using System.IO;

namespace Bar.Utility
{
    /// <summary>
    /// NT-Статический класс вспомогательных инструментов
    /// </summary>
    public static class MUtility
    {
        /// <summary>
        /// Слеш пути файловой системы
        /// </summary>
        private const string FilePathLimiter = "\\";


        #region *** File system functions ***
        /// <summary>
        /// NT-Проверить что каталог доступен для записи
        /// </summary>
        /// <param name="folderPath">Путь к проверяемому каталогу</param>
        /// <returns>Возвращает true, если каталог доступен для записи, иначе false.</returns>
        /// <remarks>
        /// Для проверки возможности записи в каталог функция создает в нем файл "writetest.txt".
        /// Если при этом выбрасывается любое исключение, то функция перехватывает его и возвращает отрицательный результат. 
        /// </remarks>
        internal static bool isFolderWritable(string folderPath)
        {
            bool ro = false;
            //generate test file name
            String test = Path.Combine(folderPath, "writetest.txt");
            try
            {
                //if test file already exists, its writable folder
                if (File.Exists(test))
                    File.Delete(test);//тут тоже будет исключение, если каталог read-only
                //test creation of new file
                FileStream fs = File.Create(test);
                fs.Close();
            }
            catch (Exception)
            {
                ro = true;
            }
            finally
            {
                File.Delete(test);
            }
            return (!ro);
        }

        /// <summary>
        /// RT-Вернуть абсолютный путь к каталогу
        /// </summary>
        /// <param name="basedir">Абсолютный путь основного каталога</param>
        /// <param name="localPath">Относительный или абсолютный путь конечного каталога</param>
        /// <returns>Функция возвращает абсолютный путь к конечному каталогу</returns>
        public static string makeAbsolutePath(string basedir, string localPath)
        {
            //проверяем  аргументы
            if(String.IsNullOrEmpty(basedir))
                throw new ArgumentException("Неправильный путь каталога", "basedir");
            if (localPath == null)
                throw new ArgumentException("Неправильный путь каталога", "localPath");
            if (!IsAbsolutePath(basedir))
                throw new ArgumentException("Путь должен быть абсолютным", "basedir");
            //если первый символ \, то удалить его, иначе пути не склеятся этой функцией
            String locP = localPath.Trim();
            locP = RemoveStartSlash(locP);
            locP = RemoveEndSlash(locP);
            //если localPath не относительный, возвращаем его            
            if (IsAbsolutePath(locP))
                return locP;
            //иначе создаем абсолютный путь из локального и базового 
            String result = Path.Combine(basedir, locP);

            return result;
        }

        /// <summary>
        /// NT-Удалить последний символ строки если это слеш ( \ )
        /// </summary>
        /// <param name="s">Исходная строка</param>
        /// <returns></returns>
        private static String RemoveEndSlash(String s)
        {
            if (s.EndsWith(FilePathLimiter))
                s = s.Remove(s.Length - 1);
            return s;
        }
        /// <summary>
        /// NT-Удалить начальный символ строки если это слеш ( \ )
        /// </summary>
        /// <param name="s">Исходная строка</param>
        /// <returns></returns>
        private static String RemoveStartSlash(String s)
        {
            if (s.StartsWith(FilePathLimiter))
                s = s.Substring(1);
            return s;
        }

        /// <summary>
        /// RT-Вернуть относительный (если возможно) или абсолютный путь к каталогу
        /// </summary>
        /// <param name="basedir">Абсолютный путь основного каталога</param>
        /// <param name="absolutePath">Относительный или абсолютный путь конечного каталога</param>
        /// <returns></returns>
        public static string makeRelativePath(string basedir, string absolutePath)
        {
            //проверяем аргументы
            if (String.IsNullOrEmpty(basedir))
                throw new ArgumentException("Неправильный путь каталога", "basedir");
            if (absolutePath == null)
                throw new ArgumentException("Неправильный путь каталога", "absolutePath");
            if (!IsAbsolutePath(basedir))
                throw new ArgumentException("Путь должен быть абсолютным", "basedir");
            //удалить конечный слеш в базовом каталоге если он есть
            String basePath = basedir.Trim();
            basePath = RemoveEndSlash(basePath);
            //удалить начальный и конечный слеш в пути конечного каталога, если они есть
            String absPath = absolutePath.Trim();
            absPath = RemoveStartSlash(absPath);
            absPath = RemoveEndSlash(absPath);
            //если absolutePath - абсолютный
            if (IsAbsolutePath(absPath))
            {
                //то надо определить, приводится ли он в относительный путь
                //и если не приводится, то вернуть целиком
                //а если приводится, то вернуть относительную часть.
                String p1 = Path.GetFullPath(basePath);
                String p2 = Path.GetFullPath(absPath);
                bool res = p2.StartsWith(p1, StringComparison.InvariantCultureIgnoreCase);
                if (res == true)//если absolutePath начинается с basedir, то он и приводится в относительный путь.
                {
                    string result = p2.Remove(0, p1.Length);
                    result = RemoveStartSlash(result);
                    return result;
                }
                else
                    return absPath;//поскольку не приводится, то вернуть целиком
            }
            else
            {
                //если absolutePath - относительный, просто копируем его.
                return absPath;
            }
        }


        /// <summary>
        /// RT-Убедиться что файловый путь является абсолютным
        /// </summary>
        /// <param name="p">Проверяемый файловый путь, не сетевой.</param>
        /// <returns></returns>
        public static bool IsAbsolutePath(string p)
        {
            if (p == null) throw new ArgumentException("Path is null", "p");
            //если путь - пустая строка то это точно локальный путь.
            if (p == String.Empty)
                return false;
            //проверяем
            String vol = Path.GetPathRoot(p);
            //returns "" or "\" for relative path, and "C:\" for absolute path
            if (vol.Length != 3)
                return false;
            //первый символ должен быть буквой дискового тома
            return (Char.IsLetter(vol, 0));
        }

        /// <summary>
        /// NT-Создать каталог, в котором запрещено индексирование средствами операционной системы
        /// </summary>
        /// <param name="folderPath">Путь к создаваемому каталогу</param>
        public static void CreateNotIndexedFolder(String folderPath)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            //create directory
            if (!di.Exists)
                di.Create();
            //set attribute Not indexed
            di.Attributes = FileAttributes.NotContentIndexed | FileAttributes.Directory;
            di = null;
            return;
        }

        #endregion


        /// <summary>
        /// NT-копировать массив байт
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        internal static byte[] CopyArray(byte[] src)
        {
            if (src == null)
                throw new NullReferenceException("Byte array = null");
            int len = src.Length;
            byte[] result = new Byte[len];
            if (len == 0) return result;
            //copy data
            Array.Copy(src, result, len);
            return result;
        }

        /// <summary>
        /// NT-Compare two byte array
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns>Return True if array's are equal, False otherwise</returns>
        public static bool ArrayEqual(byte[] b1, byte[] b2)
        {
            if ((b1 == null) && (b2 == null)) return true;
            else if ((b1 == null) || (b2 == null)) return false;
            else if (b1.Length != b2.Length) return false;
            else
            {
                for (int i = 0; i < b1.Length; i++)
                {
                    if (b1[i] != b2[i]) return false;
                }
                return true;
            }
        }



    }
}
