using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x020001A4 RID: 420
	public class X9FieldID : Asn1Encodable
	{
		// Token: 0x06001014 RID: 4116 RVA: 0x0005D3B7 File Offset: 0x0005C3B7
		public X9FieldID(BigInteger primeP)
		{
			this.id = X9ObjectIdentifiers.PrimeField;
			this.parameters = new DerInteger(primeP);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0005D3D8 File Offset: 0x0005C3D8
		public X9FieldID(int m, int k1, int k2, int k3)
		{
			this.id = X9ObjectIdentifiers.CharacteristicTwoField;
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				new DerInteger(m)
			});
			if (k2 == 0)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					X9ObjectIdentifiers.TPBasis,
					new DerInteger(k1)
				});
			}
			else
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					X9ObjectIdentifiers.PPBasis,
					new DerSequence(new Asn1Encodable[]
					{
						new DerInteger(k1),
						new DerInteger(k2),
						new DerInteger(k3)
					})
				});
			}
			this.parameters = new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0005D484 File Offset: 0x0005C484
		internal X9FieldID(Asn1Sequence seq)
		{
			this.id = (DerObjectIdentifier)seq[0];
			this.parameters = (Asn1Object)seq[1];
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x0005D4B0 File Offset: 0x0005C4B0
		public DerObjectIdentifier Identifier
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x0005D4B8 File Offset: 0x0005C4B8
		public Asn1Object Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0005D4C0 File Offset: 0x0005C4C0
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.id,
				this.parameters
			});
		}

		// Token: 0x04000BDD RID: 3037
		private readonly DerObjectIdentifier id;

		// Token: 0x04000BDE RID: 3038
		private readonly Asn1Object parameters;
	}
}
