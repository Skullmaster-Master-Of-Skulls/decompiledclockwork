using System;
using System.Runtime.InteropServices;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020002A3 RID: 675
	internal class MemoryBytes
	{
		// Token: 0x06002314 RID: 8980 RVA: 0x00097010 File Offset: 0x00096010
		internal MemoryBytes(string fileName, long offset, long fileSize)
		{
			this._bufferType = BufferType.TransmitFile;
			this._intptrData = IntPtr.Zero;
			this._fileHandle = IntPtr.Zero;
			this._fileSize = fileSize;
			this._fileName = fileName;
			this._offset = offset;
			this._size = IntPtr.Size;
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x00097060 File Offset: 0x00096060
		internal MemoryBytes(byte[] data, int size) : this(data, size, false, 0L)
		{
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x00097070 File Offset: 0x00096070
		internal MemoryBytes(byte[] data, int size, bool useTransmitFile, long fileSize)
		{
			if (AppSettings.CheckMemoryBytes && (size < 0 || size > data.Length))
			{
				throw new ArgumentOutOfRangeException("size");
			}
			this._size = size;
			this._arrayData = data;
			this._intptrData = IntPtr.Zero;
			this._fileHandle = IntPtr.Zero;
			if (useTransmitFile)
			{
				this._bufferType = BufferType.TransmitFile;
			}
			this._fileSize = fileSize;
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x000970D5 File Offset: 0x000960D5
		internal MemoryBytes(IntPtr data, int size, BufferType bufferType)
		{
			this._size = size;
			this._arrayData = null;
			this._intptrData = data;
			this._fileHandle = IntPtr.Zero;
			this._bufferType = bufferType;
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06002318 RID: 8984 RVA: 0x00097104 File Offset: 0x00096104
		internal long FileSize
		{
			get
			{
				return this._fileSize;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06002319 RID: 8985 RVA: 0x0009710C File Offset: 0x0009610C
		internal bool IsBufferFromUnmanagedPool
		{
			get
			{
				return this._bufferType == BufferType.UnmanagedPool;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x0600231A RID: 8986 RVA: 0x00097117 File Offset: 0x00096117
		internal BufferType BufferType
		{
			get
			{
				return this._bufferType;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x0600231B RID: 8987 RVA: 0x0009711F File Offset: 0x0009611F
		internal int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x0600231C RID: 8988 RVA: 0x00097127 File Offset: 0x00096127
		internal bool UseTransmitFile
		{
			get
			{
				return this._bufferType == BufferType.TransmitFile;
			}
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x00097132 File Offset: 0x00096132
		private void CloseHandle()
		{
			if (this._fileHandle != IntPtr.Zero && this._fileHandle != UnsafeNativeMethods.INVALID_HANDLE_VALUE)
			{
				UnsafeNativeMethods.CloseHandle(this._fileHandle);
				this._fileHandle = IntPtr.Zero;
			}
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x00097170 File Offset: 0x00096170
		private static byte[] IntPtrToBytes(IntPtr p, long offset, long length)
		{
			byte[] array = new byte[16 + IntPtr.Size];
			for (int i = 0; i < 8; i++)
			{
				array[i] = (byte)(offset >> 8 * i & 255L);
			}
			for (int j = 0; j < 8; j++)
			{
				array[8 + j] = (byte)(length >> 8 * j & 255L);
			}
			if (IntPtr.Size == 4)
			{
				int num = p.ToInt32();
				for (int k = 0; k < 4; k++)
				{
					array[16 + k] = (byte)(num >> 8 * k & 255);
				}
			}
			else
			{
				long num2 = p.ToInt64();
				for (int l = 0; l < 8; l++)
				{
					array[16 + l] = (byte)(num2 >> 8 * l & 255L);
				}
			}
			return array;
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x00097238 File Offset: 0x00096238
		private void SetHandle()
		{
			if (this._fileName != null)
			{
				this._fileHandle = UnsafeNativeMethods.GetFileHandleForTransmitFile(this._fileName);
			}
			if (this._fileHandle != IntPtr.Zero)
			{
				this._arrayData = MemoryBytes.IntPtrToBytes(this._fileHandle, this._offset, this._fileSize);
			}
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x0009728D File Offset: 0x0009628D
		internal IntPtr LockMemory()
		{
			this.SetHandle();
			if (this._arrayData != null)
			{
				this._pinnedArrayData = GCHandle.Alloc(this._arrayData, GCHandleType.Pinned);
				return Marshal.UnsafeAddrOfPinnedArrayElement(this._arrayData, 0);
			}
			return this._intptrData;
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x000972C2 File Offset: 0x000962C2
		internal void UnlockMemory()
		{
			this.CloseHandle();
			if (this._arrayData != null)
			{
				this._pinnedArrayData.Free();
			}
		}

		// Token: 0x04001B99 RID: 7065
		private int _size;

		// Token: 0x04001B9A RID: 7066
		private byte[] _arrayData;

		// Token: 0x04001B9B RID: 7067
		private GCHandle _pinnedArrayData;

		// Token: 0x04001B9C RID: 7068
		private IntPtr _intptrData;

		// Token: 0x04001B9D RID: 7069
		private long _fileSize;

		// Token: 0x04001B9E RID: 7070
		private IntPtr _fileHandle;

		// Token: 0x04001B9F RID: 7071
		private string _fileName;

		// Token: 0x04001BA0 RID: 7072
		private long _offset;

		// Token: 0x04001BA1 RID: 7073
		private BufferType _bufferType;
	}
}
