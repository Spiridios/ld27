using System;

namespace SnapEncounters
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SnapEncounters game = new SnapEncounters())
            {
                game.Run();
            }
        }
    }
#endif
}

