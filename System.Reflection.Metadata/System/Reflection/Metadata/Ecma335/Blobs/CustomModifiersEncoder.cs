using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x02000138 RID: 312
	internal struct CustomModifiersEncoder
	{
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0001D41F File Offset: 0x0001B61F
		public BlobBuilder Builder { get; }

		// Token: 0x06000A33 RID: 2611 RVA: 0x0001D427 File Offset: 0x0001B627
		public CustomModifiersEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0001D430 File Offset: 0x0001B630
		public CustomModifiersEncoder AddModifier(bool isOptional, EntityHandle typeDefRefSpec)
		{
			if (isOptional)
			{
				this.Builder.WriteByte(32);
			}
			else
			{
				this.Builder.WriteByte(31);
			}
			this.Builder.WriteCompressedInteger(CodedIndex.ToTypeDefOrRefOrSpec(typeDefRefSpec));
			return this;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000031EB File Offset: 0x000013EB
		public void EndModifiers()
		{
		}
	}
}
