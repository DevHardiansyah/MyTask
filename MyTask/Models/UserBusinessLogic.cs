using MyTask.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using static MyTask.Models.AccountViewModel;

namespace MyTask
{
    public class UserBusinessLogic
    {
        readonly DBMytaskEntities _db = new DBMytaskEntities();
        public Master_Users GetUser(LoginViewModel model)
        {
            var ret = new Master_Users()
            {
                Username = string.Empty,
            };

            var data = _db.Master_Users.FirstOrDefault(d => d.Username == model.Username && d.Password  == model.Password);

            if (data != null)
            {
                ret = data;
            }

            return ret;
        }
    }
}