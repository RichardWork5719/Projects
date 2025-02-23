using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;



namespace MessingWithCode
{
    class KeyboardControler
    {
        /*
        const UInt32 WM_KEYDOWN = 0x0100;
        const int VK_F5 = 0x74;
        
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        
        public static void PressKey()
        {
            Process[] processes = Process.GetProcessesByName("Iridium");
        
            foreach (Process proc in processes)
            {
                PostMessage(proc.MainWindowHandle, WM_KEYDOWN, VK_F5, 0);
            }
        }
        */

        /* from char to virtual key */
        [DllImport("user32.dll")]
        private static extern short VkKeyScan(char ch);

        /*
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll")]
        private static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out] char[] pwszBuff, int cchBuff, uint uFlags, IntPtr dwhkl);
        */

        //[DllImport("user32.dll")]
        //private static extern IntPtr GetKeyboardLayout(uint idThread); // already used lol

        /* keyboard language detector */
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hwnd, IntPtr proccess);

        [DllImport("user32.dll")]
        static extern IntPtr GetKeyboardLayout(uint thread);

        /* keyboard pressing thing */
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        /*
        //===========================================================================================================================================================================================
        //=================================VK-Defining Start=========================================================================================================================================
        //===========================================================================================================================================================================================
        public const byte VK_LBUTTON = 0x01;                   // Left mouse button
        public const byte VK_RBUTTON = 0x02;                   // Right mouse button
        public const byte VK_CANCEL = 0x03;                    // Control-break processing
        public const byte VK_MBUTTON = 0x04;                   // Middle mouse button
        public const byte VK_XBUTTON1 = 0x05;                  // X1 mouse button
        public const byte VK_XBUTTON2 = 0x06;                  // X2 mouse button
                                                               // private const byte - = 0x07; // Reserved
        public const byte VK_BACK = 0x08;                      // BACKSPACE key
        public const byte VK_TAB = 0x09;                       // TAB key
                                                               // private const byte - = 0x0A-0B;      // Reserved
        public const byte VK_CLEAR = 0x0C;                     // CLEAR key
        public const byte VK_RETURN = 0x0D;                    // ENTER key
                                                               // private const byte - = 0x0E-0F;      // Unassigned
        public const byte VK_SHIFT = 0x10;                     // SHIFT key
        public const byte VK_CONTROL = 0x11;                   // CTRL key
        public const byte VK_MENU = 0x12;                      // ALT key
        public const byte VK_PAUSE = 0x13;                     // PAUSE key
        public const byte VK_CAPITAL = 0x14;                   // CAPS LOCK key
        public const byte VK_KANA = 0x15;                      // IME Kana mode
        public const byte VK_HANGUL = 0x15;                    // IME Hangul mode
        public const byte VK_IME_ON = 0x16;                    // IME On
        public const byte VK_JUNJA = 0x17;                     // IME Junja mode
        public const byte VK_FINAL = 0x18;                     // IME final mode
        public const byte VK_HANJA = 0x19;                     // IME Hanja mode
        public const byte VK_KANJI = 0x19;                     // IME Kanji mode
        public const byte VK_IME_OFF = 0x1A;                   // IME Off
        public const byte VK_ESCAPE = 0x1B;                    // ESC key
        public const byte VK_CONVERT = 0x1C;                   // IME convert
        public const byte VK_NONCONVERT = 0x1D;                // IME nonconvert
        public const byte VK_ACCEPT = 0x1E;                    // IME accept
        public const byte VK_MODECHANGE = 0x1F;                // IME mode change request
        public const byte VK_SPACE = 0x20;                     // SPACEBAR
        public const byte VK_PRIOR = 0x21;                     // PAGE UP key
        public const byte VK_NEXT = 0x22;                      // PAGE DOWN key
        public const byte VK_END = 0x23;                       // END key
        public const byte VK_HOME = 0x24;                      // HOME key
        public const byte VK_LEFT = 0x25;                      // LEFT ARROW key
        public const byte VK_UP = 0x26;                        // UP ARROW key
        public const byte VK_RIGHT = 0x27;                     // RIGHT ARROW key
        public const byte VK_DOWN = 0x28;                      // DOWN ARROW key
        public const byte VK_SELECT = 0x29;                    // SELECT key
        public const byte VK_PRINT = 0x2A;                     // PRINT key
        public const byte VK_EXECUTE = 0x2B;                   // EXECUTE key
        public const byte VK_SNAPSHOT = 0x2C;                  // PRINT SCREEN key
        public const byte VK_INSERT = 0x2D;                    // INS key
        public const byte VK_DELETE = 0x2E;                    // DEL key
        public const byte VK_HELP = 0x2F;                      // HELP key
        public const byte VK_0key = 0x30;                      // 0 key
        public const byte VK_1key = 0x31;                      // 1 key
        public const byte VK_2key = 0x32;                      // 2 key
        public const byte VK_3key = 0x33;                      // 3 key
        public const byte VK_4key = 0x34;                      // 4 key
        public const byte VK_5key = 0x35;                      // 5 key
        public const byte VK_6key = 0x36;                      // 6 key
        public const byte VK_7key = 0x37;                      // 7 key
        public const byte VK_8key = 0x38;                      // 8 key
        public const byte VK_9key = 0x39;                      // 9 key
                                                               // private const byte - = 0x3A-40;      // Undefined
        public const byte VK_Akey = 0x41;                      // A key
        public const byte VK_Bkey = 0x42;                      // B key
        public const byte VK_Ckey = 0x43;                      // C key
        public const byte VK_Dkey = 0x44;                      // D key
        public const byte VK_Ekey = 0x45;                      // E key
        public const byte VK_Fkey = 0x46;                      // F key
        public const byte VK_Gkey = 0x47;                      // G key
        public const byte VK_Hkey = 0x48;                      // H key
        public const byte VK_Ikey = 0x49;                      // I key
        public const byte VK_Jkey = 0x4A;                      // J key
        public const byte VK_Kkey = 0x4B;                      // K key
        public const byte VK_Lkey = 0x4C;                      // L key
        public const byte VK_Mkey = 0x4D;                      // M key
        public const byte VK_Nkey = 0x4E;                      // N key
        public const byte VK_Okey = 0x4F;                      // O key
        public const byte VK_Pkey = 0x50;                      // P key
        public const byte VK_Qkey = 0x51;                      // Q key
        public const byte VK_Rkey = 0x52;                      // R key
        public const byte VK_Skey = 0x53;                      // S key
        public const byte VK_Tkey = 0x54;                      // T key
        public const byte VK_Ukey = 0x55;                      // U key
        public const byte VK_Vkey = 0x56;                      // V key
        public const byte VK_Wkey = 0x57;                      // W key
        public const byte VK_Xkey = 0x58;                      // X key
        public const byte VK_Ykey = 0x59;                      // Y key
        public const byte VK_Zkey = 0x5A;                      // Z key
        public const byte VK_LWIN = 0x5B;                      // Left Windows key
        public const byte VK_RWIN = 0x5C;                      // Right Windows key
        public const byte VK_APPS = 0x5D;                      // Applications key
                                                               // private const byte - = 0x5E; // Reserved
        public const byte VK_SLEEP = 0x5F;                     // Computer Sleep key
        public const byte VK_NUMPAD0 = 0x60;                   // Numeric keypad 0 key
        public const byte VK_NUMPAD1 = 0x61;                   // Numeric keypad 1 key
        public const byte VK_NUMPAD2 = 0x62;                   // Numeric keypad 2 key
        public const byte VK_NUMPAD3 = 0x63;                   // Numeric keypad 3 key
        public const byte VK_NUMPAD4 = 0x64;                   // Numeric keypad 4 key
        public const byte VK_NUMPAD5 = 0x65;                   // Numeric keypad 5 key
        public const byte VK_NUMPAD6 = 0x66;                   // Numeric keypad 6 key
        public const byte VK_NUMPAD7 = 0x67;                   // Numeric keypad 7 key
        public const byte VK_NUMPAD8 = 0x68;                   // Numeric keypad 8 key
        public const byte VK_NUMPAD9 = 0x69;                   // Numeric keypad 9 key
        public const byte VK_MULTIPLY = 0x6A;                  // Multiply key
        public const byte VK_ADD = 0x6B;                       // Add key
        public const byte VK_SEPARATOR = 0x6C;                 // Separator key
        public const byte VK_SUBTRACT = 0x6D;                  // Subtract key
        public const byte VK_DECIMAL = 0x6E;                   // Decimal key
        public const byte VK_DIVIDE = 0x6F;                    // Divide key
        public const byte VK_F1 = 0x70;                        // F1 key
        public const byte VK_F2 = 0x71;                        // F2 key
        public const byte VK_F3 = 0x72;                        // F3 key
        public const byte VK_F4 = 0x73;                        // F4 key
        public const byte VK_F5 = 0x74;                        // F5 key
        public const byte VK_F6 = 0x75;                        // F6 key
        public const byte VK_F7 = 0x76;                        // F7 key
        public const byte VK_F8 = 0x77;                        // F8 key
        public const byte VK_F9 = 0x78;                        // F9 key
        public const byte VK_F10 = 0x79;                       // F10 key
        public const byte VK_F11 = 0x7A;                       // F11 key
        public const byte VK_F12 = 0x7B;                       // F12 key
        public const byte VK_F13 = 0x7C;                       // F13 key
        public const byte VK_F14 = 0x7D;                       // F14 key
        public const byte VK_F15 = 0x7E;                       // F15 key
        public const byte VK_F16 = 0x7F;                       // F16 key
        public const byte VK_F17 = 0x80;                       // F17 key
        public const byte VK_F18 = 0x81;                       // F18 key
        public const byte VK_F19 = 0x82;                       // F19 key
        public const byte VK_F20 = 0x83;                       // F20 key
        public const byte VK_F21 = 0x84;                       // F21 key
        public const byte VK_F22 = 0x85;                       // F22 key
        public const byte VK_F23 = 0x86;                       // F23 key
        public const byte VK_F24 = 0x87;                       // F24 key
                                                               // private const byte - = 0x88-8F;      // Reserved
        public const byte VK_NUMLOCK = 0x90;                   // NUM LOCK key
        public const byte VK_SCROLL = 0x91;                    // SCROLL LOCK key
                                                               // private const byte - = 0x92-96;      // OEM specific
                                                               // private const byte - = 0x97-9F;      // Unassigned
        public const byte VK_LSHIFT = 0xA0;                    // Left SHIFT key
        public const byte VK_RSHIFT = 0xA1;                    // Right SHIFT key
        public const byte VK_LCONTROL = 0xA2;                  // Left CONTROL key
        public const byte VK_RCONTROL = 0xA3;                  // Right CONTROL key
        public const byte VK_LMENU = 0xA4;                     // Left ALT key
        public const byte VK_RMENU = 0xA5;                     // Right ALT key
        public const byte VK_BROWSER_BACK = 0xA6;              // Browser Back key
        public const byte VK_BROWSER_FORWARD = 0xA7;           // Browser Forward key
        public const byte VK_BROWSER_REFRESH = 0xA8;           // Browser Refresh key
        public const byte VK_BROWSER_STOP = 0xA9;              // Browser Stop key
        public const byte VK_BROWSER_SEARCH = 0xAA;            // Browser Search key
        public const byte VK_BROWSER_FAVORITES = 0xAB;         // Browser Favorites key
        public const byte VK_BROWSER_HOME = 0xAC;              // Browser Start and Home key
        public const byte VK_VOLUME_MUTE = 0xAD;               // Volume Mute key
        public const byte VK_VOLUME_DOWN = 0xAE;               // Volume Down key
        public const byte VK_VOLUME_UP = 0xAF;                 // Volume Up key
        public const byte VK_MEDIA_NEXT_TRACK = 0xB0;          // Next Track key
        public const byte VK_MEDIA_PREV_TRACK = 0xB1;          // Previous Track key
        public const byte VK_MEDIA_STOP = 0xB2;                // Stop Media key
        public const byte VK_MEDIA_PLAY_PAUSE = 0xB3;          // Play/Pause Media key
        public const byte VK_LAUNCH_MAIL = 0xB4;               // Start Mail key
        public const byte VK_LAUNCH_MEDIA_SELECT = 0xB5;       // Select Media key
        public const byte VK_LAUNCH_APP1 = 0xB6;               // Start Application 1 key
        public const byte VK_LAUNCH_APP2 = 0xB7;               // Start Application 2 key
                                                               // private const byte - = 0xB8-B9;      // Reserved
        public const byte VK_OEM_1 = 0xBA;                     // Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the <code>;:</code> key
        public const byte VK_OEM_PLUS = 0xBB;                  // For any country/region, the <code>+</code> key
        public const byte VK_OEM_COMMA = 0xBC;                 // For any country/region, the <code>,</code> key
        public const byte VK_OEM_MINUS = 0xBD;                 // For any country/region, the <code>-</code> key
        public const byte VK_OEM_PERIOD = 0xBE;                // For any country/region, the <code>.</code> key
        public const byte VK_OEM_2 = 0xBF;                     // Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the <code>/?</code> key
        public const byte VK_OEM_3 = 0xC0;                     // Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the <code>`~</code> key
                                                               // private const byte - = 0xC1-DA;      // Reserved
        public const byte VK_OEM_4 = 0xDB;                     // Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the <code>[{</code> key
        public const byte VK_OEM_5 = 0xDC;                     // Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the <code>\\|</code> key
        public const byte VK_OEM_6 = 0xDD;                     // Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the <code>]}</code> key
        public const byte VK_OEM_7 = 0xDE;                     // Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the <code>'"</code> key
        public const byte VK_OEM_8 = 0xDF;                     // Used for miscellaneous characters; it can vary by keyboard.
                                                               // private const byte - = 0xE0; // Reserved
                                                               // private const byte - = 0xE1; // OEM specific
        public const byte VK_OEM_102 = 0xE2;                   // The <code>&lt;&gt;</code> keys on the US standard keyboard, or the <code>\\|</code> key on the non-US 102-key keyboard
                                                               // private const byte - = 0xE3-E4;      // OEM specific
        public const byte VK_PROCESSKEY = 0xE5;                // IME PROCESS key
                                                               // private const byte - = 0xE6; // OEM specific
        public const byte VK_PACKET = 0xE7;                    // Used to pass Unicode characters as if they were keystrokes. The <code>VK_PACKET</code> key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods. For more information, see Remark in <a href="/en-us/windows/win32/api/winuser/ns-winuser-keybdinput" data-linktype="absolute-path"><code>KEYBDINPUT</code></a>, <a href="/en-us/windows/win32/api/winuser/nf-winuser-sendinput" data-linktype="absolute-path"><code>SendInput</code></a>, <a href="wm-keydown" data-linktype="relative-path"><code>WM_KEYDOWN</code></a>, and <a href="wm-keyup" data-linktype="relative-path"><code>WM_KEYUP</code></a>
                                                               // private const byte - = 0xE8; // Unassigned
                                                               // private const byte - = 0xE9-F5;      // OEM specific
        public const byte VK_ATTN = 0xF6;                      // Attn key
        public const byte VK_CRSEL = 0xF7;                     // CrSel key
        public const byte VK_EXSEL = 0xF8;                     // ExSel key
        public const byte VK_EREOF = 0xF9;                     // Erase EOF key
        public const byte VK_PLAY = 0xFA;                      // Play key
        public const byte VK_ZOOM = 0xFB;                      // Zoom key
        public const byte VK_NONAME = 0xFC;                    // Reserved
        public const byte VK_PA1 = 0xFD;                       // PA1 key
        public const byte VK_OEM_CLEAR = 0xFE;                 // Clear key

        public const byte KEYEVENTF_KEYUP = 0x0002; // Key up flag
        //===========================================================================================================================================================================================
        //=================================VK-Defining End===========================================================================================================================================
        //===========================================================================================================================================================================================
        */
        /*
        static Dictionary<char, byte> KeysDict1 = new Dictionary<char, byte>()
        {
            {'0', VK_0key},
            {'1', VK_1key},
            {'2', VK_2key},
            {'3', VK_3key},
            {'4', VK_4key},
            {'5', VK_5key},
            {'6', VK_6key},
            {'7', VK_7key},
            {'8', VK_8key},
            {'9', VK_9key},

            {'a', VK_Akey},
            {'b', VK_Bkey},
            {'c', VK_Ckey},
            {'d', VK_Dkey},
            {'e', VK_Ekey},
            {'f', VK_Fkey},
            {'g', VK_Gkey},
            {'h', VK_Hkey},
            {'i', VK_Ikey},
            {'j', VK_Jkey},
            {'k', VK_Kkey},
            {'l', VK_Lkey},
            {'m', VK_Mkey},
            {'n', VK_Nkey},
            {'o', VK_Okey},
            {'p', VK_Pkey},
            {'q', VK_Qkey},
            {'r', VK_Rkey},
            {'s', VK_Skey},
            {'t', VK_Tkey},
            {'u', VK_Ukey},
            {'v', VK_Vkey},
            {'w', VK_Wkey},
            {'x', VK_Xkey},
            {'y', VK_Ykey},
            {'z', VK_Zkey},

            {'A', VK_Akey},
            {'B', VK_Bkey},
            {'C', VK_Ckey},
            {'D', VK_Dkey},
            {'E', VK_Ekey},
            {'F', VK_Fkey},
            {'G', VK_Gkey},
            {'H', VK_Hkey},
            {'I', VK_Ikey},
            {'J', VK_Jkey},
            {'K', VK_Kkey},
            {'L', VK_Lkey},
            {'M', VK_Mkey},
            {'N', VK_Nkey},
            {'O', VK_Okey},
            {'P', VK_Pkey},
            {'Q', VK_Qkey},
            {'R', VK_Rkey},
            {'S', VK_Skey},
            {'T', VK_Tkey},
            {'U', VK_Ukey},
            {'V', VK_Vkey},
            {'W', VK_Wkey},
            {'X', VK_Xkey},
            {'Y', VK_Ykey},
            {'Z', VK_Zkey},

            {' ', VK_SPACE},

            {'=', VK_OEM_PLUS},             // for US keyboard this is +
            {'-', VK_OEM_MINUS},
            {',', VK_OEM_COMMA},
            {'.', VK_OEM_PERIOD},

            {';', VK_OEM_1},                        // Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the <code>;:</code> key
            {'/', VK_OEM_2},                        // Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the <code>/?</code> key
            {'\'', VK_OEM_3},                       // for US keyboard this is `~                                                                                          // Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the <code>`~</code> key
            {'[', VK_OEM_4},                        // Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the <code>[{</code> key
            {'\\', VK_OEM_5},                       // Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the <code>\\|</code> key
            {']', VK_OEM_6},                        // Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the <code>]}</code> key
            {'#', VK_OEM_7},                        // for US keyboard this is '"                                                                                          // Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the <code>'"</code> key
            {'`', VK_OEM_8},                        
            //{'', VK_OEM_102},                     // doesnt seem to do anything                                                                                           // The <code>&lt;&gt;</code> keys on the US standard keyboard, or the <code>\\|</code> key on the non-US 102-key keyboard
            

        };
        static Dictionary<string, byte> KeysDict2 = new Dictionary<string, byte>()
        {
            {"Num0", VK_NUMPAD0},
            {"Num1", VK_NUMPAD1},
            {"Num2", VK_NUMPAD2},
            {"Num3", VK_NUMPAD3},
            {"Num4", VK_NUMPAD4},
            {"Num5", VK_NUMPAD5},
            {"Num6", VK_NUMPAD6},
            {"Num7", VK_NUMPAD7},
            {"Num8", VK_NUMPAD8},
            {"Num9", VK_NUMPAD9},

            {"F1", VK_F1},
            {"F2", VK_F2},
            {"F3", VK_F3},
            {"F4", VK_F4},
            {"F5", VK_F5},
            {"F6", VK_F6},
            {"F7", VK_F7},
            {"F8", VK_F8},
            {"F9", VK_F9},
            {"F10", VK_F10},
            {"F11", VK_F11},
            {"F12", VK_F12},
            {"F13", VK_F13},
            {"F14", VK_F14},
            {"F15", VK_F15},
            {"F16", VK_F16},
            {"F17", VK_F17},
            {"F18", VK_F18},
            {"F19", VK_F19},
            {"F20", VK_F20},
            {"F21", VK_F21},
            {"F22", VK_F22},
            {"F23", VK_F23},
            {"F24", VK_F24},

            {"LWIN", VK_LWIN},
            {"RWIN", VK_RWIN},
            {"ESC", VK_ESCAPE},
            {"ENTER", VK_RETURN},
            {"SHIFT", VK_SHIFT},
            {"CTRL", VK_CONTROL},
            {"ALT", VK_MENU},
        };
        */

