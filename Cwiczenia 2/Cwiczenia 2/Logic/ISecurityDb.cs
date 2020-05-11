using Cwiczenia2.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia2.Logic
{
   public interface ISecurityDb
{

        string getSalt(string studentIndex);

        bool loginUser(LoginReq loginReq);

        void createRefreshToken(Guid guid);

        bool checkIfTokenExists(Guid guid);
}
}
