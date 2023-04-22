// Decompiled with JetBrains decompiler
// Type: FTD2XX_NET.FTDI
// Assembly: FTD2XX_NET, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2388D53C-FCC8-4BD0-9888-5148D47835F7
// Assembly location: C:\Users\willi\RiderProjects\Csharp-Data-Visualization\projects\audio\AudioMonitor\FTD2XX_NET.dll
// XML documentation location: C:\Users\willi\RiderProjects\Csharp-Data-Visualization\projects\audio\AudioMonitor\FTD2XX_NET.xml

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;


namespace FTD2XX_NET
{
  /// <summary>Class wrapper for FTD2XX.DLL</summary>
  [SuppressMessage("ReSharper", "LocalizableElement")]
  public class FTDI
  {
    private const uint FT_OPEN_BY_SERIAL_NUMBER = 1;
    private const uint FT_OPEN_BY_DESCRIPTION = 2;
    private const uint FT_OPEN_BY_LOCATION = 4;
    private const uint FT_DEFAULT_BAUD_RATE = 9600;
    private const uint FT_DEFAULT_DEADMAN_TIMEOUT = 5000;
    private const int FT_COM_PORT_NOT_ASSIGNED = -1;
    private const uint FT_DEFAULT_IN_TRANSFER_SIZE = 4096;
    private const uint FT_DEFAULT_OUT_TRANSFER_SIZE = 4096;
    private const byte FT_DEFAULT_LATENCY = 16;
    private const uint FT_DEFAULT_DEVICE_ID = 67330049;
    private IntPtr ftHandle = IntPtr.Zero;
    private IntPtr hFTD2XXDLL = IntPtr.Zero;
    private IntPtr pFT_CreateDeviceInfoList = IntPtr.Zero;
    private IntPtr pFT_GetDeviceInfoDetail = IntPtr.Zero;
    private IntPtr pFT_Open = IntPtr.Zero;
    private IntPtr pFT_OpenEx = IntPtr.Zero;
    private IntPtr pFT_Close = IntPtr.Zero;
    private IntPtr pFT_Read = IntPtr.Zero;
    private IntPtr pFT_Write = IntPtr.Zero;
    private IntPtr pFT_GetQueueStatus = IntPtr.Zero;
    private IntPtr pFT_GetModemStatus = IntPtr.Zero;
    private IntPtr pFT_GetStatus = IntPtr.Zero;
    private IntPtr pFT_SetBaudRate = IntPtr.Zero;
    private IntPtr pFT_SetDataCharacteristics = IntPtr.Zero;
    private IntPtr pFT_SetFlowControl = IntPtr.Zero;
    private IntPtr pFT_SetDtr = IntPtr.Zero;
    private IntPtr pFT_ClrDtr = IntPtr.Zero;
    private IntPtr pFT_SetRts = IntPtr.Zero;
    private IntPtr pFT_ClrRts = IntPtr.Zero;
    private IntPtr pFT_ResetDevice = IntPtr.Zero;
    private IntPtr pFT_ResetPort = IntPtr.Zero;
    private IntPtr pFT_CyclePort = IntPtr.Zero;
    private IntPtr pFT_Rescan = IntPtr.Zero;
    private IntPtr pFT_Reload = IntPtr.Zero;
    private IntPtr pFT_Purge = IntPtr.Zero;
    private IntPtr pFT_SetTimeouts = IntPtr.Zero;
    private IntPtr pFT_SetBreakOn = IntPtr.Zero;
    private IntPtr pFT_SetBreakOff = IntPtr.Zero;
    private IntPtr pFT_GetDeviceInfo = IntPtr.Zero;
    private IntPtr pFT_SetResetPipeRetryCount = IntPtr.Zero;
    private IntPtr pFT_StopInTask = IntPtr.Zero;
    private IntPtr pFT_RestartInTask = IntPtr.Zero;
    private IntPtr pFT_GetDriverVersion = IntPtr.Zero;
    private IntPtr pFT_GetLibraryVersion = IntPtr.Zero;
    private IntPtr pFT_SetDeadmanTimeout = IntPtr.Zero;
    private IntPtr pFT_SetChars = IntPtr.Zero;
    private IntPtr pFT_SetEventNotification = IntPtr.Zero;
    private IntPtr pFT_GetComPortNumber = IntPtr.Zero;
    private IntPtr pFT_SetLatencyTimer = IntPtr.Zero;
    private IntPtr pFT_GetLatencyTimer = IntPtr.Zero;
    private IntPtr pFT_SetBitMode = IntPtr.Zero;
    private IntPtr pFT_GetBitMode = IntPtr.Zero;
    private IntPtr pFT_SetUSBParameters = IntPtr.Zero;
    private IntPtr pFT_ReadEE = IntPtr.Zero;
    private IntPtr pFT_WriteEE = IntPtr.Zero;
    private IntPtr pFT_EraseEE = IntPtr.Zero;
    private IntPtr pFT_EE_UASize = IntPtr.Zero;
    private IntPtr pFT_EE_UARead = IntPtr.Zero;
    private IntPtr pFT_EE_UAWrite = IntPtr.Zero;
    private IntPtr pFT_EE_Read = IntPtr.Zero;
    private IntPtr pFT_EE_Program = IntPtr.Zero;
    private IntPtr pFT_EEPROM_Read = IntPtr.Zero;
    private IntPtr pFT_EEPROM_Program = IntPtr.Zero;
    private IntPtr pFT_VendorCmdGet = IntPtr.Zero;
    private IntPtr pFT_VendorCmdSet = IntPtr.Zero;

    /// <summary>Constructor for the FTDI class.</summary>
    public FTDI()
    {
      if (hFTD2XXDLL == IntPtr.Zero)
      {
        hFTD2XXDLL = LoadLibrary("FTD2XX.DLL");
        if (hFTD2XXDLL == IntPtr.Zero)
        {
          Console.WriteLine("Attempting to load FTD2XX.DLL from:\n" + Path.GetDirectoryName(GetType().Assembly.Location));
          hFTD2XXDLL = LoadLibrary(Path.GetDirectoryName(GetType().Assembly.Location) + "\\FTD2XX.DLL");
        }
      }
      if (hFTD2XXDLL != IntPtr.Zero)
        FindFunctionPointers();
      else
        Console.WriteLine("Failed to load FTD2XX.DLL.  Are the FTDI drivers installed?");
    }

    /// <summary>
    /// Non default constructor allowing passing of string for dll handle.
    /// </summary>
    public FTDI(string path)
    {
      if (path == "")
        return;
      if (hFTD2XXDLL == IntPtr.Zero)
      {
        hFTD2XXDLL = LoadLibrary(path);
        if (hFTD2XXDLL == IntPtr.Zero)
          Console.WriteLine("Attempting to load FTD2XX.DLL from:\n" + Path.GetDirectoryName(GetType().Assembly.Location));
      }
      if (hFTD2XXDLL != IntPtr.Zero)
        FindFunctionPointers();
      else
        Console.WriteLine("Failed to load FTD2XX.DLL.  Are the FTDI drivers installed?");
    }

    private void FindFunctionPointers()
    {
      pFT_CreateDeviceInfoList = GetProcAddress(hFTD2XXDLL, "FT_CreateDeviceInfoList");
      pFT_GetDeviceInfoDetail = GetProcAddress(hFTD2XXDLL, "FT_GetDeviceInfoDetail");
      pFT_Open = GetProcAddress(hFTD2XXDLL, "FT_Open");
      pFT_OpenEx = GetProcAddress(hFTD2XXDLL, "FT_OpenEx");
      pFT_Close = GetProcAddress(hFTD2XXDLL, "FT_Close");
      pFT_Read = GetProcAddress(hFTD2XXDLL, "FT_Read");
      pFT_Write = GetProcAddress(hFTD2XXDLL, "FT_Write");
      pFT_GetQueueStatus = GetProcAddress(hFTD2XXDLL, "FT_GetQueueStatus");
      pFT_GetModemStatus = GetProcAddress(hFTD2XXDLL, "FT_GetModemStatus");
      pFT_GetStatus = GetProcAddress(hFTD2XXDLL, "FT_GetStatus");
      pFT_SetBaudRate = GetProcAddress(hFTD2XXDLL, "FT_SetBaudRate");
      pFT_SetDataCharacteristics = GetProcAddress(hFTD2XXDLL, "FT_SetDataCharacteristics");
      pFT_SetFlowControl = GetProcAddress(hFTD2XXDLL, "FT_SetFlowControl");
      pFT_SetDtr = GetProcAddress(hFTD2XXDLL, "FT_SetDtr");
      pFT_ClrDtr = GetProcAddress(hFTD2XXDLL, "FT_ClrDtr");
      pFT_SetRts = GetProcAddress(hFTD2XXDLL, "FT_SetRts");
      pFT_ClrRts = GetProcAddress(hFTD2XXDLL, "FT_ClrRts");
      pFT_ResetDevice = GetProcAddress(hFTD2XXDLL, "FT_ResetDevice");
      pFT_ResetPort = GetProcAddress(hFTD2XXDLL, "FT_ResetPort");
      pFT_CyclePort = GetProcAddress(hFTD2XXDLL, "FT_CyclePort");
      pFT_Rescan = GetProcAddress(hFTD2XXDLL, "FT_Rescan");
      pFT_Reload = GetProcAddress(hFTD2XXDLL, "FT_Reload");
      pFT_Purge = GetProcAddress(hFTD2XXDLL, "FT_Purge");
      pFT_SetTimeouts = GetProcAddress(hFTD2XXDLL, "FT_SetTimeouts");
      pFT_SetBreakOn = GetProcAddress(hFTD2XXDLL, "FT_SetBreakOn");
      pFT_SetBreakOff = GetProcAddress(hFTD2XXDLL, "FT_SetBreakOff");
      pFT_GetDeviceInfo = GetProcAddress(hFTD2XXDLL, "FT_GetDeviceInfo");
      pFT_SetResetPipeRetryCount = GetProcAddress(hFTD2XXDLL, "FT_SetResetPipeRetryCount");
      pFT_StopInTask = GetProcAddress(hFTD2XXDLL, "FT_StopInTask");
      pFT_RestartInTask = GetProcAddress(hFTD2XXDLL, "FT_RestartInTask");
      pFT_GetDriverVersion = GetProcAddress(hFTD2XXDLL, "FT_GetDriverVersion");
      pFT_GetLibraryVersion = GetProcAddress(hFTD2XXDLL, "FT_GetLibraryVersion");
      pFT_SetDeadmanTimeout = GetProcAddress(hFTD2XXDLL, "FT_SetDeadmanTimeout");
      pFT_SetChars = GetProcAddress(hFTD2XXDLL, "FT_SetChars");
      pFT_SetEventNotification = GetProcAddress(hFTD2XXDLL, "FT_SetEventNotification");
      pFT_GetComPortNumber = GetProcAddress(hFTD2XXDLL, "FT_GetComPortNumber");
      pFT_SetLatencyTimer = GetProcAddress(hFTD2XXDLL, "FT_SetLatencyTimer");
      pFT_GetLatencyTimer = GetProcAddress(hFTD2XXDLL, "FT_GetLatencyTimer");
      pFT_SetBitMode = GetProcAddress(hFTD2XXDLL, "FT_SetBitMode");
      pFT_GetBitMode = GetProcAddress(hFTD2XXDLL, "FT_GetBitMode");
      pFT_SetUSBParameters = GetProcAddress(hFTD2XXDLL, "FT_SetUSBParameters");
      pFT_ReadEE = GetProcAddress(hFTD2XXDLL, "FT_ReadEE");
      pFT_WriteEE = GetProcAddress(hFTD2XXDLL, "FT_WriteEE");
      pFT_EraseEE = GetProcAddress(hFTD2XXDLL, "FT_EraseEE");
      pFT_EE_UASize = GetProcAddress(hFTD2XXDLL, "FT_EE_UASize");
      pFT_EE_UARead = GetProcAddress(hFTD2XXDLL, "FT_EE_UARead");
      pFT_EE_UAWrite = GetProcAddress(hFTD2XXDLL, "FT_EE_UAWrite");
      pFT_EE_Read = GetProcAddress(hFTD2XXDLL, "FT_EE_Read");
      pFT_EE_Program = GetProcAddress(hFTD2XXDLL, "FT_EE_Program");
      pFT_EEPROM_Read = GetProcAddress(hFTD2XXDLL, "FT_EEPROM_Read");
      pFT_EEPROM_Program = GetProcAddress(hFTD2XXDLL, "FT_EEPROM_Program");
      pFT_VendorCmdGet = GetProcAddress(hFTD2XXDLL, "FT_VendorCmdGet");
      pFT_VendorCmdSet = GetProcAddress(hFTD2XXDLL, "FT_VendorCmdSet");
    }

    /// <summary>Destructor for the FTDI class.</summary>
    ~FTDI()
    {
      FreeLibrary(hFTD2XXDLL);
      hFTD2XXDLL = IntPtr.Zero;
    }

    /// <summary>
    /// Built-in Windows API functions to allow us to dynamically load our own DLL.
    /// Will allow us to use old versions of the DLL that do not have all of these functions available.
    /// </summary>
    [DllImport("kernel32.dll")]
    private static extern IntPtr LoadLibrary(string dllToLoad);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

    [DllImport("kernel32.dll")]
    private static extern bool FreeLibrary(IntPtr hModule);

    /// <summary>Gets the number of FTDI devices available.</summary>
    /// <returns>FT_STATUS value from FT_CreateDeviceInfoList in FTD2XX.DLL</returns>
    /// <param name="devcount">The number of FTDI devices available.</param>
    public FT_STATUS GetNumberOfDevices(ref uint devcount)
    {
      var numberOfDevices = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return numberOfDevices;
      if (pFT_CreateDeviceInfoList != IntPtr.Zero)
        numberOfDevices = ((tFT_CreateDeviceInfoList) Marshal.GetDelegateForFunctionPointer(pFT_CreateDeviceInfoList, typeof (tFT_CreateDeviceInfoList)))(ref devcount);
      else
        Console.WriteLine("Failed to load function FT_CreateDeviceInfoList.");
      return numberOfDevices;
    }