        /*
        private const uint MAPVK_VK_TO_VSC = 0; // Virtual-key to scan-code
        private const uint MAPVK_VSC_TO_VK = 1; // Scan-code to virtual-key
        */

        public const byte KEYEVENTF_KEYUP = 0x0002; // Key up flag

        public static readonly Dictionary<string, ushort> CharToVkMap = new Dictionary<string, ushort>
        {
            // Uppercase letters
            { "A", 0x41 }, { "B", 0x42 }, { "C", 0x43 }, { "D", 0x44 },
            { "E", 0x45 }, { "F", 0x46 }, { "G", 0x47 }, { "H", 0x48 },
            { "I", 0x49 }, { "J", 0x4A }, { "K", 0x4B }, { "L", 0x4C },
            { "M", 0x4D }, { "N", 0x4E }, { "O", 0x4F }, { "P", 0x50 },
            { "Q", 0x51 }, { "R", 0x52 }, { "S", 0x53 }, { "T", 0x54 },
            { "U", 0x55 }, { "V", 0x56 }, { "W", 0x57 }, { "X", 0x58 },
            { "Y", 0x59 }, { "Z", 0x5A },

            // Lowercase letters
            { "a", 0x41 }, { "b", 0x42 }, { "c", 0x43 }, { "d", 0x44 },
            { "e", 0x45 }, { "f", 0x46 }, { "g", 0x47 }, { "h", 0x48 },
            { "i", 0x49 }, { "j", 0x4A }, { "k", 0x4B }, { "l", 0x4C },
            { "m", 0x4D }, { "n", 0x4E }, { "o", 0x4F }, { "p", 0x50 },
            { "q", 0x51 }, { "r", 0x52 }, { "s", 0x53 }, { "t", 0x54 },
            { "u", 0x55 }, { "v", 0x56 }, { "w", 0x57 }, { "x", 0x58 },
            { "y", 0x59 }, { "z", 0x5A },

            // Digits
            { "0", 0x30 }, { "1", 0x31 }, { "2", 0x32 }, { "3", 0x33 },
            { "4", 0x34 }, { "5", 0x35 }, { "6", 0x36 }, { "7", 0x37 },
            { "8", 0x38 }, { "9", 0x39 },

            // Special characters (US)
            /*
            { " ", 0x20 }, // Space
            { "!", 0x31 }, // Shift + 1
            { "\"", 0x22 }, // Shift + 2
            { "#", 0x33 }, // Shift + 3
            { "$", 0x34 }, // Shift + 4
            { "%", 0x35 }, // Shift + 5
            { "&", 0x36 }, // Shift + 6
            { "\'", 0x27 }, // Shift + 7
            { "(", 0x39 }, // Shift + 8
            { ")", 0x30 }, // Shift + 9
            { "*", 0x6A }, // Shift + 0 (on numpad)
            { "+", 0x6B }, // Shift + = (on numpad)
            { ",", 0xBC }, // Comma
            { "-", 0xBD }, // Minus
            { ".", 0xBE }, // Period
            { "/", 0xBF }, // Slash
            { ":", 0xBA }, // Shift + ;
            { ";", 0xBA }, // Semicolon
            { "<", 0xBC }, // Shift + ,
            { "=", 0xBB }, // Equals
            { ">", 0xBE }, // Shift + .
            { "?", 0xBF }, // Shift + /
            { "@", 0x32 }, // Shift + 2
            { "[", 0xDB }, // Left bracket
            { "\\", 0xDC }, // Backslash
            { "]", 0xDD }, // Right bracket
            { "^", 0xBF }, // Shift + 6 (on some keyboards)
            { "_", 0xBD }, // Shift + -
            { "`", 0xC0 }, // Grave accent
            { "{", 0xDB }, // Shift + [
            { "}", 0xDD }, // Shift + ]
            { "|", 0xDC }, // Shift + \
            { "~", 0xC0 }, // Shift + `
            */

            // Special characters (UK)
            { " ", 0x20 }, // Space
            { "!", 0x31 }, // Shift + 1
            { "\"", 0x34 }, // Shift + 2 (UK uses " instead of @)
            { "#", 0x33 }, // 
            { "£", 0x32 }, // Shift + 3 (UK pound sign)
            { "$", 0x34 }, // Shift + 4
            { "%", 0x35 }, // Shift + 5
            { "^", 0x36 }, // Shift + 6
            { "&", 0x38 }, // Shift + 7
            { "*", 0x37 }, // Shift + 8
            { "(", 0x39 }, // Shift + 9
            { ")", 0x30 }, // Shift + 0
            { "+", 0x6B }, // Numpad +
            { ",", 0xBC }, // Comma
            { "-", 0xBD }, // Minus
            { ".", 0xBE }, // Period
            { "/", 0xBF }, // Slash
            { ":", 0xBA }, // Shift + ;
            { ";", 0xBA }, // Semicolon
            { "<", 0xBC }, // Shift + ,
            { "=", 0xBB }, // Equals
            { ">", 0xBE }, // Shift + .
            { "?", 0xBF }, // Shift + /
            { "\'", 0x32 },
            { "@", 0x32 }, // Shift + 2 (UK uses " instead of @)
            { "~", 0xC0 }, // Shift + ` (tilde)
            { "_", 0xBD }, // Shift + -
            { "{", 0xDB }, // Shift + [
            { "}", 0xDD }, // Shift + ]
            { "|", 0xDC }, // Shift + \
            { "`", 0xC0 }, // Grave accent

            // Control keys
            { "Shift", 0x10 }, // Shift key
            { "Ctrl", 0x11 },  // Control key
            { "Alt", 0x12 },   // Alt key
            { "Win", 0x5B },   // Windows key
            { "Tab", 0x09 },   // Tab key
            { "Esc", 0x1B },   // Escape key
            { "Enter", 0x0D }, // Enter key
            { "Backspace", 0x08 }, // Backspace key

            // Arrow keys
            { "Up", 0x26 },    // Up arrow
            { "Down", 0x28 },  // Down arrow
            { "Left", 0x25 },  // Left arrow
            { "Right", 0x27 }, // Right arrow

            // Function keys
            { "F1", 0x70 },    // F1 key
            { "F2", 0x71 },    // F2 key
            { "F3", 0x72 },    // F3 key
            { "F4", 0x73 },    // F4 key
            { "F5", 0x74 },    // F5 key
            { "F6", 0x75 },    // F6 key
            { "F7", 0x76 },    // F7 key
            { "F8", 0x77 },    // F8 key
            { "F9", 0x78 },    // F9 key
            { "F10", 0x79 },   // F10 key
            { "F11", 0x7A },   // F11 key
            { "F12", 0x7B },   // F12 key

            // Numpad keys
            { "Numpad0", 0x60 }, // Numpad 0
            { "Numpad1", 0x61 }, // Numpad 1
            { "Numpad2", 0x62 }, // Numpad 2
            { "Numpad3", 0x63 }, // Numpad 3
            { "Numpad4", 0x64 }, // Numpad 4
            { "Numpad5", 0x65 }, // Numpad 5
            { "Numpad6", 0x66 }, // Numpad 6
            { "Numpad7", 0x67 }, // Numpad 7
            { "Numpad8", 0x68 }, // Numpad 8
            { "Numpad9", 0x69 }, // Numpad 9
            { "NumpadAdd", 0x6B }, // Numpad +
            { "NumpadSubtract", 0x6D }, // Numpad -
            { "NumpadMultiply", 0x6A }, // Numpad *
            { "NumpadDivide", 0x6F }, // Numpad /
            { "NumpadDecimal", 0x6E }, // Numpad .

            // Additional keys
            { "PrintScreen", 0x2C }, // Print Screen key
            { "ScrollLock", 0x91 },  // Scroll Lock key
            { "PauseBreak", 0x13 },  // Pause/Break key
            { "Insert", 0x2D }, // Insert key
            { "Home", 0x24 },   // Home key
            { "PageUp", 0x21 }, // Page Up key
            { "Delete", 0x2E }, // Delete key
            { "End", 0x23 },    // End key
            { "PageDown", 0x22 }, // Page Down key
            { "LeftShift", 0xA0 }, // Left Shift key
            { "RightShift", 0xA1 }, // Right Shift key
            { "LeftCtrl", 0xA2 }, // Left Control key
            { "RightCtrl", 0xA3 }, // Right Control key
            { "LeftAlt", 0xA4 }, // Left Alt key
            { "RightAlt", 0xA5 }, // Right Alt key
            { "NumLock", 0x90 }, // Num Lock key
        };
        public static readonly Dictionary<string, ushort> SpecialCharToVkMapUK = new Dictionary<string, ushort>
        {
            { " ", 0x20 }, // Space
            { "!", 0x31 }, // Shift + 1
            { "\"", 0x34 }, // Shift + 2 (UK uses " instead of @)
            { "#", 0x33 }, // 
            { "£", 0x32 }, // Shift + 3 (UK pound sign) (")
            { "$", 0x34 }, // Shift + 4
            { "%", 0x35 }, // Shift + 5
            { "^", 0x36 }, // Shift + 6
            { "&", 0x38 }, // Shift + 7
            { "*", 0x37 }, // Shift + 8
            { "(", 0x39 }, // Shift + 9
            { ")", 0x30 }, // Shift + 0
            { "+", 0x6B }, // Numpad +
            { ",", 0xBC }, // Comma
            { "-", 0xBD }, // Minus
            { ".", 0xBE }, // Period
            { "/", 0xBF }, // Slash
            { ":", 0xBA }, // Shift + ;
            { ";", 0xBA }, // Semicolon
            { "<", 0xBC }, // Shift + ,
            { "=", 0xBB }, // Equals
            { ">", 0xBE }, // Shift + .
            { "?", 0xBF }, // Shift + /
            { "\'", 0x32 }, //
            { "@", 0x32 }, // Shift + 2 (UK uses " instead of @)
            { "~", 0xC0 }, // Shift + ` (tilde)
            { "_", 0xBD }, // Shift + -
            { "{", 0xDB }, // Shift + [
            { "}", 0xDD }, // Shift + ]
            { "|", 0xDC }, // Shift + \
            { "`", 0xC0 }, // Grave accent
        };
        public static readonly Dictionary<string, ushort> SpecialCharToVkMapUS = new Dictionary<string, ushort>
        {
            { " ", 0x20 }, // Space
            { "!", 0x31 }, // Shift + 1
            { "\"", 0x22 }, // Shift + 2
            { "#", 0x33 }, // Shift + 3
            { "$", 0x34 }, // Shift + 4
            { "%", 0x35 }, // Shift + 5
            { "&", 0x36 }, // Shift + 6
            { "\'", 0x27 }, // Shift + 7
            { "(", 0x39 }, // Shift + 8
            { ")", 0x30 }, // Shift + 9
            { "*", 0x6A }, // Shift + 0 (on numpad)
            { "+", 0x6B }, // Shift + = (on numpad)
            { ",", 0xBC }, // Comma
            { "-", 0xBD }, // Minus
            { ".", 0xBE }, // Period
            { "/", 0xBF }, // Slash
            { ":", 0xBA }, // Shift + ;
            { ";", 0xBA }, // Semicolon
            { "<", 0xBC }, // Shift + ,
            { "=", 0xBB }, // Equals
            { ">", 0xBE }, // Shift + .
            { "?", 0xBF }, // Shift + /
            { "@", 0x32 }, // Shift + 2
            { "[", 0xDB }, // Left bracket
            { "\\", 0xDC }, // Backslash
            { "]", 0xDD }, // Right bracket
            { "^", 0xBF }, // Shift + 6 (on some keyboards)
            { "_", 0xBD }, // Shift + -
            { "`", 0xC0 }, // Grave accent
            { "{", 0xDB }, // Shift + [
            { "}", 0xDD }, // Shift + ]
            { "|", 0xDC }, // Shift + \
            { "~", 0xC0 }, // Shift + `
        };

