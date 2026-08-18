using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020002B5 RID: 693
	public class PolicyMappings : Asn1Encodable
	{
		// Token: 0x06001A35 RID: 6709 RVA: 0x0009B2F4 File Offset: 0x0009A2F4
		public PolicyMappings(Asn1Sequence seq)
		{
			this.seq = seq;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0009B304 File Offset: 0x0009A304
		public PolicyMappings(Hashtable mappings)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			foreach (object obj in mappings.Keys)
			{
				string text = (string)obj;
				string identifier = (string)mappings[text];
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerSequence(new Asn1Encodable[]
					{
						new DerObjectIdentifier(text),
						new DerObjectIdentifier(identifier)
					})
				});
			}
			this.seq = new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x0009B3BC File Offset: 0x0009A3BC
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x04001176 RID: 4470
		private readonly Asn1Sequence seq;
	}
}
