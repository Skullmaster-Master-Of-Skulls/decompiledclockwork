using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x02000131 RID: 305
	internal struct CustomAttributeNamedArgumentsEncoder
	{
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0001CFD4 File Offset: 0x0001B1D4
		public BlobBuilder Builder { get; }

		// Token: 0x060009F1 RID: 2545 RVA: 0x0001CFDC File Offset: 0x0001B1DC
		public CustomAttributeNamedArgumentsEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0001CFE5 File Offset: 0x0001B1E5
		public NamedArgumentsEncoder Count(int count)
		{
			if ((ushort)count > 65535)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.Builder.WriteUInt16((ushort)count);
			return new NamedArgumentsEncoder(this.Builder);
		}
	}
}
