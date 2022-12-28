using System.Security.Principal;

namespace Carbon23
{
    public class utils
    {

        public bool isAdminstrator()
        {
            var ID = WindowsIdentity.GetCurrent();
            var Principal = new WindowsPrincipal(ID);
            return Principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        
    }
}