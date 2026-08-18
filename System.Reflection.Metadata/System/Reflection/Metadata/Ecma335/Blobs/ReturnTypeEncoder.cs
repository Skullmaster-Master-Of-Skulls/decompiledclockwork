using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x0200013A RID: 314
	internal struct ReturnTypeEncoder
	{
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0001D535 File Offset: 0x0001B735
		public BlobBuilder Builder { get; }

		// Token: 0x06000A3A RID: 2618 RVA: 0x0001D53D File Offset: 0x0001B73D
		public ReturnTypeEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0001D546 File Offset: 0x0001B746
		public CustomModifiersEncoder CustomModifiers()
		{
			return new CustomModifiersEncoder(this.Builder);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0001D553 File Offset: 0x0001B753
		public SignatureTypeEncoder Type(bool isByRef = false)
		{
			if (isByRef)
			{
				this.Builder.WriteByte(16);
			}
			return new SignatureTypeEncoder(this.Builder);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0001D570 File Offset: 0x0001B770
		public void TypedReference()
		{
			this.Builder.WriteByte(22);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0001D57F File Offset: 0x0001B77F
		public void Void()
		{
			this.Builder.WriteByte(1);
		}
	}
}
