# KZ GSI Socket

> Original implementation by [Sikari](https://bitbucket.org/Sikarii/gsisocket/src/master/).

Program designed to be used with the [kz-map-overlay](https://bitbucket.org/Sikarii/kz-map-overlay/src/master/) (also by Sikari) that acts as a server for Gamestate Integration and forwards the data via sockets.

### Notes
- The overlay has been patched to fix CORS issues. Is it recommended to use the one bundled in the `obs.zip` file.
- Gamestate Integration listener is avaiable @ port 4000 by default
- Gamestate Integration socket server is available @ port 4001 by default

### Installation

In the [Releases](https://github.com/samayala22/kz-gsi-socket/releases), download:
- `gamestate_integration_kz.cfg`
- `kz-gsi-socket.exe`
- `obs.zip`

### Usage
- Put `gamestate_integration_kz.cfg` in your `/csgo/cfg/` folder.
- Run `kz-gsi-socket.exe`, if this is your first time you may need to allow access to firewall.
- Unzip `obs.zip` and add `index.html` to your streaming software as a browser source.

If the gsi socket is working, you should see the KZ logo in the tray

![tray](images/tray_bar.png)

Right click and exit when done streaming
