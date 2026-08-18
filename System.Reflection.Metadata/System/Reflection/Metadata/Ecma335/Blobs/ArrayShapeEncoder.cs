using System;
using System.Collections.Immutable;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x02000139 RID: 313
	internal struct ArrayShapeEncoder
	{
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x0001D468 File Offset: 0x0001B668
		public BlobBuilder Builder { get; }

		// Token: 0x06000A37 RID: 2615 RVA: 0x0001D470 File Offset: 0x0001B670
		public ArrayShapeEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0001D47C File Offset: 0x0001B67C
		public void Shape(int rank, ImmutableArray<int> sizes, ImmutableArray<int> lowerBounds)
		{
			this.Builder.WriteCompressedInteger(rank);
			this.Builder.WriteCompressedInteger(sizes.Length);
			foreach (int value in sizes)
			{
				this.Builder.WriteCompressedInteger(value);
			}
			if (lowerBounds.IsDefault)
			{
				this.Builder.WriteCompressedInteger(rank);
				for (int i = 0; i < rank; i++)
				{
					this.Builder.WriteCompressedSignedInteger(0);
				}
				return;
			}
			this.Builder.WriteCompressedInteger(lowerBounds.Length);
			foreach (int value2 in lowerBounds)
			{
				this.Builder.WriteCompressedSignedInteger(value2);
			}
		}
	}
}