    /// <summary>
    /// Gets information on all of the FTDI devices available.
    /// </summary>
    /// <returns>FT_STATUS value from FT_GetDeviceInfoDetail in FTD2XX.DLL</returns>
    /// <param name="devicelist">An array of type FT_DEVICE_INFO_NODE to contain the device information for all available devices.</param>
    /// <exception cref="T:FTD2XX_NET.FTDI.FT_EXCEPTION">Thrown when the supplied buffer is not large enough to contain the device info list.</exception>
    public FT_STATUS GetDeviceList(List<FT_DEVICE_INFO_NODE> devicelist)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_CreateDeviceInfoList != IntPtr.Zero & pFT_GetDeviceInfoDetail != IntPtr.Zero)
      {
        uint numdevs = 0;
        var forFunctionPointer1 = (tFT_CreateDeviceInfoList) Marshal.GetDelegateForFunctionPointer(pFT_CreateDeviceInfoList, typeof (tFT_CreateDeviceInfoList));
        var forFunctionPointer2 = (tFT_GetDeviceInfoDetail) Marshal.GetDelegateForFunctionPointer(pFT_GetDeviceInfoDetail, typeof (tFT_GetDeviceInfoDetail));
        ftStatus = forFunctionPointer1(ref numdevs);
        var numArray1 = new byte[16];
        var numArray2 = new byte[64];
        if (numdevs > 0U)
        {
          
          for (uint index = 0; index < numdevs; ++index)
          {
            var device = new FT_DEVICE_INFO_NODE();
            ftStatus = forFunctionPointer2(index, ref device.Flags, ref device.Type, ref device.ID, ref device.LocId, numArray1, numArray2, ref device.ftHandle);
            var firstNullIndex = Array.FindIndex(numArray1, b => b == 0);
            device.SerialNumber = Encoding.Default.GetString(numArray1, 0, firstNullIndex);
            /*device.SerialNumber = Encoding.ASCII.GetString(numArray1);
            var arr = device.SerialNumber.ToCharArray();

            arr = Array.FindAll<char>(arr, (c => (char.IsLetterOrDigit(c) 
                                                  || char.IsWhiteSpace(c) 
                                                  || c == '-')));
            device.SerialNumber = new string(arr);*/
            
            firstNullIndex = Array.FindIndex(numArray2, b => b == 0);
            device.Description = Encoding.Default.GetString(numArray2, 0, firstNullIndex);
            /*device.Description = Encoding.ASCII.GetString(numArray2);
            arr = device.Description.ToCharArray();

            arr = Array.FindAll<char>(arr, (c => (char.IsLetterOrDigit(c) 
                                                  || char.IsWhiteSpace(c) 
                                                  || c == '-')));
            device.Description = new string(arr);*/
            devicelist.Add(device);
          }
        }
      }
      else
      {
        if (pFT_CreateDeviceInfoList == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_CreateDeviceInfoList.");
        if (pFT_GetDeviceInfoDetail == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_GetDeviceInfoListDetail.");
      }
      return ftStatus;
    }

    /// <summary>Opens the FTDI device with the specified index.</summary>
    /// <returns>FT_STATUS value from FT_Open in FTD2XX.DLL</returns>
    /// <param name="index">Index of the device to open.
    /// Note that this cannot be guaranteed to open a specific device.</param>
    /// <remarks>Initialises the device to 8 data bits, 1 stop bit, no parity, no flow control and 9600 Baud.</remarks>
    public FT_STATUS OpenByIndex(uint index)
    {
      var ftStatus1 = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus1;
      if (pFT_Open != IntPtr.Zero & pFT_SetDataCharacteristics != IntPtr.Zero & pFT_SetFlowControl != IntPtr.Zero & pFT_SetBaudRate != IntPtr.Zero)
      {
        var forFunctionPointer1 = (tFT_Open) Marshal.GetDelegateForFunctionPointer(pFT_Open, typeof (tFT_Open));
        var forFunctionPointer2 = (tFT_SetDataCharacteristics) Marshal.GetDelegateForFunctionPointer(pFT_SetDataCharacteristics, typeof (tFT_SetDataCharacteristics));
        var forFunctionPointer3 = (tFT_SetFlowControl) Marshal.GetDelegateForFunctionPointer(pFT_SetFlowControl, typeof (tFT_SetFlowControl));
        var forFunctionPointer4 = (tFT_SetBaudRate) Marshal.GetDelegateForFunctionPointer(pFT_SetBaudRate, typeof (tFT_SetBaudRate));
        ftStatus1 = forFunctionPointer1(index, ref ftHandle);
        if (ftStatus1 != FT_STATUS.FT_OK)
          ftHandle = IntPtr.Zero;
        if (ftHandle != IntPtr.Zero)
        {
          byte uWordLength = 8;
          byte uStopBits = 0;
          byte uParity = 0;
          var ftStatus2 = forFunctionPointer2(ftHandle, uWordLength, uStopBits, uParity);
          ushort usFlowControl = 0;
          byte uXon = 17;
          byte uXoff = 19;
          ftStatus2 = forFunctionPointer3(ftHandle, usFlowControl, uXon, uXoff);
          uint dwBaudRate = 9600;
          ftStatus1 = forFunctionPointer4(ftHandle, dwBaudRate);
        }
      }
      else
      {
        if (pFT_Open == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_Open.");
        if (pFT_SetDataCharacteristics == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_SetDataCharacteristics.");
        if (pFT_SetFlowControl == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_SetFlowControl.");
        if (pFT_SetBaudRate == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_SetBaudRate.");
      }
      return ftStatus1;
    }

    /// <summary>
    /// Opens the FTDI device with the specified serial number.
    /// </summary>
    /// <returns>FT_STATUS value from FT_OpenEx in FTD2XX.DLL</returns>
    /// <param name="serialnumber">Serial number of the device to open.</param>
    /// <remarks>Initialises the device to 8 data bits, 1 stop bit, no parity, no flow control and 9600 Baud.</remarks>
    public FT_STATUS OpenBySerialNumber(string serialnumber)
    {
      var ftStatus1 = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus1;
      if (pFT_OpenEx != IntPtr.Zero & pFT_SetDataCharacteristics != IntPtr.Zero & pFT_SetFlowControl != IntPtr.Zero & pFT_SetBaudRate != IntPtr.Zero)
      {
        var forFunctionPointer1 = (tFT_OpenEx) Marshal.GetDelegateForFunctionPointer(pFT_OpenEx, typeof (tFT_OpenEx));
        var forFunctionPointer2 = (tFT_SetDataCharacteristics) Marshal.GetDelegateForFunctionPointer(pFT_SetDataCharacteristics, typeof (tFT_SetDataCharacteristics));
        var forFunctionPointer3 = (tFT_SetFlowControl) Marshal.GetDelegateForFunctionPointer(pFT_SetFlowControl, typeof (tFT_SetFlowControl));
        var forFunctionPointer4 = (tFT_SetBaudRate) Marshal.GetDelegateForFunctionPointer(pFT_SetBaudRate, typeof (tFT_SetBaudRate));
        ftStatus1 = forFunctionPointer1(serialnumber, 1U, ref ftHandle);
        if (ftStatus1 != FT_STATUS.FT_OK)
          ftHandle = IntPtr.Zero;
        if (ftHandle != IntPtr.Zero)
        {
          byte uWordLength = 8;
          byte uStopBits = 0;
          byte uParity = 0;
          var ftStatus2 = forFunctionPointer2(ftHandle, uWordLength, uStopBits, uParity);
          ushort usFlowControl = 0;
          byte uXon = 17;
          byte uXoff = 19;
          ftStatus2 = forFunctionPointer3(ftHandle, usFlowControl, uXon, uXoff);
          uint dwBaudRate = 9600;
          ftStatus1 = forFunctionPointer4(ftHandle, dwBaudRate);
        }
      }
      else
      {
        if (pFT_OpenEx == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_OpenEx.");
        if (pFT_SetDataCharacteristics == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_SetDataCharacteristics.");
        if (pFT_SetFlowControl == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_SetFlowControl.");
        if (pFT_SetBaudRate == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_SetBaudRate.");
      }
      return ftStatus1;
    }

    /// <summary>
    /// Opens the FTDI device with the specified description.
    /// </summary>
    /// <returns>FT_STATUS value from FT_OpenEx in FTD2XX.DLL</returns>
    /// <param name="description">Description of the device to open.</param>
    /// <remarks>Initialises the device to 8 data bits, 1 stop bit, no parity, no flow control and 9600 Baud.</remarks>
    public FT_STATUS OpenByDescription(string description)
    {
      var ftStatus1 = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus1;
      if (pFT_OpenEx != IntPtr.Zero & pFT_SetDataCharacteristics != IntPtr.Zero & pFT_SetFlowControl != IntPtr.Zero & pFT_SetBaudRate != IntPtr.Zero)
      {
        var forFunctionPointer1 = (tFT_OpenEx) Marshal.GetDelegateForFunctionPointer(pFT_OpenEx, typeof (tFT_OpenEx));
        var forFunctionPointer2 = (tFT_SetDataCharacteristics) Marshal.GetDelegateForFunctionPointer(pFT_SetDataCharacteristics, typeof (tFT_SetDataCharacteristics));
        var forFunctionPointer3 = (tFT_SetFlowControl) Marshal.GetDelegateForFunctionPointer(pFT_SetFlowControl, typeof (tFT_SetFlowControl));
        var forFunctionPointer4 = (tFT_SetBaudRate) Marshal.GetDelegateForFunctionPointer(pFT_SetBaudRate, typeof (tFT_SetBaudRate));
        ftStatus1 = forFunctionPointer1(description, 2U, ref ftHandle);
        if (ftStatus1 != FT_STATUS.FT_OK)
          ftHandle = IntPtr.Zero;
        if (ftHandle != IntPtr.Zero)
        {
          byte uWordLength = 8;
          byte uStopBits = 0;
          byte uParity = 0;
          var ftStatus2 = forFunctionPointer2(ftHandle, uWordLength, uStopBits, uParity);
          ushort usFlowControl = 0;
          byte uXon = 17;
          byte uXoff = 19;
          ftStatus2 = forFunctionPointer3(ftHandle, usFlowControl, uXon, uXoff);
          uint dwBaudRate = 9600;
          ftStatus1 = forFunctionPointer4(ftHandle, dwBaudRate);
        }
      }
      else
      {
        if (pFT_OpenEx == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_OpenEx.");
        if (pFT_SetDataCharacteristics == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_SetDataCharacteristics.");
        if (pFT_SetFlowControl == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_SetFlowControl.");
        if (pFT_SetBaudRate == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_SetBaudRate.");
      }
      return ftStatus1;
    }

    /// <summary>
    /// Opens the FTDI device at the specified physical location.
    /// </summary>
    /// <returns>FT_STATUS value from FT_OpenEx in FTD2XX.DLL</returns>
    /// <param name="location">Location of the device to open.</param>
    /// <remarks>Initialises the device to 8 data bits, 1 stop bit, no parity, no flow control and 9600 Baud.</remarks>
    public FT_STATUS OpenByLocation(uint location)
    {
      var ftStatus1 = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus1;
      if (pFT_OpenEx != IntPtr.Zero & pFT_SetDataCharacteristics != IntPtr.Zero & pFT_SetFlowControl != IntPtr.Zero & pFT_SetBaudRate != IntPtr.Zero)
      {
        var forFunctionPointer1 = (tFT_OpenExLoc) Marshal.GetDelegateForFunctionPointer(pFT_OpenEx, typeof (tFT_OpenExLoc));
        var forFunctionPointer2 = (tFT_SetDataCharacteristics) Marshal.GetDelegateForFunctionPointer(pFT_SetDataCharacteristics, typeof (tFT_SetDataCharacteristics));
        var forFunctionPointer3 = (tFT_SetFlowControl) Marshal.GetDelegateForFunctionPointer(pFT_SetFlowControl, typeof (tFT_SetFlowControl));
        var forFunctionPointer4 = (tFT_SetBaudRate) Marshal.GetDelegateForFunctionPointer(pFT_SetBaudRate, typeof (tFT_SetBaudRate));
        ftStatus1 = forFunctionPointer1(location, 4U, ref ftHandle);
        if (ftStatus1 != FT_STATUS.FT_OK)
          ftHandle = IntPtr.Zero;
        if (ftHandle != IntPtr.Zero)
        {
          byte uWordLength = 8;
          byte uStopBits = 0;
          byte uParity = 0;
          var ftStatus2 = forFunctionPointer2(ftHandle, uWordLength, uStopBits, uParity);
          ushort usFlowControl = 0;
          byte uXon = 17;
          byte uXoff = 19;
          ftStatus2 = forFunctionPointer3(ftHandle, usFlowControl, uXon, uXoff);
          uint dwBaudRate = 9600;
          ftStatus1 = forFunctionPointer4(ftHandle, dwBaudRate);
        }
      }
      else
      {
        if (pFT_OpenEx == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_OpenEx.");
        if (pFT_SetDataCharacteristics == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_SetDataCharacteristics.");
        if (pFT_SetFlowControl == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_SetFlowControl.");
        if (pFT_SetBaudRate == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_SetBaudRate.");
      }
      return ftStatus1;
    }

    /// <summary>Closes the handle to an open FTDI device.</summary>
    /// <returns>FT_STATUS value from FT_Close in FTD2XX.DLL</returns>
    public FT_STATUS Close()
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_Close != IntPtr.Zero)
      {
        ftStatus = ((tFT_Close) Marshal.GetDelegateForFunctionPointer(pFT_Close, typeof (tFT_Close)))(ftHandle);
        if (ftStatus == FT_STATUS.FT_OK)
          ftHandle = IntPtr.Zero;
      }
      else if (pFT_Close == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_Close.");
      return ftStatus;
    }

    /// <summary>Read data from an open FTDI device.</summary>
    /// <returns>FT_STATUS value from FT_Read in FTD2XX.DLL</returns>
    /// <param name="dataBuffer">An array of bytes which will be populated with the data read from the device.</param>
    /// <param name="numBytesToRead">The number of bytes requested from the device.</param>
    /// <param name="numBytesRead">The number of bytes actually read.</param>
    public FT_STATUS Read(byte[] dataBuffer, uint numBytesToRead, ref uint numBytesRead)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_Read != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_Read) Marshal.GetDelegateForFunctionPointer(pFT_Read, typeof (tFT_Read));
        if (dataBuffer.Length < numBytesToRead)
          numBytesToRead = (uint) dataBuffer.Length;
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle, dataBuffer, numBytesToRead, ref numBytesRead);
      }
      else if (pFT_Read == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_Read.");
      return ftStatus;
    }

    /// <summary>Read data from an open FTDI device.</summary>
    /// <returns>FT_STATUS value from FT_Read in FTD2XX.DLL</returns>
    /// <param name="dataBuffer">A string containing the data read</param>
    /// <param name="numBytesToRead">The number of bytes requested from the device.</param>
    /// <param name="numBytesRead">The number of bytes actually read.</param>
    public FT_STATUS Read(out string dataBuffer, uint numBytesToRead, ref uint numBytesRead)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      dataBuffer = string.Empty;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_Read != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_Read) Marshal.GetDelegateForFunctionPointer(pFT_Read, typeof (tFT_Read));
        var numArray = new byte[numBytesToRead];
        if (ftHandle != IntPtr.Zero)
        {
          ftStatus = forFunctionPointer(ftHandle, numArray, numBytesToRead, ref numBytesRead);
          dataBuffer = Encoding.ASCII.GetString(numArray);
          dataBuffer = dataBuffer.Substring(0, (int) numBytesRead);
        }
      }
      else if (pFT_Read == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_Read.");
      return ftStatus;
    }

    /// <summary>Write data to an open FTDI device.</summary>
    /// <returns>FT_STATUS value from FT_Write in FTD2XX.DLL</returns>
    /// <param name="dataBuffer">An array of bytes which contains the data to be written to the device.</param>
    /// <param name="numBytesToWrite">The number of bytes to be written to the device.</param>
    /// <param name="numBytesWritten">The number of bytes actually written to the device.</param>
    public FT_STATUS Write(byte[] dataBuffer, int numBytesToWrite, ref uint numBytesWritten)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_Write != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_Write) Marshal.GetDelegateForFunctionPointer(pFT_Write, typeof (tFT_Write));
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle, dataBuffer, (uint) numBytesToWrite, ref numBytesWritten);
      }
      else if (pFT_Write == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_Write.");
      return ftStatus;
    }

    /// <summary>Write data to an open FTDI device.</summary>
    /// <returns>FT_STATUS value from FT_Write in FTD2XX.DLL</returns>
    /// <param name="dataBuffer">An array of bytes which contains the data to be written to the device.</param>
    /// <param name="numBytesToWrite">The number of bytes to be written to the device.</param>
    /// <param name="numBytesWritten">The number of bytes actually written to the device.</param>
    public FT_STATUS Write(byte[] dataBuffer, uint numBytesToWrite, ref uint numBytesWritten)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_Write != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_Write) Marshal.GetDelegateForFunctionPointer(pFT_Write, typeof (tFT_Write));
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle, dataBuffer, numBytesToWrite, ref numBytesWritten);
      }
      else if (pFT_Write == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_Write.");
      return ftStatus;
    }

    /// <summary>Write data to an open FTDI device.</summary>
    /// <returns>FT_STATUS value from FT_Write in FTD2XX.DLL</returns>
    /// <param name="dataBuffer">A  string which contains the data to be written to the device.</param>
    /// <param name="numBytesToWrite">The number of bytes to be written to the device.</param>
    /// <param name="numBytesWritten">The number of bytes actually written to the device.</param>
    public FT_STATUS Write(string dataBuffer, int numBytesToWrite, ref uint numBytesWritten)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_Write != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_Write) Marshal.GetDelegateForFunctionPointer(pFT_Write, typeof (tFT_Write));
        var bytes = Encoding.ASCII.GetBytes(dataBuffer);
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle, bytes, (uint) numBytesToWrite, ref numBytesWritten);
      }
      else if (pFT_Write == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_Write.");
      return ftStatus;
    }

    /// <summary>Write data to an open FTDI device.</summary>
    /// <returns>FT_STATUS value from FT_Write in FTD2XX.DLL</returns>
    /// <param name="dataBuffer">A  string which contains the data to be written to the device.</param>
    /// <param name="numBytesToWrite">The number of bytes to be written to the device.</param>
    /// <param name="numBytesWritten">The number of bytes actually written to the device.</param>
    public FT_STATUS Write(string dataBuffer, uint numBytesToWrite, ref uint numBytesWritten)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_Write != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_Write) Marshal.GetDelegateForFunctionPointer(pFT_Write, typeof (tFT_Write));
        var bytes = Encoding.ASCII.GetBytes(dataBuffer);
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle, bytes, numBytesToWrite, ref numBytesWritten);
      }
      else if (pFT_Write == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_Write.");
      return ftStatus;
    }

    /// <summary>Reset an open FTDI device.</summary>
    /// <returns>FT_STATUS value from FT_ResetDevice in FTD2XX.DLL</returns>
    public FT_STATUS ResetDevice()
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_ResetDevice != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_ResetDevice) Marshal.GetDelegateForFunctionPointer(pFT_ResetDevice, typeof (tFT_ResetDevice));
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle);
      }
      else if (pFT_ResetDevice == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_ResetDevice.");
      return ftStatus;
    }

    /// <summary>
    /// Purge data from the devices transmit and/or receive buffers.
    /// </summary>
    /// <returns>FT_STATUS value from FT_Purge in FTD2XX.DLL</returns>
    /// <param name="purgemask">Specifies which buffer(s) to be purged.  Valid values are any combination of the following flags: FT_PURGE_RX, FT_PURGE_TX</param>
    public FT_STATUS Purge(uint purgemask)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_Purge != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_Purge) Marshal.GetDelegateForFunctionPointer(pFT_Purge, typeof (tFT_Purge));
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle, purgemask);
      }
      else if (pFT_Purge == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_Purge.");
      return ftStatus;
    }

    /// <summary>Register for event notification.</summary>
    /// <returns>FT_STATUS value from FT_SetEventNotification in FTD2XX.DLL</returns>
    /// <remarks>After setting event notification, the event can be caught by executing the WaitOne() method of the EventWaitHandle.  If multiple event types are being monitored, the event that fired can be determined from the GetEventType method.</remarks>
    /// <param name="eventmask">The type of events to signal.  Can be any combination of the following: FT_EVENT_RXCHAR, FT_EVENT_MODEM_STATUS, FT_EVENT_LINE_STATUS</param>
    /// <param name="eventhandle">Handle to the event that will receive the notification</param>
    public FT_STATUS SetEventNotification(uint eventmask, EventWaitHandle eventhandle)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_SetEventNotification != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_SetEventNotification) Marshal.GetDelegateForFunctionPointer(pFT_SetEventNotification, typeof (tFT_SetEventNotification));
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle, eventmask, eventhandle.SafeWaitHandle);
      }
      else if (pFT_SetEventNotification == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_SetEventNotification.");
      return ftStatus;
    }

    /// <summary>Stops the driver issuing USB in requests.</summary>
    /// <returns>FT_STATUS value from FT_StopInTask in FTD2XX.DLL</returns>
    public FT_STATUS StopInTask()
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_StopInTask != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_StopInTask) Marshal.GetDelegateForFunctionPointer(pFT_StopInTask, typeof (tFT_StopInTask));
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle);
      }
      else if (pFT_StopInTask == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_StopInTask.");
      return ftStatus;
    }

    /// <summary>Resumes the driver issuing USB in requests.</summary>
    /// <returns>FT_STATUS value from FT_RestartInTask in FTD2XX.DLL</returns>
    public FT_STATUS RestartInTask()
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_RestartInTask != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_RestartInTask) Marshal.GetDelegateForFunctionPointer(pFT_RestartInTask, typeof (tFT_RestartInTask));
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle);
      }
      else if (pFT_RestartInTask == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_RestartInTask.");
      return ftStatus;
    }

    /// <summary>Resets the device port.</summary>
    /// <returns>FT_STATUS value from FT_ResetPort in FTD2XX.DLL</returns>
    public FT_STATUS ResetPort()
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_ResetPort != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_ResetPort) Marshal.GetDelegateForFunctionPointer(pFT_ResetPort, typeof (tFT_ResetPort));
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle);
      }
      else if (pFT_ResetPort == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_ResetPort.");
      return ftStatus;
    }

    /// <summary>
    /// Causes the device to be re-enumerated on the USB bus.  This is equivalent to unplugging and replugging the device.
    /// Also calls FT_Close if FT_CyclePort is successful, so no need to call this separately in the application.
    /// </summary>
    /// <returns>FT_STATUS value from FT_CyclePort in FTD2XX.DLL</returns>
    public FT_STATUS CyclePort()
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_CyclePort != IntPtr.Zero & pFT_Close != IntPtr.Zero)
      {
        var forFunctionPointer1 = (tFT_CyclePort) Marshal.GetDelegateForFunctionPointer(pFT_CyclePort, typeof (tFT_CyclePort));
        var forFunctionPointer2 = (tFT_Close) Marshal.GetDelegateForFunctionPointer(pFT_Close, typeof (tFT_Close));
        if (ftHandle != IntPtr.Zero)
        {
          ftStatus = forFunctionPointer1(ftHandle);
          if (ftStatus == FT_STATUS.FT_OK)
          {
            ftStatus = forFunctionPointer2(ftHandle);
            if (ftStatus == FT_STATUS.FT_OK)
              ftHandle = IntPtr.Zero;
          }
        }
      }
      else
      {
        if (pFT_CyclePort == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_CyclePort.");
        if (pFT_Close == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_Close.");
      }
      return ftStatus;
    }

    /// <summary>
    /// Causes the system to check for USB hardware changes.  This is equivalent to clicking on the "Scan for hardware changes" button in the Device Manager.
    /// </summary>
    /// <returns>FT_STATUS value from FT_Rescan in FTD2XX.DLL</returns>
    public FT_STATUS Rescan()
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_Rescan != IntPtr.Zero)
        ftStatus = ((tFT_Rescan) Marshal.GetDelegateForFunctionPointer(pFT_Rescan, typeof (tFT_Rescan)))();
      else if (pFT_Rescan == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_Rescan.");
      return ftStatus;
    }

    /// <summary>
    /// Forces a reload of the driver for devices with a specific VID and PID combination.
    /// </summary>
    /// <returns>FT_STATUS value from FT_Reload in FTD2XX.DLL</returns>
    /// <remarks>If the VID and PID parameters are 0, the drivers for USB root hubs will be reloaded, causing all USB devices connected to reload their drivers</remarks>
    /// <param name="VendorID">Vendor ID of the devices to have the driver reloaded</param>
    /// <param name="ProductID">Product ID of the devices to have the driver reloaded</param>
    public FT_STATUS Reload(ushort VendorID, ushort ProductID)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_Reload != IntPtr.Zero)
        ftStatus = ((tFT_Reload) Marshal.GetDelegateForFunctionPointer(pFT_Reload, typeof (tFT_Reload)))(VendorID, ProductID);
      else if (pFT_Reload == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_Reload.");
      return ftStatus;
    }

    /// <summary>
    /// Puts the device in a mode other than the default UART or FIFO mode.
    /// </summary>
    /// <returns>FT_STATUS value from FT_SetBitMode in FTD2XX.DLL</returns>
    /// <param name="Mask">Sets up which bits are inputs and which are outputs.  A bit value of 0 sets the corresponding pin to an input, a bit value of 1 sets the corresponding pin to an output.
    /// In the case of CBUS Bit Bang, the upper nibble of this value controls which pins are inputs and outputs, while the lower nibble controls which of the outputs are high and low.</param>
    /// <param name="BitMode"> For FT232H devices, valid values are FT_BIT_MODE_RESET, FT_BIT_MODE_ASYNC_BITBANG, FT_BIT_MODE_MPSSE, FT_BIT_MODE_SYNC_BITBANG, FT_BIT_MODE_CBUS_BITBANG, FT_BIT_MODE_MCU_HOST, FT_BIT_MODE_FAST_SERIAL, FT_BIT_MODE_SYNC_FIFO.
    /// For FT2232H devices, valid values are FT_BIT_MODE_RESET, FT_BIT_MODE_ASYNC_BITBANG, FT_BIT_MODE_MPSSE, FT_BIT_MODE_SYNC_BITBANG, FT_BIT_MODE_MCU_HOST, FT_BIT_MODE_FAST_SERIAL, FT_BIT_MODE_SYNC_FIFO.
    /// For FT4232H devices, valid values are FT_BIT_MODE_RESET, FT_BIT_MODE_ASYNC_BITBANG, FT_BIT_MODE_MPSSE, FT_BIT_MODE_SYNC_BITBANG.
    /// For FT232R devices, valid values are FT_BIT_MODE_RESET, FT_BIT_MODE_ASYNC_BITBANG, FT_BIT_MODE_SYNC_BITBANG, FT_BIT_MODE_CBUS_BITBANG.
    /// For FT245R devices, valid values are FT_BIT_MODE_RESET, FT_BIT_MODE_ASYNC_BITBANG, FT_BIT_MODE_SYNC_BITBANG.
    /// For FT2232 devices, valid values are FT_BIT_MODE_RESET, FT_BIT_MODE_ASYNC_BITBANG, FT_BIT_MODE_MPSSE, FT_BIT_MODE_SYNC_BITBANG, FT_BIT_MODE_MCU_HOST, FT_BIT_MODE_FAST_SERIAL.
    /// For FT232B and FT245B devices, valid values are FT_BIT_MODE_RESET, FT_BIT_MODE_ASYNC_BITBANG.</param>
    /// <exception cref="T:FTD2XX_NET.FTDI.FT_EXCEPTION">Thrown when the current device does not support the requested bit mode.</exception>
    public FT_STATUS SetBitMode(byte Mask, byte BitMode)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_SetBitMode != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_SetBitMode) Marshal.GetDelegateForFunctionPointer(pFT_SetBitMode, typeof (tFT_SetBitMode));
        if (ftHandle != IntPtr.Zero)
        {
          var DeviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
          var deviceType = (int) GetDeviceType(ref DeviceType);
          switch (DeviceType)
          {
            case FT_DEVICE.FT_DEVICE_AM:
              var ftErrorCondition1 = FT_ERROR.FT_INVALID_BITMODE;
              ErrorHandler(ftStatus, ftErrorCondition1);
              break;
            case FT_DEVICE.FT_DEVICE_100AX:
              var ftErrorCondition2 = FT_ERROR.FT_INVALID_BITMODE;
              ErrorHandler(ftStatus, ftErrorCondition2);
              break;
            default:
              if (DeviceType == FT_DEVICE.FT_DEVICE_BM && BitMode != 0)
              {
                if ((BitMode & 1) == 0)
                {
                  var ftErrorCondition3 = FT_ERROR.FT_INVALID_BITMODE;
                  ErrorHandler(ftStatus, ftErrorCondition3);
                  break;
                }
                break;
              }
              if (DeviceType == FT_DEVICE.FT_DEVICE_2232 && BitMode != 0)
              {
                if ((BitMode & 31) == 0)
                {
                  var ftErrorCondition4 = FT_ERROR.FT_INVALID_BITMODE;
                  ErrorHandler(ftStatus, ftErrorCondition4);
                }
                if (BitMode == 2 & InterfaceIdentifier != "A")
                {
                  var ftErrorCondition5 = FT_ERROR.FT_INVALID_BITMODE;
                  ErrorHandler(ftStatus, ftErrorCondition5);
                  break;
                }
                break;
              }
              if (DeviceType == FT_DEVICE.FT_DEVICE_232R && BitMode != 0)
              {
                if ((BitMode & 37) == 0)
                {
                  var ftErrorCondition6 = FT_ERROR.FT_INVALID_BITMODE;
                  ErrorHandler(ftStatus, ftErrorCondition6);
                  break;
                }
                break;
              }
              if (DeviceType == FT_DEVICE.FT_DEVICE_2232H && BitMode != 0)
              {
                if ((BitMode & 95) == 0)
                {
                  var ftErrorCondition7 = FT_ERROR.FT_INVALID_BITMODE;
                  ErrorHandler(ftStatus, ftErrorCondition7);
                }
                if ((BitMode == 8 | BitMode == 64) & InterfaceIdentifier != "A")
                {
                  var ftErrorCondition8 = FT_ERROR.FT_INVALID_BITMODE;
                  ErrorHandler(ftStatus, ftErrorCondition8);
                  break;
                }
                break;
              }
              if (DeviceType == FT_DEVICE.FT_DEVICE_4232H && BitMode != 0)
              {
                if ((BitMode & 7) == 0)
                {
                  var ftErrorCondition9 = FT_ERROR.FT_INVALID_BITMODE;
                  ErrorHandler(ftStatus, ftErrorCondition9);
                }
                if (BitMode == 2 & InterfaceIdentifier != "A" & InterfaceIdentifier != "B")
                {
                  var ftErrorCondition10 = FT_ERROR.FT_INVALID_BITMODE;
                  ErrorHandler(ftStatus, ftErrorCondition10);
                  break;
                }
                break;
              }
              if (DeviceType == FT_DEVICE.FT_DEVICE_232H && BitMode != 0 && BitMode > 64)
              {
                var ftErrorCondition11 = FT_ERROR.FT_INVALID_BITMODE;
                ErrorHandler(ftStatus, ftErrorCondition11);
                break;
              }
              break;
          }
          ftStatus = forFunctionPointer(ftHandle, Mask, BitMode);
        }
      }
      else if (pFT_SetBitMode == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_SetBitMode.");
      return ftStatus;
    }

    /// <summary>Gets the instantaneous state of the device IO pins.</summary>
    /// <returns>FT_STATUS value from FT_GetBitMode in FTD2XX.DLL</returns>
    /// <param name="BitMode">A bitmap value containing the instantaneous state of the device IO pins</param>
    public FT_STATUS GetPinStates(ref byte BitMode)
    {
      var pinStates = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return pinStates;
      if (pFT_GetBitMode != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_GetBitMode) Marshal.GetDelegateForFunctionPointer(pFT_GetBitMode, typeof (tFT_GetBitMode));
        if (ftHandle != IntPtr.Zero)
          pinStates = forFunctionPointer(ftHandle, ref BitMode);
      }
      else if (pFT_GetBitMode == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_GetBitMode.");
      return pinStates;
    }

    /// <summary>
    /// Reads an individual word value from a specified location in the device's EEPROM.
    /// </summary>
    /// <returns>FT_STATUS value from FT_ReadEE in FTD2XX.DLL</returns>
    /// <param name="Address">The EEPROM location to read data from</param>
    /// <param name="EEValue">The WORD value read from the EEPROM location specified in the Address paramter</param>
    public FT_STATUS ReadEEPROMLocation(uint Address, ref ushort EEValue)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_ReadEE != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_ReadEE) Marshal.GetDelegateForFunctionPointer(pFT_ReadEE, typeof (tFT_ReadEE));
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle, Address, ref EEValue);
      }
      else if (pFT_ReadEE == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_ReadEE.");
      return ftStatus;
    }

    /// <summary>
    /// Writes an individual word value to a specified location in the device's EEPROM.
    /// </summary>
    /// <returns>FT_STATUS value from FT_WriteEE in FTD2XX.DLL</returns>
    /// <param name="Address">The EEPROM location to read data from</param>
    /// <param name="EEValue">The WORD value to write to the EEPROM location specified by the Address parameter</param>
    public FT_STATUS WriteEEPROMLocation(uint Address, ushort EEValue)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_WriteEE != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_WriteEE) Marshal.GetDelegateForFunctionPointer(pFT_WriteEE, typeof (tFT_WriteEE));
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle, Address, EEValue);
      }
      else if (pFT_WriteEE == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_WriteEE.");
      return ftStatus;
    }

    /// <summary>Erases the device EEPROM.</summary>
    /// <returns>FT_STATUS value from FT_EraseEE in FTD2XX.DLL</returns>
    /// <exception cref="T:FTD2XX_NET.FTDI.FT_EXCEPTION">Thrown when attempting to erase the EEPROM of a device with an internal EEPROM such as an FT232R or FT245R.</exception>
    public FT_STATUS EraseEEPROM()
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_EraseEE != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_EraseEE) Marshal.GetDelegateForFunctionPointer(pFT_EraseEE, typeof (tFT_EraseEE));
        if (ftHandle != IntPtr.Zero)
        {
          var DeviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
          var deviceType = (int) GetDeviceType(ref DeviceType);
          if (DeviceType == FT_DEVICE.FT_DEVICE_232R)
          {
            var ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
            ErrorHandler(ftStatus, ftErrorCondition);
          }
          ftStatus = forFunctionPointer(ftHandle);
        }
      }
      else if (pFT_EraseEE == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_EraseEE.");
      return ftStatus;
    }

    /// <summary>
    /// Reads the EEPROM contents of an FT232B or FT245B device.
    /// </summary>
    /// <returns>FT_STATUS value from FT_EE_Read in FTD2XX DLL</returns>
    /// <param name="ee232b">An FT232B_EEPROM_STRUCTURE which contains only the relevant information for an FT232B and FT245B device.</param>
    /// <exception cref="T:FTD2XX_NET.FTDI.FT_EXCEPTION">Thrown when the current device does not match the type required by this method.</exception>
    public FT_STATUS ReadFT232BEEPROM(FT232B_EEPROM_STRUCTURE ee232b)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_EE_Read != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_EE_Read) Marshal.GetDelegateForFunctionPointer(pFT_EE_Read, typeof (tFT_EE_Read));
        if (ftHandle != IntPtr.Zero)
        {
          var DeviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
          var deviceType = (int) GetDeviceType(ref DeviceType);
          if (DeviceType != FT_DEVICE.FT_DEVICE_BM)
          {
            var ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
            ErrorHandler(ftStatus, ftErrorCondition);
          }
          var pData = new FT_PROGRAM_DATA();
          pData.Signature1 = 0U;
          pData.Signature2 = uint.MaxValue;
          pData.Version = 2U;
          pData.Manufacturer = Marshal.AllocHGlobal(32);
          pData.ManufacturerID = Marshal.AllocHGlobal(16);
          pData.Description = Marshal.AllocHGlobal(64);
          pData.SerialNumber = Marshal.AllocHGlobal(16);
          ftStatus = forFunctionPointer(ftHandle, pData);
          ee232b.Manufacturer = Marshal.PtrToStringAnsi(pData.Manufacturer);
          ee232b.ManufacturerID = Marshal.PtrToStringAnsi(pData.ManufacturerID);
          ee232b.Description = Marshal.PtrToStringAnsi(pData.Description);
          ee232b.SerialNumber = Marshal.PtrToStringAnsi(pData.SerialNumber);
          Marshal.FreeHGlobal(pData.Manufacturer);
          Marshal.FreeHGlobal(pData.ManufacturerID);
          Marshal.FreeHGlobal(pData.Description);
          Marshal.FreeHGlobal(pData.SerialNumber);
          ee232b.VendorID = pData.VendorID;
          ee232b.ProductID = pData.ProductID;
          ee232b.MaxPower = pData.MaxPower;
          ee232b.SelfPowered = Convert.ToBoolean(pData.SelfPowered);
          ee232b.RemoteWakeup = Convert.ToBoolean(pData.RemoteWakeup);
          ee232b.PullDownEnable = Convert.ToBoolean(pData.PullDownEnable);
          ee232b.SerNumEnable = Convert.ToBoolean(pData.SerNumEnable);
          ee232b.USBVersionEnable = Convert.ToBoolean(pData.USBVersionEnable);
          ee232b.USBVersion = pData.USBVersion;
        }
      }
      else if (pFT_EE_Read == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_EE_Read.");
      return ftStatus;
    }

    /// <summary>Reads the EEPROM contents of an FT2232 device.</summary>
    /// <returns>FT_STATUS value from FT_EE_Read in FTD2XX DLL</returns>
    /// <param name="ee2232">An FT2232_EEPROM_STRUCTURE which contains only the relevant information for an FT2232 device.</param>
    /// <exception cref="T:FTD2XX_NET.FTDI.FT_EXCEPTION">Thrown when the current device does not match the type required by this method.</exception>
    public FT_STATUS ReadFT2232EEPROM(FT2232_EEPROM_STRUCTURE ee2232)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_EE_Read != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_EE_Read) Marshal.GetDelegateForFunctionPointer(pFT_EE_Read, typeof (tFT_EE_Read));
        if (ftHandle != IntPtr.Zero)
        {
          var DeviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
          var deviceType = (int) GetDeviceType(ref DeviceType);
          if (DeviceType != FT_DEVICE.FT_DEVICE_2232)
          {
            var ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
            ErrorHandler(ftStatus, ftErrorCondition);
          }
          var pData = new FT_PROGRAM_DATA();
          pData.Signature1 = 0U;
          pData.Signature2 = uint.MaxValue;
          pData.Version = 2U;
          pData.Manufacturer = Marshal.AllocHGlobal(32);
          pData.ManufacturerID = Marshal.AllocHGlobal(16);
          pData.Description = Marshal.AllocHGlobal(64);
          pData.SerialNumber = Marshal.AllocHGlobal(16);
          ftStatus = forFunctionPointer(ftHandle, pData);
          ee2232.Manufacturer = Marshal.PtrToStringAnsi(pData.Manufacturer);
          ee2232.ManufacturerID = Marshal.PtrToStringAnsi(pData.ManufacturerID);
          ee2232.Description = Marshal.PtrToStringAnsi(pData.Description);
          ee2232.SerialNumber = Marshal.PtrToStringAnsi(pData.SerialNumber);
          Marshal.FreeHGlobal(pData.Manufacturer);
          Marshal.FreeHGlobal(pData.ManufacturerID);
          Marshal.FreeHGlobal(pData.Description);
          Marshal.FreeHGlobal(pData.SerialNumber);
          ee2232.VendorID = pData.VendorID;
          ee2232.ProductID = pData.ProductID;
          ee2232.MaxPower = pData.MaxPower;
          ee2232.SelfPowered = Convert.ToBoolean(pData.SelfPowered);
          ee2232.RemoteWakeup = Convert.ToBoolean(pData.RemoteWakeup);
          ee2232.PullDownEnable = Convert.ToBoolean(pData.PullDownEnable5);
          ee2232.SerNumEnable = Convert.ToBoolean(pData.SerNumEnable5);
          ee2232.USBVersionEnable = Convert.ToBoolean(pData.USBVersionEnable5);
          ee2232.USBVersion = pData.USBVersion5;
          ee2232.AIsHighCurrent = Convert.ToBoolean(pData.AIsHighCurrent);
          ee2232.BIsHighCurrent = Convert.ToBoolean(pData.BIsHighCurrent);
          ee2232.IFAIsFifo = Convert.ToBoolean(pData.IFAIsFifo);
          ee2232.IFAIsFifoTar = Convert.ToBoolean(pData.IFAIsFifoTar);
          ee2232.IFAIsFastSer = Convert.ToBoolean(pData.IFAIsFastSer);
          ee2232.AIsVCP = Convert.ToBoolean(pData.AIsVCP);
          ee2232.IFBIsFifo = Convert.ToBoolean(pData.IFBIsFifo);
          ee2232.IFBIsFifoTar = Convert.ToBoolean(pData.IFBIsFifoTar);
          ee2232.IFBIsFastSer = Convert.ToBoolean(pData.IFBIsFastSer);
          ee2232.BIsVCP = Convert.ToBoolean(pData.BIsVCP);
        }
      }
      else if (pFT_EE_Read == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_EE_Read.");
      return ftStatus;
    }

    /// <summary>
    /// Reads the EEPROM contents of an FT232R or FT245R device.
    /// Calls FT_EE_Read in FTD2XX DLL
    /// </summary>
    /// <returns>An FT232R_EEPROM_STRUCTURE which contains only the relevant information for an FT232R and FT245R device.</returns>
    /// <exception cref="T:FTD2XX_NET.FTDI.FT_EXCEPTION">Thrown when the current device does not match the type required by this method.</exception>
    public FT_STATUS ReadFT232REEPROM(FT232R_EEPROM_STRUCTURE ee232r)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_EE_Read != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_EE_Read) Marshal.GetDelegateForFunctionPointer(pFT_EE_Read, typeof (tFT_EE_Read));
        if (ftHandle != IntPtr.Zero)
        {
          var DeviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
          var deviceType = (int) GetDeviceType(ref DeviceType);
          if (DeviceType != FT_DEVICE.FT_DEVICE_232R)
          {
            var ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
            ErrorHandler(ftStatus, ftErrorCondition);
          }
          var pData = new FT_PROGRAM_DATA();
          pData.Signature1 = 0U;
          pData.Signature2 = uint.MaxValue;
          pData.Version = 2U;
          pData.Manufacturer = Marshal.AllocHGlobal(32);
          pData.ManufacturerID = Marshal.AllocHGlobal(16);
          pData.Description = Marshal.AllocHGlobal(64);
          pData.SerialNumber = Marshal.AllocHGlobal(16);
          ftStatus = forFunctionPointer(ftHandle, pData);
          ee232r.Manufacturer = Marshal.PtrToStringAnsi(pData.Manufacturer);
          ee232r.ManufacturerID = Marshal.PtrToStringAnsi(pData.ManufacturerID);
          ee232r.Description = Marshal.PtrToStringAnsi(pData.Description);
          ee232r.SerialNumber = Marshal.PtrToStringAnsi(pData.SerialNumber);
          Marshal.FreeHGlobal(pData.Manufacturer);
          Marshal.FreeHGlobal(pData.ManufacturerID);
          Marshal.FreeHGlobal(pData.Description);
          Marshal.FreeHGlobal(pData.SerialNumber);
          ee232r.VendorID = pData.VendorID;
          ee232r.ProductID = pData.ProductID;
          ee232r.MaxPower = pData.MaxPower;
          ee232r.SelfPowered = Convert.ToBoolean(pData.SelfPowered);
          ee232r.RemoteWakeup = Convert.ToBoolean(pData.RemoteWakeup);
          ee232r.UseExtOsc = Convert.ToBoolean(pData.UseExtOsc);
          ee232r.HighDriveIOs = Convert.ToBoolean(pData.HighDriveIOs);
          ee232r.EndpointSize = pData.EndpointSize;
          ee232r.PullDownEnable = Convert.ToBoolean(pData.PullDownEnableR);
          ee232r.SerNumEnable = Convert.ToBoolean(pData.SerNumEnableR);
          ee232r.InvertTXD = Convert.ToBoolean(pData.InvertTXD);
          ee232r.InvertRXD = Convert.ToBoolean(pData.InvertRXD);
          ee232r.InvertRTS = Convert.ToBoolean(pData.InvertRTS);
          ee232r.InvertCTS = Convert.ToBoolean(pData.InvertCTS);
          ee232r.InvertDTR = Convert.ToBoolean(pData.InvertDTR);
          ee232r.InvertDSR = Convert.ToBoolean(pData.InvertDSR);
          ee232r.InvertDCD = Convert.ToBoolean(pData.InvertDCD);
          ee232r.InvertRI = Convert.ToBoolean(pData.InvertRI);
          ee232r.Cbus0 = pData.Cbus0;
          ee232r.Cbus1 = pData.Cbus1;
          ee232r.Cbus2 = pData.Cbus2;
          ee232r.Cbus3 = pData.Cbus3;
          ee232r.Cbus4 = pData.Cbus4;
          ee232r.RIsD2XX = Convert.ToBoolean(pData.RIsD2XX);
        }
      }
      else if (pFT_EE_Read == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_EE_Read.");
      return ftStatus;
    }

    /// <summary>Reads the EEPROM contents of an FT2232H device.</summary>
    /// <returns>FT_STATUS value from FT_EE_Read in FTD2XX DLL</returns>
    /// <param name="ee2232h">An FT2232H_EEPROM_STRUCTURE which contains only the relevant information for an FT2232H device.</param>
    /// <exception cref="T:FTD2XX_NET.FTDI.FT_EXCEPTION">Thrown when the current device does not match the type required by this method.</exception>
    public FT_STATUS ReadFT2232HEEPROM(FT2232H_EEPROM_STRUCTURE ee2232h)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_EE_Read != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_EE_Read) Marshal.GetDelegateForFunctionPointer(pFT_EE_Read, typeof (tFT_EE_Read));
        if (ftHandle != IntPtr.Zero)
        {
          var DeviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
          var deviceType = (int) GetDeviceType(ref DeviceType);
          if (DeviceType != FT_DEVICE.FT_DEVICE_2232H)
          {
            var ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
            ErrorHandler(ftStatus, ftErrorCondition);
          }
          var pData = new FT_PROGRAM_DATA();
          pData.Signature1 = 0U;
          pData.Signature2 = uint.MaxValue;
          pData.Version = 3U;
          pData.Manufacturer = Marshal.AllocHGlobal(32);
          pData.ManufacturerID = Marshal.AllocHGlobal(16);
          pData.Description = Marshal.AllocHGlobal(64);
          pData.SerialNumber = Marshal.AllocHGlobal(16);
          ftStatus = forFunctionPointer(ftHandle, pData);
          ee2232h.Manufacturer = Marshal.PtrToStringAnsi(pData.Manufacturer);
          ee2232h.ManufacturerID = Marshal.PtrToStringAnsi(pData.ManufacturerID);
          ee2232h.Description = Marshal.PtrToStringAnsi(pData.Description);
          ee2232h.SerialNumber = Marshal.PtrToStringAnsi(pData.SerialNumber);
          Marshal.FreeHGlobal(pData.Manufacturer);
          Marshal.FreeHGlobal(pData.ManufacturerID);
          Marshal.FreeHGlobal(pData.Description);
          Marshal.FreeHGlobal(pData.SerialNumber);
          ee2232h.VendorID = pData.VendorID;
          ee2232h.ProductID = pData.ProductID;
          ee2232h.MaxPower = pData.MaxPower;
          ee2232h.SelfPowered = Convert.ToBoolean(pData.SelfPowered);
          ee2232h.RemoteWakeup = Convert.ToBoolean(pData.RemoteWakeup);
          ee2232h.PullDownEnable = Convert.ToBoolean(pData.PullDownEnable7);
          ee2232h.SerNumEnable = Convert.ToBoolean(pData.SerNumEnable7);
          ee2232h.ALSlowSlew = Convert.ToBoolean(pData.ALSlowSlew);
          ee2232h.ALSchmittInput = Convert.ToBoolean(pData.ALSchmittInput);
          ee2232h.ALDriveCurrent = pData.ALDriveCurrent;
          ee2232h.AHSlowSlew = Convert.ToBoolean(pData.AHSlowSlew);
          ee2232h.AHSchmittInput = Convert.ToBoolean(pData.AHSchmittInput);
          ee2232h.AHDriveCurrent = pData.AHDriveCurrent;
          ee2232h.BLSlowSlew = Convert.ToBoolean(pData.BLSlowSlew);
          ee2232h.BLSchmittInput = Convert.ToBoolean(pData.BLSchmittInput);
          ee2232h.BLDriveCurrent = pData.BLDriveCurrent;
          ee2232h.BHSlowSlew = Convert.ToBoolean(pData.BHSlowSlew);
          ee2232h.BHSchmittInput = Convert.ToBoolean(pData.BHSchmittInput);
          ee2232h.BHDriveCurrent = pData.BHDriveCurrent;
          ee2232h.IFAIsFifo = Convert.ToBoolean(pData.IFAIsFifo7);
          ee2232h.IFAIsFifoTar = Convert.ToBoolean(pData.IFAIsFifoTar7);
          ee2232h.IFAIsFastSer = Convert.ToBoolean(pData.IFAIsFastSer7);
          ee2232h.AIsVCP = Convert.ToBoolean(pData.AIsVCP7);
          ee2232h.IFBIsFifo = Convert.ToBoolean(pData.IFBIsFifo7);
          ee2232h.IFBIsFifoTar = Convert.ToBoolean(pData.IFBIsFifoTar7);
          ee2232h.IFBIsFastSer = Convert.ToBoolean(pData.IFBIsFastSer7);
          ee2232h.BIsVCP = Convert.ToBoolean(pData.BIsVCP7);
          ee2232h.PowerSaveEnable = Convert.ToBoolean(pData.PowerSaveEnable);
        }
      }
      else if (pFT_EE_Read == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_EE_Read.");
      return ftStatus;
    }

    /// <summary>Reads the EEPROM contents of an FT4232H device.</summary>
    /// <returns>FT_STATUS value from FT_EE_Read in FTD2XX DLL</returns>
    /// <param name="ee4232h">An FT4232H_EEPROM_STRUCTURE which contains only the relevant information for an FT4232H device.</param>
    /// <exception cref="T:FTD2XX_NET.FTDI.FT_EXCEPTION">Thrown when the current device does not match the type required by this method.</exception>
    public FT_STATUS ReadFT4232HEEPROM(FT4232H_EEPROM_STRUCTURE ee4232h)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_EE_Read != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_EE_Read) Marshal.GetDelegateForFunctionPointer(pFT_EE_Read, typeof (tFT_EE_Read));
        if (ftHandle != IntPtr.Zero)
        {
          var DeviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
          var deviceType = (int) GetDeviceType(ref DeviceType);
          if (DeviceType != FT_DEVICE.FT_DEVICE_4232H)
          {
            var ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
            ErrorHandler(ftStatus, ftErrorCondition);
          }
          var pData = new FT_PROGRAM_DATA();
          pData.Signature1 = 0U;
          pData.Signature2 = uint.MaxValue;
          pData.Version = 4U;
          pData.Manufacturer = Marshal.AllocHGlobal(32);
          pData.ManufacturerID = Marshal.AllocHGlobal(16);
          pData.Description = Marshal.AllocHGlobal(64);
          pData.SerialNumber = Marshal.AllocHGlobal(16);
          ftStatus = forFunctionPointer(ftHandle, pData);
          ee4232h.Manufacturer = Marshal.PtrToStringAnsi(pData.Manufacturer);
          ee4232h.ManufacturerID = Marshal.PtrToStringAnsi(pData.ManufacturerID);
          ee4232h.Description = Marshal.PtrToStringAnsi(pData.Description);
          ee4232h.SerialNumber = Marshal.PtrToStringAnsi(pData.SerialNumber);
          Marshal.FreeHGlobal(pData.Manufacturer);
          Marshal.FreeHGlobal(pData.ManufacturerID);
          Marshal.FreeHGlobal(pData.Description);
          Marshal.FreeHGlobal(pData.SerialNumber);
          ee4232h.VendorID = pData.VendorID;
          ee4232h.ProductID = pData.ProductID;
          ee4232h.MaxPower = pData.MaxPower;
          ee4232h.SelfPowered = Convert.ToBoolean(pData.SelfPowered);
          ee4232h.RemoteWakeup = Convert.ToBoolean(pData.RemoteWakeup);
          ee4232h.PullDownEnable = Convert.ToBoolean(pData.PullDownEnable8);
          ee4232h.SerNumEnable = Convert.ToBoolean(pData.SerNumEnable8);
          ee4232h.ASlowSlew = Convert.ToBoolean(pData.ASlowSlew);
          ee4232h.ASchmittInput = Convert.ToBoolean(pData.ASchmittInput);
          ee4232h.ADriveCurrent = pData.ADriveCurrent;
          ee4232h.BSlowSlew = Convert.ToBoolean(pData.BSlowSlew);
          ee4232h.BSchmittInput = Convert.ToBoolean(pData.BSchmittInput);
          ee4232h.BDriveCurrent = pData.BDriveCurrent;
          ee4232h.CSlowSlew = Convert.ToBoolean(pData.CSlowSlew);
          ee4232h.CSchmittInput = Convert.ToBoolean(pData.CSchmittInput);
          ee4232h.CDriveCurrent = pData.CDriveCurrent;
          ee4232h.DSlowSlew = Convert.ToBoolean(pData.DSlowSlew);
          ee4232h.DSchmittInput = Convert.ToBoolean(pData.DSchmittInput);
          ee4232h.DDriveCurrent = pData.DDriveCurrent;
          ee4232h.ARIIsTXDEN = Convert.ToBoolean(pData.ARIIsTXDEN);
          ee4232h.BRIIsTXDEN = Convert.ToBoolean(pData.BRIIsTXDEN);
          ee4232h.CRIIsTXDEN = Convert.ToBoolean(pData.CRIIsTXDEN);
          ee4232h.DRIIsTXDEN = Convert.ToBoolean(pData.DRIIsTXDEN);
          ee4232h.AIsVCP = Convert.ToBoolean(pData.AIsVCP8);
          ee4232h.BIsVCP = Convert.ToBoolean(pData.BIsVCP8);
          ee4232h.CIsVCP = Convert.ToBoolean(pData.CIsVCP8);
          ee4232h.DIsVCP = Convert.ToBoolean(pData.DIsVCP8);
        }
      }
      else if (pFT_EE_Read == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_EE_Read.");
      return ftStatus;
    }

    /// <summary>Reads the EEPROM contents of an FT232H device.</summary>
    /// <returns>FT_STATUS value from FT_EE_Read in FTD2XX DLL</returns>
    /// <param name="ee232h">An FT232H_EEPROM_STRUCTURE which contains only the relevant information for an FT232H device.</param>
    /// <exception cref="T:FTD2XX_NET.FTDI.FT_EXCEPTION">Thrown when the current device does not match the type required by this method.</exception>
    public FT_STATUS ReadFT232HEEPROM(FT232H_EEPROM_STRUCTURE ee232h)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_EE_Read != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_EE_Read) Marshal.GetDelegateForFunctionPointer(pFT_EE_Read, typeof (tFT_EE_Read));
        if (ftHandle != IntPtr.Zero)
        {
          var DeviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
          var deviceType = (int) GetDeviceType(ref DeviceType);
          if (DeviceType != FT_DEVICE.FT_DEVICE_232H)
          {
            var ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
            ErrorHandler(ftStatus, ftErrorCondition);
          }
          var pData = new FT_PROGRAM_DATA();
          pData.Signature1 = 0U;
          pData.Signature2 = uint.MaxValue;
          pData.Version = 5U;
          pData.Manufacturer = Marshal.AllocHGlobal(32);
          pData.ManufacturerID = Marshal.AllocHGlobal(16);
          pData.Description = Marshal.AllocHGlobal(64);
          pData.SerialNumber = Marshal.AllocHGlobal(16);
          ftStatus = forFunctionPointer(ftHandle, pData);
          ee232h.Manufacturer = Marshal.PtrToStringAnsi(pData.Manufacturer);
          ee232h.ManufacturerID = Marshal.PtrToStringAnsi(pData.ManufacturerID);
          ee232h.Description = Marshal.PtrToStringAnsi(pData.Description);
          ee232h.SerialNumber = Marshal.PtrToStringAnsi(pData.SerialNumber);
          Marshal.FreeHGlobal(pData.Manufacturer);
          Marshal.FreeHGlobal(pData.ManufacturerID);
          Marshal.FreeHGlobal(pData.Description);
          Marshal.FreeHGlobal(pData.SerialNumber);
          ee232h.VendorID = pData.VendorID;
          ee232h.ProductID = pData.ProductID;
          ee232h.MaxPower = pData.MaxPower;
          ee232h.SelfPowered = Convert.ToBoolean(pData.SelfPowered);
          ee232h.RemoteWakeup = Convert.ToBoolean(pData.RemoteWakeup);
          ee232h.PullDownEnable = Convert.ToBoolean(pData.PullDownEnableH);
          ee232h.SerNumEnable = Convert.ToBoolean(pData.SerNumEnableH);
          ee232h.ACSlowSlew = Convert.ToBoolean(pData.ACSlowSlewH);
          ee232h.ACSchmittInput = Convert.ToBoolean(pData.ACSchmittInputH);
          ee232h.ACDriveCurrent = pData.ACDriveCurrentH;
          ee232h.ADSlowSlew = Convert.ToBoolean(pData.ADSlowSlewH);
          ee232h.ADSchmittInput = Convert.ToBoolean(pData.ADSchmittInputH);
          ee232h.ADDriveCurrent = pData.ADDriveCurrentH;
          ee232h.Cbus0 = pData.Cbus0H;
          ee232h.Cbus1 = pData.Cbus1H;
          ee232h.Cbus2 = pData.Cbus2H;
          ee232h.Cbus3 = pData.Cbus3H;
          ee232h.Cbus4 = pData.Cbus4H;
          ee232h.Cbus5 = pData.Cbus5H;
          ee232h.Cbus6 = pData.Cbus6H;
          ee232h.Cbus7 = pData.Cbus7H;
          ee232h.Cbus8 = pData.Cbus8H;
          ee232h.Cbus9 = pData.Cbus9H;
          ee232h.IsFifo = Convert.ToBoolean(pData.IsFifoH);
          ee232h.IsFifoTar = Convert.ToBoolean(pData.IsFifoTarH);
          ee232h.IsFastSer = Convert.ToBoolean(pData.IsFastSerH);
          ee232h.IsFT1248 = Convert.ToBoolean(pData.IsFT1248H);
          ee232h.FT1248Cpol = Convert.ToBoolean(pData.FT1248CpolH);
          ee232h.FT1248Lsb = Convert.ToBoolean(pData.FT1248LsbH);
          ee232h.FT1248FlowControl = Convert.ToBoolean(pData.FT1248FlowControlH);
          ee232h.IsVCP = Convert.ToBoolean(pData.IsVCPH);
          ee232h.PowerSaveEnable = Convert.ToBoolean(pData.PowerSaveEnableH);
        }
      }
      else if (pFT_EE_Read == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_EE_Read.");
      return ftStatus;
    }

    /// <summary>Reads the EEPROM contents of an X-Series device.</summary>
    /// <returns>FT_STATUS value from FT_EEPROM_Read in FTD2XX DLL</returns>
    /// <param name="eeX">An FT_XSERIES_EEPROM_STRUCTURE which contains only the relevant information for an X-Series device.</param>
    /// <exception cref="T:FTD2XX_NET.FTDI.FT_EXCEPTION">Thrown when the current device does not match the type required by this method.</exception>
    public FT_STATUS ReadXSeriesEEPROM(FT_XSERIES_EEPROM_STRUCTURE eeX)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_EEPROM_Read != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_EEPROM_Read) Marshal.GetDelegateForFunctionPointer(pFT_EEPROM_Read, typeof (tFT_EEPROM_Read));
        if (ftHandle != IntPtr.Zero)
        {
          var DeviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
          var deviceType = (int) GetDeviceType(ref DeviceType);
          if (DeviceType != FT_DEVICE.FT_DEVICE_X_SERIES)
          {
            var ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
            ErrorHandler(ftStatus, ftErrorCondition);
          }
          var structure1 = new FT_XSERIES_DATA();
          var ftEepromHeader = new FT_EEPROM_HEADER();
          var numArray1 = new byte[32];
          var numArray2 = new byte[16];
          var numArray3 = new byte[64];
          var numArray4 = new byte[16];
          ftEepromHeader.deviceType = 9U;
          structure1.common = ftEepromHeader;
          var num1 = Marshal.SizeOf((object) structure1);
          var num2 = Marshal.AllocHGlobal(num1);
          Marshal.StructureToPtr((object) structure1, num2, false);
          ftStatus = forFunctionPointer(ftHandle, num2, (uint) num1, numArray1, numArray2, numArray3, numArray4);
          if (ftStatus == FT_STATUS.FT_OK)
          {
            var structure2 = (FT_XSERIES_DATA) Marshal.PtrToStructure(num2, typeof (FT_XSERIES_DATA));
            var utF8Encoding = new UTF8Encoding();
            eeX.Manufacturer = utF8Encoding.GetString(numArray1);
            eeX.ManufacturerID = utF8Encoding.GetString(numArray2);
            eeX.Description = utF8Encoding.GetString(numArray3);
            eeX.SerialNumber = utF8Encoding.GetString(numArray4);
            eeX.VendorID = structure2.common.VendorId;
            eeX.ProductID = structure2.common.ProductId;
            eeX.MaxPower = structure2.common.MaxPower;
            eeX.SelfPowered = Convert.ToBoolean(structure2.common.SelfPowered);
            eeX.RemoteWakeup = Convert.ToBoolean(structure2.common.RemoteWakeup);
            eeX.SerNumEnable = Convert.ToBoolean(structure2.common.SerNumEnable);
            eeX.PullDownEnable = Convert.ToBoolean(structure2.common.PullDownEnable);
            eeX.Cbus0 = structure2.Cbus0;
            eeX.Cbus1 = structure2.Cbus1;
            eeX.Cbus2 = structure2.Cbus2;
            eeX.Cbus3 = structure2.Cbus3;
            eeX.Cbus4 = structure2.Cbus4;
            eeX.Cbus5 = structure2.Cbus5;
            eeX.Cbus6 = structure2.Cbus6;
            eeX.ACDriveCurrent = structure2.ACDriveCurrent;
            eeX.ACSchmittInput = structure2.ACSchmittInput;
            eeX.ACSlowSlew = structure2.ACSlowSlew;
            eeX.ADDriveCurrent = structure2.ADDriveCurrent;
            eeX.ADSchmittInput = structure2.ADSchmittInput;
            eeX.ADSlowSlew = structure2.ADSlowSlew;
            eeX.BCDDisableSleep = structure2.BCDDisableSleep;
            eeX.BCDEnable = structure2.BCDEnable;
            eeX.BCDForceCbusPWREN = structure2.BCDForceCbusPWREN;
            eeX.FT1248Cpol = structure2.FT1248Cpol;
            eeX.FT1248FlowControl = structure2.FT1248FlowControl;
            eeX.FT1248Lsb = structure2.FT1248Lsb;
            eeX.I2CDeviceId = structure2.I2CDeviceId;
            eeX.I2CDisableSchmitt = structure2.I2CDisableSchmitt;
            eeX.I2CSlaveAddress = structure2.I2CSlaveAddress;
            eeX.InvertCTS = structure2.InvertCTS;
            eeX.InvertDCD = structure2.InvertDCD;
            eeX.InvertDSR = structure2.InvertDSR;
            eeX.InvertDTR = structure2.InvertDTR;
            eeX.InvertRI = structure2.InvertRI;
            eeX.InvertRTS = structure2.InvertRTS;
            eeX.InvertRXD = structure2.InvertRXD;
            eeX.InvertTXD = structure2.InvertTXD;
            eeX.PowerSaveEnable = structure2.PowerSaveEnable;
            eeX.RS485EchoSuppress = structure2.RS485EchoSuppress;
            eeX.IsVCP = structure2.DriverType;
          }
        }
      }
      else if (pFT_EE_Read == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_EE_Read.");
      return ftStatus;
    }

    /// <summary>
    /// Writes the specified values to the EEPROM of an FT232B or FT245B device.
    /// </summary>
    /// <returns>FT_STATUS value from FT_EE_Program in FTD2XX DLL</returns>
    /// <param name="ee232b">The EEPROM settings to be written to the device</param>
    /// <remarks>If the strings are too long, they will be truncated to their maximum permitted lengths</remarks>
    /// <exception cref="T:FTD2XX_NET.FTDI.FT_EXCEPTION">Thrown when the current device does not match the type required by this method.</exception>
    public FT_STATUS WriteFT232BEEPROM(FT232B_EEPROM_STRUCTURE ee232b)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_EE_Program != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_EE_Program) Marshal.GetDelegateForFunctionPointer(pFT_EE_Program, typeof (tFT_EE_Program));
        if (ftHandle != IntPtr.Zero)
        {
          var DeviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
          var deviceType = (int) GetDeviceType(ref DeviceType);
          if (DeviceType != FT_DEVICE.FT_DEVICE_BM)
          {
            var ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
            ErrorHandler(ftStatus, ftErrorCondition);
          }
          if (ee232b.VendorID == 0 | ee232b.ProductID == 0)
            return FT_STATUS.FT_INVALID_PARAMETER;
          var pData = new FT_PROGRAM_DATA();
          pData.Signature1 = 0U;
          pData.Signature2 = uint.MaxValue;
          pData.Version = 2U;
          pData.Manufacturer = Marshal.AllocHGlobal(32);
          pData.ManufacturerID = Marshal.AllocHGlobal(16);
          pData.Description = Marshal.AllocHGlobal(64);
          pData.SerialNumber = Marshal.AllocHGlobal(16);
          if (ee232b.Manufacturer.Length > 32)
            ee232b.Manufacturer = ee232b.Manufacturer.Substring(0, 32);
          if (ee232b.ManufacturerID.Length > 16)
            ee232b.ManufacturerID = ee232b.ManufacturerID.Substring(0, 16);
          if (ee232b.Description.Length > 64)
            ee232b.Description = ee232b.Description.Substring(0, 64);
          if (ee232b.SerialNumber.Length > 16)
            ee232b.SerialNumber = ee232b.SerialNumber.Substring(0, 16);
          pData.Manufacturer = Marshal.StringToHGlobalAnsi(ee232b.Manufacturer);
          pData.ManufacturerID = Marshal.StringToHGlobalAnsi(ee232b.ManufacturerID);
          pData.Description = Marshal.StringToHGlobalAnsi(ee232b.Description);
          pData.SerialNumber = Marshal.StringToHGlobalAnsi(ee232b.SerialNumber);
          pData.VendorID = ee232b.VendorID;
          pData.ProductID = ee232b.ProductID;
          pData.MaxPower = ee232b.MaxPower;
          pData.SelfPowered = Convert.ToUInt16(ee232b.SelfPowered);
          pData.RemoteWakeup = Convert.ToUInt16(ee232b.RemoteWakeup);
          pData.Rev4 = Convert.ToByte(true);
          pData.PullDownEnable = Convert.ToByte(ee232b.PullDownEnable);
          pData.SerNumEnable = Convert.ToByte(ee232b.SerNumEnable);
          pData.USBVersionEnable = Convert.ToByte(ee232b.USBVersionEnable);
          pData.USBVersion = ee232b.USBVersion;
          ftStatus = forFunctionPointer(ftHandle, pData);
          Marshal.FreeHGlobal(pData.Manufacturer);
          Marshal.FreeHGlobal(pData.ManufacturerID);
          Marshal.FreeHGlobal(pData.Description);
          Marshal.FreeHGlobal(pData.SerialNumber);
        }
      }
      else if (pFT_EE_Program == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_EE_Program.");
      return ftStatus;
    }

    /// <summary>
    /// Writes the specified values to the EEPROM of an FT2232 device.
    /// Calls FT_EE_Program in FTD2XX DLL
    /// </summary>
    /// <returns>FT_STATUS value from FT_EE_Program in FTD2XX DLL</returns>
    /// <param name="ee2232">The EEPROM settings to be written to the device</param>
    /// <remarks>If the strings are too long, they will be truncated to their maximum permitted lengths</remarks>
    /// <exception cref="T:FTD2XX_NET.FTDI.FT_EXCEPTION">Thrown when the current device does not match the type required by this method.</exception>
    public FT_STATUS WriteFT2232EEPROM(FT2232_EEPROM_STRUCTURE ee2232)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_EE_Program != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_EE_Program) Marshal.GetDelegateForFunctionPointer(pFT_EE_Program, typeof (tFT_EE_Program));
        if (ftHandle != IntPtr.Zero)
        {
          var DeviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
          var deviceType = (int) GetDeviceType(ref DeviceType);
          if (DeviceType != FT_DEVICE.FT_DEVICE_2232)
          {
            var ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
            ErrorHandler(ftStatus, ftErrorCondition);
          }
          if (ee2232.VendorID == 0 | ee2232.ProductID == 0)
            return FT_STATUS.FT_INVALID_PARAMETER;
          var pData = new FT_PROGRAM_DATA();
          pData.Signature1 = 0U;
          pData.Signature2 = uint.MaxValue;
          pData.Version = 2U;
          pData.Manufacturer = Marshal.AllocHGlobal(32);
          pData.ManufacturerID = Marshal.AllocHGlobal(16);
          pData.Description = Marshal.AllocHGlobal(64);
          pData.SerialNumber = Marshal.AllocHGlobal(16);
          if (ee2232.Manufacturer.Length > 32)
            ee2232.Manufacturer = ee2232.Manufacturer.Substring(0, 32);
          if (ee2232.ManufacturerID.Length > 16)
            ee2232.ManufacturerID = ee2232.ManufacturerID.Substring(0, 16);
          if (ee2232.Description.Length > 64)
            ee2232.Description = ee2232.Description.Substring(0, 64);
          if (ee2232.SerialNumber.Length > 16)
            ee2232.SerialNumber = ee2232.SerialNumber.Substring(0, 16);
          pData.Manufacturer = Marshal.StringToHGlobalAnsi(ee2232.Manufacturer);
          pData.ManufacturerID = Marshal.StringToHGlobalAnsi(ee2232.ManufacturerID);
          pData.Description = Marshal.StringToHGlobalAnsi(ee2232.Description);
          pData.SerialNumber = Marshal.StringToHGlobalAnsi(ee2232.SerialNumber);
          pData.VendorID = ee2232.VendorID;
          pData.ProductID = ee2232.ProductID;
          pData.MaxPower = ee2232.MaxPower;
          pData.SelfPowered = Convert.ToUInt16(ee2232.SelfPowered);
          pData.RemoteWakeup = Convert.ToUInt16(ee2232.RemoteWakeup);
          pData.Rev5 = Convert.ToByte(true);
          pData.PullDownEnable5 = Convert.ToByte(ee2232.PullDownEnable);
          pData.SerNumEnable5 = Convert.ToByte(ee2232.SerNumEnable);
          pData.USBVersionEnable5 = Convert.ToByte(ee2232.USBVersionEnable);
          pData.USBVersion5 = ee2232.USBVersion;
          pData.AIsHighCurrent = Convert.ToByte(ee2232.AIsHighCurrent);
          pData.BIsHighCurrent = Convert.ToByte(ee2232.BIsHighCurrent);
          pData.IFAIsFifo = Convert.ToByte(ee2232.IFAIsFifo);
          pData.IFAIsFifoTar = Convert.ToByte(ee2232.IFAIsFifoTar);
          pData.IFAIsFastSer = Convert.ToByte(ee2232.IFAIsFastSer);
          pData.AIsVCP = Convert.ToByte(ee2232.AIsVCP);
          pData.IFBIsFifo = Convert.ToByte(ee2232.IFBIsFifo);
          pData.IFBIsFifoTar = Convert.ToByte(ee2232.IFBIsFifoTar);
          pData.IFBIsFastSer = Convert.ToByte(ee2232.IFBIsFastSer);
          pData.BIsVCP = Convert.ToByte(ee2232.BIsVCP);
          ftStatus = forFunctionPointer(ftHandle, pData);
          Marshal.FreeHGlobal(pData.Manufacturer);
          Marshal.FreeHGlobal(pData.ManufacturerID);
          Marshal.FreeHGlobal(pData.Description);
          Marshal.FreeHGlobal(pData.SerialNumber);
        }
      }
      else if (pFT_EE_Program == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_EE_Program.");
      return ftStatus;
    }

    /// <summary>
    /// Writes the specified values to the EEPROM of an FT232R or FT245R device.
    /// Calls FT_EE_Program in FTD2XX DLL
    /// </summary>
    /// <returns>FT_STATUS value from FT_EE_Program in FTD2XX DLL</returns>
    /// <param name="ee232r">The EEPROM settings to be written to the device</param>
    /// <remarks>If the strings are too long, they will be truncated to their maximum permitted lengths</remarks>
    /// <exception cref="T:FTD2XX_NET.FTDI.FT_EXCEPTION">Thrown when the current device does not match the type required by this method.</exception>
    public FT_STATUS WriteFT232REEPROM(FT232R_EEPROM_STRUCTURE ee232r)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_EE_Program != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_EE_Program) Marshal.GetDelegateForFunctionPointer(pFT_EE_Program, typeof (tFT_EE_Program));
        if (ftHandle != IntPtr.Zero)
        {
          var DeviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
          var deviceType = (int) GetDeviceType(ref DeviceType);
          if (DeviceType != FT_DEVICE.FT_DEVICE_232R)
          {
            var ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
            ErrorHandler(ftStatus, ftErrorCondition);
          }
          if (ee232r.VendorID == 0 | ee232r.ProductID == 0)
            return FT_STATUS.FT_INVALID_PARAMETER;
          var pData = new FT_PROGRAM_DATA();
          pData.Signature1 = 0U;
          pData.Signature2 = uint.MaxValue;
          pData.Version = 2U;
          pData.Manufacturer = Marshal.AllocHGlobal(32);
          pData.ManufacturerID = Marshal.AllocHGlobal(16);
          pData.Description = Marshal.AllocHGlobal(64);
          pData.SerialNumber = Marshal.AllocHGlobal(16);
          if (ee232r.Manufacturer.Length > 32)
            ee232r.Manufacturer = ee232r.Manufacturer.Substring(0, 32);
          if (ee232r.ManufacturerID.Length > 16)
            ee232r.ManufacturerID = ee232r.ManufacturerID.Substring(0, 16);
          if (ee232r.Description.Length > 64)
            ee232r.Description = ee232r.Description.Substring(0, 64);
          if (ee232r.SerialNumber.Length > 16)
            ee232r.SerialNumber = ee232r.SerialNumber.Substring(0, 16);
          pData.Manufacturer = Marshal.StringToHGlobalAnsi(ee232r.Manufacturer);
          pData.ManufacturerID = Marshal.StringToHGlobalAnsi(ee232r.ManufacturerID);
          pData.Description = Marshal.StringToHGlobalAnsi(ee232r.Description);
          pData.SerialNumber = Marshal.StringToHGlobalAnsi(ee232r.SerialNumber);
          pData.VendorID = ee232r.VendorID;
          pData.ProductID = ee232r.ProductID;
          pData.MaxPower = ee232r.MaxPower;
          pData.SelfPowered = Convert.ToUInt16(ee232r.SelfPowered);
          pData.RemoteWakeup = Convert.ToUInt16(ee232r.RemoteWakeup);
          pData.PullDownEnableR = Convert.ToByte(ee232r.PullDownEnable);
          pData.SerNumEnableR = Convert.ToByte(ee232r.SerNumEnable);
          pData.UseExtOsc = Convert.ToByte(ee232r.UseExtOsc);
          pData.HighDriveIOs = Convert.ToByte(ee232r.HighDriveIOs);
          pData.EndpointSize = 64;
          pData.PullDownEnableR = Convert.ToByte(ee232r.PullDownEnable);
          pData.SerNumEnableR = Convert.ToByte(ee232r.SerNumEnable);
          pData.InvertTXD = Convert.ToByte(ee232r.InvertTXD);
          pData.InvertRXD = Convert.ToByte(ee232r.InvertRXD);
          pData.InvertRTS = Convert.ToByte(ee232r.InvertRTS);
          pData.InvertCTS = Convert.ToByte(ee232r.InvertCTS);
          pData.InvertDTR = Convert.ToByte(ee232r.InvertDTR);
          pData.InvertDSR = Convert.ToByte(ee232r.InvertDSR);
          pData.InvertDCD = Convert.ToByte(ee232r.InvertDCD);
          pData.InvertRI = Convert.ToByte(ee232r.InvertRI);
          pData.Cbus0 = ee232r.Cbus0;
          pData.Cbus1 = ee232r.Cbus1;
          pData.Cbus2 = ee232r.Cbus2;
          pData.Cbus3 = ee232r.Cbus3;
          pData.Cbus4 = ee232r.Cbus4;
          pData.RIsD2XX = Convert.ToByte(ee232r.RIsD2XX);
          ftStatus = forFunctionPointer(ftHandle, pData);
          Marshal.FreeHGlobal(pData.Manufacturer);
          Marshal.FreeHGlobal(pData.ManufacturerID);
          Marshal.FreeHGlobal(pData.Description);
          Marshal.FreeHGlobal(pData.SerialNumber);
        }
      }
      else if (pFT_EE_Program == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_EE_Program.");
      return ftStatus;
    }

    /// <summary>
    /// Writes the specified values to the EEPROM of an FT2232H device.
    /// Calls FT_EE_Program in FTD2XX DLL
    /// </summary>
    /// <returns>FT_STATUS value from FT_EE_Program in FTD2XX DLL</returns>
    /// <param name="ee2232h">The EEPROM settings to be written to the device</param>
    /// <remarks>If the strings are too long, they will be truncated to their maximum permitted lengths</remarks>
    /// <exception cref="T:FTD2XX_NET.FTDI.FT_EXCEPTION">Thrown when the current device does not match the type required by this method.</exception>
    public FT_STATUS WriteFT2232HEEPROM(FT2232H_EEPROM_STRUCTURE ee2232h)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_EE_Program != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_EE_Program) Marshal.GetDelegateForFunctionPointer(pFT_EE_Program, typeof (tFT_EE_Program));
        if (ftHandle != IntPtr.Zero)
        {
          var DeviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
          var deviceType = (int) GetDeviceType(ref DeviceType);
          if (DeviceType != FT_DEVICE.FT_DEVICE_2232H)
          {
            var ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
            ErrorHandler(ftStatus, ftErrorCondition);
          }
          if (ee2232h.VendorID == 0 | ee2232h.ProductID == 0)
            return FT_STATUS.FT_INVALID_PARAMETER;
          var pData = new FT_PROGRAM_DATA();
          pData.Signature1 = 0U;
          pData.Signature2 = uint.MaxValue;
          pData.Version = 3U;
          pData.Manufacturer = Marshal.AllocHGlobal(32);
          pData.ManufacturerID = Marshal.AllocHGlobal(16);
          pData.Description = Marshal.AllocHGlobal(64);
          pData.SerialNumber = Marshal.AllocHGlobal(16);
          if (ee2232h.Manufacturer.Length > 32)
            ee2232h.Manufacturer = ee2232h.Manufacturer.Substring(0, 32);
          if (ee2232h.ManufacturerID.Length > 16)
            ee2232h.ManufacturerID = ee2232h.ManufacturerID.Substring(0, 16);
          if (ee2232h.Description.Length > 64)
            ee2232h.Description = ee2232h.Description.Substring(0, 64);
          if (ee2232h.SerialNumber.Length > 16)
            ee2232h.SerialNumber = ee2232h.SerialNumber.Substring(0, 16);
          pData.Manufacturer = Marshal.StringToHGlobalAnsi(ee2232h.Manufacturer);
          pData.ManufacturerID = Marshal.StringToHGlobalAnsi(ee2232h.ManufacturerID);
          pData.Description = Marshal.StringToHGlobalAnsi(ee2232h.Description);
          pData.SerialNumber = Marshal.StringToHGlobalAnsi(ee2232h.SerialNumber);
          pData.VendorID = ee2232h.VendorID;
          pData.ProductID = ee2232h.ProductID;
          pData.MaxPower = ee2232h.MaxPower;
          pData.SelfPowered = Convert.ToUInt16(ee2232h.SelfPowered);
          pData.RemoteWakeup = Convert.ToUInt16(ee2232h.RemoteWakeup);
          pData.PullDownEnable7 = Convert.ToByte(ee2232h.PullDownEnable);
          pData.SerNumEnable7 = Convert.ToByte(ee2232h.SerNumEnable);
          pData.ALSlowSlew = Convert.ToByte(ee2232h.ALSlowSlew);
          pData.ALSchmittInput = Convert.ToByte(ee2232h.ALSchmittInput);
          pData.ALDriveCurrent = ee2232h.ALDriveCurrent;
          pData.AHSlowSlew = Convert.ToByte(ee2232h.AHSlowSlew);
          pData.AHSchmittInput = Convert.ToByte(ee2232h.AHSchmittInput);
          pData.AHDriveCurrent = ee2232h.AHDriveCurrent;
          pData.BLSlowSlew = Convert.ToByte(ee2232h.BLSlowSlew);
          pData.BLSchmittInput = Convert.ToByte(ee2232h.BLSchmittInput);
          pData.BLDriveCurrent = ee2232h.BLDriveCurrent;
          pData.BHSlowSlew = Convert.ToByte(ee2232h.BHSlowSlew);
          pData.BHSchmittInput = Convert.ToByte(ee2232h.BHSchmittInput);
          pData.BHDriveCurrent = ee2232h.BHDriveCurrent;
          pData.IFAIsFifo7 = Convert.ToByte(ee2232h.IFAIsFifo);
          pData.IFAIsFifoTar7 = Convert.ToByte(ee2232h.IFAIsFifoTar);
          pData.IFAIsFastSer7 = Convert.ToByte(ee2232h.IFAIsFastSer);
          pData.AIsVCP7 = Convert.ToByte(ee2232h.AIsVCP);
          pData.IFBIsFifo7 = Convert.ToByte(ee2232h.IFBIsFifo);
          pData.IFBIsFifoTar7 = Convert.ToByte(ee2232h.IFBIsFifoTar);
          pData.IFBIsFastSer7 = Convert.ToByte(ee2232h.IFBIsFastSer);
          pData.BIsVCP7 = Convert.ToByte(ee2232h.BIsVCP);
          pData.PowerSaveEnable = Convert.ToByte(ee2232h.PowerSaveEnable);
          ftStatus = forFunctionPointer(ftHandle, pData);
          Marshal.FreeHGlobal(pData.Manufacturer);
          Marshal.FreeHGlobal(pData.ManufacturerID);
          Marshal.FreeHGlobal(pData.Description);
          Marshal.FreeHGlobal(pData.SerialNumber);
        }
      }
      else if (pFT_EE_Program == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_EE_Program.");
      return ftStatus;
    }

    /// <summary>
    /// Writes the specified values to the EEPROM of an FT4232H device.
    /// Calls FT_EE_Program in FTD2XX DLL
    /// </summary>
    /// <returns>FT_STATUS value from FT_EE_Program in FTD2XX DLL</returns>
    /// <param name="ee4232h">The EEPROM settings to be written to the device</param>
    /// <remarks>If the strings are too long, they will be truncated to their maximum permitted lengths</remarks>
    /// <exception cref="T:FTD2XX_NET.FTDI.FT_EXCEPTION">Thrown when the current device does not match the type required by this method.</exception>
    public FT_STATUS WriteFT4232HEEPROM(FT4232H_EEPROM_STRUCTURE ee4232h)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_EE_Program != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_EE_Program) Marshal.GetDelegateForFunctionPointer(pFT_EE_Program, typeof (tFT_EE_Program));
        if (ftHandle != IntPtr.Zero)
        {
          var DeviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
          var deviceType = (int) GetDeviceType(ref DeviceType);
          if (DeviceType != FT_DEVICE.FT_DEVICE_4232H)
          {
            var ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
            ErrorHandler(ftStatus, ftErrorCondition);
          }
          if (ee4232h.VendorID == 0 | ee4232h.ProductID == 0)
            return FT_STATUS.FT_INVALID_PARAMETER;
          var pData = new FT_PROGRAM_DATA();
          pData.Signature1 = 0U;
          pData.Signature2 = uint.MaxValue;
          pData.Version = 4U;
          pData.Manufacturer = Marshal.AllocHGlobal(32);
          pData.ManufacturerID = Marshal.AllocHGlobal(16);
          pData.Description = Marshal.AllocHGlobal(64);
          pData.SerialNumber = Marshal.AllocHGlobal(16);
          if (ee4232h.Manufacturer.Length > 32)
            ee4232h.Manufacturer = ee4232h.Manufacturer.Substring(0, 32);
          if (ee4232h.ManufacturerID.Length > 16)
            ee4232h.ManufacturerID = ee4232h.ManufacturerID.Substring(0, 16);
          if (ee4232h.Description.Length > 64)
            ee4232h.Description = ee4232h.Description.Substring(0, 64);
          if (ee4232h.SerialNumber.Length > 16)
            ee4232h.SerialNumber = ee4232h.SerialNumber.Substring(0, 16);
          pData.Manufacturer = Marshal.StringToHGlobalAnsi(ee4232h.Manufacturer);
          pData.ManufacturerID = Marshal.StringToHGlobalAnsi(ee4232h.ManufacturerID);
          pData.Description = Marshal.StringToHGlobalAnsi(ee4232h.Description);
          pData.SerialNumber = Marshal.StringToHGlobalAnsi(ee4232h.SerialNumber);
          pData.VendorID = ee4232h.VendorID;
          pData.ProductID = ee4232h.ProductID;
          pData.MaxPower = ee4232h.MaxPower;
          pData.SelfPowered = Convert.ToUInt16(ee4232h.SelfPowered);
          pData.RemoteWakeup = Convert.ToUInt16(ee4232h.RemoteWakeup);
          pData.PullDownEnable8 = Convert.ToByte(ee4232h.PullDownEnable);
          pData.SerNumEnable8 = Convert.ToByte(ee4232h.SerNumEnable);
          pData.ASlowSlew = Convert.ToByte(ee4232h.ASlowSlew);
          pData.ASchmittInput = Convert.ToByte(ee4232h.ASchmittInput);
          pData.ADriveCurrent = ee4232h.ADriveCurrent;
          pData.BSlowSlew = Convert.ToByte(ee4232h.BSlowSlew);
          pData.BSchmittInput = Convert.ToByte(ee4232h.BSchmittInput);
          pData.BDriveCurrent = ee4232h.BDriveCurrent;
          pData.CSlowSlew = Convert.ToByte(ee4232h.CSlowSlew);
          pData.CSchmittInput = Convert.ToByte(ee4232h.CSchmittInput);
          pData.CDriveCurrent = ee4232h.CDriveCurrent;
          pData.DSlowSlew = Convert.ToByte(ee4232h.DSlowSlew);
          pData.DSchmittInput = Convert.ToByte(ee4232h.DSchmittInput);
          pData.DDriveCurrent = ee4232h.DDriveCurrent;
          pData.ARIIsTXDEN = Convert.ToByte(ee4232h.ARIIsTXDEN);
          pData.BRIIsTXDEN = Convert.ToByte(ee4232h.BRIIsTXDEN);
          pData.CRIIsTXDEN = Convert.ToByte(ee4232h.CRIIsTXDEN);
          pData.DRIIsTXDEN = Convert.ToByte(ee4232h.DRIIsTXDEN);
          pData.AIsVCP8 = Convert.ToByte(ee4232h.AIsVCP);
          pData.BIsVCP8 = Convert.ToByte(ee4232h.BIsVCP);
          pData.CIsVCP8 = Convert.ToByte(ee4232h.CIsVCP);
          pData.DIsVCP8 = Convert.ToByte(ee4232h.DIsVCP);
          ftStatus = forFunctionPointer(ftHandle, pData);
          Marshal.FreeHGlobal(pData.Manufacturer);
          Marshal.FreeHGlobal(pData.ManufacturerID);
          Marshal.FreeHGlobal(pData.Description);
          Marshal.FreeHGlobal(pData.SerialNumber);
        }
      }
      else if (pFT_EE_Program == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_EE_Program.");
      return ftStatus;
    }

    /// <summary>
    /// Writes the specified values to the EEPROM of an FT232H device.
    /// Calls FT_EE_Program in FTD2XX DLL
    /// </summary>
    /// <returns>FT_STATUS value from FT_EE_Program in FTD2XX DLL</returns>
    /// <param name="ee232h">The EEPROM settings to be written to the device</param>
    /// <remarks>If the strings are too long, they will be truncated to their maximum permitted lengths</remarks>
    /// <exception cref="T:FTD2XX_NET.FTDI.FT_EXCEPTION">Thrown when the current device does not match the type required by this method.</exception>
    public FT_STATUS WriteFT232HEEPROM(FT232H_EEPROM_STRUCTURE ee232h)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_EE_Program != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_EE_Program) Marshal.GetDelegateForFunctionPointer(pFT_EE_Program, typeof (tFT_EE_Program));
        if (ftHandle != IntPtr.Zero)
        {
          var DeviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
          var deviceType = (int) GetDeviceType(ref DeviceType);
          if (DeviceType != FT_DEVICE.FT_DEVICE_232H)
          {
            var ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
            ErrorHandler(ftStatus, ftErrorCondition);
          }
          if (ee232h.VendorID == 0 | ee232h.ProductID == 0)
            return FT_STATUS.FT_INVALID_PARAMETER;
          var pData = new FT_PROGRAM_DATA();
          pData.Signature1 = 0U;
          pData.Signature2 = uint.MaxValue;
          pData.Version = 5U;
          pData.Manufacturer = Marshal.AllocHGlobal(32);
          pData.ManufacturerID = Marshal.AllocHGlobal(16);
          pData.Description = Marshal.AllocHGlobal(64);
          pData.SerialNumber = Marshal.AllocHGlobal(16);
          if (ee232h.Manufacturer.Length > 32)
            ee232h.Manufacturer = ee232h.Manufacturer.Substring(0, 32);
          if (ee232h.ManufacturerID.Length > 16)
            ee232h.ManufacturerID = ee232h.ManufacturerID.Substring(0, 16);
          if (ee232h.Description.Length > 64)
            ee232h.Description = ee232h.Description.Substring(0, 64);
          if (ee232h.SerialNumber.Length > 16)
            ee232h.SerialNumber = ee232h.SerialNumber.Substring(0, 16);
          pData.Manufacturer = Marshal.StringToHGlobalAnsi(ee232h.Manufacturer);
          pData.ManufacturerID = Marshal.StringToHGlobalAnsi(ee232h.ManufacturerID);
          pData.Description = Marshal.StringToHGlobalAnsi(ee232h.Description);
          pData.SerialNumber = Marshal.StringToHGlobalAnsi(ee232h.SerialNumber);
          pData.VendorID = ee232h.VendorID;
          pData.ProductID = ee232h.ProductID;
          pData.MaxPower = ee232h.MaxPower;
          pData.SelfPowered = Convert.ToUInt16(ee232h.SelfPowered);
          pData.RemoteWakeup = Convert.ToUInt16(ee232h.RemoteWakeup);
          pData.PullDownEnableH = Convert.ToByte(ee232h.PullDownEnable);
          pData.SerNumEnableH = Convert.ToByte(ee232h.SerNumEnable);
          pData.ACSlowSlewH = Convert.ToByte(ee232h.ACSlowSlew);
          pData.ACSchmittInputH = Convert.ToByte(ee232h.ACSchmittInput);
          pData.ACDriveCurrentH = Convert.ToByte(ee232h.ACDriveCurrent);
          pData.ADSlowSlewH = Convert.ToByte(ee232h.ADSlowSlew);
          pData.ADSchmittInputH = Convert.ToByte(ee232h.ADSchmittInput);
          pData.ADDriveCurrentH = Convert.ToByte(ee232h.ADDriveCurrent);
          pData.Cbus0H = Convert.ToByte(ee232h.Cbus0);
          pData.Cbus1H = Convert.ToByte(ee232h.Cbus1);
          pData.Cbus2H = Convert.ToByte(ee232h.Cbus2);
          pData.Cbus3H = Convert.ToByte(ee232h.Cbus3);
          pData.Cbus4H = Convert.ToByte(ee232h.Cbus4);
          pData.Cbus5H = Convert.ToByte(ee232h.Cbus5);
          pData.Cbus6H = Convert.ToByte(ee232h.Cbus6);
          pData.Cbus7H = Convert.ToByte(ee232h.Cbus7);
          pData.Cbus8H = Convert.ToByte(ee232h.Cbus8);
          pData.Cbus9H = Convert.ToByte(ee232h.Cbus9);
          pData.IsFifoH = Convert.ToByte(ee232h.IsFifo);
          pData.IsFifoTarH = Convert.ToByte(ee232h.IsFifoTar);
          pData.IsFastSerH = Convert.ToByte(ee232h.IsFastSer);
          pData.IsFT1248H = Convert.ToByte(ee232h.IsFT1248);
          pData.FT1248CpolH = Convert.ToByte(ee232h.FT1248Cpol);
          pData.FT1248LsbH = Convert.ToByte(ee232h.FT1248Lsb);
          pData.FT1248FlowControlH = Convert.ToByte(ee232h.FT1248FlowControl);
          pData.IsVCPH = Convert.ToByte(ee232h.IsVCP);
          pData.PowerSaveEnableH = Convert.ToByte(ee232h.PowerSaveEnable);
          ftStatus = forFunctionPointer(ftHandle, pData);
          Marshal.FreeHGlobal(pData.Manufacturer);
          Marshal.FreeHGlobal(pData.ManufacturerID);
          Marshal.FreeHGlobal(pData.Description);
          Marshal.FreeHGlobal(pData.SerialNumber);
        }
      }
      else if (pFT_EE_Program == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_EE_Program.");
      return ftStatus;
    }

    /// <summary>
    /// Writes the specified values to the EEPROM of an X-Series device.
    /// Calls FT_EEPROM_Program in FTD2XX DLL
    /// </summary>
    /// <returns>FT_STATUS value from FT_EEPROM_Program in FTD2XX DLL</returns>
    /// <param name="eeX">The EEPROM settings to be written to the device</param>
    /// <remarks>If the strings are too long, they will be truncated to their maximum permitted lengths</remarks>
    /// <exception cref="T:FTD2XX_NET.FTDI.FT_EXCEPTION">Thrown when the current device does not match the type required by this method.</exception>
    public FT_STATUS WriteXSeriesEEPROM(FT_XSERIES_EEPROM_STRUCTURE eeX)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero || !(pFT_EEPROM_Program != IntPtr.Zero))
        return ftStatus;
      var forFunctionPointer = (tFT_EEPROM_Program) Marshal.GetDelegateForFunctionPointer(pFT_EEPROM_Program, typeof (tFT_EEPROM_Program));
      if (ftHandle != IntPtr.Zero)
      {
        var DeviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
        var deviceType = (int) GetDeviceType(ref DeviceType);
        if (DeviceType != FT_DEVICE.FT_DEVICE_X_SERIES)
        {
          var ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
          ErrorHandler(ftStatus, ftErrorCondition);
        }
        if (eeX.VendorID == 0 | eeX.ProductID == 0)
          return FT_STATUS.FT_INVALID_PARAMETER;
        var structure = new FT_XSERIES_DATA();
        var numArray1 = new byte[32];
        var numArray2 = new byte[16];
        var numArray3 = new byte[64];
        var numArray4 = new byte[16];
        if (eeX.Manufacturer.Length > 32)
          eeX.Manufacturer = eeX.Manufacturer.Substring(0, 32);
        if (eeX.ManufacturerID.Length > 16)
          eeX.ManufacturerID = eeX.ManufacturerID.Substring(0, 16);
        if (eeX.Description.Length > 64)
          eeX.Description = eeX.Description.Substring(0, 64);
        if (eeX.SerialNumber.Length > 16)
          eeX.SerialNumber = eeX.SerialNumber.Substring(0, 16);
        var utF8Encoding = new UTF8Encoding();
        var bytes1 = utF8Encoding.GetBytes(eeX.Manufacturer);
        var bytes2 = utF8Encoding.GetBytes(eeX.ManufacturerID);
        var bytes3 = utF8Encoding.GetBytes(eeX.Description);
        var bytes4 = utF8Encoding.GetBytes(eeX.SerialNumber);
        structure.common.deviceType = 9U;
        structure.common.VendorId = eeX.VendorID;
        structure.common.ProductId = eeX.ProductID;
        structure.common.MaxPower = eeX.MaxPower;
        structure.common.SelfPowered = Convert.ToByte(eeX.SelfPowered);
        structure.common.RemoteWakeup = Convert.ToByte(eeX.RemoteWakeup);
        structure.common.SerNumEnable = Convert.ToByte(eeX.SerNumEnable);
        structure.common.PullDownEnable = Convert.ToByte(eeX.PullDownEnable);
        structure.Cbus0 = eeX.Cbus0;
        structure.Cbus1 = eeX.Cbus1;
        structure.Cbus2 = eeX.Cbus2;
        structure.Cbus3 = eeX.Cbus3;
        structure.Cbus4 = eeX.Cbus4;
        structure.Cbus5 = eeX.Cbus5;
        structure.Cbus6 = eeX.Cbus6;
        structure.ACDriveCurrent = eeX.ACDriveCurrent;
        structure.ACSchmittInput = eeX.ACSchmittInput;
        structure.ACSlowSlew = eeX.ACSlowSlew;
        structure.ADDriveCurrent = eeX.ADDriveCurrent;
        structure.ADSchmittInput = eeX.ADSchmittInput;
        structure.ADSlowSlew = eeX.ADSlowSlew;
        structure.BCDDisableSleep = eeX.BCDDisableSleep;
        structure.BCDEnable = eeX.BCDEnable;
        structure.BCDForceCbusPWREN = eeX.BCDForceCbusPWREN;
        structure.FT1248Cpol = eeX.FT1248Cpol;
        structure.FT1248FlowControl = eeX.FT1248FlowControl;
        structure.FT1248Lsb = eeX.FT1248Lsb;
        structure.I2CDeviceId = eeX.I2CDeviceId;
        structure.I2CDisableSchmitt = eeX.I2CDisableSchmitt;
        structure.I2CSlaveAddress = eeX.I2CSlaveAddress;
        structure.InvertCTS = eeX.InvertCTS;
        structure.InvertDCD = eeX.InvertDCD;
        structure.InvertDSR = eeX.InvertDSR;
        structure.InvertDTR = eeX.InvertDTR;
        structure.InvertRI = eeX.InvertRI;
        structure.InvertRTS = eeX.InvertRTS;
        structure.InvertRXD = eeX.InvertRXD;
        structure.InvertTXD = eeX.InvertTXD;
        structure.PowerSaveEnable = eeX.PowerSaveEnable;
        structure.RS485EchoSuppress = eeX.RS485EchoSuppress;
        structure.DriverType = eeX.IsVCP;
        var num1 = Marshal.SizeOf((object) structure);
        var num2 = Marshal.AllocHGlobal(num1);
        Marshal.StructureToPtr((object) structure, num2, false);
        ftStatus = forFunctionPointer(ftHandle, num2, (uint) num1, bytes1, bytes2, bytes3, bytes4);
      }
      return ftStatus;
    }

    /// <summary>Reads data from the user area of the device EEPROM.</summary>
    /// <returns>FT_STATUS from FT_UARead in FTD2XX.DLL</returns>
    /// <param name="UserAreaDataBuffer">An array of bytes which will be populated with the data read from the device EEPROM user area.</param>
    /// <param name="numBytesRead">The number of bytes actually read from the EEPROM user area.</param>
    public FT_STATUS EEReadUserArea(byte[] UserAreaDataBuffer, ref uint numBytesRead)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_EE_UASize != IntPtr.Zero & pFT_EE_UARead != IntPtr.Zero)
      {
        var forFunctionPointer1 = (tFT_EE_UASize) Marshal.GetDelegateForFunctionPointer(pFT_EE_UASize, typeof (tFT_EE_UASize));
        var forFunctionPointer2 = (tFT_EE_UARead) Marshal.GetDelegateForFunctionPointer(pFT_EE_UARead, typeof (tFT_EE_UARead));
        if (ftHandle != IntPtr.Zero)
        {
          uint dwSize = 0;
          ftStatus = forFunctionPointer1(ftHandle, ref dwSize);
          if (UserAreaDataBuffer.Length >= dwSize)
            ftStatus = forFunctionPointer2(ftHandle, UserAreaDataBuffer, UserAreaDataBuffer.Length, ref numBytesRead);
        }
      }
      else
      {
        if (pFT_EE_UASize == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_EE_UASize.");
        if (pFT_EE_UARead == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_EE_UARead.");
      }
      return ftStatus;
    }

    /// <summary>Writes data to the user area of the device EEPROM.</summary>
    /// <returns>FT_STATUS value from FT_UAWrite in FTD2XX.DLL</returns>
    /// <param name="UserAreaDataBuffer">An array of bytes which will be written to the device EEPROM user area.</param>
    public FT_STATUS EEWriteUserArea(byte[] UserAreaDataBuffer)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_EE_UASize != IntPtr.Zero & pFT_EE_UAWrite != IntPtr.Zero)
      {
        var forFunctionPointer1 = (tFT_EE_UASize) Marshal.GetDelegateForFunctionPointer(pFT_EE_UASize, typeof (tFT_EE_UASize));
        var forFunctionPointer2 = (tFT_EE_UAWrite) Marshal.GetDelegateForFunctionPointer(pFT_EE_UAWrite, typeof (tFT_EE_UAWrite));
        if (ftHandle != IntPtr.Zero)
        {
          uint dwSize = 0;
          ftStatus = forFunctionPointer1(ftHandle, ref dwSize);
          if (UserAreaDataBuffer.Length <= dwSize)
            ftStatus = forFunctionPointer2(ftHandle, UserAreaDataBuffer, UserAreaDataBuffer.Length);
        }
      }
      else
      {
        if (pFT_EE_UASize == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_EE_UASize.");
        if (pFT_EE_UAWrite == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_EE_UAWrite.");
      }
      return ftStatus;
    }

    /// <summary>Gets the chip type of the current device.</summary>
    /// <returns>FT_STATUS value from FT_GetDeviceInfo in FTD2XX.DLL</returns>
    /// <param name="DeviceType">The FTDI chip type of the current device.</param>
    public FT_STATUS GetDeviceType(ref FT_DEVICE DeviceType)
    {
      var deviceType = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return deviceType;
      if (pFT_GetDeviceInfo != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_GetDeviceInfo) Marshal.GetDelegateForFunctionPointer(pFT_GetDeviceInfo, typeof (tFT_GetDeviceInfo));
        uint lpdwID = 0;
        var pcSerialNumber = new byte[16];
        var pcDescription = new byte[64];
        DeviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
        if (ftHandle != IntPtr.Zero)
          deviceType = forFunctionPointer(ftHandle, ref DeviceType, ref lpdwID, pcSerialNumber, pcDescription, IntPtr.Zero);
      }
      else if (pFT_GetDeviceInfo == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_GetDeviceInfo.");
      return deviceType;
    }

    /// <summary>
    /// Gets the Vendor ID and Product ID of the current device.
    /// </summary>
    /// <returns>FT_STATUS value from FT_GetDeviceInfo in FTD2XX.DLL</returns>
    /// <param name="DeviceID">The device ID (Vendor ID and Product ID) of the current device.</param>
    public FT_STATUS GetDeviceID(ref uint DeviceID)
    {
      var deviceId = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return deviceId;
      if (pFT_GetDeviceInfo != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_GetDeviceInfo) Marshal.GetDelegateForFunctionPointer(pFT_GetDeviceInfo, typeof (tFT_GetDeviceInfo));
        var pftType = FT_DEVICE.FT_DEVICE_UNKNOWN;
        var pcSerialNumber = new byte[16];
        var pcDescription = new byte[64];
        if (ftHandle != IntPtr.Zero)
          deviceId = forFunctionPointer(ftHandle, ref pftType, ref DeviceID, pcSerialNumber, pcDescription, IntPtr.Zero);
      }
      else if (pFT_GetDeviceInfo == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_GetDeviceInfo.");
      return deviceId;
    }

    /// <summary>Gets the description of the current device.</summary>
    /// <returns>FT_STATUS value from FT_GetDeviceInfo in FTD2XX.DLL</returns>
    /// <param name="Description">The description of the current device.</param>
    public FT_STATUS GetDescription(out string Description)
    {
      var description = FT_STATUS.FT_OTHER_ERROR;
      Description = string.Empty;
      if (hFTD2XXDLL == IntPtr.Zero)
        return description;
      if (pFT_GetDeviceInfo != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_GetDeviceInfo) Marshal.GetDelegateForFunctionPointer(pFT_GetDeviceInfo, typeof (tFT_GetDeviceInfo));
        uint lpdwID = 0;
        var pftType = FT_DEVICE.FT_DEVICE_UNKNOWN;
        var pcSerialNumber = new byte[16];
        var numArray = new byte[64];
        if (ftHandle != IntPtr.Zero)
        {
          description = forFunctionPointer(ftHandle, ref pftType, ref lpdwID, pcSerialNumber, numArray, IntPtr.Zero);
          Description = Encoding.ASCII.GetString(numArray);
          Description = Description.Substring(0, Description.IndexOf("\0"));
        }
      }
      else if (pFT_GetDeviceInfo == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_GetDeviceInfo.");
      return description;
    }

    /// <summary>Gets the serial number of the current device.</summary>
    /// <returns>FT_STATUS value from FT_GetDeviceInfo in FTD2XX.DLL</returns>
    /// <param name="SerialNumber">The serial number of the current device.</param>
    public FT_STATUS GetSerialNumber(out string SerialNumber)
    {
      var serialNumber = FT_STATUS.FT_OTHER_ERROR;
      SerialNumber = string.Empty;
      if (hFTD2XXDLL == IntPtr.Zero)
        return serialNumber;
      if (pFT_GetDeviceInfo != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_GetDeviceInfo) Marshal.GetDelegateForFunctionPointer(pFT_GetDeviceInfo, typeof (tFT_GetDeviceInfo));
        uint lpdwID = 0;
        var pftType = FT_DEVICE.FT_DEVICE_UNKNOWN;
        var numArray = new byte[16];
        var pcDescription = new byte[64];
        if (ftHandle != IntPtr.Zero)
        {
          serialNumber = forFunctionPointer(ftHandle, ref pftType, ref lpdwID, numArray, pcDescription, IntPtr.Zero);
          
          var firstNullIndex = Array.FindIndex(numArray, b => b == 0);
          SerialNumber = Encoding.Default.GetString(numArray, 0, firstNullIndex);
          
        }
      }
      else if (pFT_GetDeviceInfo == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_GetDeviceInfo.");
      return serialNumber;
    }

    /// <summary>
    /// Gets the number of bytes available in the receive buffer.
    /// </summary>
    /// <returns>FT_STATUS value from FT_GetQueueStatus in FTD2XX.DLL</returns>
    /// <param name="RxQueue">The number of bytes available to be read.</param>
    public FT_STATUS GetRxBytesAvailable(ref uint RxQueue)
    {
      var rxBytesAvailable = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return rxBytesAvailable;
      if (pFT_GetQueueStatus != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_GetQueueStatus) Marshal.GetDelegateForFunctionPointer(pFT_GetQueueStatus, typeof (tFT_GetQueueStatus));
        if (ftHandle != IntPtr.Zero)
          rxBytesAvailable = forFunctionPointer(ftHandle, ref RxQueue);
      }
      else if (pFT_GetQueueStatus == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_GetQueueStatus.");
      return rxBytesAvailable;
    }

    /// <summary>
    /// Gets the number of bytes waiting in the transmit buffer.
    /// </summary>
    /// <returns>FT_STATUS value from FT_GetStatus in FTD2XX.DLL</returns>
    /// <param name="TxQueue">The number of bytes waiting to be sent.</param>
    public FT_STATUS GetTxBytesWaiting(ref uint TxQueue)
    {
      var txBytesWaiting = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return txBytesWaiting;
      if (pFT_GetStatus != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_GetStatus) Marshal.GetDelegateForFunctionPointer(pFT_GetStatus, typeof (tFT_GetStatus));
        uint lpdwAmountInRxQueue = 0;
        uint lpdwEventStatus = 0;
        if (ftHandle != IntPtr.Zero)
          txBytesWaiting = forFunctionPointer(ftHandle, ref lpdwAmountInRxQueue, ref TxQueue, ref lpdwEventStatus);
      }
      else if (pFT_GetStatus == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_GetStatus.");
      return txBytesWaiting;
    }

    /// <summary>
    /// Gets the event type after an event has fired.  Can be used to distinguish which event has been triggered when waiting on multiple event types.
    /// </summary>
    /// <returns>FT_STATUS value from FT_GetStatus in FTD2XX.DLL</returns>
    /// <param name="EventType">The type of event that has occurred.</param>
    public FT_STATUS GetEventType(ref uint EventType)
    {
      var eventType = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return eventType;
      if (pFT_GetStatus != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_GetStatus) Marshal.GetDelegateForFunctionPointer(pFT_GetStatus, typeof (tFT_GetStatus));
        uint lpdwAmountInRxQueue = 0;
        uint lpdwAmountInTxQueue = 0;
        if (ftHandle != IntPtr.Zero)
          eventType = forFunctionPointer(ftHandle, ref lpdwAmountInRxQueue, ref lpdwAmountInTxQueue, ref EventType);
      }
      else if (pFT_GetStatus == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_GetStatus.");
      return eventType;
    }

    /// <summary>Gets the current modem status.</summary>
    /// <returns>FT_STATUS value from FT_GetModemStatus in FTD2XX.DLL</returns>
    /// <param name="ModemStatus">A bit map representaion of the current modem status.</param>
    public FT_STATUS GetModemStatus(ref byte ModemStatus)
    {
      var modemStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return modemStatus;
      if (pFT_GetModemStatus != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_GetModemStatus) Marshal.GetDelegateForFunctionPointer(pFT_GetModemStatus, typeof (tFT_GetModemStatus));
        uint lpdwModemStatus = 0;
        if (ftHandle != IntPtr.Zero)
          modemStatus = forFunctionPointer(ftHandle, ref lpdwModemStatus);
        ModemStatus = Convert.ToByte(lpdwModemStatus & byte.MaxValue);
      }
      else if (pFT_GetModemStatus == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_GetModemStatus.");
      return modemStatus;
    }

    /// <summary>Gets the current line status.</summary>
    /// <returns>FT_STATUS value from FT_GetModemStatus in FTD2XX.DLL</returns>
    /// <param name="LineStatus">A bit map representaion of the current line status.</param>
    public FT_STATUS GetLineStatus(ref byte LineStatus)
    {
      var lineStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return lineStatus;
      if (pFT_GetModemStatus != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_GetModemStatus) Marshal.GetDelegateForFunctionPointer(pFT_GetModemStatus, typeof (tFT_GetModemStatus));
        uint lpdwModemStatus = 0;
        if (ftHandle != IntPtr.Zero)
          lineStatus = forFunctionPointer(ftHandle, ref lpdwModemStatus);
        LineStatus = Convert.ToByte(lpdwModemStatus >> 8 & byte.MaxValue);
      }
      else if (pFT_GetModemStatus == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_GetModemStatus.");
      return lineStatus;
    }

    /// <summary>Sets the current Baud rate.</summary>
    /// <returns>FT_STATUS value from FT_SetBaudRate in FTD2XX.DLL</returns>
    /// <param name="BaudRate">The desired Baud rate for the device.</param>
    public FT_STATUS SetBaudRate(uint BaudRate)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_SetBaudRate != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_SetBaudRate) Marshal.GetDelegateForFunctionPointer(pFT_SetBaudRate, typeof (tFT_SetBaudRate));
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle, BaudRate);
      }
      else if (pFT_SetBaudRate == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_SetBaudRate.");
      return ftStatus;
    }

    /// <summary>
    /// Sets the data bits, stop bits and parity for the device.
    /// </summary>
    /// <returns>FT_STATUS value from FT_SetDataCharacteristics in FTD2XX.DLL</returns>
    /// <param name="DataBits">The number of data bits for UART data.  Valid values are FT_DATA_BITS.FT_DATA_7 or FT_DATA_BITS.FT_BITS_8</param>
    /// <param name="StopBits">The number of stop bits for UART data.  Valid values are FT_STOP_BITS.FT_STOP_BITS_1 or FT_STOP_BITS.FT_STOP_BITS_2</param>
    /// <param name="Parity">The parity of the UART data.  Valid values are FT_PARITY.FT_PARITY_NONE, FT_PARITY.FT_PARITY_ODD, FT_PARITY.FT_PARITY_EVEN, FT_PARITY.FT_PARITY_MARK or FT_PARITY.FT_PARITY_SPACE</param>
    public FT_STATUS SetDataCharacteristics(byte DataBits, byte StopBits, byte Parity)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_SetDataCharacteristics != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_SetDataCharacteristics) Marshal.GetDelegateForFunctionPointer(pFT_SetDataCharacteristics, typeof (tFT_SetDataCharacteristics));
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle, DataBits, StopBits, Parity);
      }
      else if (pFT_SetDataCharacteristics == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_SetDataCharacteristics.");
      return ftStatus;
    }

    /// <summary>Sets the flow control type.</summary>
    /// <returns>FT_STATUS value from FT_SetFlowControl in FTD2XX.DLL</returns>
    /// <param name="FlowControl">The type of flow control for the UART.  Valid values are FT_FLOW_CONTROL.FT_FLOW_NONE, FT_FLOW_CONTROL.FT_FLOW_RTS_CTS, FT_FLOW_CONTROL.FT_FLOW_DTR_DSR or FT_FLOW_CONTROL.FT_FLOW_XON_XOFF</param>
    /// <param name="Xon">The Xon character for Xon/Xoff flow control.  Ignored if not using Xon/XOff flow control.</param>
    /// <param name="Xoff">The Xoff character for Xon/Xoff flow control.  Ignored if not using Xon/XOff flow control.</param>
    public FT_STATUS SetFlowControl(ushort FlowControl, byte Xon, byte Xoff)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_SetFlowControl != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_SetFlowControl) Marshal.GetDelegateForFunctionPointer(pFT_SetFlowControl, typeof (tFT_SetFlowControl));
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle, FlowControl, Xon, Xoff);
      }
      else if (pFT_SetFlowControl == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_SetFlowControl.");
      return ftStatus;
    }

    /// <summary>Asserts or de-asserts the Request To Send (RTS) line.</summary>
    /// <returns>FT_STATUS value from FT_SetRts or FT_ClrRts in FTD2XX.DLL</returns>
    /// <param name="Enable">If true, asserts RTS.  If false, de-asserts RTS</param>
    public FT_STATUS SetRTS(bool Enable)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_SetRts != IntPtr.Zero & pFT_ClrRts != IntPtr.Zero)
      {
        var forFunctionPointer1 = (tFT_SetRts) Marshal.GetDelegateForFunctionPointer(pFT_SetRts, typeof (tFT_SetRts));
        var forFunctionPointer2 = (tFT_ClrRts) Marshal.GetDelegateForFunctionPointer(pFT_ClrRts, typeof (tFT_ClrRts));
        if (ftHandle != IntPtr.Zero)
          ftStatus = !Enable ? forFunctionPointer2(ftHandle) : forFunctionPointer1(ftHandle);
      }
      else
      {
        if (pFT_SetRts == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_SetRts.");
        if (pFT_ClrRts == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_ClrRts.");
      }
      return ftStatus;
    }

    /// <summary>
    /// Asserts or de-asserts the Data Terminal Ready (DTR) line.
    /// </summary>
    /// <returns>FT_STATUS value from FT_SetDtr or FT_ClrDtr in FTD2XX.DLL</returns>
    /// <param name="Enable">If true, asserts DTR.  If false, de-asserts DTR.</param>
    public FT_STATUS SetDTR(bool Enable)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_SetDtr != IntPtr.Zero & pFT_ClrDtr != IntPtr.Zero)
      {
        var forFunctionPointer1 = (tFT_SetDtr) Marshal.GetDelegateForFunctionPointer(pFT_SetDtr, typeof (tFT_SetDtr));
        var forFunctionPointer2 = (tFT_ClrDtr) Marshal.GetDelegateForFunctionPointer(pFT_ClrDtr, typeof (tFT_ClrDtr));
        if (ftHandle != IntPtr.Zero)
          ftStatus = !Enable ? forFunctionPointer2(ftHandle) : forFunctionPointer1(ftHandle);
      }
      else
      {
        if (pFT_SetDtr == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_SetDtr.");
        if (pFT_ClrDtr == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_ClrDtr.");
      }
      return ftStatus;
    }

    /// <summary>Sets the read and write timeout values.</summary>
    /// <returns>FT_STATUS value from FT_SetTimeouts in FTD2XX.DLL</returns>
    /// <param name="ReadTimeout">Read timeout value in ms.  A value of 0 indicates an infinite timeout.</param>
    /// <param name="WriteTimeout">Write timeout value in ms.  A value of 0 indicates an infinite timeout.</param>
    public FT_STATUS SetTimeouts(uint ReadTimeout, uint WriteTimeout)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_SetTimeouts != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_SetTimeouts) Marshal.GetDelegateForFunctionPointer(pFT_SetTimeouts, typeof (tFT_SetTimeouts));
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle, ReadTimeout, WriteTimeout);
      }
      else if (pFT_SetTimeouts == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_SetTimeouts.");
      return ftStatus;
    }

    /// <summary>Sets or clears the break state.</summary>
    /// <returns>FT_STATUS value from FT_SetBreakOn or FT_SetBreakOff in FTD2XX.DLL</returns>
    /// <param name="Enable">If true, sets break on.  If false, sets break off.</param>
    public FT_STATUS SetBreak(bool Enable)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_SetBreakOn != IntPtr.Zero & pFT_SetBreakOff != IntPtr.Zero)
      {
        var forFunctionPointer1 = (tFT_SetBreakOn) Marshal.GetDelegateForFunctionPointer(pFT_SetBreakOn, typeof (tFT_SetBreakOn));
        var forFunctionPointer2 = (tFT_SetBreakOff) Marshal.GetDelegateForFunctionPointer(pFT_SetBreakOff, typeof (tFT_SetBreakOff));
        if (ftHandle != IntPtr.Zero)
          ftStatus = !Enable ? forFunctionPointer2(ftHandle) : forFunctionPointer1(ftHandle);
      }
      else
      {
        if (pFT_SetBreakOn == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_SetBreakOn.");
        if (pFT_SetBreakOff == IntPtr.Zero)
          Console.WriteLine("Failed to load function FT_SetBreakOff.");
      }
      return ftStatus;
    }

    /// <summary>
    /// Gets or sets the reset pipe retry count.  Default value is 50.
    /// </summary>
    /// <returns>FT_STATUS vlaue from FT_SetResetPipeRetryCount in FTD2XX.DLL</returns>
    /// <param name="ResetPipeRetryCount">The reset pipe retry count.
    /// Electrically noisy environments may benefit from a larger value.</param>
    public FT_STATUS SetResetPipeRetryCount(uint ResetPipeRetryCount)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_SetResetPipeRetryCount != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_SetResetPipeRetryCount) Marshal.GetDelegateForFunctionPointer(pFT_SetResetPipeRetryCount, typeof (tFT_SetResetPipeRetryCount));
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle, ResetPipeRetryCount);
      }
      else if (pFT_SetResetPipeRetryCount == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_SetResetPipeRetryCount.");
      return ftStatus;
    }

    /// <summary>Gets the current FTDIBUS.SYS driver version number.</summary>
    /// <returns>FT_STATUS value from FT_GetDriverVersion in FTD2XX.DLL</returns>
    /// <param name="DriverVersion">The current driver version number.</param>
    public FT_STATUS GetDriverVersion(ref uint DriverVersion)
    {
      var driverVersion = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return driverVersion;
      if (pFT_GetDriverVersion != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_GetDriverVersion) Marshal.GetDelegateForFunctionPointer(pFT_GetDriverVersion, typeof (tFT_GetDriverVersion));
        if (ftHandle != IntPtr.Zero)
          driverVersion = forFunctionPointer(ftHandle, ref DriverVersion);
      }
      else if (pFT_GetDriverVersion == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_GetDriverVersion.");
      return driverVersion;
    }

    /// <summary>Gets the current FTD2XX.DLL driver version number.</summary>
    /// <returns>FT_STATUS value from FT_GetLibraryVersion in FTD2XX.DLL</returns>
    /// <param name="LibraryVersion">The current library version.</param>
    public FT_STATUS GetLibraryVersion(ref uint LibraryVersion)
    {
      var libraryVersion = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return libraryVersion;
      if (pFT_GetLibraryVersion != IntPtr.Zero)
        libraryVersion = ((tFT_GetLibraryVersion) Marshal.GetDelegateForFunctionPointer(pFT_GetLibraryVersion, typeof (tFT_GetLibraryVersion)))(ref LibraryVersion);
      else if (pFT_GetLibraryVersion == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_GetLibraryVersion.");
      return libraryVersion;
    }

    /// <summary>
    /// Sets the USB deadman timeout value.  Default is 5000ms.
    /// </summary>
    /// <returns>FT_STATUS value from FT_SetDeadmanTimeout in FTD2XX.DLL</returns>
    /// <param name="DeadmanTimeout">The deadman timeout value in ms.  Default is 5000ms.</param>
    public FT_STATUS SetDeadmanTimeout(uint DeadmanTimeout)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_SetDeadmanTimeout != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_SetDeadmanTimeout) Marshal.GetDelegateForFunctionPointer(pFT_SetDeadmanTimeout, typeof (tFT_SetDeadmanTimeout));
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle, DeadmanTimeout);
      }
      else if (pFT_SetDeadmanTimeout == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_SetDeadmanTimeout.");
      return ftStatus;
    }

    /// <summary>
    /// Sets the value of the latency timer.  Default value is 16ms.
    /// </summary>
    /// <returns>FT_STATUS value from FT_SetLatencyTimer in FTD2XX.DLL</returns>
    /// <param name="Latency">The latency timer value in ms.
    /// Valid values are 2ms - 255ms for FT232BM, FT245BM and FT2232 devices.
    /// Valid values are 0ms - 255ms for other devices.</param>
    public FT_STATUS SetLatency(byte Latency)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_SetLatencyTimer != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_SetLatencyTimer) Marshal.GetDelegateForFunctionPointer(pFT_SetLatencyTimer, typeof (tFT_SetLatencyTimer));
        if (ftHandle != IntPtr.Zero)
        {
          var DeviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
          var deviceType = (int) GetDeviceType(ref DeviceType);
          if ((DeviceType == FT_DEVICE.FT_DEVICE_BM || DeviceType == FT_DEVICE.FT_DEVICE_2232) && Latency < 2)
            Latency = 2;
          ftStatus = forFunctionPointer(ftHandle, Latency);
        }
      }
      else if (pFT_SetLatencyTimer == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_SetLatencyTimer.");
      return ftStatus;
    }

    /// <summary>
    /// Gets the value of the latency timer.  Default value is 16ms.
    /// </summary>
    /// <returns>FT_STATUS value from FT_GetLatencyTimer in FTD2XX.DLL</returns>
    /// <param name="Latency">The latency timer value in ms.</param>
    public FT_STATUS GetLatency(ref byte Latency)
    {
      var latency = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return latency;
      if (pFT_GetLatencyTimer != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_GetLatencyTimer) Marshal.GetDelegateForFunctionPointer(pFT_GetLatencyTimer, typeof (tFT_GetLatencyTimer));
        if (ftHandle != IntPtr.Zero)
          latency = forFunctionPointer(ftHandle, ref Latency);
      }
      else if (pFT_GetLatencyTimer == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_GetLatencyTimer.");
      return latency;
    }

    /// <summary>Sets the USB IN and OUT transfer sizes.</summary>
    /// <returns>FT_STATUS value from FT_SetUSBParameters in FTD2XX.DLL</returns>
    /// <param name="InTransferSize">The USB IN transfer size in bytes.</param>
    public FT_STATUS InTransferSize(uint InTransferSize)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_SetUSBParameters != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_SetUSBParameters) Marshal.GetDelegateForFunctionPointer(pFT_SetUSBParameters, typeof (tFT_SetUSBParameters));
        var dwOutTransferSize = InTransferSize;
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle, InTransferSize, dwOutTransferSize);
      }
      else if (pFT_SetUSBParameters == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_SetUSBParameters.");
      return ftStatus;
    }

    /// <summary>
    /// Sets an event character, an error character and enables or disables them.
    /// </summary>
    /// <returns>FT_STATUS value from FT_SetChars in FTD2XX.DLL</returns>
    /// <param name="EventChar">A character that will be tigger an IN to the host when this character is received.</param>
    /// <param name="EventCharEnable">Determines if the EventChar is enabled or disabled.</param>
    /// <param name="ErrorChar">A character that will be inserted into the data stream to indicate that an error has occurred.</param>
    /// <param name="ErrorCharEnable">Determines if the ErrorChar is enabled or disabled.</param>
    public FT_STATUS SetCharacters(
      byte EventChar,
      bool EventCharEnable,
      byte ErrorChar,
      bool ErrorCharEnable)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_SetChars != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_SetChars) Marshal.GetDelegateForFunctionPointer(pFT_SetChars, typeof (tFT_SetChars));
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle, EventChar, Convert.ToByte(EventCharEnable), ErrorChar, Convert.ToByte(ErrorCharEnable));
      }
      else if (pFT_SetChars == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_SetChars.");
      return ftStatus;
    }

    /// <summary>Gets the size of the EEPROM user area.</summary>
    /// <returns>FT_STATUS value from FT_EE_UASize in FTD2XX.DLL</returns>
    /// <param name="UASize">The EEPROM user area size in bytes.</param>
    public FT_STATUS EEUserAreaSize(ref uint UASize)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_EE_UASize != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_EE_UASize) Marshal.GetDelegateForFunctionPointer(pFT_EE_UASize, typeof (tFT_EE_UASize));
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle, ref UASize);
      }
      else if (pFT_EE_UASize == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_EE_UASize.");
      return ftStatus;
    }

    /// <summary>
    /// Gets the corresponding COM port number for the current device.  If no COM port is exposed, an empty string is returned.
    /// </summary>
    /// <returns>FT_STATUS value from FT_GetComPortNumber in FTD2XX.DLL</returns>
    /// <param name="ComPortName">The COM port name corresponding to the current device.  If no COM port is installed, an empty string is passed back.</param>
    public FT_STATUS GetCOMPort(out string ComPortName)
    {
      var comPort = FT_STATUS.FT_OTHER_ERROR;
      ComPortName = string.Empty;
      if (hFTD2XXDLL == IntPtr.Zero)
        return comPort;
      if (pFT_GetComPortNumber != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_GetComPortNumber) Marshal.GetDelegateForFunctionPointer(pFT_GetComPortNumber, typeof (tFT_GetComPortNumber));
        var dwComPortNumber = -1;
        if (ftHandle != IntPtr.Zero)
          comPort = forFunctionPointer(ftHandle, ref dwComPortNumber);
        ComPortName = dwComPortNumber != -1 ? "COM" + dwComPortNumber.ToString() : string.Empty;
      }
      else if (pFT_GetComPortNumber == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_GetComPortNumber.");
      return comPort;
    }

    /// <summary>
    /// Get data from the FT4222 using the vendor command interface.
    /// </summary>
    /// <returns>FT_STATUS value from FT_VendorCmdSet in FTD2XX.DLL</returns>
    public FT_STATUS VendorCmdGet(ushort request, byte[] buf, ushort len)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_VendorCmdGet != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_VendorCmdGet) Marshal.GetDelegateForFunctionPointer(pFT_VendorCmdGet, typeof (tFT_VendorCmdGet));
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle, request, buf, len);
      }
      else if (pFT_VendorCmdGet == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_VendorCmdGet.");
      return ftStatus;
    }

    /// <summary>
    /// Set data from the FT4222 using the vendor command interface.
    /// </summary>
    /// <returns>FT_STATUS value from FT_VendorCmdSet in FTD2XX.DLL</returns>
    public FT_STATUS VendorCmdSet(ushort request, byte[] buf, ushort len)
    {
      var ftStatus = FT_STATUS.FT_OTHER_ERROR;
      if (hFTD2XXDLL == IntPtr.Zero)
        return ftStatus;
      if (pFT_VendorCmdSet != IntPtr.Zero)
      {
        var forFunctionPointer = (tFT_VendorCmdSet) Marshal.GetDelegateForFunctionPointer(pFT_VendorCmdSet, typeof (tFT_VendorCmdSet));
        if (ftHandle != IntPtr.Zero)
          ftStatus = forFunctionPointer(ftHandle, request, buf, len);
      }
      else if (pFT_VendorCmdSet == IntPtr.Zero)
        Console.WriteLine("Failed to load function FT_VendorCmdSet.");
      return ftStatus;
    }

    /// <summary>Gets the open status of the device.</summary>
    public bool IsOpen => !(ftHandle == IntPtr.Zero);

    /// <summary>Gets the interface identifier.</summary>
    private string InterfaceIdentifier
    {
      get
      {
        var empty = string.Empty;
        if (IsOpen)
        {
          var DeviceType = FT_DEVICE.FT_DEVICE_BM;
          var deviceType = (int) GetDeviceType(ref DeviceType);
          if (DeviceType == FT_DEVICE.FT_DEVICE_2232 | DeviceType == FT_DEVICE.FT_DEVICE_2232H | DeviceType == FT_DEVICE.FT_DEVICE_4232H)
          {
            string Description;
            var description = (int) GetDescription(out Description);
            return Description.Substring(Description.Length - 1);
          }
        }
        return empty;
      }
    }

    /// <summary>
    /// Method to check ftStatus and ftErrorCondition values for error conditions and throw exceptions accordingly.
    /// </summary>
    private void ErrorHandler(FT_STATUS ftStatus, FT_ERROR ftErrorCondition)
    {
      switch (ftStatus)
      {
        case FT_STATUS.FT_INVALID_HANDLE:
          throw new FT_EXCEPTION("Invalid handle for FTDI device.");
        case FT_STATUS.FT_DEVICE_NOT_FOUND:
          throw new FT_EXCEPTION("FTDI device not found.");
        case FT_STATUS.FT_DEVICE_NOT_OPENED:
          throw new FT_EXCEPTION("FTDI device not opened.");
        case FT_STATUS.FT_IO_ERROR:
          throw new FT_EXCEPTION("FTDI device IO error.");
        case FT_STATUS.FT_INSUFFICIENT_RESOURCES:
          throw new FT_EXCEPTION("Insufficient resources.");
        case FT_STATUS.FT_INVALID_PARAMETER:
          throw new FT_EXCEPTION("Invalid parameter for FTD2XX function call.");
        case FT_STATUS.FT_INVALID_BAUD_RATE:
          throw new FT_EXCEPTION("Invalid Baud rate for FTDI device.");
        case FT_STATUS.FT_DEVICE_NOT_OPENED_FOR_ERASE:
          throw new FT_EXCEPTION("FTDI device not opened for erase.");
        case FT_STATUS.FT_DEVICE_NOT_OPENED_FOR_WRITE:
          throw new FT_EXCEPTION("FTDI device not opened for write.");
        case FT_STATUS.FT_FAILED_TO_WRITE_DEVICE:
          throw new FT_EXCEPTION("Failed to write to FTDI device.");
        case FT_STATUS.FT_EEPROM_READ_FAILED:
          throw new FT_EXCEPTION("Failed to read FTDI device EEPROM.");
        case FT_STATUS.FT_EEPROM_WRITE_FAILED:
          throw new FT_EXCEPTION("Failed to write FTDI device EEPROM.");
        case FT_STATUS.FT_EEPROM_ERASE_FAILED:
          throw new FT_EXCEPTION("Failed to erase FTDI device EEPROM.");
        case FT_STATUS.FT_EEPROM_NOT_PRESENT:
          throw new FT_EXCEPTION("No EEPROM fitted to FTDI device.");
        case FT_STATUS.FT_EEPROM_NOT_PROGRAMMED:
          throw new FT_EXCEPTION("FTDI device EEPROM not programmed.");
        case FT_STATUS.FT_INVALID_ARGS:
          throw new FT_EXCEPTION("Invalid arguments for FTD2XX function call.");
        case FT_STATUS.FT_OTHER_ERROR:
          throw new FT_EXCEPTION("An unexpected error has occurred when trying to communicate with the FTDI device.");
        default:
          switch (ftErrorCondition)
          {
            case FT_ERROR.FT_NO_ERROR:
              return;
            case FT_ERROR.FT_INCORRECT_DEVICE:
              throw new FT_EXCEPTION("The current device type does not match the EEPROM structure.");
            case FT_ERROR.FT_INVALID_BITMODE:
              throw new FT_EXCEPTION("The requested bit mode is not valid for the current device.");
            case FT_ERROR.FT_BUFFER_SIZE:
              throw new FT_EXCEPTION("The supplied buffer is not big enough.");
            default:
              return;
          }
      }
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_CreateDeviceInfoList(ref uint numdevs);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_GetDeviceInfoDetail(
      uint index,
      ref uint flags,
      ref FT_DEVICE chiptype,
      ref uint id,
      ref uint locid,
      byte[] serialnumber,
      byte[] description,
      ref IntPtr ftHandle);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_Open(uint index, ref IntPtr ftHandle);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_OpenEx(string devstring, uint dwFlags, ref IntPtr ftHandle);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_OpenExLoc(uint devloc, uint dwFlags, ref IntPtr ftHandle);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_Close(IntPtr ftHandle);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_Read(
      IntPtr ftHandle,
      byte[] lpBuffer,
      uint dwBytesToRead,
      ref uint lpdwBytesReturned);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_Write(
      IntPtr ftHandle,
      byte[] lpBuffer,
      uint dwBytesToWrite,
      ref uint lpdwBytesWritten);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_GetQueueStatus(
      IntPtr ftHandle,
      ref uint lpdwAmountInRxQueue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_GetModemStatus(IntPtr ftHandle, ref uint lpdwModemStatus);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_GetStatus(
      IntPtr ftHandle,
      ref uint lpdwAmountInRxQueue,
      ref uint lpdwAmountInTxQueue,
      ref uint lpdwEventStatus);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_SetBaudRate(IntPtr ftHandle, uint dwBaudRate);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_SetDataCharacteristics(
      IntPtr ftHandle,
      byte uWordLength,
      byte uStopBits,
      byte uParity);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_SetFlowControl(
      IntPtr ftHandle,
      ushort usFlowControl,
      byte uXon,
      byte uXoff);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_SetDtr(IntPtr ftHandle);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_ClrDtr(IntPtr ftHandle);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_SetRts(IntPtr ftHandle);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_ClrRts(IntPtr ftHandle);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_ResetDevice(IntPtr ftHandle);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_ResetPort(IntPtr ftHandle);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_CyclePort(IntPtr ftHandle);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_Rescan();

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_Reload(ushort wVID, ushort wPID);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_Purge(IntPtr ftHandle, uint dwMask);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_SetTimeouts(
      IntPtr ftHandle,
      uint dwReadTimeout,
      uint dwWriteTimeout);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_SetBreakOn(IntPtr ftHandle);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_SetBreakOff(IntPtr ftHandle);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_GetDeviceInfo(
      IntPtr ftHandle,
      ref FT_DEVICE pftType,
      ref uint lpdwID,
      byte[] pcSerialNumber,
      byte[] pcDescription,
      IntPtr pvDummy);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_SetResetPipeRetryCount(IntPtr ftHandle, uint dwCount);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_StopInTask(IntPtr ftHandle);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_RestartInTask(IntPtr ftHandle);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_GetDriverVersion(
      IntPtr ftHandle,
      ref uint lpdwDriverVersion);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_GetLibraryVersion(ref uint lpdwLibraryVersion);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_SetDeadmanTimeout(IntPtr ftHandle, uint dwDeadmanTimeout);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_SetChars(
      IntPtr ftHandle,
      byte uEventCh,
      byte uEventChEn,
      byte uErrorCh,
      byte uErrorChEn);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_SetEventNotification(
      IntPtr ftHandle,
      uint dwEventMask,
      SafeHandle hEvent);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_GetComPortNumber(IntPtr ftHandle, ref int dwComPortNumber);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_SetLatencyTimer(IntPtr ftHandle, byte ucLatency);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_GetLatencyTimer(IntPtr ftHandle, ref byte ucLatency);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_SetBitMode(IntPtr ftHandle, byte ucMask, byte ucMode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_GetBitMode(IntPtr ftHandle, ref byte ucMode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_SetUSBParameters(
      IntPtr ftHandle,
      uint dwInTransferSize,
      uint dwOutTransferSize);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_ReadEE(
      IntPtr ftHandle,
      uint dwWordOffset,
      ref ushort lpwValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_WriteEE(IntPtr ftHandle, uint dwWordOffset, ushort wValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_EraseEE(IntPtr ftHandle);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_EE_UASize(IntPtr ftHandle, ref uint dwSize);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_EE_UARead(
      IntPtr ftHandle,
      byte[] pucData,
      int dwDataLen,
      ref uint lpdwDataRead);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_EE_UAWrite(IntPtr ftHandle, byte[] pucData, int dwDataLen);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_EE_Read(IntPtr ftHandle, FT_PROGRAM_DATA pData);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_EE_Program(IntPtr ftHandle, FT_PROGRAM_DATA pData);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_EEPROM_Read(
      IntPtr ftHandle,
      IntPtr eepromData,
      uint eepromDataSize,
      byte[] manufacturer,
      byte[] manufacturerID,
      byte[] description,
      byte[] serialnumber);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_EEPROM_Program(
      IntPtr ftHandle,
      IntPtr eepromData,
      uint eepromDataSize,
      byte[] manufacturer,
      byte[] manufacturerID,
      byte[] description,
      byte[] serialnumber);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_VendorCmdGet(
      IntPtr ftHandle,
      ushort request,
      byte[] buf,
      ushort len);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate FT_STATUS tFT_VendorCmdSet(
      IntPtr ftHandle,
      ushort request,
      byte[] buf,
      ushort len);

    /// <summary>Status values for FTDI devices.</summary>
    public enum FT_STATUS
    {
      /// <summary>Status OK</summary>
      FT_OK,
      /// <summary>The device handle is invalid</summary>
      FT_INVALID_HANDLE,
      /// <summary>Device not found</summary>
      FT_DEVICE_NOT_FOUND,
      /// <summary>Device is not open</summary>
      FT_DEVICE_NOT_OPENED,
      /// <summary>IO error</summary>
      FT_IO_ERROR,
      /// <summary>Insufficient resources</summary>
      FT_INSUFFICIENT_RESOURCES,
      /// <summary>A parameter was invalid</summary>
      FT_INVALID_PARAMETER,
      /// <summary>The requested baud rate is invalid</summary>
      FT_INVALID_BAUD_RATE,
      /// <summary>Device not opened for erase</summary>
      FT_DEVICE_NOT_OPENED_FOR_ERASE,
      /// <summary>Device not poened for write</summary>
      FT_DEVICE_NOT_OPENED_FOR_WRITE,
      /// <summary>Failed to write to device</summary>
      FT_FAILED_TO_WRITE_DEVICE,
      /// <summary>Failed to read the device EEPROM</summary>
      FT_EEPROM_READ_FAILED,
      /// <summary>Failed to write the device EEPROM</summary>
      FT_EEPROM_WRITE_FAILED,
      /// <summary>Failed to erase the device EEPROM</summary>
      FT_EEPROM_ERASE_FAILED,
      /// <summary>An EEPROM is not fitted to the device</summary>
      FT_EEPROM_NOT_PRESENT,
      /// <summary>Device EEPROM is blank</summary>
      FT_EEPROM_NOT_PROGRAMMED,
      /// <summary>Invalid arguments</summary>
      FT_INVALID_ARGS,
      /// <summary>An other error has occurred</summary>
      FT_OTHER_ERROR,
    }

    /// <summary>Error states not supported by FTD2XX DLL.</summary>
    private enum FT_ERROR
    {
      FT_NO_ERROR,
      FT_INCORRECT_DEVICE,
      FT_INVALID_BITMODE,
      FT_BUFFER_SIZE,
    }

    /// <summary>Permitted data bits for FTDI devices</summary>
    public class FT_DATA_BITS
    {
      /// <summary>8 data bits</summary>
      public const byte FT_BITS_8 = 8;
      /// <summary>7 data bits</summary>
      public const byte FT_BITS_7 = 7;
    }

    /// <summary>Permitted stop bits for FTDI devices</summary>
    public class FT_STOP_BITS
    {
      /// <summary>1 stop bit</summary>
      public const byte FT_STOP_BITS_1 = 0;
      /// <summary>2 stop bits</summary>
      public const byte FT_STOP_BITS_2 = 2;
    }

    /// <summary>Permitted parity values for FTDI devices</summary>
    public class FT_PARITY
    {
      /// <summary>No parity</summary>
      public const byte FT_PARITY_NONE = 0;
      /// <summary>Odd parity</summary>
      public const byte FT_PARITY_ODD = 1;
      /// <summary>Even parity</summary>
      public const byte FT_PARITY_EVEN = 2;
      /// <summary>Mark parity</summary>
      public const byte FT_PARITY_MARK = 3;
      /// <summary>Space parity</summary>
      public const byte FT_PARITY_SPACE = 4;
    }

    /// <summary>Permitted flow control values for FTDI devices</summary>
    public class FT_FLOW_CONTROL
    {
      /// <summary>No flow control</summary>
      public const ushort FT_FLOW_NONE = 0;
      /// <summary>RTS/CTS flow control</summary>
      public const ushort FT_FLOW_RTS_CTS = 256;
      /// <summary>DTR/DSR flow control</summary>
      public const ushort FT_FLOW_DTR_DSR = 512;
      /// <summary>Xon/Xoff flow control</summary>
      public const ushort FT_FLOW_XON_XOFF = 1024;
    }

    /// <summary>Purge buffer constant definitions</summary>
    public class FT_PURGE
    {
      /// <summary>Purge Rx buffer</summary>
      public const byte FT_PURGE_RX = 1;
      /// <summary>Purge Tx buffer</summary>
      public const byte FT_PURGE_TX = 2;
    }

    /// <summary>Modem status bit definitions</summary>
    public class FT_MODEM_STATUS
    {
      /// <summary>Clear To Send (CTS) modem status</summary>
      public const byte FT_CTS = 16;
      /// <summary>Data Set Ready (DSR) modem status</summary>
      public const byte FT_DSR = 32;
      /// <summary>Ring Indicator (RI) modem status</summary>
      public const byte FT_RI = 64;
      /// <summary>Data Carrier Detect (DCD) modem status</summary>
      public const byte FT_DCD = 128;
    }

    /// <summary>Line status bit definitions</summary>
    public class FT_LINE_STATUS
    {
      /// <summary>Overrun Error (OE) line status</summary>
      public const byte FT_OE = 2;
      /// <summary>Parity Error (PE) line status</summary>
      public const byte FT_PE = 4;
      /// <summary>Framing Error (FE) line status</summary>
      public const byte FT_FE = 8;
      /// <summary>Break Interrupt (BI) line status</summary>
      public const byte FT_BI = 16;
    }

    /// <summary>FTDI device event types that can be monitored</summary>
    public class FT_EVENTS
    {
      /// <summary>Event on receive character</summary>
      public const uint FT_EVENT_RXCHAR = 1;
      /// <summary>Event on modem status change</summary>
      public const uint FT_EVENT_MODEM_STATUS = 2;
      /// <summary>Event on line status change</summary>
      public const uint FT_EVENT_LINE_STATUS = 4;
    }

    /// <summary>
    /// Permitted bit mode values for FTDI devices.  For use with SetBitMode
    /// </summary>
    public class FT_BIT_MODES
    {
      /// <summary>Reset bit mode</summary>
      public const byte FT_BIT_MODE_RESET = 0;
      /// <summary>Asynchronous bit-bang mode</summary>
      public const byte FT_BIT_MODE_ASYNC_BITBANG = 1;
      /// <summary>
      /// MPSSE bit mode - only available on FT2232, FT2232H, FT4232H and FT232H
      /// </summary>
      public const byte FT_BIT_MODE_MPSSE = 2;
      /// <summary>Synchronous bit-bang mode</summary>
      public const byte FT_BIT_MODE_SYNC_BITBANG = 4;
      /// <summary>
      /// MCU host bus emulation mode - only available on FT2232, FT2232H, FT4232H and FT232H
      /// </summary>
      public const byte FT_BIT_MODE_MCU_HOST = 8;
      /// <summary>
      /// Fast opto-isolated serial mode - only available on FT2232, FT2232H, FT4232H and FT232H
      /// </summary>
      public const byte FT_BIT_MODE_FAST_SERIAL = 16;
      /// <summary>
      /// CBUS bit-bang mode - only available on FT232R and FT232H
      /// </summary>
      public const byte FT_BIT_MODE_CBUS_BITBANG = 32;
      /// <summary>
      /// Single channel synchronous 245 FIFO mode - only available on FT2232H channel A and FT232H
      /// </summary>
      public const byte FT_BIT_MODE_SYNC_FIFO = 64;
    }

    /// <summary>
    /// Available functions for the FT232R CBUS pins.  Controlled by FT232R EEPROM settings
    /// </summary>
    public class FT_CBUS_OPTIONS
    {
      /// <summary>FT232R CBUS EEPROM options - Tx Data Enable</summary>
      public const byte FT_CBUS_TXDEN = 0;
      /// <summary>FT232R CBUS EEPROM options - Power On</summary>
      public const byte FT_CBUS_PWRON = 1;
      /// <summary>FT232R CBUS EEPROM options - Rx LED</summary>
      public const byte FT_CBUS_RXLED = 2;
      /// <summary>FT232R CBUS EEPROM options - Tx LED</summary>
      public const byte FT_CBUS_TXLED = 3;
      /// <summary>FT232R CBUS EEPROM options - Tx and Rx LED</summary>
      public const byte FT_CBUS_TXRXLED = 4;
      /// <summary>FT232R CBUS EEPROM options - Sleep</summary>
      public const byte FT_CBUS_SLEEP = 5;
      /// <summary>FT232R CBUS EEPROM options - 48MHz clock</summary>
      public const byte FT_CBUS_CLK48 = 6;
      /// <summary>FT232R CBUS EEPROM options - 24MHz clock</summary>
      public const byte FT_CBUS_CLK24 = 7;
      /// <summary>FT232R CBUS EEPROM options - 12MHz clock</summary>
      public const byte FT_CBUS_CLK12 = 8;
      /// <summary>FT232R CBUS EEPROM options - 6MHz clock</summary>
      public const byte FT_CBUS_CLK6 = 9;
      /// <summary>FT232R CBUS EEPROM options - IO mode</summary>
      public const byte FT_CBUS_IOMODE = 10;
      /// <summary>FT232R CBUS EEPROM options - Bit-bang write strobe</summary>
      public const byte FT_CBUS_BITBANG_WR = 11;
      /// <summary>FT232R CBUS EEPROM options - Bit-bang read strobe</summary>
      public const byte FT_CBUS_BITBANG_RD = 12;
    }

    /// <summary>
    /// Available functions for the FT232H CBUS pins.  Controlled by FT232H EEPROM settings
    /// </summary>
    public class FT_232H_CBUS_OPTIONS
    {
      /// <summary>FT232H CBUS EEPROM options - Tristate</summary>
      public const byte FT_CBUS_TRISTATE = 0;
      /// <summary>FT232H CBUS EEPROM options - Rx LED</summary>
      public const byte FT_CBUS_RXLED = 1;
      /// <summary>FT232H CBUS EEPROM options - Tx LED</summary>
      public const byte FT_CBUS_TXLED = 2;
      /// <summary>FT232H CBUS EEPROM options - Tx and Rx LED</summary>
      public const byte FT_CBUS_TXRXLED = 3;
      /// <summary>FT232H CBUS EEPROM options - Power Enable#</summary>
      public const byte FT_CBUS_PWREN = 4;
      /// <summary>FT232H CBUS EEPROM options - Sleep</summary>
      public const byte FT_CBUS_SLEEP = 5;
      /// <summary>FT232H CBUS EEPROM options - Drive pin to logic 0</summary>
      public const byte FT_CBUS_DRIVE_0 = 6;
      /// <summary>FT232H CBUS EEPROM options - Drive pin to logic 1</summary>
      public const byte FT_CBUS_DRIVE_1 = 7;
      /// <summary>FT232H CBUS EEPROM options - IO Mode</summary>
      public const byte FT_CBUS_IOMODE = 8;
      /// <summary>FT232H CBUS EEPROM options - Tx Data Enable</summary>
      public const byte FT_CBUS_TXDEN = 9;
      /// <summary>FT232H CBUS EEPROM options - 30MHz clock</summary>
      public const byte FT_CBUS_CLK30 = 10;
      /// <summary>FT232H CBUS EEPROM options - 15MHz clock</summary>
      public const byte FT_CBUS_CLK15 = 11;
      /// <summary>FT232H CBUS EEPROM options - 7.5MHz clock</summary>
      public const byte FT_CBUS_CLK7_5 = 12;
    }

    /// <summary>
    /// Available functions for the X-Series CBUS pins.  Controlled by X-Series EEPROM settings
    /// </summary>
    public class FT_XSERIES_CBUS_OPTIONS
    {
      /// <summary>FT X-Series CBUS EEPROM options - Tristate</summary>
      public const byte FT_CBUS_TRISTATE = 0;
      /// <summary>FT X-Series CBUS EEPROM options - RxLED#</summary>
      public const byte FT_CBUS_RXLED = 1;
      /// <summary>FT X-Series CBUS EEPROM options - TxLED#</summary>
      public const byte FT_CBUS_TXLED = 2;
      /// <summary>FT X-Series CBUS EEPROM options - TxRxLED#</summary>
      public const byte FT_CBUS_TXRXLED = 3;
      /// <summary>FT X-Series CBUS EEPROM options - PwrEn#</summary>
      public const byte FT_CBUS_PWREN = 4;
      /// <summary>FT X-Series CBUS EEPROM options - Sleep#</summary>
      public const byte FT_CBUS_SLEEP = 5;
      /// <summary>FT X-Series CBUS EEPROM options - Drive_0</summary>
      public const byte FT_CBUS_Drive_0 = 6;
      /// <summary>FT X-Series CBUS EEPROM options - Drive_1</summary>
      public const byte FT_CBUS_Drive_1 = 7;
      /// <summary>FT X-Series CBUS EEPROM options - GPIO</summary>
      public const byte FT_CBUS_GPIO = 8;
      /// <summary>FT X-Series CBUS EEPROM options - TxdEn</summary>
      public const byte FT_CBUS_TXDEN = 9;
      /// <summary>FT X-Series CBUS EEPROM options - Clk24MHz</summary>
      public const byte FT_CBUS_CLK24MHz = 10;
      /// <summary>FT X-Series CBUS EEPROM options - Clk12MHz</summary>
      public const byte FT_CBUS_CLK12MHz = 11;
      /// <summary>FT X-Series CBUS EEPROM options - Clk6MHz</summary>
      public const byte FT_CBUS_CLK6MHz = 12;
      /// <summary>FT X-Series CBUS EEPROM options - BCD_Charger</summary>
      public const byte FT_CBUS_BCD_Charger = 13;
      /// <summary>FT X-Series CBUS EEPROM options - BCD_Charger#</summary>
      public const byte FT_CBUS_BCD_Charger_N = 14;
      /// <summary>FT X-Series CBUS EEPROM options - I2C_TXE#</summary>
      public const byte FT_CBUS_I2C_TXE = 15;
      /// <summary>FT X-Series CBUS EEPROM options - I2C_RXF#</summary>
      public const byte FT_CBUS_I2C_RXF = 16;
      /// <summary>FT X-Series CBUS EEPROM options - VBUS_Sense</summary>
      public const byte FT_CBUS_VBUS_Sense = 17;
      /// <summary>FT X-Series CBUS EEPROM options - BitBang_WR#</summary>
      public const byte FT_CBUS_BitBang_WR = 18;
      /// <summary>FT X-Series CBUS EEPROM options - BitBang_RD#</summary>
      public const byte FT_CBUS_BitBang_RD = 19;
      /// <summary>FT X-Series CBUS EEPROM options - Time_Stampe</summary>
      public const byte FT_CBUS_Time_Stamp = 20;
      /// <summary>FT X-Series CBUS EEPROM options - Keep_Awake#</summary>
      public const byte FT_CBUS_Keep_Awake = 21;
    }

    /// <summary>
    /// Flags that provide information on the FTDI device state
    /// </summary>
    public class FT_FLAGS
    {
      /// <summary>Indicates that the device is open</summary>
      public const uint FT_FLAGS_OPENED = 1;
      /// <summary>
      /// Indicates that the device is enumerated as a hi-speed USB device
      /// </summary>
      public const uint FT_FLAGS_HISPEED = 2;
    }

    /// <summary>
    /// Valid values for drive current options on FT2232H, FT4232H and FT232H devices.
    /// </summary>
    public class FT_DRIVE_CURRENT
    {
      /// <summary>4mA drive current</summary>
      public const byte FT_DRIVE_CURRENT_4MA = 4;
      /// <summary>8mA drive current</summary>
      public const byte FT_DRIVE_CURRENT_8MA = 8;
      /// <summary>12mA drive current</summary>
      public const byte FT_DRIVE_CURRENT_12MA = 12;
      /// <summary>16mA drive current</summary>
      public const byte FT_DRIVE_CURRENT_16MA = 16;
    }

    /// <summary>List of FTDI device types</summary>
    public enum FT_DEVICE
    {
      /// <summary>FT232B or FT245B device</summary>
      FT_DEVICE_BM,
      /// <summary>FT8U232AM or FT8U245AM device</summary>
      FT_DEVICE_AM,
      /// <summary>FT8U100AX device</summary>
      FT_DEVICE_100AX,
      /// <summary>Unknown device</summary>
      FT_DEVICE_UNKNOWN,
      /// <summary>FT2232 device</summary>
      FT_DEVICE_2232,
      /// <summary>FT232R or FT245R device</summary>
      FT_DEVICE_232R,
      /// <summary>FT2232H device</summary>
      FT_DEVICE_2232H,
      /// <summary>FT4232H device</summary>
      FT_DEVICE_4232H,
      /// <summary>FT232H device</summary>
      FT_DEVICE_232H,
      /// <summary>FT X-Series device</summary>
      FT_DEVICE_X_SERIES,
      /// <summary>FT4222 hi-speed device Mode 0 - 2 interfaces</summary>
      FT_DEVICE_4222H_0,
      /// <summary>FT4222 hi-speed device Mode 1 or 2 - 4 interfaces</summary>
      FT_DEVICE_4222H_1_2,
      /// <summary>FT4222 hi-speed device Mode 3 - 1 interface</summary>
      FT_DEVICE_4222H_3,
      /// <summary>OTP programmer board for the FT4222.</summary>
      FT_DEVICE_4222_PROG,
    }

    /// <summary>
    /// Type that holds device information for GetDeviceInformation method.
    /// Used with FT_GetDeviceInfo and FT_GetDeviceInfoDetail in FTD2XX.DLL
    /// </summary>
    public class FT_DEVICE_INFO_NODE
    {
      /// <summary>
      /// Indicates device state.  Can be any combination of the following: FT_FLAGS_OPENED, FT_FLAGS_HISPEED
      /// </summary>
      public uint Flags;
      /// <summary>
      /// Indicates the device type.  Can be one of the following: FT_DEVICE_232R, FT_DEVICE_2232C, FT_DEVICE_BM, FT_DEVICE_AM, FT_DEVICE_100AX or FT_DEVICE_UNKNOWN
      /// </summary>
      public FT_DEVICE Type;
      /// <summary>The Vendor ID and Product ID of the device</summary>
      public uint ID;
      /// <summary>The physical location identifier of the device</summary>
      public uint LocId;
      /// <summary>The device serial number</summary>
      public string SerialNumber;
      /// <summary>The device description</summary>
      public string Description;
      /// <summary>
      /// The device handle.  This value is not used externally and is provided for information only.
      /// If the device is not open, this value is 0.
      /// </summary>
      public IntPtr ftHandle;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    private class FT_PROGRAM_DATA
    {
      public uint Signature1;
      public uint Signature2;
      public uint Version;
      public ushort VendorID;
      public ushort ProductID;
      public IntPtr Manufacturer;
      public IntPtr ManufacturerID;
      public IntPtr Description;
      public IntPtr SerialNumber;
      public ushort MaxPower;
      public ushort PnP;
      public ushort SelfPowered;
      public ushort RemoteWakeup;
      public byte Rev4;
      public byte IsoIn;
      public byte IsoOut;
      public byte PullDownEnable;
      public byte SerNumEnable;
      public byte USBVersionEnable;
      public ushort USBVersion;
      public byte Rev5;
      public byte IsoInA;
      public byte IsoInB;
      public byte IsoOutA;
      public byte IsoOutB;
      public byte PullDownEnable5;
      public byte SerNumEnable5;
      public byte USBVersionEnable5;
      public ushort USBVersion5;
      public byte AIsHighCurrent;
      public byte BIsHighCurrent;
      public byte IFAIsFifo;
      public byte IFAIsFifoTar;
      public byte IFAIsFastSer;
      public byte AIsVCP;
      public byte IFBIsFifo;
      public byte IFBIsFifoTar;
      public byte IFBIsFastSer;
      public byte BIsVCP;
      public byte UseExtOsc;
      public byte HighDriveIOs;
      public byte EndpointSize;
      public byte PullDownEnableR;
      public byte SerNumEnableR;
      public byte InvertTXD;
      public byte InvertRXD;
      public byte InvertRTS;
      public byte InvertCTS;
      public byte InvertDTR;
      public byte InvertDSR;
      public byte InvertDCD;
      public byte InvertRI;
      public byte Cbus0;
      public byte Cbus1;
      public byte Cbus2;
      public byte Cbus3;
      public byte Cbus4;
      public byte RIsD2XX;
      public byte PullDownEnable7;
      public byte SerNumEnable7;
      public byte ALSlowSlew;
      public byte ALSchmittInput;
      public byte ALDriveCurrent;
      public byte AHSlowSlew;
      public byte AHSchmittInput;
      public byte AHDriveCurrent;
      public byte BLSlowSlew;
      public byte BLSchmittInput;
      public byte BLDriveCurrent;
      public byte BHSlowSlew;
      public byte BHSchmittInput;
      public byte BHDriveCurrent;
      public byte IFAIsFifo7;
      public byte IFAIsFifoTar7;
      public byte IFAIsFastSer7;
      public byte AIsVCP7;
      public byte IFBIsFifo7;
      public byte IFBIsFifoTar7;
      public byte IFBIsFastSer7;
      public byte BIsVCP7;
      public byte PowerSaveEnable;
      public byte PullDownEnable8;
      public byte SerNumEnable8;
      public byte ASlowSlew;
      public byte ASchmittInput;
      public byte ADriveCurrent;
      public byte BSlowSlew;
      public byte BSchmittInput;
      public byte BDriveCurrent;
      public byte CSlowSlew;
      public byte CSchmittInput;
      public byte CDriveCurrent;
      public byte DSlowSlew;
      public byte DSchmittInput;
      public byte DDriveCurrent;
      public byte ARIIsTXDEN;
      public byte BRIIsTXDEN;
      public byte CRIIsTXDEN;
      public byte DRIIsTXDEN;
      public byte AIsVCP8;
      public byte BIsVCP8;
      public byte CIsVCP8;
      public byte DIsVCP8;
      public byte PullDownEnableH;
      public byte SerNumEnableH;
      public byte ACSlowSlewH;
      public byte ACSchmittInputH;
      public byte ACDriveCurrentH;
      public byte ADSlowSlewH;
      public byte ADSchmittInputH;
      public byte ADDriveCurrentH;
      public byte Cbus0H;
      public byte Cbus1H;
      public byte Cbus2H;
      public byte Cbus3H;
      public byte Cbus4H;
      public byte Cbus5H;
      public byte Cbus6H;
      public byte Cbus7H;
      public byte Cbus8H;
      public byte Cbus9H;
      public byte IsFifoH;
      public byte IsFifoTarH;
      public byte IsFastSerH;
      public byte IsFT1248H;
      public byte FT1248CpolH;
      public byte FT1248LsbH;
      public byte FT1248FlowControlH;
      public byte IsVCPH;
      public byte PowerSaveEnableH;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    private struct FT_EEPROM_HEADER
    {
      public uint deviceType;
      public ushort VendorId;
      public ushort ProductId;
      public byte SerNumEnable;
      public ushort MaxPower;
      public byte SelfPowered;
      public byte RemoteWakeup;
      public byte PullDownEnable;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    private struct FT_XSERIES_DATA
    {
      public FT_EEPROM_HEADER common;
      public byte ACSlowSlew;
      public byte ACSchmittInput;
      public byte ACDriveCurrent;
      public byte ADSlowSlew;
      public byte ADSchmittInput;
      public byte ADDriveCurrent;
      public byte Cbus0;
      public byte Cbus1;
      public byte Cbus2;
      public byte Cbus3;
      public byte Cbus4;
      public byte Cbus5;
      public byte Cbus6;
      public byte InvertTXD;
      public byte InvertRXD;
      public byte InvertRTS;
      public byte InvertCTS;
      public byte InvertDTR;
      public byte InvertDSR;
      public byte InvertDCD;
      public byte InvertRI;
      public byte BCDEnable;
      public byte BCDForceCbusPWREN;
      public byte BCDDisableSleep;
      public ushort I2CSlaveAddress;
      public uint I2CDeviceId;
      public byte I2CDisableSchmitt;
      public byte FT1248Cpol;
      public byte FT1248Lsb;
      public byte FT1248FlowControl;
      public byte RS485EchoSuppress;
      public byte PowerSaveEnable;
      public byte DriverType;
    }

    /// <summary>
    /// Common EEPROM elements for all devices.  Inherited to specific device type EEPROMs.
    /// </summary>
    public class FT_EEPROM_DATA
    {
      /// <summary>Vendor ID as supplied by the USB Implementers Forum</summary>
      public ushort VendorID = 1027;
      /// <summary>Product ID</summary>
      public ushort ProductID = 24577;
      /// <summary>Manufacturer name string</summary>
      public string Manufacturer = nameof (FTDI);
      /// <summary>
      /// Manufacturer name abbreviation to be used as a prefix for automatically generated serial numbers
      /// </summary>
      public string ManufacturerID = "FT";
      /// <summary>Device description string</summary>
      public string Description = "USB-Serial Converter";
      /// <summary>Device serial number string</summary>
      public string SerialNumber = "";
      /// <summary>Maximum power the device needs</summary>
      public ushort MaxPower = 144;
      /// <summary>
      /// Indicates if the device has its own power supply (self-powered) or gets power from the USB port (bus-powered)
      /// </summary>
      public bool SelfPowered;
      /// <summary>
      /// Determines if the device can wake the host PC from suspend by toggling the RI line
      /// </summary>
      public bool RemoteWakeup;
    }

    /// <summary>
    /// EEPROM structure specific to FT232B and FT245B devices.
    /// Inherits from FT_EEPROM_DATA.
    /// </summary>
    public class FT232B_EEPROM_STRUCTURE : FT_EEPROM_DATA
    {
      /// <summary>
      /// Determines if IOs are pulled down when the device is in suspend
      /// </summary>
      public bool PullDownEnable;
      /// <summary>Determines if the serial number is enabled</summary>
      public bool SerNumEnable = true;
      /// <summary>Determines if the USB version number is enabled</summary>
      public bool USBVersionEnable = true;
      /// <summary>
      /// The USB version number.  Should be either 0x0110 (USB 1.1) or 0x0200 (USB 2.0)
      /// </summary>
      public ushort USBVersion = 512;
    }

    /// <summary>
    /// EEPROM structure specific to FT2232 devices.
    /// Inherits from FT_EEPROM_DATA.
    /// </summary>
    public class FT2232_EEPROM_STRUCTURE : FT_EEPROM_DATA
    {
      /// <summary>
      /// Determines if IOs are pulled down when the device is in suspend
      /// </summary>
      public bool PullDownEnable;
      /// <summary>Determines if the serial number is enabled</summary>
      public bool SerNumEnable = true;
      /// <summary>Determines if the USB version number is enabled</summary>
      public bool USBVersionEnable = true;
      /// <summary>
      /// The USB version number.  Should be either 0x0110 (USB 1.1) or 0x0200 (USB 2.0)
      /// </summary>
      public ushort USBVersion = 512;
      /// <summary>Enables high current IOs on channel A</summary>
      public bool AIsHighCurrent;
      /// <summary>Enables high current IOs on channel B</summary>
      public bool BIsHighCurrent;
      /// <summary>Determines if channel A is in FIFO mode</summary>
      public bool IFAIsFifo;
      /// <summary>Determines if channel A is in FIFO target mode</summary>
      public bool IFAIsFifoTar;
      /// <summary>Determines if channel A is in fast serial mode</summary>
      public bool IFAIsFastSer;
      /// <summary>Determines if channel A loads the VCP driver</summary>
      public bool AIsVCP = true;
      /// <summary>Determines if channel B is in FIFO mode</summary>
      public bool IFBIsFifo;
      /// <summary>Determines if channel B is in FIFO target mode</summary>
      public bool IFBIsFifoTar;
      /// <summary>Determines if channel B is in fast serial mode</summary>
      public bool IFBIsFastSer;
      /// <summary>Determines if channel B loads the VCP driver</summary>
      public bool BIsVCP = true;
    }

    /// <summary>
    /// EEPROM structure specific to FT232R and FT245R devices.
    /// Inherits from FT_EEPROM_DATA.
    /// </summary>
    public class FT232R_EEPROM_STRUCTURE : FT_EEPROM_DATA
    {
      /// <summary>
      /// Disables the FT232R internal clock source.
      /// If the device has external oscillator enabled it must have an external oscillator fitted to function
      /// </summary>
      public bool UseExtOsc;
      /// <summary>Enables high current IOs</summary>
      public bool HighDriveIOs;
      /// <summary>
      /// Sets the endpoint size.  This should always be set to 64
      /// </summary>
      public byte EndpointSize = 64;
      /// <summary>
      /// Determines if IOs are pulled down when the device is in suspend
      /// </summary>
      public bool PullDownEnable;
      /// <summary>Determines if the serial number is enabled</summary>
      public bool SerNumEnable = true;
      /// <summary>Inverts the sense of the TXD line</summary>
      public bool InvertTXD;
      /// <summary>Inverts the sense of the RXD line</summary>
      public bool InvertRXD;
      /// <summary>Inverts the sense of the RTS line</summary>
      public bool InvertRTS;
      /// <summary>Inverts the sense of the CTS line</summary>
      public bool InvertCTS;
      /// <summary>Inverts the sense of the DTR line</summary>
      public bool InvertDTR;
      /// <summary>Inverts the sense of the DSR line</summary>
      public bool InvertDSR;
      /// <summary>Inverts the sense of the DCD line</summary>
      public bool InvertDCD;
      /// <summary>Inverts the sense of the RI line</summary>
      public bool InvertRI;
      /// <summary>
      /// Sets the function of the CBUS0 pin for FT232R devices.
      /// Valid values are FT_CBUS_TXDEN, FT_CBUS_PWRON , FT_CBUS_RXLED, FT_CBUS_TXLED,
      /// FT_CBUS_TXRXLED, FT_CBUS_SLEEP, FT_CBUS_CLK48, FT_CBUS_CLK24, FT_CBUS_CLK12,
      /// FT_CBUS_CLK6, FT_CBUS_IOMODE, FT_CBUS_BITBANG_WR, FT_CBUS_BITBANG_RD
      /// </summary>
      public byte Cbus0 = 5;
      /// <summary>
      /// Sets the function of the CBUS1 pin for FT232R devices.
      /// Valid values are FT_CBUS_TXDEN, FT_CBUS_PWRON , FT_CBUS_RXLED, FT_CBUS_TXLED,
      /// FT_CBUS_TXRXLED, FT_CBUS_SLEEP, FT_CBUS_CLK48, FT_CBUS_CLK24, FT_CBUS_CLK12,
      /// FT_CBUS_CLK6, FT_CBUS_IOMODE, FT_CBUS_BITBANG_WR, FT_CBUS_BITBANG_RD
      /// </summary>
      public byte Cbus1 = 5;
      /// <summary>
      /// Sets the function of the CBUS2 pin for FT232R devices.
      /// Valid values are FT_CBUS_TXDEN, FT_CBUS_PWRON , FT_CBUS_RXLED, FT_CBUS_TXLED,
      /// FT_CBUS_TXRXLED, FT_CBUS_SLEEP, FT_CBUS_CLK48, FT_CBUS_CLK24, FT_CBUS_CLK12,
      /// FT_CBUS_CLK6, FT_CBUS_IOMODE, FT_CBUS_BITBANG_WR, FT_CBUS_BITBANG_RD
      /// </summary>
      public byte Cbus2 = 5;
      /// <summary>
      /// Sets the function of the CBUS3 pin for FT232R devices.
      /// Valid values are FT_CBUS_TXDEN, FT_CBUS_PWRON , FT_CBUS_RXLED, FT_CBUS_TXLED,
      /// FT_CBUS_TXRXLED, FT_CBUS_SLEEP, FT_CBUS_CLK48, FT_CBUS_CLK24, FT_CBUS_CLK12,
      /// FT_CBUS_CLK6, FT_CBUS_IOMODE, FT_CBUS_BITBANG_WR, FT_CBUS_BITBANG_RD
      /// </summary>
      public byte Cbus3 = 5;
      /// <summary>
      /// Sets the function of the CBUS4 pin for FT232R devices.
      /// Valid values are FT_CBUS_TXDEN, FT_CBUS_PWRON , FT_CBUS_RXLED, FT_CBUS_TXLED,
      /// FT_CBUS_TXRXLED, FT_CBUS_SLEEP, FT_CBUS_CLK48, FT_CBUS_CLK24, FT_CBUS_CLK12,
      /// FT_CBUS_CLK6
      /// </summary>
      public byte Cbus4 = 5;
      /// <summary>Determines if the VCP driver is loaded</summary>
      public bool RIsD2XX;
    }

    /// <summary>
    /// EEPROM structure specific to FT2232H devices.
    /// Inherits from FT_EEPROM_DATA.
    /// </summary>
    public class FT2232H_EEPROM_STRUCTURE : FT_EEPROM_DATA
    {
      /// <summary>
      /// Determines if IOs are pulled down when the device is in suspend
      /// </summary>
      public bool PullDownEnable;
      /// <summary>Determines if the serial number is enabled</summary>
      public bool SerNumEnable = true;
      /// <summary>Determines if AL pins have a slow slew rate</summary>
      public bool ALSlowSlew;
      /// <summary>Determines if the AL pins have a Schmitt input</summary>
      public bool ALSchmittInput;
      /// <summary>
      /// Determines the AL pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
      /// </summary>
      public byte ALDriveCurrent = 4;
      /// <summary>Determines if AH pins have a slow slew rate</summary>
      public bool AHSlowSlew;
      /// <summary>Determines if the AH pins have a Schmitt input</summary>
      public bool AHSchmittInput;
      /// <summary>
      /// Determines the AH pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
      /// </summary>
      public byte AHDriveCurrent = 4;
      /// <summary>Determines if BL pins have a slow slew rate</summary>
      public bool BLSlowSlew;
      /// <summary>Determines if the BL pins have a Schmitt input</summary>
      public bool BLSchmittInput;
      /// <summary>
      /// Determines the BL pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
      /// </summary>
      public byte BLDriveCurrent = 4;
      /// <summary>Determines if BH pins have a slow slew rate</summary>
      public bool BHSlowSlew;
      /// <summary>Determines if the BH pins have a Schmitt input</summary>
      public bool BHSchmittInput;
      /// <summary>
      /// Determines the BH pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
      /// </summary>
      public byte BHDriveCurrent = 4;
      /// <summary>Determines if channel A is in FIFO mode</summary>
      public bool IFAIsFifo;
      /// <summary>Determines if channel A is in FIFO target mode</summary>
      public bool IFAIsFifoTar;
      /// <summary>Determines if channel A is in fast serial mode</summary>
      public bool IFAIsFastSer;
      /// <summary>Determines if channel A loads the VCP driver</summary>
      public bool AIsVCP = true;
      /// <summary>Determines if channel B is in FIFO mode</summary>
      public bool IFBIsFifo;
      /// <summary>Determines if channel B is in FIFO target mode</summary>
      public bool IFBIsFifoTar;
      /// <summary>Determines if channel B is in fast serial mode</summary>
      public bool IFBIsFastSer;
      /// <summary>Determines if channel B loads the VCP driver</summary>
      public bool BIsVCP = true;
      /// <summary>
      /// For self-powered designs, keeps the FT2232H in low power state until BCBUS7 is high
      /// </summary>
      public bool PowerSaveEnable;
    }

    /// <summary>
    /// EEPROM structure specific to FT4232H devices.
    /// Inherits from FT_EEPROM_DATA.
    /// </summary>
    public class FT4232H_EEPROM_STRUCTURE : FT_EEPROM_DATA
    {
      /// <summary>
      /// Determines if IOs are pulled down when the device is in suspend
      /// </summary>
      public bool PullDownEnable;
      /// <summary>Determines if the serial number is enabled</summary>
      public bool SerNumEnable = true;
      /// <summary>Determines if A pins have a slow slew rate</summary>
      public bool ASlowSlew;
      /// <summary>Determines if the A pins have a Schmitt input</summary>
      public bool ASchmittInput;
      /// <summary>
      /// Determines the A pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
      /// </summary>
      public byte ADriveCurrent = 4;
      /// <summary>Determines if B pins have a slow slew rate</summary>
      public bool BSlowSlew;
      /// <summary>Determines if the B pins have a Schmitt input</summary>
      public bool BSchmittInput;
      /// <summary>
      /// Determines the B pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
      /// </summary>
      public byte BDriveCurrent = 4;
      /// <summary>Determines if C pins have a slow slew rate</summary>
      public bool CSlowSlew;
      /// <summary>Determines if the C pins have a Schmitt input</summary>
      public bool CSchmittInput;
      /// <summary>
      /// Determines the C pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
      /// </summary>
      public byte CDriveCurrent = 4;
      /// <summary>Determines if D pins have a slow slew rate</summary>
      public bool DSlowSlew;
      /// <summary>Determines if the D pins have a Schmitt input</summary>
      public bool DSchmittInput;
      /// <summary>
      /// Determines the D pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
      /// </summary>
      public byte DDriveCurrent = 4;
      /// <summary>RI of port A acts as RS485 transmit enable (TXDEN)</summary>
      public bool ARIIsTXDEN;
      /// <summary>RI of port B acts as RS485 transmit enable (TXDEN)</summary>
      public bool BRIIsTXDEN;
      /// <summary>RI of port C acts as RS485 transmit enable (TXDEN)</summary>
      public bool CRIIsTXDEN;
      /// <summary>RI of port D acts as RS485 transmit enable (TXDEN)</summary>
      public bool DRIIsTXDEN;
      /// <summary>Determines if channel A loads the VCP driver</summary>
      public bool AIsVCP = true;
      /// <summary>Determines if channel B loads the VCP driver</summary>
      public bool BIsVCP = true;
      /// <summary>Determines if channel C loads the VCP driver</summary>
      public bool CIsVCP = true;
      /// <summary>Determines if channel D loads the VCP driver</summary>
      public bool DIsVCP = true;
    }

    /// <summary>
    /// EEPROM structure specific to FT232H devices.
    /// Inherits from FT_EEPROM_DATA.
    /// </summary>
    public class FT232H_EEPROM_STRUCTURE : FT_EEPROM_DATA
    {
      /// <summary>
      /// Determines if IOs are pulled down when the device is in suspend
      /// </summary>
      public bool PullDownEnable;
      /// <summary>Determines if the serial number is enabled</summary>
      public bool SerNumEnable = true;
      /// <summary>Determines if AC pins have a slow slew rate</summary>
      public bool ACSlowSlew;
      /// <summary>Determines if the AC pins have a Schmitt input</summary>
      public bool ACSchmittInput;
      /// <summary>
      /// Determines the AC pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
      /// </summary>
      public byte ACDriveCurrent = 4;
      /// <summary>Determines if AD pins have a slow slew rate</summary>
      public bool ADSlowSlew;
      /// <summary>Determines if the AD pins have a Schmitt input</summary>
      public bool ADSchmittInput;
      /// <summary>
      /// Determines the AD pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
      /// </summary>
      public byte ADDriveCurrent = 4;
      /// <summary>
      /// Sets the function of the CBUS0 pin for FT232H devices.
      /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
      /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN, FT_CBUS_CLK30,
      /// FT_CBUS_CLK15, FT_CBUS_CLK7_5
      /// </summary>
      public byte Cbus0;
      /// <summary>
      /// Sets the function of the CBUS1 pin for FT232H devices.
      /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
      /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN, FT_CBUS_CLK30,
      /// FT_CBUS_CLK15, FT_CBUS_CLK7_5
      /// </summary>
      public byte Cbus1;
      /// <summary>
      /// Sets the function of the CBUS2 pin for FT232H devices.
      /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
      /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN
      /// </summary>
      public byte Cbus2;
      /// <summary>
      /// Sets the function of the CBUS3 pin for FT232H devices.
      /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
      /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN
      /// </summary>
      public byte Cbus3;
      /// <summary>
      /// Sets the function of the CBUS4 pin for FT232H devices.
      /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
      /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN
      /// </summary>
      public byte Cbus4;
      /// <summary>
      /// Sets the function of the CBUS5 pin for FT232H devices.
      /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
      /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_IOMODE,
      /// FT_CBUS_TXDEN, FT_CBUS_CLK30, FT_CBUS_CLK15, FT_CBUS_CLK7_5
      /// </summary>
      public byte Cbus5;
      /// <summary>
      /// Sets the function of the CBUS6 pin for FT232H devices.
      /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
      /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_IOMODE,
      /// FT_CBUS_TXDEN, FT_CBUS_CLK30, FT_CBUS_CLK15, FT_CBUS_CLK7_5
      /// </summary>
      public byte Cbus6;
      /// <summary>
      /// Sets the function of the CBUS7 pin for FT232H devices.
      /// Valid values are FT_CBUS_TRISTATE
      /// </summary>
      public byte Cbus7;
      /// <summary>
      /// Sets the function of the CBUS8 pin for FT232H devices.
      /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
      /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_IOMODE,
      /// FT_CBUS_TXDEN, FT_CBUS_CLK30, FT_CBUS_CLK15, FT_CBUS_CLK7_5
      /// </summary>
      public byte Cbus8;
      /// <summary>
      /// Sets the function of the CBUS9 pin for FT232H devices.
      /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
      /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_IOMODE,
      /// FT_CBUS_TXDEN, FT_CBUS_CLK30, FT_CBUS_CLK15, FT_CBUS_CLK7_5
      /// </summary>
      public byte Cbus9;
      /// <summary>Determines if the device is in FIFO mode</summary>
      public bool IsFifo;
      /// <summary>Determines if the device is in FIFO target mode</summary>
      public bool IsFifoTar;
      /// <summary>Determines if the device is in fast serial mode</summary>
      public bool IsFastSer;
      /// <summary>Determines if the device is in FT1248 mode</summary>
      public bool IsFT1248;
      /// <summary>Determines FT1248 mode clock polarity</summary>
      public bool FT1248Cpol;
      /// <summary>
      /// Determines if data is ent MSB (0) or LSB (1) in FT1248 mode
      /// </summary>
      public bool FT1248Lsb;
      /// <summary>Determines if FT1248 mode uses flow control</summary>
      public bool FT1248FlowControl;
      /// <summary>Determines if the VCP driver is loaded</summary>
      public bool IsVCP = true;
      /// <summary>
      /// For self-powered designs, keeps the FT232H in low power state until ACBUS7 is high
      /// </summary>
      public bool PowerSaveEnable;
    }

    /// <summary>
    /// EEPROM structure specific to X-Series devices.
    /// Inherits from FT_EEPROM_DATA.
    /// </summary>
    public class FT_XSERIES_EEPROM_STRUCTURE : FT_EEPROM_DATA
    {
      /// <summary>
      /// Determines if IOs are pulled down when the device is in suspend
      /// </summary>
      public bool PullDownEnable;
      /// <summary>Determines if the serial number is enabled</summary>
      public bool SerNumEnable = true;
      /// <summary>Determines if the USB version number is enabled</summary>
      public bool USBVersionEnable = true;
      /// <summary>The USB version number: 0x0200 (USB 2.0)</summary>
      public ushort USBVersion = 512;
      /// <summary>Determines if AC pins have a slow slew rate</summary>
      public byte ACSlowSlew;
      /// <summary>Determines if the AC pins have a Schmitt input</summary>
      public byte ACSchmittInput;
      /// <summary>
      /// Determines the AC pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
      /// </summary>
      public byte ACDriveCurrent;
      /// <summary>Determines if AD pins have a slow slew rate</summary>
      public byte ADSlowSlew;
      /// <summary>Determines if AD pins have a schmitt input</summary>
      public byte ADSchmittInput;
      /// <summary>
      /// Determines the AD pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
      /// </summary>
      public byte ADDriveCurrent;
      /// <summary>
      /// Sets the function of the CBUS0 pin for FT232H devices.
      /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
      /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_GPIO, FT_CBUS_TXDEN, FT_CBUS_CLK24,
      /// FT_CBUS_CLK12, FT_CBUS_CLK6, FT_CBUS_BCD_CHARGER, FT_CBUS_BCD_CHARGER_N, FT_CBUS_VBUS_SENSE, FT_CBUS_BITBANG_WR,
      /// FT_CBUS_BITBANG_RD, FT_CBUS_TIME_STAMP, FT_CBUS_KEEP_AWAKE
      /// </summary>
      public byte Cbus0;
      /// <summary>
      /// Sets the function of the CBUS1 pin for FT232H devices.
      /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
      /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_GPIO, FT_CBUS_TXDEN, FT_CBUS_CLK24,
      /// FT_CBUS_CLK12, FT_CBUS_CLK6, FT_CBUS_BCD_CHARGER, FT_CBUS_BCD_CHARGER_N, FT_CBUS_VBUS_SENSE, FT_CBUS_BITBANG_WR,
      /// FT_CBUS_BITBANG_RD, FT_CBUS_TIME_STAMP, FT_CBUS_KEEP_AWAKE
      /// </summary>
      public byte Cbus1;
      /// <summary>
      /// Sets the function of the CBUS2 pin for FT232H devices.
      /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
      /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_GPIO, FT_CBUS_TXDEN, FT_CBUS_CLK24,
      /// FT_CBUS_CLK12, FT_CBUS_CLK6, FT_CBUS_BCD_CHARGER, FT_CBUS_BCD_CHARGER_N, FT_CBUS_VBUS_SENSE, FT_CBUS_BITBANG_WR,
      /// FT_CBUS_BITBANG_RD, FT_CBUS_TIME_STAMP, FT_CBUS_KEEP_AWAKE
      /// </summary>
      public byte Cbus2;
      /// <summary>
      /// Sets the function of the CBUS3 pin for FT232H devices.
      /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
      /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_GPIO, FT_CBUS_TXDEN, FT_CBUS_CLK24,
      /// FT_CBUS_CLK12, FT_CBUS_CLK6, FT_CBUS_BCD_CHARGER, FT_CBUS_BCD_CHARGER_N, FT_CBUS_VBUS_SENSE, FT_CBUS_BITBANG_WR,
      /// FT_CBUS_BITBANG_RD, FT_CBUS_TIME_STAMP, FT_CBUS_KEEP_AWAKE
      /// </summary>
      public byte Cbus3;
      /// <summary>
      /// Sets the function of the CBUS4 pin for FT232H devices.
      /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
      /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN, FT_CBUS_CLK24,
      /// FT_CBUS_CLK12, FT_CBUS_CLK6, FT_CBUS_BCD_CHARGER, FT_CBUS_BCD_CHARGER_N, FT_CBUS_VBUS_SENSE, FT_CBUS_BITBANG_WR,
      /// FT_CBUS_BITBANG_RD, FT_CBUS_TIME_STAMP, FT_CBUS_KEEP_AWAKE
      /// </summary>
      public byte Cbus4;
      /// <summary>
      /// Sets the function of the CBUS5 pin for FT232H devices.
      /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
      /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN, FT_CBUS_CLK24,
      /// FT_CBUS_CLK12, FT_CBUS_CLK6, FT_CBUS_BCD_CHARGER, FT_CBUS_BCD_CHARGER_N, FT_CBUS_VBUS_SENSE, FT_CBUS_BITBANG_WR,
      /// FT_CBUS_BITBANG_RD, FT_CBUS_TIME_STAMP, FT_CBUS_KEEP_AWAKE
      /// </summary>
      public byte Cbus5;
      /// <summary>
      /// Sets the function of the CBUS6 pin for FT232H devices.
      /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
      /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN, FT_CBUS_CLK24,
      /// FT_CBUS_CLK12, FT_CBUS_CLK6, FT_CBUS_BCD_CHARGER, FT_CBUS_BCD_CHARGER_N, FT_CBUS_VBUS_SENSE, FT_CBUS_BITBANG_WR,
      /// FT_CBUS_BITBANG_RD, FT_CBUS_TIME_STAMP, FT_CBUS_KEEP_AWAKE
      /// </summary>
      public byte Cbus6;
      /// <summary>Inverts the sense of the TXD line</summary>
      public byte InvertTXD;
      /// <summary>Inverts the sense of the RXD line</summary>
      public byte InvertRXD;
      /// <summary>Inverts the sense of the RTS line</summary>
      public byte InvertRTS;
      /// <summary>Inverts the sense of the CTS line</summary>
      public byte InvertCTS;
      /// <summary>Inverts the sense of the DTR line</summary>
      public byte InvertDTR;
      /// <summary>Inverts the sense of the DSR line</summary>
      public byte InvertDSR;
      /// <summary>Inverts the sense of the DCD line</summary>
      public byte InvertDCD;
      /// <summary>Inverts the sense of the RI line</summary>
      public byte InvertRI;
      /// <summary>
      /// Determines whether the Battery Charge Detection option is enabled.
      /// </summary>
      public byte BCDEnable;
      /// <summary>
      /// Asserts the power enable signal on CBUS when charging port detected.
      /// </summary>
      public byte BCDForceCbusPWREN;
      /// <summary>Forces the device never to go into sleep mode.</summary>
      public byte BCDDisableSleep;
      /// <summary>I2C slave device address.</summary>
      public ushort I2CSlaveAddress;
      /// <summary>I2C device ID</summary>
      public uint I2CDeviceId;
      /// <summary>Disable I2C Schmitt trigger.</summary>
      public byte I2CDisableSchmitt;
      /// <summary>
      /// FT1248 clock polarity - clock idle high (1) or clock idle low (0)
      /// </summary>
      public byte FT1248Cpol;
      /// <summary>FT1248 data is LSB (1) or MSB (0)</summary>
      public byte FT1248Lsb;
      /// <summary>FT1248 flow control enable.</summary>
      public byte FT1248FlowControl;
      /// <summary>Enable RS485 Echo Suppression</summary>
      public byte RS485EchoSuppress;
      /// <summary>Enable Power Save mode.</summary>
      public byte PowerSaveEnable;
      /// <summary>Determines whether the VCP driver is loaded.</summary>
      public byte IsVCP;
    }

    /// <summary>Exceptions thrown by errors within the FTDI class.</summary>
    [Serializable]
    public class FT_EXCEPTION : Exception
    {
      /// <summary>
      /// 
      /// </summary>
      public FT_EXCEPTION()
      {
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="message"></param>
      public FT_EXCEPTION(string message)
        : base(message)
      {
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="message"></param>
      /// <param name="inner"></param>
      public FT_EXCEPTION(string message, Exception inner)
        : base(message, inner)
      {
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="info"></param>
      /// <param name="context"></param>
      protected FT_EXCEPTION(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }
    }
  }
}
