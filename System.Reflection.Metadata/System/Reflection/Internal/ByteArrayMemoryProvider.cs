using System;
using System.Collections.Immutable;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Reflection.Internal
{
	// Token: 0x02000154 RID: 340
	internal sealed class ByteArrayMemoryProvider : MemoryBlockProvider
	{
		// Token: 0x06000AA9 RID: 2729 RVA: 0x0001E835 File Offset: 0x0001CA35
		public ByteArrayMemoryProvider(ImmutableArray<byte> array)
		{
			this.array = array;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0001E844 File Offset: 0x0001CA44
		~ByteArrayMemoryProvider()
		{
			this.Dispose(false);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0001E874 File Offset: 0x0001CA74
		protected override void Dispose(bool disposing)
		{
			if (this._pinned != null)
			{
				this._pinned.Value.Free();
				this._pinned = null;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x0001E898 File Offset: 0x0001CA98
		public override int Size
		{
			get
			{
				return this.array.Length;
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0001E8B3 File Offset: 0x0001CAB3
		protected override AbstractMemoryBlock GetMemoryBlockImpl(int start, int size)
		{
			return new ByteArrayMemoryBlock(this, start, size);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0001E8BD File Offset: 0x0001CABD
		public override Stream GetStream(out StreamConstraints constraints)
		{
			constraints = new StreamConstraints(null, 0L, this.Size);
			return new ImmutableMemoryStream(this.array);
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x0001E8E0 File Offset: 0x0001CAE0
		internal unsafe byte* Pointer
		{
			get
			{
				if (this._pinned == null)
				{
					StrongBox<GCHandle> strongBox = new StrongBox<GCHandle>(GCHandle.Alloc(ImmutableByteArrayInterop.DangerousGetUnderlyingArray(this.array), GCHandleType.Pinned));
					if (Interlocked.CompareExchange<StrongBox<GCHandle>>(ref this._pinned, strongBox, null) != null)
					{
						strongBox.Value.Free();
					}
				}
				return (byte*)((void*)this._pinned.Value.AddrOfPinnedObject());
			}
		}

		// Token: 0x040008F0 RID: 2288
		internal readonly ImmutableArray<byte> array;

		// Token: 0x040008F1 RID: 2289
		private StrongBox<GCHandle> _pinned;
	}
}
