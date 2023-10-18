using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.model;

namespace Framework.service
{
    public class UserCreator
    {
        private readonly string userName = "seleniumtest1@interia.pl";
        private readonly string userPassword = "Testowekonto";

        public User withCredentials()
        {
            return new User(userName, this.userPassword);
        }

        public User withEmptyUserName()
        {
            return new User("", userPassword);
        }

        public User withEmptyUserPassword()
        {
            return new User(userName, "");
        }
    }
}
