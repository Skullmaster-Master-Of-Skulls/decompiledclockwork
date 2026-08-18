using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x0200013D RID: 317
	internal struct MethodBodiesEncoder
	{
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0001D5DB File Offset: 0x0001B7DB
		public BlobBuilder Builder { get; }

		// Token: 0x06000A45 RID: 2629 RVA: 0x0001D5E3 File Offset: 0x0001B7E3
		public MethodBodiesEncoder(BlobBuilder builder = null)
		{
			if (builder == null)
			{
				builder = new BlobBuilder(256);
			}
			if (builder.Count % 4 != 0)
			{
				throw new ArgumentException("Builder has to be aligned to 4 byte boundary", "builder");
			}
			this.Builder = builder;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0001D615 File Offset: 0x0001B815
		public MethodBodyEncoder AddMethodBody(int maxStack = 8, int exceptionRegionCount = 0, StandaloneSignatureHandle localVariablesSignature = default(StandaloneSignatureHandle), MethodBodyAttributes attributes = MethodBodyAttributes.InitLocals)
		{
			if ((ushort)maxStack > 65535)
			{
				throw new ArgumentOutOfRangeException("maxStack");
			}
			if (exceptionRegionCount < 0)
			{
				throw new ArgumentOutOfRangeException("exceptionRegionCount");
			}
			return new MethodBodyEncoder(this.Builder, (ushort)maxStack, exceptionRegionCount, localVariablesSignature, attributes);
		}
	}
}