        public static CultureInfo GetCurrentKeyboardLayout()
        {
            try
            {
                IntPtr foregroundWindow = GetForegroundWindow();
                uint foregroundProcess = GetWindowThreadProcessId(foregroundWindow, IntPtr.Zero);
                int keyboardLayout = GetKeyboardLayout(foregroundProcess).ToInt32() & 0xFFFF;
                return new CultureInfo(keyboardLayout);
            }
            catch (Exception)
            {
                return new CultureInfo(1033); // Default to English (US) if an error occurs
            }
        }

        /// <summary>
        /// Presses a single char key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="debug"></param>
        public static void KeyPress(char key, bool debug = false)
        {
            short vkCode = VkKeyScan(key);
            byte virtualKey = (byte)(vkCode & 0xFF);
            byte shiftState = (byte)((vkCode >> 8) & 0xFF);

            if (debug)
            {
                Console.WriteLine($"Character: '{key}'");
                Console.WriteLine($"Virtual Key Scan Code: {vkCode}");
                Console.WriteLine($"Virtual Key Code: {virtualKey:X}");
                Console.WriteLine($"Shift State: {shiftState}");
            }

            if (shiftState == (byte)1)
            {
                KeyDown("Shift");
            }

            SendKey(virtualKey);

            if (shiftState == (byte)1)
            {
                KeyUp("Shift");
            }
        }

