using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x02000130 RID: 304
	internal struct NameEncoder
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x0001CFB5 File Offset: 0x0001B1B5
		public BlobBuilder Builder { get; }

		// Token: 0x060009EE RID: 2542 RVA: 0x0001CFBD File Offset: 0x0001B1BD
		public NameEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0001CFC6 File Offset: 0x0001B1C6
		public void Name(string name)
		{
			this.Builder.WriteSerializedString(name);
		}
	}
}
