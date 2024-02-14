using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace MyTask.Models
{
    public class EmailScheduler
    {
        readonly DBMytaskEntities db = new DBMytaskEntities();
        public EmailScheduler()
        {
            try
            {
                db.spSendEmail();
            }
            catch(Exception e)
            {
                
            }
              
        }

    }
}