        static void SendKey(byte key)
        {
            keybd_event(key, 0, 0, UIntPtr.Zero); // Key down
            keybd_event(key, 0, KEYEVENTF_KEYUP, UIntPtr.Zero); // Key up
        }

        static void KeyDown(byte key)
        {
            keybd_event(key, 0, 0, UIntPtr.Zero); // Key down
        }

        static void KeyUp(byte key)
        {
            keybd_event(key, 0, KEYEVENTF_KEYUP, UIntPtr.Zero); // Key up
        }

        /// <summary>
        /// "Shift"/"Ctrl"/"Alt"/"Win"/"Tab"/"Esc"/"Enter"/"Backspace"/
        /// "Up"/"Down"/"Left"/"Right"/
        /// "F1"/"F2"/"F3"/"F4"/"F5"/"F6"/"F7"/"F8"/"F9"/"F10"/"F11"/"F12"/
        /// "Numpad0"/"Numpad1"/"Numpad2"/"Numpad3"/"Numpad4"/"Numpad5"/"Numpad6"/"Numpad7"/"Numpad8"/"Numpad9"/
        /// "NumpadAdd"/"NumpadSubtract"/"NumpadMultiply"/"NumpadDivide"/"NumpadDecimal"/"NumLock"/
        /// "PrintScreen"/"ScrollLock"/"PauseBreak"/"Insert"/"Home"/"PageUp"/"Delete"/"End"/"PageDown"/
        /// "LeftShift"/"RightShift"/"LeftCtrl"/"RightCtrl"/"LeftAlt"/"RightAlt"
        /// </summary>
        /// <param name="key"></param>
        public static void KeyDown(string key)
        {
            KeyDown((byte)CharToVkMap[key]); // Key down
        }

