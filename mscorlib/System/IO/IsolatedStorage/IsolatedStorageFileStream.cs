using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO.IsolatedStorage
{
	// Token: 0x020007B0 RID: 1968
	[ComVisible(true)]
	public class IsolatedStorageFileStream : FileStream
	{
		// Token: 0x06004642 RID: 17986 RVA: 0x000EFF7C File Offset: 0x000EEF7C
		private IsolatedStorageFileStream()
		{
		}

		// Token: 0x06004643 RID: 17987 RVA: 0x000EFF84 File Offset: 0x000EEF84
		public IsolatedStorageFileStream(string path, FileMode mode) : this(path, mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, FileShare.None, null)
		{
		}

		// Token: 0x06004644 RID: 17988 RVA: 0x000EFF98 File Offset: 0x000EEF98
		public IsolatedStorageFileStream(string path, FileMode mode, IsolatedStorageFile isf) : this(path, mode, FileAccess.ReadWrite, FileShare.None, isf)
		{
		}

		// Token: 0x06004645 RID: 17989 RVA: 0x000EFFA5 File Offset: 0x000EEFA5
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access) : this(path, mode, access, (access == FileAccess.Read) ? FileShare.Read : FileShare.None, 4096, null)
		{
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x000EFFBE File Offset: 0x000EEFBE
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, IsolatedStorageFile isf) : this(path, mode, access, (access == FileAccess.Read) ? FileShare.Read : FileShare.None, 4096, isf)
		{
		}

		// Token: 0x06004647 RID: 17991 RVA: 0x000EFFD8 File Offset: 0x000EEFD8
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share) : this(path, mode, access, share, 4096, null)
		{
		}

		// Token: 0x06004648 RID: 17992 RVA: 0x000EFFEB File Offset: 0x000EEFEB
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share, IsolatedStorageFile isf) : this(path, mode, access, share, 4096, isf)
		{
		}

		// Token: 0x06004649 RID: 17993 RVA: 0x000EFFFF File Offset: 0x000EEFFF
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize) : this(path, mode, access, share, bufferSize, null)
		{
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x000F0010 File Offset: 0x000EF010
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, IsolatedStorageFile isf)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0 || path.Equals("\\"))
			{
				throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_Path"));
			}
			ulong num = 0UL;
			bool flag = false;
			bool flag2 = false;
			if (isf == null)
			{
				this.m_OwnedStore = true;
				isf = IsolatedStorageFile.GetUserStoreForDomain();
			}
			this.m_isf = isf;
			FileIOPermission fileIOPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, this.m_isf.RootDirectory);
			fileIOPermission.Assert();
			fileIOPermission.PermitOnly();
			this.m_GivenPath = path;
			this.m_FullPath = this.m_isf.GetFullPath(this.m_GivenPath);
			try
			{
				switch (mode)
				{
				case FileMode.CreateNew:
					flag = true;
					goto IL_105;
				case FileMode.Create:
				case FileMode.OpenOrCreate:
				case FileMode.Truncate:
				case FileMode.Append:
					this.m_isf.Lock();
					flag2 = true;
					try
					{
						FileInfo fileInfo = new FileInfo(this.m_FullPath);
						num = IsolatedStorageFile.RoundToBlockSize((ulong)fileInfo.Length);
						goto IL_105;
					}
					catch (FileNotFoundException)
					{
						flag = true;
						goto IL_105;
					}
					catch
					{
						goto IL_105;
					}
					break;
				case FileMode.Open:
					goto IL_105;
				}
				throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_FileOpenMode"));
				IL_105:
				if (flag)
				{
					this.m_isf.ReserveOneBlock();
				}
				try
				{
					this.m_fs = new FileStream(this.m_FullPath, mode, access, share, bufferSize, FileOptions.None, this.m_GivenPath, true);
				}
				catch
				{
					if (flag)
					{
						this.m_isf.UnreserveOneBlock();
					}
					throw;
				}
				if (!flag && (mode == FileMode.Truncate || mode == FileMode.Create))
				{
					ulong num2 = IsolatedStorageFile.RoundToBlockSize((ulong)this.m_fs.Length);
					if (num > num2)
					{
						this.m_isf.Unreserve(num - num2);
					}
					else if (num2 > num)
					{
						this.m_isf.Reserve(num2 - num);
					}
				}
			}
			finally
			{
				if (flag2)
				{
					this.m_isf.Unlock();
				}
			}
			CodeAccessPermission.RevertAll();
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x0600464B RID: 17995 RVA: 0x000F01F0 File Offset: 0x000EF1F0
		public override bool CanRead
		{
			get
			{
				return this.m_fs.CanRead;
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x0600464C RID: 17996 RVA: 0x000F01FD File Offset: 0x000EF1FD
		public override bool CanWrite
		{
			get
			{
				return this.m_fs.CanWrite;
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x0600464D RID: 17997 RVA: 0x000F020A File Offset: 0x000EF20A
		public override bool CanSeek
		{
			get
			{
				return this.m_fs.CanSeek;
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x0600464E RID: 17998 RVA: 0x000F0217 File Offset: 0x000EF217
		public override bool IsAsync
		{
			get
			{
				return this.m_fs.IsAsync;
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x0600464F RID: 17999 RVA: 0x000F0224 File Offset: 0x000EF224
		public override long Length
		{
			get
			{
				return this.m_fs.Length;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06004650 RID: 18000 RVA: 0x000F0231 File Offset: 0x000EF231
		// (set) Token: 0x06004651 RID: 18001 RVA: 0x000F023E File Offset: 0x000EF23E
		public override long Position
		{
			get
			{
				return this.m_fs.Position;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x06004652 RID: 18002 RVA: 0x000F0263 File Offset: 0x000EF263
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_fs != null)
				{
					this.m_fs.Close();
				}
				if (this.m_OwnedStore && this.m_isf != null)
				{
					this.m_isf.Close();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x000F029D File Offset: 0x000EF29D
		public override void Flush()
		{
			this.m_fs.Flush();
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06004654 RID: 18004 RVA: 0x000F02AA File Offset: 0x000EF2AA
		[Obsolete("This property has been deprecated.  Please use IsolatedStorageFileStream's SafeFileHandle property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public override IntPtr Handle
		{
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				this.NotPermittedError();
				return Win32Native.INVALID_HANDLE_VALUE;
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06004655 RID: 18005 RVA: 0x000F02B7 File Offset: 0x000EF2B7
		public override SafeFileHandle SafeFileHandle
		{
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				this.NotPermittedError();
				return null;
			}
		}

		// Token: 0x06004656 RID: 18006 RVA: 0x000F02C0 File Offset: 0x000EF2C0
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_isf.Lock();
			try
			{
				ulong length = (ulong)this.m_fs.Length;
				this.m_isf.Reserve(length, (ulong)value);
				try
				{
					this.ZeroInit(length, (ulong)value);
					this.m_fs.SetLength(value);
				}
				catch
				{
					this.m_isf.UndoReserveOperation(length, (ulong)value);
					throw;
				}
				if (length > (ulong)value)
				{
					this.m_isf.UndoReserveOperation((ulong)value, length);
				}
			}
			finally
			{
				this.m_isf.Unlock();
			}
		}

		// Token: 0x06004657 RID: 18007 RVA: 0x000F0370 File Offset: 0x000EF370
		private void ZeroInit(ulong oldLen, ulong newLen)
		{
			if (oldLen >= newLen)
			{
				return;
			}
			ulong num = newLen - oldLen;
			byte[] buffer = new byte[1024];
			long position = this.m_fs.Position;
			this.m_fs.Seek((long)oldLen, SeekOrigin.Begin);
			if (num <= 1024UL)
			{
				this.m_fs.Write(buffer, 0, (int)num);
				this.m_fs.Position = position;
				return;
			}
			int num2 = 1024 - (int)(oldLen & 1023UL);
			this.m_fs.Write(buffer, 0, num2);
			num -= (ulong)((long)num2);
			int num3 = (int)(num / 1024UL);
			for (int i = 0; i < num3; i++)
			{
				this.m_fs.Write(buffer, 0, 1024);
			}
			this.m_fs.Write(buffer, 0, (int)(num & 1023UL));
			this.m_fs.Position = position;
		}

		// Token: 0x06004658 RID: 18008 RVA: 0x000F0443 File Offset: 0x000EF443
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.m_fs.Read(buffer, offset, count);
		}

		// Token: 0x06004659 RID: 18009 RVA: 0x000F0453 File Offset: 0x000EF453
		public override int ReadByte()
		{
			return this.m_fs.ReadByte();
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x000F0460 File Offset: 0x000EF460
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.m_isf.Lock();
			long result;
			try
			{
				ulong length = (ulong)this.m_fs.Length;
				ulong newLen;
				switch (origin)
				{
				case SeekOrigin.Begin:
					newLen = (ulong)((offset < 0L) ? 0L : offset);
					break;
				case SeekOrigin.Current:
					newLen = (ulong)((this.m_fs.Position + offset < 0L) ? 0L : (this.m_fs.Position + offset));
					break;
				case SeekOrigin.End:
					newLen = (ulong)((this.m_fs.Length + offset < 0L) ? 0L : (this.m_fs.Length + offset));
					break;
				default:
					throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_SeekOrigin"));
				}
				this.m_isf.Reserve(length, newLen);
				try
				{
					this.ZeroInit(length, newLen);
					result = this.m_fs.Seek(offset, origin);
				}
				catch
				{
					this.m_isf.UndoReserveOperation(length, newLen);
					throw;
				}
			}
			finally
			{
				this.m_isf.Unlock();
			}
			return result;
		}

		// Token: 0x0600465B RID: 18011 RVA: 0x000F0564 File Offset: 0x000EF564
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.m_isf.Lock();
			try
			{
				ulong length = (ulong)this.m_fs.Length;
				ulong newLen = (ulong)(this.m_fs.Position + (long)count);
				this.m_isf.Reserve(length, newLen);
				try
				{
					this.m_fs.Write(buffer, offset, count);
				}
				catch
				{
					this.m_isf.UndoReserveOperation(length, newLen);
					throw;
				}
			}
			finally
			{
				this.m_isf.Unlock();
			}
		}

		// Token: 0x0600465C RID: 18012 RVA: 0x000F05F0 File Offset: 0x000EF5F0
		public override void WriteByte(byte value)
		{
			this.m_isf.Lock();
			try
			{
				ulong length = (ulong)this.m_fs.Length;
				ulong newLen = (ulong)(this.m_fs.Position + 1L);
				this.m_isf.Reserve(length, newLen);
				try
				{
					this.m_fs.WriteByte(value);
				}
				catch
				{
					this.m_isf.UndoReserveOperation(length, newLen);
					throw;
				}
			}
			finally
			{
				this.m_isf.Unlock();
			}
		}

		// Token: 0x0600465D RID: 18013 RVA: 0x000F0678 File Offset: 0x000EF678
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			return this.m_fs.BeginRead(buffer, offset, numBytes, userCallback, stateObject);
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x000F068C File Offset: 0x000EF68C
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.m_fs.EndRead(asyncResult);
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x000F069C File Offset: 0x000EF69C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			this.m_isf.Lock();
			IAsyncResult result;
			try
			{
				ulong length = (ulong)this.m_fs.Length;
				ulong newLen = (ulong)(this.m_fs.Position + (long)numBytes);
				this.m_isf.Reserve(length, newLen);
				try
				{
					result = this.m_fs.BeginWrite(buffer, offset, numBytes, userCallback, stateObject);
				}
				catch
				{
					this.m_isf.UndoReserveOperation(length, newLen);
					throw;
				}
			}
			finally
			{
				this.m_isf.Unlock();
			}
			return result;
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x000F072C File Offset: 0x000EF72C
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.m_fs.EndWrite(asyncResult);
		}

		// Token: 0x06004661 RID: 18017 RVA: 0x000F073A File Offset: 0x000EF73A
		internal void NotPermittedError(string str)
		{
			throw new IsolatedStorageException(str);
		}

		// Token: 0x06004662 RID: 18018 RVA: 0x000F0742 File Offset: 0x000EF742
		internal void NotPermittedError()
		{
			this.NotPermittedError(Environment.GetResourceString("IsolatedStorage_Operation"));
		}

		// Token: 0x040022FD RID: 8957
		private const int s_BlockSize = 1024;

		// Token: 0x040022FE RID: 8958
		private const string s_BackSlash = "\\";

		// Token: 0x040022FF RID: 8959
		private FileStream m_fs;

		// Token: 0x04002300 RID: 8960
		private IsolatedStorageFile m_isf;

		// Token: 0x04002301 RID: 8961
		private string m_GivenPath;

		// Token: 0x04002302 RID: 8962
		private string m_FullPath;

		// Token: 0x04002303 RID: 8963
		private bool m_OwnedStore;
	}
}
