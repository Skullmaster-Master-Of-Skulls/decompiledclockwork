using System;
using System.Collections.Generic;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x0200012A RID: 298
	internal sealed class Pkcs9ExtensionRequest : X501Attribute
	{
		// Token: 0x060009CB RID: 2507 RVA: 0x000237C7 File Offset: 0x000219C7
		internal Pkcs9ExtensionRequest(IEnumerable<X509Extension> extensions) : base("1.2.840.113549.1.9.14", Pkcs9ExtensionRequest.EncodeAttribute(extensions))
		{
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x000237DC File Offset: 0x000219DC
		private static byte[] EncodeAttribute(IEnumerable<X509Extension> extensions)
		{
			if (extensions == null)
			{
				throw new ArgumentNullException("extensions");
			}
			List<byte[][]> list = new List<byte[][]>();
			foreach (X509Extension x509Extension in extensions)
			{
				if (x509Extension != null)
				{
					list.Add(x509Extension.SegmentedEncodedX509Extension());
				}
			}
			return DerEncoder.ConstructSequence(list.ToArray());
		}
	}
}
