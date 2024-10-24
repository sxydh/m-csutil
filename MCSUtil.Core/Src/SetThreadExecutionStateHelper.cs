using System.Runtime.InteropServices;

namespace MCSUtil.Core
{
    public static class SetThreadExecutionStateHelper
    {
        private const uint ES_CONTINUOUS = 0x80000000;
        private const uint ES_DISPLAY_REQUIRED = 0x00000002;
        private const uint ES_SYSTEM_REQUIRED = 0x00000001;

        public static uint KeepAwake()
        {
            return SetThreadExecutionState(ES_CONTINUOUS | ES_DISPLAY_REQUIRED | ES_SYSTEM_REQUIRED);
        }

        public static uint ResetAwake()
        {
            return SetThreadExecutionState(ES_CONTINUOUS);
        }

        [DllImport("kernel32.dll")]
        private static extern uint SetThreadExecutionState(uint esFlags);
    }
}