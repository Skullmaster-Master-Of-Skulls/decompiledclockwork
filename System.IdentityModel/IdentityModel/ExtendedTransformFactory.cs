using System;

namespace System.IdentityModel
{
	// Token: 0x02000040 RID: 64
	internal class ExtendedTransformFactory : StandardTransformFactory
	{
		// Token: 0x06000259 RID: 601 RVA: 0x0000A4DA File Offset: 0x000086DA
		private ExtendedTransformFactory()
		{
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000A4E2 File Offset: 0x000086E2
		internal new static ExtendedTransformFactory Instance
		{
			get
			{
				return ExtendedTransformFactory.instance;
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000A4E9 File Offset: 0x000086E9
		public override Transform CreateTransform(string transformAlgorithmUri)
		{
			if (transformAlgorithmUri == XD.XmlSignatureDictionary.EnvelopedSignature.Value)
			{
				return new EnvelopedSignatureTransform();
			}
			return base.CreateTransform(transformAlgorithmUri);
		}

		// Token: 0x04000170 RID: 368
		private static ExtendedTransformFactory instance = new ExtendedTransformFactory();
	}
}
