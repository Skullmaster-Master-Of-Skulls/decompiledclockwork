using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Reflection.Internal
{
	// Token: 0x0200007F RID: 127
	internal sealed class MemoryMappedFileBlock : AbstractMemoryBlock
	{
		// Token: 0x06000328 RID: 808 RVA: 0x00007E6D File Offset: 0x0000606D
		[SecurityCritical]
		internal MemoryMappedFileBlock(IDisposable accessor, SafeBuffer safeBuffer, long offset, int size)
		{
			this._data = new MemoryMappedFileBlock.DisposableData(accessor, safeBuffer, offset);
			this._size = size;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00007E8B File Offset: 0x0000608B
		public override void Dispose()
		{
			this._data.Dispose();
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00007E98 File Offset: 0x00006098
		public unsafe override byte* Pointer
		{
			[SecurityCritical]
			get
			{
				return this._data.Pointer;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00007EA5 File Offset: 0x000060A5
		public override int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x04000481 RID: 1153
		private readonly MemoryMappedFileBlock.DisposableData _data;

		// Token: 0x04000482 RID: 1154
		private readonly int _size;

		// Token: 0x020002FC RID: 764
		private sealed class DisposableData : CriticalDisposableObject
		{
			// Token: 0x06001A5C RID: 6748 RVA: 0x00060B14 File Offset: 0x0005ED14
			[SecuritySafeCritical]
			public unsafe DisposableData(IDisposable accessor, SafeBuffer safeBuffer, long offset)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					byte* ptr = null;
					safeBuffer.AcquirePointer(ref ptr);
					this._accessor = accessor;
					this._safeBuffer = safeBuffer;
					this._pointer = ptr + offset;
				}
			}

			// Token: 0x06001A5D RID: 6749 RVA: 0x00060B64 File Offset: 0x0005ED64
			[SecuritySafeCritical]
			protected override void Release()
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					SafeBuffer safeBuffer = Interlocked.Exchange<SafeBuffer>(ref this._safeBuffer, null);
					if (safeBuffer != null)
					{
						safeBuffer.ReleasePointer();
					}
					IDisposable disposable = Interlocked.Exchange<IDisposable>(ref this._accessor, null);
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				this._pointer = null;
			}

			// Token: 0x170004E4 RID: 1252
			// (get) Token: 0x06001A5E RID: 6750 RVA: 0x00060BC0 File Offset: 0x0005EDC0
			public unsafe byte* Pointer
			{
				[SecurityCritical]
				get
				{
					return this._pointer;
				}
			}

			// Token: 0x04000DF9 RID: 3577
			private IDisposable _accessor;

			// Token: 0x04000DFA RID: 3578
			[SecurityCritical]
			private SafeBuffer _safeBuffer;

			// Token: 0x04000DFB RID: 3579
			[SecurityCritical]
			private unsafe byte* _pointer;
		}
	}
}
