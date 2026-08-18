using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x02000126 RID: 294
	internal struct LocalVariablesEncoder
	{
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x0001CD4F File Offset: 0x0001AF4F
		public BlobBuilder Builder { get; }

		// Token: 0x060009C1 RID: 2497 RVA: 0x0001CD57 File Offset: 0x0001AF57
		public LocalVariablesEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0001CD60 File Offset: 0x0001AF60
		public LocalVariableTypeEncoder AddVariable()
		{
			return new LocalVariableTypeEncoder(this.Builder);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x000031EB File Offset: 0x000013EB
		public void EndVariables()
		{
		}
	}
}
