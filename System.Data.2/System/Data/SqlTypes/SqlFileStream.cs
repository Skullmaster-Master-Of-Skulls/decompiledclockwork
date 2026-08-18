using System;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Data.SqlTypes
{
	// Token: 0x0200015C RID: 348
	public sealed class SqlFileStream : Stream
	{
		// Token: 0x06001572 RID: 5490 RVA: 0x000A2288 File Offset: 0x000A1688
		public SqlFileStream(string path, byte[] transactionContext, FileAccess access) : this(path, transactionContext, access, FileOptions.None, 0L)
		{
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x000A22A4 File Offset: 0x000A16A4
		public SqlFileStream(string path, byte[] transactionContext, FileAccess access, FileOptions options, long allocationSize)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlFileStream.ctor|API> %d# access=%d options=%d path='%ls' ", this.ObjectID, (int)access, (int)options, path);
			try
			{
				if (transactionContext == null)
				{
					throw ADP.ArgumentNull("transactionContext");
				}
				if (path == null)
				{
					throw ADP.ArgumentNull("path");
				}
				this.m_disposed = false;
				this.m_fs = null;
				this.OpenSqlFileStream(path, transactionContext, access, options, allocationSize);
				this.Name = path;
				this.TransactionContext = transactionContext;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x000A2348 File Offset: 0x000A1748
		~SqlFileStream()
		{
			this.Dispose(false);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x000A2384 File Offset: 0x000A1784
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this.m_disposed)
				{
					try
					{
						if (disposing && this.m_fs != null)
						{
							this.m_fs.Close();
							this.m_fs = null;
						}
					}
					finally
					{
						this.m_disposed = true;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x000A2400 File Offset: 0x000A1800
		// (set) Token: 0x06001577 RID: 5495 RVA: 0x000A2414 File Offset: 0x000A1814
		public string Name
		{
			get
			{
				return this.m_path;
			}
			private set
			{
				this.m_path = SqlFileStream.GetFullPathInternal(value);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x000A2430 File Offset: 0x000A1830
		// (set) Token: 0x06001579 RID: 5497 RVA: 0x000A2458 File Offset: 0x000A1858
		public byte[] TransactionContext
		{
			get
			{
				if (this.m_txn == null)
				{
					return null;
				}
				return (byte[])this.m_txn.Clone();
			}
			private set
			{
				this.m_txn = (byte[])value.Clone();
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x000A2478 File Offset: 0x000A1878
		public override bool CanRead
		{
			get
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				return this.m_fs.CanRead;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x000A24A0 File Offset: 0x000A18A0
		public override bool CanSeek
		{
			get
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				return this.m_fs.CanSeek;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x000A24C8 File Offset: 0x000A18C8
		[ComVisible(false)]
		public override bool CanTimeout
		{
			get
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				return this.m_fs.CanTimeout;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x000A24F0 File Offset: 0x000A18F0
		public override bool CanWrite
		{
			get
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				return this.m_fs.CanWrite;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x000A2518 File Offset: 0x000A1918
		public override long Length
		{
			get
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				return this.m_fs.Length;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x000A2540 File Offset: 0x000A1940
		// (set) Token: 0x06001580 RID: 5504 RVA: 0x000A2568 File Offset: 0x000A1968
		public override long Position
		{
			get
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				return this.m_fs.Position;
			}
			set
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				this.m_fs.Position = value;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x000A2590 File Offset: 0x000A1990
		// (set) Token: 0x06001582 RID: 5506 RVA: 0x000A25B8 File Offset: 0x000A19B8
		[ComVisible(false)]
		public override int ReadTimeout
		{
			get
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				return this.m_fs.ReadTimeout;
			}
			set
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				this.m_fs.ReadTimeout = value;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x000A25E0 File Offset: 0x000A19E0
		// (set) Token: 0x06001584 RID: 5508 RVA: 0x000A2608 File Offset: 0x000A1A08
		[ComVisible(false)]
		public override int WriteTimeout
		{
			get
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				return this.m_fs.WriteTimeout;
			}
			set
			{
				if (this.m_disposed)
				{
					throw ADP.ObjectDisposed(this);
				}
				this.m_fs.WriteTimeout = value;
			}
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x000A2630 File Offset: 0x000A1A30
		public override void Flush()
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			this.m_fs.Flush();
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x000A2658 File Offset: 0x000A1A58
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			return this.m_fs.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x000A2688 File Offset: 0x000A1A88
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			return this.m_fs.EndRead(asyncResult);
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x000A26B0 File Offset: 0x000A1AB0
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			IAsyncResult result = this.m_fs.BeginWrite(buffer, offset, count, callback, state);
			if (count == 1)
			{
				this.m_fs.Flush();
			}
			return result;
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x000A26F0 File Offset: 0x000A1AF0
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			this.m_fs.EndWrite(asyncResult);
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x000A2718 File Offset: 0x000A1B18
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			return this.m_fs.Seek(offset, origin);
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x000A2744 File Offset: 0x000A1B44
		public override void SetLength(long value)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			this.m_fs.SetLength(value);
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x000A276C File Offset: 0x000A1B6C
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			return this.m_fs.Read(buffer, offset, count);
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x000A2798 File Offset: 0x000A1B98
		public override int ReadByte()
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			return this.m_fs.ReadByte();
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x000A27C0 File Offset: 0x000A1BC0
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			this.m_fs.Write(buffer, offset, count);
			if (count == 1)
			{
				this.m_fs.Flush();
			}
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x000A27FC File Offset: 0x000A1BFC
		public override void WriteByte(byte value)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			this.m_fs.WriteByte(value);
			this.m_fs.Flush();
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x000A2830 File Offset: 0x000A1C30
		[Conditional("DEBUG")]
		private static void AssertPathFormat(string path)
		{
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x000A2840 File Offset: 0x000A1C40
		private static string GetFullPathInternal(string path)
		{
			path = path.Trim();
			if (path.Length == 0)
			{
				throw ADP.Argument(Res.GetString("SqlFileStream_InvalidPath"), "path");
			}
			if (path.Length > 32766)
			{
				throw ADP.Argument(Res.GetString("SqlFileStream_InvalidPath"), "path");
			}
			if (path.IndexOfAny(SqlFileStream.InvalidPathChars) >= 0)
			{
				throw ADP.Argument(Res.GetString("SqlFileStream_InvalidPath"), "path");
			}
			if (!path.StartsWith("\\\\", StringComparison.OrdinalIgnoreCase))
			{
				throw ADP.Argument(Res.GetString("SqlFileStream_InvalidPath"), "path");
			}
			path = UnsafeNativeMethods.SafeGetFullPathName(path);
			if (path.StartsWith("\\\\.\\", StringComparison.Ordinal))
			{
				throw ADP.Argument(Res.GetString("SqlFileStream_PathNotValidDiskResource"), "path");
			}
			return path;
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x000A2908 File Offset: 0x000A1D08
		private static void DemandAccessPermission(string path, FileAccess access)
		{
			FileIOPermissionAccess fileIOPermissionAccess;
			switch (access)
			{
			case FileAccess.Read:
				fileIOPermissionAccess = FileIOPermissionAccess.Read;
				goto IL_20;
			case FileAccess.Write:
				fileIOPermissionAccess = FileIOPermissionAccess.Write;
				goto IL_20;
			}
			fileIOPermissionAccess = (FileIOPermissionAccess.Read | FileIOPermissionAccess.Write);
			IL_20:
			bool flag = false;
			try
			{
				FileIOPermission fileIOPermission = new FileIOPermission(fileIOPermissionAccess, path);
				fileIOPermission.Demand();
			}
			catch (PathTooLongException e)
			{
				flag = true;
				ADP.TraceExceptionWithoutRethrow(e);
			}
			if (flag)
			{
				new FileIOPermission(PermissionState.Unrestricted)
				{
					AllFiles = fileIOPermissionAccess
				}.Demand();
			}
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x000A2988 File Offset: 0x000A1D88
		private void OpenSqlFileStream(string path, byte[] transactionContext, FileAccess access, FileOptions options, long allocationSize)
		{
			if (access != FileAccess.Read && access != FileAccess.Write && access != FileAccess.ReadWrite)
			{
				throw ADP.ArgumentOutOfRange("access");
			}
			if ((options & (FileOptions)671088639) != FileOptions.None)
			{
				throw ADP.ArgumentOutOfRange("options");
			}
			path = SqlFileStream.GetFullPathInternal(path);
			SqlFileStream.DemandAccessPermission(path, access);
			FileFullEaInformation fileFullEaInformation = null;
			SecurityQualityOfService securityQualityOfService = null;
			UnicodeString unicodeString = null;
			SafeFileHandle safeFileHandle = null;
			int num = 1048704;
			uint num2 = 0U;
			FileShare fileShare;
			uint num3;
			switch (access)
			{
			case FileAccess.Read:
				num |= 1;
				fileShare = (FileShare.Read | FileShare.Write | FileShare.Delete);
				num3 = 1U;
				goto IL_8C;
			case FileAccess.Write:
				num |= 2;
				fileShare = (FileShare.Read | FileShare.Delete);
				num3 = 4U;
				goto IL_8C;
			}
			num |= 3;
			fileShare = (FileShare.Read | FileShare.Delete);
			num3 = 4U;
			IL_8C:
			if ((options & FileOptions.WriteThrough) != FileOptions.None)
			{
				num2 |= 2U;
			}
			if ((options & FileOptions.Asynchronous) == FileOptions.None)
			{
				num2 |= 32U;
			}
			if ((options & FileOptions.SequentialScan) != FileOptions.None)
			{
				num2 |= 4U;
			}
			if ((options & FileOptions.RandomAccess) != FileOptions.None)
			{
				num2 |= 2048U;
			}
			try
			{
				fileFullEaInformation = new FileFullEaInformation(transactionContext);
				securityQualityOfService = new SecurityQualityOfService(UnsafeNativeMethods.SecurityImpersonationLevel.SecurityAnonymous, false, false);
				string path2 = SqlFileStream.InitializeNtPath(path);
				unicodeString = new UnicodeString(path2);
				UnsafeNativeMethods.OBJECT_ATTRIBUTES object_ATTRIBUTES;
				object_ATTRIBUTES.length = Marshal.SizeOf(typeof(UnsafeNativeMethods.OBJECT_ATTRIBUTES));
				object_ATTRIBUTES.rootDirectory = IntPtr.Zero;
				object_ATTRIBUTES.attributes = 64;
				object_ATTRIBUTES.securityDescriptor = IntPtr.Zero;
				object_ATTRIBUTES.securityQualityOfService = securityQualityOfService;
				object_ATTRIBUTES.objectName = unicodeString;
				uint num4 = 0U;
				uint mode;
				UnsafeNativeMethods.SetErrorModeWrapper(1U, out mode);
				try
				{
					Bid.Trace("<sc.SqlFileStream.OpenSqlFileStream|ADV> %d#, desiredAccess=0x%08x, allocationSize=%I64d, fileAttributes=0x%08x, shareAccess=0x%08x, dwCreateDisposition=0x%08x, createOptions=0x%08x\n", this.ObjectID, num, allocationSize, 0U, (int)fileShare, num3, num2);
					UnsafeNativeMethods.IO_STATUS_BLOCK io_STATUS_BLOCK;
					num4 = UnsafeNativeMethods.NtCreateFile(out safeFileHandle, num, ref object_ATTRIBUTES, out io_STATUS_BLOCK, ref allocationSize, 0U, fileShare, num3, num2, fileFullEaInformation, (uint)fileFullEaInformation.Length);
				}
				finally
				{
					UnsafeNativeMethods.SetErrorModeWrapper(mode, out mode);
				}
				if (num4 <= 3221225485U)
				{
					if (num4 != 0U)
					{
						if (num4 == 3221225485U)
						{
							throw ADP.Argument(Res.GetString("SqlFileStream_InvalidParameter"));
						}
					}
					else
					{
						if (safeFileHandle.IsInvalid)
						{
							Win32Exception ex = new Win32Exception(6);
							ADP.TraceExceptionAsReturnValue(ex);
							throw ex;
						}
						UnsafeNativeMethods.FileType fileType = UnsafeNativeMethods.GetFileType(safeFileHandle);
						if (fileType != UnsafeNativeMethods.FileType.Disk)
						{
							safeFileHandle.Dispose();
							throw ADP.Argument(Res.GetString("SqlFileStream_PathNotValidDiskResource"));
						}
						if (access == FileAccess.ReadWrite)
						{
							uint ioControlCode = UnsafeNativeMethods.CTL_CODE(9, 2392, 0, 0);
							uint num5 = 0U;
							if (!UnsafeNativeMethods.DeviceIoControl(safeFileHandle, ioControlCode, IntPtr.Zero, 0U, IntPtr.Zero, 0U, out num5, IntPtr.Zero))
							{
								Win32Exception ex2 = new Win32Exception(Marshal.GetLastWin32Error());
								ADP.TraceExceptionAsReturnValue(ex2);
								throw ex2;
							}
						}
						bool flag = false;
						try
						{
							SecurityPermission securityPermission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
							securityPermission.Assert();
							flag = true;
							this.m_fs = new FileStream(safeFileHandle, access, 1, (options & FileOptions.Asynchronous) > FileOptions.None);
						}
						finally
						{
							if (flag)
							{
								CodeAccessPermission.RevertAssert();
							}
						}
						return;
					}
				}
				else
				{
					if (num4 == 3221225524U)
					{
						DirectoryNotFoundException ex3 = new DirectoryNotFoundException();
						ADP.TraceExceptionAsReturnValue(ex3);
						throw ex3;
					}
					if (num4 == 3221225539U)
					{
						throw ADP.InvalidOperation(Res.GetString("SqlFileStream_FileAlreadyInTransaction"));
					}
				}
				uint num6 = UnsafeNativeMethods.RtlNtStatusToDosError(num4);
				if (num6 == 317U)
				{
					num6 = num4;
				}
				Win32Exception ex4 = new Win32Exception((int)num6);
				ADP.TraceExceptionAsReturnValue(ex4);
				throw ex4;
			}
			catch
			{
				if (safeFileHandle != null && !safeFileHandle.IsInvalid)
				{
					safeFileHandle.Dispose();
				}
				throw;
			}
			finally
			{
				if (fileFullEaInformation != null)
				{
					fileFullEaInformation.Dispose();
					fileFullEaInformation = null;
				}
				if (securityQualityOfService != null)
				{
					securityQualityOfService.Dispose();
					securityQualityOfService = null;
				}
				if (unicodeString != null)
				{
					unicodeString.Dispose();
					unicodeString = null;
				}
			}
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x000A2D00 File Offset: 0x000A2100
		private static string InitializeNtPath(string path)
		{
			string format = "\\??\\UNC\\{0}\\{1}";
			string text = Guid.NewGuid().ToString("N");
			return string.Format(CultureInfo.InvariantCulture, format, new object[]
			{
				path.Trim(new char[]
				{
					'\\'
				}),
				text
			});
		}

		// Token: 0x04000DD6 RID: 3542
		private static int _objectTypeCount;

		// Token: 0x04000DD7 RID: 3543
		internal readonly int ObjectID = Interlocked.Increment(ref SqlFileStream._objectTypeCount);

		// Token: 0x04000DD8 RID: 3544
		internal const int DefaultBufferSize = 1;

		// Token: 0x04000DD9 RID: 3545
		private const ushort IoControlCodeFunctionCode = 2392;

		// Token: 0x04000DDA RID: 3546
		private FileStream m_fs;

		// Token: 0x04000DDB RID: 3547
		private string m_path;

		// Token: 0x04000DDC RID: 3548
		private byte[] m_txn;

		// Token: 0x04000DDD RID: 3549
		private bool m_disposed;

		// Token: 0x04000DDE RID: 3550
		private static readonly char[] InvalidPathChars = Path.GetInvalidPathChars();

		// Token: 0x04000DDF RID: 3551
		private const int MaxWin32PathLength = 32766;
	}
}
