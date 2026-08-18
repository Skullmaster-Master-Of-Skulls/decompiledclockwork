using System;
using System.Security.Cryptography;

namespace System.IdentityModel
{
	// Token: 0x020000AC RID: 172
	internal class StandardTransformFactory : TransformFactory
	{
		// Token: 0x06000543 RID: 1347 RVA: 0x0001413F File Offset: 0x0001233F
		protected StandardTransformFactory()
		{
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00014147 File Offset: 0x00012347
		internal static StandardTransformFactory Instance
		{
			get
			{
				return StandardTransformFactory.instance;
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00014150 File Offset: 0x00012350
		public override Transform CreateTransform(string transformAlgorithmUri)
		{
			if (transformAlgorithmUri == "http://www.w3.org/2001/10/xml-exc-c14n#")
			{
				return new ExclusiveCanonicalizationTransform();
			}
			if (transformAlgorithmUri == "http://www.w3.org/2001/10/xml-exc-c14n#WithComments")
			{
				return new ExclusiveCanonicalizationTransform(false, true);
			}
			if (transformAlgorithmUri == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#STR-Transform")
			{
				return new StrTransform();
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("UnsupportedTransformAlgorithm")));
		}

		// Token: 0x040004C3 RID: 1219
		private static StandardTransformFactory instance = new StandardTransformFactory();
	}
}
