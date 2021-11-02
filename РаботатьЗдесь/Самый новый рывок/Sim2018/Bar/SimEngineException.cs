using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Bar
{
    //See ms-help://MS.VSCC.v90/MS.MSDNQTR.v90.en/dv_fxdesignguide/html/eede32ea-1fe9-4a9b-a70a-749840b92e26.htm
    //See ms-help://MS.MSDNQTR.v90.en/fxref_mscorlib/html/1b064c3b-98b3-1ca9-c62d-320ff2a4e79d.htm
    
    //Это я рисую сериализабельное исключение для передачи данных через домены приложений итп.
    //Рисую по статьям из МСДН и нифига не понимаю, как это работает, и работает ли.

    /// <summary>
    /// NT-Базовое исключение Движка.
    /// </summary>
    /// <remarks>
    /// Оно отличает предусмотренные исключительные ситуации Движка от непредусмотренных, 
    /// которые выражаются системными типами исключений.
    /// </remarks>
    [Serializable()]
    public class SimEngineException : Exception, ISerializable
    {
        public SimEngineException() 
            : base()
        {
            // Add implementation.
        }
        public SimEngineException(string message)
            :  base(message)
        {
            // Add implementation.
        }
        public SimEngineException(string message, Exception inner) 
            : base(message, inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        protected SimEngineException(SerializationInfo info, StreamingContext context)
            :base(info, context)
        {
            // Add implementation.
        }

    }
}