        /// <summary>
        /// "Shift"/"Ctrl"/"Alt"/"Win"/"Tab"/"Esc"/"Enter"/"Backspace"/
        /// "Up"/"Down"/"Left"/"Right"/
        /// "F1"/"F2"/"F3"/"F4"/"F5"/"F6"/"F7"/"F8"/"F9"/"F10"/"F11"/"F12"/
        /// "Numpad0"/"Numpad1"/"Numpad2"/"Numpad3"/"Numpad4"/"Numpad5"/"Numpad6"/"Numpad7"/"Numpad8"/"Numpad9"/
        /// "NumpadAdd"/"NumpadSubtract"/"NumpadMultiply"/"NumpadDivide"/"NumpadDecimal"/"NumLock"/
        /// "PrintScreen"/"ScrollLock"/"PauseBreak"/"Insert"/"Home"/"PageUp"/"Delete"/"End"/"PageDown"/
        /// "LeftShift"/"RightShift"/"LeftCtrl"/"RightCtrl"/"LeftAlt"/"RightAlt"
        /// </summary>
        /// <param name="key"></param>
        public static void KeyUp(string key)
        {
            KeyUp((byte)CharToVkMap[key]); // Key up
        }

        /// <summary>
        /// Works as a single key press too if only one letter/character is provided.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="withDelay"></param>
        public static void TypeString(string text, bool withDelay = false)
        {
            foreach (char c in text)
            {
                if (withDelay)
                {
                    Thread.Sleep(new Random().Next(10, 250));
                }
                KeyPress(c);
            }
        }

