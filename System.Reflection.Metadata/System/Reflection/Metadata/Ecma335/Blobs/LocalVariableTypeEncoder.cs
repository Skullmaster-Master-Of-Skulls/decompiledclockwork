using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x02000127 RID: 295
	internal struct LocalVariableTypeEncoder
	{
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0001CD6D File Offset: 0x0001AF6D
		public BlobBuilder Builder { get; }

		// Token: 0x060009C5 RID: 2501 RVA: 0x0001CD75 File Offset: 0x0001AF75
		public LocalVariableTypeEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0001CD7E File Offset: 0x0001AF7E
		public CustomModifiersEncoder CustomModifiers()
		{
			return new CustomModifiersEncoder(this.Builder);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0001CD8B File Offset: 0x0001AF8B
		public SignatureTypeEncoder Type(bool isByRef = false, bool isPinned = false)
		{
			if (isPinned)
			{
				this.Builder.WriteByte(69);
			}
			if (isByRef)
			{
				this.Builder.WriteByte(16);
			}
			return new SignatureTypeEncoder(this.Builder);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0001CDB8 File Offset: 0x0001AFB8
		public void TypedReference()
		{
			this.Builder.WriteByte(22);
		}
	}
}
