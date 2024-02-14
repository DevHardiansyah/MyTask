using MyTask.Repository;
using System.Linq;


namespace MyTask.BusinessLogic.AppsBusinessLogic
{
    public class ProfileBusinessLogic
    {
        MyTaskEntities db = new MyTaskEntities();

        public fnGetuserbyUsername_Result fnGetuserbyUsername(string username)
        {
            var data = db.fnGetuserbyUsername(username).FirstOrDefault();
            return data == null ? null : data;
        }
    }
}
