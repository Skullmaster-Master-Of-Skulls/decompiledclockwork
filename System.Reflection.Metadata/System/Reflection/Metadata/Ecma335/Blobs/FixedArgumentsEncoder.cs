using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x0200012B RID: 299
	internal struct FixedArgumentsEncoder
	{
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0001CE76 File Offset: 0x0001B076
		public BlobBuilder Builder { get; }

		// Token: 0x060009D7 RID: 2519 RVA: 0x0001CE7E File Offset: 0x0001B07E
		public FixedArgumentsEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0001CE87 File Offset: 0x0001B087
		public LiteralEncoder AddArgument()
		{
			return new LiteralEncoder(this.Builder);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x000031EB File Offset: 0x000013EB
		public void EndArguments()
		{
		}
	}
}
