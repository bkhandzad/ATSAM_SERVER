using Macro;

namespace Atsam.Server
{
    // Server Administrator
    public interface ISA
    {
        string getConnectionString();

        int getPortNumber();

        string getUserID();

        string getPassword();

        ErrorCode getStatus();
    }
}