        /*
       static void KeyPress2(char key)
       {
           if (char.IsAsciiLetterOrDigit(key))
           {
               if (!char.IsAsciiDigit(key))
               {
                   if (char.IsAsciiLetterLower(key))
                   {
                       Console.WriteLine($"conversion guess: LOW {(byte)(key - 32)}"); // this works

                       SendKey((byte)(key - 32));
                   }
                   else
                   {
                       Console.WriteLine($"conversion guess: UPP {(byte)key}"); // this works

                       KeyDown("Shift");
                       SendKey((byte)key);
                       KeyUp("Shift");
                   }
               }
               else
               {
                   Console.WriteLine($"conversion guess: NUM {(byte)key}"); // this works

                   SendKey((byte)key);
               }
           }
           else if (char.IsPunctuation(key) || char.IsSymbol(key))
           {
               Console.WriteLine($"conversion guess: PUN {(byte)key} zeroMap: {MapVirtualKey((byte)key, 0)} oneMap: {MapVirtualKey((byte)key, 1)}");

               short vkCode = VkKeyScan(key);
               byte virtualKey = (byte)(vkCode & 0xFF);
               byte shiftState = (byte)((vkCode >> 8) & 0xFF);

               Console.WriteLine($"Character: '{key}'");
               Console.WriteLine($"Virtual Key Scan Code: {vkCode}");
               Console.WriteLine($"Virtual Key Code: {virtualKey:X}");
               Console.WriteLine($"Shift State: {shiftState}");

               if (shiftState == (byte)1)
               {
                   KeyDown("Shift");
               }

               SendKey(virtualKey);

               if (shiftState == (byte)1)
               {
                   KeyUp("Shift");
               }
           }
           else if (char.IsSeparator(key))
           {
               Console.WriteLine($"conversion guess: SPA {(byte)key}"); // this works

               SendKey((byte)key);
           }
           else
           {
               Console.WriteLine($"conversion guess: {(byte)key}");
           }
       }
       */

