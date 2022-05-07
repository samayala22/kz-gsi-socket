using GSISocket.Socket;

namespace kz_gsi_socket
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                GSI_Socket();
            }).Start();

            ApplicationConfiguration.Initialize();
            Application.Run(new AppContext());            
        }

        static void GSI_Socket()
        {
            var gsi = new GSISocketServer();
            gsi.Start();
            gsi.SetupSockets();
        }
    }
}