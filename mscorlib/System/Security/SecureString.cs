using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Microsoft.Win32;

namespace System.Security
{
	// Token: 0x02000689 RID: 1673
	public sealed class SecureString : IDisposable
	{
		// Token: 0x06003C62 RID: 15458 RVA: 0x000CE530 File Offset: 0x000CD530
		private static bool EncryptionSupported()
		{
			bool result = true;
			try
			{
				Win32Native.SystemFunction041(SafeBSTRHandle.Allocate(null, 16U), 16U, 0U);
			}
			catch (EntryPointNotFoundException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06003C63 RID: 15459 RVA: 0x000CE568 File Offset: 0x000CD568
		internal SecureString(SecureString str)
		{
			this.AllocateBuffer(str.BufferLength);
			SafeBSTRHandle.Copy(str.m_buffer, this.m_buffer);
			this.m_length = str.m_length;
			this.m_enrypted = str.m_enrypted;
		}

		// Token: 0x06003C64 RID: 15460 RVA: 0x000CE5A5 File Offset: 0x000CD5A5
		public SecureString()
		{
			this.CheckSupportedOnCurrentPlatform();
			this.AllocateBuffer(8);
			this.m_length = 0;
		}

		// Token: 0x06003C65 RID: 15461 RVA: 0x000CE5C4 File Offset: 0x000CD5C4
		[CLSCompliant(false)]
		public unsafe SecureString(char* value, int length)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (length > 65536)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_Length"));
			}
			this.CheckSupportedOnCurrentPlatform();
			this.AllocateBuffer(length);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.m_buffer.AcquirePointer(ref ptr);
				Buffer.memcpyimpl((byte*)value, ptr, length * 2);
			}
			finally
			{
				if (ptr != null)
				{
					this.m_buffer.ReleasePointer();
				}
			}
			this.m_length = length;
			this.ProtectMemory();
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06003C66 RID: 15462 RVA: 0x000CE67C File Offset: 0x000CD67C
		public int Length
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			get
			{
				this.EnsureNotDisposed();
				return this.m_length;
			}
		}

