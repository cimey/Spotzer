using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotzer.Model
{
    public enum CustomExceptionTypeEnum
    {
        /// <summary>
        /// Gecersiz bir Parametre geldigi durumlar (400)
        /// </summary>
        BadRequest = 400,
        /// <summary>
        /// Kullanici'nin authorized olmadigi durumlar (401)
        /// </summary>
        Unauthorized = 401,
        /// <summary>
        /// Authorized olmus Kullanici'nin istenilen isleme yetkisinin olmadigi durumlar (403)
        /// </summary>
        Forbidden = 403,
        /// <summary>
        /// Istenilen entity'nin bulunamadigi durumlar (404)
        /// </summary>
        NotFound = 404,
        /// <summary>
        /// Islem icin gerekli olan on kosullarin saglanmadigi durumlarda (409). 
        /// Or: Baska tabloda related veri varsa.
        /// </summary>
        Conflict = 409,
        /// <summary>
        /// Islem sirasinda hata olustugu durumlar (500)
        /// </summary>
        InternalServerError = 500
        //MethodNotAllowed=405
    }
    public class CustomException : Exception
    {
        public CustomExceptionTypeEnum CustomExceptionType { get; private set; }
        public string CustomMessage { get; private set; }
        public object CustomData { get; set; }

        public CustomException(CustomExceptionTypeEnum customExceptionType)
            : this(customExceptionType, "")
        {
        }
        public CustomException(CustomExceptionTypeEnum customExceptionType, string customMessage)
            : this(customExceptionType, customMessage, null)
        {
        }
        public CustomException(CustomExceptionTypeEnum customExceptionType, string customMessage, object customData)
            : base(customMessage)
        {
            this.CustomExceptionType = customExceptionType;
            this.CustomMessage = customMessage;
            this.CustomData = customData;
        }
    }
}
