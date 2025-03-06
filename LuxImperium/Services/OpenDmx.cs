using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace LuxImperium.Services
{
    [ExcludeFromCodeCoverage]
    public abstract class OpenDmx
    {
        private static readonly byte[] Buffer = new byte[513];
        private static uint _handle;
        public static FtStatus Status;

        private const byte Bits8 = 8;
        private const byte StopBits2 = 2;
        private const byte ParityNone = 0;
        private const UInt16 FlowNone = 0;
        private const byte PurgeRx = 1;
        private const byte PurgeTx = 2;

        [DllImport("FTD2XX.dll")]
        private static extern FtStatus FT_Open(UInt32 uiPort, ref uint ftHandle);

        [DllImport("FTD2XX.dll")]
        private static extern FtStatus FT_Close(uint ftHandle);

        [DllImport("FTD2XX.dll")]
        private static extern FtStatus FT_Read(uint ftHandle, IntPtr lpBuffer, UInt32 dwBytesToRead,
            ref UInt32 lpdwBytesReturned);

        [DllImport("FTD2XX.dll")]
        private static extern FtStatus FT_Write(uint ftHandle, IntPtr lpBuffer, UInt32 dwBytesToRead,
            ref UInt32 lpdwBytesWritten);

        [DllImport("FTD2XX.dll")]
        private static extern FtStatus FT_SetDataCharacteristics(uint ftHandle, byte uWordLength, byte uStopBits,
            byte uParity);

        [DllImport("FTD2XX.dll")]
        private static extern FtStatus FT_SetFlowControl(uint ftHandle, char usFlowControl, byte uXon, byte uXoff);

        [DllImport("FTD2XX.dll")]
        private static extern FtStatus FT_GetModemStatus(uint ftHandle, ref UInt32 lpdwModemStatus);

        [DllImport("FTD2XX.dll")]
        private static extern FtStatus FT_Purge(uint ftHandle, UInt32 dwMask);

        [DllImport("FTD2XX.dll")]
        private static extern FtStatus FT_ClrRts(uint ftHandle);

        [DllImport("FTD2XX.dll")]
        private static extern FtStatus FT_SetBreakOn(uint ftHandle);

        [DllImport("FTD2XX.dll")]
        private static extern FtStatus FT_SetBreakOff(uint ftHandle);

        [DllImport("FTD2XX.dll")]
        private static extern FtStatus FT_GetStatus(uint ftHandle, ref UInt32 lpdwAmountInRxQueue,
            ref UInt32 lpdwAmountInTxQueue, ref UInt32 lpdwEventStatus);

        [DllImport("FTD2XX.dll")]
        private static extern FtStatus FT_ResetDevice(uint ftHandle);

        [DllImport("FTD2XX.dll")]
        private static extern FtStatus FT_SetDivisor(uint ftHandle, char usDivisor);

        public static void Start()
        {
            _handle = 0;
            Status = FT_Open(0, ref _handle);
        }

        public static void Stop()
        {
            Status = FT_Close(_handle);
            Status = FT_ResetDevice(_handle);
        }

        /// <summary>
        /// Set a channel to a value, follow by a write/send the data (Or time it)
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="value"></param>
        public static void SetDmxValue(int channel, byte value)
        {
            if (Buffer != null)
            {
                Buffer[channel] = value;
            }
        }

        /// <summary>
        /// Writes the DMX data - remember time between each write (25ms is optimal)
        /// </summary>
        public static void WriteData()
        {
            try
            {
                InitOpenDmx();
                if (Status != FtStatus.FT_OK) return;
                
                Status = FT_SetBreakOn(_handle);
                Status = FT_SetBreakOff(_handle);
                Write(_handle, Buffer, Buffer.Length);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
            }
        }

        private static void Write(uint handle, byte[] data, int length)
        {
            try
            {
                IntPtr ptr = Marshal.AllocHGlobal(length);
                Marshal.Copy(data, 0, ptr, length);
                uint bytesWritten = 0;
                Status = FT_Write(handle, ptr, (uint)length, ref bytesWritten);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
            }
        }

        private static void InitOpenDmx()
        {
            Status = FT_ResetDevice(_handle);
            Status = FT_SetDivisor(_handle, (char)12); // set baud rate
            Status = FT_SetDataCharacteristics(_handle, Bits8, StopBits2, ParityNone);
            Status = FT_SetFlowControl(_handle, (char)FlowNone, 0, 0);
            Status = FT_ClrRts(_handle);
            Status = FT_Purge(_handle, PurgeTx);
            Status = FT_Purge(_handle, PurgeRx);
        }
    }

    /// <summary>
    /// Enumeration containing the various return status for the DLL functions.
    /// </summary>
    public enum FtStatus
    {
        FT_OK = 0,
        FT_INVALID_HANDLE,
        FT_DEVICE_NOT_FOUND,
        FT_DEVICE_NOT_OPENED,
        FT_IO_ERROR,
        FT_INSUFFICIENT_RESOURCES,
        FT_INVALID_PARAMETER,
        FT_INVALID_BAUD_RATE,
        FT_DEVICE_NOT_OPENED_FOR_ERASE,
        FT_DEVICE_NOT_OPENED_FOR_WRITE,
        FT_FAILED_TO_WRITE_DEVICE,
        FT_EEPROM_READ_FAILED,
        FT_EEPROM_WRITE_FAILED,
        FT_EEPROM_ERASE_FAILED,
        FT_EEPROM_NOT_PRESENT,
        FT_EEPROM_NOT_PROGRAMMED,
        FT_INVALID_ARGS,
        FT_OTHER_ERROR
    };
}