using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x02000128 RID: 296
	internal struct ParameterTypeEncoder
	{
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x0001CDC7 File Offset: 0x0001AFC7
		public BlobBuilder Builder { get; }

		// Token: 0x060009CA RID: 2506 RVA: 0x0001CDCF File Offset: 0x0001AFCF
		public ParameterTypeEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0001CDD8 File Offset: 0x0001AFD8
		public CustomModifiersEncoder CustomModifiers()
		{
			return new CustomModifiersEncoder(this.Builder);
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0001CDE5 File Offset: 0x0001AFE5
		public SignatureTypeEncoder Type(bool isByRef = false)
		{
			if (isByRef)
			{
				this.Builder.WriteByte(16);
			}
			return new SignatureTypeEncoder(this.Builder);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0001CE02 File Offset: 0x0001B002
		public void TypedReference()
		{
			this.Builder.WriteByte(22);
		}
	}
}
