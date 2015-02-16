#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace RouteDefense
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class RouteDefense
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MainGameEngine game = new MainGameEngine();
            game.Run();
        }
    }
#endif
}
