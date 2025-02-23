using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessingWithCode // MouseControler
{
    class MouseControler
    {
        // Import necessary functions from user32.dll
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern void SetCursorPos(int X, int Y);

        // Constants for mouse_event
        private const uint MOUSEEVENTF_MOVE = 0x0001;
        private const uint MOUSEEVENTF_ABSOLUTE = 0x8000;

        // Structure to hold the cursor position
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        private static Timer timer;
        private static int secondsDelay = 5; // Set your delay in seconds here
        private static POINT lastMousePosition;

        public static void AFKMouse()
        {
            // Get the initial mouse position
            GetCursorPos(out lastMousePosition);

            // Initialize the timer
            timer = new Timer(TimerCallback, null, secondsDelay * 1000, Timeout.Infinite);

            // Keep the application running
            Console.WriteLine("Mouse mover is running. Press Enter to exit...");
            Console.ReadLine();
        }

        private static void TimerCallback(object state)
        {
            // Move the mouse to a random position
            Random random = new Random();
            int screenWidth = 1920; //Console.LargestWindowWidth; // Use the console window size for demonstration
            int screenHeight = 1080; // Console.LargestWindowHeight; // Use the console window size for demonstration

            int x = random.Next(0, screenWidth);
            int y = random.Next(0, screenHeight);

            POINT currentPos;
            GetCursorPos(out currentPos);

            if (currentPos.X == lastMousePosition.X && currentPos.Y == lastMousePosition.Y)
            {
                SetCursorPos(x, y);
            }

            GetCursorPos(out lastMousePosition);

            // Restart the timer
            timer.Change(secondsDelay * 1000, Timeout.Infinite);
        }

        public static void MoveMouse(int x, int y)
        {
            SetCursorPos(x, y);
        }
    }
}