        /*
        static void KeyPress(char key)
        {
            SendKey((byte)CharToVkMap[key.ToString()]);
        }

        static void KeyPress(string key)
        {
            SendKey((byte)CharToVkMap[key]);
        }

        static void SpecialCharPress(char key, CultureInfo keyboardLang = null)
        {
            if (keyboardLang == null)
            {
                KeyPress(key);
            }
            else if (keyboardLang.Name == "en-GB") // AKA proper english
            {
                SendKey((byte)SpecialCharToVkMapUK[key.ToString()]);
            }
            else if (keyboardLang.Name == "en-US") // AKA broken english
            {
                SendKey((byte)SpecialCharToVkMapUS[key.ToString()]);
            }
            else
            {
                Debug.WriteLine($"Unknown/Unimplemented keyboard language: {keyboardLang.Name}\nUsing Default");
                KeyPress(key);
            }
        }
        */

        /*
            47 = /
            48 = 0
            49 = 1
            50 = 2
            51 = 3
            52 = 4
            53 = 5
            54 = 6
            55 = 7
            56 = 8
            57 = 9
            58 = ?
            59 = ?
            60 = ?
            61 = ?
            62 = ?
            63 = ?
            64 = ?
            65 = A
            66 = B
            67 = C
            68 = D
            69 = E
            70 = F
            71 = G
            72 = H
            73 = I
            74 = J
            75 = K
            76 = L
            77 = M
            78 = N
            79 = O
            80 = P
            81 = Q
            82 = R
            83 = S
            84 = T
            85 = U
            86 = V
            87 = W
            88 = X
            89 = Y
            90 = Z
            91 = WINKEY
            92 = WINKEY ?
            93 = RightClick ?
            94 = ?
            95 = ?
            96-105 = 0-9
            106 = *
            107 = +
            108 = ?
            109 = -
            110 = .
            111 = /
            112 = some kind of fucking auto browser openner (opened this cuz i was in notepad: https://www.bing.com/search?q=get%20help%20with%20notepad%20in%20windows&filters=guid:%224466414-en-dia%22%20lang:%22en%22)
            113 = ?
            114 = ?
            115 = ?
            116 = fucking print the current time and date!?!?!?!? (in notepad)
            117 = ?
            118 = ?
            119 = ?
            120 = ?
            121 = select/navigateTo the file tab/box/topLeftOptionInLikeMostApps
            122 = ?
            123 = ? does nothing in notepad but in VS it says it "cannot navigate to the symbol under the caret" ... no clue what that means
            124 = ?
            125 = ?
            126 = ?
            127 = ?
            128 = ?
            129 = ?
            130-169(nice) = ?
            170 = opens a fucking browser inside of VS and does the search function in notepad
            171-190 = ?
         */

