using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Smime
{
	// Token: 0x02000404 RID: 1028
	public class SmimeCapabilitiesAttribute : AttributeX509
	{
		// Token: 0x06002318 RID: 8984 RVA: 0x000D82A4 File Offset: 0x000D72A4
		public SmimeCapabilitiesAttribute(SmimeCapabilityVector capabilities) : base(SmimeAttributes.SmimeCapabilities, new DerSet(new DerSequence(capabilities.ToAsn1EncodableVector())))
		{
		}
	}
}
