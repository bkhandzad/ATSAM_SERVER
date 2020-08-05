namespace Atsam
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
