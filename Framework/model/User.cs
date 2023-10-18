using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.model
{
    public class User
    {
        private readonly string userName;
        private readonly string userPassword;

        public User(string userName, string userPassword)
        {
            this.userName = userName;
            this.userPassword = userPassword;
        }

        public string getUserName()
        {
            return this.userName;
        }

        public string getUserPassword()
        {
            return this.userPassword;
        }
    }
}
