using System;
using System.Reflection.Metadata;
using System.Security;

namespace System.Reflection.Internal
{
	// Token: 0x02000079 RID: 121
	internal abstract class AbstractMemoryBlock : IDisposable
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000306 RID: 774
		public unsafe abstract byte* Pointer { [SecuritySafeCritical] get; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000307 RID: 775
		public abstract int Size { get; }

		// Token: 0x06000308 RID: 776 RVA: 0x00007C5F File Offset: 0x00005E5F
		[SecuritySafeCritical]
		public BlobReader GetReader()
		{
			return new BlobReader(this.Pointer, this.Size);
		}

		// Token: 0x06000309 RID: 777
		public abstract void Dispose();
	}
}
