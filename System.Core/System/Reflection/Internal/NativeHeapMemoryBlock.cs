using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Reflection.Internal
{
	// Token: 0x02000080 RID: 128
	internal sealed class NativeHeapMemoryBlock : AbstractMemoryBlock
	{
		// Token: 0x0600032C RID: 812 RVA: 0x00007EAD File Offset: 0x000060AD
		internal NativeHeapMemoryBlock(int size)
		{
			this._data = new NativeHeapMemoryBlock.DisposableData(size);
			this._size = size;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00007EC8 File Offset: 0x000060C8
		public override void Dispose()
		{
			this._data.Dispose();
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00007ED5 File Offset: 0x000060D5
		public unsafe override byte* Pointer
		{
			[SecurityCritical]
			get
			{
				return this._data.Pointer;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00007EE2 File Offset: 0x000060E2
		public override int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x04000483 RID: 1155
		private readonly NativeHeapMemoryBlock.DisposableData _data;

		// Token: 0x04000484 RID: 1156
		private readonly int _size;

		// Token: 0x020002FD RID: 765
		private sealed class DisposableData : CriticalDisposableObject
		{
			// Token: 0x06001A5F RID: 6751 RVA: 0x00060BC8 File Offset: 0x0005EDC8
			[SecuritySafeCritical]
			public DisposableData(int size)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					this._pointer = Marshal.AllocHGlobal(size);
				}
			}

			// Token: 0x06001A60 RID: 6752 RVA: 0x00060C00 File Offset: 0x0005EE00
			[SecuritySafeCritical]
			protected override void Release()
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					IntPtr intPtr = Interlocked.Exchange(ref this._pointer, IntPtr.Zero);
					if (intPtr != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intPtr);
					}
				}
			}

			// Token: 0x170004E5 RID: 1253
			// (get) Token: 0x06001A61 RID: 6753 RVA: 0x00060C4C File Offset: 0x0005EE4C
			public unsafe byte* Pointer
			{
				[SecurityCritical]
				get
				{
					return (byte*)((void*)this._pointer);
				}
			}

			// Token: 0x04000DFC RID: 3580
			private IntPtr _pointer;
		}
	}
}
