using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace Bar.Utility
{
    /// <summary>
    /// Использует Windows Shell подсистему для выполнения файловых операций с возможностью их отмены пользователем.
    /// </summary>
    public class ShellFileOperations
    {
        private const int FO_MOVE = 1;
        private const int FO_COPY = 2;
        private const int FO_DELETE = 3;

        private const int FOF_MULTIDESTFILES = 1;
        private const int FOF_NOCONFIRMMKDIR = 512;
        private const int FOF_NOERRORUI = 1024;
        private const int FOF_SILENT = 4;
        private const int FOF_WANTNUKEWARNING = 0x4000;     // Windows 2000 and later
        private const int FOF_ALLOWUNDO = 0x0040;           // Preserve undo information, if possible. 
        private const int FOF_NOCONFIRMATION = 0x0010;      // Show no confirmation dialog box to the user

        // Struct which contains information that the SHFileOperation function uses to perform file operations. 
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)]
            public int wFunc;
            public string pFrom;
            public string pTo;
            public short fFlags;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);

        /// <summary>
        /// Переместить файл или папку в корзину
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFileOrFolder(string path)
        {
            //Возвращает 0 если успешно или если пользователь отменил операцию, не 0 при любой ошибке
            //Приложение нормально просто проверяет результат на 0  
            //Значение поля fAnyOperationsAborted в SHFILEOPSTRUCT сигнализирует, что пользователь отменил операцию. 
            //Если не проверять fAnyOperationsAborted, то нельзя узнать, выполнена ли операция, или пользователь ее отменил. 

            SHFILEOPSTRUCT fileop = new SHFILEOPSTRUCT();
            fileop.wFunc = FO_DELETE;
            fileop.pFrom = path + '\0' + '\0';
            fileop.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION;
            SHFileOperation(ref fileop);
        }

        /// <summary>
        /// Собственно файловая операция
        /// </summary>
        /// <param name="opcode">Код операции, один из: FO_COPY FO_MOVE FO_DELETE</param>
        /// <param name="srcPath">Абсолютный путь к исходному файлу или каталогу</param>
        /// <param name="dstPath">Асолютный путь к конечному файлу или каталогу, null если не нужен</param>
        private static int shellFileOp(ref bool Cancelled, int opcode, string srcPath, string dstPath)
        {
            //fileOp = SHFILEOPSTRUCTW()
            SHFILEOPSTRUCT fileop = new SHFILEOPSTRUCT();
            //srcPathWc = ctypes.c_wchar_p(srcPath + u"\0")
            //fileOp.hwnd = 0
            //fileOp.wFunc = opcode
            //fileOp.pFrom = srcPathWc
            fileop.wFunc = opcode;
            fileop.pFrom = srcPath + '\0' + '\0';
            //if dstPath is not None:
            //    dstDir = os.path.dirname(dstPath)
            if (!String.IsNullOrEmpty(dstPath))
            {
                String dstDir = Path.GetDirectoryName(dstPath);
                //    if not os.path.exists(pathEnc(dstDir)):
                //        os.makedirs(dstDir)
                //создать папку - тут надо повозиться - оно почему-то плохо тут работает
                DirectoryInfo di = new DirectoryInfo(dstDir);//не создает каталог почему-то
                if (!di.Exists)
                {
                    //di.Create();//не работает
                    Directory.CreateDirectory(di.FullName);
                    di.Refresh();
                    //установить атрибуты, запрещающие индексацию, архивацию и прочее в том же духе
                    di.Attributes = (di.Attributes | FileAttributes.NotContentIndexed);
                }

                fileop.pTo = dstPath + '\0' + '\0';
            }
            else fileop.pTo = null;

            fileop.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION | FOF_MULTIDESTFILES | FOF_NOCONFIRMMKDIR | FOF_WANTNUKEWARNING;

            int result = SHFileOperation(ref fileop);
            //вернуть флаг что операция была прервана пользователем через ГУЙ
            Cancelled = fileop.fAnyOperationsAborted;

            //Возвращаем код ошибки для последующей обработки
            return result;
        }

        /// <summary>
        /// NT-Копировать файл или каталог с показом диалога и возможностью отменить операцию.
        /// </summary>
        /// <param name="srcPath">Путь к исходному файлу</param>
        /// <param name="dstPath">Путь к конечному файлу</param>
        /// <returns>Функция возвращает true, если операция была выполнена успешно, false если операция прервана пользователем.</returns>
        /// <remarks>HTML файлы обрабатываются вместе с принадлежащими им папками ресурсов, если в Проводнике установлен соответствующий флаг.</remarks>
        public static bool CopyFile(string srcPath, string dstPath)
        {
            //Copy file from srcPath to dstPath. dstPath may be overwritten if
            //existing already. dstPath must point to a file, not a directory.
            //If some directories in dstPath do not exist, they are created.
            bool Cancelled = false;
            int result = shellFileOp(ref Cancelled, FO_COPY, srcPath, dstPath);
            if (result != 0)
            {
                throw new IOException(String.Format("Copying from {0} to {1} failed. SHFileOperation returns {2}", srcPath, dstPath, result));
            }
            return !Cancelled;
        }
        /// <summary>
        /// NT-Переместить файл или каталог с показом диалога и возможностью отменить операцию.
        /// </summary>
        /// <param name="srcPath">Путь к исходному файлу</param>
        /// <param name="dstPath">Путь к конечному файлу</param>
        /// <returns>Функция возвращает true, если операция была выполнена успешно, false если операция прервана пользователем.</returns>
        /// <remarks>HTML файлы обрабатываются вместе с принадлежащими им папками ресурсов, если в Проводнике установлен соответствующий флаг.</remarks>
        public static bool MoveFile(string srcPath, string dstPath)
        {
            //Move file from srcPath to dstPath. dstPath may be overwritten if
            //existing already. dstPath must point to a file, not a directory.
            //If some directories in dstPath do not exist, they are created.
            bool Cancelled = false;
            int result = shellFileOp(ref Cancelled, FO_MOVE, srcPath, dstPath);
            if (result != 0)
            {
                throw new IOException(String.Format("Moving from {0} to {1} failed. SHFileOperation returns {2}", srcPath, dstPath, result));
            }
            return !Cancelled;
        }
        /// <summary>
        /// NT-Удалить файл или каталог с показом диалога и возможностью отменить операцию.
        /// </summary>
        /// <param name="srcPath">Путь к удаляемому файлу</param>
        /// <returns>Функция возвращает true, если операция была выполнена успешно, false если операция прервана пользователем.</returns>
        /// <remarks>
        /// Файл удаляется в Корзину Windows.
        /// HTML файлы обрабатываются вместе с принадлежащими им папками ресурсов, если в Проводнике установлен соответствующий флаг.
        /// </remarks>
        public static bool DeleteFile(string srcPath)
        {
            //Delete file or directory  path.
            bool Cancelled = false;
            int result = shellFileOp(ref Cancelled, FO_DELETE, srcPath, null);
            if (result != 0)
            {
                throw new IOException(String.Format("Deleting {0} failed. SHFileOperation returns {1}", srcPath, result));
            }
            return !Cancelled;
        }



    }
}
