using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x02000132 RID: 306
	internal struct NamedArgumentsEncoder
	{
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0001D013 File Offset: 0x0001B213
		public BlobBuilder Builder { get; }

		// Token: 0x060009F4 RID: 2548 RVA: 0x0001D01B File Offset: 0x0001B21B
		public NamedArgumentsEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0001D024 File Offset: 0x0001B224
		public void AddArgument(bool isField, out NamedArgumentTypeEncoder typeEncoder, out NameEncoder name, out LiteralEncoder literal)
		{
			this.Builder.WriteByte(isField ? 83 : 84);
			typeEncoder = new NamedArgumentTypeEncoder(this.Builder);
			name = new NameEncoder(this.Builder);
			literal = new LiteralEncoder(this.Builder);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x000031EB File Offset: 0x000013EB
		public void EndArguments()
		{
		}
	}
}
