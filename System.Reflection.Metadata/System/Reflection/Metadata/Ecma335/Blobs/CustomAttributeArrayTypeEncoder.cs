using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x02000134 RID: 308
	internal struct CustomAttributeArrayTypeEncoder
	{
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0001D0B3 File Offset: 0x0001B2B3
		public BlobBuilder Builder { get; }

		// Token: 0x060009FD RID: 2557 RVA: 0x0001D0BB File Offset: 0x0001B2BB
		public CustomAttributeArrayTypeEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0001D0C4 File Offset: 0x0001B2C4
		public void ObjectArray()
		{
			this.Builder.WriteByte(29);
			this.Builder.WriteByte(81);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0001D0E0 File Offset: 0x0001B2E0
		public CustomAttributeElementTypeEncoder ElementType()
		{
			this.Builder.WriteByte(29);
			return new CustomAttributeElementTypeEncoder(this.Builder);
		}
	}
}
