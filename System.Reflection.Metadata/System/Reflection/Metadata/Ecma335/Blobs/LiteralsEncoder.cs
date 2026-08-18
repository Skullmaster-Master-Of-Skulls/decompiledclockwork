using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x0200012E RID: 302
	internal struct LiteralsEncoder
	{
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0001CF6D File Offset: 0x0001B16D
		public BlobBuilder Builder { get; }

		// Token: 0x060009E7 RID: 2535 RVA: 0x0001CF75 File Offset: 0x0001B175
		public LiteralsEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0001CF7E File Offset: 0x0001B17E
		public LiteralEncoder AddLiteral()
		{
			return new LiteralEncoder(this.Builder);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x000031EB File Offset: 0x000013EB
		public void EndLiterals()
		{
		}
	}
}
