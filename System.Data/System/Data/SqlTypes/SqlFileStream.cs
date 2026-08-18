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
	// Token: 0x0200034B RID: 843
	public sealed class SqlFileStream : Stream
	{
		// Token: 0x06002D1D RID: 11549 RVA: 0x002CC648 File Offset: 0x002CBA48
		public SqlFileStream(string path, byte[] transactionContext, FileAccess access) : this(path, transactionContext, access, FileOptions.None, 0L)
		{
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x002CC668 File Offset: 0x002CBA68
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

		// Token: 0x06002D1F RID: 11551 RVA: 0x002CC718 File Offset: 0x002CBB18
		~SqlFileStream()
		{
			this.Dispose(false);
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x002CC758 File Offset: 0x002CBB58
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

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06002D21 RID: 11553 RVA: 0x002CC7D8 File Offset: 0x002CBBD8
		// (set) Token: 0x06002D22 RID: 11554 RVA: 0x002CC7F8 File Offset: 0x002CBBF8
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

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06002D23 RID: 11555 RVA: 0x002CC818 File Offset: 0x002CBC18
		// (set) Token: 0x06002D24 RID: 11556 RVA: 0x002CC848 File Offset: 0x002CBC48
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

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06002D25 RID: 11557 RVA: 0x002CC868 File Offset: 0x002CBC68
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

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06002D26 RID: 11558 RVA: 0x002CC898 File Offset: 0x002CBC98
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

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06002D27 RID: 11559 RVA: 0x002CC8C8 File Offset: 0x002CBCC8
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

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06002D28 RID: 11560 RVA: 0x002CC8F8 File Offset: 0x002CBCF8
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

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06002D29 RID: 11561 RVA: 0x002CC928 File Offset: 0x002CBD28
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

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x002CC958 File Offset: 0x002CBD58
		// (set) Token: 0x06002D2B RID: 11563 RVA: 0x002CC988 File Offset: 0x002CBD88
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

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06002D2C RID: 11564 RVA: 0x002CC9B8 File Offset: 0x002CBDB8
		// (set) Token: 0x06002D2D RID: 11565 RVA: 0x002CC9E8 File Offset: 0x002CBDE8
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

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002D2E RID: 11566 RVA: 0x002CCA18 File Offset: 0x002CBE18
		// (set) Token: 0x06002D2F RID: 11567 RVA: 0x002CCA48 File Offset: 0x002CBE48
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

		// Token: 0x06002D30 RID: 11568 RVA: 0x002CCA78 File Offset: 0x002CBE78
		public override void Flush()
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			this.m_fs.Flush();
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x002CCAA8 File Offset: 0x002CBEA8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			return this.m_fs.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x002CCAD8 File Offset: 0x002CBED8
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			return this.m_fs.EndRead(asyncResult);
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x002CCB08 File Offset: 0x002CBF08
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

		// Token: 0x06002D34 RID: 11572 RVA: 0x002CCB48 File Offset: 0x002CBF48
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			this.m_fs.EndWrite(asyncResult);
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x002CCB78 File Offset: 0x002CBF78
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			return this.m_fs.Seek(offset, origin);
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x002CCBA8 File Offset: 0x002CBFA8
		public override void SetLength(long value)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			this.m_fs.SetLength(value);
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x002CCBD8 File Offset: 0x002CBFD8
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			return this.m_fs.Read(buffer, offset, count);
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x002CCC08 File Offset: 0x002CC008
		public override int ReadByte()
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			return this.m_fs.ReadByte();
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x002CCC38 File Offset: 0x002CC038
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

		// Token: 0x06002D3A RID: 11578 RVA: 0x002CCC78 File Offset: 0x002CC078
		public override void WriteByte(byte value)
		{
			if (this.m_disposed)
			{
				throw ADP.ObjectDisposed(this);
			}
			this.m_fs.WriteByte(value);
			this.m_fs.Flush();
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x002CCCB8 File Offset: 0x002CC0B8
		[Conditional("DEBUG")]
		private static void AssertPathFormat(string path)
		{
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x002CCCC8 File Offset: 0x002CC0C8
		private static string GetFullPathInternal(string path)
		{
			path = path.Trim();
			if (path.Length == 0)
			{
				throw ADP.Argument(Res.GetString("SqlFileStream_InvalidPath"), "path");
			}
			if (path.Length > SqlFileStream.MaxWin32PathLength)
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

		// Token: 0x06002D3D RID: 11581 RVA: 0x002CCD98 File Offset: 0x002CC198
		private static void DemandAccessPermission(string path, FileAccess access)
		{
			FileIOPermissionAccess fileIOPermissionAccess;
			switch (access)
			{
			case FileAccess.Read:
				fileIOPermissionAccess = FileIOPermissionAccess.Read;
				goto IL_24;
			case FileAccess.Write:
				fileIOPermissionAccess = FileIOPermissionAccess.Write;
				goto IL_24;
			}
			fileIOPermissionAccess = (FileIOPermissionAccess.Read | FileIOPermissionAccess.Write);
			IL_24:
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

		// Token: 0x06002D3E RID: 11582 RVA: 0x002CCE28 File Offset: 0x002CC228
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
				goto IL_8F;
			case FileAccess.Write:
				num |= 2;
				fileShare = (FileShare.Read | FileShare.Delete);
				num3 = 4U;
				goto IL_8F;
			}
			num |= 3;
			fileShare = (FileShare.Read | FileShare.Delete);
			num3 = 4U;
			IL_8F:
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
					uint errorMode = UnsafeNativeMethods.SetErrorMode(1U);
					uint num4 = 0U;
					try
					{
						Bid.Trace("<sc.SqlFileStream.OpenSqlFileStream|ADV> %d#, desiredAccess=0x%08x, allocationSize=%I64d, fileAttributes=0x%08x, shareAccess=0x%08x, dwCreateDisposition=0x%08x, createOptions=0x%08x\n", this.ObjectID, num, allocationSize, 0U, (int)fileShare, num3, num2);
						UnsafeNativeMethods.IO_STATUS_BLOCK io_STATUS_BLOCK;
						num4 = UnsafeNativeMethods.NtCreateFile(out safeFileHandle, num, ref object_ATTRIBUTES, out io_STATUS_BLOCK, ref allocationSize, 0U, fileShare, num3, num2, fileFullEaInformation, (uint)fileFullEaInformation.Length);
					}
					finally
					{
						UnsafeNativeMethods.SetErrorMode(errorMode);
					}
					uint num5 = num4;
					if (num5 <= 3221225485U)
					{
						if (num5 != 0U)
						{
							if (num5 == 3221225485U)
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
								uint num6 = 0U;
								if (!UnsafeNativeMethods.DeviceIoControl(safeFileHandle, ioControlCode, IntPtr.Zero, 0U, IntPtr.Zero, 0U, out num6, IntPtr.Zero))
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
								this.m_fs = new FileStream(safeFileHandle, access, 1, (options & FileOptions.Asynchronous) != FileOptions.None);
							}
							finally
							{
								if (flag)
								{
									CodeAccessPermission.RevertAssert();
								}
							}
							goto IL_2EC;
						}
					}
					else
					{
						if (num5 == 3221225524U)
						{
							DirectoryNotFoundException ex3 = new DirectoryNotFoundException();
							ADP.TraceExceptionAsReturnValue(ex3);
							throw ex3;
						}
						if (num5 == 3221225539U)
						{
							throw ADP.InvalidOperation(Res.GetString("SqlFileStream_FileAlreadyInTransaction"));
						}
					}
					uint num7 = UnsafeNativeMethods.RtlNtStatusToDosError(num4);
					if (num7 == 317U)
					{
						num7 = num4;
					}
					Win32Exception ex4 = new Win32Exception((int)num7);
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
				IL_2EC:;
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

		// Token: 0x06002D3F RID: 11583 RVA: 0x002CD1B8 File Offset: 0x002CC5B8
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

		// Token: 0x04001CED RID: 7405
		internal const int DefaultBufferSize = 1;

		// Token: 0x04001CEE RID: 7406
		private const ushort IoControlCodeFunctionCode = 2392;

		// Token: 0x04001CEF RID: 7407
		private static int _objectTypeCount;

		// Token: 0x04001CF0 RID: 7408
		internal readonly int ObjectID = Interlocked.Increment(ref SqlFileStream._objectTypeCount);

		// Token: 0x04001CF1 RID: 7409
		private FileStream m_fs;

		// Token: 0x04001CF2 RID: 7410
		private string m_path;

		// Token: 0x04001CF3 RID: 7411
		private byte[] m_txn;

		// Token: 0x04001CF4 RID: 7412
		private bool m_disposed;

		// Token: 0x04001CF5 RID: 7413
		private static readonly char[] InvalidPathChars = Path.GetInvalidPathChars();

		// Token: 0x04001CF6 RID: 7414
		private static readonly int MaxWin32PathLength = 32766;
	}
}
