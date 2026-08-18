using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO
{
	// Token: 0x020000A0 RID: 160
	internal class LogStream : BufferedStream2
	{
		// Token: 0x0600045F RID: 1119 RVA: 0x0000C978 File Offset: 0x0000AB78
		[SecurityCritical]
		internal LogStream(string path, int bufferSize, LogRetentionOption retention, long maxFileSize, int maxNumOfFiles)
		{
			string fullPath = Path.GetFullPath(path);
			this._fileName = fullPath;
			if (fullPath.StartsWith("\\\\.\\", StringComparison.Ordinal))
			{
				throw new NotSupportedException(SR.GetString("NotSupported_IONonFileDevices"));
			}
			UnsafeNativeMethods.SECURITY_ATTRIBUTES secAttrs = LogStream.GetSecAttrs(FileShare.Read);
			int flagsAndAttributesSav = 1048576;
			this._canWrite = true;
			this._pathSav = fullPath;
			this._fAccessSav = 1073741824;
			this._shareSav = FileShare.Read;
			this._secAttrsSav = secAttrs;
			this._secAccessSav = FileIOPermissionAccess.Write;
			this._modeSav = ((retention != LogRetentionOption.SingleFileUnboundedSize) ? FileMode.Create : FileMode.OpenOrCreate);
			this._flagsAndAttributesSav = flagsAndAttributesSav;
			this._seekToEndSav = (retention == LogRetentionOption.SingleFileUnboundedSize);
			this.bufferSize = bufferSize;
			this._retention = retention;
			this._maxFileSize = maxFileSize;
			this._maxNumberOfFiles = maxNumOfFiles;
			this._Init(fullPath, this._fAccessSav, this._shareSav, this._secAttrsSav, this._secAccessSav, this._modeSav, this._flagsAndAttributesSav, this._seekToEndSav);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000CA8C File Offset: 0x0000AC8C
		[SecurityCritical]
		internal void _Init(string path, int fAccess, FileShare share, UnsafeNativeMethods.SECURITY_ATTRIBUTES secAttrs, FileIOPermissionAccess secAccess, FileMode mode, int flagsAndAttributes, bool seekToEnd)
		{
			string fullPath = Path.GetFullPath(path);
			this._fileName = fullPath;
			new FileIOPermission(secAccess, new string[]
			{
				fullPath
			}).Demand();
			int errorMode = UnsafeNativeMethods.SetErrorMode(1);
			try
			{
				this._handle = UnsafeNativeMethods.SafeCreateFile(fullPath, fAccess, share, secAttrs, mode, flagsAndAttributes, UnsafeNativeMethods.NULL);
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (this._handle.IsInvalid)
				{
					bool flag = false;
					try
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, new string[]
						{
							this._fileName
						}).Demand();
						flag = true;
					}
					catch (SecurityException)
					{
					}
					if (flag)
					{
						__Error.WinIOError(lastWin32Error, this._fileName);
					}
					else
					{
						__Error.WinIOError(lastWin32Error, Path.GetFileName(this._fileName));
					}
				}
			}
			finally
			{
				UnsafeNativeMethods.SetErrorMode(errorMode);
			}
			this.pos = 0L;
			if (seekToEnd)
			{
				this.SeekCore(0L, SeekOrigin.End);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000CB74 File Offset: 0x0000AD74
		public override bool CanRead
		{
			get
			{
				return this._canRead;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000CB7C File Offset: 0x0000AD7C
		public override bool CanWrite
		{
			get
			{
				return this._canWrite;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000CB84 File Offset: 0x0000AD84
		public override bool CanSeek
		{
			get
			{
				return this._canSeek;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000CB8C File Offset: 0x0000AD8C
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000CB93 File Offset: 0x0000AD93
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x0000CB9A File Offset: 0x0000AD9A
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000CBA1 File Offset: 0x0000ADA1
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000CBA8 File Offset: 0x0000ADA8
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000CBAF File Offset: 0x0000ADAF
		public override int Read(byte[] array, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000CBB8 File Offset: 0x0000ADB8
		[SecurityCritical]
		protected override void WriteCore(byte[] buffer, int offset, int count, bool blockForWrite, out long streamPos)
		{
			int num = 0;
			int num2 = this.WriteFileNative(buffer, offset, count, null, out num);
			if (num2 == -1)
			{
				if (num == 232)
				{
					num2 = 0;
				}
				else
				{
					if (num == 87)
					{
						throw new IOException(SR.GetString("IO_FileTooLongOrHandleNotSync"));
					}
					__Error.WinIOError(num, string.Empty);
				}
			}
			streamPos = base.AddUnderlyingStreamPosition((long)num2);
			this.EnforceRetentionPolicy(this._handle, streamPos);
			streamPos = this.pos;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000CC28 File Offset: 0x0000AE28
		[SecurityCritical]
		private unsafe int WriteFileNative(byte[] bytes, int offset, int count, NativeOverlapped* overlapped, out int hr)
		{
			if (this._handle.IsClosed)
			{
				__Error.FileNotOpen();
			}
			if (this._disableLogging)
			{
				hr = 0;
				return 0;
			}
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
				num = UnsafeNativeMethods.WriteFile(this._handle, ptr + offset, count, out result, overlapped);
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

		// Token: 0x0600046C RID: 1132 RVA: 0x0000CCCC File Offset: 0x0000AECC
		[SecurityCritical]
		private long SeekCore(long offset, SeekOrigin origin)
		{
			int num = 0;
			long num2 = UnsafeNativeMethods.SetFilePointer(this._handle, offset, origin, out num);
			if (num2 == -1L)
			{
				if (num == 6)
				{
					this._handle.SetHandleAsInvalid();
				}
				__Error.WinIOError(num, string.Empty);
			}
			base.UnderlyingStreamPosition = num2;
			return num2;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000CD18 File Offset: 0x0000AF18
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this._handle == null || this._handle.IsClosed)
				{
					base.DiscardBuffer();
				}
			}
			finally
			{
				try
				{
					base.Dispose(disposing);
				}
				finally
				{
					if (this._handle != null && !this._handle.IsClosed)
					{
						this._handle.Dispose();
					}
					this._handle = null;
					this._canRead = false;
					this._canWrite = false;
					this._canSeek = false;
				}
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000CDA8 File Offset: 0x0000AFA8
		[SecurityCritical]
		~LogStream()
		{
			if (this._handle != null)
			{
				this.Dispose(false);
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000CDE0 File Offset: 0x0000AFE0
		[SecurityCritical]
		private void EnforceRetentionPolicy(SafeFileHandle handle, long lastPos)
		{
			switch (this._retention)
			{
			case LogRetentionOption.UnlimitedSequentialFiles:
			case LogRetentionOption.LimitedCircularFiles:
			case LogRetentionOption.LimitedSequentialFiles:
			{
				if (lastPos < this._maxFileSize || handle != this._handle)
				{
					return;
				}
				object lockObject = this.m_lockObject;
				lock (lockObject)
				{
					if (handle != this._handle || lastPos < this._maxFileSize)
					{
						return;
					}
					this._currentFileNum++;
					if (this._retention == LogRetentionOption.LimitedCircularFiles && this._currentFileNum > this._maxNumberOfFiles)
					{
						this._currentFileNum = 1;
					}
					else if (this._retention == LogRetentionOption.LimitedSequentialFiles && this._currentFileNum > this._maxNumberOfFiles)
					{
						this._DisableLogging();
						return;
					}
					if (this._fileNameWithoutExt == null)
					{
						this._fileNameWithoutExt = Path.Combine(Path.GetDirectoryName(this._pathSav), Path.GetFileNameWithoutExtension(this._pathSav));
						this._fileExt = Path.GetExtension(this._pathSav);
					}
					string path = (this._currentFileNum == 1) ? this._pathSav : (this._fileNameWithoutExt + this._currentFileNum.ToString(CultureInfo.InvariantCulture) + this._fileExt);
					try
					{
						this._Init(path, this._fAccessSav, this._shareSav, this._secAttrsSav, this._secAccessSav, this._modeSav, this._flagsAndAttributesSav, this._seekToEndSav);
						if (handle != null && !handle.IsClosed)
						{
							handle.Dispose();
						}
						return;
					}
					catch (IOException)
					{
						this._handle = handle;
						this._retentionRetryCount++;
						if (this._retentionRetryCount >= 2)
						{
							this._DisableLogging();
						}
						return;
					}
					catch (UnauthorizedAccessException)
					{
						this._DisableLogging();
						return;
					}
					catch (Exception)
					{
						this._DisableLogging();
						return;
					}
				}
				break;
			}
			case LogRetentionOption.SingleFileUnboundedSize:
				return;
			case LogRetentionOption.SingleFileBoundedSize:
				break;
			default:
				return;
			}
			if (lastPos >= this._maxFileSize)
			{
				this._DisableLogging();
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000D008 File Offset: 0x0000B208
		[MethodImpl(MethodImplOptions.Synchronized)]
		private void _DisableLogging()
		{
			this._disableLogging = true;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000D014 File Offset: 0x0000B214
		[SecurityCritical]
		private static UnsafeNativeMethods.SECURITY_ATTRIBUTES GetSecAttrs(FileShare share)
		{
			UnsafeNativeMethods.SECURITY_ATTRIBUTES security_ATTRIBUTES = null;
			if ((share & FileShare.Inheritable) != FileShare.None)
			{
				security_ATTRIBUTES = new UnsafeNativeMethods.SECURITY_ATTRIBUTES();
				security_ATTRIBUTES.nLength = Marshal.SizeOf(security_ATTRIBUTES);
				security_ATTRIBUTES.bInheritHandle = 1;
			}
			return security_ATTRIBUTES;
		}

		// Token: 0x040004F8 RID: 1272
		internal const long DefaultFileSize = 10240000L;

		// Token: 0x040004F9 RID: 1273
		internal const int DefaultNumberOfFiles = 2;

		// Token: 0x040004FA RID: 1274
		internal const LogRetentionOption DefaultRetention = LogRetentionOption.SingleFileUnboundedSize;

		// Token: 0x040004FB RID: 1275
		private const int _retentionRetryThreshold = 2;

		// Token: 0x040004FC RID: 1276
		private LogRetentionOption _retention;

		// Token: 0x040004FD RID: 1277
		private long _maxFileSize = 10240000L;

		// Token: 0x040004FE RID: 1278
		private int _maxNumberOfFiles = 2;

		// Token: 0x040004FF RID: 1279
		private int _currentFileNum = 1;

		// Token: 0x04000500 RID: 1280
		private bool _disableLogging;

		// Token: 0x04000501 RID: 1281
		private int _retentionRetryCount;

		// Token: 0x04000502 RID: 1282
		private bool _canRead;

		// Token: 0x04000503 RID: 1283
		private bool _canWrite;

		// Token: 0x04000504 RID: 1284
		private bool _canSeek;

		// Token: 0x04000505 RID: 1285
		[SecurityCritical]
		private SafeFileHandle _handle;

		// Token: 0x04000506 RID: 1286
		private string _fileName;

		// Token: 0x04000507 RID: 1287
		private string _fileNameWithoutExt;

		// Token: 0x04000508 RID: 1288
		private string _fileExt;

		// Token: 0x04000509 RID: 1289
		private string _pathSav;

		// Token: 0x0400050A RID: 1290
		private int _fAccessSav;

		// Token: 0x0400050B RID: 1291
		private FileShare _shareSav;

		// Token: 0x0400050C RID: 1292
		private UnsafeNativeMethods.SECURITY_ATTRIBUTES _secAttrsSav;

		// Token: 0x0400050D RID: 1293
		private FileIOPermissionAccess _secAccessSav;

		// Token: 0x0400050E RID: 1294
		private FileMode _modeSav;

		// Token: 0x0400050F RID: 1295
		private int _flagsAndAttributesSav;

		// Token: 0x04000510 RID: 1296
		private bool _seekToEndSav;

		// Token: 0x04000511 RID: 1297
		private readonly object m_lockObject = new object();
	}
}
