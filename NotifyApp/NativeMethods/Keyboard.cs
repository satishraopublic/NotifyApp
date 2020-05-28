namespace NotifyApp.NativeMethods.Keyboard
{
    using System;
    using System.Runtime.InteropServices;

    public static class Keyboard
    {
        [DllImport("User32.Dll", EntryPoint = "PostMessageA")]
        private static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr LoadLibrary(string librayName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static int hook_installed = 0;
        private static LowLevelKeyboardProc hook_proc = hook_callback;
        private static IntPtr hook_id = IntPtr.Zero;

        private static int keycode = 0;

        private const int WH_KEYBOARD_LL = 13;
        private const int VK_RETURN = 0x0D;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;


        private static IntPtr hook_callback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            Console.Write("e: " + nCode);

            if (nCode >= 0)
            {
                if (wParam == (IntPtr)WM_KEYDOWN)
                    keycode = Marshal.ReadInt32(lParam);
                else if (wParam == (IntPtr)WM_KEYUP)
                    keycode = -1;
            }

            Console.WriteLine("e: " + keycode);


            return CallNextHookEx(hook_id, nCode, wParam, lParam);
        }


        public static void set_hook()
        {
            IntPtr h = LoadLibrary("SmallBasic Extension.dll");

            if (h == null)
                Console.WriteLine("No module handle!");
            else
            {
                hook_id = SetWindowsHookEx(WH_KEYBOARD_LL, hook_proc, h, 0);
                if (hook_id == null)
                    Console.WriteLine("FAILED HOOK!");
                else
                    Console.Write("Hooked: " + hook_id);
            }

            hook_installed = 1;
        }
    }
}