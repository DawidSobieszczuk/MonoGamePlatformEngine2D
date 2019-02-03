using System;
using System.Collections.Generic;
using System.IO;

namespace MonoGameWindows {
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            using (var game = new MainGameWindows())
                game.Run();
        }
    }
#endif
}
