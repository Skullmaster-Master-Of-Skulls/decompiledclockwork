using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Ports
{
	// Token: 0x02000415 RID: 1045
	internal sealed class SerialStream : Stream
	{
		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06002729 RID: 10025 RVA: 0x000B4028 File Offset: 0x000B2228
		// (remove) Token: 0x0600272A RID: 10026 RVA: 0x000B4060 File Offset: 0x000B2260
		internal event SerialDataReceivedEventHandler DataReceived;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x0600272B RID: 10027 RVA: 0x000B4098 File Offset: 0x000B2298
		// (remove) Token: 0x0600272C RID: 10028 RVA: 0x000B40D0 File Offset: 0x000B22D0
		internal event SerialPinChangedEventHandler PinChanged;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x0600272D RID: 10029 RVA: 0x000B4108 File Offset: 0x000B2308
		// (remove) Token: 0x0600272E RID: 10030 RVA: 0x000B4140 File Offset: 0x000B2340
		internal event SerialErrorReceivedEventHandler ErrorReceived;

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x0600272F RID: 10031 RVA: 0x000B4175 File Offset: 0x000B2375
		public override bool CanRead
		{
			get
			{
				return this._handle != null;
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06002730 RID: 10032 RVA: 0x000B4180 File Offset: 0x000B2380
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06002731 RID: 10033 RVA: 0x000B4183 File Offset: 0x000B2383
		public override bool CanTimeout
		{
			get
			{
				return this._handle != null;
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06002732 RID: 10034 RVA: 0x000B418E File Offset: 0x000B238E
		public override bool CanWrite
		{
			get
			{
				return this._handle != null;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06002733 RID: 10035 RVA: 0x000B4199 File Offset: 0x000B2399
		public override long Length
		{
			get
			{
				throw new NotSupportedException(SR.GetString("NotSupported_UnseekableStream"));
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06002734 RID: 10036 RVA: 0x000B41AA File Offset: 0x000B23AA
		// (set) Token: 0x06002735 RID: 10037 RVA: 0x000B41BB File Offset: 0x000B23BB
		public override long Position
		{
			get
			{
				throw new NotSupportedException(SR.GetString("NotSupported_UnseekableStream"));
			}
			set
			{
				throw new NotSupportedException(SR.GetString("NotSupported_UnseekableStream"));
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (set) Token: 0x06002736 RID: 10038 RVA: 0x000B41CC File Offset: 0x000B23CC
		internal int BaudRate
		{
			set
			{
				if (value > 0 && (value <= this.commProp.dwMaxBaud || this.commProp.dwMaxBaud <= 0))
				{
					if ((long)value != (long)((ulong)this.dcb.BaudRate))
					{
						int baudRate = (int)this.dcb.BaudRate;
						this.dcb.BaudRate = (uint)value;
						if (!UnsafeNativeMethods.SetCommState(this._handle, ref this.dcb))
						{
							this.dcb.BaudRate = (uint)baudRate;
							InternalResources.WinIOError();
						}
					}
					return;
				}
				if (this.commProp.dwMaxBaud == 0)
				{
					throw new ArgumentOutOfRangeException("baudRate", SR.GetString("ArgumentOutOfRange_NeedPosNum"));
				}
				throw new ArgumentOutOfRangeException("baudRate", SR.GetString("ArgumentOutOfRange_Bounds_Lower_Upper", new object[]
				{
					0,
					this.commProp.dwMaxBaud
				}));
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06002737 RID: 10039 RVA: 0x000B429E File Offset: 0x000B249E
		// (set) Token: 0x06002738 RID: 10040 RVA: 0x000B42A6 File Offset: 0x000B24A6
		public bool BreakState
		{
			get
			{
				return this.inBreak;
			}
			set
			{
				if (value)
				{
					if (!UnsafeNativeMethods.SetCommBreak(this._handle))
					{
						InternalResources.WinIOError();
					}
					this.inBreak = true;
					return;
				}
				if (!UnsafeNativeMethods.ClearCommBreak(this._handle))
				{
					InternalResources.WinIOError();
				}
				this.inBreak = false;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (set) Token: 0x06002739 RID: 10041 RVA: 0x000B42E0 File Offset: 0x000B24E0
		internal int DataBits
		{
			set
			{
				if (value != (int)this.dcb.ByteSize)
				{
					byte byteSize = this.dcb.ByteSize;
					this.dcb.ByteSize = (byte)value;
					if (!UnsafeNativeMethods.SetCommState(this._handle, ref this.dcb))
					{
						this.dcb.ByteSize = byteSize;
						InternalResources.WinIOError();
					}
				}
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (set) Token: 0x0600273A RID: 10042 RVA: 0x000B4338 File Offset: 0x000B2538
		internal bool DiscardNull
		{
			set
			{
				int dcbFlag = this.GetDcbFlag(11);
				if ((value && dcbFlag == 0) || (!value && dcbFlag == 1))
				{
					int setting = dcbFlag;
					this.SetDcbFlag(11, value ? 1 : 0);
					if (!UnsafeNativeMethods.SetCommState(this._handle, ref this.dcb))
					{
						this.SetDcbFlag(11, setting);
						InternalResources.WinIOError();
					}
				}
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x0600273B RID: 10043 RVA: 0x000B4390 File Offset: 0x000B2590
		// (set) Token: 0x0600273C RID: 10044 RVA: 0x000B43AC File Offset: 0x000B25AC
		internal bool DtrEnable
		{
			get
			{
				int dcbFlag = this.GetDcbFlag(4);
				return dcbFlag == 1;
			}
			set
			{
				int dcbFlag = this.GetDcbFlag(4);
				this.SetDcbFlag(4, value ? 1 : 0);
				if (!UnsafeNativeMethods.SetCommState(this._handle, ref this.dcb))
				{
					this.SetDcbFlag(4, dcbFlag);
					InternalResources.WinIOError();
				}
				if (!UnsafeNativeMethods.EscapeCommFunction(this._handle, value ? 5 : 6))
				{
					InternalResources.WinIOError();
				}
			}
		}

		// Token: 0x170009BA RID: 2490
		// (set) Token: 0x0600273D RID: 10045 RVA: 0x000B4408 File Offset: 0x000B2608
		internal Handshake Handshake
		{
			set
			{
				if (value != this.handshake)
				{
					Handshake handshake = this.handshake;
					int dcbFlag = this.GetDcbFlag(9);
					int dcbFlag2 = this.GetDcbFlag(2);
					int dcbFlag3 = this.GetDcbFlag(12);
					this.handshake = value;
					int setting = (this.handshake == Handshake.XOnXOff || this.handshake == Handshake.RequestToSendXOnXOff) ? 1 : 0;
					this.SetDcbFlag(9, setting);
					this.SetDcbFlag(8, setting);
					this.SetDcbFlag(2, (this.handshake == Handshake.RequestToSend || this.handshake == Handshake.RequestToSendXOnXOff) ? 1 : 0);
					if (this.handshake == Handshake.RequestToSend || this.handshake == Handshake.RequestToSendXOnXOff)
					{
						this.SetDcbFlag(12, 2);
					}
					else if (this.rtsEnable)
					{
						this.SetDcbFlag(12, 1);
					}
					else
					{
						this.SetDcbFlag(12, 0);
					}
					if (!UnsafeNativeMethods.SetCommState(this._handle, ref this.dcb))
					{
						this.handshake = handshake;
						this.SetDcbFlag(9, dcbFlag);
						this.SetDcbFlag(8, dcbFlag);
						this.SetDcbFlag(2, dcbFlag2);
						this.SetDcbFlag(12, dcbFlag3);
						InternalResources.WinIOError();
					}
				}
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x0600273E RID: 10046 RVA: 0x000B450B File Offset: 0x000B270B
		internal bool IsOpen
		{
			get
			{
				return this._handle != null && !this.eventRunner.ShutdownLoop;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (set) Token: 0x0600273F RID: 10047 RVA: 0x000B4528 File Offset: 0x000B2728
		internal Parity Parity
		{
			set
			{
				if ((byte)value != this.dcb.Parity)
				{
					byte parity = this.dcb.Parity;
					int dcbFlag = this.GetDcbFlag(1);
					byte errorChar = this.dcb.ErrorChar;
					int dcbFlag2 = this.GetDcbFlag(10);
					this.dcb.Parity = (byte)value;
					int num = (this.dcb.Parity == 0) ? 0 : 1;
					this.SetDcbFlag(1, num);
					if (num == 1)
					{
						this.SetDcbFlag(10, (this.parityReplace != 0) ? 1 : 0);
						this.dcb.ErrorChar = this.parityReplace;
					}
					else
					{
						this.SetDcbFlag(10, 0);
						this.dcb.ErrorChar = 0;
					}
					if (!UnsafeNativeMethods.SetCommState(this._handle, ref this.dcb))
					{
						this.dcb.Parity = parity;
						this.SetDcbFlag(1, dcbFlag);
						this.dcb.ErrorChar = errorChar;
						this.SetDcbFlag(10, dcbFlag2);
						InternalResources.WinIOError();
					}
				}
			}
		}

		// Token: 0x170009BD RID: 2493
		// (set) Token: 0x06002740 RID: 10048 RVA: 0x000B461C File Offset: 0x000B281C
		internal byte ParityReplace
		{
			set
			{
				if (value != this.parityReplace)
				{
					byte b = this.parityReplace;
					byte errorChar = this.dcb.ErrorChar;
					int dcbFlag = this.GetDcbFlag(10);
					this.parityReplace = value;
					if (this.GetDcbFlag(1) == 1)
					{
						this.SetDcbFlag(10, (this.parityReplace != 0) ? 1 : 0);
						this.dcb.ErrorChar = this.parityReplace;
					}
					else
					{
						this.SetDcbFlag(10, 0);
						this.dcb.ErrorChar = 0;
					}
					if (!UnsafeNativeMethods.SetCommState(this._handle, ref this.dcb))
					{
						this.parityReplace = b;
						this.SetDcbFlag(10, dcbFlag);
						this.dcb.ErrorChar = errorChar;
						InternalResources.WinIOError();
					}
				}
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06002741 RID: 10049 RVA: 0x000B46D4 File Offset: 0x000B28D4
		// (set) Token: 0x06002742 RID: 10050 RVA: 0x000B46F8 File Offset: 0x000B28F8
		public override int ReadTimeout
		{
			get
			{
				int readTotalTimeoutConstant = this.commTimeouts.ReadTotalTimeoutConstant;
				if (readTotalTimeoutConstant == -2)
				{
					return -1;
				}
				return readTotalTimeoutConstant;
			}
			set
			{
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("ReadTimeout", SR.GetString("ArgumentOutOfRange_Timeout"));
				}
				if (this._handle == null)
				{
					InternalResources.FileNotOpen();
				}
				int readTotalTimeoutConstant = this.commTimeouts.ReadTotalTimeoutConstant;
				int readIntervalTimeout = this.commTimeouts.ReadIntervalTimeout;
				int readTotalTimeoutMultiplier = this.commTimeouts.ReadTotalTimeoutMultiplier;
				if (value == 0)
				{
					this.commTimeouts.ReadTotalTimeoutConstant = 0;
					this.commTimeouts.ReadTotalTimeoutMultiplier = 0;
					this.commTimeouts.ReadIntervalTimeout = -1;
				}
				else if (value == -1)
				{
					this.commTimeouts.ReadTotalTimeoutConstant = -2;
					this.commTimeouts.ReadTotalTimeoutMultiplier = -1;
					this.commTimeouts.ReadIntervalTimeout = -1;
				}
				else
				{
					this.commTimeouts.ReadTotalTimeoutConstant = value;
					this.commTimeouts.ReadTotalTimeoutMultiplier = -1;
					this.commTimeouts.ReadIntervalTimeout = -1;
				}
				if (!UnsafeNativeMethods.SetCommTimeouts(this._handle, ref this.commTimeouts))
				{
					this.commTimeouts.ReadTotalTimeoutConstant = readTotalTimeoutConstant;
					this.commTimeouts.ReadTotalTimeoutMultiplier = readTotalTimeoutMultiplier;
					this.commTimeouts.ReadIntervalTimeout = readIntervalTimeout;
					InternalResources.WinIOError();
				}
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06002743 RID: 10051 RVA: 0x000B4808 File Offset: 0x000B2A08
		// (set) Token: 0x06002744 RID: 10052 RVA: 0x000B4838 File Offset: 0x000B2A38
		internal bool RtsEnable
		{
			get
			{
				int dcbFlag = this.GetDcbFlag(12);
				if (dcbFlag == 2)
				{
					throw new InvalidOperationException(SR.GetString("CantSetRtsWithHandshaking"));
				}
				return dcbFlag == 1;
			}
			set
			{
				if (this.handshake == Handshake.RequestToSend || this.handshake == Handshake.RequestToSendXOnXOff)
				{
					throw new InvalidOperationException(SR.GetString("CantSetRtsWithHandshaking"));
				}
				if (value != this.rtsEnable)
				{
					int dcbFlag = this.GetDcbFlag(12);
					this.rtsEnable = value;
					if (value)
					{
						this.SetDcbFlag(12, 1);
					}
					else
					{
						this.SetDcbFlag(12, 0);
					}
					if (!UnsafeNativeMethods.SetCommState(this._handle, ref this.dcb))
					{
						this.SetDcbFlag(12, dcbFlag);
						this.rtsEnable = !this.rtsEnable;
						InternalResources.WinIOError();
					}
					if (!UnsafeNativeMethods.EscapeCommFunction(this._handle, value ? 3 : 4))
					{
						InternalResources.WinIOError();
					}
				}
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (set) Token: 0x06002745 RID: 10053 RVA: 0x000B48E0 File Offset: 0x000B2AE0
		internal StopBits StopBits
		{
			set
			{
				byte b;
				if (value == StopBits.One)
				{
					b = 0;
				}
				else if (value == StopBits.OnePointFive)
				{
					b = 1;
				}
				else
				{
					b = 2;
				}
				if (b != this.dcb.StopBits)
				{
					byte stopBits = this.dcb.StopBits;
					this.dcb.StopBits = b;
					if (!UnsafeNativeMethods.SetCommState(this._handle, ref this.dcb))
					{
						this.dcb.StopBits = stopBits;
						InternalResources.WinIOError();
					}
				}
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06002746 RID: 10054 RVA: 0x000B494C File Offset: 0x000B2B4C
		// (set) Token: 0x06002747 RID: 10055 RVA: 0x000B496C File Offset: 0x000B2B6C
		public override int WriteTimeout
		{
			get
			{
				int writeTotalTimeoutConstant = this.commTimeouts.WriteTotalTimeoutConstant;
				if (writeTotalTimeoutConstant != 0)
				{
					return writeTotalTimeoutConstant;
				}
				return -1;
			}
			set
			{
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("WriteTimeout", SR.GetString("ArgumentOutOfRange_WriteTimeout"));
				}
				if (this._handle == null)
				{
					InternalResources.FileNotOpen();
				}
				int writeTotalTimeoutConstant = this.commTimeouts.WriteTotalTimeoutConstant;
				this.commTimeouts.WriteTotalTimeoutConstant = ((value == -1) ? 0 : value);
				if (!UnsafeNativeMethods.SetCommTimeouts(this._handle, ref this.commTimeouts))
				{
					this.commTimeouts.WriteTotalTimeoutConstant = writeTotalTimeoutConstant;
					InternalResources.WinIOError();
				}
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06002748 RID: 10056 RVA: 0x000B49E8 File Offset: 0x000B2BE8
		internal bool CDHolding
		{
			get
			{
				int num = 0;
				if (!UnsafeNativeMethods.GetCommModemStatus(this._handle, ref num))
				{
					InternalResources.WinIOError();
				}
				return (128 & num) != 0;
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06002749 RID: 10057 RVA: 0x000B4A18 File Offset: 0x000B2C18
		internal bool CtsHolding
		{
			get
			{
				int num = 0;
				if (!UnsafeNativeMethods.GetCommModemStatus(this._handle, ref num))
				{
					InternalResources.WinIOError();
				}
				return (16 & num) != 0;
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x0600274A RID: 10058 RVA: 0x000B4A44 File Offset: 0x000B2C44
		internal bool DsrHolding
		{
			get
			{
				int num = 0;
				if (!UnsafeNativeMethods.GetCommModemStatus(this._handle, ref num))
				{
					InternalResources.WinIOError();
				}
				return (32 & num) != 0;
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x0600274B RID: 10059 RVA: 0x000B4A70 File Offset: 0x000B2C70
		internal int BytesToRead
		{
			get
			{
				int num = 0;
				if (!UnsafeNativeMethods.ClearCommError(this._handle, ref num, ref this.comStat))
				{
					InternalResources.WinIOError();
				}
				return (int)this.comStat.cbInQue;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x000B4AA4 File Offset: 0x000B2CA4
		internal int BytesToWrite
		{
			get
			{
				int num = 0;
				if (!UnsafeNativeMethods.ClearCommError(this._handle, ref num, ref this.comStat))
				{
					InternalResources.WinIOError();
				}
				return (int)this.comStat.cbOutQue;
			}
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x000B4AD8 File Offset: 0x000B2CD8
		internal SerialStream(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits, int readTimeout, int writeTimeout, Handshake handshake, bool dtrEnable, bool rtsEnable, bool discardNull, byte parityReplace)
		{
			int dwFlagsAndAttributes = 1073741824;
			if (Environment.OSVersion.Platform == PlatformID.Win32Windows)
			{
				dwFlagsAndAttributes = 128;
				this.isAsync = false;
			}
			if (portName == null || !portName.StartsWith("COM", StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(SR.GetString("Arg_InvalidSerialPort"), "portName");
			}
			SafeFileHandle safeFileHandle = UnsafeNativeMethods.CreateFile("\\\\.\\" + portName, -1073741824, 0, IntPtr.Zero, 3, dwFlagsAndAttributes, IntPtr.Zero);
			if (safeFileHandle.IsInvalid)
			{
				InternalResources.WinIOError(portName);
			}
			try
			{
				int fileType = UnsafeNativeMethods.GetFileType(safeFileHandle);
				if (fileType != 2 && fileType != 0)
				{
					throw new ArgumentException(SR.GetString("Arg_InvalidSerialPort"), "portName");
				}
				this._handle = safeFileHandle;
				this.portName = portName;
				this.handshake = handshake;
				this.parityReplace = parityReplace;
				this.tempBuf = new byte[1];
				this.commProp = default(UnsafeNativeMethods.COMMPROP);
				int num = 0;
				if (!UnsafeNativeMethods.GetCommProperties(this._handle, ref this.commProp) || !UnsafeNativeMethods.GetCommModemStatus(this._handle, ref num))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error == 87 || lastWin32Error == 6)
					{
						throw new ArgumentException(SR.GetString("Arg_InvalidSerialPortExtended"), "portName");
					}
					InternalResources.WinIOError(lastWin32Error, string.Empty);
				}
				if (this.commProp.dwMaxBaud != 0 && baudRate > this.commProp.dwMaxBaud)
				{
					throw new ArgumentOutOfRangeException("baudRate", SR.GetString("Max_Baud", new object[]
					{
						this.commProp.dwMaxBaud
					}));
				}
				this.comStat = default(UnsafeNativeMethods.COMSTAT);
				this.dcb = default(UnsafeNativeMethods.DCB);
				this.InitializeDCB(baudRate, parity, dataBits, stopBits, discardNull);
				this.DtrEnable = dtrEnable;
				this.rtsEnable = (this.GetDcbFlag(12) == 1);
				if (handshake != Handshake.RequestToSend && handshake != Handshake.RequestToSendXOnXOff)
				{
					this.RtsEnable = rtsEnable;
				}
				if (readTimeout == 0)
				{
					this.commTimeouts.ReadTotalTimeoutConstant = 0;
					this.commTimeouts.ReadTotalTimeoutMultiplier = 0;
					this.commTimeouts.ReadIntervalTimeout = -1;
				}
				else if (readTimeout == -1)
				{
					this.commTimeouts.ReadTotalTimeoutConstant = -2;
					this.commTimeouts.ReadTotalTimeoutMultiplier = -1;
					this.commTimeouts.ReadIntervalTimeout = -1;
				}
				else
				{
					this.commTimeouts.ReadTotalTimeoutConstant = readTimeout;
					this.commTimeouts.ReadTotalTimeoutMultiplier = -1;
					this.commTimeouts.ReadIntervalTimeout = -1;
				}
				this.commTimeouts.WriteTotalTimeoutMultiplier = 0;
				this.commTimeouts.WriteTotalTimeoutConstant = ((writeTimeout == -1) ? 0 : writeTimeout);
				if (!UnsafeNativeMethods.SetCommTimeouts(this._handle, ref this.commTimeouts))
				{
					InternalResources.WinIOError();
				}
				if (this.isAsync && !ThreadPool.BindHandle(this._handle))
				{
					throw new IOException(SR.GetString("IO_BindHandleFailed"));
				}
				UnsafeNativeMethods.SetCommMask(this._handle, 507);
				this.eventRunner = new SerialStream.EventLoopRunner(this);
				Thread thread = LocalAppContextSwitches.DoNotCatchSerialStreamThreadExceptions ? new Thread(new ThreadStart(this.eventRunner.WaitForCommEvent)) : new Thread(new ThreadStart(this.eventRunner.SafelyWaitForCommEvent));
				thread.IsBackground = true;
				thread.Start();
			}
			catch
			{
				safeFileHandle.Close();
				this._handle = null;
				throw;
			}
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x000B4E28 File Offset: 0x000B3028
		~SerialStream()
		{
			this.Dispose(false);
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x000B4E58 File Offset: 0x000B3058
		protected override void Dispose(bool disposing)
		{
			if (this._handle != null && !this._handle.IsInvalid)
			{
				try
				{
					this.eventRunner.endEventLoop = true;
					Thread.MemoryBarrier();
					bool flag = false;
					UnsafeNativeMethods.SetCommMask(this._handle, 0);
					if (!UnsafeNativeMethods.EscapeCommFunction(this._handle, 6))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						if ((lastWin32Error == 5 || lastWin32Error == 22 || lastWin32Error == 1617) && !disposing)
						{
							flag = true;
						}
						else if (disposing)
						{
							InternalResources.WinIOError();
						}
					}
					if (!flag && !this._handle.IsClosed)
					{
						this.Flush();
					}
					this.eventRunner.waitCommEventWaitHandle.Set();
					if (!flag)
					{
						this.DiscardInBuffer();
						this.DiscardOutBuffer();
					}
					if (disposing && this.eventRunner != null)
					{
						this.eventRunner.eventLoopEndedSignal.WaitOne();
						this.eventRunner.eventLoopEndedSignal.Close();
						this.eventRunner.waitCommEventWaitHandle.Close();
					}
				}
				finally
				{
					if (disposing)
					{
						lock (this)
						{
							this._handle.Close();
							this._handle = null;
							goto IL_122;
						}
					}
					this._handle.Close();
					this._handle = null;
					IL_122:
					base.Dispose(disposing);
				}
			}
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x000B4FAC File Offset: 0x000B31AC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (numBytes < 0)
			{
				throw new ArgumentOutOfRangeException("numBytes", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (array.Length - offset < numBytes)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidOffLen"));
			}
			if (this._handle == null)
			{
				InternalResources.FileNotOpen();
			}
			int readTimeout = this.ReadTimeout;
			this.ReadTimeout = -1;
			IAsyncResult result;
			try
			{
				if (!this.isAsync)
				{
					result = base.BeginRead(array, offset, numBytes, userCallback, stateObject);
				}
				else
				{
					result = this.BeginReadCore(array, offset, numBytes, userCallback, stateObject);
				}
			}
			finally
			{
				this.ReadTimeout = readTimeout;
			}
			return result;
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x000B5070 File Offset: 0x000B3270
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			if (this.inBreak)
			{
				throw new InvalidOperationException(SR.GetString("In_Break_State"));
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (numBytes < 0)
			{
				throw new ArgumentOutOfRangeException("numBytes", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (array.Length - offset < numBytes)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidOffLen"));
			}
			if (this._handle == null)
			{
				InternalResources.FileNotOpen();
			}
			int writeTimeout = this.WriteTimeout;
			this.WriteTimeout = -1;
			IAsyncResult result;
			try
			{
				if (!this.isAsync)
				{
					result = base.BeginWrite(array, offset, numBytes, userCallback, stateObject);
				}
				else
				{
					result = this.BeginWriteCore(array, offset, numBytes, userCallback, stateObject);
				}
			}
			finally
			{
				this.WriteTimeout = writeTimeout;
			}
			return result;
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x000B514C File Offset: 0x000B334C
		internal void DiscardInBuffer()
		{
			if (!UnsafeNativeMethods.PurgeComm(this._handle, 10U))
			{
				InternalResources.WinIOError();
			}
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x000B5162 File Offset: 0x000B3362
		internal void DiscardOutBuffer()
		{
			if (!UnsafeNativeMethods.PurgeComm(this._handle, 5U))
			{
				InternalResources.WinIOError();
			}
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x000B5178 File Offset: 0x000B3378
		public unsafe override int EndRead(IAsyncResult asyncResult)
		{
			if (!this.isAsync)
			{
				return base.EndRead(asyncResult);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			SerialStream.SerialStreamAsyncResult serialStreamAsyncResult = asyncResult as SerialStream.SerialStreamAsyncResult;
			if (serialStreamAsyncResult == null || serialStreamAsyncResult._isWrite)
			{
				InternalResources.WrongAsyncResult();
			}
			if (1 == Interlocked.CompareExchange(ref serialStreamAsyncResult._EndXxxCalled, 1, 0))
			{
				InternalResources.EndReadCalledTwice();
			}
			bool flag = false;
			WaitHandle waitHandle = serialStreamAsyncResult._waitHandle;
			if (waitHandle != null)
			{
				try
				{
					waitHandle.WaitOne();
					if (serialStreamAsyncResult._numBytes == 0 && this.ReadTimeout == -1 && serialStreamAsyncResult._errorCode == 0)
					{
						flag = true;
					}
				}
				finally
				{
					waitHandle.Close();
				}
			}
			NativeOverlapped* overlapped = serialStreamAsyncResult._overlapped;
			if (overlapped != null)
			{
				Overlapped.Free(overlapped);
			}
			if (serialStreamAsyncResult._errorCode != 0)
			{
				InternalResources.WinIOError(serialStreamAsyncResult._errorCode, this.portName);
			}
			if (flag)
			{
				throw new IOException(SR.GetString("IO_OperationAborted"));
			}
			return serialStreamAsyncResult._numBytes;
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x000B525C File Offset: 0x000B345C
		public unsafe override void EndWrite(IAsyncResult asyncResult)
		{
			if (!this.isAsync)
			{
				base.EndWrite(asyncResult);
				return;
			}
			if (this.inBreak)
			{
				throw new InvalidOperationException(SR.GetString("In_Break_State"));
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			SerialStream.SerialStreamAsyncResult serialStreamAsyncResult = asyncResult as SerialStream.SerialStreamAsyncResult;
			if (serialStreamAsyncResult == null || !serialStreamAsyncResult._isWrite)
			{
				InternalResources.WrongAsyncResult();
			}
			if (1 == Interlocked.CompareExchange(ref serialStreamAsyncResult._EndXxxCalled, 1, 0))
			{
				InternalResources.EndWriteCalledTwice();
			}
			WaitHandle waitHandle = serialStreamAsyncResult._waitHandle;
			if (waitHandle != null)
			{
				try
				{
					waitHandle.WaitOne();
				}
				finally
				{
					waitHandle.Close();
				}
			}
			NativeOverlapped* overlapped = serialStreamAsyncResult._overlapped;
			if (overlapped != null)
			{
				Overlapped.Free(overlapped);
			}
			if (serialStreamAsyncResult._errorCode != 0)
			{
				InternalResources.WinIOError(serialStreamAsyncResult._errorCode, this.portName);
			}
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x000B5320 File Offset: 0x000B3520
		public override void Flush()
		{
			if (this._handle == null)
			{
				throw new ObjectDisposedException(SR.GetString("Port_not_open"));
			}
			UnsafeNativeMethods.FlushFileBuffers(this._handle);
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x000B5346 File Offset: 0x000B3546
		public override int Read([In] [Out] byte[] array, int offset, int count)
		{
			return this.Read(array, offset, count, this.ReadTimeout);
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x000B5358 File Offset: 0x000B3558
		internal int Read([In] [Out] byte[] array, int offset, int count, int timeout)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", SR.GetString("ArgumentNull_Buffer"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidOffLen"));
			}
			if (count == 0)
			{
				return 0;
			}
			if (this._handle == null)
			{
				InternalResources.FileNotOpen();
			}
			int num;
			if (this.isAsync)
			{
				IAsyncResult asyncResult = this.BeginReadCore(array, offset, count, null, null);
				num = this.EndRead(asyncResult);
			}
			else
			{
				int num2;
				num = this.ReadFileNative(array, offset, count, null, out num2);
				if (num == -1)
				{
					InternalResources.WinIOError();
				}
			}
			if (num == 0)
			{
				throw new TimeoutException();
			}
			return num;
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x000B541A File Offset: 0x000B361A
		public override int ReadByte()
		{
			return this.ReadByte(this.ReadTimeout);
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x000B5428 File Offset: 0x000B3628
		internal int ReadByte(int timeout)
		{
			if (this._handle == null)
			{
				InternalResources.FileNotOpen();
			}
			int num;
			if (this.isAsync)
			{
				IAsyncResult asyncResult = this.BeginReadCore(this.tempBuf, 0, 1, null, null);
				num = this.EndRead(asyncResult);
			}
			else
			{
				int num2;
				num = this.ReadFileNative(this.tempBuf, 0, 1, null, out num2);
				if (num == -1)
				{
					InternalResources.WinIOError();
				}
			}
			if (num == 0)
			{
				throw new TimeoutException();
			}
			return (int)this.tempBuf[0];
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x000B5494 File Offset: 0x000B3694
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("NotSupported_UnseekableStream"));
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x000B54A5 File Offset: 0x000B36A5
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("NotSupported_UnseekableStream"));
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x000B54B6 File Offset: 0x000B36B6
		internal void SetBufferSizes(int readBufferSize, int writeBufferSize)
		{
			if (this._handle == null)
			{
				InternalResources.FileNotOpen();
			}
			if (!UnsafeNativeMethods.SetupComm(this._handle, readBufferSize, writeBufferSize))
			{
				InternalResources.WinIOError();
			}
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x000B54D9 File Offset: 0x000B36D9
		public override void Write(byte[] array, int offset, int count)
		{
			this.Write(array, offset, count, this.WriteTimeout);
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x000B54EC File Offset: 0x000B36EC
		internal void Write(byte[] array, int offset, int count, int timeout)
		{
			if (this.inBreak)
			{
				throw new InvalidOperationException(SR.GetString("In_Break_State"));
			}
			if (array == null)
			{
				throw new ArgumentNullException("buffer", SR.GetString("ArgumentNull_Array"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.GetString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (count == 0)
			{
				return;
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException("count", SR.GetString("ArgumentOutOfRange_OffsetOut"));
			}
			if (this._handle == null)
			{
				InternalResources.FileNotOpen();
			}
			int num;
			if (this.isAsync)
			{
				IAsyncResult asyncResult = this.BeginWriteCore(array, offset, count, null, null);
				this.EndWrite(asyncResult);
				SerialStream.SerialStreamAsyncResult serialStreamAsyncResult = asyncResult as SerialStream.SerialStreamAsyncResult;
				num = serialStreamAsyncResult._numBytes;
			}
			else
			{
				int num2;
				num = this.WriteFileNative(array, offset, count, null, out num2);
				if (num == -1)
				{
					if (num2 == 1121)
					{
						throw new TimeoutException(SR.GetString("Write_timed_out"));
					}
					InternalResources.WinIOError();
				}
			}
			if (num == 0)
			{
				throw new TimeoutException(SR.GetString("Write_timed_out"));
			}
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x000B55F6 File Offset: 0x000B37F6
		public override void WriteByte(byte value)
		{
			this.WriteByte(value, this.WriteTimeout);
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x000B5608 File Offset: 0x000B3808
		internal void WriteByte(byte value, int timeout)
		{
			if (this.inBreak)
			{
				throw new InvalidOperationException(SR.GetString("In_Break_State"));
			}
			if (this._handle == null)
			{
				InternalResources.FileNotOpen();
			}
			this.tempBuf[0] = value;
			int num;
			if (this.isAsync)
			{
				IAsyncResult asyncResult = this.BeginWriteCore(this.tempBuf, 0, 1, null, null);
				this.EndWrite(asyncResult);
				SerialStream.SerialStreamAsyncResult serialStreamAsyncResult = asyncResult as SerialStream.SerialStreamAsyncResult;
				num = serialStreamAsyncResult._numBytes;
			}
			else
			{
				int num2;
				num = this.WriteFileNative(this.tempBuf, 0, 1, null, out num2);
				if (num == -1)
				{
					if (Marshal.GetLastWin32Error() == 1121)
					{
						throw new TimeoutException(SR.GetString("Write_timed_out"));
					}
					InternalResources.WinIOError();
				}
			}
			if (num == 0)
			{
				throw new TimeoutException(SR.GetString("Write_timed_out"));
			}
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x000B56C0 File Offset: 0x000B38C0
		private void InitializeDCB(int baudRate, Parity parity, int dataBits, StopBits stopBits, bool discardNull)
		{
			if (!UnsafeNativeMethods.GetCommState(this._handle, ref this.dcb))
			{
				InternalResources.WinIOError();
			}
			this.dcb.DCBlength = (uint)Marshal.SizeOf(this.dcb);
			this.dcb.BaudRate = (uint)baudRate;
			this.dcb.ByteSize = (byte)dataBits;
			switch (stopBits)
			{
			case StopBits.One:
				this.dcb.StopBits = 0;
				break;
			case StopBits.Two:
				this.dcb.StopBits = 2;
				break;
			case StopBits.OnePointFive:
				this.dcb.StopBits = 1;
				break;
			}
			this.dcb.Parity = (byte)parity;
			this.SetDcbFlag(1, (parity == Parity.None) ? 0 : 1);
			this.SetDcbFlag(0, 1);
			this.SetDcbFlag(2, (this.handshake == Handshake.RequestToSend || this.handshake == Handshake.RequestToSendXOnXOff) ? 1 : 0);
			this.SetDcbFlag(3, 0);
			this.SetDcbFlag(4, 0);
			this.SetDcbFlag(6, 0);
			this.SetDcbFlag(9, (this.handshake == Handshake.XOnXOff || this.handshake == Handshake.RequestToSendXOnXOff) ? 1 : 0);
			this.SetDcbFlag(8, (this.handshake == Handshake.XOnXOff || this.handshake == Handshake.RequestToSendXOnXOff) ? 1 : 0);
			if (parity != Parity.None)
			{
				this.SetDcbFlag(10, (this.parityReplace != 0) ? 1 : 0);
				this.dcb.ErrorChar = this.parityReplace;
			}
			else
			{
				this.SetDcbFlag(10, 0);
				this.dcb.ErrorChar = 0;
			}
			this.SetDcbFlag(11, discardNull ? 1 : 0);
			if (this.handshake == Handshake.RequestToSend || this.handshake == Handshake.RequestToSendXOnXOff)
			{
				this.SetDcbFlag(12, 2);
			}
			else if (this.GetDcbFlag(12) == 2)
			{
				this.SetDcbFlag(12, 0);
			}
			this.dcb.XonChar = 17;
			this.dcb.XoffChar = 19;
			this.dcb.XonLim = (this.dcb.XoffLim = (ushort)(this.commProp.dwCurrentRxQueue / 4));
			this.dcb.EofChar = 26;
			this.dcb.EvtChar = 26;
			if (!UnsafeNativeMethods.SetCommState(this._handle, ref this.dcb))
			{
				InternalResources.WinIOError();
			}
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x000B58E0 File Offset: 0x000B3AE0
		internal int GetDcbFlag(int whichFlag)
		{
			uint num;
			if (whichFlag == 4 || whichFlag == 12)
			{
				num = 3U;
			}
			else if (whichFlag == 15)
			{
				num = 131071U;
			}
			else
			{
				num = 1U;
			}
			uint num2 = this.dcb.Flags & num << whichFlag;
			return (int)(num2 >> whichFlag);
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x000B5924 File Offset: 0x000B3B24
		internal void SetDcbFlag(int whichFlag, int setting)
		{
			setting <<= whichFlag;
			uint num;
			if (whichFlag == 4 || whichFlag == 12)
			{
				num = 3U;
			}
			else if (whichFlag == 15)
			{
				num = 131071U;
			}
			else
			{
				num = 1U;
			}
			this.dcb.Flags = (this.dcb.Flags & ~(num << whichFlag));
			this.dcb.Flags = (this.dcb.Flags | (uint)setting);
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x000B597C File Offset: 0x000B3B7C
		private unsafe SerialStream.SerialStreamAsyncResult BeginReadCore(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			SerialStream.SerialStreamAsyncResult serialStreamAsyncResult = new SerialStream.SerialStreamAsyncResult();
			serialStreamAsyncResult._userCallback = userCallback;
			serialStreamAsyncResult._userStateObject = stateObject;
			serialStreamAsyncResult._isWrite = false;
			ManualResetEvent waitHandle = new ManualResetEvent(false);
			serialStreamAsyncResult._waitHandle = waitHandle;
			Overlapped overlapped = new Overlapped(0, 0, IntPtr.Zero, serialStreamAsyncResult);
			NativeOverlapped* overlapped2 = overlapped.Pack(SerialStream.IOCallback, array);
			serialStreamAsyncResult._overlapped = overlapped2;
			int num = 0;
			int num2 = this.ReadFileNative(array, offset, numBytes, overlapped2, out num);
			if (num2 == -1 && num != 997)
			{
				if (num == 38)
				{
					InternalResources.EndOfFile();
				}
				else
				{
					InternalResources.WinIOError(num, string.Empty);
				}
			}
			return serialStreamAsyncResult;
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x000B5A10 File Offset: 0x000B3C10
		private unsafe SerialStream.SerialStreamAsyncResult BeginWriteCore(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			SerialStream.SerialStreamAsyncResult serialStreamAsyncResult = new SerialStream.SerialStreamAsyncResult();
			serialStreamAsyncResult._userCallback = userCallback;
			serialStreamAsyncResult._userStateObject = stateObject;
			serialStreamAsyncResult._isWrite = true;
			ManualResetEvent waitHandle = new ManualResetEvent(false);
			serialStreamAsyncResult._waitHandle = waitHandle;
			Overlapped overlapped = new Overlapped(0, 0, IntPtr.Zero, serialStreamAsyncResult);
			NativeOverlapped* overlapped2 = overlapped.Pack(SerialStream.IOCallback, array);
			serialStreamAsyncResult._overlapped = overlapped2;
			int num = 0;
			int num2 = this.WriteFileNative(array, offset, numBytes, overlapped2, out num);
			if (num2 == -1 && num != 997)
			{
				if (num == 38)
				{
					InternalResources.EndOfFile();
				}
				else
				{
					InternalResources.WinIOError(num, string.Empty);
				}
			}
			return serialStreamAsyncResult;
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x000B5AA4 File Offset: 0x000B3CA4
		private unsafe int ReadFileNative(byte[] bytes, int offset, int count, NativeOverlapped* overlapped, out int hr)
		{
			if (bytes.Length - offset < count)
			{
				throw new IndexOutOfRangeException(SR.GetString("IndexOutOfRange_IORaceCondition"));
			}
			if (bytes.Length == 0)
			{
				hr = 0;
				return 0;
			}
			int result = 0;
			int num;
			fixed (byte[] array = bytes)
			{
				byte* ptr;
				if (bytes == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				if (this.isAsync)
				{
					num = UnsafeNativeMethods.ReadFile(this._handle, ptr + offset, count, IntPtr.Zero, overlapped);
				}
				else
				{
					num = UnsafeNativeMethods.ReadFile(this._handle, ptr + offset, count, out result, IntPtr.Zero);
				}
			}
			if (num == 0)
			{
				hr = Marshal.GetLastWin32Error();
				if (hr == 6)
				{
					this._handle.SetHandleAsInvalid();
				}
				return -1;
			}
			hr = 0;
			return result;
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x000B5B4C File Offset: 0x000B3D4C
		private unsafe int WriteFileNative(byte[] bytes, int offset, int count, NativeOverlapped* overlapped, out int hr)
		{
			if (bytes.Length - offset < count)
			{
				throw new IndexOutOfRangeException(SR.GetString("IndexOutOfRange_IORaceCondition"));
			}
			if (bytes.Length == 0)
			{
				hr = 0;
				return 0;
			}
			int result = 0;
			int num;
			fixed (byte[] array = bytes)
			{
				byte* ptr;
				if (bytes == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				if (this.isAsync)
				{
					num = UnsafeNativeMethods.WriteFile(this._handle, ptr + offset, count, IntPtr.Zero, overlapped);
				}
				else
				{
					num = UnsafeNativeMethods.WriteFile(this._handle, ptr + offset, count, out result, IntPtr.Zero);
				}
			}
			if (num == 0)
			{
				hr = Marshal.GetLastWin32Error();
				if (hr == 6)
				{
					this._handle.SetHandleAsInvalid();
				}
				return -1;
			}
			hr = 0;
			return result;
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x000B5BF4 File Offset: 0x000B3DF4
		private unsafe static void AsyncFSCallback(uint errorCode, uint numBytes, NativeOverlapped* pOverlapped)
		{
			Overlapped overlapped = Overlapped.Unpack(pOverlapped);
			SerialStream.SerialStreamAsyncResult serialStreamAsyncResult = (SerialStream.SerialStreamAsyncResult)overlapped.AsyncResult;
			serialStreamAsyncResult._numBytes = (int)numBytes;
			serialStreamAsyncResult._errorCode = (int)errorCode;
			serialStreamAsyncResult._completedSynchronously = false;
			serialStreamAsyncResult._isComplete = true;
			ManualResetEvent waitHandle = serialStreamAsyncResult._waitHandle;
			if (waitHandle != null && !waitHandle.Set())
			{
				InternalResources.WinIOError();
			}
			AsyncCallback userCallback = serialStreamAsyncResult._userCallback;
			if (userCallback != null)
			{
				userCallback(serialStreamAsyncResult);
			}
		}

		// Token: 0x04002145 RID: 8517
		private const int errorEvents = 271;

		// Token: 0x04002146 RID: 8518
		private const int receivedEvents = 3;

		// Token: 0x04002147 RID: 8519
		private const int pinChangedEvents = 376;

		// Token: 0x04002148 RID: 8520
		private const int infiniteTimeoutConst = -2;

		// Token: 0x04002149 RID: 8521
		private string portName;

		// Token: 0x0400214A RID: 8522
		private byte parityReplace = 63;

		// Token: 0x0400214B RID: 8523
		private bool inBreak;

		// Token: 0x0400214C RID: 8524
		private bool isAsync = true;

		// Token: 0x0400214D RID: 8525
		private Handshake handshake;

		// Token: 0x0400214E RID: 8526
		private bool rtsEnable;

		// Token: 0x0400214F RID: 8527
		private UnsafeNativeMethods.DCB dcb;

		// Token: 0x04002150 RID: 8528
		private UnsafeNativeMethods.COMMTIMEOUTS commTimeouts;

		// Token: 0x04002151 RID: 8529
		private UnsafeNativeMethods.COMSTAT comStat;

		// Token: 0x04002152 RID: 8530
		private UnsafeNativeMethods.COMMPROP commProp;

		// Token: 0x04002153 RID: 8531
		private const int maxDataBits = 8;

		// Token: 0x04002154 RID: 8532
		private const int minDataBits = 5;

		// Token: 0x04002155 RID: 8533
		internal SafeFileHandle _handle;

		// Token: 0x04002156 RID: 8534
		internal SerialStream.EventLoopRunner eventRunner;

		// Token: 0x04002157 RID: 8535
		private byte[] tempBuf;

		// Token: 0x04002158 RID: 8536
		private static readonly IOCompletionCallback IOCallback = new IOCompletionCallback(SerialStream.AsyncFSCallback);

		// Token: 0x02000814 RID: 2068
		internal sealed class EventLoopRunner
		{
			// Token: 0x06004502 RID: 17666 RVA: 0x00120D20 File Offset: 0x0011EF20
			internal EventLoopRunner(SerialStream stream)
			{
				this.handle = stream._handle;
				this.streamWeakReference = new WeakReference(stream);
				this.callErrorEvents = new WaitCallback(this.CallErrorEvents);
				this.callReceiveEvents = new WaitCallback(this.CallReceiveEvents);
				this.callPinEvents = new WaitCallback(this.CallPinEvents);
				this.freeNativeOverlappedCallback = new IOCompletionCallback(this.FreeNativeOverlappedCallback);
				this.isAsync = stream.isAsync;
			}

			// Token: 0x17000FA9 RID: 4009
			// (get) Token: 0x06004503 RID: 17667 RVA: 0x00120DB7 File Offset: 0x0011EFB7
			internal bool ShutdownLoop
			{
				get
				{
					return this.endEventLoop;
				}
			}

			// Token: 0x06004504 RID: 17668 RVA: 0x00120DC0 File Offset: 0x0011EFC0
			internal unsafe void WaitForCommEvent()
			{
				int num = 0;
				bool flag = false;
				NativeOverlapped* ptr = null;
				while (!this.ShutdownLoop)
				{
					SerialStream.SerialStreamAsyncResult serialStreamAsyncResult = null;
					if (this.isAsync)
					{
						serialStreamAsyncResult = new SerialStream.SerialStreamAsyncResult();
						serialStreamAsyncResult._userCallback = null;
						serialStreamAsyncResult._userStateObject = null;
						serialStreamAsyncResult._isWrite = false;
						serialStreamAsyncResult._numBytes = 2;
						serialStreamAsyncResult._waitHandle = this.waitCommEventWaitHandle;
						this.waitCommEventWaitHandle.Reset();
						Overlapped overlapped = new Overlapped(0, 0, this.waitCommEventWaitHandle.SafeWaitHandle.DangerousGetHandle(), serialStreamAsyncResult);
						ptr = overlapped.Pack(this.freeNativeOverlappedCallback, null);
					}
					try
					{
						fixed (int* ptr2 = &this.eventsOccurred)
						{
							int* lpEvtMask = ptr2;
							if (!UnsafeNativeMethods.WaitCommEvent(this.handle, lpEvtMask, ptr))
							{
								int lastWin32Error = Marshal.GetLastWin32Error();
								if (lastWin32Error == 5 || lastWin32Error == 22 || lastWin32Error == 1617)
								{
									flag = true;
									break;
								}
								if (lastWin32Error == 997)
								{
									bool flag2 = this.waitCommEventWaitHandle.WaitOne();
									int lastWin32Error2;
									do
									{
										flag2 = UnsafeNativeMethods.GetOverlappedResult(this.handle, ptr, ref num, false);
										lastWin32Error2 = Marshal.GetLastWin32Error();
									}
									while (lastWin32Error2 == 996 && !this.ShutdownLoop && !flag2);
									if (!flag2 && (lastWin32Error2 == 996 || lastWin32Error2 == 87) && !this.ShutdownLoop)
									{
									}
								}
							}
						}
					}
					finally
					{
						int* ptr2 = null;
					}
					if (!this.ShutdownLoop)
					{
						this.CallEvents(this.eventsOccurred);
					}
					if (this.isAsync && Interlocked.Decrement(ref serialStreamAsyncResult._numBytes) == 0)
					{
						Overlapped.Free(ptr);
					}
				}
				if (flag)
				{
					this.endEventLoop = true;
					Overlapped.Free(ptr);
				}
				this.eventLoopEndedSignal.Set();
			}

			// Token: 0x06004505 RID: 17669 RVA: 0x00120F5C File Offset: 0x0011F15C
			private unsafe void FreeNativeOverlappedCallback(uint errorCode, uint numBytes, NativeOverlapped* pOverlapped)
			{
				Overlapped overlapped = Overlapped.Unpack(pOverlapped);
				SerialStream.SerialStreamAsyncResult serialStreamAsyncResult = (SerialStream.SerialStreamAsyncResult)overlapped.AsyncResult;
				if (Interlocked.Decrement(ref serialStreamAsyncResult._numBytes) == 0)
				{
					Overlapped.Free(pOverlapped);
				}
			}

			// Token: 0x06004506 RID: 17670 RVA: 0x00120F90 File Offset: 0x0011F190
			private void CallEvents(int nativeEvents)
			{
				if ((nativeEvents & 129) != 0)
				{
					int num = 0;
					if (!UnsafeNativeMethods.ClearCommError(this.handle, ref num, IntPtr.Zero))
					{
						this.endEventLoop = true;
						Thread.MemoryBarrier();
						return;
					}
					num &= 271;
					if (num != 0)
					{
						ThreadPool.QueueUserWorkItem(this.callErrorEvents, num);
					}
				}
				if ((nativeEvents & 376) != 0)
				{
					ThreadPool.QueueUserWorkItem(this.callPinEvents, nativeEvents);
				}
				if ((nativeEvents & 3) != 0)
				{
					ThreadPool.QueueUserWorkItem(this.callReceiveEvents, nativeEvents);
				}
			}

			// Token: 0x06004507 RID: 17671 RVA: 0x00121018 File Offset: 0x0011F218
			private void CallErrorEvents(object state)
			{
				int num = (int)state;
				SerialStream serialStream = (SerialStream)this.streamWeakReference.Target;
				if (serialStream == null)
				{
					return;
				}
				if (serialStream.ErrorReceived != null)
				{
					if ((num & 256) != 0)
					{
						serialStream.ErrorReceived(serialStream, new SerialErrorReceivedEventArgs(SerialError.TXFull));
					}
					if ((num & 1) != 0)
					{
						serialStream.ErrorReceived(serialStream, new SerialErrorReceivedEventArgs(SerialError.RXOver));
					}
					if ((num & 2) != 0)
					{
						serialStream.ErrorReceived(serialStream, new SerialErrorReceivedEventArgs(SerialError.Overrun));
					}
					if ((num & 4) != 0)
					{
						serialStream.ErrorReceived(serialStream, new SerialErrorReceivedEventArgs(SerialError.RXParity));
					}
					if ((num & 8) != 0)
					{
						serialStream.ErrorReceived(serialStream, new SerialErrorReceivedEventArgs(SerialError.Frame));
					}
				}
			}

			// Token: 0x06004508 RID: 17672 RVA: 0x001210C8 File Offset: 0x0011F2C8
			private void CallReceiveEvents(object state)
			{
				int num = (int)state;
				SerialStream serialStream = (SerialStream)this.streamWeakReference.Target;
				if (serialStream == null)
				{
					return;
				}
				if (serialStream.DataReceived != null)
				{
					if ((num & 1) != 0)
					{
						serialStream.DataReceived(serialStream, new SerialDataReceivedEventArgs(SerialData.Chars));
					}
					if ((num & 2) != 0)
					{
						serialStream.DataReceived(serialStream, new SerialDataReceivedEventArgs(SerialData.Eof));
					}
				}
			}

			// Token: 0x06004509 RID: 17673 RVA: 0x0012112C File Offset: 0x0011F32C
			private void CallPinEvents(object state)
			{
				int num = (int)state;
				SerialStream serialStream = (SerialStream)this.streamWeakReference.Target;
				if (serialStream == null)
				{
					return;
				}
				if (serialStream.PinChanged != null)
				{
					if ((num & 8) != 0)
					{
						serialStream.PinChanged(serialStream, new SerialPinChangedEventArgs(SerialPinChange.CtsChanged));
					}
					if ((num & 16) != 0)
					{
						serialStream.PinChanged(serialStream, new SerialPinChangedEventArgs(SerialPinChange.DsrChanged));
					}
					if ((num & 32) != 0)
					{
						serialStream.PinChanged(serialStream, new SerialPinChangedEventArgs(SerialPinChange.CDChanged));
					}
					if ((num & 256) != 0)
					{
						serialStream.PinChanged(serialStream, new SerialPinChangedEventArgs(SerialPinChange.Ring));
					}
					if ((num & 64) != 0)
					{
						serialStream.PinChanged(serialStream, new SerialPinChangedEventArgs(SerialPinChange.Break));
					}
				}
			}

			// Token: 0x0600450A RID: 17674 RVA: 0x001211E4 File Offset: 0x0011F3E4
			internal void SafelyWaitForCommEvent()
			{
				try
				{
					this.WaitForCommEvent();
				}
				catch (ObjectDisposedException)
				{
				}
				catch (Exception ex)
				{
				}
			}

			// Token: 0x04003585 RID: 13701
			private WeakReference streamWeakReference;

			// Token: 0x04003586 RID: 13702
			internal ManualResetEvent eventLoopEndedSignal = new ManualResetEvent(false);

			// Token: 0x04003587 RID: 13703
			internal ManualResetEvent waitCommEventWaitHandle = new ManualResetEvent(false);

			// Token: 0x04003588 RID: 13704
			private SafeFileHandle handle;

			// Token: 0x04003589 RID: 13705
			private bool isAsync;

			// Token: 0x0400358A RID: 13706
			internal bool endEventLoop;

			// Token: 0x0400358B RID: 13707
			private int eventsOccurred;

			// Token: 0x0400358C RID: 13708
			private WaitCallback callErrorEvents;

			// Token: 0x0400358D RID: 13709
			private WaitCallback callReceiveEvents;

			// Token: 0x0400358E RID: 13710
			private WaitCallback callPinEvents;

			// Token: 0x0400358F RID: 13711
			private IOCompletionCallback freeNativeOverlappedCallback;
		}

		// Token: 0x02000815 RID: 2069
		internal sealed class SerialStreamAsyncResult : IAsyncResult
		{
			// Token: 0x17000FAA RID: 4010
			// (get) Token: 0x0600450B RID: 17675 RVA: 0x0012121C File Offset: 0x0011F41C
			public object AsyncState
			{
				get
				{
					return this._userStateObject;
				}
			}

			// Token: 0x17000FAB RID: 4011
			// (get) Token: 0x0600450C RID: 17676 RVA: 0x00121224 File Offset: 0x0011F424
			public bool IsCompleted
			{
				get
				{
					return this._isComplete;
				}
			}

			// Token: 0x17000FAC RID: 4012
			// (get) Token: 0x0600450D RID: 17677 RVA: 0x0012122C File Offset: 0x0011F42C
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					return this._waitHandle;
				}
			}

			// Token: 0x17000FAD RID: 4013
			// (get) Token: 0x0600450E RID: 17678 RVA: 0x00121234 File Offset: 0x0011F434
			public bool CompletedSynchronously
			{
				get
				{
					return this._completedSynchronously;
				}
			}

			// Token: 0x04003590 RID: 13712
			internal AsyncCallback _userCallback;

			// Token: 0x04003591 RID: 13713
			internal object _userStateObject;

			// Token: 0x04003592 RID: 13714
			internal bool _isWrite;

			// Token: 0x04003593 RID: 13715
			internal bool _isComplete;

			// Token: 0x04003594 RID: 13716
			internal bool _completedSynchronously;

			// Token: 0x04003595 RID: 13717
			internal ManualResetEvent _waitHandle;

			// Token: 0x04003596 RID: 13718
			internal int _EndXxxCalled;

			// Token: 0x04003597 RID: 13719
			internal int _numBytes;

			// Token: 0x04003598 RID: 13720
			internal int _errorCode;

			// Token: 0x04003599 RID: 13721
			internal unsafe NativeOverlapped* _overlapped;
		}
	}
}
