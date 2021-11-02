using System;
using System.Collections.Generic;
using System.Text;

namespace Bar
{
    /// <summary>
    /// Коллекция ссылок на объекты разных Солюшенов
    /// </summary>
    /// <remarks>
    /// Этот класс проектируется впервые и может содержать непроверенные решения.
    /// Это полностью статический класс, который создается при создании солюшена.
    /// Он либо изолирует, либо объединяет ссылки на солюшены, открытые в процессе движка.
    /// И размечает доступ к контейнеру для ячейки или связи в мультиконтейнерной или сетевой конфигурации движка.
    /// </remarks>
    public static class MSolutionRefCollection
    {
        private static Dictionary<int, MSolution> dictionary;

        /// <summary>
        /// NT-Добавить ссылку на объект Солюшена в коллекцию
        /// </summary>
        /// <param name="solutionRef">Ссылка на объект Солюшена</param>
        /// <exception cref="SimEngineException">Другой объект Солюшена с тем же ИД уже существует</exception>
        internal static void AddSolutionReference(MSolution solutionRef)
        {
            //create new dictionary if not exists
            if (dictionary == null)
                dictionary = new Dictionary<int, MSolution>();
            //add ref to dictionary if not exists
            int key = solutionRef.SolutionId;
            if (dictionary.ContainsKey(key))
            {
                //проверить что это тот же объект солюшена, с тем же адресом в памяти.
                //если нет - выдать исключение
                MSolution sol = dictionary[key];
                if (sol != solutionRef) //TODO: тут сравнить адреса объектов в памяти
                    throw new SimEngineException("Другой объект Солюшена с тем же ИД уже существует");
            }
            else
            {
                dictionary.Add(key, solutionRef);
            }

            return;
        }
        /// <summary>
        /// NT-Получить ссылку на объект Солюшена по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор Солюшена</param>
        /// <returns>Возвращает ссылку на объект Солюшена или выбрасывает исключение, если объект не найден в коллекции.</returns>
        /// <exception cref="SimEngineException">Коллекция не инициализирована или Солюшен не найден</exception>
        public static MSolution GetSolutionReference(int id)
        {
            if(dictionary == null)
                throw new SimEngineException("Коллекция Солюшенов не инициализирована");
            
            if (dictionary.ContainsKey(id))
            {
                //проверить что это тот же объект солюшена, с тем же адресом в памяти.
                //если нет - выдать исключение
                MSolution sol = dictionary[id];
                return sol;
            }
            else
                throw new SimEngineException("Объект Солюшена не найден в коллекции солюшенов");
        }

        /// <summary>
        /// NT-Удалить ссылку на Солюшен
        /// </summary>
        /// <param name="solutionRef">Ссылка на объект Солюшена</param>
        /// <param name="throwIfNotExists">
        /// True - Выбрасывать исключение если Коллекция не инициализирована или Солюшен с таким идентификатором  не найден.
        /// False - Не выбрасывать исключение.
        /// </param>
        /// <exception cref="SimEngineException">Коллекция не инициализирована или Солюшен не найден</exception>
        internal static void RemoveSolutionReference(MSolution solutionRef, bool throwIfNotExists)
        {
            //create new dictionary if not exists
            if (dictionary == null)
            {
                //TODO: Решить, тут молча выйти или выбросить исключение об ошибке, ведь ни одного солюшена нет?
                if (throwIfNotExists) 
                    throw new SimEngineException("Коллекция Солюшенов не инициализирована");
                else
                    return;
            }
            //remove ref from dictionary if exists
            int key = solutionRef.SolutionId;
            if (!dictionary.ContainsKey(key))
            {
                //TODO: Решить, тут молча выйти или выбросить исключение об ошибке, ведь указанного солюшена нет?
                if (throwIfNotExists)
                    throw new SimEngineException("Объект Солюшена не найден в коллекции солюшенов");
                else
                    return;
            }
            dictionary.Remove(key);

            return;
        }
    }
}
