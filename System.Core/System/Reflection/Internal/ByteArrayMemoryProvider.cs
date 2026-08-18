using System;
using System.Collections.Immutable;
using System.IO;
using System.Security;
using System.Threading;

namespace System.Reflection.Internal
{
	// Token: 0x0200007B RID: 123
	internal sealed class ByteArrayMemoryProvider : MemoryBlockProvider
	{
		// Token: 0x0600030F RID: 783 RVA: 0x00007CBC File Offset: 0x00005EBC
		public ByteArrayMemoryProvider(ImmutableArray<byte> array)
		{
			this._array = array;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00007CCB File Offset: 0x00005ECB
		protected override void Dispose(bool disposing)
		{
			PinnedObject pinnedObject = Interlocked.Exchange<PinnedObject>(ref this._pinned, null);
			if (pinnedObject == null)
			{
				return;
			}
			pinnedObject.Dispose();
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00007CE4 File Offset: 0x00005EE4
		public override int Size
		{
			get
			{
				return this._array.Length;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00007CFF File Offset: 0x00005EFF
		public ImmutableArray<byte> Array
		{
			get
			{
				return this._array;
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00007D07 File Offset: 0x00005F07
		protected override AbstractMemoryBlock GetMemoryBlockImpl(int start, int size)
		{
			return new ByteArrayMemoryBlock(this, start, size);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00007D11 File Offset: 0x00005F11
		public override Stream GetStream(out StreamConstraints constraints)
		{
			constraints = new StreamConstraints(null, 0L, this.Size);
			return new ImmutableMemoryStream(this._array);
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00007D34 File Offset: 0x00005F34
		internal unsafe byte* Pointer
		{
			[SecurityCritical]
			get
			{
				if (this._pinned == null)
				{
					PinnedObject pinnedObject = new PinnedObject(this._array.UnderlyingArray);
					if (Interlocked.CompareExchange<PinnedObject>(ref this._pinned, pinnedObject, null) != null)
					{
						pinnedObject.Dispose();
					}
				}
				return this._pinned.Pointer;
			}
		}

		// Token: 0x0400047A RID: 1146
		private readonly ImmutableArray<byte> _array;

		// Token: 0x0400047B RID: 1147
		private PinnedObject _pinned;
	}
}
