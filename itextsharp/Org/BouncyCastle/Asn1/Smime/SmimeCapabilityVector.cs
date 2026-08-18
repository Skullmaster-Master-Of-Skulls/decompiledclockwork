using System;

namespace Org.BouncyCastle.Asn1.Smime
{
	// Token: 0x02000518 RID: 1304
	public class SmimeCapabilityVector
	{
		// Token: 0x06002C94 RID: 11412 RVA: 0x0010F1B8 File Offset: 0x0010E1B8
		public void AddCapability(DerObjectIdentifier capability)
		{
			this.capabilities.Add(new Asn1Encodable[]
			{
				new DerSequence(capability)
			});
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x0010F1E4 File Offset: 0x0010E1E4
		public void AddCapability(DerObjectIdentifier capability, int value)
		{
			this.capabilities.Add(new Asn1Encodable[]
			{
				new DerSequence(new Asn1Encodable[]
				{
					capability,
					new DerInteger(value)
				})
			});
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x0010F224 File Offset: 0x0010E224
		public void AddCapability(DerObjectIdentifier capability, Asn1Encodable parameters)
		{
			this.capabilities.Add(new Asn1Encodable[]
			{
				new DerSequence(new Asn1Encodable[]
				{
					capability,
					parameters
				})
			});
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x0010F25C File Offset: 0x0010E25C
		public Asn1EncodableVector ToAsn1EncodableVector()
		{
			return this.capabilities;
		}

		// Token: 0x04001EAB RID: 7851
		private readonly Asn1EncodableVector capabilities = new Asn1EncodableVector(new Asn1Encodable[0]);
	}
}