        /*
        public static void TestKeys()
        {
            for (byte i = byte.MinValue; i < byte.MaxValue; i++)
            {
                //Thread.Sleep(new Random().Next(10, 250));
                Thread.Sleep(5000);
                Console.WriteLine($"{i} = {(char)i}");
                SendKey(i);
            }
        }
        */
    }

    /*
    public class KeyboardTest1
    {
        // Define the INPUT structure
        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public uint type;
            public KEYBDINPUT ki;
        }

        // Define the KEYBDINPUT structure
        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk; // Virtual-key code
            public ushort wScan; // Hardware scan code
            public uint dwFlags; // Flags indicating various aspects of the keystroke
            public uint time; // Time stamp for the event
            public IntPtr dwExtraInfo; // Additional data associated with the keystroke
        }

        // Constants
        const uint INPUT_KEYBOARD = 1;
        const uint KEYEVENTF_KEYUP = 0x0002;

        // Import the SendInput function from user32.dll
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        // Virtual key codes
        private static readonly ushort[] VirtualKeyCodes = new ushort[256];

        public static void TypeKey(string key)
        {
            if (key.Length != 1)
            {
                Console.WriteLine("Only single character keys are supported.");
                return;
            }

            char character = key[0];
            ushort vkCode = (ushort)character;

            // Handle special keys (e.g., Shift, Ctrl, etc.)
            if (char.IsUpper(character))
            {
                // Send Shift down
                SendKey(0x10); // VK_SHIFT
            }

            // Send the key down
            SendKey(vkCode);

            // Send the key up
            SendKey(vkCode, true);

            if (char.IsUpper(character))
            {
                // Send Shift up
                SendKey(0x10, true); // VK_SHIFT
            }
        }

        public static void SendKey(ushort keyCode, bool keyUp = false)
        {
            INPUT[] inputs = new INPUT[1];

            inputs[0].type = INPUT_KEYBOARD;
            inputs[0].ki.wVk = keyCode;
            inputs[0].ki.wScan = 0;
            inputs[0].ki.dwFlags = keyUp ? KEYEVENTF_KEYUP : 0; // Key up if specified
            inputs[0].ki.time = 0;
            inputs[0].ki.dwExtraInfo = IntPtr.Zero;

            // Send the input
            SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        public static void InitializeVirtualKeyCodes()
        {
            for (int i = 0; i < 256; i++)
            {
                VirtualKeyCodes[i] = (ushort)i;
            }
        }
    }

    public class KeyboardSimulator
    {
        // Define the INPUT structure
        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public uint type;
            public KEYBDINPUT ki;
        }

        // Define the KEYBDINPUT structure
        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk; // Virtual-key code
            public ushort wScan; // Hardware scan code
            public uint dwFlags; // Flags indicating various aspects of the keystroke
            public uint time; // Time stamp for the event
            public IntPtr dwExtraInfo; // Additional data associated with the keystroke
        }

        // Constants
        const uint INPUT_KEYBOARD = 1;
        const uint KEYEVENTF_KEYUP = 0x0002;

        // Import the SendInput function from user32.dll
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        public static void TypeKey(string key)
        {
            if (key.Length != 1)
            {
                Console.WriteLine("Only single character keys are supported.");
                return;
            }

            char character = key[0];
            ushort vkCode = (ushort)character;

            // Handle special keys (e.g., Shift for uppercase letters)
            if (char.IsUpper(character))
            {
                // Send Shift down
                SendKey(0x10); // VK_SHIFT
            }

            // Send the key down
            SendKey(vkCode);

            // Send the key up
            SendKey(vkCode, true);

            if (char.IsUpper(character))
            {
                // Send Shift up
                SendKey(0x10, true); // VK_SHIFT
            }
        }

        private static void SendKey(ushort keyCode, bool keyUp = false)
        {
            INPUT[] inputs = new INPUT[1];

            inputs[0].type = INPUT_KEYBOARD;
            inputs[0].ki.wVk = keyCode;
            inputs[0].ki.wScan = 0;
            inputs[0].ki.dwFlags = keyUp ? KEYEVENTF_KEYUP : 0; // Key up if specified
            inputs[0].ki.time = 0;
            inputs[0].ki.dwExtraInfo = IntPtr.Zero;

            // Send the input
            SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
        }
    }
    */
}

