using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x02000133 RID: 307
	internal struct NamedArgumentTypeEncoder
	{
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0001D079 File Offset: 0x0001B279
		public BlobBuilder Builder { get; }

		// Token: 0x060009F8 RID: 2552 RVA: 0x0001D081 File Offset: 0x0001B281
		public NamedArgumentTypeEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0001D08A File Offset: 0x0001B28A
		public CustomAttributeElementTypeEncoder ScalarType()
		{
			return new CustomAttributeElementTypeEncoder(this.Builder);
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0001D097 File Offset: 0x0001B297
		public void Object()
		{
			this.Builder.WriteByte(81);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0001D0A6 File Offset: 0x0001B2A6
		public CustomAttributeArrayTypeEncoder SZArray()
		{
			return new CustomAttributeArrayTypeEncoder(this.Builder);
		}
	}
}
