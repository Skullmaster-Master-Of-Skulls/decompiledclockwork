using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x0200012C RID: 300
	internal struct LiteralEncoder
	{
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x0001CE94 File Offset: 0x0001B094
		public BlobBuilder Builder { get; }

		// Token: 0x060009DB RID: 2523 RVA: 0x0001CE9C File Offset: 0x0001B09C
		public LiteralEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0001CEA5 File Offset: 0x0001B0A5
		public VectorEncoder Vector()
		{
			return new VectorEncoder(this.Builder);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0001CEB2 File Offset: 0x0001B0B2
		public void TaggedVector(out CustomAttributeArrayTypeEncoder arrayType, out VectorEncoder vector)
		{
			arrayType = new CustomAttributeArrayTypeEncoder(this.Builder);
			vector = new VectorEncoder(this.Builder);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0001CED6 File Offset: 0x0001B0D6
		public ScalarEncoder Scalar()
		{
			return new ScalarEncoder(this.Builder);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0001CEE3 File Offset: 0x0001B0E3
		public void TaggedScalar(out CustomAttributeElementTypeEncoder type, out ScalarEncoder scalar)
		{
			type = new CustomAttributeElementTypeEncoder(this.Builder);
			scalar = new ScalarEncoder(this.Builder);
		}
	}
}
