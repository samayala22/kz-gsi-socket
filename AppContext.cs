using kz_gsi_socket.Properties;

namespace kz_gsi_socket
{
    public class AppContext : ApplicationContext
    {
        private NotifyIcon trayIcon;

        public AppContext()
        { 
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.kz_logo,
                Text = "KZ Overlay GSI",
                ContextMenuStrip = new ContextMenuStrip()
                {
                    Items = { new ToolStripMenuItem("Exit", null, Exit) }
                },
                Visible = true
            };
        }

        void Exit(object? sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Environment.Exit(0);
            Application.Exit();
        }

        
    }
}
