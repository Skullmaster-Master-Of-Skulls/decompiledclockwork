using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO.MemoryMappedFiles
{
	// Token: 0x020000AA RID: 170
	internal class MemoryMappedView : IDisposable
	{
		// Token: 0x060004A9 RID: 1193 RVA: 0x0000DDF0 File Offset: 0x0000BFF0
		[SecurityCritical]
		private MemoryMappedView(SafeMemoryMappedViewHandle viewHandle, long pointerOffset, long size, MemoryMappedFileAccess access)
		{
			this.m_viewHandle = viewHandle;
			this.m_pointerOffset = pointerOffset;
			this.m_size = size;
			this.m_access = access;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0000DE15 File Offset: 0x0000C015
		internal SafeMemoryMappedViewHandle ViewHandle
		{
			[SecurityCritical]
			get
			{
				return this.m_viewHandle;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000DE1D File Offset: 0x0000C01D
		internal long PointerOffset
		{
			get
			{
				return this.m_pointerOffset;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000DE25 File Offset: 0x0000C025
		internal long Size
		{
			get
			{
				return this.m_size;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000DE2D File Offset: 0x0000C02D
		internal MemoryMappedFileAccess Access
		{
			get
			{
				return this.m_access;
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000DE38 File Offset: 0x0000C038
		[SecurityCritical]
		internal static MemoryMappedView CreateView(SafeMemoryMappedFileHandle memMappedFileHandle, MemoryMappedFileAccess access, long offset, long size)
		{
			ulong num = (ulong)(offset % (long)MemoryMappedFile.GetSystemPageAllocationGranularity());
			ulong num2 = (ulong)(offset - (long)num);
			ulong num3;
			if (size != 0L)
			{
				num3 = (ulong)(size + (long)num);
			}
			else
			{
				num3 = 0UL;
			}
			if (IntPtr.Size == 4 && num3 > (ulong)-1)
			{
				throw new ArgumentOutOfRangeException("size", SR.GetString("ArgumentOutOfRange_CapacityLargerThanLogicalAddressSpaceNotAllowed"));
			}
			UnsafeNativeMethods.MEMORYSTATUSEX memorystatusex = default(UnsafeNativeMethods.MEMORYSTATUSEX);
			bool flag = UnsafeNativeMethods.GlobalMemoryStatusEx(ref memorystatusex);
			ulong ullTotalVirtual = memorystatusex.ullTotalVirtual;
			if (num3 >= ullTotalVirtual)
			{
				throw new IOException(SR.GetString("IO_NotEnoughMemory"));
			}
			uint dwFileOffsetLow = (uint)(num2 & (ulong)-1);
			uint dwFileOffsetHigh = (uint)(num2 >> 32);
			SafeMemoryMappedViewHandle safeMemoryMappedViewHandle = UnsafeNativeMethods.MapViewOfFile(memMappedFileHandle, MemoryMappedFile.GetFileMapAccess(access), dwFileOffsetHigh, dwFileOffsetLow, new UIntPtr(num3));
			if (safeMemoryMappedViewHandle.IsInvalid)
			{
				__Error.WinIOError(Marshal.GetLastWin32Error(), string.Empty);
			}
			UnsafeNativeMethods.MEMORY_BASIC_INFORMATION memory_BASIC_INFORMATION = default(UnsafeNativeMethods.MEMORY_BASIC_INFORMATION);
			UnsafeNativeMethods.VirtualQuery(safeMemoryMappedViewHandle, ref memory_BASIC_INFORMATION, (IntPtr)Marshal.SizeOf(memory_BASIC_INFORMATION));
			ulong num4 = (ulong)memory_BASIC_INFORMATION.RegionSize;
			if ((memory_BASIC_INFORMATION.State & 8192U) != 0U || num4 < num3)
			{
				ulong value = (num3 == 0UL) ? num4 : num3;
				IntPtr intPtr = UnsafeNativeMethods.VirtualAlloc(safeMemoryMappedViewHandle, (UIntPtr)value, 4096, MemoryMappedFile.GetPageAccess(access));
				int lastWin32Error = Marshal.GetLastWin32Error();
				memory_BASIC_INFORMATION = default(UnsafeNativeMethods.MEMORY_BASIC_INFORMATION);
				UnsafeNativeMethods.VirtualQuery(safeMemoryMappedViewHandle, ref memory_BASIC_INFORMATION, (IntPtr)Marshal.SizeOf(memory_BASIC_INFORMATION));
				num4 = (ulong)memory_BASIC_INFORMATION.RegionSize;
			}
			if (size == 0L)
			{
				size = (long)(num4 - num);
			}
			safeMemoryMappedViewHandle.Initialize((ulong)(size + (long)num));
			return new MemoryMappedView(safeMemoryMappedViewHandle, (long)num, size, access);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000DFB0 File Offset: 0x0000C1B0
		[SecurityCritical]
		public unsafe void Flush(IntPtr capacity)
		{
			if (this.m_viewHandle != null)
			{
				byte* ptr = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this.m_viewHandle.AcquirePointer(ref ptr);
					bool flag = UnsafeNativeMethods.FlushViewOfFile(ptr, capacity);
					if (!flag)
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						bool flag2 = !flag && lastWin32Error == 33;
						int num = 0;
						while (flag2 && num < 15)
						{
							int millisecondsTimeout = 1 << num;
							Thread.Sleep(millisecondsTimeout);
							int num2 = 0;
							while (flag2 && num2 < 20)
							{
								flag = UnsafeNativeMethods.FlushViewOfFile(ptr, capacity);
								if (flag)
								{
									return;
								}
								Thread.Sleep(0);
								lastWin32Error = Marshal.GetLastWin32Error();
								flag2 = (lastWin32Error == 33);
								num2++;
							}
							num++;
						}
						__Error.WinIOError(lastWin32Error, string.Empty);
					}
				}
				finally
				{
					if (ptr != null)
					{
						this.m_viewHandle.ReleasePointer();
					}
				}
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000E084 File Offset: 0x0000C284
		[SecurityCritical]
		protected virtual void Dispose(bool disposing)
		{
			if (this.m_viewHandle != null && !this.m_viewHandle.IsClosed)
			{
				this.m_viewHandle.Dispose();
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000E0A6 File Offset: 0x0000C2A6
		[SecurityCritical]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000E0B5 File Offset: 0x0000C2B5
		internal bool IsClosed
		{
			[SecuritySafeCritical]
			get
			{
				return this.m_viewHandle == null || this.m_viewHandle.IsClosed;
			}
		}

		// Token: 0x04000539 RID: 1337
		private SafeMemoryMappedViewHandle m_viewHandle;

		// Token: 0x0400053A RID: 1338
		private long m_pointerOffset;

		// Token: 0x0400053B RID: 1339
		private long m_size;

		// Token: 0x0400053C RID: 1340
		private MemoryMappedFileAccess m_access;

		// Token: 0x0400053D RID: 1341
		private const int MaxFlushWaits = 15;

		// Token: 0x0400053E RID: 1342
		private const int MaxFlushRetriesPerWait = 20;
	}
}
