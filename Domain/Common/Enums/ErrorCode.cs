using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Enums;

public enum errorCode
{
    None = 0,              

    // Ошибки валидации (400 Bad Request)
    InvalidDataFormat = 1,
    PasswordTooShort = 2,
    EmailInvalid = 3,
    PasswordTooLong = 4,
    UserNotFound = 5,
    CoursesNotFound = 6,
    ChapterNotFound = 7,
    IpNotFound = 8,
    
    ReviewNotFound = 11,


    // Бизнес-конфликты (409 Conflict) 


    // NotFound (404 Not Found)

    // forbidden (403 Forbidden)
    NoRights = 401,


    // Системные ошибки (500 Internal Server Error)
    DatabaseError = 501,
    UnknownError = 502,
    
    NotFound = 506,

    // timeout (504 Gateway Timeout)
    TimeoutError = 603,
}
