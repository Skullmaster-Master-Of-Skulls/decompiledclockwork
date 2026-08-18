using System;
using System.Runtime.InteropServices;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020007C6 RID: 1990
	internal class MemoryBytes
	{
		// Token: 0x06005F3E RID: 24382 RVA: 0x00148CEC File Offset: 0x00146EEC
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

		// Token: 0x06005F3F RID: 24383 RVA: 0x00148D3C File Offset: 0x00146F3C
		internal MemoryBytes(byte[] data, int size) : this(data, size, false, 0L)
		{
		}

		// Token: 0x06005F40 RID: 24384 RVA: 0x00148D4C File Offset: 0x00146F4C
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

		// Token: 0x06005F41 RID: 24385 RVA: 0x00148DB1 File Offset: 0x00146FB1
		internal MemoryBytes(IntPtr data, int size, BufferType bufferType)
		{
			this._size = size;
			this._arrayData = null;
			this._intptrData = data;
			this._fileHandle = IntPtr.Zero;
			this._bufferType = bufferType;
		}

		// Token: 0x17001B69 RID: 7017
		// (get) Token: 0x06005F42 RID: 24386 RVA: 0x00148DE0 File Offset: 0x00146FE0
		internal long FileSize
		{
			get
			{
				return this._fileSize;
			}
		}

		// Token: 0x17001B6A RID: 7018
		// (get) Token: 0x06005F43 RID: 24387 RVA: 0x00148DE8 File Offset: 0x00146FE8
		internal bool IsBufferFromUnmanagedPool
		{
			get
			{
				return this._bufferType == BufferType.UnmanagedPool;
			}
		}

		// Token: 0x17001B6B RID: 7019
		// (get) Token: 0x06005F44 RID: 24388 RVA: 0x00148DF3 File Offset: 0x00146FF3
		internal BufferType BufferType
		{
			get
			{
				return this._bufferType;
			}
		}

		// Token: 0x17001B6C RID: 7020
		// (get) Token: 0x06005F45 RID: 24389 RVA: 0x00148DFB File Offset: 0x00146FFB
		internal int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17001B6D RID: 7021
		// (get) Token: 0x06005F46 RID: 24390 RVA: 0x00148E03 File Offset: 0x00147003
		internal bool UseTransmitFile
		{
			get
			{
				return this._bufferType == BufferType.TransmitFile;
			}
		}

		// Token: 0x06005F47 RID: 24391 RVA: 0x00148E0E File Offset: 0x0014700E
		private void CloseHandle()
		{
			if (this._fileHandle != IntPtr.Zero && this._fileHandle != UnsafeNativeMethods.INVALID_HANDLE_VALUE)
			{
				UnsafeNativeMethods.CloseHandle(this._fileHandle);
				this._fileHandle = IntPtr.Zero;
			}
		}

		// Token: 0x06005F48 RID: 24392 RVA: 0x00148E4C File Offset: 0x0014704C
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

		// Token: 0x06005F49 RID: 24393 RVA: 0x00148F14 File Offset: 0x00147114
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

		// Token: 0x06005F4A RID: 24394 RVA: 0x00148F69 File Offset: 0x00147169
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

		// Token: 0x06005F4B RID: 24395 RVA: 0x00148F9E File Offset: 0x0014719E
		internal void UnlockMemory()
		{
			this.CloseHandle();
			if (this._arrayData != null)
			{
				this._pinnedArrayData.Free();
			}
		}

		// Token: 0x040031B1 RID: 12721
		private int _size;

		// Token: 0x040031B2 RID: 12722
		private byte[] _arrayData;

		// Token: 0x040031B3 RID: 12723
		private GCHandle _pinnedArrayData;

		// Token: 0x040031B4 RID: 12724
		private IntPtr _intptrData;

		// Token: 0x040031B5 RID: 12725
		private long _fileSize;

		// Token: 0x040031B6 RID: 12726
		private IntPtr _fileHandle;

		// Token: 0x040031B7 RID: 12727
		private string _fileName;

		// Token: 0x040031B8 RID: 12728
		private long _offset;

		// Token: 0x040031B9 RID: 12729
		private BufferType _bufferType;
	}
}
