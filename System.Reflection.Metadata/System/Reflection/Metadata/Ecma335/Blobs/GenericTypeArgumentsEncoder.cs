using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x0200012A RID: 298
	internal struct GenericTypeArgumentsEncoder
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x0001CE58 File Offset: 0x0001B058
		public BlobBuilder Builder { get; }

		// Token: 0x060009D3 RID: 2515 RVA: 0x0001CE60 File Offset: 0x0001B060
		public GenericTypeArgumentsEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0001CE69 File Offset: 0x0001B069
		public SignatureTypeEncoder AddArgument()
		{
			return new SignatureTypeEncoder(this.Builder);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x000031EB File Offset: 0x000013EB
		public void EndArguments()
		{
		}
	}
}
