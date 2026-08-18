using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x0200012F RID: 303
	internal struct VectorEncoder
	{
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x0001CF8B File Offset: 0x0001B18B
		public BlobBuilder Builder { get; }

		// Token: 0x060009EB RID: 2539 RVA: 0x0001CF93 File Offset: 0x0001B193
		public VectorEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0001CF9C File Offset: 0x0001B19C
		public LiteralsEncoder Count(int count)
		{
			this.Builder.WriteUInt32((uint)count);
			return new LiteralsEncoder(this.Builder);
		}
	}
}