		// Token: 0x06003C67 RID: 15463 RVA: 0x000CE68C File Offset: 0x000CD68C
		[MethodImpl(MethodImplOptions.Synchronized)]
		public void AppendChar(char c)
		{
			this.EnsureNotDisposed();
			this.EnsureNotReadOnly();
			this.EnsureCapacity(this.m_length + 1);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.UnProtectMemory();
				this.m_buffer.Write<char>((uint)(this.m_length * 2), c);
				this.m_length++;
			}
			finally
			{
				this.ProtectMemory();
			}
		}

		// Token: 0x06003C68 RID: 15464 RVA: 0x000CE6FC File Offset: 0x000CD6FC
		[MethodImpl(MethodImplOptions.Synchronized)]
		public void Clear()
		{
			this.EnsureNotDisposed();
			this.EnsureNotReadOnly();
			this.m_length = 0;
			this.m_buffer.ClearBuffer();
			this.m_enrypted = false;
		}

		// Token: 0x06003C69 RID: 15465 RVA: 0x000CE723 File Offset: 0x000CD723
		[MethodImpl(MethodImplOptions.Synchronized)]
		public SecureString Copy()
		{
			this.EnsureNotDisposed();
			return new SecureString(this);
		}

		// Token: 0x06003C6A RID: 15466 RVA: 0x000CE731 File Offset: 0x000CD731
		[MethodImpl(MethodImplOptions.Synchronized)]
		public void Dispose()
		{
			if (this.m_buffer != null && !this.m_buffer.IsInvalid)
			{
				this.m_buffer.Close();
				this.m_buffer = null;
			}
		}

		// Token: 0x06003C6B RID: 15467 RVA: 0x000CE75C File Offset: 0x000CD75C
		[MethodImpl(MethodImplOptions.Synchronized)]
		public unsafe void InsertAt(int index, char c)
		{
			this.EnsureNotDisposed();
			this.EnsureNotReadOnly();
			if (index < 0 || index > this.m_length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_IndexString"));
			}
			this.EnsureCapacity(this.m_length + 1);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.UnProtectMemory();
				this.m_buffer.AcquirePointer(ref ptr);
				char* ptr2 = (char*)ptr;
				for (int i = this.m_length; i > index; i--)
				{
					ptr2[i] = ptr2[i - 1];
				}
				ptr2[index] = c;
				this.m_length++;
			}
			finally
			{
				this.ProtectMemory();
				if (ptr != null)
				{
					this.m_buffer.ReleasePointer();
				}
			}
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x000CE824 File Offset: 0x000CD824
		[MethodImpl(MethodImplOptions.Synchronized)]
		public bool IsReadOnly()
		{
			this.EnsureNotDisposed();
			return this.m_readOnly;
		}

		// Token: 0x06003C6D RID: 15469 RVA: 0x000CE832 File Offset: 0x000CD832
		[MethodImpl(MethodImplOptions.Synchronized)]
		public void MakeReadOnly()
		{
			this.EnsureNotDisposed();
			this.m_readOnly = true;
		}

		// Token: 0x06003C6E RID: 15470 RVA: 0x000CE844 File Offset: 0x000CD844
		[MethodImpl(MethodImplOptions.Synchronized)]
		public unsafe void RemoveAt(int index)
		{
			this.EnsureNotDisposed();
			this.EnsureNotReadOnly();
			if (index < 0 || index >= this.m_length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_IndexString"));
			}
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.UnProtectMemory();
				this.m_buffer.AcquirePointer(ref ptr);
				char* ptr2 = (char*)ptr;
				for (int i = index; i < this.m_length - 1; i++)
				{
					ptr2[i] = ptr2[i + 1];
				}
				ptr2[(IntPtr)(--this.m_length) * 2] = '\0';
			}
			finally
			{
				this.ProtectMemory();
				if (ptr != null)
				{
					this.m_buffer.ReleasePointer();
				}
			}
		}

		// Token: 0x06003C6F RID: 15471 RVA: 0x000CE900 File Offset: 0x000CD900
		[MethodImpl(MethodImplOptions.Synchronized)]
		public void SetAt(int index, char c)
		{
			this.EnsureNotDisposed();
			this.EnsureNotReadOnly();
			if (index < 0 || index >= this.m_length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_IndexString"));
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.UnProtectMemory();
				this.m_buffer.Write<char>((uint)(index * 2), c);
			}
			finally
			{
				this.ProtectMemory();
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06003C70 RID: 15472 RVA: 0x000CE970 File Offset: 0x000CD970
		private int BufferLength
		{
			get
			{
				return this.m_buffer.Length;
			}
		}

		// Token: 0x06003C71 RID: 15473 RVA: 0x000CE980 File Offset: 0x000CD980
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private void AllocateBuffer(int size)
		{
			uint alignedSize = SecureString.GetAlignedSize(size);
			this.m_buffer = SafeBSTRHandle.Allocate(null, alignedSize);
			if (this.m_buffer.IsInvalid)
			{
				throw new OutOfMemoryException();
			}
		}

		// Token: 0x06003C72 RID: 15474 RVA: 0x000CE9B4 File Offset: 0x000CD9B4
		private void CheckSupportedOnCurrentPlatform()
		{
			if (!SecureString.supportedOnCurrentPlatform)
			{
				throw new NotSupportedException(Environment.GetResourceString("Arg_PlatformSecureString"));
			}
		}

		// Token: 0x06003C73 RID: 15475 RVA: 0x000CE9D0 File Offset: 0x000CD9D0
		private void EnsureCapacity(int capacity)
		{
			if (capacity <= this.m_buffer.Length)
			{
				return;
			}
			if (capacity > 65536)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_Capacity"));
			}
			SafeBSTRHandle safeBSTRHandle = SafeBSTRHandle.Allocate(null, SecureString.GetAlignedSize(capacity));
			if (safeBSTRHandle.IsInvalid)
			{
				throw new OutOfMemoryException();
			}
			SafeBSTRHandle.Copy(this.m_buffer, safeBSTRHandle);
			this.m_buffer.Close();
			this.m_buffer = safeBSTRHandle;
		}

		// Token: 0x06003C74 RID: 15476 RVA: 0x000CEA42 File Offset: 0x000CDA42
		private void EnsureNotDisposed()
		{
			if (this.m_buffer == null)
			{
				throw new ObjectDisposedException(null);
			}
		}

		// Token: 0x06003C75 RID: 15477 RVA: 0x000CEA53 File Offset: 0x000CDA53
		private void EnsureNotReadOnly()
		{
			if (this.m_readOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
			}
		}

		// Token: 0x06003C76 RID: 15478 RVA: 0x000CEA70 File Offset: 0x000CDA70
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static uint GetAlignedSize(int size)
		{
			uint num = (uint)(size / 8 * 8);
			if (size % 8 != 0 || size == 0)
			{
				num += 8U;
			}
			return num;
		}

		// Token: 0x06003C77 RID: 15479 RVA: 0x000CEA90 File Offset: 0x000CDA90
		private unsafe int GetAnsiByteCount()
		{
			uint flags = 1024U;
			uint num = 63U;
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			int result;
			try
			{
				this.m_buffer.AcquirePointer(ref ptr);
				result = Win32Native.WideCharToMultiByte(0U, flags, (char*)ptr, this.m_length, null, 0, IntPtr.Zero, new IntPtr((void*)(&num)));
			}
			finally
			{
				if (ptr != null)
				{
					this.m_buffer.ReleasePointer();
				}
			}
			return result;
		}

		// Token: 0x06003C78 RID: 15480 RVA: 0x000CEB00 File Offset: 0x000CDB00
		private unsafe void GetAnsiBytes(byte* ansiStrPtr, int byteCount)
		{
			uint flags = 1024U;
			uint num = 63U;
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.m_buffer.AcquirePointer(ref ptr);
				Win32Native.WideCharToMultiByte(0U, flags, (char*)ptr, this.m_length, ansiStrPtr, byteCount - 1, IntPtr.Zero, new IntPtr((void*)(&num)));
				*(ansiStrPtr + byteCount - 1) = 0;
			}
			finally
			{
				if (ptr != null)
				{
					this.m_buffer.ReleasePointer();
				}
			}
		}

		// Token: 0x06003C79 RID: 15481 RVA: 0x000CEB78 File Offset: 0x000CDB78
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		private void ProtectMemory()
		{
			if (this.m_length == 0 || this.m_enrypted)
			{
				return;
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				int num = Win32Native.SystemFunction040(this.m_buffer, (uint)(this.m_buffer.Length * 2), 0U);
				if (num < 0)
				{
					throw new CryptographicException(Win32Native.LsaNtStatusToWinError(num));
				}
				this.m_enrypted = true;
			}
		}

		// Token: 0x06003C7A RID: 15482 RVA: 0x000CEBE0 File Offset: 0x000CDBE0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.Synchronized)]
		internal unsafe IntPtr ToBSTR()
		{
			this.EnsureNotDisposed();
			int length = this.m_length;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					intPtr = Win32Native.SysAllocStringLen(null, length);
				}
				if (intPtr == IntPtr.Zero)
				{
					throw new OutOfMemoryException();
				}
				this.UnProtectMemory();
				this.m_buffer.AcquirePointer(ref ptr);
				Buffer.memcpyimpl(ptr, (byte*)intPtr.ToPointer(), length * 2);
				intPtr2 = intPtr;
			}
			finally
			{
				this.ProtectMemory();
				if (intPtr2 == IntPtr.Zero && intPtr != IntPtr.Zero)
				{
					Win32Native.ZeroMemory(intPtr, (uint)(length * 2));
					Win32Native.SysFreeString(intPtr);
				}
				if (ptr != null)
				{
					this.m_buffer.ReleasePointer();
				}
			}
			return intPtr2;
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x000CECB8 File Offset: 0x000CDCB8
		[MethodImpl(MethodImplOptions.Synchronized)]
		internal unsafe IntPtr ToUniStr(bool allocateFromHeap)
		{
			this.EnsureNotDisposed();
			int length = this.m_length;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					if (allocateFromHeap)
					{
						intPtr = Marshal.AllocHGlobal((length + 1) * 2);
					}
					else
					{
						intPtr = Marshal.AllocCoTaskMem((length + 1) * 2);
					}
				}
				if (intPtr == IntPtr.Zero)
				{
					throw new OutOfMemoryException();
				}
				this.UnProtectMemory();
				this.m_buffer.AcquirePointer(ref ptr);
				Buffer.memcpyimpl(ptr, (byte*)intPtr.ToPointer(), length * 2);
				char* ptr2 = (char*)intPtr.ToPointer();
				ptr2[length] = '\0';
				intPtr2 = intPtr;
			}
			finally
			{
				this.ProtectMemory();
				if (intPtr2 == IntPtr.Zero && intPtr != IntPtr.Zero)
				{
					Win32Native.ZeroMemory(intPtr, (uint)(length * 2));
					if (allocateFromHeap)
					{
						Marshal.FreeHGlobal(intPtr);
					}
					else
					{
						Marshal.FreeCoTaskMem(intPtr);
					}
				}
				if (ptr != null)
				{
					this.m_buffer.ReleasePointer();
				}
			}
			return intPtr2;
		}

		// Token: 0x06003C7C RID: 15484 RVA: 0x000CEDC0 File Offset: 0x000CDDC0
		[MethodImpl(MethodImplOptions.Synchronized)]
		internal unsafe IntPtr ToAnsiStr(bool allocateFromHeap)
		{
			this.EnsureNotDisposed();
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			int num = 0;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.UnProtectMemory();
				num = this.GetAnsiByteCount() + 1;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					if (allocateFromHeap)
					{
						intPtr = Marshal.AllocHGlobal(num);
					}
					else
					{
						intPtr = Marshal.AllocCoTaskMem(num);
					}
				}
				if (intPtr == IntPtr.Zero)
				{
					throw new OutOfMemoryException();
				}
				this.GetAnsiBytes((byte*)intPtr.ToPointer(), num);
				intPtr2 = intPtr;
			}
			finally
			{
				this.ProtectMemory();
				if (intPtr2 == IntPtr.Zero && intPtr != IntPtr.Zero)
				{
					Win32Native.ZeroMemory(intPtr, (uint)num);
					if (allocateFromHeap)
					{
						Marshal.FreeHGlobal(intPtr);
					}
					else
					{
						Marshal.FreeCoTaskMem(intPtr);
					}
				}
			}
			return intPtr2;
		}

		// Token: 0x06003C7D RID: 15485 RVA: 0x000CEE8C File Offset: 0x000CDE8C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private void UnProtectMemory()
		{
			if (this.m_length == 0)
			{
				return;
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				if (this.m_enrypted)
				{
					int num = Win32Native.SystemFunction041(this.m_buffer, (uint)(this.m_buffer.Length * 2), 0U);
					if (num < 0)
					{
						throw new CryptographicException(Win32Native.LsaNtStatusToWinError(num));
					}
					this.m_enrypted = false;
				}
			}
		}

		// Token: 0x04001F06 RID: 7942
		private const int BlockSize = 8;

		// Token: 0x04001F07 RID: 7943
		private const int MaxLength = 65536;

		// Token: 0x04001F08 RID: 7944
		private const uint ProtectionScope = 0U;

		// Token: 0x04001F09 RID: 7945
		private SafeBSTRHandle m_buffer;

		// Token: 0x04001F0A RID: 7946
		private int m_length;

		// Token: 0x04001F0B RID: 7947
		private bool m_readOnly;

		// Token: 0x04001F0C RID: 7948
		private bool m_enrypted;

		// Token: 0x04001F0D RID: 7949
		private static bool supportedOnCurrentPlatform = SecureString.EncryptionSupported();
	}
}
