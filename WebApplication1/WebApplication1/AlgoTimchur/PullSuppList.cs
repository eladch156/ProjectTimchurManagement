using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Database;

namespace WebApplication1.AlgoTimchur
{
    public interface PullSuppList
    {
        /***
        יש לממש את האינטרפייס הנ"ל
        גישה לטבלה - Entity Framework :
        using(TimchurDatabaseEntities ent=new TimchurDatabaseEntities())
        {

            }
            זה יוצר מופע לוקאלי ששואב ערכים מתוך הטבלה 
            https://www.tutorialspoint.com/entity_framework/entity_framework_database_operations.htm
            לאייך בעקרון לעבוד יש עוד מדריכים טובים באינטרנט אל תתעצל
            המנעול ב  Cache.gen_lock סטאטי לכולם אין מה לדאוג
            לכל טיפוס הוספתי הסבר קצר בהצלחה
            לשגיאות תשתמש ב try catch אני כבר יטפל בזה 
            תבצע את תהליך "2.6.8.	תהליך שליפת רשימת ספקים חדשה"
    ***/

        TablePullResult TichurAlgorithem(TichurInfo input);
    }
}